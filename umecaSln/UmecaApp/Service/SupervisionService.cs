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

namespace UmecaApp
{
	public class SupervisionService  : Java.Lang.Object
	{

		readonly CatalogServiceController services;


		Context context;

		public SupervisionService(Context context)
		{
			this.context = context;
			services = new CatalogServiceController ();
		}

		public SupervisionService ()
		{
			services = new CatalogServiceController ();
		}



		[Export("findAddressByLocation")]
		public Java.Lang.String findAddressByLocation(Java.Lang.String LocationId){
			var JsonStates = services.findAddressByLocation (LocationId.ToString());
			return new Java.Lang.String(JsonStates);
		}


		[Export("findAddressByCp")]
		public Java.Lang.String findAddressByCp(Java.Lang.String Cp){
			return new Java.Lang.String(services.findAddressByCp (Cp.ToString()));
		}


		[Export("findAllStates")]
		public Java.Lang.String findAllStates(){
			var estados = new List<State>();
			using (var db = FactoryConn.GetConn ()) {
				estados = db.Table<State> ().OrderBy (c => c.Name).ToList () ?? new List<State> ();
				db.Close ();
			}
			return new Java.Lang.String(JsonConvert.SerializeObject(estados));
		}

		[Export("findMunicipalityByState")]
		public Java.Lang.String findMunicipalityByState(Java.Lang.String idState){
			using (var db = FactoryConn.GetConn ()) {
				var n = int.Parse (idState.ToString ());
				var municipios = db.Table<Municipality> ().Where (muni => muni.StateId == n).OrderBy (c => c.Name).ToList () ?? new List<Municipality> ();
				db.Close ();
				return new Java.Lang.String (JsonConvert.SerializeObject (municipios));
			}
		}


		[Export("findLocationByMunicipality")]
		public Java.Lang.String findLocationByMunicipality(Java.Lang.String idMun){
			using (var db = FactoryConn.GetConn ()) {
				var n = int.Parse (idMun.ToString ());
				var municipios = db.Table<Location> ().Where (muni => muni.MunicipalityId == n).OrderBy (c => c.Name).ToList () ?? new List<Location> ();
				db.Close ();
				return new Java.Lang.String (JsonConvert.SerializeObject (municipios));
			}
		}

		[Export("findAllByLocation")]
		public Java.Lang.String findAllByLocation(Java.Lang.String idLocation){
			using (var db = FactoryConn.GetConn ()) {
				var n = int.Parse (idLocation.ToString ());
				var location = db.Table<Location> ().Where (loc => loc.Id == n).FirstOrDefault ();
				var mnid = location.MunicipalityId;
				var municipio = db.Table<Municipality> ().Where (muni => muni.Id == mnid).FirstOrDefault ();
				var stid = municipio.StateId;
				String obj = "{ \"StateId\" : " + stid + ", \"MunicipalityId\" : " + mnid + " }";
				db.Close ();
				return new Java.Lang.String (obj);
			}
		}

		[Export("findAllDrugType")]
		public Java.Lang.String findAllDrugType(){
			using (var db = FactoryConn.GetConn ()) {
				var drogas = db.Table<DrugType> ().OrderBy (c=>c.Name).ToList ()??new List<DrugType> ();
				db.Close();
				return new Java.Lang.String(JsonConvert.SerializeObject(drogas));
			}
		}

		[Export("findAllPeriodicity")]
		public Java.Lang.String findAllPeriodicity(){
			using (var db = FactoryConn.GetConn ()) {
				var periodicidad = db.Table<Periodicity> ().OrderBy (c => c.Name).ToList () ?? new List<Periodicity> ();
				db.Close();
				return new Java.Lang.String (JsonConvert.SerializeObject (periodicidad));
			}
		}




		[Export("getArrangmentLst")]
		public Java.Lang.String getArrangmentLst(Java.Lang.String national, Java.Lang.String idTipo){
			using (var db = FactoryConn.GetConn ()) {
			var n = bool.Parse (national.ToString());
			var typ = int.Parse (idTipo.ToString());
			List<ArrangementView> lstArrmntView = new List<ArrangementView> ();

			var arras = db.Table<Arrangement> ().Where (ar => ar.IsNational == n
				&& ar.Type == typ).OrderByDescending(ar=>ar.Index).ToList();

			foreach(Arrangement ar in arras){
				ArrangementView arVnew = new ArrangementView ();
				arVnew.id = ar.Id;
					arVnew.name = ar.Description;
				arVnew.isDefault = ar.IsDefault;
				if (arVnew.isDefault == true) {
					arVnew.selVal = true;
					arVnew.description = "--";
				}else {
					arVnew.selVal = false;
				}
				arVnew.isExclusive = ar.IsExclusive;
				lstArrmntView.Add (arVnew);
			}
				db.Close();
			return new Java.Lang.String(JsonConvert.SerializeObject(lstArrmntView));
			}
		}



		[Export("upsertHearingFormat")]
		public Java.Lang.String upsertHearingFormat(Java.Lang.String modelJson){
				var output = new Java.Lang.String("");
				Console.WriteLine ("upsertHearingFormat json model-->"+modelJson);
				try{
					var convertable = modelJson.Replace(":null",":''").Replace(":[]",":null").ToString();
					var model = JsonConvert.DeserializeObject<HearingFormatView> (convertable);
					using (var db = FactoryConn.GetConn ()) {
						var incompleteHf = db.Table<HearingFormat>().Where(hef=>hef.CaseDetention==model.idCase
							&&hef.IsFinished==false).OrderByDescending(hef=>hef.Id).FirstOrDefault();
						if (incompleteHf != null && incompleteHf.Id > 0 && incompleteHf.Id != model.idFormat){
							output = new Java.Lang.String("Tiene un formato de audiencia anterior incompleto, debe terminarlo para poder agregar un nuevo formato de audiencia.");
						}else if (model.isFinished??false) {
							if (model.vincProcess != null && model.vincProcess == Constants.PROCESS_VINC_NO) {
								var renewCred = Crypto.HashPassword(model.credPass);
								var loggedUsr = db.Table<User>().FirstOrDefault();
								if(loggedUsr == null || renewCred!=loggedUsr.password){
									output = new Java.Lang.String("La contraseña es incorrecta, verifique los datos.");
								}
							}
						}
						db.Close();
					}
					if(output.ToString()==""){
						HFDtoSave salve = new HFDtoSave();
						salve = fillHearingFormatWithView(model);
						salve.IsSubstracted = model.IsSubstracted;
						output = new Java.Lang.String (hearingFormatServiceSave(salve));
					}
				}catch(Exception e){
					Console.WriteLine ("exception in upsertHearingFormat()");
					Console.WriteLine("Exception message :::>"+e.Message);
					output = new Java.Lang.String ("Ha ocurrido un error, intente nuevamente");
				}
				return output;
		}







		public HFDtoSave fillHearingFormatWithView(HearingFormatView viewFormat){
			string testJson = "{'id':1,'idFolder':'1020304050','recidivist':false,'dateCreate':'2015/06/08'}";
			var objecto = JsonConvert.DeserializeObject<TestTabletCaseDto> (testJson);
			using (var db = FactoryConn.GetConn ()) {
			HFDtoSave result = new HFDtoSave ();
			HearingFormat hearingFormat = new HearingFormat ();
			if (viewFormat.idFormat != null && viewFormat.idFormat > 0) {
				var updatable = db.Table<HearingFormat> ().Where (up => up.Id == viewFormat.idFormat).FirstOrDefault ();
				if(updatable!=null){
					hearingFormat = updatable;
				}
			}
			hearingFormat.RegisterTime = DateTime.Now;
//			var supervisor = db.Table<User> ().FirstOrDefault ();
//			hearingFormat.Supervisor = supervisor.Id;

			hearingFormat.HearingType = viewFormat.hearingTypeId??0;
			hearingFormat.ImputedPresence = viewFormat.imputedPresence??0;
			hearingFormat.HearingResult = viewFormat.hearingResult;

				hearingFormat.TimeAgo = viewFormat.TimeAgo;
				hearingFormat.LocationPlace = viewFormat.LocationPlace;

			bool hasFirstFH = false;
			HearingFormat lastHF = null;
			Case existCase = db.Table<Case> ().Where (cs => cs.Id == viewFormat.idCase).FirstOrDefault ();
			hearingFormat.CaseDetention = existCase.Id;
			List<HearingFormat> existFinishedFormats = db.Table<HearingFormat> ().Where (hef=>hef.CaseDetention==existCase.Id 
				&& hef.IsFinished == true).OrderByDescending(hef=>hef.Id).ToList();

			if (existFinishedFormats != null && existFinishedFormats.Count > 0) {
				hasFirstFH = true;
				lastHF = existFinishedFormats.First ();
			}

			if (hasFirstFH) {
				hearingFormat.IdFolder = lastHF.IdFolder;
				hearingFormat.IdJudicial = lastHF.IdJudicial;

				if (viewFormat.hearingTypeId != null) {
					hearingFormat.HearingType = viewFormat.hearingTypeId??0;
					hearingFormat.HearingTypeSpecification = viewFormat.hearingTypeSpecification;
				}

				hearingFormat.ImputedPresence = viewFormat.imputedPresence??0;
				hearingFormat.HearingResult = viewFormat.hearingResult;

			} else {
				hearingFormat.IdFolder = viewFormat.idFolder;
				hearingFormat.IdJudicial = viewFormat.idJudicial;
			}

			hearingFormat.Room = viewFormat.room;
			hearingFormat.District = viewFormat.District;
			hearingFormat.IsHomeless = viewFormat.IsHomeless;

				hearingFormat.TimeAgo = viewFormat.TimeAgo;
				hearingFormat.LocationPlace = viewFormat.LocationPlace;

			try {

				if (viewFormat.appointmentDateStr != null && viewFormat.appointmentDateStr.Trim()!="")
					hearingFormat.AppointmentDate = DateTime.ParseExact(viewFormat.appointmentDateStr, "yyyy/MM/dd",
						System.Globalization.CultureInfo.InvariantCulture);
				else
					hearingFormat.AppointmentDate = null;

				if (viewFormat.initTimeStr != null && viewFormat.initTimeStr.Trim()!="")
					hearingFormat.InitTime = DateTime.ParseExact(viewFormat.initTimeStr, "HH:mm:ss",
						System.Globalization.CultureInfo.InvariantCulture);
				else
					hearingFormat.InitTime = null;

				if (viewFormat.endTimeStr != null && viewFormat.endTimeStr.Trim() != "")
					hearingFormat.EndTime = DateTime.ParseExact(viewFormat.endTimeStr, "HH:mm:ss",
						System.Globalization.CultureInfo.InvariantCulture);
				else
					hearingFormat.EndTime = null;

				if (viewFormat.umecaDateStr != null && viewFormat.umecaDateStr.Trim() != "")
					hearingFormat.UmecaDate =  DateTime.ParseExact(viewFormat.umecaDateStr, "yyyy/MM/dd",
						System.Globalization.CultureInfo.InvariantCulture);
				else
					hearingFormat.AppointmentDate = null;

				if (viewFormat.umecaTimeStr != null && viewFormat.umecaTimeStr.Trim() != "")
					hearingFormat.UmecaTime = DateTime.ParseExact(viewFormat.umecaTimeStr, "HH:mm:ss",
						System.Globalization.CultureInfo.InvariantCulture);
				else
					hearingFormat.UmecaTime = null;

				if (viewFormat.umecaSupervisorId != null){
					var super = db.Table<User>().FirstOrDefault();
					hearingFormat.UmecaSupervisor=super.Id;
				}

			} catch (Exception e) {
				Console.WriteLine("Ha ocurrido un error fillHearingFormat");
				Console.WriteLine("Exception message :::>"+e.Message);
				return null;
			}

			hearingFormat.JudgeName = viewFormat.judgeName;
			hearingFormat.MpName = viewFormat.mpName;
			hearingFormat.DefenderName = viewFormat.defenderName;

			hearingFormat.PreviousHearing = viewFormat.previousHearing??0;

			HearingFormatImputed hearingImputed = db.Table<HearingFormatImputed> ().Where (hfi => hfi.Id == hearingFormat.hearingImputed).FirstOrDefault();

			if (hearingImputed == null)
				hearingImputed = new HearingFormatImputed();

			hearingImputed.Name = viewFormat.imputedName;
			hearingImputed.LastNameP = viewFormat.imputedFLastName;
			hearingImputed.LastNameM = viewFormat.imputedSLastName;

			try {
				if (viewFormat.imputedBirthDateStr != null && viewFormat.imputedBirthDateStr.Trim() != "")
					hearingImputed.BirthDate = DateTime.ParseExact(viewFormat.imputedBirthDateStr, "yyyy/MM/dd",
						System.Globalization.CultureInfo.InvariantCulture);
				else
					hearingImputed.BirthDate = null;
			} catch (Exception e) {
				Console.WriteLine("Ha ocurrido un error fillHearingFormat hearingImputed.BirthDate");
				Console.WriteLine("Exception message :::>"+e.Message);
				//logException.Write(e, this.getClass(), "parsingBirthDateHearingFormat", sharedUserService);
				return null;
			}
			try{
				hearingImputed.ImputeTel = viewFormat.imputedTel;

				if (viewFormat.location != null &&  viewFormat.location.Id > 0 ) {
					Address address = db.Table<Address>().Where(ad=>ad.Id==hearingImputed.Address).FirstOrDefault();
					if (address == null)
						address = new Address();

					address.Street = viewFormat.street;
					address.OutNum = viewFormat.outNum;
					address.InnNum = viewFormat.innNum;
					address.Lat = viewFormat.lat;
					address.Lng = viewFormat.lng;
					address.LocationId = viewFormat.location.Id;
					address.addressString = address.ToString();
					hearingImputed.Address = address.Id;//TODO: asignar el id al guardar el address
					result.addressImputado = address;

					}
					if(hearingFormat.IsHomeless){
						Address address = db.Table<Address>().Where(ad=>ad.Id==hearingImputed.Address).FirstOrDefault();
						if (address == null)
							address = new Address();
						address.Street = viewFormat.street;
						address.OutNum = viewFormat.outNum;
						address.InnNum = viewFormat.innNum;
						Location homeles = db.Table<Location>().Where(homeLess=>homeLess.Name == Constants.HOMELESS_LOC).FirstOrDefault();
						if(homeles!=null && homeles.Id>0){
							address.LocationId = homeles.Id;
						}
						address.addressString = address.ToString();
						hearingImputed.Address = address.Id;//TODO: asignar el id al guardar el address
						result.addressImputado = address;
					}

				hearingFormat.hearingImputed =hearingImputed.Id;//TODO: asignar el id al guardar el imputed
				result.hearingFormatImputed = hearingImputed;
			} catch (Exception e) {
				Console.WriteLine("Ha ocurrido un error fillHearingFormat Imputed address");
				Console.WriteLine("Exception message :::>"+e.Message);
				//logException.Write(e, this.getClass(), "parsingBirthDateHearingFormat", sharedUserService);
				return null;
			}


			HearingFormatSpecs hearingSpecs =  new HearingFormatSpecs();

			if (hearingFormat.HearingFormatSpecs > 0 ) {
				hearingSpecs = db.Table<HearingFormatSpecs> ().Where (hspc=>hspc.Id == hearingFormat.HearingFormatSpecs).FirstOrDefault ();
			}
			hearingSpecs.ControlDetention = viewFormat.controlDetention??0;
			hearingSpecs.Extension = viewFormat.extension??0;
			hearingSpecs.ImputationFormulation = viewFormat.impForm??0;

			try {
				if (viewFormat.imputationDateStr != null && viewFormat.imputationDateStr.Trim()!="")
					hearingSpecs.ImputationDate = DateTime.ParseExact(viewFormat.imputationDateStr, "yyyy/MM/dd",
						System.Globalization.CultureInfo.InvariantCulture);
				else
					hearingSpecs.ImputationDate = null;

				if (viewFormat.extDate != null)
						hearingSpecs.ExtDate = viewFormat.extDate;
				else
					hearingSpecs.ExtDate = null;

				if (viewFormat.linkageDate != null)
						hearingSpecs.LinkageDate = viewFormat.linkageDate;
				else
					hearingSpecs.LinkageDate = null;

				if (viewFormat.linkageTime != null)
						hearingSpecs.LinkageTime = viewFormat.linkageTime;

			} catch (Exception e) {
				Console.WriteLine("Ha ocurrido un error fillHearingFormat times");
				Console.WriteLine("Exception message :::>"+e.Message);
				//logException.Write(e, this.getClass(), "parsingSpecsHearingFormat", sharedUserService);
				return null;
			}

			hearingSpecs.LinkageProcess = viewFormat.vincProcess??0;
			hearingSpecs.LinkageRoom = viewFormat.linkageRoom;


			if (viewFormat.vincProcess != null &&
				viewFormat.vincProcess == Constants.PROCESS_VINC_YES
				|| viewFormat.vincProcess == Constants.PROCESS_VINC_NO_REGISTER) {

				hearingSpecs.ArrangementType = viewFormat.arrangementType??0;
				hearingSpecs.NationalArrangement = viewFormat.nationalArrangement.GetValueOrDefault();


				String[] terms = null;
				if (viewFormat.terms != null)
					viewFormat.terms.Split(',');

				if (terms != null && terms.Length > 0)
					hearingFormat.Terms = terms[0];
				else
					hearingFormat.Terms = viewFormat.terms;

//				List<ArrangementView> lstAssignedArrnmtView;
//				Type type = new TypeToken<>() {
//				}.getType();

				if (viewFormat.lstArrangement != null && viewFormat.lstArrangement.Trim() != "") {

					var lstAssignedArrnmtView = JsonConvert.DeserializeObject<List<ArrangementView>>(viewFormat.lstArrangement);

					if (lstAssignedArrnmtView.Count > 0) {

						List<AssignedArrangement> lstNewAssigArrmnt = new List<AssignedArrangement>();

						foreach (ArrangementView arrV in lstAssignedArrnmtView) {

							if (arrV.selVal == true) {
								AssignedArrangement assArrmnt = new AssignedArrangement();
								assArrmnt.Arrangement = arrV.id;
								assArrmnt.Description = arrV.description;
								assArrmnt.HearingFormat = hearingFormat.Id;
								lstNewAssigArrmnt.Add(assArrmnt);
							}
						}

						var oldArrangements = db.Table<AssignedArrangement> ().Where (aa=>aa.HearingFormat==hearingFormat.Id).ToList ();
							

						if (oldArrangements != null&&oldArrangements.Count>0) {
							foreach (AssignedArrangement actArr in oldArrangements) {
//								actArr.HearingFormat = 0;
//								actArr.Arrangement = 0;
								db.Delete(actArr);
							}
						}

						result.newArrangments = lstNewAssigArrmnt;
					}		
				}

				var lstContactData = new List<ContactData>();




				if (viewFormat.lstContactData != null && viewFormat.lstContactData.Trim() != "") {
					lstContactData = JsonConvert.DeserializeObject<List<ContactData>>(viewFormat.lstContactData);

					if (lstContactData.Count > 0) {
						List<ContactData> lstNewContactData = new List<ContactData>();

						foreach (ContactData conV in lstContactData) {

							ContactData contact = new ContactData();
							contact.NameTxt = conV.NameTxt;
							contact.PhoneTxt = conV.PhoneTxt;
							contact.AddressTxt = conV.AddressTxt;
							contact.HearingFormat = hearingFormat.Id;
							contact.liveWith = conV.liveWith;
							lstNewContactData.Add(contact);
						}

						var oldContacts = new List<ContactData> ();
						if (hearingFormat != null && hearingFormat.Id > 0) {
							var prevContacts = db.Table<ContactData> ().Where (cont => cont.HearingFormat == hearingFormat.Id).ToList ();
							if (prevContacts != null && prevContacts.Count > 0) {
								oldContacts = prevContacts;
							}
						}


						if (oldContacts != null && oldContacts.Count>0) {
							foreach (ContactData act in oldContacts) {
//								act.HearingFormat = 0;
								db.Delete(act);
							}
						}

						result.lstContactDataView = lstNewContactData;
					} else {
						var oldContacts = new List<ContactData> ();
						if (hearingFormat != null && hearingFormat.Id > 0) {
							var prevContacts = db.Table<ContactData> ().Where (cont => cont.HearingFormat == hearingFormat.Id).ToList ();
							if (prevContacts != null && prevContacts.Count > 0) {
								oldContacts = prevContacts;
							}
						}


						if (oldContacts != null && oldContacts.Count>0) {
							foreach (ContactData act in oldContacts) {
								//act.HearingFormat = 0;
								db.Delete(act);
							}
						}
					}
				}

			} else {
				hearingFormat.ConfirmComment = viewFormat.confirmComment;
			}


			var prevCrime = db.Table<Crime> ().Where (crm=>crm.HearingFormat == hearingFormat.Id ).ToList ();
			if(prevCrime!=null && prevCrime.Count>0){
				foreach (Crime c in prevCrime) {
//					c.setHearingFormat = null;
					db.Delete(c);
				}
			}
			var auxCrime = new List<Crime>();
			var crimeView = JsonConvert.DeserializeObject<List<CrimeDto>>(viewFormat.listCrime);
			if(crimeView!=null && crimeView.Count > 0){
				foreach(CrimeDto cdto in crimeView){
					auxCrime.Add (cdto.ToCrime ());
				}
			}
			result.crimeList = auxCrime;
			result.newHearingFormatSpecs = hearingSpecs;
			hearingFormat.IsFinished = viewFormat.isFinished.GetValueOrDefault();
			hearingFormat.Comments = viewFormat.comments;


			result.hearingFormat = hearingFormat;
				db.Close();
			return result;

			}
		}



		public String hearingFormatServiceSave(HFDtoSave model) {
			using (var db = FactoryConn.GetConn ()) {
				var response = "";
				Console.WriteLine ("hearingFormatServiceSave-->");
				db.BeginTransaction ();
				var caso = db.Table<Case> ().Where (cs => cs.Id == model.hearingFormat.CaseDetention).FirstOrDefault ();
				String idFolder = caso.IdFolder;
				String idJudicial = caso.IdMP;
				int idCase = caso.Id;
				if (model.IsSubstracted != null && model.IsSubstracted == true) {
					caso.IsSubstracted = true;
					caso.DateSubstracted = DateTime.Now;
				} else {
					caso.IsSubstracted = null;
					caso.DateSubstracted = null;
				}
				if (model.hearingFormat != null && model.hearingFormat.IsFinished == true) {
					model.hearingFormat.EndTime = DateTime.Now;
					if (idJudicial == null || idJudicial.Trim () == "") {
						caso.IdMP = model.hearingFormat.IdJudicial;
					}

				} else {
					caso.StatusCaseId = db.Table<StatusCase> ().Where (stsc => stsc.Name == Constants.CASE_STATUS_HEARING_FORMAT_INCOMPLETE).FirstOrDefault ().Id;
				}
				if (model.hearingFormat != null && model.hearingFormat.IsFinished == true && model.newHearingFormatSpecs != null && model.newHearingFormatSpecs.LinkageProcess != null &&
				    model.newHearingFormatSpecs.LinkageProcess == Constants.PROCESS_VINC_NO) {
					caso.StatusCaseId = db.Table<StatusCase> ().Where (stsc => stsc.Name == Constants.CASE_STATUS_PRE_CLOSED).FirstOrDefault ().Id;
				}
				if (model.hearingFormat != null && model.hearingFormat.IsFinished == true && model.newHearingFormatSpecs != null &&
				    model.newHearingFormatSpecs.LinkageProcess != null &&
				    (model.newHearingFormatSpecs.LinkageProcess == Constants.PROCESS_VINC_YES
				    || model.newHearingFormatSpecs.LinkageProcess == Constants.PROCESS_VINC_NO_REGISTER)) {
					model.hearingFormat.ShowNotification = true;
				}
				db.Update (caso);
				//			hearingFormatRepository.save(hearingFormat);//TODO save todo lo que esta en el hearing format
				try {
					var imputedAddres = model.addressImputado;
					if (imputedAddres != null && imputedAddres.Id > 0) {
						db.Update (imputedAddres);
						model.hearingFormatImputed.Address = imputedAddres.Id;
					} else if (imputedAddres != null) {
						db.Insert (imputedAddres);
						model.hearingFormatImputed.Address = imputedAddres.Id;
					}
					var hearingImputed = model.hearingFormatImputed;
					if (hearingImputed != null && hearingImputed.Id > 0) {
						db.Update (hearingImputed);
					} else {
						db.Insert (hearingImputed);
					}
					model.hearingFormat.hearingImputed = hearingImputed.Id;
					var specs = model.newHearingFormatSpecs;
					if (specs != null && specs.Id > 0) {
						db.Update (specs);
					} else {
						db.Insert (specs);
					}
					model.hearingFormat.HearingFormatSpecs = specs.Id;
					HearingFormat formato = model.hearingFormat;
					if (formato != null && formato.Id > 0) {
						db.Update (formato);
					} else {
						db.Insert (formato);
					}

					var arrangements = model.newArrangments;
					if (arrangements != null && arrangements.Count > 0) {
						foreach (AssignedArrangement aa in arrangements) {
							aa.HearingFormat = model.hearingFormat.Id;
							db.Insert (aa);
						}
					}

					var contacts = model.lstContactDataView;
					if (contacts != null && contacts.Count > 0) {
						foreach (ContactData cd in contacts) {
							cd.HearingFormat = model.hearingFormat.Id;
							db.Insert (cd);
						}
					}

					var crimes = model.crimeList;
					if (crimes != null && crimes.Count > 0) {
						foreach (Crime cr in crimes) {
							cr.HearingFormat = model.hearingFormat.Id;
							db.Insert (cr);
						}
					}
					response = "" + formato.Id;
				} catch (Exception e) {
					db.Rollback ();
					Console.WriteLine ("exception in hearingFormatServiceSave()");
					Console.WriteLine ("Exception message :::>" + e.Message);
					response = "Ha ocurrido un error al salvar el formato, intente nuevamente";
				} finally {
					
					db.Commit ();

				}
				db.Close ();
				return response;
			}
		}


		[Export("upsertLogCase")]
		public Java.Lang.String upsertLogCase(Java.Lang.String modelJson){
			using (var db = FactoryConn.GetConn ()) {
				var output = new Java.Lang.String("");
				Console.WriteLine ("upsertLogCase json model-->"+modelJson);
				db.BeginTransaction ();
				try{
					var model = JsonConvert.DeserializeObject<LogCase> (modelJson.ToString());
					db.Insert(model);
				}catch(Exception e){
					db.Rollback ();
					Console.WriteLine ("exception in upsertLogCase()");
					Console.WriteLine("Exception message :::>"+e.Message);
					output = new Java.Lang.String ("Ha ocurrido un error, intente nuevamente");
				}
				finally{
					db.Commit ();
				}
				db.Close();
				return output;
			}
		}

	}//class end

	public class TestTabletCaseDto {

		public int id{ get; set; }
		public String idFolder{ get; set; }
		public String idMP{ get; set; }
		public Boolean recidivist{ get; set; }
		public String dateNotProsecute{ get; set; }
		public String dateObsolete{ get; set; }
		public String dateCreate{ get; set; }

	}

}