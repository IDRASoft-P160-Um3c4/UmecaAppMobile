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

			services.createVerificationTest();
			StatusVerification statusVerification1 = services.statusVerificationfindByCode(Constants.VERIFICATION_STATUS_AUTHORIZED);
			StatusVerification statusVerification2 = services.statusVerificationfindByCode(Constants.VERIFICATION_STATUS_MEETING_COMPLETE);
			StatusCase sc = services.statusCasefindByCode(Constants.CASE_STATUS_VERIFICATION);
			var result = db.Query<MeetingTblDto> (
				"SELECT cs.id_case as 'CaseId',cs.id_folder as 'IdFolder',im.name as 'Name',im.lastname_p as 'LastNameP',im.lastname_m as 'LastNameM',"
				+" im.birth_date as 'DateBirth', im.gender as 'Gender', csm.name as 'StatusCode', csm.description as 'Description'"
				+" FROM verification as me "
				+" left JOIN case_detention as cs ON me.id_case = cs.id_case "
				+" left JOIN imputed as im ON im.id_meeting = me.id_meeting "
				+" left JOIN cat_status_verification as csm ON csm.id_status = me.id_status "
				+" WHERE me.id_status in (?,?) "
				//				+" and me.id_reviewer = 2 "
				+" AND cs.id_status = ?; ", statusVerification1.Id, statusVerification2.Id, sc.Id);

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
			var sources = db.Table<SourceVerification> ().Where (sv => (sv.VerificationId == verification.Id && sv.Visible == true && sv.IsAuthorized == true && sv.IdCase == idCase && sv.DateComplete == null)).ToList ();
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
			int idCase = source.IdCase;
			var verification = db.Table<Verification> ().Where (ver => ver.CaseDetentionId == idCase).FirstOrDefault ();

			var result = db.Query<VerificationMeetingSourceDto> (
				"SELECT cs.id_folder as 'IdFolder', im.id_imputed as 'ImputedId',im.name as 'Name',im.lastname_p as 'LastNameP',im.lastname_m as 'LastNameM'"
				+" ,im.birth_date as 'BirthDate', im.gender as 'Gender'"
				+" ,im.fonetic_string as 'FoneticString', im.cel_phone as 'CelPhone'"
				+" ,im.years_marital_status as 'YearsMaritalStatus', im.id_marital_status as 'MaritalStatusId'"
				+" ,im.boys as 'Boys', im.dependent_boys as 'DependentBoys'"
				+" ,im.id_country as 'BirthCountry', im.birth_municipality as 'BirthMunicipality'"
				+" ,im.birth_state as 'BirthState', im.birth_location as 'BirthLocation'"
				+" ,im.nickname as 'Nickname', im.id_location as 'LocationId'"
				+" ,me.id_meeting as 'MeetingId'"
				+" ,me.id_reviewer as 'ReviewerId', me.id_status as 'StatusMeetingId'"
				+" ,me.comment_refernce as 'CommentReference', me.comment_job as 'CommentJob'"
				+" ,me.comment_school as 'CommentSchool', me.comment_country as 'CommentCountry'"
				+" ,me.comment_home as 'CommentHome', me.comment_drug as 'CommentDrug'"
				+" ,me.date_create as 'DateCreate', me.date_terminate as 'DateTerminate'"
				//				+", csm.status as 'StatusCode', csm.description as 'Description'"
				+" FROM meeting as me "
				+" left JOIN case_detention as cs ON me.id_case = cs.id_case "
				+" left JOIN imputed as im ON im.id_meeting = me.id_meeting "
				//				+" left JOIN cat_status_meeting as csm ON csm.id_status = me.id_status "
				//				+" and me.id_reviewer = 2 "
				+" where cs.id_case = ?; ", idCase).FirstOrDefault();
			result.CaseId = idCase;

			result.ageString = services.calculateAge (result.BirthDate);

			var domiciliosImputado= db.Table<ImputedHome> ().Where (im => im.MeetingId == result.MeetingId).ToList ();
			if(domiciliosImputado!=null){
				result.JsonDomicilios = domiciliosImputado;
			}
			foreach(ImputedHome h in result.JsonDomicilios){
				h.Schedule = "";
				var horario = db.Table<Schedule> ().Where (sche=>sche.ImputedHomeId==h.Id).ToList ();
				if (horario != null && horario.Count > 0) {
					foreach(Schedule es in horario){
						h.Schedule += "<tr><td class='element-center'>"+es.Day+"</td><td class='element-center'>"+es.Start+"</td><td class='element-center'>"+es.End+"</td></tr>";
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
			if(personsSocNet!=null){
				result.JsonPersonSN = personsSocNet;
			}else{
				result.JsonPersonSN = null;
			}

			//Reference
			var references = db.Table<Reference> ().Where (sn=>sn.MeetingId==result.MeetingId).ToList ();
			if(references!=null){
				result.JsonReferences = references;
			}else{
				result.JsonReferences = null;
			}

			//Laboral History
			var trabajos = db.Table<Job> ().Where (sn=>sn.MeetingId==result.MeetingId).ToList ();
			if(trabajos!=null){
				result.JsonJobs = trabajos;
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


			//DROGAS
			var drogas = db.Table<Drug> ().Where (sn=>sn.MeetingId==result.MeetingId).ToList ();
			if(drogas!=null){
				result.JsonDrugs = drogas;
			}else{
				result.JsonDrugs = null;
			}


			if(escuelaUtlActual!=null){
				result.SchoolId = escuelaUtlActual.Id;
				var schedule = db.Table<Schedule>().Where(sc=>sc.SchoolId==escuelaUtlActual.Id).ToList();
				if(schedule!=null){
					result.ScheduleSchool = schedule;
				}
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



		public void IndexVerificacion()
		{

			services.createVerificationTest();
			StatusMeeting statusMeeting1 = services.statusMeetingfindByCode(Constants.S_MEETING_INCOMPLETE);
			StatusMeeting statusMeeting2 = services.statusMeetingfindByCode(Constants.S_MEETING_INCOMPLETE_LEGAL);
			StatusCase sc = services.statusCasefindByCode(Constants.CASE_STATUS_MEETING);

			var result = db.Query<MeetingTblDto> (
				"SELECT cs.id_case as 'CaseId',cs.id_folder as 'IdFolder',im.name as 'Name',im.lastname_p as 'LastNameP',im.lastname_m as 'LastNameM',"
				+" im.birth_date as 'DateBirth', im.gender as 'Gender', csm.status as 'StatusCode', csm.description as 'Description'"
				+" FROM meeting as me "
				+" left JOIN case_detention as cs ON me.id_case = cs.id_case "
				+" left JOIN imputed as im ON im.id_meeting = me.id_meeting "
				+" left JOIN cat_status_meeting as csm ON csm.id_status = me.id_status "
				+" WHERE me.id_status in (?,?) "
				//				+" and me.id_reviewer = 2 "
				+" AND cs.id_status = ?; ", statusMeeting1.Id,statusMeeting2.Id, sc.Id);

			Console.WriteLine ("carga de casos "+result.Count);

			var temp = new MeetingList{Model = result};
			var pagestring = "nada que ver";
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
		}




	}
}

