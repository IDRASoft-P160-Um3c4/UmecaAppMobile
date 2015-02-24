using System;
using PortableRazor;
using System.IO;
using SQLite.Net;
using System.Linq;
using SQLiteNetExtensions.Extensions;

//TODO DONE StatusMeeting, Imputed, Case

namespace UmecaApp
{
	public class MeetingController //: ControllerBase
	{

		IHybridWebView webView;
		readonly SQLiteConnection db;

		public MeetingController(IHybridWebView webView, SQLiteConnection dbConection)
		{
			this.webView = webView;
			this.db = dbConection;
		}

		public void Index()
		{
			//			Console.WriteLine ("Creating database, if it doesn't already exist");
			//			string dbPath = Path.Combine (
			//				                Environment.GetFolderPath (Environment.SpecialFolder.Personal),
			//				                "umeca.sqlite");
			//
			//			var db = new SQLiteConnection (dbPath);



			//			create new meeting
			db.DropTable<Case> ();
			db.CreateTable<Case> ();
			db.DropTable<Meeting> ();
			db.CreateTable<Meeting> ();
			db.DropTable<Imputed> ();
			db.CreateTable<Imputed> ();
//
			db.DropTable<StatusCase> ();
			db.CreateTable<StatusCase> ();

			StatusCase catalogo1  = new StatusCase(); catalogo1 .Description="Verificación finalizada"                                ; catalogo1                .Name="ST_CASE_VERIFICATION_COMPLETE";                           db.Insert(catalogo1 );
			StatusCase catalogo2  = new StatusCase(); catalogo2 .Description="Instrumento de evaluación finalizado"                   ; catalogo2                             .Name="ST_CASE_TECHNICAL_REVIEW_COMPLETE";          db.Insert(catalogo2 );
			StatusCase catalogo3  = new StatusCase(); catalogo3 .Description="Entrevista de riesgos procesales"                       ; catalogo3                         .Name="ST_CASE_MEETING";                                db.Insert(catalogo3 );
			StatusCase catalogo4  = new StatusCase(); catalogo4 .Description="Formato de audiencia finalizado."                       ; catalogo4                         .Name="ST_CASE_HEARING_FORMAT_END";                     db.Insert(catalogo4 );
			StatusCase catalogo5  = new StatusCase(); catalogo5 .Description="Validación de fuentes"                                  ; catalogo5              .Name="ST_CASE_SOURCE_VALIDATION";                                 db.Insert(catalogo5 );
			StatusCase catalogo6  = new StatusCase(); catalogo6 .Description="Entrevista de encuadre incompleta."                     ; catalogo6                           .Name="ST_CASE_FRAMING_MEETING_INCOMPLETE";           db.Insert(catalogo6 );
			StatusCase catalogo7  = new StatusCase(); catalogo7 .Description="Entrevista de encuadre completa."                       ; catalogo7                         .Name="ST_CASE_FRAMING_MEETING_COMPLETE";               db.Insert(catalogo7 );
			StatusCase catalogo8  = new StatusCase(); catalogo8 .Description="Nuevo caso."                                            ; catalogo8    .Name="ST_CASE_CONDITIONAL_REPRIEVE";                                        db.Insert(catalogo8 );
			StatusCase catalogo9  = new StatusCase(); catalogo9 .Description="En verificación"                                        ; catalogo9        .Name="ST_CASE_VERIFICATION";                                            db.Insert(catalogo9 );
			StatusCase catalogo10 = new StatusCase(); catalogo10.Description="Pre-cerrado por vinculación a proceso NO."              ; catalogo10                                 .Name="ST_CASE_PRE_CLOSED";                    db.Insert(catalogo10);
			StatusCase catalogo11 = new StatusCase(); catalogo11.Description="Cerrado."                                               ; catalogo11.Name="ST_CASE_CLOSED";                                                         db.Insert(catalogo11);
			StatusCase catalogo12 = new StatusCase(); catalogo12.Description="En solicitud pendiente"                                 ; catalogo12              .Name="ST_CASE_REQUEST";                                          db.Insert(catalogo12);
			StatusCase catalogo13 = new StatusCase(); catalogo13.Description="Edición de instrumento de evaluación autorizada"        ; catalogo13                                       .Name="ST_CASE_EDIT_TEC_REV";            db.Insert(catalogo13);
			StatusCase catalogo14 = new StatusCase(); catalogo14.Description="Caso eliminado en evaluación"                           ; catalogo14                    .Name="ST_CASE_OBSOLETE_EVALUATION";                        db.Insert(catalogo14);
			StatusCase catalogo15 = new StatusCase(); catalogo15.Description="Formato de audiencia incompleto."                       ; catalogo15                        .Name="ST_CASE_HEARING_FORMAT_INCOMPLETE";              db.Insert(catalogo15);
			StatusCase catalogo16 = new StatusCase(); catalogo16.Description="Instrumento de evaluación incompleto."                  ; catalogo16                             .Name="ST_CASE_TECHNICAL_REVIEW_INCOMPLETE";       db.Insert(catalogo16);
			StatusCase catalogo17 = new StatusCase(); catalogo17.Description="No judializado."                                        ; catalogo17       .Name="ST_CASE_NOT_PROSECUTE";                                           db.Insert(catalogo17);
			StatusCase catalogo18 = new StatusCase(); catalogo18.Description="No judicializado abierto."                              ; catalogo18                 .Name="ST_CASE_NOT_PROSECUTE_OPEN";                            db.Insert(catalogo18);
			StatusCase catalogo19 = new StatusCase(); catalogo19.Description="Cerrado por prisión preventiva / promesa del imputado." ; catalogo19                                              .Name="ST_CASE_PRISON_CLOSED";    db.Insert(catalogo19);
			StatusCase catalogo20 = new StatusCase(); catalogo20.Description="Caso eliminado en supervisión"                          ; catalogo20                     .Name="ST_CASE_OBSOLETE_SUPERVISION";                      db.Insert(catalogo20);
			StatusCase catalogo21 = new StatusCase(); catalogo21.Description="En solicitud pendiente"                                 ; catalogo21              .Name="ST_CASE_REQUEST_SUPERVISION";                              db.Insert(catalogo21);

			var tableStatusCase = db.Table<StatusCase>().ToList<StatusCase>();
			Console.WriteLine("StatusCase------------->");
			foreach(var c in tableStatusCase){
				Console.WriteLine("id:"+c.Id+", name:"+c.Name+", Descripcion:"+c.Description);
			}


						db.DropTable<StatusMeeting> ();
						db.CreateTable<StatusMeeting> ();


			StatusMeeting incomplete = new StatusMeeting ();
			incomplete.Status = "INCOMPLETE";
			incomplete.Description = "Entrevista incompleta";
			db.Insert (incomplete);

			StatusMeeting incompleteL = new StatusMeeting ();
			incompleteL.Status = "INCOMPLETE_LEGAL";
			incompleteL.Description = "Por agregar información legal";
			db.Insert (incompleteL);

			StatusMeeting complete = new StatusMeeting ();
			complete.Status = "COMPLETE";
			complete.Description = "Finalizado";
			db.Insert (complete);

			StatusMeeting completeV = new StatusMeeting ();
			completeV.Status = "COMPLETE_VERIFICATION";
			completeV.Description = "Verificación finalizada";
			db.Insert (completeV);

			StatusMeeting obsolete = new StatusMeeting ();
			obsolete.Status = "OBSOLETE";
			obsolete.Description = "Caso eliminado";
			db.Insert (obsolete);

			var tableStatusMeeting = db.Table<StatusMeeting>().ToList<StatusMeeting>();
			Console.WriteLine("StatusMeeting------------->");
			foreach(var c in tableStatusMeeting){
				Console.WriteLine("id:"+c.Id+", Status:"+c.Status+", Description:"+c.Description);
			}
//
				Case caseDetention = new Case();
				Imputed newImputed = new Imputed();
				newImputed.Name="axl".Trim();
				newImputed.LastNameP="sanz".Trim();
				newImputed.LastNameM="perz".Trim();
				newImputed.FoneticString=getFoneticByName("axl", "sanz", "perz");
				newImputed.Gender=true;
				newImputed.BirthDate=DateTime.Today.AddYears(-27);
				caseDetention.Status=statusCasefindByCode(Constants.CASE_STATUS_MEETING);
				caseDetention.StatusCaseId=statusCasefindByCode(Constants.CASE_STATUS_MEETING).Id;
				caseDetention.IdFolder="nuevo Folder example";
				caseDetention.DateCreate=DateTime.Today;
			db.InsertWithChildren (caseDetention);
				Meeting meeting = new Meeting();
				meeting.MeetingType=Constants.MEETING_PROCEDURAL_RISK;
				meeting.CaseDetentionId=caseDetention.Id;
				meeting.CaseDetention = caseDetention;
				StatusMeeting statusMeeting = statusMeetingfindByCode(Constants.S_MEETING_INCOMPLETE);
				meeting.StatusMeetingId=statusMeeting.Id;
				meeting.StatusMeeting=statusMeeting;
				meeting.DateCreate=DateTime.Today;
			db.InsertWithChildren (meeting);
				newImputed.MeetingId=meeting.Id;
				newImputed.Meeting = meeting;
			db.InsertWithChildren (newImputed);
			db.UpdateWithChildren (meeting);
			db.UpdateWithChildren (caseDetention);
			Console.WriteLine("caseDetention.Id> {0}",caseDetention.Id);
			var ese = db.GetWithChildren<Meeting> (meeting.Id);
			Console.WriteLine("done execution>");
			Console.WriteLine("ese.Id>"+ese.Id+"  _ese.CaseDetentionId=>"+ese.CaseDetentionId);


			//el imputado
			//			
			//			db.CreateTable<SocialEnvironment> ();
			//			db.CreateTable<Reference> ();
			//			db.CreateTable<Job> ();
			//			db.CreateTable<School> ();
			//			db.CreateTable<LeaveCountry> ();
			//			db.CreateTable<SocialNetwork> ();

			StatusMeeting statusMeeting1 = statusMeetingfindByCode(Constants.S_MEETING_INCOMPLETE);
			StatusMeeting statusMeeting2 = statusMeetingfindByCode(Constants.S_MEETING_INCOMPLETE_LEGAL);
			StatusCase sc = statusCasefindByCode(Constants.CASE_STATUS_MEETING);

//			var result = db.Query<Meeting> (
//				"SELECT me.* FROM meeting as me"
//				+"left JOIN case_detention ON me.id_case = case_detention.id_case "
//				+"left JOIN imputed ON imputed.id_meeting = meeting.id_meeting"
//				+ "WHERE me.id_status in (?,?) "
//				//+"and me.id_reviewer = 2"
//				+" AND case_detention.id_status = ?; ", statusMeeting1.Id,statusMeeting2.Id, sc.Id);

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

//			var result = db.GetAllWithChildren<Meeting> (s =>s.ReviewerId=User.id && s.StatusMeetingId.Equals(statusMeeting1.Id)||s.StatusMeetingId.Equals(statusMeeting2.Id));
//			var result = db.GetAllWithChildren<Meeting> (s => s.StatusMeetingId.Equals(statusMeeting1.Id)||s.StatusMeetingId.Equals(statusMeeting2.Id));
//			var subgeting = result.Select (m => m.CaseDetention.StatusCaseId == sc.Id);

			Console.WriteLine ("carga de casos "+result.Count);
//			var temp = new MeetingList{Model = new MeetingTblDto{ IdFolder="60", Fullname="u a s p" , StatusCode="INCOMPLETE_LEGAL", Action="60"}};
			var temp = new MeetingList{Model = result[0]};

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


		public void  MeetingDatosPersonales(int idCase)
		{
			var result = db.Query<MeetingDatosPersonalesDto> (
				"SELECT cs.id_folder as 'idFolder',im.name as 'Name',im.lastname_p as 'LastNameP',im.lastname_m as 'LastNameM'"
//				+" ,im.birth_date as 'i.DateBirth', im.gender as 'i.Gender'"
//				+", csm.status as 'StatusCode', csm.description as 'Description'"
				+" FROM meeting as me "
				+" left JOIN case_detention as cs ON me.id_case = cs.id_case "
				+" left JOIN imputed as im ON im.id_meeting = me.id_meeting "
//				+" left JOIN cat_status_meeting as csm ON csm.id_status = me.id_status "
//				+" and me.id_reviewer = 2 "
				+" where cs.id_case = ?; ", idCase).FirstOrDefault();
			var temp = new MeetingDatosPersonales{Model = result };
			//			var temp = new NewMeeting{Model = new EntrevistaTabla{Name="nombre" , DateBirthString=DateTime.Today.ToString("yyyy/mm/dd")} };
			var pagestring = "nada que ver";
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
		}

		public void AddMeeting([Bind]NewMeetingDto model) {
			Console.WriteLine ("AddMeeting");
			Console.WriteLine ("EntrevistaTabla....."+model.ResponseMessage);
			String validateCreateMsg = validateCreateMeeting(model);
			if (validateCreateMsg != null) {
				model.ResponseMessage = validateCreateMsg;
				var temp = new NewMeeting{ Model = model };
				var pagestring = "nada que ver";
				pagestring = temp.GenerateString ();
				webView.LoadHtmlString (pagestring);
			} else {
				int? idCase = createMeeting(model);
				model.ResponseMessage = "Se ha guardado exitosamente";
				var temp = new NewMeeting{ Model = model };
//				var temp = new MeetingEdit {Model = new NewMeetingDto{ResponseMessage="Se ha guardado exitosamente"} };
				var pagestring = "nada que ver";
				pagestring = temp.GenerateString ();
				webView.LoadHtmlString (pagestring);
			}
		}

		public String getFoneticByName(String name, String lastNameP, String lastNameM) {
			return name.Trim().ToLowerInvariant()+lastNameP.Trim().ToLowerInvariant()+lastNameM.Trim().ToLowerInvariant();
		}

		public int calculateAge(DateTime dateOfBirth)
		{
			DateTime now = DateTime.Today; 
			int age = now.Year - dateOfBirth.Year;
			if (now.DayOfYear<dateOfBirth.DayOfYear) 
				age--;
			return age; 
		}

		public String validateCreateMeeting(NewMeetingDto model) {
			if (model.DateBirth.HasValue) {
				int age = calculateAge(model.DateBirth.Value);
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
						Console.WriteLine ("m.fonetic__>"+getFoneticByName(model.Name, model.LastNameP, model.LastNameM)+"  m.birthdate==>"+model.DateBirth);
						if(c.FoneticString.Equals(getFoneticByName(model.Name, model.LastNameP, model.LastNameM))&&c.BirthDate.Equals(model.DateBirth)){
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
				newImputed.FoneticString=getFoneticByName(imputed.Name, imputed.LastNameP, imputed.LastNameM);
				newImputed.Gender=imputed.Gender.GetValueOrDefault();
				newImputed.BirthDate=imputed.DateBirth.GetValueOrDefault();
				caseDetention.Status=statusCasefindByCode(Constants.CASE_STATUS_MEETING);
				caseDetention.StatusCaseId=statusCasefindByCode(Constants.CASE_STATUS_MEETING).Id;
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
				StatusMeeting statusMeeting = statusMeetingfindByCode(Constants.S_MEETING_INCOMPLETE);
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

		public StatusCase statusCasefindByCode(String code){
			//var tableStatus = db.Query<StatusCase>("SELECT s from StatusCase s where s.name = "+code);
			var tableStatus = db.Table<StatusCase> ().Where (s => s.Name == code).FirstOrDefault ();
			return tableStatus;
		}

		public StatusMeeting statusMeetingfindByCode(String code){
			var tableStatus = db.Table<StatusMeeting>().Where (s => s.Status == code).FirstOrDefault ();
			return tableStatus;
		}

		public StatusMeeting userRepositoryfindOne(String code){
			var tableStatus = db.Table<StatusMeeting>().Where (s => s.Status == code).FirstOrDefault ();
			return tableStatus;
		}


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

