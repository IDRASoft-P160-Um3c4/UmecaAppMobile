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

namespace UmecaApp
{
	public class MeetingService  : Java.Lang.Object
	{

		readonly SQLiteConnection db;
		readonly CatalogServiceController services;

		Context context;

		public MeetingService(Context context)
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

		public MeetingService ()
		{
			db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			services = new CatalogServiceController ();
		}

		[Export("upsertPersonalData")]
		public Java.Lang.String Example(Java.Lang.String modelJson){
			var output = new Java.Lang.String("");
			Console.WriteLine ("json model-->"+modelJson);
			var model = JsonConvert.DeserializeObject<MeetingDatosPersonalesDto> (modelJson.ToString());
			db.BeginTransaction ();
			try{
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
				imputado.Nickname = model.Nickname;
				imputado.LocationId = model.LocationId;
				if(model.BirthCountry!=null){
					Country country =db.Get<Country>(model.BirthCountry);
					imputado.BirthCountry = model.BirthCountry;
					if (country.Alpha2.Equals(Constants.ALPHA2_MEXICO)) {
						imputado.BirthState = null;
						imputado.BirthLocation = null;
						imputado.BirthMunicipality = null;
						if (model.LocationId != null) {
							imputado.LocationId = model.LocationId;
						}
					} else {
						imputado.LocationId = null;
						imputado.BirthMunicipality = model.BirthMunicipality;
						imputado.BirthState = model.BirthState;
						imputado.BirthLocation = model.BirthLocation;
					}
				}else{
					imputado.BirthCountry = model.BirthCountry;
					imputado.BirthMunicipality = model.BirthMunicipality;
					imputado.BirthState = model.BirthState;
					imputado.BirthLocation = model.BirthLocation;
				}
					db.CreateTable<SocialEnvironment>();
				SocialEnvironment seCase = db.Table<SocialEnvironment>().Where (s => s.MeetingId== model.MeetingId).FirstOrDefault ();

				if (seCase != null) {
					seCase.MeetingId = model.MeetingId.GetValueOrDefault();
					seCase.physicalCondition = model.PhysicalCondition??"";
					db.Update (seCase);
				} else {
					seCase = new SocialEnvironment ();
					seCase.MeetingId = model.MeetingId.GetValueOrDefault();
					seCase.physicalCondition = model.PhysicalCondition??"";
					seCase.comment = "";
					db.Insert (seCase);
				}
					db.CreateTable<RelActivity>();
				if (seCase != null) {
					List<RelActivity> relAux = db.Table<RelActivity> ().Where(s => s.SocialEnvironmentId == seCase.Id).ToList();
					foreach(RelActivity act in relAux){
						db.Delete<RelActivity> (act.Id);
					}
				}
				if(model.Activities!=null){
					List<RelActivity> nuevasActivities = JsonConvert.DeserializeObject<List<RelActivity>>(model.Activities);
					foreach(RelActivity act in nuevasActivities){
						act.SocialEnvironmentId=seCase.Id;
						db.Insert(act);
					}
				}
				db.Update(imputado);
				output = new Java.Lang.String(Constants.MSG_SUCCESS_UPSERT);
			}catch(Exception e){
				db.Rollback ();
				Console.WriteLine("catched exception in MeetingService method Example invoked javascript calling -> MeetingService.upsertPersonalData() Exception message :::>"+e.Message);
				output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
			}
			finally{
				db.Commit ();
			}
			return output;
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


		[Export("findAllCountry")]
		public Java.Lang.String findAllCountry(){
			var paises = db.Table<Country> ().OrderBy (c=>c.Name).ToList ()??new List<Country> ();
			return new Java.Lang.String(JsonConvert.SerializeObject(paises));
		}

		[Export("findAllStates")]
		public Java.Lang.String findAllStates(){
			var estados = db.Table<State> ().OrderBy (c=>c.Name).ToList ()??new List<State> ();
			return new Java.Lang.String(JsonConvert.SerializeObject(estados));
		}

		[Export("findMunicipalityByState")]
		public Java.Lang.String findMunicipalityByState(Java.Lang.String idState){
			var n = int.Parse (idState.ToString());
			var municipios = db.Table<Municipality> ().Where (muni => muni.StateId == n).OrderBy (c=>c.Name).ToList ()??new List<Municipality> ();
			return new Java.Lang.String(JsonConvert.SerializeObject(municipios));
		}


		[Export("findLocationByMunicipality")]
		public Java.Lang.String findLocationByMunicipality(Java.Lang.String idMun){
			var n = int.Parse (idMun.ToString());
			var municipios = db.Table<Location> ().Where (muni => muni.MunicipalityId == n).OrderBy (c=>c.Name).ToList ()??new List<Location> ();
			return new Java.Lang.String(JsonConvert.SerializeObject(municipios));
		}

		[Export("findAllByLocation")]
		public Java.Lang.String findAllByLocation(Java.Lang.String idLocation){
			var n = int.Parse (idLocation.ToString());
			var location = db.Table<Location> ().Where (loc => loc.Id == n).FirstOrDefault();
			var mnid = location.MunicipalityId;
			var municipio = db.Table<Municipality> ().Where (muni => muni.Id == mnid).FirstOrDefault();
			var stid = municipio.StateId;
			String obj = "{ \"StateId\" : "+stid+", \"MunicipalityId\" : "+mnid+" }";
			return new Java.Lang.String(obj);
		}

		[Export("findAllDrugType")]
		public Java.Lang.String findAllDrugType(){
			var drogas = db.Table<DrugType> ().OrderBy (c=>c.Name).ToList ()??new List<DrugType> ();
			return new Java.Lang.String(JsonConvert.SerializeObject(drogas));
		}

		[Export("findAllPeriodicity")]
		public Java.Lang.String findAllPeriodicity(){
			var periodicidad = db.Table<Periodicity> ().OrderBy (c=>c.Name).ToList ()??new List<Periodicity> ();
			return new Java.Lang.String(JsonConvert.SerializeObject(periodicidad));
		}


		[Export("HomeTypeFindAllOrderByName")]
		public Java.Lang.String HomeTypeFindAllOrderByName(){
			return new Java.Lang.String(JsonConvert.SerializeObject(services.HomeTypeFindAllOrderByName ()));
		}

		[Export("RegisterTypeFindAllOrderByName")]
		public Java.Lang.String RegisterTypeFindAllOrderByName(){
			return new Java.Lang.String(JsonConvert.SerializeObject(services.RegisterTypeFindAllOrderByName ()));
		}

		[Export("findAllRelationship")]
		public Java.Lang.String findAllRelationship(){
			var relationships = db.Table<Relationship>().OrderBy(s=>s.Name).ToList();
			return new Java.Lang.String(JsonConvert.SerializeObject(relationships));
		}

		[Export("findAllDocumentType")]
		public Java.Lang.String findAllDocumentType(){
			var documents = db.Table<DocumentType>().OrderBy(s=>s.Name).ToList();
			return new Java.Lang.String(JsonConvert.SerializeObject(documents));
		}

		[Export("findAllElection")]
		public Java.Lang.String findAllElection(){
			return new Java.Lang.String(JsonConvert.SerializeObject (services.ElectionFindAll()));
		}

		[Export("upsertDomicilioComment")]
		public Java.Lang.String upsertDomicilioComment(Java.Lang.String modelJson){
			var output = new Java.Lang.String("");
			Console.WriteLine (" upsertDomicilioComment json model-->"+modelJson);
			var model = JsonConvert.DeserializeObject<MeetingDatosPersonalesDto> (modelJson.ToString());
			db.BeginTransaction ();
			try{
				Meeting me = db.Table<Meeting>().Where(mee => mee.Id == model.MeetingId ).FirstOrDefault();
				me.CommentHome = model.CommentHome;
				db.Update(me);
				output = new Java.Lang.String(Constants.MSG_SUCCESS_UPSERT);
			}catch(Exception e){
				db.Rollback ();
				Console.WriteLine("catched exception in MeetingService method upsertDomicilioComment invoked javascript calling -> MeetingService.upsertDomicilioComment() Exception message :::>"+e.Message);
				output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
			}
			finally{
				db.Commit ();
			}
			return output;
		}


		[Export("upsertImputedHome")]
		public Java.Lang.String upsertImputedHome(Java.Lang.String modelJson){
			var output = new Java.Lang.String("");
			Console.WriteLine ("upsertDomicilioComment json model-->"+modelJson);
			var model = JsonConvert.DeserializeObject<ImputedHome> (modelJson.ToString());
			db.BeginTransaction ();
			try{
				db.CreateTable<ImputedHome>();
				ImputedHome me = db.Table<ImputedHome>().Where(mee => mee.Id == model.Id ).FirstOrDefault();
				if(me==null){
					me = new ImputedHome();
					me.addressString = model.addressString;
					me.Description = model.Description;
					me.HomeTypeId = model.HomeTypeId;
					me.InnNum = model.InnNum;
					me.Lat = model.Lat;
					me.Lng = model.Lng;
					me.LocationId = model.LocationId;
					me.OutNum = model.OutNum;
					me.Phone = model.Phone;
					me.ReasonChange = model.ReasonChange;
					me.ReasonSecondary = model.ReasonSecondary;
					me.RegisterTypeId = model.RegisterTypeId;
					me.Specification = model.Specification;
					me.Street = model.Street;
					me.TimeLive = model.TimeLive;
					me.Lat = model.Lat;
					me.Lat = model.Lat;
					me.MeetingId = model.MeetingId??0;
					me.addressString = ImputedHomeAddressString(me);
					db.Insert(me);
				}else{
					me.addressString = model.addressString;
					me.Description = model.Description;
					me.HomeTypeId = model.HomeTypeId;
					me.InnNum = model.InnNum;
					me.Lat = model.Lat;
					me.Lng = model.Lng;
					me.LocationId = model.LocationId;
					me.OutNum = model.OutNum;
					me.Phone = model.Phone;
					me.ReasonChange = model.ReasonChange;
					me.ReasonSecondary = model.ReasonSecondary;
					me.RegisterTypeId = model.RegisterTypeId;
					me.Specification = model.Specification;
					me.Street = model.Street;
					me.TimeLive = model.TimeLive;
					me.Lat = model.Lat;
					me.Lat = model.Lat;
					me.MeetingId = model.MeetingId??0;
					me.addressString = ImputedHomeAddressString(me);
					db.Update(me);
				}
				db.CreateTable<Schedule>();
				var schedule = db.Table<Schedule>().Where(sc=>sc.ImputedHomeId==me.Id).ToList();
				foreach(Schedule sch in schedule){
					db.Delete(sch);
				}
				if(model.Schedule!=null){
					var newSchedules = JsonConvert.DeserializeObject<List<Schedule>>(model.Schedule);
					foreach(Schedule sch in newSchedules){
						sch.ImputedHomeId = me.Id;
						db.Insert(sch);
					}
				}
				output = new Java.Lang.String("");
				Console.WriteLine ("saved imputed home-->"+me.MeetingId);
			}catch(Exception e){
				db.Rollback ();
				Console.WriteLine ("catched exception in MeetingService method upsertImputedHome invoked javascript calling -> MeetingService.upsertImputedHome()");
				Console.WriteLine("Exception message :::>"+e.Message);
				output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
			}
			finally{
				db.Commit ();
			}
			return output;
		}


		[Export("eraseImputedHome")]
		public Java.Lang.String eraseImputedHome(Java.Lang.String idHome){
			var output = new Java.Lang.String("");
			try{
				var HomeId = int.Parse (idHome.ToString ());
				db.CreateTable<ImputedHome> ();
				ImputedHome mdl=db.Table<ImputedHome> ().Where (lv => lv.Id == HomeId).FirstOrDefault ();
				db.CreateTable<Schedule>();
				var schedule = db.Table<Schedule>().Where(sc=>sc.ImputedHomeId==HomeId).ToList();
				foreach(Schedule sch in schedule){
					db.Delete(sch);
				}
				db.Delete(mdl);
				Console.WriteLine ("erased imputed home-->"+idHome);
			}catch(Exception e){
				db.Rollback ();
				Console.WriteLine ("catched exception in MeetingService method eraseImputedHome invoked javascript calling -> MeetingService.eraseImputedHome("+idHome+")");
				Console.WriteLine("Exception message :::>"+e.Message);
				output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
			}
			finally{
				db.Commit ();
			}
			return output;
		}

		[Export("upsertPersonaRedSocial")]
		public Java.Lang.String upsertPersonaRedSocial(Java.Lang.String modelJson){
			var output = new Java.Lang.String("");
			Console.WriteLine ("upsertPersonaRedSocial json model-->"+modelJson);
			var model = JsonConvert.DeserializeObject<PersonSocialNetwork> (modelJson.ToString());
			db.BeginTransaction ();
			try{
				db.CreateTable<PersonSocialNetwork>();
				PersonSocialNetwork me = db.Table<PersonSocialNetwork>().Where(mee => mee.Id == model.Id ).FirstOrDefault();
				if(me==null){
					me = new PersonSocialNetwork();
					me = model;
					db.Insert(me);
				}else{
					db.Update(model);
				}
			}catch(Exception e){
				db.Rollback ();
				Console.WriteLine ("catched exception in MeetingService method upsertPersonaRedSocial invoked javascript calling -> MeetingService.upsertPersonaRedSocial()");
				Console.WriteLine("Exception message :::>"+e.Message);
				output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
			}
			finally{
				db.Commit ();
			}
			return output;
		}

		[Export("erasePersonaRedSocial")]
		public Java.Lang.String erasePersonaRedSocial(Java.Lang.String PersonId){
			var prsnId = int.Parse(PersonId.ToString ());
			var output = new Java.Lang.String("");
			Console.WriteLine ("erasePersonaRedSocial json PersonId-->"+PersonId);
			db.CreateTable<PersonSocialNetwork> ();
			var model = db.Table<PersonSocialNetwork> ().Where(s=> s.Id == prsnId).FirstOrDefault();
			db.BeginTransaction ();
			try{
				db.Delete(model);
			}catch(Exception e){
				db.Rollback ();
				Console.WriteLine ("catched exception in MeetingService method erasePersonaRedSocial invoked javascript calling -> MeetingService.erasePersonaRedSocial()");
				Console.WriteLine("Exception message :::>"+e.Message);
				output = new Java.Lang.String (Constants.MSG_ERROR_DELETE);
			}
			finally{
				db.Commit ();
			}
			return output;
		}

		[Export("upsertRedSocialComment")]
		public Java.Lang.String upsertRedSocialComment(Java.Lang.String modelJson){
			var output = new Java.Lang.String("");
			Console.WriteLine ("upsertRedSocialComment json model-->"+modelJson);
			var model = JsonConvert.DeserializeObject<MeetingDatosPersonalesDto> (modelJson.ToString());
			db.BeginTransaction ();
			try{
				db.CreateTable<SocialNetwork>();
				SocialNetwork me = db.Table<SocialNetwork>().Where(mee => mee.MeetingId == model.MeetingId ).FirstOrDefault();
				if(me==null){
					me = new SocialNetwork();
					me.Comment = model.CommentSocialNetwork;
					me.MeetingId = model.MeetingId??0;
					db.Insert(me);
				}else{
					me.Comment = model.CommentSocialNetwork;
					me.MeetingId = model.MeetingId??0;
					db.Update(me);
				}
				output = new Java.Lang.String(Constants.MSG_SUCCESS_UPSERT);
			}catch(Exception e){
				db.Rollback ();
				Console.WriteLine("catched exception in MeetingService method upsertRedSocialComment invoked javascript calling -> MeetingService.upsertRedSocialComment() Exception message :::>"+e.Message);
				output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
			}
			finally{
				db.Commit ();
			}
			return output;
		}

		[Export("upsertRefrerencia")]
		public Java.Lang.String upsertRefrerencia(Java.Lang.String modelJson){
			var output = new Java.Lang.String("");
			Console.WriteLine ("upsertRefrerencia json model-->"+modelJson);
			var model = JsonConvert.DeserializeObject<Reference> (modelJson.ToString());
			db.BeginTransaction ();
			try{
				db.CreateTable<Reference>();
				Reference me = db.Table<Reference>().Where(mee => mee.Id == model.Id ).FirstOrDefault();
				if(me==null){
					me = new Reference();
					me = model;
					db.Insert(me);
				}else{
					db.Update(model);
				}
			}catch(Exception e){
				db.Rollback ();
				Console.WriteLine ("catched exception in MeetingService method upsertReference invoked javascript calling -> MeetingService.upsertReference()");
				Console.WriteLine("Exception message :::>"+e.Message);
				output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
			}
			finally{
				db.Commit ();
			}
			return output;
		}

		[Export("eraseReferencia")]
		public Java.Lang.String eraseReferencia(Java.Lang.String ReferenceId){
			var referencedId = int.Parse(ReferenceId.ToString ());
			var output = new Java.Lang.String("");
			Console.WriteLine ("eraseReferencia json ReferenceId-->"+ReferenceId);
			db.CreateTable<Reference> ();
			var model = db.Table<Reference> ().Where(s=> s.Id == referencedId).FirstOrDefault();
			db.BeginTransaction ();
			try{
				db.Delete(model);
			}catch(Exception e){
				db.Rollback ();
				Console.WriteLine ("catched exception in MeetingService method eraseReferencia invoked javascript calling -> MeetingService.eraseReferencia()");
				Console.WriteLine("Exception message :::>"+e.Message);
				output = new Java.Lang.String (Constants.MSG_ERROR_DELETE);
			}
			finally{
				db.Commit ();
			}
			return output;
		}

		[Export("upsertReferenciasComment")]
		public Java.Lang.String upsertReferenciasComment(Java.Lang.String modelJson){
			var output = new Java.Lang.String("");
			Console.WriteLine ("upsertReferenciasComment json model-->"+modelJson);
			var model = JsonConvert.DeserializeObject<MeetingDatosPersonalesDto> (modelJson.ToString());
			db.BeginTransaction ();
			try{
				Meeting me = db.Table<Meeting>().Where(mee => mee.Id == model.MeetingId ).FirstOrDefault();
				me.CommentReference = model.CommentReference;
				db.Update(me);
				output = new Java.Lang.String(Constants.MSG_SUCCESS_UPSERT);
			}catch(Exception e){
				db.Rollback ();
				Console.WriteLine("catched exception in MeetingService method upsertReferenciasComment invoked javascript calling -> MeetingService.upsertReferenciasComment() Exception message :::>"+e.Message);
				output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
			}
			finally{
				db.Commit ();
			}
			return output;
		}

		[Export("upsertLaboral")]
		public Java.Lang.String upsertLaboral(Java.Lang.String modelJson){
			var output = new Java.Lang.String("");
			Console.WriteLine ("upsertLaboral json model-->"+modelJson);
			var model = JsonConvert.DeserializeObject<Job> (modelJson.ToString());
			db.BeginTransaction ();
			try{
				db.CreateTable<Job>();
				Job me = db.Table<Job>().Where(mee => mee.Id == model.Id ).FirstOrDefault();
				if(me==null){
					me = new Job();
					me = model;
					db.Insert(me);
				}else{
					db.Update(model);
				}
				db.CreateTable<Schedule>();
				var schedule = db.Table<Schedule>().Where(sc=>sc.JobId==me.Id).ToList();
				foreach(Schedule sch in schedule){
					db.Delete(sch);
				}
				if(model.Schedule!=null){
					var newSchedules = JsonConvert.DeserializeObject<List<Schedule>>(model.Schedule);
					foreach(Schedule sch in newSchedules){
						sch.JobId = me.Id;
						db.Insert(sch);
					}
				}
			}catch(Exception e){
				db.Rollback ();
				Console.WriteLine ("catched exception in MeetingService method upsertLaboral invoked javascript calling -> MeetingService.upsertLaboral()");
				Console.WriteLine("Exception message :::>"+e.Message);
				output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
			}
			finally{
				db.Commit ();
			}
			return output;
		}

		[Export("eraseLaboral")]
		public Java.Lang.String eraseLaboral(Java.Lang.String idLaboral){
			var laboralId = int.Parse(idLaboral.ToString ());
			var output = new Java.Lang.String("");
			Console.WriteLine ("eraseLaboral json laboralId-->"+laboralId);
			db.CreateTable<Job> ();
			var model = db.Table<Job> ().Where(s=> s.Id == laboralId).FirstOrDefault();
			db.BeginTransaction ();
			try{
				if(model==null){
					output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
				}else{
					db.CreateTable<Schedule>();
					var schedule = db.Table<Schedule>().Where(sc=>sc.JobId==model.Id).ToList();
					foreach(Schedule sch in schedule){
						db.Delete(sch);
					}
					db.Delete(model);
				}
			}catch(Exception e){
				db.Rollback ();
				Console.WriteLine ("catched exception in MeetingService method eraseLaboral invoked javascript calling -> MeetingService.eraseLaboral()");
				Console.WriteLine("Exception message :::>"+e.Message);
				output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
			}
			finally{
				db.Commit ();
			}
			return output;
		}

		[Export("upsertLaboralComment")]
		public Java.Lang.String upsertLaboralComment(Java.Lang.String modelJson){
			var output = new Java.Lang.String("");
			Console.WriteLine ("upsertDomicilioComment json model-->"+modelJson);
			var model = JsonConvert.DeserializeObject<MeetingDatosPersonalesDto> (modelJson.ToString());
			db.BeginTransaction ();
			try{
				Meeting me = db.Table<Meeting>().Where(mee => mee.Id == model.MeetingId ).FirstOrDefault();
				me.CommentJob = model.CommentJob;
				db.Update(me);
				output = new Java.Lang.String(Constants.MSG_SUCCESS_UPSERT);
			}catch(Exception e){
				db.Rollback ();
				Console.WriteLine("catched exception in MeetingService method upsertDomicilioComment invoked javascript calling -> MeetingService.upsertDomicilioComment() Exception message :::>"+e.Message);
				output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
			}
			finally{
				db.Commit ();
			}
			return output;
		}

		[Export("upsertDrug")]
		public Java.Lang.String upsertDrug(Java.Lang.String modelJson){
			var output = new Java.Lang.String("");
			Console.WriteLine ("upsertDrug json model-->"+modelJson);
			var model = JsonConvert.DeserializeObject<Drug> (modelJson.ToString());
			db.BeginTransaction ();
			try{
				db.CreateTable<Drug>();
				Drug me = db.Table<Drug>().Where(mee => mee.Id == model.Id ).FirstOrDefault();
				if(me==null){
					me = new Drug();
					me = model;
					db.Insert(me);
				}else{
					db.Update(model);
				}
			}catch(Exception e){
				db.Rollback ();
				Console.WriteLine ("catched exception in MeetingService method upsertDrug invoked javascript calling -> MeetingService.upsertDrug()");
				Console.WriteLine("Exception message :::>"+e.Message);
				output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
			}
			finally{
				db.Commit ();
			}
			return output;
		}

		[Export("eraseDrug")]
		public Java.Lang.String eraseDrug(Java.Lang.String DrugId){
			var IdDrug = int.Parse(DrugId.ToString ());
			var output = new Java.Lang.String("");
			Console.WriteLine ("eraseDrug json IdDrug-->"+IdDrug);
			db.CreateTable<Drug> ();
			var model = db.Table<Drug> ().Where(s=> s.Id == IdDrug).FirstOrDefault();
			db.BeginTransaction ();
			try{
				db.Delete(model);
			}catch(Exception e){
				db.Rollback ();
				Console.WriteLine ("catched exception in MeetingService method eraseDrug invoked javascript calling -> MeetingService.eraseDrug()");
				Console.WriteLine("Exception message :::>"+e.Message);
				output = new Java.Lang.String (Constants.MSG_ERROR_DELETE);
			}
			finally{
				db.Commit ();
			}
			return output;
		}

		[Export("upsertDrugComment")]
		public Java.Lang.String upsertDrugComment(Java.Lang.String modelJson){
			var output = new Java.Lang.String("");
			Console.WriteLine ("upsertDomicilioComment json model-->"+modelJson);
			var model = JsonConvert.DeserializeObject<MeetingDatosPersonalesDto> (modelJson.ToString());
			db.BeginTransaction ();
			try{
				Meeting me = db.Table<Meeting>().Where(mee => mee.Id == model.MeetingId ).FirstOrDefault();
				me.CommentDrug = model.CommentDrug;
				db.Update(me);
				output = new Java.Lang.String(Constants.MSG_SUCCESS_UPSERT);
			}catch(Exception e){
				db.Rollback ();
				Console.WriteLine("catched exception in MeetingService method upsertDomicilioComment invoked javascript calling -> MeetingService.upsertDomicilioComment() Exception message :::>"+e.Message);
				output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
			}
			finally{
				db.Commit ();
			}
			return output;
		}




		[Export("upsertSchoolarship")]
		public Java.Lang.String upsertSchoolarship(Java.Lang.String modelJson){
			var output = new Java.Lang.String("");
			Console.WriteLine ("upsertDomicilioComment json model-->"+modelJson);
			var model = JsonConvert.DeserializeObject<MeetingDatosPersonalesDto> (modelJson.ToString());
			db.BeginTransaction ();
			try{
				db.CreateTable<School>();
				School me = db.Table<School>().Where(mee => mee.MeetingId == model.MeetingId ).FirstOrDefault();
				if(me==null){
					me = new School();
					me.Address = model.SchoolAddress;
					me.block = model.SchoolBlock;
					me.DegreeId = model.SchoolDegreeId;
					me.Name = model.SchoolName;
					me.Phone = model.SchoolPhone;
					me.Specification = model.SchoolSpecification;
					me.MeetingId = model.MeetingId??0;
					db.Insert(me);
				}else{
					me.Address = model.SchoolAddress;
					me.block = model.SchoolBlock;
					me.DegreeId = model.SchoolDegreeId;
					me.Name = model.SchoolName;
					me.Phone = model.SchoolPhone;
					me.Specification = model.SchoolSpecification;
					me.MeetingId = model.MeetingId??0;
					db.Update(me);
				}
				Meeting comentary = db.Table<Meeting>().Where(s=>s.Id==model.MeetingId).FirstOrDefault();
				comentary.CommentSchool = model.CommentSchool;
				db.Update(comentary);
				db.CreateTable<Schedule>();
				var schedule = db.Table<Schedule>().Where(sc=>sc.SchoolId==me.Id).ToList();
				foreach(Schedule sch in schedule){
					db.Delete(sch);
				}
				if(model.ScheduleSchool!=null){
					var newSchedules = JsonConvert.DeserializeObject<List<Schedule>>(model.ScheduleSchool);
					foreach(Schedule sch in newSchedules){
						sch.SchoolId = me.Id;
						db.Insert(sch);
					}
				}
				output = new Java.Lang.String(Constants.MSG_SUCCESS_UPSERT);
			}catch(Exception e){
				db.Rollback ();
				Console.WriteLine ("catched exception in MeetingService method upsertSchoolarship invoked javascript calling -> MeetingService.upsertSchoolarship()");
				Console.WriteLine("Exception message :::>"+e.Message);
				output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
			}
			finally{
				db.Commit ();
			}
			return output;
		}


		[Export("upsertLeaveCountry")]
		public Java.Lang.String upsertLeaveCountry(Java.Lang.String modelJson){
			var output = new Java.Lang.String("");
			Console.WriteLine ("upsertLeaveCountry json model-->"+modelJson);
			var model = JsonConvert.DeserializeObject<MeetingDatosPersonalesDto> (modelJson.ToString());
			db.BeginTransaction ();
			try{
				db.CreateTable<LeaveCountry>();
				LeaveCountry me = db.Table<LeaveCountry>().Where(mee => mee.MeetingId == model.MeetingId ).FirstOrDefault();
				if(me==null){
					me = new LeaveCountry();
					me.Address = model.Address;
					me.CommunicationFamilyId = model.CommunicationFamilyId;
					me.CountryId = model.CountryId;
					me.FamilyAnotherCountryId = model.FamilyAnotherCountryId;
					me.ImmigrationDocumentId = model.ImmigrationDocumentId;
					me.LivedCountryId = model.LivedCountryId;
					me.Media = model.Media;
					me.MeetingId = model.MeetingId??0;
					me.OfficialDocumentationId = model.OfficialDocumentationId;
					me.Reason = model.Reason;
					me.RelationshipId = model.RelationshipId;
					me.SpecficationImmigranDoc = model.SpecficationImmigranDoc;
					me.SpecificationRelationship = model.SpecificationRelationship;
					me.State = model.State;
					me.timeAgo = model.timeAgo;
					me.TimeResidence = model.TimeResidence;
					db.Insert(me);
				}else{
					me.Address = model.Address;
					me.CommunicationFamilyId = model.CommunicationFamilyId;
					me.CountryId = model.CountryId;
					me.FamilyAnotherCountryId = model.FamilyAnotherCountryId;
					me.ImmigrationDocumentId = model.ImmigrationDocumentId;
					me.LivedCountryId = model.LivedCountryId;
					me.Media = model.Media;
					me.MeetingId = model.MeetingId??0;
					me.OfficialDocumentationId = model.OfficialDocumentationId;
					me.Reason = model.Reason;
					me.RelationshipId = model.RelationshipId;
					me.SpecficationImmigranDoc = model.SpecficationImmigranDoc;
					me.SpecificationRelationship = model.SpecificationRelationship;
					me.State = model.State;
					me.timeAgo = model.timeAgo;
					me.TimeResidence = model.TimeResidence;
					db.Update(me);
				}
				Meeting comentary = db.Table<Meeting>().Where(s=>s.Id==model.MeetingId).FirstOrDefault();
				comentary.CommentCountry = model.CommentCountry;
				db.Update(comentary);
				db.CreateTable<SocialEnvironment>();
				var social = db.Table<SocialEnvironment>().Where(s=>s.Id==model.MeetingId).FirstOrDefault();
				if(social==null){
					social = new SocialEnvironment();
					social.MeetingId=model.MeetingId.GetValueOrDefault();
					social.comment= model.comment;
					db.Insert(social);
				}else{
					social.MeetingId=model.MeetingId.GetValueOrDefault();
					social.comment= model.comment;
					db.Update(social);
				}
				output = new Java.Lang.String(Constants.MSG_SUCCESS_UPSERT);
			}catch(Exception e){
				db.Rollback ();
				Console.WriteLine ("catched exception in MeetingService method upsertLeaveCountry invoked javascript calling -> MeetingService.upsertLeaveCountry()");
				Console.WriteLine("Exception message :::>"+e.Message);
				output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
			}
			finally{
				db.Commit ();
			}
			return output;
		}


		//terminate de Meeting
		[Export("TerminateMeeting")]
		public Java.Lang.String TerminateMeeting(Java.Lang.String modelJson){
			var output = new Java.Lang.String("");
			Console.WriteLine ("TerminateMeeting json model-->"+modelJson);
			var model = JsonConvert.DeserializeObject<MeetingDatosPersonalesDto> (modelJson.ToString());
			db.BeginTransaction ();
			try{

/*save de imputado sin validacion*/
				var imputado = db.Get<Imputed>(model.ImputedId); 
				imputado.Name = model.Name;
				imputado.LastNameP = model.LastNameP;
				imputado.LastNameM = model.LastNameM;
				imputado.FoneticString = services.getFoneticByName(model.Name,model.LastNameP,model.LastNameM);
				imputado.Gender = model.Gender;
				imputado.CelPhone = model.CelPhone;
				imputado.YearsMaritalStatus = model.YearsMaritalStatus;
				imputado.MaritalStatusId = model.MaritalStatusId;
				imputado.Boys = model.Boys;
				imputado.DependentBoys = model.DependentBoys;
				imputado.Nickname = model.Nickname;
				imputado.LocationId = model.LocationId;
				if(model.BirthCountry!=null){
					Country country =db.Get<Country>(model.BirthCountry);
					imputado.BirthCountry = model.BirthCountry;
					if (country.Alpha2.Equals(Constants.ALPHA2_MEXICO)) {
						imputado.BirthState = null;
						imputado.BirthLocation = null;
						imputado.BirthMunicipality = null;
						if (model.LocationId != null) {
							imputado.LocationId = model.LocationId;
						}
					} else {
						imputado.LocationId = null;
						imputado.BirthMunicipality = model.BirthMunicipality;
						imputado.BirthState = model.BirthState;
						imputado.BirthLocation = model.BirthLocation;
					}
				}else{
					imputado.BirthCountry = model.BirthCountry;
					imputado.BirthMunicipality = model.BirthMunicipality;
					imputado.BirthState = model.BirthState;
					imputado.BirthLocation = model.BirthLocation;
				}
				db.CreateTable<SocialEnvironment>();
				SocialEnvironment seCase = db.Table<SocialEnvironment>().Where (s => s.MeetingId== model.MeetingId).FirstOrDefault ();
				if (seCase != null) {
					seCase.MeetingId = model.MeetingId.GetValueOrDefault();
					seCase.physicalCondition = model.PhysicalCondition??"";
					db.Update (seCase);
				} else {
					seCase = new SocialEnvironment ();
					seCase.MeetingId = model.MeetingId.GetValueOrDefault();
					seCase.physicalCondition = model.PhysicalCondition??"";
					seCase.comment = "";
					db.Insert (seCase);
				}
				db.CreateTable<RelActivity>();
				if (seCase != null) {
					List<RelActivity> relAux = db.Table<RelActivity> ().Where(s => s.SocialEnvironmentId == seCase.Id).ToList();
					foreach(RelActivity act in relAux){
						db.Delete<RelActivity> (act.Id);
					}
				}
				if(model.Activities!=null){
					List<RelActivity> nuevasActivities = JsonConvert.DeserializeObject<List<RelActivity>>(model.Activities);
					foreach(RelActivity act in nuevasActivities){
						act.SocialEnvironmentId=seCase.Id;
						db.Insert(act);
					}
				}
				db.Update(imputado);
/*end save datos personales*/

/* saving comments */
				Meeting me = db.Table<Meeting>().Where(mee => mee.Id == model.MeetingId ).FirstOrDefault();
				/*domicilio*/
				me.CommentHome = model.CommentHome;
				/*referencias*/
				me.CommentReference = model.CommentReference;
				/*laboral*/
				me.CommentJob = model.CommentJob;
				/*historial escolar*/
				me.CommentSchool = model.CommentSchool;
				/*Drogas negocio redondo*/
				me.CommentDrug = model.CommentDrug;
				/*Leave Country*/
				me.CommentCountry = model.CommentCountry;
				db.Update(me);
/* end saving comments */

/* comentario de social network */
				db.CreateTable<SocialNetwork>();
				SocialNetwork socnet = db.Table<SocialNetwork>().Where(mee => mee.MeetingId == model.MeetingId ).FirstOrDefault();
				if(socnet==null){
					socnet = new SocialNetwork();
					socnet.Comment = model.CommentSocialNetwork;
					socnet.MeetingId = model.MeetingId??0;
					db.Insert(socnet);
				}else{
					socnet.Comment = model.CommentSocialNetwork;
					socnet.MeetingId = model.MeetingId??0;
					db.Update(socnet);
				}
/* end comentario de social network */

/* historial escolar */
					db.CreateTable<School>();
				School meSchool = db.Table<School>().Where(mee => mee.MeetingId == model.MeetingId ).FirstOrDefault();
				if(meSchool==null){
					meSchool = new School();
					meSchool.Address = model.SchoolAddress;
					meSchool.block = model.SchoolBlock;
					meSchool.DegreeId = model.SchoolDegreeId;
					meSchool.Name = model.SchoolName;
					meSchool.Phone = model.SchoolPhone;
					meSchool.Specification = model.SchoolSpecification;
					meSchool.MeetingId = model.MeetingId??0;
					db.Insert(meSchool);
					}else{
					meSchool.Address = model.SchoolAddress;
					meSchool.block = model.SchoolBlock;
					meSchool.DegreeId = model.SchoolDegreeId;
					meSchool.Name = model.SchoolName;
					meSchool.Phone = model.SchoolPhone;
					meSchool.Specification = model.SchoolSpecification;
					meSchool.MeetingId = model.MeetingId??0;
					db.Update(meSchool);
					}
					db.CreateTable<Schedule>();
					var schedule = db.Table<Schedule>().Where(sc=>sc.SchoolId==me.Id).ToList();
					foreach(Schedule sch in schedule){
						db.Delete(sch);
					}
				if(model.ScheduleSchool!=null){
					var newSchedules = JsonConvert.DeserializeObject<List<Schedule>>(model.ScheduleSchool);
					foreach(Schedule sch in newSchedules){
					sch.SchoolId = meSchool.Id;
						db.Insert(sch);
					}
				}
/* end historial escolar */

/* Leave Country */
				db.CreateTable<LeaveCountry>();
				LeaveCountry meLeaveCountry = db.Table<LeaveCountry>().Where(mee => mee.MeetingId == model.MeetingId ).FirstOrDefault();
				if(meLeaveCountry==null){
					meLeaveCountry = new LeaveCountry();
					meLeaveCountry.Address = model.Address;
					meLeaveCountry.CommunicationFamilyId = model.CommunicationFamilyId;
					meLeaveCountry.CountryId = model.CountryId;
					meLeaveCountry.FamilyAnotherCountryId = model.FamilyAnotherCountryId;
					meLeaveCountry.ImmigrationDocumentId = model.ImmigrationDocumentId;
					meLeaveCountry.LivedCountryId = model.LivedCountryId;
					meLeaveCountry.Media = model.Media;
					meLeaveCountry.MeetingId = model.MeetingId??0;
					meLeaveCountry.OfficialDocumentationId = model.OfficialDocumentationId;
					meLeaveCountry.Reason = model.Reason;
					meLeaveCountry.RelationshipId = model.RelationshipId;
					meLeaveCountry.SpecficationImmigranDoc = model.SpecficationImmigranDoc;
					meLeaveCountry.SpecificationRelationship = model.SpecificationRelationship;
					meLeaveCountry.State = model.State;
					meLeaveCountry.timeAgo = model.timeAgo;
					meLeaveCountry.TimeResidence = model.TimeResidence;
					db.Insert(meLeaveCountry);
				}else{
					meLeaveCountry.Address = model.Address;
					meLeaveCountry.CommunicationFamilyId = model.CommunicationFamilyId;
					meLeaveCountry.CountryId = model.CountryId;
					meLeaveCountry.FamilyAnotherCountryId = model.FamilyAnotherCountryId;
					meLeaveCountry.ImmigrationDocumentId = model.ImmigrationDocumentId;
					meLeaveCountry.LivedCountryId = model.LivedCountryId;
					meLeaveCountry.Media = model.Media;
					meLeaveCountry.MeetingId = model.MeetingId??0;
					meLeaveCountry.OfficialDocumentationId = model.OfficialDocumentationId;
					meLeaveCountry.Reason = model.Reason;
					meLeaveCountry.RelationshipId = model.RelationshipId;
					meLeaveCountry.SpecficationImmigranDoc = model.SpecficationImmigranDoc;
					meLeaveCountry.SpecificationRelationship = model.SpecificationRelationship;
					meLeaveCountry.State = model.State;
					meLeaveCountry.timeAgo = model.timeAgo;
					meLeaveCountry.TimeResidence = model.TimeResidence;
					db.Update(meLeaveCountry);
				}
/* endLeave Country */

				db.CreateTable<SocialEnvironment>();
				var social = db.Table<SocialEnvironment>().Where(s=>s.Id==model.MeetingId).FirstOrDefault();
				if(seCase==null){
					seCase = new SocialEnvironment();
					seCase.MeetingId=model.MeetingId.GetValueOrDefault();
					seCase.comment= model.comment;
					db.Insert(seCase);
				}else{
					seCase.MeetingId=model.MeetingId.GetValueOrDefault();
					seCase.comment= model.comment;
					db.Update(seCase);
				}

/* validaciones */
				var validate = new TerminateMeetingMessageDto();
				validateMeetingImputed(validate, imputado);
				validateMeetingSocialEnvironment(validate, seCase);
				validateMeetingSchool(validate, meSchool);
				validateMeetingCountry(validate, meLeaveCountry);

				List<String> r = new List<string>();
				const String e = "entity";

				db.CreateTable<ImputedHome> ();
				var domiciliosImputado= db.Table<ImputedHome> ().Where (im => im.MeetingId == model.MeetingId).ToList ();
				if(domiciliosImputado==null||domiciliosImputado.Count.Equals(0)){
					r.Add("Debe registrar al menos un domicilio del imputado.");
					validate.groupMessage.Add(new GroupMessageMeetingDto("imputedHome", r));
				}
				//PersonSocialNetwork
				r = new List<string>();
				db.CreateTable<PersonSocialNetwork> ();
				var personsSocNet = db.Table<PersonSocialNetwork> ().Where (sn=>sn.SocialNetworkId==socnet.Id).ToList ();
				if(socnet.Comment==null||socnet.Comment.Equals("")){
					r.Add(validate.template.Replace(e, "Las observaciones"));
				}
				if(personsSocNet==null||personsSocNet.Count.Equals(0)){
					r.Add("Para terminar la entrevista debe agregar al menos una persona en su red social.");
				}
				if(r.Count>0){
					validate.groupMessage.Add(new GroupMessageMeetingDto("socialNetwork", r));
				}
				r = new List<string>();
				db.CreateTable<Reference> ();
				var references = db.Table<Reference> ().Where (sn=>sn.MeetingId==model.MeetingId).ToList ();
				if(references==null||references.Count.Equals(0)){
					r.Add("Para terminar la entrevista debe agregar al menos una referencia personal.");
					validate.groupMessage.Add(new GroupMessageMeetingDto("reference", r));
				}

				r = new List<string>();
				db.CreateTable<Job> ();
				var trabajos = db.Table<Job> ().Where (sn=>sn.MeetingId==model.MeetingId).ToList ();
				if(trabajos==null||trabajos.Count.Equals(0)){
					r.Add("Debe agregar al menos un empleo del imputado.");
					validate.groupMessage.Add(new GroupMessageMeetingDto("job", r));
				}

				r = new List<string>();
				db.CreateTable<Drug> ();
				var drogas = db.Table<Drug> ().Where (sn=>sn.MeetingId==model.MeetingId).ToList ();
				if(drogas==null||drogas.Count.Equals(0)){
					r.Add("Debe agregar al menos una sustancia que consume el imputado. (En caso de no consumir sustancias seleccione otro y especifique ninguna)");
					validate.groupMessage.Add(new GroupMessageMeetingDto("drug", r));
				}


/* end validaciones */
				if(validate.groupMessage.Count<=0){
				output = new Java.Lang.String("");
					var casoMeeting = db.Table<Case>().Where(cm=>cm.Id==me.CaseDetentionId).FirstOrDefault();
					StatusMeeting statusMeeting2 = services.statusMeetingfindByCode(Constants.S_MEETING_INCOMPLETE_LEGAL);
					StatusCase sc = services.statusCasefindByCode(Constants.CASE_STATUS_MEETING);
					casoMeeting.StatusCaseId = sc.Id;
					casoMeeting.Status = sc;
					me.StatusMeetingId = statusMeeting2.Id;
					me.StatusMeeting = statusMeeting2;
					me.DateTerminate = DateTime.Today;
					db.Update(casoMeeting);
					db.Update(me);
				}
				else{
					List<String> listGeneral = new List<string>();
					listGeneral.Add("No se puede terminar la entrevista puesto que falta por responder preguntas, para m&aacute;s detalles revise los mensajes de cada secci&oacute;n");
					validate.groupMessage.Add(new GroupMessageMeetingDto("general", listGeneral));
					validate.formatMessages();
					output = new Java.Lang.String(JsonConvert.SerializeObject(validate));
				}
			}catch(Exception e){
				db.Rollback ();
				Console.WriteLine ("catched exception in MeetingService method TerminateMeeting invoked javascript calling -> MeetingService.TerminateMeeting()");
				Console.WriteLine("Exception message :::>"+e.Message);
				output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
			}
			finally{
				db.Commit ();
			}
			return output;
		}


		[Export("allAcademicLevels")]
		public Java.Lang.String allAcademicLevels(){
			Java.Lang.String output;
			try{
				var aL = db.Table<AcademicLevel>().ToList();
				output = new Java.Lang.String(JsonConvert.SerializeObject(aL));
			}catch(Exception e){
				Console.WriteLine("catched exception in MeetingService method allAcademicLevels -> MeetingService.allAcademicLevels() Exception message :::>"+e.Message);
				output = new Java.Lang.String ("[]");
			}
			return output;
		}


		[Export("gradeByNivel")]
		public Java.Lang.String gradeByNivel(Java.Lang.String Nivel){
			var di = int.Parse (Nivel.ToString ());
			Java.Lang.String output;
			try{
				var aL = db.Table<Degree>().Where(d=>d.AcademicLevelId==di).ToList();
				output = new Java.Lang.String(JsonConvert.SerializeObject(aL));
			}catch(Exception e){
				Console.WriteLine("catched exception in MeetingService method gradeByNivel -> MeetingService.gradeByNivel() Exception message :::>"+e.Message);
				output = new Java.Lang.String ("[]");
			}
			return output;
		}

		[Export("gradesBySelectedDegree")]
		public Java.Lang.String gradesBySelectedDegree(Java.Lang.String Degree){
			var degreeId = int.Parse (Degree.ToString ());
			Java.Lang.String output;
			try{
				var deg = db.Table<Degree>().Where(d=>d.Id==degreeId).FirstOrDefault();
				if(deg!=null){
				var aL = db.Table<Degree>().Where(d=>d.AcademicLevelId==deg.AcademicLevelId).ToList();
				output = new Java.Lang.String(JsonConvert.SerializeObject(aL));
				}else{
					output = new Java.Lang.String("[]");
				}
			}catch(Exception e){
				Console.WriteLine("catched exception in MeetingService method gradesBySelectedDegree -> MeetingService.gradesBySelectedDegree () Exception message :::>"+e.Message);
				output = new Java.Lang.String ("[]");
			}
			return output;
		}


		[Export("documentosMigratorios")]
		public Java.Lang.String documentosMigratorios(){
			Java.Lang.String output;
			try{
				var aL = db.Table<ImmigrationDocument>().ToList();
				output = new Java.Lang.String(JsonConvert.SerializeObject(aL));
			}catch(Exception e){
				Console.WriteLine("catched exception in MeetingService method documentosMigratorios -> MeetingService.documentosMigratorios() Exception message :::>"+e.Message);
				output = new Java.Lang.String ("[]");
			}
			return output;
		}

		[Export("relacionPersonal")]
		public Java.Lang.String relacionPersonal(){
			Java.Lang.String output;
			try{
				var aL = db.Table<Relationship>().ToList();
				output = new Java.Lang.String(JsonConvert.SerializeObject(aL));
			}catch(Exception e){
				Console.WriteLine("catched exception in MeetingService method documentosMigratorios -> MeetingService.documentosMigratorios() Exception message :::>"+e.Message);
				output = new Java.Lang.String ("[]");
			}
			return output;
		}




/*VALIDACIONES DE TERMINATE MEETING*/

		public TerminateMeetingMessageDto validateMeetingImputed(TerminateMeetingMessageDto t, Imputed imp){
			List<String> result = new List<string>();
			const String e = "entity";
			if(imp.Name== null || (imp.Name!=null && imp.Name.Trim().Equals(""))){
				result.Add(t.template.Replace(e, "El nombre"));
			}
			if(imp.LastNameP== null || (imp.LastNameP!=null && imp.LastNameP.Trim().Equals(""))){
				result.Add(t.template.Replace(e, "El apellido paterno"));
			}
			if(imp.LastNameM== null || (imp.LastNameM!=null && imp.LastNameM.Trim().Equals(""))){
				result.Add(t.template.Replace(e, "El apellido materno"));
			}
			if (imp.Gender == null) {
				result.Add(t.template.Replace(e, "El g&eacute;nero"));
			}
			if (imp.CelPhone == null || (imp.CelPhone != null && imp.CelPhone.Trim().Equals(""))) {
				result.Add(t.template.Replace(e, "El n&uacute;mero celular"));
			}
			if (imp.MaritalStatusId == null) {
				result.Add(t.template.Replace(e, "El estado civil"));
			} else if ((imp.MaritalStatusId.Equals(Constants.MARITAL_MARRIED) || imp.MaritalStatusId.Equals(Constants.MARITAL_UNION_FREE))
				&& imp.YearsMaritalStatus == null) {
				result.Add(t.template.Replace(e, "El n&uacute;mero de años en el estado civil"));
			}
			if (imp.Boys == null) {
				result.Add(t.template.Replace(e, "El total de hijos"));
			}
			if (imp.Nickname == null || imp.Nickname.Equals("")) {
				result.Add(t.template.Replace(e, "El ap&oacute;do"));
			}
			if (imp.DependentBoys == null) {
				result.Add(t.template.Replace(e, "El n&uacute;mero de dependientes econ&oacute;micos"));
			}
			if(imp.BirthCountry!=null){
				Country country =db.Get<Country>(imp.BirthCountry);
				if (country.Alpha2.Equals(Constants.ALPHA2_MEXICO)) {
					if (imp.LocationId == null) {
						result.Add(t.template.Replace(e, "La localidad"));
					}
				} else {
					if (imp.BirthMunicipality == null || imp.BirthMunicipality.Trim().Equals("")) {
						result.Add(t.template.Replace(e, "El municipio de nacimiento"));
					}
					if (imp.BirthState == null || imp.BirthState.Trim().Equals("")) {
						result.Add(t.template.Replace(e, "El estado de naciemiento"));
					}
					if (imp.BirthLocation == null || imp.BirthLocation.Trim().Equals("")) {
						result.Add(t.template.Replace(e, "La ciudad o localidad de nacimiento"));
					}
				}
			}else{
				result.Add(t.template.Replace(e, "El pa&iacute;s de nacimiento"));
			}
			if (result != null && result.Count > 0) {
				t.groupMessage.Add (new GroupMessageMeetingDto ("personalData", result));
			}
			return t;
		}

		public TerminateMeetingMessageDto validateMeetingSocialEnvironment(TerminateMeetingMessageDto t, SocialEnvironment Se){
			List<String> result = new List<string>();
			const String e = "entity";
			if(Se.physicalCondition==null||Se.physicalCondition.Trim().Equals("")){
				result.Add(t.template.Replace(e,"La condici&oacute;n f&iacute;sica"));
			}
			db.CreateTable<RelActivity>();
			if (Se != null) {
				List<RelActivity> relAux = db.Table<RelActivity> ().Where(s => s.SocialEnvironmentId == Se.Id).ToList();
				if(relAux==null || (relAux!= null
					&& relAux.Count==0)){
					result.Add(t.template.Replace(e,"Las actividades que realiza el imputado"));
				}
			}
			if(t.groupMessage!=null){
				foreach (GroupMessageMeetingDto gmdto in t.groupMessage){
					if(gmdto.section.Equals("personalData")){
						foreach(String a in result){
							gmdto.listString.Add(a);
						}
					}
				}
			}
			return t;
		}

		public TerminateMeetingMessageDto validateMeetingSchool(TerminateMeetingMessageDto t, School Sc){
			List<String> r = new List<string>();
			const String e = "entity";
			if (Sc.Name == null || Sc.Name.Trim().Equals("")) {
				r.Add(t.template.Replace(e, "La escuela"));
			}
			if (Sc.Phone == null || Sc.Phone.Trim().Equals("")) {
				r.Add(t.template.Replace(e, "El tel&eacute;fono"));
			}
			if (Sc.Address == null || Sc.Address.Trim().Equals("")) {
				r.Add(t.template.Replace(e, "La direcci&oacute;n"));
			}
			if (Sc.DegreeId == null||Sc.DegreeId.Equals(0)) {
				r.Add(t.template.Replace(e, "El grado escolar"));
			}
			if (r != null && r.Count > 0) {
				t.groupMessage.Add (new GroupMessageMeetingDto ("school", r));
			}
			return t;
		}


		public TerminateMeetingMessageDto validateMeetingCountry(TerminateMeetingMessageDto t, LeaveCountry Lc){
			List<String> r = new List<string>();
			const String e = "entity";
			if(Lc.OfficialDocumentationId==null||Lc.OfficialDocumentationId.Equals(0)){
				r.Add(t.template.Replace(e,"Si cuenta con documentaci&oacute;n para salir del pa&iacute;s"));
			}else if(Lc.OfficialDocumentationId!=null && Lc.OfficialDocumentationId.Equals(Constants.ELECTION_YES)){
				if(Lc.ImmigrationDocumentId== null || Lc.ImmigrationDocumentId.Equals(0)){
					r.Add(t.template.Replace(e,"El tipo de documentaci&oacute;n"));
				}
			}
			if(Lc.LivedCountryId==null||Lc.LivedCountryId.Equals(0)){
				r.Add(t.template.Replace(e,"Si ha vivido en otro pa&iacute;s"));
			}else if(Lc.LivedCountryId!=null && Lc.LivedCountryId.Equals(Constants.ELECTION_YES)){
				if(Lc.TimeResidence==null || Lc.TimeResidence.Trim().Equals("")){
					r.Add(t.template.Replace(e,"El tiempo que vivi&oacute; en otro pa&iacute;s"));
				}
				if(Lc.timeAgo==null || Lc.timeAgo.Trim().Equals("")){
					r.Add(t.template.Replace(e,"Hace cuento tiempo vivi&oacute; en otro pa&iacute;s"));
				}
				if(Lc.Reason==null || Lc.Reason.Trim().Equals("")){
					r.Add(t.template.Replace(e,"La razon por la que dejo de vivir en otro pa&iacute;s"));
				}
				if(Lc.CountryId==null){
					r.Add(t.template.Replace(e,"El pa&iacute;s donde ha vivido"));
				}
				if(Lc.State==null || Lc.State.Trim().Equals("")){
					r.Add(t.template.Replace(e,"El estado donde ha vivido"));
				}
			}
			var social = db.Table<SocialEnvironment>().Where(s=>s.MeetingId==Lc.MeetingId).FirstOrDefault();
			if(social==null||social.comment==null||social.comment.Equals("")){
				r.Add(t.template.Replace(e,"Los comentarios"));
			}
			if(Lc.FamilyAnotherCountryId==null||Lc.FamilyAnotherCountryId.Equals(0)){
				r.Add(t.template.Replace(e,"Si tiene familia en otro pa&iacute;s"));
			}else if(Lc.FamilyAnotherCountryId!=null && Lc.FamilyAnotherCountryId.Equals(Constants.ELECTION_YES)){
				if(Lc.RelationshipId==null || Lc.RelationshipId.Equals(0)){
					r.Add(t.template.Replace(e,"La relaci&oacute;n con el familiar"));
				}
				if(Lc.CommunicationFamilyId== null){
					r.Add(t.template.Replace(e,"Si tiene comunicaci&oacute;n con su familia"));
				}else if (Lc.CommunicationFamilyId!=null && Lc.CommunicationFamilyId.Equals(Constants.ELECTION_YES)){
					if(Lc.Media==null || Lc.Media.Trim().Equals("")){
						r.Add(t.template.Replace(e,"El medio de comunci&oacute;n con su familia"));
					}
				}
			}
			if (r != null && r.Count > 0) {
				t.groupMessage.Add (new GroupMessageMeetingDto ("leavingCountry", r));
			}
			return t;
		}

		public void validateMeeting(TerminateMeetingMessageDto t, Meeting model){
			db.CreateTable<ImputedHome> ();
			var imputedHomeList = db.Table<ImputedHome>().Where(mee => mee.MeetingId == model.Id ).ToList();
			if(imputedHomeList== null || (imputedHomeList !=null && imputedHomeList.Count==0)){
				List<String> result = new List<string>();
				result.Add("Debe registrar al menos un domicilio del imputado.");
				t.groupMessage.Add(new GroupMessageMeetingDto("imputedHome",result));
			}
//			List<PersonSocialNetwork> listPS = getSocialNetwork()==null? new ArrayList<PersonSocialNetwork>() :getSocialNetwork().getPeopleSocialNetwork();
//			db.CreateTable<PersonSocialNetwork> ();
//			var listPS = db.Table<PersonSocialNetwork>().Where(mee => mee.MeetingId == model.Id ).ToList();
//			List<Reference> referenceList = getReferences();
//			db.CreateTable<Reference> ();
//			var referenceList = db.Table<Reference>().Where(mee => mee.MeetingId == model.Id ).ToList();
//			List<String> listMessSN = new List<string>();
//			if ((referenceList==null || (referenceList != null && referenceList.size() == 0))) {
//				List<String> listMess = new List<string>();
//				listMess.Add("Para terminar la entrevista debe agragar al menos una referencia personal.");
//				t.groupMessage.Add(new GroupMessageMeetingDto("reference",listMess));
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
		}

		public String ImputedHomeAddressString(ImputedHome im){
			String result = "";
			if (im.Street != null && !im.Street.Equals("")) {
				result = "Calle: " + im.Street + " No Ext: " + im.OutNum;
			}
			if (im.OutNum != null && !im.OutNum.Equals ("")) {
				result = result + " No Ext: " + im.OutNum;
			}
			if (im.InnNum != null && !im.InnNum.Equals ("")) {
				result = result + " No Int:" + im.InnNum;
			}
			if (im.LocationId != null) {
				var Location = db.Table<Location> ().Where (lo=>lo.Id==im.LocationId).FirstOrDefault ();
				var Municipality = db.Table<Municipality> ().Where (lo=>lo.Id==Location.MunicipalityId).FirstOrDefault ();
				var Estado = db.Table<State> ().Where (lo=>lo.Id==Municipality.StateId).FirstOrDefault ();
				result = result + "," + Location.Name + ". CP: " + Location.ZipCode + ". " + Municipality.Name + ", " + Estado.Name + ".";
			}
			return result;
		}

	}//class end
}