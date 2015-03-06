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
	public class MeetingController : Java.Lang.Object//: ControllerBase
	{

		IHybridWebView webView;
		readonly SQLiteConnection db;
		CatalogServiceController services;

		String JsonCountrys;
		String JsonStates;
		String JsonMunycipality;
		String JsonElection;
		String JsonActivities;

		public MeetingController(IHybridWebView webView, SQLiteConnection dbConection)
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

			services.createMeetingTest();
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

		public void  MeetingEditNew()
		{
			var temp = new NewMeeting{Model = new NewMeetingDto() };
//			var temp = new NewMeeting{Model = new EntrevistaTabla{Name="nombre" , DateBirthString=DateTime.Today.ToString("yyyy/mm/dd")} };
			var pagestring = "nada que ver";
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
		}

		public void AddMeeting([Bind]NewMeetingDto model) {
			Console.WriteLine ("AddMeeting");
			String validateCreateMsg = validateCreateMeeting(model);
			if (validateCreateMsg != null) {
				model.ResponseMessage = validateCreateMsg;
				var temp = new NewMeeting{ Model = model };
				var pagestring = "nada que ver";
				pagestring = temp.GenerateString ();
				webView.LoadHtmlString (pagestring);
			} else {
				int? idCase = createMeeting(model);
				int az = idCase.GetValueOrDefault ();
				//String Response = "Se ha guardado exitosamente";
				MeetingDatosPersonales (az);
			}
		}

		public String validateCreateMeeting(NewMeetingDto model) {
			if (model.DateBirth.HasValue) {
				int age = services.calculateAge(model.DateBirth.Value);
				if (age.CompareTo(18)<0) {
					return "El imputado debe tener m&aacute;s de 18 a&ntilde;os para continuar";
				}
			} else {
				return "Favor de ingresar la fecha de nacimiento del imputado.";
			}
			if (model.IdFolder != null) {
				var tableImputed = db.Query<Imputed>("select i.* from imputed as i "+
					"left join meeting as m on i.id_meeting=m.id_meeting " +
					"left join case_detention as c on c.id_case = m.id_case " +
					"where c.id_folder='feb-09-2015-02'");
				if(tableImputed!=null&& tableImputed.Count>0){
					foreach (var c in tableImputed) {
						Console.WriteLine ("c.fonetic__>"+c.FoneticString+"  c.birthdate==>"+c.BirthDate);
						Console.WriteLine ("m.fonetic__>"+services.getFoneticByName(model.Name, model.LastNameP, model.LastNameM)+"  m.birthdate==>"+model.DateBirth);
						if(c.FoneticString.Equals(services.getFoneticByName(model.Name, model.LastNameP, model.LastNameM))&&c.BirthDate.Equals(model.DateBirth)){
							return "El n&uacute;mero de carpeta de investigaci&oacute;n y el imputado ya se encuentran registrados.";
						}
					}
				}
			} else {
				return "Favor de ingresar el n&uacute;mero de carpeta de investigaci&oacute;n para continuar";
			}
			return null;
		}

		public int? createMeeting(NewMeetingDto imputed) {
			int? result = null;
			try {
				Case caseDetention = new Case();
				Imputed newImputed = new Imputed();
				newImputed.Name=imputed.Name.Trim();
				newImputed.LastNameP=imputed.LastNameP.Trim();
				newImputed.LastNameM=imputed.LastNameM.Trim();
				newImputed.FoneticString=services.getFoneticByName(imputed.Name, imputed.LastNameP, imputed.LastNameM);
				newImputed.Gender=imputed.Gender.GetValueOrDefault();
				newImputed.BirthDate=imputed.DateBirth.GetValueOrDefault();
				caseDetention.Status=services.statusCasefindByCode(Constants.CASE_STATUS_MEETING);
				caseDetention.StatusCaseId=services.statusCasefindByCode(Constants.CASE_STATUS_MEETING).Id;
				caseDetention.IdFolder=imputed.IdFolder;
				caseDetention.DateCreate=DateTime.Today;
				//caseDetention.setChangeArrangementType(false);
				// se agrega para poder contar si un caso cambia de MC a SCPP en algun formato de audiencia
				//caseDetention = caseRepository.save(caseDetention);
				db.InsertWithChildren (caseDetention);
				Meeting meeting = new Meeting();
				meeting.MeetingType=Constants.MEETING_PROCEDURAL_RISK;
				meeting.CaseDetentionId=caseDetention.Id;
				meeting.CaseDetention = caseDetention;
				StatusMeeting statusMeeting = services.statusMeetingfindByCode(Constants.S_MEETING_INCOMPLETE);
				meeting.StatusMeetingId=statusMeeting.Id;
				meeting.StatusMeeting=statusMeeting;
				//				meeting.ReviewerId=LoggedUserId(); TODO agrega al usuario asociado al dispositivo
				meeting.DateCreate=DateTime.Today;
				//				meeting = meetingRepository.save(meeting);
				db.InsertWithChildren (meeting);
				newImputed.MeetingId=meeting.Id;
				newImputed.Meeting = meeting;
				db.InsertWithChildren (newImputed);
				db.UpdateWithChildren (meeting);
				db.UpdateWithChildren (caseDetention);
				//				imputedRepository.save(imputed);
				result = caseDetention.Id;
			} catch (Exception e) {
				Console.WriteLine("e.Message>"+e.Message+"<< createMeeting");
			} 
			return result;
		}

		public void  MeetingDatosPersonales(int idCase)
		{
			if(idCase==0){
				idCase = db.Table<Case> ().FirstOrDefault().Id;
			}
			var result = db.Query<MeetingDatosPersonalesDto> (
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
			string output = JsonConvert.SerializeObject(result);
			result.JsonMeeting = output;


			result.JsonCountrys = this.JsonCountrys;
			result.JsonStates = this.JsonStates;
			result.JsonMunycipality = this.JsonMunycipality;
			result.JsonElection = this.JsonElection;
			result.JsonActivities = this.JsonActivities;

			var temp = new MeetingDatosPersonales{Model = result };
			//			var temp = new NewMeeting{Model = new EntrevistaTabla{Name="nombre" , DateBirthString=DateTime.Today.ToString("yyyy/mm/dd")} };
			var pagestring = "nada que ver";
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
		}

		public void SaveMeetingDatosPersonales([Bind]MeetingDatosPersonalesDto model) {
			var imputado = db.Get<Imputed>(model.ImputedId); 
			imputado.Name = model.Name;
			imputado.LastNameP = model.LastNameP;
			imputado.LastNameM = model.LastNameM;
			imputado.FoneticString = services.getFoneticByName(model.Name,model.LastNameP,model.LastNameM);
			imputado.Gender = model.Gender;
			imputado.CelPhone = model.CelPhone;
			imputado.YearsMaritalStatus = model.YearsMaritalStatus;
			imputado.MaritalStatusId = model.MaritalStatusId;
//			imputado.MaritalStatus = db.Get<MaritalStatus>(model.MaritalStatusId);
			imputado.Boys = model.Boys;
			imputado.DependentBoys = model.DependentBoys;
			imputado.BirthCountry = model.BirthCountry;
			imputado.BirthMunicipality = model.BirthMunicipality;
			imputado.BirthState = model.BirthState;
			imputado.BirthLocation = model.BirthLocation;
			imputado.Nickname = model.Nickname;
			imputado.LocationId = model.LocationId;

			db.Update(imputado);
			string output = JsonConvert.SerializeObject(model);
			model.JsonMeeting = output;
			model.JsonCountrys = this.JsonCountrys;
			model.JsonStates = this.JsonStates;
			model.JsonMunycipality = this.JsonMunycipality;
			model.JsonElection = this.JsonElection;
			model.JsonActivities = this.JsonActivities;

			var temp = new MeetingDatosPersonales{Model = model };
			var pagestring = "nada que ver";
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
		}




//		@Query("select a from Activity as a where a.isObsolete=false")
//		List<Activity> findNotObsolete();

//		public void validateMeeting(TerminateMeetingMessageDto t){
//			List<ImputedHome> imputedHomeList = getImputedHomes();
//			if(imputedHomeList== null || (imputedHomeList !=null && imputedHomeList.size()==0)){
//				List<String> result = new ArrayList<>();
//				result.add("Debe registrar al menos un domicilio del imputado.");
//				t.getGroupMessage().add(new GroupMessageMeetingDto("imputedHome",result));
//			}
//			List<PersonSocialNetwork> listPS = getSocialNetwork()==null? new ArrayList<PersonSocialNetwork>() :getSocialNetwork().getPeopleSocialNetwork();
//			List<Reference> referenceList = getReferences();
//			List<String> listMessSN = new ArrayList<>();
//			if ((referenceList==null || (referenceList != null && referenceList.size() == 0))) {
//				List<String> listMess = new ArrayList<>();
//				listMess.add("Para terminar la entrevista debe agragar al menos una referencia personal.");
//				t.getGroupMessage().add(new GroupMessageMeetingDto("reference",listMess));
//			}
//
//			if(socialNetwork==null|| (socialNetwork.getComment()==null || (socialNetwork.getComment()!=null && socialNetwork.getComment().equals("")))){
//				listMessSN.add(t.template.replace("entity","Las observaciones"));
//			}
//
//			if((listPS== null ||( listPS!= null && listPS.size()==0))){
//				listMessSN.add("Para terminar la entrevista debe agregar al menos una persona en su red social.");
//			}
//
//			if(listMessSN.size()>0){
//				t.getGroupMessage().add(new GroupMessageMeetingDto("socialNetwork",listMessSN));
//			}
//			List<Job> jobList = getJobs();
//			if(jobList==null || (jobList!=null && jobList.size()==0)){
//				List<String> result = new ArrayList<>();
//				result.add("Debe agregar al menos un empleo del imputado.");
//				t.getGroupMessage().add(new GroupMessageMeetingDto("job",result));
//			}
//			List<Drug> drugsList= getDrugs();
//			if(drugsList==null || (drugsList!=null && drugsList.size()==0)){
//				List<String> result = new ArrayList<>();
//				result.add("Debe agregar al menos una sustancia que consume el imputado. (En caso de no consumir sustancias seleccione otro y especifique ninguna)");
//				t.getGroupMessage().add(new GroupMessageMeetingDto("drug",result));
//			}
//		}

	}
}

