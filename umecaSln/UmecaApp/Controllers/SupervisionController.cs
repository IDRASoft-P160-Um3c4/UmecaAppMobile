using System;
using PortableRazor;
using System.IO;
using SQLite.Net;
using System.Linq;
using SQLiteNetExtensions.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using Umeca.Data;

//TODO DONE StatusMeeting, Imputed, Case

namespace UmecaApp
{
	public class SupervisionController : Java.Lang.Object//: ControllerBase
	{

		IHybridWebView webView;
		readonly SQLiteConnection db;
		CatalogServiceController services;

		String JsonCountrys;
		String JsonStates;
		String JsonMunycipality;
		String JsonElection;
		String JsonActivities;

		public SupervisionController(IHybridWebView webView, SQLiteConnection dbConection)
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
			db.CreateTable<SocialEnvironment>();
			db.CreateTable<RelActivity>();
			db.Commit ();

			this.JsonCountrys =JsonConvert.SerializeObject(services.CountryFindAllOrderByName ());
			this.JsonStates = JsonConvert.SerializeObject(services.StateFindAllOrderByName ());
			this.JsonMunycipality = JsonConvert.SerializeObject(services.MunicipalityFindAllOrderByName ());
			this.JsonElection = JsonConvert.SerializeObject (services.ElectionFindAll());
			this.JsonActivities = "[{'id':1,'name':'Laborales','specification':true},{'id':2,'name':'Escolares','specification':true},{'id':3,'name':'Religiosas','specification':true},{'id':4,'name':'Deportivas','specification':true},{'id':5,'name':'Reuniones sociales','specification':true},{'id':6,'name':'Reuniones familiares','specification':true},{'id':7,'name':'Otras','specification':true},{'id':8,'name':'Ninguna','specification':false}]";
		}



		public void Index()
		{
			//services.createVerificationTest();
			StatusCase statusCaseSupervition1 = services.statusCasefindByCode(Constants.CASE_STATUS_TECHNICAL_REVIEW);
			StatusCase statusCaseSupervition2 = services.statusCasefindByCode(Constants.CASE_STATUS_HEARING_FORMAT_END);
			StatusCase statusCaseSupervition3 = services.statusCasefindByCode(Constants.CASE_STATUS_HEARING_FORMAT_INCOMPLETE);
			StatusCase statusCaseSupervition4 = services.statusCasefindByCode(Constants.CASE_STATUS_CONDITIONAL_REPRIEVE);
			StatusCase statusCaseSupervition5 = services.statusCasefindByCode(Constants.CASE_STATUS_FRAMING_INCOMPLETE);
			StatusCase statusCaseSupervition6 = services.statusCasefindByCode(Constants.CASE_STATUS_FRAMING_COMPLETE);
			StatusCase statusCaseSupervition7 = services.statusCasefindByCode(Constants.CASE_STATUS_NOT_PROSECUTE_OPEN);
			StatusCase sc = services.statusCasefindByCode(Constants.CASE_STATUS_VERIFICATION);
			var result = db.Query<HearingFormatTblDto> (
				"SELECT cs.id_case as 'CaseId',cs.id_folder as 'IdFolder', cs.id_mp as 'IdMP', im.name as 'Name',im.lastname_p as 'LastNameP',im.lastname_m as 'LastNameM',"
				+" im.birth_date as 'DateBirth', im.gender as 'Gender', scs.name as 'StatusCode', scs.description as 'Description'"
				+" FROM meeting as me "
				+" left JOIN case_detention as cs ON me.id_case = cs.id_case "
				+" left JOIN imputed as im ON im.id_meeting = me.id_meeting "
				+" left JOIN cat_status_case as scs ON scs.id_status = cs.id_status "
//				+" WHERE me.id_status in (?,?,?,?,?,?,?) "
				+" Where cs.id_status in (?,?,?,?,?,?,?); ", statusCaseSupervition1.Id, statusCaseSupervition2.Id,
				statusCaseSupervition3.Id, statusCaseSupervition4.Id,
				statusCaseSupervition5.Id, statusCaseSupervition6.Id, statusCaseSupervition7.Id);
			Console.WriteLine ("result.count supervition index> {0}", result.Count);
			var temp = new CaseHearingList{Model = result};
			var pagestring = "nada que ver";
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
		}

		public void  CaseConditionalReprieveEditNew()
		{
			var temp = new NewConditionalReprieve{Model = new NewMeetingDto() };
			//			var temp = new NewMeeting{Model = new EntrevistaTabla{Name="nombre" , DateBirthString=DateTime.Today.ToString("yyyy/mm/dd")} };
			var pagestring = "nada que ver";
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
		}

		public void AddCaseConditionalReprieve([Bind]NewMeetingDto model) {
			Console.WriteLine ("AddCaseConditionalReprieve");
			String validateCreateMsg = validateCaseConditionalReprieve(model);
			if (validateCreateMsg != null) {
				model.ResponseMessage = validateCreateMsg;
				var temp = new NewConditionalReprieve{ Model = model };
				var pagestring = "nada que ver";
				pagestring = temp.GenerateString ();
				webView.LoadHtmlString (pagestring);
			} else {
				int? idCase = createCaseConditionalReprieve(model);
				int az = idCase.GetValueOrDefault ();
				Index();
			}
		}

		public String validateCaseConditionalReprieve(NewMeetingDto model) {
			if (model.DateBirth.HasValue) {
				int age = services.calculateAge(model.DateBirth.Value);
				if (age.CompareTo(18)<0) {
					return "El imputado debe tener más de 18 años para continuar";
				}
			} else {
				return "Favor de ingresar la fecha de nacimiento del imputado.";
			}
			if (model.IdMP != null) {
				var repeated = 0;
				var fonetic = services.getFoneticByName(model.Name,model.LastNameP,model.LastNameM);
				var casos = db.Table<Case> ().Where (cs=>cs.IdMP==model.IdMP).ToList ();
				if (casos != null && casos.Count > 0) {
					foreach(Case c in casos){
						var entrevistas = db.Table<Meeting> ().Where (ent=>ent.CaseDetentionId==c.Id).ToList ();
						if(entrevistas != null && entrevistas.Count > 0) {
							foreach(Meeting entrevista in entrevistas){
								var imputado = db.Table<Imputed> ().Where (imp=>imp.MeetingId==entrevista.Id
									&& imp.FoneticString==fonetic
									&& imp.BirthDate==model.DateBirth).ToList ();
								if (imputado != null && imputado.Count > 0) {
									repeated++;
								}
							}
						}
					}

				}
				if(repeated>0){
					return "El número de carpeta judicial y el imputado ya se encuentran registrados.";
				}
			} else {
				return "Favor de ingresar el número de carpeta judicial para continuar";
			}
			return null;
		}

		public int? createCaseConditionalReprieve(NewMeetingDto imputed) {
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

				var reincident = db.Table<Imputed>().Where(impu=>impu.LastNameM==newImputed.LastNameM
					&&impu.LastNameP==newImputed.LastNameP && impu.Name==newImputed.Name
					&& impu.BirthDate==newImputed.BirthDate).ToList();
				if(reincident!=null&&reincident.Count>0){
					caseDetention.Recidivist = true;
				}else{
					caseDetention.Recidivist = false;
				}

				caseDetention.Status=services.statusCasefindByCode(Constants.CASE_STATUS_CONDITIONAL_REPRIEVE);
				caseDetention.StatusCaseId=services.statusCasefindByCode(Constants.CASE_STATUS_CONDITIONAL_REPRIEVE).Id;

				caseDetention.IdMP=imputed.IdMP;
				caseDetention.IdFolder = "SIN EVALUACIÓN REGISTRADA";

				caseDetention.DateCreate=DateTime.Today;
				//caseDetention.setChangeArrangementType(false);
				// se agrega para poder contar si un caso cambia de MC a SCPP en algun formato de audiencia
				//caseDetention = caseRepository.save(caseDetention);
				db.InsertWithChildren (caseDetention);
				Meeting meeting = new Meeting();
				meeting.MeetingType=Constants.MEETING_CONDITIONAL_REPRIEVE;
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

		public void HearingFormatList(int idCase)
		{
			var caso = db.Table<Case> ().Where(cs => cs .Id == idCase).FirstOrDefault ();
			var meeting = db.Table<Meeting> ().Where (me => me.CaseDetentionId == idCase).FirstOrDefault ();
			var imputado = db.Table<Imputed> ().Where (im => im.MeetingId == meeting.Id).FirstOrDefault ();
			var result = new List<HearingFormatGrid> ();

			var formatosAudiencia = db.Table<HearingFormat> ().Where (hf=>hf.CaseDetention == idCase).ToList ();
			if (formatosAudiencia != null && formatosAudiencia.Count > 0) {
				foreach(HearingFormat au in formatosAudiencia){
					var imputedAu = db.Table<HearingFormatImputed> ().Where (hfimp=>hfimp.Id==au.hearingImputed).FirstOrDefault ();
					var specs = db.Table<HearingFormatSpecs> ().Where (hfspcs=>hfspcs.Id==au.HearingFormatSpecs).FirstOrDefault ();
					var parsing = new HearingFormatGrid (au.Id,au.IsFinished,au.IdFolder,au.IdJudicial,imputedAu.Name,imputedAu.LastNameP,imputedAu.LastNameM,specs.ArrangementType,specs.Extension,specs.LinkageProcess,au.RegisterTime.GetValueOrDefault(),"TODO: supervisor",idCase);
					result.Add (parsing);
				}
			}
			Console.WriteLine ("source--"+result.Count);
			var temp = new HearingFormatList{Model = result};
			var pagestring = "nada que ver";  
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
		}

		public void  HearingFormatUpsert(int idCase,int idAudiencia)
		{
			var view = new HearingFormatEditDto();
			var hearingFormatData = new HearingFormatView ();





			var temp = new HearingFormatEdit{Model = new HearingFormatEditDto() };
			//			var temp = new NewMeeting{Model = new EntrevistaTabla{Name="nombre" , DateBirthString=DateTime.Today.ToString("yyyy/mm/dd")} };
			var pagestring = "nada que ver";
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
		}

		public HearingFormatView newHearingFormatByCase(int idCase){
			var hearingFormatData = new HearingFormatView ();
			var formatosAnteriores = db.Table<HearingFormat>().Where(hf=>hf.CaseDetention==idCase).ToList();
			//intenta traer los formatos anteriores y con eso llenar el nuevo formato
			if (formatosAnteriores != null && formatosAnteriores.Count > 0) {
				var ultimo = formatosAnteriores.OrderByDescending (hf => hf.Id).First ();
				hearingFormatData = fillHearingFormaData(ultimo.Id);
			} else {
				var caso = db.Table<Case> ().Where (cs => cs.Id == idCase).FirstOrDefault ();
				var meeting = db.Table<Meeting> ().Where (me => me.CaseDetentionId == idCase).FirstOrDefault ();
				var imputado = db.Table<Imputed> ().Where (im => im.MeetingId == meeting.Id).FirstOrDefault ();
				hearingFormatData.InitTime = DateTime.Now;
				hearingFormatData.AppointmentDate = DateTime.Today;
				hearingFormatData.IdCase = caso.Id;
				hearingFormatData.IdFolder = caso.IdFolder;
				hearingFormatData.IdJudicial = caso.IdMP;
				hearingFormatData.ImputedName = imputado.Name;
				hearingFormatData.ImputedFLastName = imputado.LastNameP;
				hearingFormatData.ImputedSLastName = imputado.LastNameM;
				hearingFormatData.ImputedBirthDate = imputado.BirthDate;
				var usuario = db.Table<User> ().ToList ();
				hearingFormatData.UserName =  usuario.First ().fullname;

//				if (meeting.MeetingType == Constants.MEETING_PROCEDURAL_RISK) {
//					var verificacion = db.Table<Verification> ().Where (ver => ver.CaseDetentionId == idCase).FirstOrDefault ();
//					if (verificacion != null&&verificacion.MeetingId!=null) {
//						meeting = db.Table<Meeting> ().Where (me => me.Id == verificacion.MeetingId).FirstOrDefault ();
//						if(meeting==null){
//							meeting = db.Table<Meeting> ().Where (me => me.CaseDetentionId == idCase).FirstOrDefault ();
//						}
//					}
//				}
			}
			hearingFormatData.HasPrevHF = true;
			hearingFormatData.CanSave = true;
			hearingFormatData.CanEdit = true;
			hearingFormatData.DisableAll = false;
			return hearingFormatData;
		}


		public HearingFormatView fillHearingFormaData(int idFormato){
			var result = new HearingFormatView ();
			var formatosFuente = db.Table<HearingFormat>().Where(hf=>hf.Id==idFormato).FirstOrDefault();
			if(formatosFuente!=null){

			}
			result.IdFormat = formatosFuente.Id;
			result.IdCase = formatosFuente.CaseDetention;

			result.IdFolder = result.IdFolder;
			result.IdJudicial = result.IdJudicial;
			result.AppointmentDate = result.AppointmentDate;
			result.Room = formatosFuente.Room;
			result.InitTime = formatosFuente.InitTime??DateTime.Now;
			result.EndTime = formatosFuente.EndTime??DateTime.Now;
			result.JudgeName = formatosFuente.JudgeName;
			result.MpName = formatosFuente.MpName;
			result.DefenderName = formatosFuente.DefenderName;

			var imputado = db.Table<HearingFormatImputed> ().Where (imp => imp.Id == formatosFuente.hearingImputed).FirstOrDefault ();
			if(imputado!=null&&imputado.Id>0){
				result.ImputedName = imputado.Name;
				result.ImputedFLastName = imputado.LastNameP;
				result.ImputedSLastName = imputado.LastNameM;
				result.ImputedBirthDate = imputado.BirthDate;
				result.ImputedTel = imputado.ImputeTel;
			}

			result.UmecaDate = formatosFuente.UmecaDate??DateTime.Now;
			result.UmecaTime = formatosFuente.UmecaTime ?? DateTime.Now;

			var usuario = db.Table<User> ().ToList ();
			result.UserName =  usuario.First ().fullname;

			if (formatosFuente.HearingType > 0) {
				result.HearingTypeId = formatosFuente.HearingType;
				result.HearingTypeSpecification = formatosFuente.HearingTypeSpecification;
			}

			result.ImputedPresence = formatosFuente.ImputedPresence;
			result.HearingResult = formatosFuente.HearingResult;

			if(imputado!=null&&imputado.Id>0){
				result.IdAddres = imputado.Address;
			}

			var specs = db.Table<HearingFormatSpecs> ().Where (hfs=> hfs.Id == formatosFuente.HearingFormatSpecs).FirstOrDefault ();

			if (specs != null) {
				result.ControlDetention = specs.ControlDetention;
				result.Extension = specs.Extension;
				result.ExtDate = specs.ExtDate??DateTime.Now;
				result.ImpForm = specs.ImputationFormulation;
				result.ImputationDate = specs.ImputationDate??DateTime.Now;
				result.VincProcess = specs.LinkageProcess;
				result.LinkageRoom = specs.LinkageRoom;
				result.ImputationDate = specs.LinkageDate??DateTime.Now;
				result.LinkageTime = specs.LinkageTime ??DateTime.Now;
			}

			if(specs!=null&&specs.LinkageProcess>0
				&&(specs.LinkageProcess==Constants.PROCESS_VINC_YES||specs.LinkageProcess==Constants.PROCESS_VINC_NO_REGISTER)){
				result.ArrangementType = specs.ArrangementType;
				result.NationalArrangement = specs.NationalArrangement;
				result.Terms = formatosFuente.Terms;
			}

			var contactos = db.Table<ContactData> ().Where (con=>con.HearingFormat == formatosFuente.Id).ToList ();
			if(contactos!=null&&contactos.Count>0){
				result.LstContactData = JsonConvert.SerializeObject(contactos);
			}


//			if (specs != null && specs.NationalArrangement != null && specs.ArrangementType !=null) {
//				
//				//List<ArrangementView> lstExistArrangement = db.tablethis.getArrangmentLst(existHF.getHearingFormatSpecs().getNationalArrangement(), existHF.getHearingFormatSpecs().getArrangementType());
//
//				//hearingFormatView.setLstArrangement(conv.toJson(this.selectedAssignedArrangementForView(lstExistArrangement, existHF.getAssignedAr//rangements())));
//			}

//			if (existHF.getHearingFormatSpecs() != null && existHF.getHearingFormatSpecs().getLinkageProcess() != null &&
//				(existHF.getHearingFormatSpecs().getLinkageProcess().equals(HearingFormatConstants.PROCESS_VINC_YES) || existHF.getHearingFormatSpecs().getLinkageProcess().equals(HearingFormatConstants.PROCESS_VINC_NO_REGISTER))) {
//				hearingFormatView.setArrangementType(existHF.getHearingFormatSpecs().getArrangementType());
//				hearingFormatView.setNationalArrangement(existHF.getHearingFormatSpecs().getNationalArrangement());
//				hearingFormatView.setTerms(existHF.getTerms());
//
//				if (existHF.getHearingFormatSpecs().getNationalArrangement() != null && existHF.getHearingFormatSpecs().getArrangementType() != null) {
//
//					List<ArrangementView> lstExistArrangement = this.getArrangmentLst(existHF.getHearingFormatSpecs().getNationalArrangement(), existHF.getHearingFormatSpecs().getArrangementType());
//					hearingFormatView.setLstArrangement(conv.toJson(this.selectedAssignedArrangementForView(lstExistArrangement, existHF.getAssignedArrangements())));
//				}
//
//				hearingFormatView.setLstContactData(conv.toJson(this.contactDataForView(existHF.getContacts())));
//			}
//
//			hearingFormatView.setImputedPresence(existHF.getImputedPresence());
//			hearingFormatView.setHearingResult(existHF.getHearingResult());
//			hearingFormatView.setPreviousHearing(existHF.getPreviousHearing());
//
//
//			hearingFormatView.setComments(existHF.getComments());
//			hearingFormatView.setIsFinished(false);
			return result;
		}



	}
}