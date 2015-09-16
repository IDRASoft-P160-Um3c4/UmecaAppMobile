using System;
using PortableRazor;
using System.IO;
using SQLite.Net;
//query toList()
using System.Linq;
using SQLiteNetExtensions.Extensions;
using Newtonsoft.Json;
//listas
using System.Collections.Generic;
//cript
using BCrypt;


//TODO DONE StatusMeeting, Imputed, Case
	
namespace UmecaApp
{
	public class VerificationController : Java.Lang.Object//: ControllerBase
	{

		IHybridWebView webView;
		readonly SQLiteConnection db;
		CatalogServiceController services;

		String JsonCountrys;
		String JsonStates;
		String JsonMunycipality;
		String JsonElection;
		String JsonActivities;

		public VerificationController(IHybridWebView webView, SQLiteConnection dbConection)
		{
			this.webView = webView;
			this.db = dbConection;
			services = new CatalogServiceController (db);	

			services.CreateStatusCaseCatalog ();
			services.CreateStatusMeetingCatalog ();
			services.CreateElection ();

			services.CreateCountryCatalog ();
			services.CreateStateCatalog ();
			services.CreateMunicipalityCatalog ();
			db.Commit ();

			this.JsonCountrys =JsonConvert.SerializeObject(services.CountryFindAllOrderByName ());
			this.JsonStates = JsonConvert.SerializeObject(services.StateFindAllOrderByName ());
			this.JsonMunycipality = JsonConvert.SerializeObject(services.MunicipalityFindAllOrderByName ());
			this.JsonElection = JsonConvert.SerializeObject (services.ElectionFindAll());
			this.JsonActivities = "[{'id':1,'name':'Laborales','specification':true},{'id':2,'name':'Escolares','specification':true},{'id':3,'name':'Religiosas','specification':true},{'id':4,'name':'Deportivas','specification':true},{'id':5,'name':'Reuniones sociales','specification':true},{'id':6,'name':'Reuniones familiares','specification':true},{'id':7,'name':'Otras','specification':true},{'id':8,'name':'Ninguna','specification':false}]";

		}



		public void Index()
		{	
			StatusVerification statusVerification1 = services.statusVerificationfindByCode(Constants.VERIFICATION_STATUS_AUTHORIZED);
			StatusVerification statusVerification2 = services.statusVerificationfindByCode(Constants.VERIFICATION_STATUS_MEETING_COMPLETE);
			StatusCase sc = services.statusCasefindByCode(Constants.CASE_STATUS_VERIFICATION);
			StatusCase sc1 = services.statusCasefindByCode(Constants.ST_CASE_TABLET_ASSIGNED);

			db.CreateTable<User> ();
			var usrList = db.Table<User> ().ToList ();
			User reviewer = usrList.FirstOrDefault ();
			int revId = 0;
			if (reviewer != null && reviewer.Id > 0) {
				revId = reviewer.Id;
			}

			var result = db.Query<MeetingTblDto> (
				"SELECT cs.id_case as 'CaseId',cs.id_folder as 'IdFolder',"
				+" csm.description as 'StatusCode', csm.description as 'Description' , me.id_reviewer as 'ReviewerId' "
				+" FROM verification as me "
				+" left JOIN case_detention as cs ON me.id_case = cs.id_case "
				+" left JOIN cat_status_verification as csm ON csm.id_status = me.id_status "
				+" WHERE me.id_status in (?,?) "
				+" and me.id_reviewer = ? "
				+" AND cs.id_status in (?,?); ", statusVerification1.Id, statusVerification2.Id, revId , sc.Id,sc1.Id );

			var counter = 0;
			for (counter = 0; counter < result.Count; counter++) {
				var caseis = result [counter].CaseId;
				var me = db.Table<Meeting> ().Where (met => met.CaseDetentionId == caseis).FirstOrDefault ();
				if (me != null) {
					var imp = db.Table<Imputed> ().Where (iputad=>iputad.MeetingId == me.Id).FirstOrDefault ();
					if (imp != null) {
						result [counter].Name = imp.Name;
						result [counter].LastNameP = imp.LastNameP;
						result [counter].LastNameM = imp.LastNameM;
						result [counter].Gender = imp.Gender;
					}
				}
			}

			var temp = new VerificationList{Model = result};
			var pagestring = "nada que ver";
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
		}

		public void IndexFuentes(int idCase)
		{
			db.CreateTable<User> ();
			var usrList = db.Table<User> ().ToList ();
			User reviewer = usrList.FirstOrDefault ();
			int revId = 0;
			if (reviewer != null && reviewer.Id > 0) {
				revId = reviewer.Id;
			}

			var caso = db.Table<Case> ().Where(cs => cs .Id == idCase).FirstOrDefault ();
			var meeting = db.Table<Meeting> ().Where (me => me.CaseDetentionId == idCase).FirstOrDefault ();
			var imputado = db.Table<Imputed> ().Where (im => im.MeetingId == meeting.Id).FirstOrDefault ();
			var verification = db.Table<Verification> ().Where (ver => ver.CaseDetentionId == idCase && ver.ReviewerId == revId).FirstOrDefault ();
			var sources = db.Table<SourceVerification> ().Where (sv => (sv.VerificationId == verification.Id && sv.Visible == true 
//				&& sv.IsAuthorized == true 
				&& sv.CaseRequestId == idCase && sv.DateComplete == null)).ToList ();

			var alls = db.Table<SourceVerification> ().ToList ();
			var entrevistador = db.Table<User> ().Where (u => u.Id.Equals(meeting.ReviewerId)).FirstOrDefault ();
			var result = new SourcesTblDto ();
			result.Age=services.calculateAge(imputado.BirthDate);
			result.CaseId = idCase;
			result.FullnameImputed = imputado.Name+" "+imputado.LastNameP+" "+imputado.LastNameM;
			result.SourceListJson = JsonConvert.SerializeObject (sources);
			result.tEnd = meeting.DateTerminate.ToString();
			result.tStart = meeting.DateCreate.ToString();
			result.IdFolder = caso.IdFolder;
			if (entrevistador == null) {
				result.reviewerFullname = "";
			} else {
				result.reviewerFullname = entrevistador.fullname;
			}
			var temp = new VerificationSourceList{Model = result};
			var pagestring = "nada que ver";  
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
		}

		public void ValidationMeetingBySource(int idSource)
		{
			var source = db.Table<SourceVerification> ().Where (sv => sv.Id==idSource ).FirstOrDefault ();
			int idCase = (int)source.CaseRequestId;
			User Reviewer = db.Table<User> ().First ();
			Case cs = db.Table<Case> ().Where(cas=>cas.Id==idCase).ToList ().First ();
			Meeting baseMe = db.Table<Meeting> ().Where (meet => meet.CaseDetentionId == cs.Id).First ();
			Imputed baseImp = db.Table<Imputed> ().Where (imput => imput.MeetingId == baseMe.Id).First ();
			var verification = db.Table<Verification> ().Where (ver => ver.CaseDetentionId == idCase).FirstOrDefault ();
			var result = new VerificationMeetingSourceDto ();
			result.IdFolder = cs.IdFolder;
			result.ImputedId = baseImp.Id;
			result.Name = baseImp.Name;
			result.LastNameP = baseImp.LastNameP;
			result.LastNameM = baseImp.LastNameM;
			result.BirthDate = baseImp.BirthDate;
			result.Gender = baseImp.Gender;
			result.FoneticString = baseImp.FoneticString;
			result.CelPhone = baseImp.CelPhone;
			result.YearsMaritalStatus = baseImp.YearsMaritalStatus;
			result.MaritalStatusId = baseImp.MaritalStatusId;
			result.Boys = baseImp.Boys;
			result.DependentBoys = baseImp.DependentBoys;
			result.Location = baseImp.Location;
			result.LocationId = baseImp.LocationId;
			result.BirthLocation = baseImp.BirthLocation;
			result.BirthCountry = baseImp.BirthCountry;
			result.BirthState = baseImp.BirthState;
			result.BirthMunicipality = baseImp.BirthMunicipality;
			result.Nickname = baseImp.Nickname;
			result.MeetingId = baseMe.Id;
			result.ReviewerId = Reviewer.Id;
			result.StatusMeetingId = baseMe.StatusMeetingId;
			result.CommentReference = baseMe.CommentReference;
			result.CommentCountry = baseMe.CommentCountry;
			result.CommentDrug = baseMe.CommentDrug;
			result.CommentHome = baseMe.CommentHome;
			result.CommentJob = baseMe.CommentJob;
			result.CommentSchool = baseMe.CommentSchool;
			result.DateCreate = baseMe.DateCreate;
			result.DateTerminate = baseMe.DateTerminate;

			result.CaseId = idCase;

			result.ageString = services.calculateAge (result.BirthDate);

			var parantesco = db.Table<Relationship> ().ToList ();
			result.ListaDeRelaciones = parantesco;
			var elecciones = db.Table<Election> ().ToList ();
			result.ListaDeElection = elecciones;
			var documentosIdentificacion = db.Table<DocumentType> ().ToList ();
			result.ListaDeIdentificaciones = documentosIdentificacion;
			var drogasCatalog = db.Table<DrugType> ().ToList ();
			result.ListaDeDrogas = drogasCatalog;
			var periodoCatalog = db.Table<Periodicity> ().ToList ();
			result.ListaDePeriodicidad = periodoCatalog;
			var registerCatalog = db.Table<RegisterType> ().ToList ();
			result.ListaDeRegisterType = registerCatalog;

			result.JsonDomicilios = new List<DomiciliosVerificationDto> ();
			var domiciliosImputado= db.Table<ImputedHome> ().Where (im => im.MeetingId == result.MeetingId).ToList ();
			if(domiciliosImputado!=null && domiciliosImputado.Count>0){
				var domVerified = new List<DomiciliosVerificationDto> ();
				foreach (ImputedHome i in domiciliosImputado) {
					var home = new DomiciliosVerificationDto (i);
					if (i.webId != null && i.webId != 0L) {
						home.Id = (int) i.webId;
					}
					home.ScheduleList = db.Table<Schedule> ().Where (sc => sc.ImputedHomeId == home.Id).ToList ();
					domVerified.Add (home);
				}
				result.JsonDomicilios = domVerified;
			}

			foreach(DomiciliosVerificationDto h in result.JsonDomicilios){
				h.Schedule = "";
				var horario = db.Table<Schedule> ().Where (sche=>sche.ImputedHomeId==h.Id).ToList ();
				if (horario != null && horario.Count > 0) {
					foreach(Schedule skdl in horario){
						h.Schedule += "<tr>";
						h.Schedule += "<td>"+skdl.Day+"</td>";
						h.Schedule += "<td>"+skdl.Start+"</td>";
						h.Schedule += "<td>"+skdl.End+"</td>";
						h.Schedule += "</ tr>";
					}
				}
			}

			var SE = db.Table<SocialEnvironment> ().Where(s=> s.MeetingId==result.MeetingId).FirstOrDefault();
			if(SE!=null){
				result.PhysicalCondition = SE.physicalCondition;
				result.comment = SE.comment;
				var ActList = db.Table<RelActivity> ().Where(s=> s.SocialEnvironmentId==SE.Id).ToList();
				if(ActList!=null&&ActList.Count>0){
					result.Activities = JsonConvert.SerializeObject(ActList);
				}
			}
			//socialNetworkComment
			var socialNetComent = db.Table<SocialNetwork> ().Where (s => s.MeetingId == result.MeetingId).FirstOrDefault ();
			if (socialNetComent != null) {
				result.CommentSocialNetwork = socialNetComent.Comment;
			} else {		
				socialNetComent = new SocialNetwork ();
				socialNetComent.Comment = "";
				socialNetComent.MeetingId = result.MeetingId ?? 0;
				db.Insert (socialNetComent);
			}

			//PersonSocialNetwork
			var personsSocNet = db.Table<PersonSocialNetwork> ().Where (sn=>sn.SocialNetworkId==socialNetComent.Id).ToList ();
			if(personsSocNet!=null&&personsSocNet.Count>0){
				var socialList = new List<PersonSocialNetworkVerificationDto> ();
				foreach(PersonSocialNetwork psn in personsSocNet){
					var nuev = new PersonSocialNetworkVerificationDto (psn);
					if (psn.webId != null && psn.webId != 0L) {
						nuev.Id = (int) psn.webId;
					}

					socialList.Add (nuev);
				}
				result.JsonPersonSN = socialList;
			}else{
				result.JsonPersonSN = null;
			}

			//Reference
			var references = db.Table<Reference> ().Where (sn=>sn.MeetingId==result.MeetingId).ToList ();
			if(references!=null && references.Count>0){
				for (var cont = 0; cont < references.Count; cont++) {
					if (references[cont].webId != null && references[cont].webId != 0L) {
						references[cont].Id = (int) references[cont].webId;
					}
				}
				result.JsonReferences = references;
			}else{
				result.JsonReferences = null;
			}

			//Laboral History
			var trabajos = db.Table<Job> ().Where (sn=>sn.MeetingId==result.MeetingId).ToList ();
			if(trabajos!=null && trabajos.Count>0){
				var dtojob = new List<JobVerificationDto> ();
				foreach(Job j in trabajos){
					var trabajo = new JobVerificationDto (j);
					if (j.webId != null && j.webId != 0L) {
						trabajo.Id = (int) j.webId;
					}
					trabajo.ScheduleList = db.Table<Schedule> ().Where (sc => sc.JobId == trabajo.Id).ToList ()??new List<Schedule>();
					dtojob.Add (trabajo);
				}
				result.JsonJobs = dtojob;
			}else{
				result.JsonJobs = null;
			}

			//school history
			var escuelaUtlActual = db.Table<School> ().Where (sc=>sc.MeetingId == result.MeetingId).FirstOrDefault ();
			if (escuelaUtlActual != null) {
				result.SchoolAddress = escuelaUtlActual.Address;
				result.SchoolBlock = escuelaUtlActual.block;
				result.SchoolDegreeId = escuelaUtlActual.DegreeId.GetValueOrDefault ();
				result.SchoolName = escuelaUtlActual.Name;
				result.SchoolPhone = escuelaUtlActual.Phone;
				result.SchoolSpecification = escuelaUtlActual.Specification;
			}

			if(escuelaUtlActual!=null){
				result.SchoolId = escuelaUtlActual.Id;
				var schedule = db.Table<Schedule>().Where(sc=>sc.SchoolId==escuelaUtlActual.Id).ToList();
				if(schedule!=null){
					result.ScheduleSchool = schedule;
				}
			}

			//DROGAS
			var drogas = db.Table<Drug> ().Where (sn=>sn.MeetingId==result.MeetingId).ToList ();
			if(drogas!=null && drogas.Count>0){
				for (var cont2 = 0; cont2 < drogas.Count; cont2++) {
					if (drogas[cont2].webId != null && drogas[cont2].webId != 0L) {
						drogas[cont2].Id = (int) drogas[cont2].webId;
					}
				}
				result.JsonDrugs = drogas;
			}else{
				result.JsonDrugs = null;
			}



			//leave country
			var leaveActual = db.Table<LeaveCountry> ().Where (lv => lv.MeetingId == result.MeetingId).FirstOrDefault ();
			if (leaveActual != null) {
				result.OfficialDocumentationId = leaveActual.OfficialDocumentationId;
				result.LivedCountryId = leaveActual.LivedCountryId;
				result.timeAgo = leaveActual.timeAgo;
				result.Reason = leaveActual.Reason;
				result.FamilyAnotherCountryId = leaveActual.FamilyAnotherCountryId;
				result.CountryId = leaveActual.CountryId;
				result.State = leaveActual.State;
				result.Media = leaveActual.Media;
				result.Address = leaveActual.Address;
				result.ImmigrationDocumentId = leaveActual.ImmigrationDocumentId;
				result.RelationshipId = leaveActual.RelationshipId;
				result.TimeResidence = leaveActual.TimeResidence;
				result.SpecficationImmigranDoc = leaveActual.SpecficationImmigranDoc;
				result.SpecificationRelationship = leaveActual.SpecificationRelationship;
				result.CommunicationFamilyId = leaveActual.CommunicationFamilyId;
			}
			//End leave country


			string output = JsonConvert.SerializeObject(result);
			result.JsonMeeting = output;


			result.JsonCountrys = this.JsonCountrys;
			result.JsonStates = this.JsonStates;
			result.JsonMunycipality = this.JsonMunycipality;
			result.JsonElection = this.JsonElection;
			result.JsonActivities = this.JsonActivities;

			//result source
			result.SourceAddress = source.Address;
			result.SourceAge = source.Age;
			result.SourceId = source.Id;
			result.SourceName = source.FullName;
			result.SourcePhone = source.Phone;
			var SourceRelationship = db.Table<Relationship> ().Where (sore=> sore.Id==source.RelationshipId).FirstOrDefault ();
			if (SourceRelationship.Equals (null)) {
				result.SourceRelationshipString = "";
			} else {
				result.SourceRelationshipString = SourceRelationship.Name;
			}

			var temp = new VerificacionInterview{Model = result };
			//			var temp = new NewMeeting{Model = new EntrevistaTabla{Name="nombre" , DateBirthString=DateTime.Today.ToString("yyyy/mm/dd")} };
			var pagestring = "nada que ver";
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
		}

		public void  AddVerificationSource(int idCase)
		{
			try{

				db.CreateTable<User> ();
				var usrList = db.Table<User> ().ToList ();
				User reviewer = usrList.FirstOrDefault ();
				int revId = 0;
				if (reviewer != null && reviewer.Id > 0) {
					revId = reviewer.Id;
				}

				var casoId = int.Parse (idCase.ToString ());
				var caso = db.Table<Case>().Where(cs=>cs.Id==casoId).FirstOrDefault();
//				var usuario = db.Table<User>().FirstOrDefault();
				var verificacion = db.Table<Verification>().Where(s=>s.CaseDetentionId == casoId
					&& s.ReviewerId==revId).FirstOrDefault();
				var dto = new ModelContainer ();
				dto.Reference = idCase.ToString ();
				SourceVerification mdl = new SourceVerification ();
				mdl.IdCase = casoId;
				mdl.IsAuthorized = false;
				mdl.Visible = true;
				mdl.VerificationId = verificacion.Id;
				dto.JsonModel = JsonConvert.SerializeObject (mdl);
				var temp = new NewVerificationSource{ Model = dto };
				var pagestring = "nada que ver";
				pagestring = temp.GenerateString ();
				webView.LoadHtmlString (pagestring);
			}catch(Exception e){
				db.Rollback ();
				Console.WriteLine ("catched exception in MeetingController method PersonSocialNetwork");
				Console.WriteLine("Exception message :::>"+e.Message);
			}
			finally{
				db.Commit ();
			}
		}

	}
}

