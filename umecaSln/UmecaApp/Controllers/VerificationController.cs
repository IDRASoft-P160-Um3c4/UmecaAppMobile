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
//			Console.WriteLine ("quesque hasheado---> "+Crypto.HashPassword("99630110"));
//			Console.WriteLine ("quesque pera---> "+Crypto.HashPassword("pera"));
//			Console.WriteLine ("quesque hija de puerca---> "+Crypto.HashPassword("hija de puerca"));
//			Console.WriteLine ("quesque bastardo hijo de puta---> "+Crypto.HashPassword("bastardo hijo de puta"));
//			Console.WriteLine ("quesque puta la pinche mierda---> "+Crypto.HashPassword("puta la pinche mierda"));
//			Console.WriteLine ("quesque coñooo---> "+Crypto.HashPassword("coñooo"));
			services.createVerificationTest();
			StatusVerification statusVerification1 = services.statusVerificationfindByCode(Constants.VERIFICATION_STATUS_AUTHORIZED);
			StatusVerification statusVerification2 = services.statusVerificationfindByCode(Constants.VERIFICATION_STATUS_MEETING_COMPLETE);
			StatusCase sc = services.statusCasefindByCode(Constants.CASE_STATUS_VERIFICATION);
			StatusCase sc1 = services.statusCasefindByCode(Constants.ST_CASE_TABLET_ASSIGNED);
			var result = db.Query<MeetingTblDto> (
				"SELECT cs.id_case as 'CaseId',cs.id_folder as 'IdFolder',im.name as 'Name',im.lastname_p as 'LastNameP',im.lastname_m as 'LastNameM',"
				+" im.birth_date as 'DateBirth', im.gender as 'Gender', csm.description as 'StatusCode', csm.description as 'Description'"
				+" FROM verification as me "
				+" left JOIN case_detention as cs ON me.id_case = cs.id_case "
				+" left JOIN meeting as met ON met.id_case = cs.id_case "
				+" left JOIN imputed as im ON im.id_meeting = met.id_meeting "
				+" left JOIN cat_status_verification as csm ON csm.id_status = me.id_status "
				+" WHERE me.id_status in (?,?) "
				//				+" and me.id_reviewer = 2 "
				+" AND cs.id_status in (?,?); ", statusVerification1.Id, statusVerification2.Id, sc.Id,sc1.Id );

			Console.WriteLine ("result.count> {0}", result.Count);
			var temp = new VerificationList{Model = result};
			var pagestring = "nada que ver";
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
		}

		public void IndexFuentes(int idCase)
		{
			var caso = db.Table<Case> ().Where(cs => cs .Id == idCase).FirstOrDefault ();
			var meeting = db.Table<Meeting> ().Where (me => me.CaseDetentionId == idCase).FirstOrDefault ();
			var imputado = db.Table<Imputed> ().Where (im => im.MeetingId == meeting.Id).FirstOrDefault ();
			var verification = db.Table<Verification> ().Where (ver => ver.CaseDetentionId == idCase).FirstOrDefault ();
			var sources = db.Table<SourceVerification> ().Where (sv => (sv.VerificationId == verification.Id && sv.Visible == true 
//				&& sv.IsAuthorized == true 
				&& sv.CaseRequestId == idCase && sv.DateComplete == null)).ToList ();
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
			Console.WriteLine ("source--"+result.SourceListJson);
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

//			var result = db.Query<VerificationMeetingSourceDto> (
//				"SELECT cs.id_folder as 'IdFolder', im.id_imputed as 'ImputedId',im.name as 'Name',im.lastname_p as 'LastNameP',im.lastname_m as 'LastNameM'"
//				+" ,im.birth_date as 'BirthDate', im.gender as 'Gender'"
//				+" ,im.fonetic_string as 'FoneticString', im.cel_phone as 'CelPhone'"
//				+" ,im.years_marital_status as 'YearsMaritalStatus', im.id_marital_status as 'MaritalStatusId'"
//				+" ,im.boys as 'Boys', im.dependent_boys as 'DependentBoys'"
//				+" ,im.id_country as 'BirthCountry', im.birth_municipality as 'BirthMunicipality'"
//				+" ,im.birth_state as 'BirthState', im.birth_location as 'BirthLocation'"
//				+" ,im.nickname as 'Nickname', im.id_location as 'LocationId'"
//				+" ,me.id_meeting as 'MeetingId'"
//				+" ,me.id_reviewer as 'ReviewerId', me.id_status as 'StatusMeetingId'"
//				+" ,me.comment_refernce as 'CommentReference', me.comment_job as 'CommentJob'"
//				+" ,me.comment_school as 'CommentSchool', me.comment_country as 'CommentCountry'"
//				+" ,me.comment_home as 'CommentHome', me.comment_drug as 'CommentDrug'"
//				+" ,me.date_create as 'DateCreate', me.date_terminate as 'DateTerminate'"
//				//				+", csm.status as 'StatusCode', csm.description as 'Description'"
//				+" FROM meeting as me "
//				+" left JOIN case_detention as cs ON me.id_case = cs.id_case "
//				+" left JOIN imputed as im ON im.id_meeting = me.id_meeting "
//				//				+" left JOIN cat_status_meeting as csm ON csm.id_status = me.id_status "
//				//				+" and me.id_reviewer = 2 "
//				+" where cs.id_case = ?; ", idCase).FirstOrDefault();
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
					socialList.Add (nuev);
				}
				result.JsonPersonSN = socialList;
			}else{
				result.JsonPersonSN = null;
			}

			//Reference
			var references = db.Table<Reference> ().Where (sn=>sn.MeetingId==result.MeetingId).ToList ();
			if(references!=null && references.Count>0){
				result.JsonReferences = references;
			}else{
				result.JsonReferences = null;
				/////DELETE
				result.JsonReferences = new List<Reference>();
				for (var a = 0; a < 2; a++) {
					var socialPerson = new Reference ();
					socialPerson.Address = "secc 3" + a + " # 8" + a + " lt. " + a + "4 rio de luz ecatepec";
					socialPerson.Age = 27 + a;
					socialPerson.block = true;
					socialPerson.DocumentTypeId = 1;
					socialPerson.Id = 2 + a;
					socialPerson.FullName = "Axel Rosa";
					socialPerson.Phone = "2461809"+a;
					socialPerson.RelationshipId = 18;
					socialPerson.SpecificationDocumentType = "documento firmado ante notario";
					socialPerson.SpecificationRelationship = "relative";
					result.JsonReferences.Add(socialPerson);
				}
				/////DELETE
			}

			//Laboral History
			var trabajos = db.Table<Job> ().Where (sn=>sn.MeetingId==result.MeetingId).ToList ();
			if(trabajos!=null && trabajos.Count>0){
				var dtojob = new List<JobVerificationDto> ();
				foreach(Job j in trabajos){
					var trabajo = new JobVerificationDto (j);
					trabajo.ScheduleList = db.Table<Schedule> ().Where (sc => sc.JobId == trabajo.Id).ToList ()??new List<Schedule>();
					dtojob.Add (trabajo);
				}
				result.JsonJobs = dtojob;
			}else{
				result.JsonJobs = null;
				/////DELETE
				result.JsonJobs = new List<JobVerificationDto>();
				for (var a = 0; a < 2; a++) {
					var socialPerson = new JobVerificationDto ();
					socialPerson.block = true;
					socialPerson.RegisterTypeId = 1;
					socialPerson.Id = 2 + a;
					socialPerson.Start = DateTime.Today;
					socialPerson.End = DateTime.Today;
					socialPerson.MeetingId = result.MeetingId??0;
					socialPerson.Address = "22";
					socialPerson.Company = "compañiera";
					socialPerson.NameHead = "18";
					socialPerson.Phone = "white kush";
					socialPerson.Post = "wgwhwhw";
					socialPerson.StartPrev = DateTime.Today;
					result.JsonJobs.Add(socialPerson);
				}
				/////DELETE
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
				result.JsonDrugs = drogas;
			}else{
//				result.JsonDrugs = null;
				/////DELETE
				result.JsonDrugs = new List<Drug>();
				for (var a = 0; a < 2; a++) {
					var socialPerson = new Drug ();
					socialPerson.block = true;
					socialPerson.DrugTypeId = 1;
					socialPerson.Id = 2 + a;
					socialPerson.LastUse = DateTime.Today;
					socialPerson.MeetingId = result.MeetingId??0;
					socialPerson.OnsetAge = "22";
					socialPerson.PeriodicityId = 2;
					socialPerson.Quantity = "18";
					socialPerson.Specification = "white kush";
					socialPerson.SpecificationPeriodicity = "cuando lo puedo pagar";
					result.JsonDrugs.Add(socialPerson);
				}
				/////DELETE
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
			Console.WriteLine (JsonConvert.SerializeObject (result));
			var temp = new VerificacionInterview{Model = result };
			//			var temp = new NewMeeting{Model = new EntrevistaTabla{Name="nombre" , DateBirthString=DateTime.Today.ToString("yyyy/mm/dd")} };
			var pagestring = "nada que ver";
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
		}

		public void  AddVerificationSource(int idCase)
		{
			try{
				var casoId = int.Parse (idCase.ToString ());
				var caso = db.Table<Case>().Where(cs=>cs.Id==casoId).FirstOrDefault();
//				var usuario = db.Table<User>().FirstOrDefault();
				var verificacion = db.Table<Verification>().Where(s=>s.CaseDetentionId == casoId
//					&& s.ReviewerId==usuario.Id
				).FirstOrDefault();
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

