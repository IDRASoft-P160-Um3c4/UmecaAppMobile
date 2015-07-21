using System;
using SQLite.Net;
//insert with children etc
using SQLiteNetExtensions.Extensions;
//query to list
using System.Linq;
//listas
using System.Collections.Generic;

using Java.Interop;
using Newtonsoft.Json;
using Android.Content;

//cript
using BCrypt;

using Umeca.Data;

namespace UmecaApp
{
	public class SyncService  : Java.Lang.Object
	{

		readonly SQLiteConnection db;
		readonly CatalogServiceController services;


		Context context;

		public SyncService(Context context)
		{
			this.context = context;
			db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			services = new CatalogServiceController ();
		}

		public SyncService ()
		{
			db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			services = new CatalogServiceController ();
		}

		[Export("userUpsert")]
		public Java.Lang.String userUpsert(Java.Lang.String user,Java.Lang.String pass){
			localhostUmecaWs.UmecaWS uwsl = new UmecaApp.localhostUmecaWs.UmecaWS ();
			var ecodedPass = Crypto.HashPassword (pass.ToString());
			var usuario = user.ToString ();
			try{
				db.BeginTransaction();
				db.CreateTable<User> ();
				var respuesta = uwsl.loginFromTablet (usuario,ecodedPass);
				if(respuesta.hasError){
					return new Java.Lang.String ("{\"error\":true, \"response\":\""+respuesta.message+"\"}");
				}else{
					var usuarios = db.Table<User>().ToList();
					foreach(User u in usuarios){
						db.Delete(u);
					}
					User asociado = new User();
					var dataString =  (System.Xml.XmlNode[]) respuesta.returnData;
					var getData = JsonConvert.DeserializeObject<TabletUserDto>(dataString[0].Value.ToString());
					var roleUsr = db.Table<Role>().Where(rl=>rl.role == getData.roleCode).FirstOrDefault();
					asociado.fullname = getData.fullname;
					asociado.roles = roleUsr.Id;
					asociado.Id = getData.id??0;
					asociado.username = usuario;
					asociado.password = ecodedPass;
					db.Insert(asociado);
					String donde = "";
					if(asociado.roles==2){
						donde = "Meeting";
					}else{
						donde = "Supervision";
					}
					return new Java.Lang.String ("{\"error\":false, \"response\":\""+donde+"\"}");
				}
			}catch(Exception e){
				db.Rollback ();
				Console.WriteLine ("exception in userUpsert()");
				Console.WriteLine("Exception message :::>"+e.Message);
			}finally{
				db.Commit ();
			}
			db.CreateTable<User> ();
			var usrList = db.Table<User> ().ToList ();
			if (usrList != null && usrList.Count > 0) {
				var savedUsr = usrList [0];
				if (savedUsr.password == ecodedPass && savedUsr.username == usuario) {
					String direct = "";
					if(savedUsr.roles==2){
						direct = "Meeting";
					}else{
						direct = "Supervision";
					}
					return new Java.Lang.String ("{\"error\":false, \"response\":\""+direct+"\"}");
				} else {
					
					return new Java.Lang.String ("{\"error\":true, \"response\":\"El usuario y/o password son incorrectos. Favor de verificar los datos e intente nuevamente\"}");
				}
			} else {
				return new Java.Lang.String ("{\"error\":true, \"response\":\"No se encontro ningun usuario asociado\"}");
			}
		}



		[Export("downloadVerificacion")]
		public Java.Lang.String downloadVerificacion(Java.Lang.String pass){
			String guid = "";
			User revisor = new User();
			localhostUmecaWs.UmecaWS uwsl = new UmecaApp.localhostUmecaWs.UmecaWS ();
			var ecodedPass = Crypto.HashPassword (pass.ToString());
			db.CreateTable<User> ();
			var usrList = db.Table<User> ().ToList ();
			if (usrList != null && usrList.Count > 0) {
				//login tablet
				try{
					var savedUsr = usrList [0];
					var respuesta = uwsl.loginFromTablet (savedUsr.username, ecodedPass);
					if(respuesta.hasError){
						return new Java.Lang.String ("{\"error\":true, \"response\":\""+respuesta.message+"\"}");
					}else{
						var usuarios = db.Table<User>().ToList();
						foreach(User u in usuarios){
							db.Delete(u);
						}
						User asociado = new User();
						var dataString =  (System.Xml.XmlNode[]) respuesta.returnData;
						var getData = JsonConvert.DeserializeObject<TabletUserDto>(dataString[0].Value.ToString());
						var roleUsr = db.Table<Role>().Where(rl=>rl.role == getData.roleCode).FirstOrDefault();
						asociado.fullname = getData.fullname;
						asociado.roles = roleUsr.Id;
						asociado.Id = getData.id??0;
						asociado.username = savedUsr.username;
						asociado.password = ecodedPass;
						db.Insert(asociado);
						revisor = asociado;
						guid = getData.guid;
					}
				}catch(Exception e){
					db.Rollback ();
					Console.WriteLine ("exception in downloadVerificacion() login");
					Console.WriteLine("Exception message :::>"+e.Message);
					return new Java.Lang.String ("{\"error\":true, \"response\":\"Conexion fallida intente nuevamente.\"}");
				}finally{
					db.Commit ();
				}
				//obtencion de asignaciones
				List<TabletAssignmentInfo> listAsignados = new List<TabletAssignmentInfo>();
				try{
					var asigmentsResponse = uwsl.getAssignmentsByUser(revisor.username,guid);
					if(asigmentsResponse.hasError){
						return new Java.Lang.String ("{\"error\":true, \"response\":\""+asigmentsResponse.message+"\"}");
					}else{
						var dataString =  (System.Xml.XmlNode[]) asigmentsResponse.returnData;
						var getData = JsonConvert.DeserializeObject<List<TabletAssignmentInfo>>(dataString[0].Value.ToString());
						if(getData!=null && getData.Count>0){
							listAsignados = getData;
						}
					}
				}catch(Exception e){
					Console.WriteLine ("exception in downloadVerificacion() asigments");
					Console.WriteLine("Exception message :::>"+e.Message);
					return new Java.Lang.String ("{\"error\":true, \"response\":\"Conexion fallida intente nuevamente.\"}");
				}

				//obtencion de cada asignacion de la lista obtenida
				int exitos = 0;
				foreach(TabletAssignmentInfo tbltAi in listAsignados){
					try{
						db.BeginTransaction();
						var caseAsignResponse = uwsl.getAssignedCaseByAssignmentId(revisor.username,guid,tbltAi.id,true);
						if(caseAsignResponse.hasError){
							return new Java.Lang.String ("{\"error\":true, \"response\":\""+caseAsignResponse.message+"\"}");
						}else{
							var dataString1 =  (System.Xml.XmlNode[])  caseAsignResponse.returnData;
							var getData1 = JsonConvert.DeserializeObject<TabletCaseDto>(dataString1[0].Value.ToString());
							Imputed imp = new Imputed();
							Meeting me = new Meeting();
							Case cs = new Case();
							Verification ve = new Verification();
							cs = getData1.CaseToObject();
							//se salva al caso
							var anterior = db.Table<Case>().Where(cas=>cas.webId == cs.webId).FirstOrDefault();
							if(anterior!=null){
								cs.Id = anterior.Id;
							}else{
								db.Insert(cs);
							}
							//hearingFormats
							if(getData1.hearingFormats!=null&& getData1.hearingFormats.Count>0){
								foreach(TabletHearingFormatDto hfDto in getData1.hearingFormats){
									HearingFormat Formato = new HearingFormat();

									//se inserta el hearing imputed con su address
									if(hfDto.hearingImputed!=null){
										var himp = hfDto.hearingImputed;
										HearingFormatImputed hfimp = new HearingFormatImputed();
										if(!string.IsNullOrEmpty (himp.birthDate)){
											hfimp.BirthDate = DateTime.ParseExact (himp.birthDate, "yyyy/MM/dd",
												System.Globalization.CultureInfo.InvariantCulture);
										}
										hfimp.ImputeTel = himp.imputeTel;
										hfimp.LastNameM = himp.lastNameM;
										hfimp.LastNameP = himp.lastNameP;
										hfimp.Name = himp.name;
										if(hfDto.hearingImputed.address!=null){
											var adrs = hfDto.hearingImputed.address;
											Address ad = new Address();
											ad.addressString = adrs.addressString;
											ad.InnNum = adrs.innNum;
											ad.Lat = adrs.lat;
											ad.Lng = adrs.lng;
											if(adrs.location!=null){
												ad.LocationId = adrs.location.id;
											}
											ad.OutNum = adrs.outNum;
											ad.Street = adrs.street;
											db.Insert(ad);
											hfimp.Address = ad.Id;
										}
										db.Insert(hfimp);
										Formato.hearingImputed = hfimp.Id;
									}
									//se agregan los specs del formato
									if(hfDto.hearingFormatSpecs!=null){
										var spec = hfDto.hearingFormatSpecs;
										HearingFormatSpecs hfs = new HearingFormatSpecs();
										hfs.ArrangementType = spec.arrangementType;
										hfs.ControlDetention = spec.controlDetention;
										if(!string.IsNullOrEmpty (spec.extDate)){
											hfs.ExtDate = DateTime.ParseExact (spec.extDate, "yyyy/MM/dd",
												System.Globalization.CultureInfo.InvariantCulture);
										}
										hfs.Extension = spec.extension;
										if(!string.IsNullOrEmpty (spec.imputationDate)){
											hfs.ImputationDate = DateTime.ParseExact (spec.imputationDate, "yyyy/MM/dd",
												System.Globalization.CultureInfo.InvariantCulture);
										}
										hfs.ImputationFormulation = spec.imputationFormulation;
										if(!string.IsNullOrEmpty (spec.linkageDate)){
											hfs.LinkageDate = DateTime.ParseExact (spec.linkageDate, "yyyy/MM/dd",
												System.Globalization.CultureInfo.InvariantCulture);
										}
										hfs.LinkageProcess = spec.linkageProcess;
										hfs.LinkageRoom = spec.linkageRoom;
										if(!string.IsNullOrEmpty (spec.linkageTime)){
											hfs.LinkageTime = DateTime.ParseExact (spec.linkageTime, "HH:mm:ss",
												System.Globalization.CultureInfo.InvariantCulture);
										}
										hfs.NationalArrangement = spec.nationalArrangement;
										db.Insert(hfs);
										Formato.HearingFormatSpecs = hfs.Id;
									}

									//llenado del hearingformat
									if (!string.IsNullOrEmpty (hfDto.appointmentDateTime)) {
										Formato.AppointmentDate = DateTime.ParseExact (hfDto.appointmentDateTime, "HH:mm:ss",
											System.Globalization.CultureInfo.InvariantCulture);
									}
									Formato.CaseDetention = cs.Id;
									Formato.Comments = hfDto.comments;
									Formato.ConfirmComment = hfDto.confirmComment;
									Formato.DefenderName = hfDto.defenderName;

									if(!string.IsNullOrEmpty (hfDto.endTime)){
										Formato.EndTime = DateTime.ParseExact (hfDto.endTime, "HH:mm:ss",
											System.Globalization.CultureInfo.InvariantCulture);
									}
									Formato.HearingResult = hfDto.hearingResult;
									if(hfDto.hearingType!=null){
										Formato.HearingType = hfDto.hearingType.id;
									}
									Formato.HearingTypeSpecification = hfDto.hearingTypeSpecification;
									Formato.IdFolder = hfDto.idFolder;
									Formato.IdJudicial = hfDto.idJudicial;
									Formato.ImputedPresence = hfDto.imputedPresence;
									if(!string.IsNullOrEmpty (hfDto.initTime)){
										Formato.InitTime = DateTime.ParseExact (hfDto.initTime, "HH:mm:ss",
											System.Globalization.CultureInfo.InvariantCulture);
									}
									Formato.IsFinished = hfDto.isFinished??false;
									Formato.JudgeName = hfDto.judgeName;
									Formato.MpName = hfDto.mpName;
									Formato.PreviousHearing = hfDto.previousHearing;
									if(!string.IsNullOrEmpty (hfDto.registerTime)){
										Formato.RegisterTime = DateTime.ParseExact (hfDto.registerTime, "HH:mm:ss",
											System.Globalization.CultureInfo.InvariantCulture);
									}
									Formato.Room = hfDto.room;
									Formato.ShowNotification = hfDto.showNotification??true;
									Formato.Supervisor = revisor.Id;
									Formato.Terms = hfDto.terms;
									if(!string.IsNullOrEmpty (hfDto.umecaDate)){
										Formato.UmecaDate = DateTime.ParseExact (hfDto.umecaDate, "yyyy/MM/dd",
											System.Globalization.CultureInfo.InvariantCulture);
									}
									Formato.UmecaSupervisor = revisor.Id;
									if(!string.IsNullOrEmpty (hfDto.umecaTime)){
										Formato.UmecaTime = DateTime.ParseExact (hfDto.umecaTime, "HH:mm:ss",
											System.Globalization.CultureInfo.InvariantCulture);
									}
									db.Insert(Formato);
									if(hfDto.assignedArrangements!=null&&hfDto.assignedArrangements.Count>0){
										foreach(TabletAssignedArrangementDto asArr in hfDto.assignedArrangements ){
											AssignedArrangement aa = new AssignedArrangement();
											if(asArr.arrangement!=null){
												aa.Arrangement = asArr.arrangement.id;
											}
											aa.Description = asArr.description;
											aa.HearingFormat = Formato.Id;
											db.Insert(aa);
										}
									}

									if(hfDto.contacts!=null&&hfDto.contacts.Count>0){
										foreach(TabletContactDataDto contc in hfDto.contacts ){
											ContactData cnt = new ContactData();
											cnt.AddressTxt = contc.addressTxt;
											cnt.NameTxt = contc.nameTxt;
											cnt.PhoneTxt = contc.phoneTxt;
											cnt.HearingFormat = Formato.Id;
											db.Insert(cnt);
										}
									}

									if(hfDto.crimeList!=null&&hfDto.crimeList.Count>0){
										foreach(TabletCrimeDto crimen in hfDto.crimeList ){
											Crime cri = new Crime();
											cri.Article = crimen.article;
											cri.Comment = crimen.comment;
											if(crimen.federal!=null){
												cri.Federal = crimen.federal.id;
											}
											if(crimen.crime!=null){
												cri.IdCrimeCat = crimen.crime.id;
											}
											cri.HearingFormat = Formato.Id;
											db.Insert(cri);
										}
									}

									//									hfDto.crimeList;
									HearingFormat hfn = new HearingFormat();

									hfn.CaseDetention = cs.Id;
								}//end foreach formats
							}//end de hearingFormats


							if(getData1.meeting!=null){
								TabletMeetingDto tme = getData1.meeting;
								me = tme.MeetingToObject();
								if(me.ReviewerId==null||me.ReviewerId==0){
									me.ReviewerId = revisor.Id;
								}
								me.CaseDetentionId = cs.Id;
								db.Insert(me);

								if(getData1.meeting!=null && getData1.meeting.imputed!=null ){
									TabletImputedDto tid = new TabletImputedDto();
									tid = getData1.meeting.imputed;
									imp = tid.ImputedDtoToObject();
									imp.MeetingId = me.Id;
									db.Insert(imp);
								}//end de tiene imputado

								if(tme.drugs!=null&&tme.drugs.Count>0){
									foreach(TabletDrugDto tdrug in tme.drugs){
										Drug d = new Drug();
										d.block = tdrug.block;
										if(tdrug.drugType!=null){
											d.DrugTypeId = tdrug.drugType.id;
										}
										if(!string.IsNullOrEmpty (tdrug.lastUse)){
											d.LastUse = DateTime.ParseExact (tdrug.lastUse, "HH:mm:ss",
												System.Globalization.CultureInfo.InvariantCulture);
										}
										d.MeetingId = me.Id;
										d.OnsetAge = tdrug.onsetAge;
										if(tdrug.periodicity!=null){
											d.PeriodicityId = tdrug.periodicity.id;
										}
										d.Quantity = tdrug.quantity;
										d.Specification = tdrug.specificationType;
										d.SpecificationPeriodicity = tdrug.specificationPeriodicity;
										db.Insert(d);
									}
								}

								if(tme.jobs!=null&&tme.jobs.Count>0){
									foreach(TabletJobDto tjob in tme.jobs){
										Job j = new Job();
										j.Address = tjob.address;
										j.block = tjob.block;
										j.Company = tjob.company;
										if(!string.IsNullOrEmpty (tjob.end)){
											j.End = DateTime.ParseExact (tjob.end, "HH:mm:ss",
												System.Globalization.CultureInfo.InvariantCulture);
										}
										j.MeetingId = me.Id;
										j.NameHead = tjob.nameHead;
										j.Phone = tjob.phone;
										j.Post = tjob.post;
										j.ReasonChange = tjob.reasonChange;
										if(tjob.registerType!=null){
											j.RegisterTypeId = tjob.registerType.id;
										}
										j.SalaryWeek = tjob.salaryWeek;

										if(!string.IsNullOrEmpty (tjob.start)){
											j.Start = DateTime.ParseExact (tjob.start, "HH:mm:ss",
												System.Globalization.CultureInfo.InvariantCulture);
										}
										if(!string.IsNullOrEmpty (tjob.startPrev)){
											j.StartPrev = DateTime.ParseExact (tjob.startPrev, "HH:mm:ss",
												System.Globalization.CultureInfo.InvariantCulture);
										}
										db.Insert(j);
										if(tjob.schedule!=null && tjob.schedule.Count>0){
											foreach(TabletScheduleDto tsch in tjob.schedule){
												var sh = new Schedule();
												sh.Day = tsch.day;
												sh.End = tsch.end;
												sh.Start = tsch.start;
												sh.webId = tsch.webId;
												sh.JobId = j.Id;
												db.Insert(sh);
											}
										}
									}
								}// end og jobs

								if(tme.leaveCountry!=null){
									LeaveCountry lc = new LeaveCountry();
									lc.Address = tme.leaveCountry.address;
									if(tme.leaveCountry.communicationFamily!=null){
										lc.CommunicationFamilyId = tme.leaveCountry.communicationFamily.id;
									}
									if(tme.leaveCountry.country!=null){
										lc.CountryId = tme.leaveCountry.country.id;
									}
									if(tme.leaveCountry.familyAnotherCountry!=null){
										lc.FamilyAnotherCountryId = tme.leaveCountry.familyAnotherCountry.id;
									}
									if(tme.leaveCountry.immigrationDocument!=null){
										lc.ImmigrationDocumentId = tme.leaveCountry.familyAnotherCountry.id;
									}
									if(tme.leaveCountry.livedCountry!=null){
										lc.LivedCountryId = tme.leaveCountry.livedCountry.id;
									}
									lc.Media = tme.leaveCountry.media;
									lc.MeetingId = me.Id;
									if(tme.leaveCountry.officialDocumentation!=null){
										lc.OfficialDocumentationId = tme.leaveCountry.officialDocumentation.id;
									}
									lc.Reason = tme.leaveCountry.reason;
									if(tme.leaveCountry.relationship!=null){
										lc.RelationshipId = tme.leaveCountry.relationship.id;
									}
									lc.SpecficationImmigranDoc = tme.leaveCountry.specficationImmigranDoc;
									lc.SpecificationRelationship = tme.leaveCountry.specificationRelationship;
									lc.State = tme.leaveCountry.state;
									lc.timeAgo = tme.leaveCountry.timeAgo;
									lc.TimeResidence = tme.leaveCountry.timeResidence;
									lc.webId = tme.leaveCountry.webId;
									db.Insert(lc);
								}//end leave country

								if(tme.references!=null && tme.references.Count>0){
									foreach(TabletReferenceDto tref in tme.references){
										Reference nref = new Reference();
										nref.Address = tref.address;
										nref.Age = tref.age;
										nref.block = tref.block;
										nref.FullName = tref.fullName;
										nref.IsAccompaniment = tref.isAccompaniment;
										nref.MeetingId = me.Id;
										nref.Phone = tref.phone;
										nref.SpecificationDocumentType = tref.specification;
										nref.SpecificationRelationship = tref.specificationRelationship;
										nref.IsAccompaniment = tref.isAccompaniment;
										nref.webId = tref.webId;
										if(tref.documentType!=null){
											nref.DocumentTypeId = tref.documentType.id??0;
										}
										if(tref.relationship!=null){
											nref.RelationshipId = tref.relationship.id??0;
										}
										db.Insert(nref);
									}
								}//end references


							}// end de meeting

							//se inserta la verificacion
							if(getData1.verification!=null){
								TabletVerificationDto vmd = getData1.verification;
								ve.CaseDetentionId = cs.Id;
								if(!string.IsNullOrEmpty (vmd.dateComplete)){
									ve.DateComplete = DateTime.ParseExact (vmd.dateComplete, "yyyy/MM/dd",
										System.Globalization.CultureInfo.InvariantCulture);
								}
								if(!string.IsNullOrEmpty (vmd.dateCreate)){
									ve.DateCreate = DateTime.ParseExact (vmd.dateCreate, "yyyy/MM/dd",
										System.Globalization.CultureInfo.InvariantCulture);
								}
								ve.ReviewerId = revisor.Id;
								if(vmd.status!=null){
									ve.StatusVerificationId = vmd.status.id??0;
								}
								db.Insert(ve);
								if(vmd.sourceVerifications!=null  && vmd.sourceVerifications.Count>0){
									foreach(TabletSourceVerificationDto tsv in vmd.sourceVerifications){
										SourceVerification sv = new SourceVerification();
										sv.Address = tsv.address;
										sv.Age = tsv.age??0;
										sv.CaseRequestId = cs.Id;
										if(!string.IsNullOrEmpty (tsv.dateAuthorized)){
											sv.DateAuthorized = DateTime.ParseExact (tsv.dateAuthorized, "yyyy/MM/dd",
												System.Globalization.CultureInfo.InvariantCulture);
										}
										if(!string.IsNullOrEmpty (tsv.dateComplete)){
											sv.DateComplete = DateTime.ParseExact (tsv.dateComplete, "yyyy/MM/dd",
												System.Globalization.CultureInfo.InvariantCulture);
										}
										sv.FullName = tsv.fullName;
										sv.IsAuthorized = tsv.isAuthorized??false;
										sv.Phone = tsv.phone;
										if(tsv.relationship!=null){
											sv.RelationshipId = tsv.relationship.id??0;
										}
										sv.Specification = tsv.specification;
										sv.VerificationId = ve.Id;
										if(tsv.verificationMethod != null){
											sv.VerificationMethodId = tsv.verificationMethod.id;
										}
										sv.Visible = tsv.visible??false;
										sv.webId = tsv.webId;
										db.Insert(sv);
										if(tsv.fieldMeetingSourceList!=null && tsv.fieldMeetingSourceList.Count>0){
											foreach(TabletFieldMeetingSourceDto tfms in tsv.fieldMeetingSourceList){
												FieldMeetingSource fms = new FieldMeetingSource();
												if(tfms.fieldVerification!=null){
													fms.FieldVerificationId = tfms.fieldVerification.id;
												}
												fms.IdFieldList = tfms.idFieldList;
												fms.IsFinal = tfms.isFinal;
												fms.JsonValue = tfms.jsonValue;
												fms.Reason = tfms.reason;
												fms.SourceVerificationId = sv.Id;
												if(tfms.statusFieldVerification!=null){
													fms.StatusFieldVerificationId = tfms.statusFieldVerification.id;
												}
												fms.Value = tfms.value;
												db.Insert(fms);
											}
										}//end de list of fieldmeetingsources o FMS(s)

									}// end for each source
								}//end hay sources
							}//end verifiaction insert

							exitos++;
						}

					}catch(Exception e){
						db.Rollback ();
						Console.WriteLine ("exception in downloadVerificacion() getAssignedCaseByAssignmentId");
						Console.WriteLine("Exception message :::>"+e.Message);
						return new Java.Lang.String ("{\"error\":true, \"response\":\"Conexion fallida intente nuevamente.\"}");
					}finally{
						db.Commit ();
					}
				}

				return new Java.Lang.String ("{\"error\":false, \"response\":\"fin de descarga se descargaron "+exitos+" casos\"}");
			} else {
				return new Java.Lang.String ("{\"error\":true, \"response\":\"No se encontro ningun usuario asociado\"}");
			}

		}




	}//class end


}