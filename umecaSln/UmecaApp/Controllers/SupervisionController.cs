﻿using System;
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
			var espontaneaExample = new LogCase ();
			espontaneaExample.activity="Actividad espont&aacute;nea";
			espontaneaExample.activityString="Actividad espont&aacute;nea";
			espontaneaExample.caseDetentionId=1;
			espontaneaExample.date=DateTime.Now;
			espontaneaExample.dateString=String.Format("{0:yyyy/MM/dd}", DateTime.Now);
			espontaneaExample.id = 2;
			espontaneaExample.resume = "Información proporcionada: Confirma la dirección del anexo.\n\tObservaciones del supervisor: Se deben cancelar las actividades de psicología esta semana por anexo del imputado.\n";
			espontaneaExample.title = "Visita Domiciliar planeada a fuente José Contreras Linares  en la dirección: Av. Insurgentes 125 Col. Pedregal Lindavista, con el objetivo de verificar.";
			espontaneaExample.userId=35;
			espontaneaExample.userName = "Uriel Axel Sánchez Pérez";
			var logAct2 = new LogCase ();
			logAct2.activity="Actividad espont&aacute;nea";
			logAct2.activityString="Actividad espont&aacute;nea";
			logAct2.caseDetentionId=1;
			logAct2.date=DateTime.Now;
			logAct2.dateString=String.Format("{0:yyyy/MM/dd}", DateTime.Now);
			logAct2.id = 3;
			logAct2.resume = "Información proporcionada: Se confirma que hay al menos 1 persona viviendo en el domicilio.\n\tObservaciones del supervisor: La persona que atendio en el domicilio se nego salir y presentar identificación vigente\n";
			logAct2.title = "Visita Domiciliar emergente a Imputado en la dirección: Av. Juan de Dios Bátiz esq. Av. Miguel Othón de Mendizába, Gustavo A. Madero, Lindavista, 07738 Ciudad de Mexico, D.F., con el objetivo de realizar una visita sorpresa en casa del imputado.";
			logAct2.userId=35;
			logAct2.userName = "Uriel Axel Sánchez Pérez";
			var logAct3 = new LogCase ();
			logAct3.activity="Actividad espont&aacute;nea";
			logAct3.activityString="Actividad espont&aacute;nea";
			logAct3.caseDetentionId=1;
			logAct3.date=DateTime.Now;
			logAct3.dateString=String.Format("{0:yyyy/MM/dd}", DateTime.Now);
			logAct3.id = 4;
			logAct3.resume = "Información proporcionada: La victima sigue en tratamiento con anti-depresivos.\n\tObservaciones del supervisor: La victima se nota bien de salud y sin problemas para respirar.\n";
			logAct3.title = "Visita Domiciliar emergente a Víctima o testigo, Relacion Ninguno, Hernandez Mendez Fernando en la dirección: Secc. 35 #80 Lt. 34 Col. Rio de Luz, Ecatepec de Morelos, con el objetivo de revisar el estado animico de la victima.";
			logAct3.userId=35;
			logAct3.userName = "Uriel Axel Sánchez Pérez";
			var listoski = new List<LogCase>();
			listoski.Add(espontaneaExample);
			listoski.Add(logAct2);
			listoski.Add(logAct3);
			String nosense = JsonConvert.SerializeObject (listoski);
			Console.WriteLine("json de LogCase"+nosense);


			services.createMeetingTest();
			db.CreateTable<HearingFormatImputed>();
			db.CreateTable<HearingFormat>();
			StatusCase statusCaseSupervition1 = services.statusCasefindByCode(Constants.CASE_STATUS_TECHNICAL_REVIEW);
			StatusCase statusCaseSupervition2 = services.statusCasefindByCode(Constants.CASE_STATUS_HEARING_FORMAT_END);
			StatusCase statusCaseSupervition3 = services.statusCasefindByCode(Constants.CASE_STATUS_HEARING_FORMAT_INCOMPLETE);
			StatusCase statusCaseSupervition4 = services.statusCasefindByCode(Constants.CASE_STATUS_CONDITIONAL_REPRIEVE);
			StatusCase statusCaseSupervition5 = services.statusCasefindByCode(Constants.CASE_STATUS_FRAMING_INCOMPLETE);
			StatusCase statusCaseSupervition6 = services.statusCasefindByCode(Constants.CASE_STATUS_FRAMING_COMPLETE);
			StatusCase statusCaseSupervition7 = services.statusCasefindByCode(Constants.CASE_STATUS_NOT_PROSECUTE_OPEN);
			StatusCase sc = services.statusCasefindByCode(Constants.CASE_STATUS_VERIFICATION);
			var result = db.Query<CaseHearingFormatTblDto> (
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
			db.CreateTable<HearingFormat> ();
			db.CreateTable<Arrangement> ();
			db.CreateTable<AssignedArrangement> ();
			db.CreateTable<ContactData> ();
			db.CreateTable<Crime> ();
			db.CreateTable<CrimeCatalog> ();
			db.CreateTable<GroupCrime> ();
			db.CreateTable< HearingFormatSpecs> ();
			db.CreateTable<HearingFormatImputed> ();
			db.CreateTable<HearingType> ();
			var caso = db.Table<Case> ().Where(cs => cs .Id == idCase).FirstOrDefault ();
			var meeting = db.Table<Meeting> ().Where (me => me.CaseDetentionId == idCase).FirstOrDefault ();
			var imputado = db.Table<Imputed> ().Where (im => im.MeetingId == meeting.Id).FirstOrDefault ();
			var result = new HearingFormatTblDto ();
			result.rows = new List<HearingFormatGrid> ();
			result.CaseId = caso.Id;

			var formatosAudiencia = db.Table<HearingFormat> ().Where (hf=>hf.CaseDetention == idCase).ToList ();
			if (formatosAudiencia != null && formatosAudiencia.Count > 0) {
				foreach(HearingFormat au in formatosAudiencia){
					var imputedAu = db.Table<HearingFormatImputed> ().Where (hfimp=>hfimp.Id==au.hearingImputed).FirstOrDefault ();
					var specs = db.Table<HearingFormatSpecs> ().Where (hfspcs=>hfspcs.Id==au.HearingFormatSpecs).FirstOrDefault ();
					var parsing = new HearingFormatGrid (au.Id,au.IsFinished,au.IdFolder,au.IdJudicial,imputedAu.Name,imputedAu.LastNameP,imputedAu.LastNameM,specs.ArrangementType,specs.Extension,specs.LinkageProcess,au.RegisterTime.GetValueOrDefault(),"TODO: supervisor",idCase);
					result.rows.Add (parsing);
				}
			}

			Console.WriteLine ("source--"+result.rows.Count);
			var temp = new HearingFormatList{Model = result};
			var pagestring = "nada que ver";  
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
		}

		public void HearingFormatList(int idCase,string mesage)
		{
			db.CreateTable<HearingFormat> ();
			db.CreateTable<Arrangement> ();
			db.CreateTable<AssignedArrangement> ();
			db.CreateTable<ContactData> ();
			db.CreateTable<Crime> ();
			db.CreateTable<CrimeCatalog> ();
			db.CreateTable<GroupCrime> ();
			db.CreateTable< HearingFormatSpecs> ();
			db.CreateTable<HearingFormatImputed> ();
			db.CreateTable<HearingType> ();
			var caso = db.Table<Case> ().Where(cs => cs .Id == idCase).FirstOrDefault ();
			var meeting = db.Table<Meeting> ().Where (me => me.CaseDetentionId == idCase).FirstOrDefault ();
			var imputado = db.Table<Imputed> ().Where (im => im.MeetingId == meeting.Id).FirstOrDefault ();
			var result = new HearingFormatTblDto ();
			result.rows = new List<HearingFormatGrid> ();
			result.CaseId = caso.Id;

			var formatosAudiencia = db.Table<HearingFormat> ().Where (hf=>hf.CaseDetention == idCase).ToList ();
			if (formatosAudiencia != null && formatosAudiencia.Count > 0) {
				foreach(HearingFormat au in formatosAudiencia){
					var imputedAu = db.Table<HearingFormatImputed> ().Where (hfimp=>hfimp.Id==au.hearingImputed).FirstOrDefault ();
					var specs = db.Table<HearingFormatSpecs> ().Where (hfspcs=>hfspcs.Id==au.HearingFormatSpecs).FirstOrDefault ();
					var parsing = new HearingFormatGrid (au.Id,au.IsFinished,au.IdFolder,au.IdJudicial,imputedAu.Name,imputedAu.LastNameP,imputedAu.LastNameM,specs.ArrangementType,specs.Extension,specs.LinkageProcess,au.RegisterTime.GetValueOrDefault(),"TODO: supervisor",idCase);
					result.rows.Add (parsing);
				}
			}
			Console.WriteLine ("source--"+result.rows.Count);
			result.message = mesage;
			var temp = new HearingFormatList{Model = result};
			var pagestring = "nada que ver";  
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
		}

		public void  HearingFormatUpsert(int idCase,int idFormato)
		{
			var view = new HearingFormatEditDto();
			var hearingFormatData = new HearingFormatView ();
			var statusPreClosed = db.Table<StatusCase> ().Where (stc1 => stc1.Name == Constants.CASE_STATUS_PRE_CLOSED).FirstOrDefault ();
			var statusClosed = db.Table<StatusCase> ().Where (stc2 => stc2.Name == Constants.CASE_STATUS_CLOSED).FirstOrDefault ();
			var casoverStatus = db.Table<Case> ().Where (cs => cs.Id == idCase && (cs.StatusCaseId == statusPreClosed.Id || cs.StatusCaseId == statusClosed.Id)).ToList ();
			
			if(casoverStatus!=null && casoverStatus.Count>0){
				HearingFormatList (idCase, "No es posible agregar mas formatos, el caso se encuentra cerrado.");
				return;
			}

			if (idFormato > 0) {
				hearingFormatData = fillHearingFormaData (idFormato);
			} else { //si es nuevo 
				var last = db.Table<HearingFormat>().Where(hf=>hf.CaseDetention==idCase && hf.IsFinished==false).ToList();
				if(last!=null && last.Count>0){
					HearingFormatList (idCase, "No es posible agregar mas formatos, el caso tiene un formato de audiencia incompleto.");
					return;
				}
				hearingFormatData = newHearingFormatByCase (idCase);
				hearingFormatData.isFinished = false;
				hearingFormatData.endTime = null;
				hearingFormatData.idFormat = null;
				hearingFormatData.confirmComment = null;
				hearingFormatData.credPass = null;
			}

			var pervF = db.Table<HearingFormat> ().Where (prev=>prev.CaseDetention == idCase 
				&& prev.IsFinished == true).ToList ();
			if (pervF != null && pervF.Count > 0) {
				hearingFormatData.hasPrevHF = true;
			} else {
				hearingFormatData.hasPrevHF = false;
			}

			view.IdCase = idCase;
			if (string.IsNullOrEmpty (hearingFormatData.listCrime)) {
				view.listCrime = "[]";
			} else {
				view.listCrime = hearingFormatData.listCrime;
			}


			var catalogoCrimenes = db.Table<CrimeCatalog> ().Where (ccs => ccs.IsObsolete == false).ToList ();
			var optCrimes = new List<CatalogDto>();
			foreach(CrimeCatalog cc in catalogoCrimenes){
				optCrimes.Add (new CatalogDto(cc.Id, cc.Name));
			}
			view.optionsCrime = JsonConvert.SerializeObject(optCrimes);
			var elections = db.Table<Election> ().ToList ();
			var optElections = new List<CatalogDto>();
			foreach(Election el in elections){
				optElections.Add (new CatalogDto(el.Id, el.Name));
			}
			view.listElection = JsonConvert.SerializeObject(optElections);
			view.readonlyBand = false;

			var hearingtypes = db.Table<HearingType> ().Where (ht => ht.IsObsolete == false).OrderBy (ht => ht.Description).ToList ();
			if (hearingtypes != null & hearingtypes.Count > 0) {
				var tiposFormato = new List<SelectList> ();
				foreach (HearingType tp in hearingtypes) {
					var tipo = new SelectList (tp.Id, tp.Description, tp.Lock, tp.Specification);
					tiposFormato.Add (tipo);
				}
				view.lstHearingType = JsonConvert.SerializeObject (tiposFormato);

			} else {
				view.lstHearingType = "[]";
			}

			var country = db.Table<Country> ().Where (ctry=>ctry.Alpha2==Constants.ALPHA2_MEXICO).FirstOrDefault ();
			var listState = db.Table<State> ().Where (st => st.CountryId == country.Id).OrderBy (st => st.Name).ToList ();
			if (listState != null & listState.Count > 0) {
				var stateDtoList = new List<StateDto> ();
				foreach (State lst in listState) {
					var rowState = new StateDto (lst);
					stateDtoList.Add (rowState);
				}
				view.listState = JsonConvert.SerializeObject (stateDtoList);
			} else {
				view.listState = "[]";
			}

			if (hearingFormatData.idAddres != null && hearingFormatData.idAddres > 0) {
				var direccion = db.Table<Address> ().Where (addr => addr.Id == hearingFormatData.idAddres).FirstOrDefault ();
				var adres = db.Table<Location> ().Where (loc=>loc.Id==direccion.LocationId).FirstOrDefault ();
				var parser = new AddressDto (direccion);
				parser.locationId = direccion.LocationId??0;
				parser.zipCode = adres.ZipCode;
				view.address = JsonConvert.SerializeObject (parser);
			}
			view.model = JsonConvert.SerializeObject (hearingFormatData);
			var temp = new HearingFormatEdit{Model = view };
			//			var temp = new NewMeeting{Model = new EntrevistaTabla{Name="nombre" , DateBirthString=DateTime.Today.ToString("yyyy/mm/dd")} };
			var pagestring = "nada que ver";
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
		}

		public HearingFormatView newHearingFormatByCase(int idCase){
			var hearingFormatData = new HearingFormatView ();

			var formatosAnteriores = db.Table<HearingFormat> ().Where (prev=>prev.CaseDetention == idCase 
				&& prev.IsFinished == true).OrderByDescending(prev=>prev.Id).ToList ();

			//intenta traer los formatos anteriores y con eso llenar el nuevo formato
			if (formatosAnteriores != null && formatosAnteriores.Count > 0) {
				var ultimo = formatosAnteriores.FirstOrDefault ();
				hearingFormatData = fillHearingFormaData(ultimo.Id);
			} else {
				var caso = db.Table<Case> ().Where (cs => cs.Id == idCase).FirstOrDefault ();
				var meeting = db.Table<Meeting> ().Where (me => me.CaseDetentionId == idCase).FirstOrDefault ();
				var imputado = db.Table<Imputed> ().Where (im => im.MeetingId == meeting.Id).FirstOrDefault ();
				hearingFormatData.initTime = DateTime.Now;
				hearingFormatData.appointmentDate = DateTime.Today;
				hearingFormatData.idCase = caso.Id;
				hearingFormatData.idFolder = caso.IdFolder;
				hearingFormatData.idJudicial = caso.IdMP;
				hearingFormatData.imputedName = imputado.Name;
				hearingFormatData.imputedFLastName = imputado.LastNameP;
				hearingFormatData.imputedSLastName = imputado.LastNameM;
				hearingFormatData.imputedBirthDate = imputado.BirthDate;
				var usuario = db.Table<User> ().ToList ();
				if(usuario.Count>0){
					hearingFormatData.userName = usuario.FirstOrDefault ().fullname??"";
				}else{
					hearingFormatData.userName = "";
				}

			}
			hearingFormatData.canSave = true;
			hearingFormatData.canEdit = true;
			hearingFormatData.disableAll = false;
			return hearingFormatData;
		}


		public HearingFormatView fillHearingFormaData(int idFormato){
			var result = new HearingFormatView ();
			result.canSave = true;
			result.canEdit = true;
			result.disableAll = false;
			var formatosFuente = db.Table<HearingFormat>().Where(hf=>hf.Id==idFormato).FirstOrDefault();

			result.idFormat = formatosFuente.Id;
			result.idCase = formatosFuente.CaseDetention;

			result.idFolder = formatosFuente.IdFolder;
			result.idJudicial = formatosFuente.IdJudicial;
			result.appointmentDate = formatosFuente.AppointmentDate??DateTime.Now;
			result.room = formatosFuente.Room;
			result.initTime = formatosFuente.InitTime??DateTime.Now;
			result.endTime = formatosFuente.EndTime??DateTime.Now;
			result.judgeName = formatosFuente.JudgeName;
			result.mpName = formatosFuente.MpName;
			result.defenderName = formatosFuente.DefenderName;
			result.isFinished = formatosFuente.IsFinished;
			db.CreateTable<HearingFormatImputed> ();
			var imputado = db.Table<HearingFormatImputed> ().Where (imp => imp.Id == formatosFuente.hearingImputed).FirstOrDefault ();
			if(imputado!=null&&imputado.Id>0){
				result.imputedName = imputado.Name;
				result.imputedFLastName = imputado.LastNameP;
				result.imputedSLastName = imputado.LastNameM;
				result.imputedBirthDate = imputado.BirthDate;
				result.imputedTel = imputado.ImputeTel;
			}

			result.umecaDate = formatosFuente.UmecaDate??DateTime.Now;
			result.umecaTime = formatosFuente.UmecaTime ?? DateTime.Now;

//			var usuario = db.Table<User> ().ToList ();
//			result.userName =  usuario.First ().fullname; 

			if (formatosFuente.HearingType > 0) {
				result.hearingTypeId = formatosFuente.HearingType;
				result.hearingTypeSpecification = formatosFuente.HearingTypeSpecification;
			}

			result.imputedPresence = formatosFuente.ImputedPresence;
			result.hearingResult = formatosFuente.HearingResult;

			if(imputado!=null&&imputado.Id>0){
				result.idAddres = imputado.Address;
			}

			var specs = db.Table<HearingFormatSpecs> ().Where (hfs=> hfs.Id == formatosFuente.HearingFormatSpecs).FirstOrDefault ();

			if (specs != null) {
				result.controlDetention = specs.ControlDetention;
				result.extension = specs.Extension;
				result.extDate = specs.ExtDate??DateTime.Now;
				result.impForm = specs.ImputationFormulation;
				result.imputationDate = specs.ImputationDate??DateTime.Now;
				result.vincProcess = specs.LinkageProcess;
				result.linkageRoom = specs.LinkageRoom;
				result.imputationDate = specs.LinkageDate??DateTime.Now;
				result.linkageTime = specs.LinkageTime ??DateTime.Now;
			}

			if(specs!=null&&specs.LinkageProcess>0
				&&(specs.LinkageProcess==Constants.PROCESS_VINC_YES||specs.LinkageProcess==Constants.PROCESS_VINC_NO_REGISTER)){
				result.arrangementType = specs.ArrangementType;
				result.nationalArrangement = specs.NationalArrangement;
				result.terms = formatosFuente.Terms;
			}

			if (specs != null && specs.NationalArrangement!=null && specs.ArrangementType > 0) {
				List<ArrangementView> lstExistArrangement = getArrangmentLst (specs.NationalArrangement, specs.ArrangementType, idFormato);
				result.lstArrangement = JsonConvert.SerializeObject (lstExistArrangement);
			}


			result.imputedPresence = formatosFuente.ImputedPresence;
			result.hearingResult = formatosFuente.HearingResult;
			result.previousHearing = formatosFuente.PreviousHearing;
			result.comments = formatosFuente.Comments;
			var contactos = db.Table<ContactData> ().Where (con=>con.HearingFormat == formatosFuente.Id).ToList ();
			if(contactos!=null&&contactos.Count>0){
				result.lstContactData = JsonConvert.SerializeObject(contactos);
			}

			var crimes = db.Table<Crime> ().Where (crm=>crm.HearingFormat == formatosFuente.Id).ToList ();
			if (crimes != null && crimes.Count > 0) {
				var crimeList = new List<CrimeDto> ();
				foreach (Crime c in crimes) {
					var parsedCrime = new CrimeDto();
					var crimen = new CatalogDto ();
					crimen.id = c.IdCrimeCat??0;
					crimen.name = db.Table<CrimeCatalog> ().Where (cct=>cct.Id==c.IdCrimeCat).FirstOrDefault ().Name??"";
					parsedCrime.crime = crimen;
					parsedCrime.comment = c.Comment;
					CatalogDto aux = new CatalogDto();
					var federal = db.Table<Election> ().Where (ele => ele.Id == c.Federal).FirstOrDefault ();
					aux.name = federal.Name;
					aux.id = c.Federal??0;
					parsedCrime.federal = aux;
					parsedCrime.article = c.Article;
					crimeList.Add (parsedCrime);
				}
				result.listCrime = JsonConvert.SerializeObject (crimeList);
			}

			if(contactos!=null&&contactos.Count>0){
				result.lstContactData = JsonConvert.SerializeObject(contactos);
			}

			return result;
		}



		public List<ArrangementView> getArrangmentLst(Boolean? national, int? idTipo, int? idFormato) {
			List<ArrangementView> lstArrmntView = new List<ArrangementView>();
			var lstArrmnt = db.Table<Arrangement>().Where(Arr=>Arr.IsNational == national && Arr.Type == idTipo).ToList();

			foreach (Arrangement arrmnt in lstArrmnt) {
				ArrangementView arrV = new ArrangementView();
				arrV.id = arrmnt.Id;
				arrV.name = arrmnt.Description;
				arrV.isDefault = arrmnt.IsDefault;
				if (arrV.isDefault == true) {
					arrV.selVal = true;
				} else {
					arrV.selVal = false;
				}
				arrV.isExclusive = arrmnt.IsExclusive;
				var intermedia = db.Table<AssignedArrangement> ().Where (asar => asar.HearingFormat == idFormato
					&& asar.Arrangement == arrV.id).FirstOrDefault ();
				if (intermedia != null && intermedia.Description != "") {
					arrV.description = intermedia.Description;
				} else {
					arrV.description = "";
				}
				lstArrmntView.Add(arrV);
			}
			return lstArrmntView;
		}


		public void Visita()
		{
			var result = db.Query<CaseHearingFormatTblDto> (
				"SELECT cs.id_case as 'CaseId',cs.id_folder as 'IdFolder', cs.id_mp as 'IdMP', im.name as 'Name',im.lastname_p as 'LastNameP',im.lastname_m as 'LastNameM',"
				+" im.birth_date as 'DateBirth', im.gender as 'Gender', scs.name as 'StatusCode', scs.description as 'Description'"
				+" FROM meeting as me "
				+" left JOIN case_detention as cs ON me.id_case = cs.id_case "
				+" left JOIN imputed as im ON im.id_meeting = me.id_meeting "
				+" left JOIN cat_status_case as scs ON scs.id_status = cs.id_status ");
			Console.WriteLine ("result.count supervition index> {0}", result.Count);
			var temp = new CaseLogList{Model = result};
			var pagestring = "nada que ver";
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
		}

		public void LogActivityLst(int idCase)
		{
			db.CreateTable<LogCase> ();
			var caso = db.Table<Case> ().Where (cs => cs.Id == idCase).FirstOrDefault ();
			var me = db.Table<Meeting> ().Where (mi => mi.CaseDetentionId == caso.Id).FirstOrDefault();
			Imputed imputed = new Imputed ();
			if (me != null) {
				imputed = db.Table<Imputed> ().Where (imp=>imp.MeetingId == me.Id).FirstOrDefault ();
			}

			var model = new LogActivityListDto ();
			model.imputado = imputed.Name+" "+imputed.LastNameP+" "+imputed.LastNameM;
			model.CaseId = idCase;
			model.folder = caso.IdFolder;
			var usuarioAct = db.Table<User> ().ToList();
			User superviser = new User ();
			if (usuarioAct != null && usuarioAct.Count > 0) {
				superviser = usuarioAct[0];
			}
			if (superviser != null && superviser.Id != null && superviser.Id > 0) {
				var logs = db.Table<LogCase> ().Where (lc => lc.caseDetentionId == idCase
				                       && lc.userId == superviser.Id).ToList ();
				model.rows = new List<LogCase> ();
				foreach(LogCase l in logs){
					l.dateString = String.Format("{0:yyyy/MM/dd HH:mm}", l.date);
					l.userName = superviser.fullname;
					model.rows.Add (l);
				}
			} else {
				model.rows = new List<LogCase> ();
			}
			var temp = new LogActivityList{Model = model };
			//			var temp = new NewMeeting{Model = new EntrevistaTabla{Name="nombre" , DateBirthString=DateTime.Today.ToString("yyyy/mm/dd")} };
			var pagestring = "nada que ver";
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
		}

		public void  AddCaseLog(int idCase)
		{
			var nuevo = new LogCase();
			nuevo.caseDetentionId = idCase;
			nuevo.activity = "Actividad espontánea";
			nuevo.activityString = "Actividad espontánea";
			var usuarioAct = db.Table<User> ().ToList();
			User superviser = new User ();
			if (usuarioAct != null && usuarioAct.Count > 0) {
				superviser = usuarioAct[0];
			}
			nuevo.userId = superviser.Id;
			var temp = new NewLogActivity{Model = nuevo };
			//			var temp = new NewMeeting{Model = new EntrevistaTabla{Name="nombre" , DateBirthString=DateTime.Today.ToString("yyyy/mm/dd")} };
			var pagestring = "nada que ver";
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
		}




//		public void AddCaseConditionalReprieve([Bind]NewMeetingDto model) {
//			Console.WriteLine ("AddCaseConditionalReprieve");
//			String validateCreateMsg = validateCaseConditionalReprieve(model);
//			if (validateCreateMsg != null) {
//				model.ResponseMessage = validateCreateMsg;
//				var temp = new NewConditionalReprieve{ Model = model };
//				var pagestring = "nada que ver";
//				pagestring = temp.GenerateString ();
//				webView.LoadHtmlString (pagestring);
//			} else {
//				int? idCase = createCaseConditionalReprieve(model);
//				int az = idCase.GetValueOrDefault ();
//				Index();
//			}
//		}

//		@RequestMapping(value = "/shared/logCase/doNewLogSpontaneous", method = RequestMethod.POST)
//		public
//		@ResponseBody
//		ResponseMessage doNewLogSpontaneous(@ModelAttribute LogCase logCase, @RequestParam Long id) {
//			try{
//				logCaseService.addLog(ConstantsLogCase.SPONTANEOUS_ACTIVITY,id,logCase);
//				String message= logCaseService.getLogCase(id);
//				return new ResponseMessage(false,message);
//			}catch (Exception e){
//				logException.Write(e, this.getClass(), "addLog", sharedUserService);
//				return new ResponseMessage(true,"Ha ocurrido un error al guardar la actividad.");
//			}
//		}



		public void  HearingFormatVisualize(int idCase,int idFormato)
		{
			var view = new HearingFormatEditDto();
			var hearingFormatData = new HearingFormatView ();
			var statusPreClosed = db.Table<StatusCase> ().Where (stc1 => stc1.Name == Constants.CASE_STATUS_PRE_CLOSED).FirstOrDefault ();
			var statusClosed = db.Table<StatusCase> ().Where (stc2 => stc2.Name == Constants.CASE_STATUS_CLOSED).FirstOrDefault ();
			var casoverStatus = db.Table<Case> ().Where (cs => cs.Id == idCase && (cs.StatusCaseId == statusPreClosed.Id || cs.StatusCaseId == statusClosed.Id)).ToList ();

			if(casoverStatus!=null && casoverStatus.Count>0){
				HearingFormatList (idCase, "No es posible agregar mas formatos, el caso se encuentra cerrado.");
				return;
			}

			if (idFormato > 0) {
				hearingFormatData = fillHearingFormaData (idFormato);
			} else { //si es nuevo 
				var last = db.Table<HearingFormat>().Where(hf=>hf.CaseDetention==idCase && hf.IsFinished==false).ToList();
				if(last!=null && last.Count>0){
					HearingFormatList (idCase, "No es posible agregar mas formatos, el caso tiene un formato de audiencia incompleto.");
					return;
				}
				hearingFormatData = newHearingFormatByCase (idCase);
				hearingFormatData.isFinished = false;
				hearingFormatData.endTime = null;
				hearingFormatData.idFormat = null;
				hearingFormatData.confirmComment = null;
				hearingFormatData.credPass = null;
			}

			var pervF = db.Table<HearingFormat> ().Where (prev=>prev.CaseDetention == idCase 
				&& prev.IsFinished == true).ToList ();
			if (pervF != null && pervF.Count > 0) {
				hearingFormatData.hasPrevHF = true;
			} else {
				hearingFormatData.hasPrevHF = false;
			}

			view.IdCase = idCase;
			if (string.IsNullOrEmpty (hearingFormatData.listCrime)) {
				view.listCrime = "[]";
			} else {
				view.listCrime = hearingFormatData.listCrime;
			}


			var catalogoCrimenes = db.Table<CrimeCatalog> ().Where (ccs => ccs.IsObsolete == false).ToList ();
			var optCrimes = new List<CatalogDto>();
			foreach(CrimeCatalog cc in catalogoCrimenes){
				optCrimes.Add (new CatalogDto(cc.Id, cc.Name));
			}
			view.optionsCrime = JsonConvert.SerializeObject(optCrimes);
			var elections = db.Table<Election> ().ToList ();
			var optElections = new List<CatalogDto>();
			foreach(Election el in elections){
				optElections.Add (new CatalogDto(el.Id, el.Name));
			}
			view.listElection = JsonConvert.SerializeObject(optElections);
			view.readonlyBand = false;

			var hearingtypes = db.Table<HearingType> ().Where (ht => ht.IsObsolete == false).OrderBy (ht => ht.Description).ToList ();
			if (hearingtypes != null & hearingtypes.Count > 0) {
				var tiposFormato = new List<SelectList> ();
				foreach (HearingType tp in hearingtypes) {
					var tipo = new SelectList (tp.Id, tp.Description, tp.Lock, tp.Specification);
					tiposFormato.Add (tipo);
				}
				view.lstHearingType = JsonConvert.SerializeObject (tiposFormato);

			} else {
				view.lstHearingType = "[]";
			}

			var country = db.Table<Country> ().Where (ctry=>ctry.Alpha2==Constants.ALPHA2_MEXICO).FirstOrDefault ();
			var listState = db.Table<State> ().Where (st => st.CountryId == country.Id).OrderBy (st => st.Name).ToList ();
			if (listState != null & listState.Count > 0) {
				var stateDtoList = new List<StateDto> ();
				foreach (State lst in listState) {
					var rowState = new StateDto (lst);
					stateDtoList.Add (rowState);
				}
				view.listState = JsonConvert.SerializeObject (stateDtoList);
			} else {
				view.listState = "[]";
			}

			if (hearingFormatData.idAddres != null && hearingFormatData.idAddres > 0) {
				var direccion = db.Table<Address> ().Where (addr => addr.Id == hearingFormatData.idAddres).FirstOrDefault ();
				var adres = db.Table<Location> ().Where (loc=>loc.Id==direccion.LocationId).FirstOrDefault ();
				var parser = new AddressDto (direccion);
				parser.locationId = direccion.LocationId??0;
				parser.zipCode = adres.ZipCode;
				view.address = JsonConvert.SerializeObject (parser);
			}
			hearingFormatData.canEdit = false;
			hearingFormatData.canSave = false;
			hearingFormatData.disableAll = true;
			view.model = JsonConvert.SerializeObject (hearingFormatData);
			var temp = new HearingFormatEdit{Model = view };
			//			var temp = new NewMeeting{Model = new EntrevistaTabla{Name="nombre" , DateBirthString=DateTime.Today.ToString("yyyy/mm/dd")} };
			var pagestring = "nada que ver";
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
		}



	}
}