using System;
using PortableRazor;
using System.IO;

//query toList()
using System.Linq;

using Newtonsoft.Json;
//listas
using System.Collections.Generic;

using SQLite;
using Umeca.Data;


//TODO DONE StatusMeeting, Imputed, Case

namespace UmecaApp
{
	public class SyncController : Java.Lang.Object//: ControllerBase
	{

		IHybridWebView webView;
		CatalogServiceController services;



		public SyncController(IHybridWebView webView)
		{
			this.webView = webView;
			services = new CatalogServiceController ();
		}

		public void Index()
		{
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<User> ();
				var usrList = db.Table<User> ().ToList ();
				User reviewer = usrList.FirstOrDefault ();
				int revId = 0;
				if (reviewer != null && reviewer.Id > 0) {
					revId = reviewer.Id;
				}

				//estatus de meeting terminados
				StatusMeeting statusMeeting2 = services.statusMeetingfindByCode (Constants.S_MEETING_INCOMPLETE_LEGAL);
				StatusCase sc = services.statusCasefindByCode (Constants.CASE_STATUS_MEETING);
				StatusCase sc1 = services.statusCasefindByCode (Constants.ST_CASE_TABLET_ASSIGNED);

				var result = db.Query<MeetingTblDto> (
					            "SELECT cs.id_case as 'CaseId',cs.id_folder as 'IdFolder',im.name as 'Name',im.lastname_p as 'LastNameP',im.lastname_m as 'LastNameM',"
					            + " im.birth_date as 'DateBirth', im.gender as 'Gender', csm.status as 'StatusCode', csm.description as 'Description' , me.id_reviewer as 'ReviewerId' "
					            + " FROM meeting as me "
					            + " left JOIN case_detention as cs ON me.id_case = cs.id_case "
					            + " left JOIN imputed as im ON im.id_meeting = me.id_meeting "
					            + " left JOIN cat_status_meeting as csm ON csm.id_status = me.id_status "
					            + " WHERE me.id_status =? "
					            + " and me.id_reviewer = ? "
					            + " AND cs.id_status in (?,?); ", statusMeeting2.Id, revId, sc.Id, sc1.Id);
				var c1 = 0;
				if (result != null) {
					for (c1 = 0; c1 < result.Count; c1++) {
						result [c1].Action = "meeting";
					}
				}

				StatusVerification statusVerification1 = services.statusVerificationfindByCode (Constants.VERIFICATION_STATUS_AUTHORIZED);
				StatusVerification statusVerification2 = services.statusVerificationfindByCode (Constants.VERIFICATION_STATUS_MEETING_COMPLETE);
				StatusCase scv = services.statusCasefindByCode (Constants.CASE_STATUS_VERIFICATION);
				StatusCase scv1 = services.statusCasefindByCode (Constants.ST_CASE_TABLET_ASSIGNED);

				var result2 = db.Query<MeetingTblDto> (
					             "SELECT cs.id_case as 'CaseId',cs.id_folder as 'IdFolder', "
					             + " csm.description as 'StatusCode', csm.description as 'Description'  , me.id_reviewer as 'ReviewerId' "
					             + " , me.id_verification as 'MeetingStatusId' "
					             + " FROM verification as me "
					             + " left JOIN case_detention as cs ON me.id_case = cs.id_case "
					             + "  left JOIN cat_status_verification as csm ON csm.id_status = me.id_status_verification "
								 + " WHERE me.id_status_verification  in (?,?) "
					             + " and me.id_reviewer = ? "
					             + " AND cs.id_status in (?,?); ", statusVerification1.Id, statusVerification2.Id, revId, scv.Id, scv1.Id);
				var c2 = 0;
				try {
					if (result2 != null) {
						for (c2 = 0; c2 < result2.Count; c2++) {
							var id = result2 [c2].MeetingStatusId;
							var expected = db.Table<SourceVerification> ().Where (sv => sv.VerificationId == id && sv.Visible == true).ToList ();
							var finished = db.Table<SourceVerification> ().Where (sv => sv.VerificationId == id && sv.DateComplete != null && sv.Visible == true).ToList ();
							if (expected.Count == finished.Count) {
								result2 [c2].Action = "verification";
							} else {
								result2 [c2].Action = "verificationIncomplete";
							}
							var caseis = result2 [c2].CaseId;
							var me = db.Table<Meeting> ().Where (met => met.CaseDetentionId == caseis).FirstOrDefault ();
							if (me != null) {
								var imp = db.Table<Imputed> ().Where (iputad => iputad.MeetingId == me.Id).FirstOrDefault ();
								if (imp != null) {
									result2 [c2].Name = imp.Name;
									result2 [c2].LastNameP = imp.LastNameP;
									result2 [c2].LastNameM = imp.LastNameM;
									result2 [c2].Gender = imp.Gender;
								}
							}

						}
					}
				} catch (Exception e) {
					Console.WriteLine ("excepcion en sincronizacion ");
					Console.WriteLine (e.Message);
				}
			
			
				result.AddRange (result2);

				/////
				StatusMeeting statusMeetingNeg = services.statusMeetingfindByCode (Constants.S_MEETING_DECLINE);
				StatusCase sc2 = services.statusCasefindByCode (Constants.CASE_STATUS_NOT_PROSECUTE);
				var result3 = db.Query<MeetingTblDto> (
					"SELECT cs.id_case as 'CaseId',cs.id_folder as 'IdFolder',im.name as 'Name',im.lastname_p as 'LastNameP',im.lastname_m as 'LastNameM',"
					+ " im.birth_date as 'DateBirth', im.gender as 'Gender', csm.status as 'StatusCode', csm.description as 'Description' , me.id_reviewer as 'ReviewerId' "
					+ " FROM meeting as me "
					+ " left JOIN case_detention as cs ON me.id_case = cs.id_case "
					+ " left JOIN imputed as im ON im.id_meeting = me.id_meeting "
					+ " left JOIN cat_status_meeting as csm ON csm.id_status = me.id_status "
					+ " WHERE me.id_status =? "
					+ " and me.id_reviewer = ? "
					+ " AND cs.id_status = ?; ", statusMeetingNeg.Id, revId, sc2.Id);
				var c3 = 0;
				if (result != null) {
					for (c3 = 0; c3 < result3.Count; c3++) {
						result3 [c3].Action = "negacion";
					}
				}
				result.AddRange (result3);
				/////



				var temp = new SyncCaseList{ Model = result };
				var pagestring = "nada que ver";
				pagestring = temp.GenerateString ();
				webView.LoadHtmlString (pagestring);
				db.Close ();
			}
		}


		public void IndexSuperv()
		{
			
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<User> ();
				var usrList = db.Table<User> ().ToList ();
				User reviewer = usrList.FirstOrDefault ();
				int revId = 0;
				if (reviewer != null && reviewer.Id > 0) {
					revId = reviewer.Id;
				}

				//estatus para supervicion
				db.CreateTable<HearingFormatImputed> ();
				db.CreateTable<HearingFormat> ();
				StatusCase statusCaseSupervition1 = services.statusCasefindByCode (Constants.CASE_STATUS_TECHNICAL_REVIEW);
				StatusCase statusCaseSupervition2 = services.statusCasefindByCode (Constants.CASE_STATUS_HEARING_FORMAT_END);
				StatusCase statusCaseSupervition3 = services.statusCasefindByCode (Constants.CASE_STATUS_HEARING_FORMAT_INCOMPLETE);
				StatusCase statusCaseSupervition4 = services.statusCasefindByCode (Constants.CASE_STATUS_CONDITIONAL_REPRIEVE);
				StatusCase statusCaseSupervition5 = services.statusCasefindByCode (Constants.CASE_STATUS_FRAMING_INCOMPLETE);
				StatusCase statusCaseSupervition6 = services.statusCasefindByCode (Constants.CASE_STATUS_FRAMING_COMPLETE);
				StatusCase statusCaseSupervition7 = services.statusCasefindByCode (Constants.CASE_STATUS_NOT_PROSECUTE_OPEN);
				StatusCase sc = services.statusCasefindByCode (Constants.CASE_STATUS_VERIFICATION);

				var result = db.Query<MeetingTblDto> (
					            "SELECT cs.id_case as 'CaseId',cs.id_folder as 'IdFolder',im.name as 'Name',im.lastname_p as 'LastNameP',im.lastname_m as 'LastNameM',"
					            + " im.birth_date as 'DateBirth', im.gender as 'Gender', csm.status as 'StatusCode', csm.description as 'Description'"
					            + " FROM meeting as me "
					            + " left JOIN case_detention as cs ON me.id_case = cs.id_case "
					            + " left JOIN imputed as im ON im.id_meeting = me.id_meeting "
					            + " left JOIN cat_status_meeting as csm ON csm.id_status = me.id_status "
					            + " Where cs.id_status in (?,?,?,?,?,?,?) "
					            + " and me.id_reviewer = ? ", statusCaseSupervition1.Id, statusCaseSupervition2.Id,
					            statusCaseSupervition3.Id, statusCaseSupervition4.Id,
					            statusCaseSupervition5.Id, statusCaseSupervition6.Id, statusCaseSupervition7.Id, revId);
		

				var c1 = 0;
				if (result != null) {
					for (c1 = 0; c1 < result.Count; c1++) {
						result [c1].Action = "hearing";
					}
				}

				var temp = new SyncCaseListSup{ Model = result };
				var pagestring = "nada que ver";
				pagestring = temp.GenerateString ();
				webView.LoadHtmlString (pagestring);
				db.Close ();
			}
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
					return "El imputado debe tener más de 18 años para continuar";
				}
			} else {
				return "Favor de ingresar la fecha de nacimiento del imputado.";
			}
			if (model.IdFolder != null) {
				var repeated = 0;
				var fonetic = services.getFoneticByName(model.Name,model.LastNameP,model.LastNameM);
				using (var db = FactoryConn.GetConn ()) {
					var casos = db.Table<Case> ().Where (cs => cs.IdFolder == model.IdFolder).ToList ();
					if (casos != null && casos.Count > 0) {
						foreach (Case c in casos) {
							var entrevistas = db.Table<Meeting> ().Where (ent => ent.CaseDetentionId == c.Id).ToList ();
							if (entrevistas != null && entrevistas.Count > 0) {
								foreach (Meeting entrevista in entrevistas) {
									var imputado = db.Table<Imputed> ().Where (imp => imp.MeetingId == entrevista.Id
									              && imp.FoneticString == fonetic
									              && imp.BirthDate == model.DateBirth).ToList ();
									if (imputado != null && imputado.Count > 0) {
										repeated++;
									}
								}
							}
						}

					}
					db.Close ();
				}
				if(repeated>0){
					return "El número de carpeta de investigación y el imputado ya se encuentran registrados.";
				}
			} else {
				return "Favor de ingresar el número de carpeta de investigación para continuar";
			}
			return null;
		}

		public int? createMeeting(NewMeetingDto imputed) {
			using (var db = FactoryConn.GetConn ()) {
				int? result = null;
				try {
					Case caseDetention = new Case ();
					Imputed newImputed = new Imputed ();
					newImputed.Name = imputed.Name.Trim ();
					newImputed.LastNameP = imputed.LastNameP.Trim ();
					newImputed.LastNameM = imputed.LastNameM.Trim ();
					newImputed.FoneticString = services.getFoneticByName (imputed.Name, imputed.LastNameP, imputed.LastNameM);
					newImputed.Gender = imputed.Gender.GetValueOrDefault ();
					newImputed.BirthDate = imputed.DateBirth.GetValueOrDefault ();

					var reincident = db.Table<Imputed> ().Where (impu => impu.LastNameM == newImputed.LastNameM
					                && impu.LastNameP == newImputed.LastNameP && impu.Name == newImputed.Name
					                && impu.BirthDate == newImputed.BirthDate).ToList ();
					if (reincident != null && reincident.Count > 0) {
						caseDetention.Recidivist = true;
					} else {
						caseDetention.Recidivist = false;
					}

//					caseDetention.Status = services.statusCasefindByCode (Constants.CASE_STATUS_MEETING);
					caseDetention.StatusCaseId = services.statusCasefindByCode (Constants.CASE_STATUS_MEETING).Id;
					caseDetention.IdFolder = imputed.IdFolder;
					caseDetention.DateCreate = DateTime.Today;
					//caseDetention.setChangeArrangementType(false);
					// se agrega para poder contar si un caso cambia de MC a SCPP en algun formato de audiencia
					//caseDetention = caseRepository.save(caseDetention);
					db.Insert (caseDetention);
					Meeting meeting = new Meeting ();
					meeting.MeetingType = Constants.MEETING_PROCEDURAL_RISK;
					meeting.CaseDetentionId = caseDetention.Id;
//					meeting.CaseDetention = caseDetention;
					StatusMeeting statusMeeting = services.statusMeetingfindByCode (Constants.S_MEETING_INCOMPLETE);
					meeting.StatusMeetingId = statusMeeting.Id;
//					meeting.StatusMeeting = statusMeeting;
					//				meeting.ReviewerId=LoggedUserId(); TODO agrega al usuario asociado al dispositivo
					meeting.DateCreate = DateTime.Today;
					db.Insert (meeting);
					newImputed.MeetingId = meeting.Id;
//					newImputed.Meeting = meeting;
					db.Insert (newImputed);
					db.Update (meeting);
					db.Update (caseDetention);
					//				imputedRepository.save(imputed);
					result = caseDetention.Id;
				} catch (Exception e) {
					Console.WriteLine ("e.Message>" + e.Message + "<< createMeeting");
				} 
				db.Close ();
				return result;
			}
		}

		public void  MeetingDatosPersonales(int idCase)
		{
			using (var db = FactoryConn.GetConn ()) {
				if (idCase == 0) {
					idCase = db.Table<Case> ().FirstOrDefault ().Id;
				}
				var result = db.Query<MeetingDatosPersonalesDto> (
					            "SELECT cs.id_folder as 'IdFolder', im.id_imputed as 'ImputedId',im.name as 'Name',im.lastname_p as 'LastNameP',im.lastname_m as 'LastNameM'"
					            + " ,im.birth_date as 'BirthDate', im.gender as 'Gender'"
					            + " ,im.fonetic_string as 'FoneticString', im.cel_phone as 'CelPhone'"
					            + " ,im.years_marital_status as 'YearsMaritalStatus', im.id_marital_status as 'MaritalStatusId'"
					            + " ,im.boys as 'Boys', im.dependent_boys as 'DependentBoys'"
					            + " ,im.id_country as 'BirthCountry', im.birth_municipality as 'BirthMunicipality'"
					            + " ,im.birth_state as 'BirthState', im.birth_location as 'BirthLocation'"
					            + " ,im.nickname as 'Nickname', im.id_location as 'LocationId'"
					            + " ,me.id_meeting as 'MeetingId'"
					            + " ,me.id_reviewer as 'ReviewerId', me.id_status as 'StatusMeetingId'"
					            + " ,me.comment_refernce as 'CommentReference', me.comment_job as 'CommentJob'"
					            + " ,me.comment_school as 'CommentSchool', me.comment_country as 'CommentCountry'"
					            + " ,me.comment_home as 'CommentHome', me.comment_drug as 'CommentDrug'"
					            + " ,me.date_create as 'DateCreate', me.date_terminate as 'DateTerminate'"
				//				+", csm.status as 'StatusCode', csm.description as 'Description'"
					            + " FROM meeting as me "
					            + " left JOIN case_detention as cs ON me.id_case = cs.id_case "
					            + " left JOIN imputed as im ON im.id_meeting = me.id_meeting "
				//				+" left JOIN cat_status_meeting as csm ON csm.id_status = me.id_status "
				//				+" and me.id_reviewer = 2 "
					            + " where cs.id_case = ?; ", idCase).FirstOrDefault ();
				result.CaseId = idCase;

				result.ageString = services.calculateAge (result.BirthDate);

				var domiciliosImputado = db.Table<ImputedHome> ().Where (im => im.MeetingId == result.MeetingId).ToList ();
				if (domiciliosImputado != null) {
					result.JsonDomicilios = domiciliosImputado;
				}


				var SE = db.Table<SocialEnvironment> ().Where (s => s.MeetingId == result.MeetingId).FirstOrDefault ();
				if (SE != null) {
					result.PhysicalCondition = SE.physicalCondition;
					result.comment = SE.comment;
					var ActList = db.Table<RelActivity> ().Where (s => s.SocialEnvironmentId == SE.Id).ToList ();
					if (ActList != null && ActList.Count > 0) {
						result.Activities = JsonConvert.SerializeObject (ActList);
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
				var personsSocNet = db.Table<PersonSocialNetwork> ().Where (sn => sn.SocialNetworkId == socialNetComent.Id).ToList ();
				if (personsSocNet != null) {
					result.JsonPersonSN = personsSocNet;
				} else {
					result.JsonPersonSN = null;
				}

				//Reference
				var references = db.Table<Reference> ().Where (sn => sn.MeetingId == result.MeetingId).ToList ();
				if (references != null) {
					result.JsonReferences = references;
				} else {
					result.JsonReferences = null;
				}

				//Laboral History
				var trabajos = db.Table<Job> ().Where (sn => sn.MeetingId == result.MeetingId).ToList ();
				if (trabajos != null) {
					result.JsonJobs = trabajos;
				} else {
					result.JsonJobs = null;
				}

				//school history
				var escuelaUtlActual = db.Table<School> ().Where (sc => sc.MeetingId == result.MeetingId).FirstOrDefault ();
				if (escuelaUtlActual != null) {
					result.SchoolAddress = escuelaUtlActual.Address;
					result.SchoolBlock = escuelaUtlActual.block;
					result.SchoolDegreeId = escuelaUtlActual.DegreeId.GetValueOrDefault ();
					result.SchoolName = escuelaUtlActual.Name;
					result.SchoolPhone = escuelaUtlActual.Phone;
					result.SchoolSpecification = escuelaUtlActual.Specification;
				}


				//DROGAS
				var drogas = db.Table<Drug> ().Where (sn => sn.MeetingId == result.MeetingId).ToList ();
				if (drogas != null) {
					result.JsonDrugs = drogas;
				} else {
					result.JsonDrugs = null;
				}


				if (escuelaUtlActual != null) {
					var schedule = db.Table<Schedule> ().Where (sc => sc.SchoolId == escuelaUtlActual.Id).ToList ();
					if (schedule != null) {
						result.ScheduleSchool = JsonConvert.SerializeObject (schedule);
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


				string output = JsonConvert.SerializeObject (result);
				result.JsonMeeting = output;




				var temp = new MeetingDatosPersonales{ Model = result };
				//			var temp = new NewMeeting{Model = new EntrevistaTabla{Name="nombre" , DateBirthString=DateTime.Today.ToString("yyyy/mm/dd")} };
				var pagestring = "nada que ver";
				pagestring = temp.GenerateString ();
				webView.LoadHtmlString (pagestring);
				db.Close ();
			}
		}

		public void SaveMeetingDatosPersonales([Bind]MeetingDatosPersonalesDto model) {
			using (var db = FactoryConn.GetConn ()) {
				var imputado = db.Get<Imputed> (model.ImputedId); 
				imputado.Name = model.Name;
				imputado.LastNameP = model.LastNameP;
				imputado.LastNameM = model.LastNameM;
				imputado.FoneticString = services.getFoneticByName (model.Name, model.LastNameP, model.LastNameM);
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

				db.Update (imputado);
				string output = JsonConvert.SerializeObject (model);
				model.JsonMeeting = output;


				var temp = new MeetingDatosPersonales{ Model = model };
				var pagestring = "nada que ver";
				pagestring = temp.GenerateString ();
				webView.LoadHtmlString (pagestring);
				db.Close ();
			}
		}

		public void  MeetingDomicilio(int idMeeting)
		{
			using (var db = FactoryConn.GetConn ()) {
				var MeetingId = int.Parse (idMeeting.ToString ());
				var dto = new ModelContainer ();
				dto.Reference = db.Table<Meeting> ().Where (s => s.Id == MeetingId).FirstOrDefault ().CaseDetentionId.ToString () ?? "";
				ImputedHome mdl = new ImputedHome ();
				mdl.MeetingId = idMeeting;
				dto.JsonModel = JsonConvert.SerializeObject (mdl);
				var temp = new AddressUpsert{ Model = dto };
				var pagestring = "nada que ver";
				pagestring = temp.GenerateString ();
				webView.LoadHtmlString (pagestring);
				db.Close ();
			}
		}

		public void  EditMeetingDomicilio(int idHome)
		{
			using (var db = FactoryConn.GetConn ()) {
				var HomeId = int.Parse (idHome.ToString ());
				var dto = new ModelContainer ();
				ImputedHome mdl = db.Table<ImputedHome> ().Where (lv => lv.Id == HomeId).FirstOrDefault ();
				if (mdl != null) {
					var schedule = db.Table<Schedule> ().Where (sc => sc.ImputedHomeId == mdl.Id).ToList ();
					if (schedule != null) {
						mdl.Schedule = JsonConvert.SerializeObject (schedule);
					}
				}
				if (mdl != null) {
					dto.JsonModel = JsonConvert.SerializeObject (mdl);
					dto.Reference = db.Table<Meeting> ().Where (s => s.Id == mdl.MeetingId).FirstOrDefault ().CaseDetentionId.ToString () ?? "";
				} else {
					mdl = new ImputedHome ();
					mdl.MeetingId = idHome;
					dto.JsonModel = JsonConvert.SerializeObject (mdl);
				}

				var temp = new AddressUpsert{ Model = dto };
				//			var temp = new NewMeeting{Model = new EntrevistaTabla{Name="nombre" , DateBirthString=DateTime.Today.ToString("yyyy/mm/dd")} };
				var pagestring = "nada que ver";
				pagestring = temp.GenerateString ();
				webView.LoadHtmlString (pagestring);
				db.Close ();
			}
		}

		public void  PersonSocialNetwork(int idMeeting)
		{
			using (var db = FactoryConn.GetConn ()) {
				try{
					var MeetingId = int.Parse (idMeeting.ToString ());
					var dto = new ModelContainer ();
					dto.Reference = db.Table<Meeting>().Where(s=>s.Id == idMeeting).FirstOrDefault().CaseDetentionId.ToString()??"";
					SocialNetwork me = db.Table<SocialNetwork>().Where(mee => mee.MeetingId == idMeeting ).FirstOrDefault();
					if(me==null){
						me = new SocialNetwork();
						me.Comment = "";
						me.MeetingId = idMeeting;
						db.Insert(me);
					}
					PersonSocialNetwork mdl = new PersonSocialNetwork ();
					mdl.SocialNetworkId = me.Id;
					dto.JsonModel = JsonConvert.SerializeObject (mdl);
					var temp = new PersonSocialNetworkUpsert{ Model = dto };
					var pagestring = "nada que ver";
					pagestring = temp.GenerateString ();
					webView.LoadHtmlString (pagestring);
				}catch(Exception e){
					db.Rollback ();
					Console.WriteLine ("catched exception in SyncController method PersonSocialNetwork");
					Console.WriteLine("Exception message :::>"+e.Message);
				}
				finally{
					db.Commit ();
				}
				db.Close();
			}
		}

		public void  EditPersonSocialNetwork(int idPerson)
		{
			using (var db = FactoryConn.GetConn ()) {
				try{
					var SNPersonId = int.Parse (idPerson.ToString ());
					var dto = new ModelContainer ();
					var mdl = db.Table<PersonSocialNetwork>().Where(mee => mee.Id == SNPersonId ).FirstOrDefault();
					var sn = db.Table<SocialNetwork>().Where(a=>a.Id == mdl.SocialNetworkId).FirstOrDefault();
					dto.Reference = db.Table<Meeting>().Where(s=>s.Id == sn.MeetingId).FirstOrDefault().CaseDetentionId.ToString()??"";
					dto.JsonModel = JsonConvert.SerializeObject (mdl);
					var temp = new PersonSocialNetworkUpsert{ Model = dto };
					var pagestring = "nada que ver";
					pagestring = temp.GenerateString ();
					webView.LoadHtmlString (pagestring);
				}catch(Exception e){
					db.Rollback ();
					Console.WriteLine ("catched exception in SyncController method PersonSocialNetwork");
					Console.WriteLine("Exception message :::>"+e.Message);
				}
				finally{
					db.Commit ();
				}
				db.Close();
			}
		}


		public void  ReferenceMeeting(int idMeeting)
		{
			using (var db = FactoryConn.GetConn ()) {
				try{
					var MeetingId = int.Parse (idMeeting.ToString ());
					var dto = new ModelContainer ();
					dto.Reference = db.Table<Meeting>().Where(s=>s.Id == idMeeting).FirstOrDefault().CaseDetentionId.ToString()??"";
					Reference mdl = new Reference ();
					mdl.MeetingId = MeetingId;
					dto.JsonModel = JsonConvert.SerializeObject (mdl);
					var temp = new ReferenciasUpsert{ Model = dto };
					var pagestring = "nada que ver";
					pagestring = temp.GenerateString ();
					webView.LoadHtmlString (pagestring);
				}catch(Exception e){
					db.Rollback ();
					Console.WriteLine ("catched exception in SyncController method PersonSocialNetwork");
					Console.WriteLine("Exception message :::>"+e.Message);
				}
				finally{
					db.Commit ();
				}
				db.Close();
			}
		}

		public void  EditReferenceMeeting(int idReference)
		{
			using (var db = FactoryConn.GetConn ()) {
				try{
					var referenceId = int.Parse (idReference.ToString ());
					var dto = new ModelContainer ();
					var mdl = db.Table<Reference>().Where(mee => mee.Id == referenceId ).FirstOrDefault();
					dto.Reference = db.Table<Meeting>().Where(s=>s.Id == mdl.MeetingId).FirstOrDefault().CaseDetentionId.ToString()??"";
					dto.JsonModel = JsonConvert.SerializeObject (mdl);
					var temp = new ReferenciasUpsert{ Model = dto };
					var pagestring = "nada que ver";
					pagestring = temp.GenerateString ();
					webView.LoadHtmlString (pagestring);
				}catch(Exception e){
					db.Rollback ();
					Console.WriteLine ("catched exception in SyncController method PersonSocialNetwork");
					Console.WriteLine("Exception message :::>"+e.Message);
				}
				finally{
					db.Commit ();
				}
				db.Close();
			}
		}

		public void  JobMeeting(int idMeeting)
		{
			using (var db = FactoryConn.GetConn ()) {
				try{
					var MeetingId = int.Parse (idMeeting.ToString ());
					var dto = new ModelContainer ();
					dto.Reference = db.Table<Meeting>().Where(s=>s.Id == idMeeting).FirstOrDefault().CaseDetentionId.ToString()??"";
					Job mdl = new Job ();
					mdl.MeetingId = MeetingId;
					dto.JsonModel = JsonConvert.SerializeObject (mdl);
					var temp = new JobUpsert{ Model = dto };
					var pagestring = "nada que ver";
					pagestring = temp.GenerateString ();
					webView.LoadHtmlString (pagestring);
				}catch(Exception e){
					db.Rollback ();
					Console.WriteLine ("catched exception in SyncController method PersonSocialNetwork");
					Console.WriteLine("Exception message :::>"+e.Message);
				}
				finally{
					db.Commit ();
				}
				db.Close();
			}
		}

		public void  EditJobMeeting(int idJob)
		{
			using (var db = FactoryConn.GetConn ()) {
				try{
					var jobId = int.Parse (idJob.ToString ());
					var dto = new ModelContainer ();
					var mdl = db.Table<Job>().Where(mee => mee.Id == jobId ).FirstOrDefault();
					dto.Reference = db.Table<Meeting>().Where(s=>s.Id == mdl.MeetingId).FirstOrDefault().CaseDetentionId.ToString()??"";
					dto.JsonModel = JsonConvert.SerializeObject (mdl);
					var temp = new JobUpsert{ Model = dto };
					var pagestring = "nada que ver";
					pagestring = temp.GenerateString ();
					webView.LoadHtmlString (pagestring);
				}catch(Exception e){
					db.Rollback ();
					Console.WriteLine ("catched exception in SyncController method PersonSocialNetwork");
					Console.WriteLine("Exception message :::>"+e.Message);
				}
				finally{
					db.Commit ();
				}
				db.Close();
			}
		}

		public void  DrugMeeting(int idMeeting)
		{
			using (var db = FactoryConn.GetConn ()) {
				try{
					var MeetingId = int.Parse (idMeeting.ToString ());
					var dto = new ModelContainer ();
					dto.Reference = db.Table<Meeting>().Where(s=>s.Id == idMeeting).FirstOrDefault().CaseDetentionId.ToString()??"";
					Drug mdl = new Drug ();
					mdl.MeetingId = MeetingId;
					dto.JsonModel = JsonConvert.SerializeObject (mdl);
					var temp = new DrugUpsert{ Model = dto };
					var pagestring = "nada que ver";
					pagestring = temp.GenerateString ();
					webView.LoadHtmlString (pagestring);
				}catch(Exception e){
					db.Rollback ();
					Console.WriteLine ("catched exception in SyncController method PersonSocialNetwork");
					Console.WriteLine("Exception message :::>"+e.Message);
				}
				finally{
					db.Commit ();
				}
			db.Close();
			}
		}

		public void  EditDrugMeeting(int idDrug)
		{
			using (var db = FactoryConn.GetConn ()) {
				try{
					var drugId = int.Parse (idDrug.ToString ());
					var dto = new ModelContainer ();
					var mdl = db.Table<Drug>().Where(mee => mee.Id == drugId ).FirstOrDefault();
					dto.Reference = db.Table<Meeting>().Where(s=>s.Id == mdl.MeetingId).FirstOrDefault().CaseDetentionId.ToString()??"";
					dto.JsonModel = JsonConvert.SerializeObject (mdl);
					var temp = new DrugUpsert{ Model = dto };
					var pagestring = "nada que ver";
					pagestring = temp.GenerateString ();
					webView.LoadHtmlString (pagestring);
				}catch(Exception e){
					db.Rollback ();
					Console.WriteLine ("catched exception in SyncController method PersonSocialNetwork");
					Console.WriteLine("Exception message :::>"+e.Message);
				}
				finally{
					db.Commit ();
				}
				db.Close();
			}
		}



		public void IndexVerificacion()
		{
			StatusMeeting statusMeeting1 = services.statusMeetingfindByCode(Constants.S_MEETING_INCOMPLETE);
			StatusMeeting statusMeeting2 = services.statusMeetingfindByCode(Constants.S_MEETING_INCOMPLETE_LEGAL);
			StatusCase sc = services.statusCasefindByCode(Constants.CASE_STATUS_MEETING);
			using (var db = FactoryConn.GetConn ()) {
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



			var temp = new MeetingList{Model = result};
			var pagestring = "nada que ver";
			pagestring = temp.GenerateString ();
			webView.LoadHtmlString (pagestring);
				db.Close();
			}
		}


	}
}

