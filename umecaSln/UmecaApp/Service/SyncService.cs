using System;

//insert with children etc

//query to list
using System.Linq;
//listas
using System.Collections.Generic;

using Java.Interop;
using Newtonsoft.Json;
using Android.Content;

//cript
using BCrypt;

using SQLite;
using Umeca.Data;
using UmecaApp.UmecaWebService;


namespace UmecaApp
{
	public class SyncService  : Java.Lang.Object
	{

		readonly CatalogServiceController services;


		Context context;

		public SyncService(Context context)
		{
			this.context = context;
			services = new CatalogServiceController ();
		}

		public SyncService ()
		{
			services = new CatalogServiceController ();
		}

		[Export("userUpsert")]
		public Java.Lang.String userUpsert(Java.Lang.String user,Java.Lang.String pass){
			using (var db = FactoryConn.GetConn ()) {
				UmecaWebService.UmecaWS uwsl;

				var content = db.Table<StatusCase> ().ToList();
				foreach (StatusCase m in content) {
					Console.WriteLine ("Id: "+m.Id+" Name:"+m.Name);
				}

				var everything = db.Table<Configuracion>().FirstOrDefault();
				try{
				if (everything != null && !String.IsNullOrEmpty (everything.url)) {
					uwsl = new UmecaApp.UmecaWebService.UmecaWS (everything.url);
				} else {
					uwsl = new UmecaApp.UmecaWebService.UmecaWS ();
				}
				}catch(Exception e){
					return new Java.Lang.String ("{\"error\":true, \"response\":\""+e.Message+"\"}");
				}

				var ecodedPass = Crypto.HashPassword (pass.ToString());
				var usuario = user.ToString ();
				var extraCode = "";
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
					extraCode = "Fallo en la conexion con el servicio: "+e.Message;
	//				return new Java.Lang.String ("{\"error\":true, \"response\":\"Fallo en la conexion con el servicio revise su conexion e intente nuevamente\"}");
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
						
						return new Java.Lang.String ("{\"error\":true, \"response\":\"El usuario y/o password son incorrectos. Favor de verificar los datos e intente nuevamente. \\n"+extraCode+"  \"}");
					}
				} else {
					return new Java.Lang.String ("{\"error\":true, \"response\":\"No se encontro ningun usuario asociado. \\n"+extraCode+"  \"}");
				}
			}
		}

		[Export("updateAplicationUrl")]
		public Java.Lang.String updateAplicationUrl(Java.Lang.String newUrl){
			using (var db = FactoryConn.GetConn ()) {
				var response = "false";
				try {
					db.BeginTransaction ();
					var contexto = newUrl.ToString ();
					db.CreateTable<Configuracion> ();
					var everything = db.Table<Configuracion> ().ToList ();
					foreach(var m in everything){
						db.Delete (m);
					}
					var nuevaRuta = Constants.UMECA_SERVICE_PROTOCOL + contexto + Constants.UMECA_SERVICE_END_POINT;
					var config = new Configuracion ();
					config.Description = "cambio de url para web service";
					config.url = nuevaRuta;
					db.Insert (config);
				} catch (Exception e) {
					db.Rollback ();
					Console.WriteLine ("exception in updateAplicationUrl()");
					Console.WriteLine ("Exception message :::>" + e.Message);
					response = "true";
				} finally {
					db.Commit ();
				}
				db.Close ();
				return new Java.Lang.String (response);
			}
		}



		[Export("downloadVerificacion")]
		public Java.Lang.String downloadVerificacion(Java.Lang.String pass){
			using (var db = FactoryConn.GetConn ()) {
				String guid = "";
				User revisor = new User ();
				UmecaWebService.UmecaWS uwsl = new UmecaApp.UmecaWebService.UmecaWS ();
				var everything = db.Table<Configuracion> ().FirstOrDefault ();
				if (everything != null && !String.IsNullOrEmpty (everything.url)) {
					uwsl.Url = everything.url;
				}
				var ecodedPass = Crypto.HashPassword (pass.ToString ());
				db.CreateTable<User> ();
				var ServiceResult = new ResponseMessage ();
				ServiceResult.hasError = false;
				var usrList = db.Table<User> ().ToList ();
				if (usrList != null && usrList.Count > 0) {
					//login tablet
					try {
						var savedUsr = usrList [0];
						var respuesta = uwsl.loginFromTablet (savedUsr.username, ecodedPass);
						if (respuesta.hasError) {
							return new Java.Lang.String ("{\"hasError\":true, \"message\":\"" + respuesta.message + "\"}");
						} else {
							var usuarios = db.Table<User> ().ToList ();
							foreach (User u in usuarios) {
								db.Delete (u);
							}
							User asociado = new User ();
							var dataString = (System.Xml.XmlNode[])respuesta.returnData;
							var getData = JsonConvert.DeserializeObject<TabletUserDto> (dataString [0].Value.ToString ());
							var roleUsr = db.Table<Role> ().Where (rl => rl.role == getData.roleCode).FirstOrDefault ();
							asociado.fullname = getData.fullname;
							asociado.roles = roleUsr.Id;
							asociado.Id = getData.id ?? 0;
							asociado.username = savedUsr.username;
							asociado.password = ecodedPass;
							db.Insert (asociado);
							revisor = asociado;
							guid = getData.guid;
						}
					} catch (Exception e) {
						db.Rollback ();
						Console.WriteLine ("exception in downloadVerificacion() login");
						Console.WriteLine ("Exception message :::>" + e.Message);
						return new Java.Lang.String ("{\"hasError\":true, \"message\":\"Conexion fallida intente nuevamente.\"}");
					} finally {
						db.Commit ();
					}
					//obtencion de asignaciones
					List<TabletAssignmentInfo> listAsignados = new List<TabletAssignmentInfo> ();
					try {
						var asigmentsResponse = uwsl.getAssignmentsByUser (revisor.username, guid);
						if (asigmentsResponse.hasError) {
							return new Java.Lang.String ("{\"hasError\":true, \"message\":\"" + asigmentsResponse.message + "\"}");
						} else {
							var dataString = (System.Xml.XmlNode[])asigmentsResponse.returnData;
							var getData = JsonConvert.DeserializeObject<List<TabletAssignmentInfo>> (dataString [0].Value.ToString ());
							if (getData != null && getData.Count > 0) {
								listAsignados = getData;
							}
						}
					} catch (Exception e) {
						Console.WriteLine ("exception in downloadVerificacion() asigments");
						Console.WriteLine ("Exception message :::>" + e.Message);
						return new Java.Lang.String ("{\"hasError\":true, \"message\":\"Conexion fallida intente nuevamente.\"}");
					}

					//obtencion de cada asignacion de la lista obtenida
					int exitos = 0;
					foreach (TabletAssignmentInfo tbltAi in listAsignados) {
						try {
							db.BeginTransaction ();
							var caseAsignResponse = uwsl.getAssignedCaseByAssignmentId (revisor.username, guid, tbltAi.id, true);
							if (caseAsignResponse.hasError) {
								ServiceResult.message += "El Caso " + tbltAi.id + " no se pudo descargar: " + caseAsignResponse.message + " \n";
								ServiceResult.hasError = true;
							} else {
								var dataString1 = (System.Xml.XmlNode[])caseAsignResponse.returnData;
								var getData1 = JsonConvert.DeserializeObject<TabletCaseDto> (dataString1 [0].Value.ToString ());
								Imputed imp = new Imputed ();
								Meeting me = new Meeting ();
								Case cs = new Case ();
								Verification ve = new Verification ();
								cs = getData1.CaseToObject ();
								var already = db.Table<Case> ().Where (alr => alr.webId == cs.webId && alr.webId != null).ToList ();
								foreach (Case cais in already) {
									caseDeleteCascade (cais.Id, revisor.Id);
								}

								StatusCase stcase = db.Table<StatusCase> ().Where (stc => stc.Name == getData1.previousStateCode).FirstOrDefault ();
								if (stcase != null && stcase.Id != 0) {
									cs.StatusCaseId = stcase.Id;
								}
								cs.tac = tbltAi.id;
								//se salva al caso
								var anterior = db.Table<Case> ().Where (cas => cas.webId == cs.webId).FirstOrDefault ();
								if (anterior != null) {
									cs.Id = anterior.Id;
								} else {
									db.Insert (cs);
								}
								//hearingFormats
								if (getData1.hearingFormats != null && getData1.hearingFormats.Count > 0) {
									foreach (TabletHearingFormatDto hfDto in getData1.hearingFormats) {
										HearingFormat Formato = new HearingFormat ();

										//se inserta el hearing imputed con su address
										if (hfDto.hearingImputed != null) {
											var himp = hfDto.hearingImputed;
											HearingFormatImputed hfimp = new HearingFormatImputed ();
											if (!string.IsNullOrEmpty (himp.birthDate)) {
												hfimp.BirthDate = DateTime.ParseExact (himp.birthDate, "yyyy/MM/dd",
													System.Globalization.CultureInfo.InvariantCulture);
											}
											hfimp.ImputeTel = himp.imputeTel;
											hfimp.LastNameM = himp.lastNameM;
											hfimp.LastNameP = himp.lastNameP;
											hfimp.Name = himp.name;
											if (hfDto.hearingImputed.address != null) {
												var adrs = hfDto.hearingImputed.address;
												Address ad = new Address ();
												ad.addressString = adrs.addressString;
												ad.InnNum = adrs.innNum;
												ad.Lat = adrs.lat;
												ad.Lng = adrs.lng;
												if (adrs.location != null) {
													ad.LocationId = adrs.location.id;
												}
												ad.OutNum = adrs.outNum;
												ad.Street = adrs.street;
												db.Insert (ad);
												hfimp.Address = ad.Id;
											}
											db.Insert (hfimp);
											Formato.hearingImputed = hfimp.Id;
										}
										//se agregan los specs del formato
										if (hfDto.hearingFormatSpecs != null) {
											var spec = hfDto.hearingFormatSpecs;
											HearingFormatSpecs hfs = new HearingFormatSpecs ();
											hfs.ArrangementType = spec.arrangementType;
											hfs.ControlDetention = spec.controlDetention;
											if (!string.IsNullOrEmpty (spec.extDate)) {
												hfs.ExtDate = DateTime.ParseExact (spec.extDate, "yyyy/MM/dd",
													System.Globalization.CultureInfo.InvariantCulture);
											}
											hfs.Extension = spec.extension;
											if (!string.IsNullOrEmpty (spec.imputationDate)) {
												hfs.ImputationDate = DateTime.ParseExact (spec.imputationDate, "yyyy/MM/dd",
													System.Globalization.CultureInfo.InvariantCulture);
											}
											hfs.ImputationFormulation = spec.imputationFormulation;
											if (!string.IsNullOrEmpty (spec.linkageDate)) {
												hfs.LinkageDate = DateTime.ParseExact (spec.linkageDate, "yyyy/MM/dd",
													System.Globalization.CultureInfo.InvariantCulture);
											} else {
												hfs.LinkageDate = null;
											}
											hfs.LinkageProcess = spec.linkageProcess;
											hfs.LinkageRoom = spec.linkageRoom;
											if (!string.IsNullOrEmpty (spec.linkageTime)) {
												hfs.LinkageTime = DateTime.ParseExact (spec.linkageTime, "HH:mm:ss",
													System.Globalization.CultureInfo.InvariantCulture);
											} else {
												hfs.LinkageTime = null;
											}
											hfs.NationalArrangement = spec.nationalArrangement;
											db.Insert (hfs);
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

										Formato.District = hfDto.district;
										Formato.IsHomeless = hfDto.isHomeless;
										Formato.LocationPlace = hfDto.locationPlace;
										Formato.TimeAgo = hfDto.timeAgo;

										if (!string.IsNullOrEmpty (hfDto.endTime)) {
											Formato.EndTime = DateTime.ParseExact (hfDto.endTime, "HH:mm:ss",
												System.Globalization.CultureInfo.InvariantCulture);
										}
										Formato.HearingResult = hfDto.hearingResult;
										if (hfDto.hearingType != null) {
											Formato.HearingType = hfDto.hearingType.id;
										}
										Formato.HearingTypeSpecification = hfDto.hearingTypeSpecification;
										Formato.IdFolder = hfDto.idFolder;
										Formato.IdJudicial = hfDto.idJudicial;
										Formato.ImputedPresence = hfDto.imputedPresence;
										if (!string.IsNullOrEmpty (hfDto.initTime)) {
											Formato.InitTime = DateTime.ParseExact (hfDto.initTime, "HH:mm:ss",
												System.Globalization.CultureInfo.InvariantCulture);
										}
										Formato.IsFinished = hfDto.isFinished ?? false;
										Formato.JudgeName = hfDto.judgeName;
										Formato.MpName = hfDto.mpName;
										Formato.PreviousHearing = hfDto.previousHearing;
										if (!string.IsNullOrEmpty (hfDto.registerTime)) {
											Formato.RegisterTime = DateTime.ParseExact (hfDto.registerTime, "yyyy/MM/dd HH:mm:ss",
												System.Globalization.CultureInfo.InvariantCulture);
										}else{
											Formato.RegisterTime = DateTime.Now;
										}
										Formato.Room = hfDto.room;
										Formato.ShowNotification = hfDto.showNotification ?? true;
										Formato.Supervisor = revisor.Id;
										Formato.Terms = hfDto.terms;
										if (!string.IsNullOrEmpty (hfDto.umecaDate)) {
											Formato.UmecaDate = DateTime.ParseExact (hfDto.umecaDate, "yyyy/MM/dd",
												System.Globalization.CultureInfo.InvariantCulture);
										}
										Formato.UmecaSupervisor = revisor.Id;
										if (!string.IsNullOrEmpty (hfDto.umecaTime)) {
											Formato.UmecaTime = DateTime.ParseExact (hfDto.umecaTime, "HH:mm:ss",
												System.Globalization.CultureInfo.InvariantCulture);
										}
										db.Insert (Formato);
										if (hfDto.assignedArrangements != null && hfDto.assignedArrangements.Count > 0) {
											foreach (TabletAssignedArrangementDto asArr in hfDto.assignedArrangements) {
												AssignedArrangement aa = new AssignedArrangement ();
												if (asArr.arrangement != null) {
													aa.Arrangement = asArr.arrangement.id;
												}
												aa.Description = asArr.description;
												aa.HearingFormat = Formato.Id;
												db.Insert (aa);
											}
										}

										if (hfDto.contacts != null && hfDto.contacts.Count > 0) {
											foreach (TabletContactDataDto contc in hfDto.contacts) {
												ContactData cnt = new ContactData ();
												cnt.	AddressTxt = contc.addressTxt;
												cnt.NameTxt = contc.nameTxt;
												cnt.PhoneTxt = contc.phoneTxt;
												cnt.HearingFormat = Formato.Id;
												cnt.liveWith = contc.liveWith;
												db.Insert (cnt);
											}
										}

										if (hfDto.crimeList != null && hfDto.crimeList.Count > 0) {
											foreach (TabletCrimeDto crimen in hfDto.crimeList) {
												Crime cri = new Crime ();
												cri.Article = crimen.article;
												cri.Comment = crimen.comment;
												if (crimen.federal != null) {
													cri.Federal = crimen.federal.id;
												}
												if (crimen.crime != null) {
													cri.IdCrimeCat = crimen.crime.id;
												}
												cri.HearingFormat = Formato.Id;
												db.Insert (cri);
											}
										}

										//									hfDto.crimeList;
										HearingFormat hfn = new HearingFormat ();

										hfn.CaseDetention = cs.Id;
									}//end foreach formats
								}//end de hearingFormats


								if (getData1.meeting != null) {
									TabletMeetingDto tme = getData1.meeting;
									me = tme.MeetingToObject ();
//									if (tme.reviewer != null && tme.reviewer.id != 0) {
										me.ReviewerId = revisor.Id;
//									}
									me.CaseDetentionId = cs.Id;
									db.Insert (me);

									if (getData1.meeting != null && getData1.meeting.imputed != null) {
										TabletImputedDto tid = new TabletImputedDto ();
										tid = getData1.meeting.imputed;
										imp = tid.ImputedDtoToObject ();
										imp.MeetingId = me.Id;
										db.Insert (imp);
									}//end de tiene imputado

									if (tme.drugs != null && tme.drugs.Count > 0) {
										foreach (TabletDrugDto tdrug in tme.drugs) {
											Drug d = new Drug ();
											d.block = tdrug.block;
											if (tdrug.drugType != null) {
												d.DrugTypeId = tdrug.drugType.id;
											}
											if (!string.IsNullOrEmpty (tdrug.lastUse)) {
												d.LastUse = DateTime.ParseExact (tdrug.lastUse, "yyyy/MM/dd",
													System.Globalization.CultureInfo.InvariantCulture);
											}
											d.MeetingId = me.Id;
											d.OnsetAge = tdrug.onsetAge;
											if (tdrug.periodicity != null) {
												d.PeriodicityId = tdrug.periodicity.id;
											}
											d.Quantity = tdrug.quantity;
											d.Specification = tdrug.specificationType;
											d.SpecificationPeriodicity = tdrug.specificationPeriodicity;
											d.webId = tdrug.webId;
											db.Insert (d);
										}
									}// end of drugs

									if (tme.imputedHomes != null && tme.imputedHomes.Count > 0) {
										foreach (TabletImputedHomeDto thome in tme.imputedHomes) {
											ImputedHome ih = new ImputedHome ();
											if (thome.address != null) {
												ih.AddressId = thome.address.id;

												String result = "";
												if (!String.IsNullOrEmpty (thome.address.street)) {
													result = "Calle: " + thome.address.street + " No Ext: " + thome.address.outNum;
												}
												if (!String.IsNullOrEmpty (thome.address.innNum)) {
													result = result + " No Int:" + thome.address.innNum;
												}
												if (thome.address.location != null && thome.address.location.id != null ) {
													var local = db.Table<Location>().Where(loc=>loc.Id == thome.address.location.id).FirstOrDefault();
													var municipio = db.Table<Municipality>().Where(mun=>mun.Id == local.MunicipalityId).FirstOrDefault();
													var estad = db.Table<State>().Where(stt=>stt.Id == municipio.StateId).FirstOrDefault();
													result = result + "," + local.Name + ". CP: " + local.ZipCode + ". " + municipio.Name + ", " + estad.Name + ".";
												}
												ih.addressString = result;
											}
											ih.Description = thome.description;
											if (thome.homeType != null) {
												ih.HomeTypeId = thome.homeType.id;
												ih.InnNum = thome.address.innNum;
												ih.Lat = thome.address.lat;
												ih.Lng = thome.address.lng;
												if (thome.address.location != null) {
													ih.LocationId = thome.address.location.id;
												}
												ih.OutNum = thome.address.outNum;
												ih.Street = thome.address.street;
											}
											ih.Phone = thome.phone;
											ih.ReasonChange = thome.reasonChange;
											ih.ReasonSecondary = thome.reasonSecondary;
											ih.ReasonChange = thome.reasonChange;
											if (thome.registerType != null) {
												ih.RegisterTypeId = thome.registerType.id;
											}
											ih.Specification = thome.specification;
											ih.TimeLive = thome.timeLive;
											ih.webId = thome.webId;
											ih.MeetingId = me.Id;
											ih.IsHomeless = thome.isHomeless;
											db.Insert (ih);
											if (thome.schedule != null && thome.schedule.Count > 0) {
												foreach (TabletScheduleDto sh in thome.schedule) {
													Schedule sch = new Schedule ();
													sch.Day = sh.day;
													sch.End = sh.end;
													sch.Start = sh.start;
													sch.webId = sh.webId;
													sch.ImputedHomeId = ih.Id;
													db.Insert (sch);
												}
											}
										}
									}// end of imputed home

									if (tme.jobs != null && tme.jobs.Count > 0) {
										foreach (TabletJobDto tjob in tme.jobs) {
											Job j = new Job ();
											j.Address = tjob.address;
											j.block = tjob.block;
											j.Company = tjob.company;
											if (!string.IsNullOrEmpty (tjob.end)) {
												j.End = DateTime.ParseExact (tjob.end, "yyyy/MM/dd",
													System.Globalization.CultureInfo.InvariantCulture);
											}
											j.MeetingId = me.Id;
											j.NameHead = tjob.nameHead;
											j.Phone = tjob.phone;
											j.Post = tjob.post;
											j.ReasonChange = tjob.reasonChange;
											if (tjob.registerType != null) {
												j.RegisterTypeId = tjob.registerType.id;
											}
											j.SalaryWeek = tjob.salaryWeek;

											if (!string.IsNullOrEmpty (tjob.start)) {
												j.Start = DateTime.ParseExact (tjob.start, "yyyy/MM/dd",
													System.Globalization.CultureInfo.InvariantCulture);
											}
											if (!string.IsNullOrEmpty (tjob.startPrev)) {
												j.StartPrev = DateTime.ParseExact (tjob.startPrev, "yyyy/MM/dd",
													System.Globalization.CultureInfo.InvariantCulture);
											}
											j.webId = tjob.webId;
											db.Insert (j);
											if (tjob.schedule != null && tjob.schedule.Count > 0) {
												foreach (TabletScheduleDto tsch in tjob.schedule) {
													Schedule jobschedl = new Schedule ();
													jobschedl.Day = tsch.day;
													jobschedl.End = tsch.end;
													jobschedl.Start = tsch.start;
													jobschedl.webId = tsch.webId;
													jobschedl.JobId = j.Id;
													db.Insert (jobschedl);
												}
											}
										}
									}// end og jobs

									if (tme.leaveCountry != null) {
										LeaveCountry lc = new LeaveCountry ();
										lc.Address = tme.leaveCountry.address;
										if (tme.leaveCountry.communicationFamily != null) {
											lc.CommunicationFamilyId = tme.leaveCountry.communicationFamily.id;
										}
										if (tme.leaveCountry.country != null) {
											lc.CountryId = tme.leaveCountry.country.id;
										}
										if (tme.leaveCountry.familyAnotherCountry != null) {
											lc.FamilyAnotherCountryId = tme.leaveCountry.familyAnotherCountry.id;
										}
										if (tme.leaveCountry.immigrationDocument != null) {
											lc.ImmigrationDocumentId = tme.leaveCountry.familyAnotherCountry.id;
										}
										if (tme.leaveCountry.livedCountry != null) {
											lc.LivedCountryId = tme.leaveCountry.livedCountry.id;
										}
										lc.Media = tme.leaveCountry.media;
										lc.MeetingId = me.Id;
										if (tme.leaveCountry.officialDocumentation != null) {
											lc.OfficialDocumentationId = tme.leaveCountry.officialDocumentation.id;
										}
										lc.Reason = tme.leaveCountry.reason;
										if (tme.leaveCountry.relationship != null) {
											lc.RelationshipId = tme.leaveCountry.relationship.id;
										}
										lc.SpecficationImmigranDoc = tme.leaveCountry.specficationImmigranDoc;
										lc.SpecificationRelationship = tme.leaveCountry.specificationRelationship;
										lc.State = tme.leaveCountry.state;
										lc.timeAgo = tme.leaveCountry.timeAgo;
										lc.TimeResidence = tme.leaveCountry.timeResidence;
										lc.webId = tme.leaveCountry.webId;
										db.Insert (lc);
									}//end leave country

									if (tme.references != null && tme.references.Count > 0) {
										foreach (TabletReferenceDto tref in tme.references) {
											Reference nref = new Reference ();
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
											if (tref.documentType != null) {
												nref.DocumentTypeId = tref.documentType.id ?? 0;
											}
											if (tref.relationship != null) {
												nref.RelationshipId = tref.relationship.id ?? 0;
											}
											db.Insert (nref);
										}
									}//end references

									if (tme.school != null) {
										School nskul = new School ();
										nskul.Address = tme.school.address;
										nskul.block = tme.school.block;
										if (tme.school.degree != null) {
											nskul.DegreeId = tme.school.degree.id;
										}
										nskul.MeetingId = me.Id;
										nskul.Name = tme.school.name;
										nskul.Phone = tme.school.phone;
										nskul.Specification = tme.school.specification ?? "";
										nskul.webId = tme.webId;
										db.Insert (nskul);
										if (tme.school.schedule != null && tme.school.schedule.Count > 0) {
											foreach (TabletScheduleDto tsch in tme.school.schedule) {
												Schedule escuelaSched = new Schedule ();
												escuelaSched.Day = tsch.day;
												escuelaSched.End = tsch.end;
												escuelaSched.Start = tsch.start;
												escuelaSched.webId = tsch.webId;
												escuelaSched.SchoolId = nskul.Id;
												db.Insert (escuelaSched);
											}
										}
									}//end school

									if (tme.socialEnvironment != null) {
										SocialEnvironment nse = new SocialEnvironment ();
										nse.comment = tme.socialEnvironment.comment;
										nse.MeetingId = me.Id;
										nse.physicalCondition = tme.socialEnvironment.physicalCondition;
										db.Insert (nse);
										if (tme.socialEnvironment.relSocialEnvironmentActivities != null && tme.socialEnvironment.relSocialEnvironmentActivities.Count > 0) {
											foreach (TabletRelSocialEnvironmentActivityDto rel in tme.socialEnvironment.relSocialEnvironmentActivities) {
												RelActivity nrel = new RelActivity ();
												if (rel.activity != null) {
													nrel.ActivityId = rel.activity.id;
												}
												nrel.SocialEnvironmentId = nse.Id;
												nrel.specification = rel.specification;
												db.Insert (nrel);
											}
										}

									}//end social environment

									if (tme.socialNetwork != null) {
										SocialNetwork nsoc = new SocialNetwork ();
										nsoc.Comment = tme.socialNetwork.comment;
										nsoc.MeetingId = me.Id;
										nsoc.webId = tme.socialNetwork.webId;
										db.Insert (nsoc);
										if (tme.socialNetwork.peopleSocialNetwork != null && tme.socialNetwork.peopleSocialNetwork.Count > 0) {
											foreach (TabletPersonSocialNetworkDto tpsn in tme.socialNetwork.peopleSocialNetwork) {
												PersonSocialNetwork psn = new PersonSocialNetwork ();
												psn.Address = tpsn.address;
												psn.Age = tpsn.age;
												psn.block = tpsn.block;
												if (tpsn.dependent != null) {
													psn.DependentId = tpsn.dependent.id ?? 0;
												}
												if (tpsn.documentType != null) {
													psn.DocumentTypeId = tpsn.documentType.id ?? 0;
												}
												psn.isAccompaniment = tpsn.isAccompaniment;
												if (tpsn.livingWith != null) {
													psn.LivingWithIde = tpsn.livingWith.id ?? 0;
												}
												psn.Name = tpsn.name;
												psn.Phone = tpsn.phone;
												if (tpsn.relationship != null) {
													psn.RelationshipId = tpsn.relationship.id ?? 0;
												}
												psn.SocialNetworkId = nsoc.Id;
												psn.SpecificationDocumentType = tpsn.specification;
												psn.specificationRelationship = tpsn.specificationRelationship;
												psn.webId = tpsn.webId;
												db.Insert (psn);
											}
										}
									}//end school


								}// end de meeting

								//se inserta la verificacion
								if (getData1.verification != null) {
									TabletVerificationDto vmd = getData1.verification;
									ve.CaseDetentionId = cs.Id;
									if (!string.IsNullOrEmpty (vmd.dateComplete)) {
										ve.DateComplete = DateTime.ParseExact (vmd.dateComplete, "yyyy/MM/dd",
											System.Globalization.CultureInfo.InvariantCulture);
									}
									if (!string.IsNullOrEmpty (vmd.dateCreate)) {
										ve.DateCreate = DateTime.ParseExact (vmd.dateCreate, "yyyy/MM/dd",
											System.Globalization.CultureInfo.InvariantCulture);
									}
									ve.ReviewerId = revisor.Id;
									if (vmd.status != null) {
										ve.StatusVerificationId = vmd.status.id ?? 0;
									}
									db.Insert (ve);
									if (vmd.sourceVerifications != null && vmd.sourceVerifications.Count > 0) {
										foreach (TabletSourceVerificationDto tsv in vmd.sourceVerifications) {
											SourceVerification sv = new SourceVerification ();
											sv.Address = tsv.address;
											sv.Age = tsv.age ?? 0;
											sv.CaseRequestId = cs.Id;
											if (!string.IsNullOrEmpty (tsv.dateAuthorized)) {
												sv.DateAuthorized = DateTime.ParseExact (tsv.dateAuthorized, "yyyy/MM/dd",
													System.Globalization.CultureInfo.InvariantCulture);
											}
											if (!string.IsNullOrEmpty (tsv.dateComplete)) {
												sv.DateComplete = DateTime.ParseExact (tsv.dateComplete, "yyyy/MM/dd",
													System.Globalization.CultureInfo.InvariantCulture);
											}
											sv.FullName = tsv.fullName;
											sv.IsAuthorized = tsv.isAuthorized ?? false;
											sv.Phone = tsv.phone;
											if (tsv.relationship != null) {
												sv.RelationshipId = tsv.relationship.id ?? 0;
											}
											sv.Specification = tsv.specification;
											sv.VerificationId = ve.Id;
											if (tsv.verificationMethod != null) {
												sv.VerificationMethodId = tsv.verificationMethod.id;
											}
											sv.Visible = tsv.visible ?? false;
											sv.webId = tsv.webId;
											db.Insert (sv);
											if (tsv.fieldMeetingSourceList != null && tsv.fieldMeetingSourceList.Count > 0) {
												foreach (TabletFieldMeetingSourceDto tfms in tsv.fieldMeetingSourceList) {
													FieldMeetingSource fms = new FieldMeetingSource ();
													if (tfms.fieldVerification != null) {
														fms.FieldVerificationId = tfms.fieldVerification.id;
													}
													fms.IdFieldList = tfms.idFieldList;
													fms.IsFinal = tfms.isFinal;
													fms.JsonValue = tfms.jsonValue ?? "";
													fms.Reason = tfms.reason;
													fms.SourceVerificationId = sv.Id;
													if (tfms.statusFieldVerification != null) {
														fms.StatusFieldVerificationId = tfms.statusFieldVerification.id;
													}
													fms.Value = tfms.value;
													db.Insert (fms);
												}
											}//end de list of fieldmeetingsources o FMS(s)

										}// end for each source
									}//end hay sources
								}//end verifiaction insert


								var confirm = uwsl.confirmReceivedAssignment (revisor.username, guid, tbltAi.id, true);
								if (confirm.hasError) {
									db.Rollback ();
									ServiceResult.message += "El Caso " + tbltAi.id + " no se pudo descargar: '" + caseAsignResponse.message + "' \n";
									ServiceResult.hasError = true;
								} else {
									exitos++;
								}

							} // end has no error 

						} catch (Exception e) { // end of tyr
							db.Rollback ();
							Console.WriteLine ("exception in downloadVerificacion() getAssignedCaseByAssignmentId");
							Console.WriteLine ("Exception message :::>" + e.Message);
							ServiceResult.message += "El Caso " + tbltAi.id + " no se pudo descargar: 'Conexion terminada intente nuevamente.' \n";
							ServiceResult.hasError = true;
						} finally {
							db.Commit ();
						}
					} // end for each get asigned case
					ServiceResult.message += "Se descargaron " + exitos + " casos \n";
					return new Java.Lang.String (JsonConvert.SerializeObject (ServiceResult));
				} else {
					return new Java.Lang.String ("{\"hasError\":true, \"message\":\"No se encontro ningun usuario asociado\"}");
				}
			}
		}

		static TabletImputedHomeDto NewMethod (TabletImputedHomeDto nhome)
		{
			return nhome;
		}

		[Export("sincrinozeCase")]
		public Java.Lang.String sincronizeCase(Java.Lang.String listCases, Java.Lang.String pass, Java.Lang.String Method){
			using (var db = FactoryConn.GetConn ()) {
				String guid = "";
				User revisor = new User ();
				UmecaWebService.UmecaWS uwsl = new UmecaApp.UmecaWebService.UmecaWS ();
				var everything = db.Table<Configuracion> ().FirstOrDefault ();
				if (everything != null && !String.IsNullOrEmpty (everything.url)) {
					uwsl.Url = everything.url;
				}
				var ecodedPass = Crypto.HashPassword (pass.ToString ());
				db.CreateTable<User> ();
				var usrList = db.Table<User> ().ToList ();
				if (usrList != null && usrList.Count > 0) {
					//login tablet
					try {
						var savedUsr = usrList [0];
						var respuesta = uwsl.loginFromTablet (savedUsr.username, ecodedPass);
						if (respuesta.hasError) {
							return new Java.Lang.String ("{\"error\":true, \"response\":\"" + respuesta.message + "\"}");
						} else {
							var usuarios = db.Table<User> ().ToList ();
							foreach (User u in usuarios) {
								db.Delete (u);
							}
							User asociado = new User ();
							var dataString = (System.Xml.XmlNode[])respuesta.returnData;
							var getData = JsonConvert.DeserializeObject<TabletUserDto> (dataString [0].Value.ToString ());
							var roleUsr = db.Table<Role> ().Where (rl => rl.role == getData.roleCode).FirstOrDefault ();
							asociado.fullname = getData.fullname;
							asociado.roles = roleUsr.Id;
							asociado.Id = getData.id ?? 0;
							asociado.username = savedUsr.username;
							asociado.password = ecodedPass;
							db.Insert (asociado);
							revisor = asociado;
							guid = getData.guid;
						}
					} catch (Exception e) {
						db.Rollback ();
						Console.WriteLine ("exception in downloadVerificacion() login");
						Console.WriteLine ("Exception message :::>" + e.Message);
						return new Java.Lang.String ("{\"error\":true, \"response\":\"Conexion fallida intente nuevamente.\"}");
					} finally {
						db.Commit ();
					}
					//obtencion de asignaciones
					var listSynchro = new List<int> ();
					try {
						listSynchro = JsonConvert.DeserializeObject<List<int>> (listCases.ToString ());
					} catch (Exception e) {
						Console.WriteLine ("exception in downloadVerificacion() asigments");
						Console.WriteLine ("Exception message :::>" + e.Message);
						return new Java.Lang.String ("{\"error\":true, \"response\":\"error al crear los objetos\"}");
					}
					db.BeginTransaction ();
					foreach (int i in listSynchro) {
						Case cs = db.Table<Case> ().Where (caso => caso.Id == i).FirstOrDefault ();
						if (cs != null) {
							TabletCaseDto caseSync = new TabletCaseDto ();
							caseSync.dateCreate = String.Format ("{0:yyyy/MM/dd}", cs.DateCreate);
							if (cs.DateObsolete != null) {
								caseSync.dateObsolete = String.Format ("{0:yyyy/MM/dd}", cs.DateObsolete);
							}
							if (cs.DateNotProsecute != null) {
								caseSync.dateNotProsecute = String.Format ("{0:yyyy/MM/dd}", cs.DateNotProsecute);
							}
							caseSync.id = cs.Id;
							caseSync.idFolder = cs.IdFolder;
							caseSync.idMP = cs.IdMP;
							caseSync.recidivist = cs.Recidivist;
							caseSync.webId = cs.webId;
							caseSync.hasNegation = cs.HasNegation;
							caseSync.isSubstracted = cs.IsSubstracted;
							 
							if (cs.DateSubstracted != null) {
								caseSync.dateSubstracted = String.Format ("{0:yyyy/MM/dd}", cs.DateSubstracted);
							}

							var stCase = db.Table<StatusCase> ().Where (stcase => stcase.Id == cs.StatusCaseId).FirstOrDefault ();
							if (stCase != null) {
								var dtoStCase = new TabletStatusCaseDto ();
								dtoStCase.id = stCase.Id;
								dtoStCase.description = stCase.Description;
								dtoStCase.name = stCase.Name;
								caseSync.status = dtoStCase;
							}

							if (String.IsNullOrEmpty (caseSync.previousStateCode)) {
								caseSync.previousStateCode = stCase.Name;
							}

							var verify = db.Table<Verification> ().Where (verf => verf.CaseDetentionId == cs.Id && verf.ReviewerId == revisor.Id).FirstOrDefault ();
							if (verify != null) {
								var verificacion = new TabletVerificationDto ();
								verificacion.dateComplete = String.Format ("{0:yyyy/MM/dd}", verify.DateComplete);
								verificacion.dateCreate = String.Format ("{0:yyyy/MM/dd}", verify.DateCreate);
								verificacion.id = verify.Id;
								var reviuer = new TabletUserDto ();
								var rlcode = db.Table<Role> ().Where (rlc => rlc.Id == revisor.roles).FirstOrDefault ();
								if (rlcode != null) {
									reviuer.roleCode = rlcode.role;
								}
								reviuer.fullname = revisor.fullname;
								reviuer.guid = guid;
								reviuer.hPassword = ecodedPass;
								reviuer.id = revisor.Id;
								verificacion.reviewer = reviuer;

								var stVerify = db.Table<StatusVerification> ().Where (stVer => stVer.Id == verify.StatusVerificationId).FirstOrDefault ();
								if (stVerify != null) {
									var dtostVerify = new TabletStatusVerificationDto ();
									dtostVerify.id = stVerify.Id;
									dtostVerify.description = stVerify.Description;
									dtostVerify.name = stVerify.Name;
									verificacion.status = dtostVerify;
								}

								var fuentes = db.Table<SourceVerification> ().Where (svr => svr.CaseRequestId == cs.Id && svr.VerificationId == verify.Id && svr.DateComplete != null && svr.Visible == true).ToList ();
								var dtoFuentes = new List<TabletSourceVerificationDto> ();
								if (fuentes != null && fuentes.Count > 0) {
									foreach (SourceVerification svt in fuentes) {
										var dtoSource = new TabletSourceVerificationDto ();
										dtoSource.address = svt.Address;
										dtoSource.age = svt.Age;
										dtoSource.dateAuthorized = String.Format ("{0:yyyy/MM/dd}", svt.DateAuthorized);
										dtoSource.dateComplete = String.Format ("{0:yyyy/MM/dd}", svt.DateComplete);
										dtoSource.fullName = svt.FullName;
										dtoSource.id = svt.Id;
										dtoSource.isAuthorized = svt.IsAuthorized;
										dtoSource.phone = svt.Phone;
										dtoSource.specification = svt.Specification;
										dtoSource.visible = svt.Visible;
										dtoSource.webId = svt.webId;

										if (svt != null && svt.RelationshipId > 0) {
											var inm = db.Table<Relationship> ().Where (tis => tis.Id == svt.RelationshipId).FirstOrDefault ();
											if (inm != null) {
												var nElect = new TabletRelationshipDto ();
												nElect.id = inm.Id;
												nElect.name = inm.Name;
												nElect.isObsolete = inm.IsObsolete;
												nElect.specification = inm.Specification;
												dtoSource.relationship = nElect;
											}
										}

										if (svt.VerificationMethodId != null && svt.VerificationMethodId != 0) {
											var inm = db.Table<VerificationMethod> ().Where (tis => tis.Id == svt.VerificationMethodId).FirstOrDefault ();
											if (inm != null) {
												var nElect = new TabletVerificationMethodDto ();
												nElect.id = inm.Id;
												nElect.name = inm.Name;
												nElect.isObsolete = inm.IsObsolete;
												dtoSource.verificationMethod = nElect;
											}
										}

										var efemeses = db.Table<FieldMeetingSource> ().Where (efem => efem.SourceVerificationId == svt.Id).ToList ();
										if (efemeses != null && efemeses.Count > 0) {
											var fields = new List<TabletFieldMeetingSourceDto> ();
											foreach (FieldMeetingSource field in efemeses) {
												var drofield = new TabletFieldMeetingSourceDto ();
												if (field.FieldVerificationId != null && field.FieldVerificationId != 0) {
													db.CreateTable<FieldVerification> ();
													var fieldverification = db.Table<FieldVerification> ().Where (fv => fv.Id == field.FieldVerificationId).FirstOrDefault ();
													if (fieldverification != null) {
														var nfv = new TabletFieldVerificationDto ();
														nfv.code = fieldverification.Code;
														nfv.fieldName = fieldverification.FieldName;
														nfv.id = fieldverification.Id;
														nfv.idSubsection = fieldverification.IdSubsection;
														nfv.indexField = fieldverification.IndexField;
														nfv.isObsolete = fieldverification.IsObsolete;
														nfv.section = fieldverification.Section;
														nfv.sectionCode = fieldverification.SectionCode;
														nfv.type = fieldverification.Type;
														drofield.fieldVerification = nfv;
													}

													drofield.id = field.Id;
													drofield.idFieldList = field.IdFieldList ?? 0;
													drofield.isFinal = field.IsFinal ?? false;
													drofield.jsonValue = field.JsonValue ?? "";
													drofield.reason = field.Reason;
													drofield.value = field.Value ?? "";	//TODO CRITICO: al salvar con resouesta igual a la proporcionada por el imputado no encuentra el fms de la fuente 
													if (field.StatusFieldVerificationId != null && field.StatusFieldVerificationId != 0) {
														var statusfield = db.Table<StatusFieldVerification> ().Where (statfv => statfv.Id == field.StatusFieldVerificationId).FirstOrDefault ();
														if (statusfield != null) {
															var stfvDto = new TabletStatusFieldVerificationDto ();
															stfvDto.description = statusfield.Description;
															stfvDto.id = statusfield.Id;
															stfvDto.name = statusfield.Name;
															drofield.statusFieldVerification = stfvDto;
														}
													}//and asignacion estatus field

													fields.Add (drofield);

												}//end fieldverification not null
											}//end foreach}
											dtoSource.fieldMeetingSourceList = fields;
										}//end de lista de fms no vacia
										dtoFuentes.Add (dtoSource);

									}//end de foreach sourceverification
									verificacion.sourceVerifications = dtoFuentes;
								}//end validacion de lista de fuentes no vacia 
								caseSync.verification = verificacion;
							}// end of verification



							var me = db.Table<Meeting> ().Where (dme => dme.CaseDetentionId == cs.Id).FirstOrDefault ();
							if (me != null) {
								var dtoMeeting = new TabletMeetingDto ();
								dtoMeeting.commentCountry = me.CommentCountry;
								dtoMeeting.commentDrug = me.CommentDrug;
								dtoMeeting.commentHome = me.CommentHome;
								dtoMeeting.commentJob = me.CommentJob;
								dtoMeeting.commentReference = me.CommentReference;
								dtoMeeting.commentSchool = me.CommentSchool;
								if (me.DateCreate != null) {
									dtoMeeting.dateCreate = String.Format ("{0:yyyy/MM/dd}", me.DateCreate);
								}
								if (me.DateTerminate != null) {
									dtoMeeting.dateTerminate = String.Format ("{0:yyyy/MM/dd}", me.DateTerminate);
								}
								dtoMeeting.id = me.Id;
								dtoMeeting.meetingType = me.MeetingType;
								dtoMeeting.District = me.District;
								dtoMeeting.declineReason = me.DeclineReason;
								dtoMeeting.webId = me.WebId;

								if (me.StatusMeetingId != null && me.StatusMeetingId != 0) {
									var stme = db.Table<StatusMeeting> ().Where (TimeSpan => TimeSpan.Id == me.StatusMeetingId).FirstOrDefault ();
									if (stme != null) {
										dtoMeeting.status = new TabletStatusMeetingDto ();
										dtoMeeting.status.description = stme.Description;
										dtoMeeting.status.id = stme.Id;
										dtoMeeting.status.name = stme.Status;
									}
								}

								var input = db.Table<Imputed> ().Where (inpu => inpu.MeetingId == me.Id).FirstOrDefault ();
								if (input != null) {
									var dtoImp = new TabletImputedDto ();
									if (input.BirthCountry != null && input.BirthCountry != 0) {
										var paisNacimiento = db.Table<Country> ().Where (pais => pais.Id == input.BirthCountry).FirstOrDefault ();
										if (paisNacimiento != null) {
											var paisDto = new TabletCountryDto ();
											paisDto.alpha2 = paisNacimiento.Alpha2;
											paisDto.alpha3 = paisNacimiento.Alpha3;
											paisDto.id = paisNacimiento.Id;
//											paisDto.latitude = paisNacimiento.Latitude;
//											paisDto.longitude = paisNacimiento.Longitude;
											paisDto.name = paisNacimiento.Name;
											dtoImp.birthCountry = paisDto;
										}
									}//end de country
									dtoImp.birthDate = String.Format ("{0:yyyy/MM/dd}", input.BirthDate);
									dtoImp.birthLocation = input.BirthLocation ?? "";
									dtoImp.birthMunicipality = input.BirthMunicipality ?? "";
									dtoImp.birthState = input.BirthState ?? "";
									dtoImp.boys = input.Boys;
									dtoImp.celPhone = input.CelPhone;
									dtoImp.dependentBoys = input.DependentBoys;
									dtoImp.foneticString = input.FoneticString;
									dtoImp.gender = input.Gender;
									dtoImp.id = input.Id;
									dtoImp.lastNameM = input.LastNameM;
									dtoImp.lastNameP = input.LastNameP;
									dtoImp.birthInfoId = input.BirthInfo;


									if (input.LocationId != null && input.LocationId != 0) {
										var iloc = db.Table<Location> ().Where (tl => tl.Id == input.LocationId).FirstOrDefault ();
										if (iloc != null) {
											var ilocDto = new TabletLocationDto ();
											ilocDto.abbreviation = iloc.Abbreviation;
											ilocDto.description = iloc.Description;
											ilocDto.id = iloc.Id;
											ilocDto.name = iloc.Name;
											ilocDto.zipCode = iloc.ZipCode;
											var muni = db.Table<Municipality> ().Where (mun => mun.Id == iloc.MunicipalityId).FirstOrDefault ();
											if (muni != null) {
												var nmun = new TabletMunicipalityDto ();
												nmun.id = muni.Id;
												nmun.abbreviation = muni.Abbreviation;
												nmun.description = muni.Description;
												nmun.name = muni.Name;
												nmun.description = muni.Description;
												var stait = db.Table<State> ().Where (st => st.Id == muni.StateId).FirstOrDefault ();
												if (stait != null) {
													var nstt = new TabletStateDto ();
													nstt.id = stait.Id;
													nstt.abbreviation = stait.Abbreviation;
													nstt.description = stait.Description;
													nstt.name = stait.Name;
													nstt.description = stait.Description;
													var cntry = db.Table<Country> ().Where (cou => cou.Id == stait.CountryId).FirstOrDefault ();
													if (cntry != null) {
														var ncoun = new TabletCountryDto ();
														ncoun.id = cntry.Id;
														ncoun.alpha2 = cntry.Alpha2;
														ncoun.alpha3 = cntry.Alpha3;
//														ncoun.latitude = cntry.Latitude;
//														ncoun.longitude = cntry.Longitude;
														ncoun.name = cntry.Name;
														nstt.country = ncoun;
													}//end de pais
													nmun.state = nstt;
												}//end de estado
												ilocDto.municipality = nmun;
											}//end municipio
											dtoImp.location = ilocDto;
										}//end location
									}//end de addres 

									if (input.MaritalStatusId != null && input.MaritalStatusId != 0) {
										var marStat = db.Table<MaritalStatus> ().Where (mrt => mrt.Id == input.MaritalStatusId).FirstOrDefault ();
										if (marStat != null) {
											var nmar = new TabletMaritalStatusDto ();
											nmar.id = marStat.Id;
											nmar.name = marStat.Name;
											dtoImp.maritalStatus = nmar;
										}
									}
									dtoImp.birthInfoId = input.BirthInfo;
									dtoImp.name = input.Name;
									dtoImp.nickname = input.Nickname;
									dtoImp.webId = input.WebId;
									dtoImp.yearsMaritalStatus = input.YearsMaritalStatus;
									dtoMeeting.imputed = dtoImp;
								}//end of imputado para meeting

								var casas = db.Table<ImputedHome> ().Where (imph => imph.MeetingId == me.Id).ToList ();
								if (casas != null && casas.Count > 0) {
									var meHomes = new List<TabletImputedHomeDto> ();
									foreach (ImputedHome imh in casas) {
										var nhome = new TabletImputedHomeDto ();	
											
										var nad = new TabletAddressDto ();
										nad.addressString = imh.addressString;
										if (String.IsNullOrEmpty (nad.addressString)) {
											nad.addressString = imh.addressString;
										}
										if (imh.AddressId != null && imh.AddressId > 0) {
											nad.id = imh.AddressId;
										}
										nad.id = imh.AddressId;
										nad.innNum = imh.InnNum;
//											nad.lat = imh.Lat;
//											nad.lng = imh.Lng;
										nad.outNum = imh.OutNum;
										nad.street = imh.Street;
										//location

										if (imh.LocationId != null && imh.LocationId != 0) {
											var tloc = db.Table<Location> ().Where (tl => tl.Id == imh.LocationId).FirstOrDefault ();
											if (tloc != null) {
												var locdto = new TabletLocationDto ();
												locdto.abbreviation = tloc.Abbreviation;
												locdto.description = tloc.Description;
												locdto.id = tloc.Id;
												locdto.name = tloc.Name;
												locdto.zipCode = tloc.ZipCode;
												var muni = db.Table<Municipality> ().Where (mun => mun.Id == tloc.MunicipalityId).FirstOrDefault ();
												if (muni != null) {
													var nmun = new TabletMunicipalityDto ();
													nmun.id = muni.Id;
													nmun.abbreviation = muni.Abbreviation;
													nmun.description = muni.Description;
													nmun.name = muni.Name;
													nmun.description = muni.Description;
													var stait = db.Table<State> ().Where (st => st.Id == muni.StateId).FirstOrDefault ();
													if (stait != null) {
														var nstt = new TabletStateDto ();
														nstt.id = stait.Id;
														nstt.abbreviation = stait.Abbreviation;
														nstt.description = stait.Description;
														nstt.name = stait.Name;
														nstt.description = stait.Description;
														var cntry = db.Table<Country> ().Where (cou => cou.Id == stait.CountryId).FirstOrDefault ();
														if (cntry != null) {
															var ncoun = new TabletCountryDto ();
															ncoun.id = cntry.Id;
															ncoun.alpha2 = cntry.Alpha2;
															ncoun.alpha3 = cntry.Alpha3;
//																ncoun.latitude = cntry.Latitude;
//																ncoun.longitude = cntry.Longitude;
															ncoun.name = cntry.Name;
															nstt.country = ncoun;
														}//end de pais
														nmun.state = nstt;
													}//end de estado
													locdto.municipality = nmun;
												}//end municipio
												nad.location = locdto;
											}//end location
										}//end de locationId

										//END location
										nhome.address = nad;
										nhome.description = imh.Description;
										nhome.id = imh.Id;

										if (imh.HomeTypeId != null && imh.HomeTypeId != 0) {
											var hometp = db.Table<HomeType> ().Where (tis => tis.Id == imh.HomeTypeId).FirstOrDefault ();
											if (hometp != null) {
												nhome.homeType = new TabletHomeTypeDto ();
												nhome.homeType.id = hometp.Id;
												nhome.homeType.name = hometp.Name;
												nhome.homeType.obsolete = hometp.IsObsolete;
												nhome.homeType.specification = hometp.Specification;
											}
										}

										nhome.phone = imh.Phone;
										nhome.reasonChange = imh.ReasonChange;
										nhome.reasonSecondary = imh.ReasonSecondary;

										if (imh.RegisterTypeId != null && imh.RegisterTypeId != 0) {
											var registertp = db.Table<RegisterType> ().Where (tis => tis.Id == imh.RegisterTypeId).FirstOrDefault ();
											if (registertp != null) {
												nhome.registerType = new TabletRegisterTypeDto ();
												nhome.registerType.id = registertp.Id;
												nhome.registerType.name = registertp.Name;
											}
										}

										var horario = db.Table<Schedule> ().Where (hr => hr.ImputedHomeId == imh.Id).ToList ();
										if (horario != null && horario.Count > 0) {
											nhome.schedule = new List<TabletScheduleDto> ();
											foreach (Schedule s in horario) {
												var ns = new TabletScheduleDto ();
												ns.day = s.Day;
												ns.end = s.End;
												ns.id = s.Id;
												ns.start = s.Start;
												ns.webId = s.webId;
												nhome.schedule.Add (ns);
											}
										}

										nhome.specification = imh.Specification;
										nhome.timeLive = imh.TimeLive;
										nhome.webId = imh.webId;

										nhome.isHomeless = imh.IsHomeless;

										meHomes.Add (nhome);
									}//end foreach
									dtoMeeting.imputedHomes = meHomes;
								}//end casas

								var drogas = db.Table<Drug> ().Where (drgs => drgs.MeetingId == me.Id).ToList ();
								if (drogas != null && drogas.Count > 0) {
									var meDrugs = new List<TabletDrugDto> ();
									foreach (Drug d in drogas) {
										var ndrug = new TabletDrugDto ();	
										if (d.DrugTypeId != null && d.DrugTypeId != 0) {
											var typed = db.Table<DrugType> ().Where (dt => dt.Id == d.DrugTypeId).FirstOrDefault ();
											var drugtypo = new TabletDrugTypeDto ();
											drugtypo.id = typed.Id;
											drugtypo.isObsolete = typed.IsObsolete;
											drugtypo.name = typed.Name;
											drugtypo.specification = typed.Specification;
											ndrug.drugType = drugtypo;
										}
										ndrug.block = d.block;
										ndrug.id = d.Id;
										ndrug.lastUse = String.Format ("{0:yyyy/MM/dd}", d.LastUse);
										ndrug.onsetAge = d.OnsetAge;
										if (d.PeriodicityId != null && d.PeriodicityId != 0) {
											var ped = db.Table<Periodicity> ().Where (peri => peri.Id == d.PeriodicityId).FirstOrDefault ();
											var nperiod = new TabletPeriodicityDto ();
											nperiod.id = ped.Id;
											nperiod.isObsolete = ped.IsObsolete;
											nperiod.name = ped.Name;
											nperiod.specification = ped.Specification;
											ndrug.periodicity = nperiod;
										}
										ndrug.quantity = d.Quantity;
										ndrug.specificationPeriodicity = d.SpecificationPeriodicity;
										ndrug.specificationType = d.Specification;
										ndrug.webId = d.webId;
										meDrugs.Add (ndrug);
									}//end foreach
									dtoMeeting.drugs = meDrugs;
								}//end drugs


								var trabajos = db.Table<Job> ().Where (drgs => drgs.MeetingId == me.Id).ToList ();
								if (trabajos != null && trabajos.Count > 0) {
									var meJobs = new List<TabletJobDto> ();
									foreach (Job j in trabajos) {
										var nJob = new TabletJobDto ();	

										if (j.RegisterTypeId != null && j.RegisterTypeId != 0) {
											var registertp = db.Table<RegisterType> ().Where (tis => tis.Id == j.RegisterTypeId).FirstOrDefault ();
											if (registertp != null) {
												nJob.registerType = new TabletRegisterTypeDto ();
												nJob.registerType.id = registertp.Id;
												nJob.registerType.name = registertp.Name;
											}
										}

										var horario = db.Table<Schedule> ().Where (hr => hr.JobId == j.Id).ToList ();
										if (horario != null && horario.Count > 0) {
											nJob.schedule = new List<TabletScheduleDto> ();
											foreach (Schedule s in horario) {
												var ns = new TabletScheduleDto ();
												ns.day = s.Day;
												ns.end = s.End;
												ns.id = s.Id;
												ns.start = s.Start;
												ns.webId = s.webId;
												nJob.schedule.Add (ns);
											}
										}

										nJob.address = j.Address;
										nJob.block = j.block;
										nJob.company = j.Company;
										nJob.end = String.Format ("{0:yyyy/MM/dd}", j.End);
										nJob.id = j.Id;
										nJob.nameHead = j.NameHead;
										nJob.phone = j.Phone;
										nJob.post = j.Post;
										nJob.reasonChange = j.ReasonChange;
										nJob.salaryWeek = j.SalaryWeek;
										nJob.start = String.Format ("{0:yyyy/MM/dd}", j.Start);
										nJob.startPrev = String.Format ("{0:yyyy/MM/dd}", j.StartPrev);
										nJob.webId = j.webId;
										meJobs.Add (nJob);
									}//end foreach
									dtoMeeting.jobs = meJobs;
								}//end Jobs

								var dejarElPais = db.Table<LeaveCountry> ().Where (lc => lc.MeetingId == me.Id).FirstOrDefault ();
								if (dejarElPais != null) {
									dtoMeeting.leaveCountry = new TabletLeaveCountryDto ();
									dtoMeeting.leaveCountry.address = dejarElPais.Address;
									dtoMeeting.leaveCountry.media = dejarElPais.Media;
									dtoMeeting.leaveCountry.reason = dejarElPais.Reason;
									dtoMeeting.leaveCountry.specficationImmigranDoc = dejarElPais.SpecficationImmigranDoc;
									dtoMeeting.leaveCountry.specificationRelationship = dejarElPais.SpecificationRelationship;
									dtoMeeting.leaveCountry.state = dejarElPais.State;
									dtoMeeting.leaveCountry.timeAgo = dejarElPais.timeAgo;
									dtoMeeting.leaveCountry.timeResidence = dejarElPais.TimeResidence;
									dtoMeeting.leaveCountry.webId = dejarElPais.webId;

									if (dejarElPais.CommunicationFamilyId != null && dejarElPais.CommunicationFamilyId != 0) {
										var comunicationf = db.Table<Election> ().Where (tis => tis.Id == dejarElPais.CommunicationFamilyId).FirstOrDefault ();
										if (comunicationf != null) {
											var nElect = new TabletElectionDto ();
											nElect.id = comunicationf.Id;
											nElect.name = comunicationf.Name;
											dtoMeeting.leaveCountry.communicationFamily = nElect;
										}
									}

									if (dejarElPais.CountryId != null && dejarElPais.CountryId != 0) {
										var cntry = db.Table<Country> ().Where (cou => cou.Id == dejarElPais.CountryId).FirstOrDefault ();
										if (cntry != null) {
											var ncoun = new TabletCountryDto ();
											ncoun.id = cntry.Id;
											ncoun.alpha2 = cntry.Alpha2;
											ncoun.alpha3 = cntry.Alpha3;
//											ncoun.latitude = cntry.Latitude;
//											ncoun.longitude = cntry.Longitude;
											ncoun.name = cntry.Name;
											dtoMeeting.leaveCountry.country = ncoun;
										}//end de pais
									}

									if (dejarElPais.FamilyAnotherCountryId != null && dejarElPais.FamilyAnotherCountryId != 0) {
										var fotherc = db.Table<Election> ().Where (tis => tis.Id == dejarElPais.FamilyAnotherCountryId).FirstOrDefault ();
										if (fotherc != null) {
											var nElect = new TabletElectionDto ();
											nElect.id = fotherc.Id;
											nElect.name = fotherc.Name;
											dtoMeeting.leaveCountry.familyAnotherCountry = nElect;
										}
									}

									if (dejarElPais.ImmigrationDocumentId != null && dejarElPais.ImmigrationDocumentId != 0) {
										var inm = db.Table<ImmigrationDocument> ().Where (tis => tis.Id == dejarElPais.ImmigrationDocumentId).FirstOrDefault ();
										if (inm != null) {
											var nElect = new TabletImmigrationDocumentDto ();
											nElect.id = inm.Id;
											nElect.name = inm.Name;
											nElect.obsolete = inm.IsObsolete;
											nElect.specification = inm.Specification;
											dtoMeeting.leaveCountry.immigrationDocument = nElect;
										}
									}

									if (dejarElPais.LivedCountryId != null && dejarElPais.LivedCountryId != 0) {
										var fotherc = db.Table<Election> ().Where (tis => tis.Id == dejarElPais.LivedCountryId).FirstOrDefault ();
										if (fotherc != null) {
											var nElect = new TabletElectionDto ();
											nElect.id = fotherc.Id;
											nElect.name = fotherc.Name;
											dtoMeeting.leaveCountry.livedCountry = nElect;
										}
									}


									if (dejarElPais.OfficialDocumentationId != null && dejarElPais.OfficialDocumentationId != 0) {
										var fotherc = db.Table<Election> ().Where (tis => tis.Id == dejarElPais.OfficialDocumentationId).FirstOrDefault ();
										if (fotherc != null) {
											var nElect = new TabletElectionDto ();
											nElect.id = fotherc.Id;
											nElect.name = fotherc.Name;
											dtoMeeting.leaveCountry.officialDocumentation = nElect;
										}
									}

									if (dejarElPais.RelationshipId != null && dejarElPais.RelationshipId != 0) {
										var inm = db.Table<Relationship> ().Where (tis => tis.Id == dejarElPais.RelationshipId).FirstOrDefault ();
										if (inm != null) {
											var nElect = new TabletRelationshipDto ();
											nElect.id = inm.Id;
											nElect.name = inm.Name;
											nElect.isObsolete = inm.IsObsolete;
											nElect.specification = inm.Specification;
											dtoMeeting.leaveCountry.relationship = nElect;
										}
									}
								}// end leave country

								var referencias = db.Table<Reference> ().Where (rfs => rfs.MeetingId == me.Id).ToList ();
								if (referencias != null && referencias.Count > 0) {
									var meRefs = new List<TabletReferenceDto> ();
									foreach (Reference r in referencias) {
										var nRef = new TabletReferenceDto ();	

										if (r.DocumentTypeId != null && r.DocumentTypeId != 0) {
											var docuTyp = db.Table<DocumentType> ().Where (tis => tis.Id == r.DocumentTypeId).FirstOrDefault ();
											if (docuTyp != null) {
												nRef.documentType = new TabletDocumentTypeDto ();
												nRef.documentType.id = docuTyp.Id;
												nRef.documentType.name = docuTyp.Name;
												nRef.documentType.isObsolete = docuTyp.IsObsolete;
												nRef.documentType.specification = docuTyp.Specification;
											}
										}

										if (r.RelationshipId != null && r.RelationshipId != 0) {
											var inm = db.Table<Relationship> ().Where (tis => tis.Id == r.RelationshipId).FirstOrDefault ();
											if (inm != null) {
												var nElect = new TabletRelationshipDto ();
												nElect.id = inm.Id;
												nElect.name = inm.Name; 
												nElect.isObsolete = inm.IsObsolete;
												nElect.specification = inm.Specification;
												nRef.relationship = nElect;
											}
										}

										nRef.address = r.Address;
										nRef.age = r.Age;
										nRef.block = r.block;
										nRef.fullName = r.FullName;
										nRef.id = r.Id;
										nRef.isAccompaniment = r.IsAccompaniment;
										nRef.phone = r.Phone;
										nRef.specification = r.SpecificationDocumentType;
										nRef.specificationRelationship = r.SpecificationRelationship;
										nRef.webId = r.webId;
										meRefs.Add (nRef);
									}//end foreach
									dtoMeeting.references = meRefs;
								}//end Jobs


								//reviewer
								var reviuer = new TabletUserDto ();
								var rlcode = db.Table<Role> ().Where (rlc => rlc.Id == revisor.roles).FirstOrDefault ();
								if (rlcode != null) {
									reviuer.roleCode = rlcode.role;
								}
								reviuer.fullname = revisor.fullname;
								reviuer.guid = guid;
								reviuer.hPassword = ecodedPass;
								reviuer.id = revisor.Id;
								dtoMeeting.reviewer = reviuer;
								//reviewer end


								var escuela = db.Table<School> ().Where (escul => escul.MeetingId == me.Id).FirstOrDefault ();
								if (escuela != null) {
									dtoMeeting.school = new TabletSchoolDto ();
									dtoMeeting.school.address = escuela.Address;
									dtoMeeting.school.block = escuela.block;
									if (escuela.DegreeId != null && escuela.DegreeId != 0) {
										var dregre = db.Table<Degree> ().Where (Deg => Deg.Id == escuela.DegreeId).FirstOrDefault ();
										if (dregre != null) {
											dtoMeeting.school.degree = new TabletDegreeDto ();
											dtoMeeting.school.degree.id = dregre.Id;
											dtoMeeting.school.degree.isObsolete = dregre.IsObsolete;
											dtoMeeting.school.degree.name = dregre.Name;
										}
									}

									dtoMeeting.school.id = escuela.Id;
									dtoMeeting.school.name = escuela.Name;
									dtoMeeting.school.phone = escuela.Phone;
									dtoMeeting.school.specification = escuela.Specification ?? "";
									dtoMeeting.school.webId = escuela.webId;
									//schedule
									var horario = db.Table<Schedule> ().Where (hr => hr.SchoolId == escuela.Id).ToList ();
									if (horario != null && horario.Count > 0) {
										dtoMeeting.school.schedule = new List<TabletScheduleDto> ();
										foreach (Schedule s in horario) {
											var ns = new TabletScheduleDto ();
											ns.day = s.Day;
											ns.end = s.End;
											ns.id = s.Id;
											ns.start = s.Start;
											ns.webId = s.webId;
											dtoMeeting.school.schedule.Add (ns);
										}
									}
								}// end of school

								var environment = db.Table<SocialEnvironment> ().Where (escul => escul.MeetingId == me.Id).FirstOrDefault ();
								if (environment != null) {
									dtoMeeting.socialEnvironment = new TabletSocialEnvironmentDto ();
									dtoMeeting.socialEnvironment.comment = environment.comment;
									dtoMeeting.socialEnvironment.id = environment.Id;
									dtoMeeting.socialEnvironment.physicalCondition = environment.physicalCondition;
									dtoMeeting.socialEnvironment.webId = environment.webId;

									var relactivities = db.Table<RelActivity> ().Where (ractiv => ractiv.SocialEnvironmentId == environment.Id).ToList ();
									if (relactivities != null && relactivities.Count > 0) {
										var actividades = new List<TabletRelSocialEnvironmentActivityDto> ();
										foreach (RelActivity rel in relactivities) {
											var nRelAct = new TabletRelSocialEnvironmentActivityDto ();
											nRelAct.id = rel.ActivityId;
											nRelAct.specification = rel.specification;
											if (rel != null && rel.ActivityId > 0) {
												var ac = db.Table<ActivityCatalog> ().Where (act => act.Id == rel.ActivityId).FirstOrDefault ();
												if (ac != null) {
													nRelAct.activity = new TabletActivityDto ();
													nRelAct.activity.id = ac.Id;
													nRelAct.activity.isObsolete = ac.IsObsolete;
													nRelAct.activity.name = ac.Name;
													nRelAct.activity.specification = ac.Specification;
												}
											}
											nRelAct.id = rel.Id;
											actividades.Add (nRelAct);
										}//end de foreach
										dtoMeeting.socialEnvironment.relSocialEnvironmentActivities = actividades;
									}//end de rel activities
								}// end of environment

								var social = db.Table<SocialNetwork> ().Where (socia => socia.MeetingId == me.Id).FirstOrDefault ();
								if (social != null) {
									dtoMeeting.socialNetwork = new TabletSocialNetworkDto ();
									dtoMeeting.socialNetwork.comment = social.Comment;
									dtoMeeting.socialNetwork.id = social.Id;
									dtoMeeting.socialNetwork.webId = social.webId;
									dtoMeeting.socialNetwork.webId = social.webId;

									var personsNetwork = db.Table<PersonSocialNetwork> ().Where (psn => psn.SocialNetworkId == social.Id).ToList ();
									if (personsNetwork != null && personsNetwork.Count > 0) {
										var persons = new List<TabletPersonSocialNetworkDto> ();
										foreach (PersonSocialNetwork per in personsNetwork) {
											var nPersn = new TabletPersonSocialNetworkDto ();
											nPersn.address = per.Address;
											nPersn.age = per.Age;
											nPersn.block = per.block;
											nPersn.id = per.Id;
											nPersn.isAccompaniment = per.isAccompaniment;
											nPersn.name = per.Name;
											nPersn.phone = per.Phone;
											nPersn.specification = per.SpecificationDocumentType;
											nPersn.specificationRelationship = per.specificationRelationship;
											nPersn.webId = per.webId;

											if (per.DocumentTypeId != null && per.DocumentTypeId != 0) {
												var docuTyp = db.Table<DocumentType> ().Where (tis => tis.Id == per.DocumentTypeId).FirstOrDefault ();
												if (docuTyp != null) {
													nPersn.documentType = new TabletDocumentTypeDto ();
													nPersn.documentType.id = docuTyp.Id;
													nPersn.documentType.name = docuTyp.Name;
													nPersn.documentType.isObsolete = docuTyp.IsObsolete;
													nPersn.documentType.specification = docuTyp.Specification;
												}
											}

											if (per.DependentId != null && per.DependentId != 0) {
												var fotherc = db.Table<Election> ().Where (tis => tis.Id == per.DependentId).FirstOrDefault ();
												if (fotherc != null) {
													var nElect = new TabletElectionDto ();
													nElect.id = fotherc.Id;
													nElect.name = fotherc.Name;
													nPersn.dependent = nElect;
												}
											}

											if (per.LivingWithIde != null && per.LivingWithIde != 0) {
												var fotherc = db.Table<Election> ().Where (tis => tis.Id == per.LivingWithIde).FirstOrDefault ();
												if (fotherc != null) {
													var nElect = new TabletElectionDto ();
													nElect.id = fotherc.Id;
													nElect.name = fotherc.Name;
													nPersn.livingWith = nElect;
												}
											}

											if (per.RelationshipId != null && per.RelationshipId != 0) {
												var inm = db.Table<Relationship> ().Where (tis => tis.Id == per.RelationshipId).FirstOrDefault ();
												if (inm != null) {
													var nElect = new TabletRelationshipDto ();
													nElect.id = inm.Id;
													nElect.name = inm.Name;
													nElect.isObsolete = inm.IsObsolete;
													nElect.specification = inm.Specification;
													nPersn.relationship = nElect;
												}
											}
											persons.Add (nPersn);
										}//end de foreach
										dtoMeeting.socialNetwork.peopleSocialNetwork = persons;
									}//end de rel activities
								}// end of social
								caseSync.meeting = dtoMeeting;
							}


							db.CreateTable<HearingFormat> ();
							var formats = db.Table<HearingFormat> ().Where (hf => hf.CaseDetention == cs.Id && hf.IsFinished == true).ToList ();
							if (formats != null && formats.Count > 0) {
								var formatList = new List<TabletHearingFormatDto> ();
								foreach (HearingFormat hf in formats) {
									TabletHearingFormatDto thf = new TabletHearingFormatDto ();
									hf.CaseDetention = cs.Id;
									if (hf.AppointmentDate != null) {
										thf.appointmentDateTime = String.Format ("{0:yyyy/MM/dd HH:mm:ss}", hf.AppointmentDate);
									}
									thf.comments = hf.Comments;
									thf.confirmComment = hf.ConfirmComment;
									thf.defenderName = hf.DefenderName;
									if (hf.EndTime != null) {
										thf.endTime = String.Format ("{0:HH:mm:ss}", hf.EndTime);
									}
									thf.hearingResult = hf.HearingResult;
									var thtype = db.Table<HearingType> ().Where (hearty => hearty.Id == hf.HearingType).FirstOrDefault ();
									if (thtype != null) {
										var nhtype = new TabletHearingTypeDto ();
										nhtype.description = thtype.Description;
										nhtype.id = thtype.Id;
										nhtype.isObsolete = thtype.IsObsolete;
										nhtype.Lock = thtype.Lock;
										nhtype.specification = thtype.Specification;
										thf.hearingType = nhtype;
									}
									thf.hearingTypeSpecification = hf.HearingTypeSpecification;
									thf.idFolder = hf.IdFolder;
									thf.idJudicial = hf.IdJudicial;
									if (hf.ImputedPresence != null) {
										thf.imputedPresence = hf.ImputedPresence ?? 0;
									}
									if (hf.InitTime != null) {
										thf.initTime = String.Format ("{0:HH:mm:ss}", hf.InitTime);
									}
									thf.isFinished = hf.IsFinished;
									thf.judgeName = hf.JudgeName;
									thf.mpName = hf.MpName;
									thf.previousHearing = hf.PreviousHearing ?? 0;
									if (hf.RegisterTime != null) {
										thf.registerTime = String.Format ("{0:yyyy/MM/dd HH:mm:ss}", hf.RegisterTime);
									}
									thf.room = hf.Room;

									thf.district = hf.District;
									thf.isHomeless = hf.IsHomeless;
									thf.locationPlace = hf.LocationPlace;
									thf.timeAgo = hf.TimeAgo;

									var reviuer = new TabletUserDto ();
									var rlcode = db.Table<Role> ().Where (rlc => rlc.Id == revisor.roles).FirstOrDefault ();
									if (rlcode != null) {
										reviuer.roleCode = rlcode.role;
									}
									reviuer.fullname = revisor.fullname;
									reviuer.guid = guid;
									reviuer.hPassword = ecodedPass;
									reviuer.id = revisor.Id;
									thf.supervisor = reviuer;
									thf.umecaSupervisor = reviuer;



									thf.showNotification = hf.ShowNotification;
									thf.terms = hf.Terms;
									if (hf.UmecaDate != null) {
										thf.umecaDate = String.Format ("{0:yyyy/MM/dd HH:mm:ss}", hf.UmecaDate);
									}
									if (hf.UmecaTime != null) {
										thf.umecaTime = String.Format ("{0:yyyy/MM/dd HH:mm:ss}", hf.UmecaTime);
									}

									var spcs = db.Table<HearingFormatSpecs> ().Where (hfsp => hfsp.Id == hf.HearingFormatSpecs).FirstOrDefault ();
									if (spcs != null) {
										var nSpecs = new TabletHearingFormatSpecsDto ();
										nSpecs.arrangementType = spcs.ArrangementType;
										nSpecs.controlDetention = spcs.ControlDetention;
										if (spcs.ExtDate != null) {
											nSpecs.extDate = String.Format ("{0:yyyy/MM/dd HH:mm:ss}", spcs.ExtDate);
										} else {
											nSpecs.extDate = null;
										}
										nSpecs.extension = spcs.Extension;
										nSpecs.id = spcs.Id;
										if (spcs.ImputationDate != null) {
											nSpecs.imputationDate = String.Format ("{0:yyyy/MM/dd HH:mm:ss}", spcs.ImputationDate);
										} else {
											nSpecs.imputationDate = null;
										}
										nSpecs.imputationFormulation = spcs.ImputationFormulation;
										if (spcs.LinkageDate != null) {
											nSpecs.linkageDate = String.Format ("{0:yyyy/MM/dd}", spcs.LinkageDate);
										} else {
											nSpecs.linkageDate = null;
										}
										nSpecs.linkageProcess = spcs.LinkageProcess;
										nSpecs.linkageRoom = spcs.LinkageRoom;
										if (spcs.LinkageTime != null) {
											nSpecs.linkageTime = String.Format ("{0:HH:mm:ss}", spcs.LinkageTime);
										} else {
											nSpecs.linkageTime = null;
										}
										nSpecs.nationalArrangement = spcs.NationalArrangement ?? false;
										thf.hearingFormatSpecs = nSpecs;
									}

									var himputed = db.Table<HearingFormatImputed> ().Where (hfsp => hfsp.Id == hf.hearingImputed).FirstOrDefault ();
									if (himputed != null) {
										var nhImputed = new TabletHearingFormatImputedDto ();
										if (himputed != null && himputed.Address > 0) {
											var adrss = db.Table<Address> ().Where (adrs => adrs.Id == himputed.Address).FirstOrDefault ();
											var nAdrss = new TabletAddressDto ();
											nAdrss.addressString = adrss.addressString;
											nAdrss.id = adrss.Id;
											nAdrss.innNum = adrss.InnNum;
											nAdrss.lat = adrss.Lat;
											nAdrss.lng = adrss.Lng;
											if (adrss.LocationId != null && adrss.LocationId != 0) {
												var tloc = db.Table<Location> ().Where (tl => tl.Id == adrss.LocationId).FirstOrDefault ();
												if (tloc != null) {
													var locdto = new TabletLocationDto ();
													locdto.abbreviation = tloc.Abbreviation;
													locdto.description = tloc.Description;
													locdto.id = tloc.Id;
													locdto.name = tloc.Name;
													locdto.zipCode = tloc.ZipCode;
													var muni = db.Table<Municipality> ().Where (mun => mun.Id == tloc.MunicipalityId).FirstOrDefault ();
													if (muni != null) {
														var nmun = new TabletMunicipalityDto ();
														nmun.id = muni.Id;
														nmun.abbreviation = muni.Abbreviation;
														nmun.description = muni.Description;
														nmun.name = muni.Name;
														nmun.description = muni.Description;
														var stait = db.Table<State> ().Where (st => st.Id == muni.StateId).FirstOrDefault ();
														if (stait != null) {
															var nstt = new TabletStateDto ();
															nstt.id = stait.Id;
															nstt.abbreviation = stait.Abbreviation;
															nstt.description = stait.Description;
															nstt.name = stait.Name;
															nstt.description = stait.Description;
															var cntry = db.Table<Country> ().Where (cou => cou.Id == stait.CountryId).FirstOrDefault ();
															if (cntry != null) {
																var ncoun = new TabletCountryDto ();
																ncoun.id = cntry.Id;
																ncoun.alpha2 = cntry.Alpha2;
																ncoun.alpha3 = cntry.Alpha3;
//																ncoun.latitude = cntry.Latitude;
//																ncoun.longitude = cntry.Longitude;
																ncoun.name = cntry.Name;
																nstt.country = ncoun;
															}//end de pais
															nmun.state = nstt;
														}//end de estado
														locdto.municipality = nmun;
													}//end municipio
													nAdrss.location = locdto;
												}//end location
											}//end de addres 
											nAdrss.outNum = adrss.OutNum;
											nAdrss.street = adrss.Street;
											nhImputed.address = nAdrss;
										}//end del if adres no es nulo
										nhImputed.birthDate = String.Format ("{0:yyyy/MM/dd HH:mm:ss}", himputed.BirthDate);
										nhImputed.id = himputed.Id;
										nhImputed.imputeTel = himputed.ImputeTel;
										nhImputed.lastNameM = himputed.LastNameM;
										nhImputed.lastNameP = himputed.LastNameP;
										nhImputed.name = himputed.Name;
										thf.hearingImputed = nhImputed;
									}

									//asigned arrangments del case
									var arrangmentsAsigned = db.Table<AssignedArrangement> ().Where (asar => asar.HearingFormat == hf.Id).ToList ();
									if (arrangmentsAsigned != null && arrangmentsAsigned.Count > 0) {
										var asignedArrms = new List<TabletAssignedArrangementDto> ();
										foreach (AssignedArrangement assArr in arrangmentsAsigned) {
											var nassignes = new TabletAssignedArrangementDto ();
											nassignes.description = assArr.Description;
											nassignes.id = assArr.Id;
											var arrangment = db.Table<Arrangement> ().Where (arrr => arrr.Id == assArr.Arrangement).FirstOrDefault ();
											if (arrangment != null) {
												var narrr = new TabletArrangementDto ();
												narrr.description = arrangment.Description;
												narrr.id = arrangment.Id;
												narrr.index = arrangment.Index;
												narrr.isDefault = arrangment.IsDefault;
												narrr.isExclusive = arrangment.IsExclusive;
												narrr.isNational = arrangment.IsNational;
												narrr.isObsolete = arrangment.IsObsolete;
												narrr.type = arrangment.Type;
												nassignes.arrangement = narrr;
											}
											asignedArrms.Add (nassignes);
										}
										thf.assignedArrangements = asignedArrms;
									}

									//contactos
									var contcts = db.Table<ContactData> ().Where (cntac => cntac.HearingFormat == hf.Id).ToList ();
									if (contcts != null && contcts.Count > 0) {
										var contactosFormato = new List<TabletContactDataDto> ();
										foreach (ContactData ctc in contcts) {
											var ncontct = new TabletContactDataDto ();
											ncontct.addressTxt = ctc.AddressTxt;
											ncontct.id = ctc.Id;
											ncontct.nameTxt = ctc.NameTxt;
											ncontct.phoneTxt = ctc.PhoneTxt;
											ncontct.liveWith = ctc.liveWith;
											contactosFormato.Add (ncontct);
										}
										thf.contacts = contactosFormato;
									}

									//crimes
									var crms = db.Table<Crime> ().Where (cry => cry.HearingFormat == hf.Id).ToList ();
									if (crms != null && crms.Count > 0) {
										var crimesFormato = new List<TabletCrimeDto> ();
										foreach (Crime crim in crms) {
											var ncrime = new TabletCrimeDto ();
											ncrime.article = crim.Article;
											ncrime.comment = crim.Comment;
											ncrime.id = crim.Id;
											if (crim.Federal != null && crim.Federal != 0) {
												var fed = new TabletElectionDto ();
												fed.id = crim.Federal;
												var electionFederal = db.Table<Election> ().Where (federa => federa.Id == crim.Federal).FirstOrDefault ();
												fed.id = electionFederal.Id;
												fed.name = electionFederal.Name;
												ncrime.federal = fed;
											}
											if (crim.IdCrimeCat != null && crim.IdCrimeCat != 0) {
												var cat = new TabletCrimeCatalogDto ();
												cat.id = crim.IdCrimeCat ?? 0;
												var catCrim = db.Table<CrimeCatalog> ().Where (cct => cct.Id == crim.IdCrimeCat).FirstOrDefault ();
												cat.description = catCrim.Description;
												cat.id = catCrim.Id;
												cat.name = catCrim.Name;
												cat.obsolete = catCrim.IsObsolete;
												ncrime.crime = cat;
											}
											crimesFormato.Add (ncrime);
										}
										thf.crimeList = crimesFormato;
									}
									formatList.Add (thf);
								}
								caseSync.hearingFormats = formatList;
							}//end of hearing formats not null

							db.CreateTable<LogCase> ();
							var espontaneas = db.Table<LogCase> ().Where (espo => espo.caseDetentionId == cs.Id).ToList ();
							if (espontaneas != null && espontaneas.Count > 0) {
								var actEspontaneas = new List<TabletLogCaseDto> ();
								foreach (LogCase lc in espontaneas) {
									var caseLog = new TabletLogCaseDto ();
									caseLog.activity = lc.activity;
									caseLog.activityString = lc.activityString;
									caseLog.caseDetentionId = lc.caseDetentionId;
									caseLog.date = lc.date;
									caseLog.dateString = lc.dateString;
									caseLog.id = lc.id;
									caseLog.resume = lc.resume;
									caseLog.title = lc.title;
									caseLog.userId = lc.userId;
									caseLog.userName = lc.userName;
									actEspontaneas.Add (caseLog);
								}
								caseSync.logsCase = actEspontaneas;
							}

							var strngEncode = JsonConvert.SerializeObject (caseSync);
							Console.Write ("strngEncode"+strngEncode);
							//aqui el caso esta lleno y se puede sincronizar
							try {
								var sincronizacionError = false;
								var mensaje = "";
								if (Method.ToString () == "verificacion") {
									if( caseSync.verification.sourceVerifications != null && caseSync.verification.sourceVerifications.Count > 0){
										var strng = JsonConvert.SerializeObject (caseSync);
										var sincronizacionMsg = uwsl.synchronizeSourcesVerification (revisor.username, guid, cs.tac ?? null, strng);
										sincronizacionError = sincronizacionMsg.hasError;
										mensaje = sincronizacionMsg.message;
										Console.WriteLine (sincronizacionMsg.message);
									}else{
										sincronizacionError = true;
										mensaje = "Para sincronizar este caso debe terminar al menos una entrevista de verificación, revise la información.";
									}
								}
								if (Method.ToString () == "meeting") {
									var sincronizacionMsg = uwsl.synchronizeMeeting (revisor.username, guid, cs.tac ?? null, JsonConvert.SerializeObject (caseSync));
									sincronizacionError = sincronizacionMsg.hasError;
									mensaje = sincronizacionMsg.message;
									Console.WriteLine (sincronizacionMsg.message);
								}
								if (Method.ToString () == "hearing") {
									var sincronizacionMsg = uwsl.synchronizeHearingFormat (revisor.username, guid, cs.tac ?? null, JsonConvert.SerializeObject (caseSync));
									sincronizacionError = sincronizacionMsg.hasError;
									mensaje = sincronizacionMsg.message;
									Console.WriteLine (sincronizacionMsg.message);
								}

								if (sincronizacionError) {
									db.Rollback ();
									return new Java.Lang.String ("{\"error\":true, \"response\":\"" + mensaje + "\"}");
								} 	
							} catch (Exception e) {
								db.Rollback ();
								db.Commit ();
								Console.WriteLine ("excepcion al sincronizar el objeto :>>>");
								Console.WriteLine (e.Message);
								return new Java.Lang.String ("{\"error\":true, \"response\":\"" + e.Message + "\"}");
							} finally {
								db.Commit ();
							}

							caseDeleteCascade (cs.Id, revisor.Id);

						}//end de si el caso no es nulo
					}// end foreach listSynchro

					return new Java.Lang.String ("{\"error\":false, \"response\":\"El caso se ha sincronizado con éxito.\"}");
				} else {
					return new Java.Lang.String ("{\"error\":true, \"response\":\"No se encontro ningun usuario asociado\"}");
				}

			}
		}



		public void caseDeleteCascade(int i,int revisor){
			try{
				using (var db = FactoryConn.GetConn ()) {
					Case cs = db.Table<Case> ().Where (caso => caso.Id == i).FirstOrDefault ();

					var verify = db.Table<Verification> ().Where (verf => verf.CaseDetentionId == cs.Id && verf.ReviewerId == revisor).FirstOrDefault ();
					if (verify != null) {
						var fuentes = db.Table<SourceVerification> ().Where (svr => svr.CaseRequestId == cs.Id && svr.VerificationId == verify.Id).ToList ();
						if (fuentes != null && fuentes.Count > 0) {
							foreach (SourceVerification svt in fuentes) {
								var efemeses = db.Table<FieldMeetingSource> ().Where (efem => efem.SourceVerificationId == svt.Id).ToList ();
								if (efemeses != null && efemeses.Count > 0) {
									foreach (FieldMeetingSource ef in efemeses) {
										Console.WriteLine ("ef-->"+ef.FieldVerificationId+" and svId->"+ef.SourceVerificationId);
										db.Delete (ef);
									}
								}//end de lista de fms no vacia
							db.Delete (svt);
							}//end de foreach sourceverification
//							db.Delete (fuentes);
						}//end validacion de lista de fuentes no vacia 
						db.Delete (verify);
					}// end of verification


					var me = db.Table<Meeting> ().Where (dme => dme.CaseDetentionId == cs.Id).FirstOrDefault ();
					if (me != null) {
						var input = db.Table<Imputed> ().Where (inpu => inpu.MeetingId == me.Id).FirstOrDefault ();
						if (input != null) {
							db.Delete (input);
						}//end of imputado para meeting

						var casas = db.Table<ImputedHome> ().Where (imph => imph.MeetingId == me.Id).ToList ();
						if (casas != null && casas.Count > 0) {
							foreach (ImputedHome imh in casas) {
								if (imh.AddressId != null && imh.AddressId != 0) {
									var adres = db.Table<Address> ().Where (adt => adt.Id == imh.AddressId).FirstOrDefault ();
									if (adres != null) {
										db.Delete (adres);
									}
								}
								var horario = db.Table<Schedule> ().Where (hr => hr.ImputedHomeId == imh.Id).ToList ();
								if (horario != null && horario.Count > 0) {
									foreach (Schedule horari in horario) {
										db.Delete (horari);
									}
								}
							db.Delete (imh);
							}//end foreach
//							db.Delete (casas);
						}//end casas

						var drogas = db.Table<Drug> ().Where (drgs => drgs.MeetingId == me.Id).ToList ();
						if (drogas != null && drogas.Count > 0) {
							foreach(Drug droga in drogas){
								db.Delete (droga);
							}
						}//end drugs


						var trabajos = db.Table<Job> ().Where (drgs => drgs.MeetingId == me.Id).ToList ();
						if (trabajos != null && trabajos.Count > 0) {
							foreach (Job j in trabajos) {
								var horario = db.Table<Schedule> ().Where (hr => hr.JobId == j.Id).ToList ();
								if (horario != null && horario.Count > 0) {
									foreach (Schedule horari in horario) {
										db.Delete (horari);
									}	
								}
							db.Delete (j);
							}//end foreach
//							db.Delete (trabajos);
						}//end Jobs

						var dejarElPais = db.Table<LeaveCountry> ().Where (lc => lc.MeetingId == me.Id).FirstOrDefault ();
						if (dejarElPais != null) {
							db.Delete (dejarElPais);
						}// end leave country

						var referencias = db.Table<Reference> ().Where (rfs => rfs.MeetingId == me.Id).ToList ();
						if (referencias != null && referencias.Count > 0) {
							foreach(Reference referencia in referencias){
								db.Delete (referencias);
							}
						}//end Jobs

						var escuela = db.Table<School> ().Where (escul => escul.MeetingId == me.Id).FirstOrDefault ();
						if (escuela != null) {
							var horario = db.Table<Schedule> ().Where (hr => hr.SchoolId == escuela.Id).ToList ();
							if (horario != null && horario.Count > 0) {
								foreach (Schedule horari in horario) {
									db.Delete (horari);
								}
							}
							db.Delete (escuela);
						}// end of school

						var environment = db.Table<SocialEnvironment> ().Where (escul => escul.MeetingId == me.Id).FirstOrDefault ();
						if (environment != null) {
							var relactivities = db.Table<RelActivity> ().Where (ractiv => ractiv.SocialEnvironmentId == environment.Id).ToList ();
							if (relactivities != null && relactivities.Count > 0) {
								foreach(RelActivity relactivitie in relactivities){
									db.Delete (relactivitie);
								}
							}//end de rel activities
							db.Delete (environment);
						}// end of environment

						var social = db.Table<SocialNetwork> ().Where (socia => socia.MeetingId == me.Id).FirstOrDefault ();
						if (social != null) {
							var personsNetwork = db.Table<PersonSocialNetwork> ().Where (psn => psn.SocialNetworkId == social.Id).ToList ();
							if (personsNetwork != null && personsNetwork.Count > 0) {
								foreach (PersonSocialNetwork personsNetwor in personsNetwork) {
									db.Delete (personsNetwor);
								}
							}//end de rel activities
							db.Delete (social);
						}// end of social
						db.Delete (me);
					}



					var formats = db.Table<HearingFormat> ().Where (hf => hf.CaseDetention == cs.Id).ToList ();
					if (formats != null && formats.Count > 0) {
						var formatList = new List<TabletHearingFormatDto> ();
						foreach (HearingFormat hf in formats) {
							var spcs = db.Table<HearingFormatSpecs> ().Where (hfsp => hfsp.Id == hf.HearingFormatSpecs).FirstOrDefault ();
							if (spcs != null) {
								db.Delete (spcs);
							}

							var himputed = db.Table<HearingFormatImputed> ().Where (hfsp => hfsp.Id == hf.hearingImputed).FirstOrDefault ();
							if (himputed != null) {
								if (himputed != null && himputed.Address > 0) {
									var adrss = db.Table<Address> ().Where (adrs => adrs.Id == himputed.Address).FirstOrDefault ();
									db.Delete (adrss);
								}//end del if adres no es nulo
								db.Delete (himputed);
							}

							//asigned arrangments del case
							var arrangmentsAsigned = db.Table<AssignedArrangement> ().Where (asar => asar.HearingFormat == hf.Id).ToList ();
							if (arrangmentsAsigned != null && arrangmentsAsigned.Count > 0) {
								foreach (AssignedArrangement arrangmentsAsigne in arrangmentsAsigned) {
									db.Delete (arrangmentsAsigne);
								}
							}

							//contactos
							var contcts = db.Table<ContactData> ().Where (cntac => cntac.HearingFormat == hf.Id).ToList ();
							if (contcts != null && contcts.Count > 0) {
								foreach(ContactData contct in contcts){
									db.Delete (contct);
								}
							}

							//crimes
							var crms = db.Table<Crime> ().Where (cry => cry.HearingFormat == hf.Id).ToList ();
							if (crms != null && crms.Count > 0) {
								foreach (Crime crm in crms) {
									db.Delete (crm);
								}
							}
						db.Delete (hf);
						}
//						db.Delete (formats);
					}//end of hearing formats not null

					db.CreateTable<LogCase> ();
					var espontaneas = db.Table<LogCase> ().Where (espo => espo.caseDetentionId == cs.Id).ToList ();
					if (espontaneas != null && espontaneas.Count > 0) {
						foreach(LogCase espontanea in espontaneas){
							db.Delete (espontanea);
						}
					}
					db.Delete (cs);
					db.Close ();
				}
			}catch(Exception e){
				Console.WriteLine ("excepcion al borrar el objeto :>>>");
				Console.WriteLine (e.Message);
			}
		}


	}//class end


}