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
				//TODO: insert activities again
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



		[Export("upsertDomicilioComment")]
		public Java.Lang.String upsertDomicilioComment(Java.Lang.String modelJson){
			var output = new Java.Lang.String("");
			Console.WriteLine ("upsertDomicilioComment json model-->"+modelJson);
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

		[Export("upsertReferenciasComment")]
		public Java.Lang.String upsertReferenciasComment(Java.Lang.String modelJson){
			var output = new Java.Lang.String("");
			Console.WriteLine ("upsertDomicilioComment json model-->"+modelJson);
			var model = JsonConvert.DeserializeObject<MeetingDatosPersonalesDto> (modelJson.ToString());
			db.BeginTransaction ();
			try{
				Meeting me = db.Table<Meeting>().Where(mee => mee.Id == model.MeetingId ).FirstOrDefault();
				me.CommentReference = model.CommentReference;
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
				var newSchedules = JsonConvert.DeserializeObject<List<Schedule>>(model.ScheduleSchool);
				foreach(Schedule sch in newSchedules){
					sch.SchoolId = me.Id;
					db.Insert(sch);
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

	}//class end
}