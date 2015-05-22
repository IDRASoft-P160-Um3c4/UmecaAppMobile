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
	public class VerificationService  : Java.Lang.Object
	{

		readonly SQLiteConnection db;
		readonly CatalogServiceController services;

		Context context;

		public VerificationService(Context context)
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

		public VerificationService ()
		{
			db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			services = new CatalogServiceController ();
		}

		[Export("saveFieldVerification")]
		public Java.Lang.String saveFieldVerification(Java.Lang.String val, Java.Lang.String idCase, Java.Lang.String idSource, Java.Lang.String idList){
			var output = new Java.Lang.String("");
			db.BeginTransaction ();
			try{
				var caseId = int.Parse (idCase.ToString ());
				var sourceId = int.Parse (idSource.ToString ());
				var listId = int.Parse (idList.ToString ());
				var model = JsonConvert.DeserializeObject<List<FieldVerified>> (val.ToString());
				var caso = db.Table<Case> ().Where (cs=>cs.Id == caseId).FirstOrDefault ();
				var estatusCaso = db.Table<StatusCase> ().Where (esc=>esc.Id == caso.StatusCaseId).FirstOrDefault ();

				if (estatusCaso == null || estatusCaso.Name != Constants.CASE_STATUS_VERIFICATION) {
					output = new Java.Lang.String("De acuerdo al estado del caso y la verificación no se puede realizar esta acción");
					return output;
				}
				var fuente = db.Table<SourceVerification> ().Where (sv => sv.Id == sourceId).FirstOrDefault ();
				var verifcacion = db.Table<Verification> ().Where (ver => ver.Id == fuente.VerificationId).FirstOrDefault ();
				var estatusVerificacion = db.Table<StatusVerification> ().Where (stv=>stv.Id == verifcacion.StatusVerificationId).FirstOrDefault ();

				if (estatusVerificacion == null || estatusVerificacion.Name != Constants.VERIFICATION_STATUS_AUTHORIZED) {
					output = new Java.Lang.String("De acuerdo al estado del caso y la verificación no se puede realizar esta acción");
					return output;
				}
				if (fuente.DateComplete != null) {
					output = new Java.Lang.String ("No se puede modificar la información de esta fuente");
					return output;
				}

				var estatusCampoVerificacionNotEq = db.Table<StatusFieldVerification> ().Where (ecv => ecv.Name == Constants.ST_FIELD_VERIF_NOEQUALS).FirstOrDefault ();
				if (model!=null&&model.Count > 0) {
					//encuentra el fielVerification by code y borra el valor anterior guardado
					for (var inx=0;inx<model.Count; inx++) {
						var codigo = model[inx].name;
						var key = db.Table<FieldVerification>().Where(ky=>ky.Code==codigo).FirstOrDefault();
						//var fieldVerificationTarget =  db.Table<FieldVerification>().Where (fvt => fvt.Code==field.name.Trim()).FirstOrDefault();
						if(key == null)
							Console.WriteLine("inx->"+inx+" ...model[inx]->"+model[inx]);
						
						var fmsToDelete = new FieldMeetingSource();
						//busca con id list o campo solo
						if (listId != 0) {
							fmsToDelete = db.Table<FieldMeetingSource> ().Where (fms => fms.SourceVerificationId == fuente.Id
								&& fms.FieldVerificationId == key.Id
								&& fms.IdFieldList == listId).FirstOrDefault ();
						} else {
							fmsToDelete = db.Table<FieldMeetingSource> ().Where (fms => fms.SourceVerificationId == fuente.Id
								&& fms.FieldVerificationId == key.Id).FirstOrDefault ();
						}
						if (fmsToDelete != null) {
							db.Delete (fmsToDelete);
						}
					}
					//inserta los nuevos valores
					var insertions = 0;
					foreach (FieldVerified field in model) {
						var codigo = field.name.Trim();
						var fieldVerificationTarget =  db.Table<FieldVerification> ().Where (fv=>fv.Code==codigo).FirstOrDefault ();
						var fmsToInsert = new FieldMeetingSource();
						//crea los nuevos FMS con idlist o solo
						if (listId != 0) {
							fmsToInsert.FieldVerificationId = fieldVerificationTarget.Id;
							fmsToInsert.IdFieldList = listId;
							fmsToInsert.IsFinal = false;
							fmsToInsert.JsonValue = field.value;
							fmsToInsert.SourceVerificationId = fuente.Id;
							fmsToInsert.StatusFieldVerificationId = estatusCampoVerificacionNotEq.Id;
							fmsToInsert.Value = field.name;
							var vars = field.name.Split('.');
							fmsToInsert = setObjectNameOfCatalog(fmsToInsert, vars, field.value, fieldVerificationTarget);
						} else {
							fmsToInsert.FieldVerificationId = fieldVerificationTarget.Id;
							fmsToInsert.IsFinal = false;
							fmsToInsert.JsonValue = field.value;
							fmsToInsert.SourceVerificationId = fuente.Id;
							fmsToInsert.StatusFieldVerificationId = estatusCampoVerificacionNotEq.Id;
							fmsToInsert.Value = field.name;
							var vars = field.name.Split('.');
							fmsToInsert = setObjectNameOfCatalog(fmsToInsert, vars, field.value, fieldVerificationTarget);
						}
						if (fmsToInsert!=null&&!string.IsNullOrEmpty (fmsToInsert.Value)) {
							db.Insert (fmsToInsert);
							insertions++;
						}
					}
					if (insertions <= 0) {
						output = new Java.Lang.String("Ha ocurrido un error al crear la lista.");
						db.Rollback ();
						return output;
					}
				}//end model count
				output = new Java.Lang.String("");
				db.Commit ();
			}catch(Exception e){
				db.Rollback ();
				Console.WriteLine("catched exception in VerificationService method Example invoked javascript calling -> VerificationService.saveFieldVerification() Exception message :::>"+e.Message);
				output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
			}
			finally{
				db.Commit ();
			}
			return output;
		}



		[Export("searchFieldVerification")]
		public Java.Lang.String searchFieldVerification(Java.Lang.String code, Java.Lang.String idCase, Java.Lang.String idSource, Java.Lang.String idList){
			var caseId = int.Parse (idCase.ToString ());
			var sourceId = int.Parse (idSource.ToString ());
			var listId = int.Parse (idList.ToString ());
			var codigo = code.ToString();
			var output = new Java.Lang.String("La fuente no ha proporcionado informaci&oacute;n para &eacute;ste campo");
			String aux = "";
			try{
				//trae la subseccion a la que pertenece
				var IdSubFVTarget = db.Table<FieldVerification> ().Where (fv=>fv.Code==codigo).FirstOrDefault ();
				var fields = db.Table<FieldVerification> ().Where (fv=>fv.IdSubsection == IdSubFVTarget.IdSubsection).ToList ();
				var fmsList = new List<FieldMeetingSource>(); 
				if(fields.Count>0){
					foreach (FieldVerification field in fields) {
						if (listId != 0) {
							var foundFMS = db.Table<FieldMeetingSource> ().Where (fms =>
								fms.FieldVerificationId == field.Id
							               && fms.SourceVerificationId == sourceId
							               && fms.IdFieldList == listId).FirstOrDefault ();
							if (foundFMS.Value.Trim () != "") {
								var estatusVerificacionOfFMS = db.Table<StatusFieldVerification> ().Where (stv => stv.Id == foundFMS.StatusFieldVerificationId).FirstOrDefault ();
								if (estatusVerificacionOfFMS.Name == Constants.ST_FIELD_VERIF_EQUALS) {
									aux += "<i class=\"icon-ok green  icon-only bigger-120\"></i>&nbsp;&nbsp;" + field.FieldName + ": " + foundFMS.Value.Trim () + "<br/>";
								}
								if (estatusVerificacionOfFMS.Name == Constants.ST_FIELD_VERIF_NOEQUALS) {
									aux += "<i class=\"icon-remove red  icon-only bigger-120\"></i>&nbsp;&nbsp;" + field.FieldName + ": " + foundFMS.Value.Trim () + "<br/>";
								}
								if (estatusVerificacionOfFMS.Name == Constants.ST_FIELD_VERIF_DONTKNOW) {
									aux += "<i class=\"icon-ban-circle grey  icon-only bigger-120\"></i>&nbsp;&nbsp;" + field.FieldName + ": " + foundFMS.Value.Trim () + "<br/>";
								}
							}
						} else {
							var foundFMS = db.Table<FieldMeetingSource> ().Where (fms =>
								fms.FieldVerificationId == field.Id
							               && fms.SourceVerificationId == sourceId).FirstOrDefault ();
							if (foundFMS != null && foundFMS.Value.Trim () != "") {
								var estatusVerificacionOfFMS = db.Table<StatusFieldVerification> ().Where (stv => stv.Id == foundFMS.StatusFieldVerificationId).FirstOrDefault ();
								if (estatusVerificacionOfFMS.Name == Constants.ST_FIELD_VERIF_EQUALS) {
									aux += "<i class=\"icon-ok green  icon-only bigger-120\"></i>&nbsp;&nbsp;" + field.FieldName + ": " + foundFMS.Value.Trim () + "<br/>";
								}
								if (estatusVerificacionOfFMS.Name == Constants.ST_FIELD_VERIF_NOEQUALS) {
									aux += "<i class=\"icon-remove red  icon-only bigger-120\"></i>&nbsp;&nbsp;" + field.FieldName + ": " + foundFMS.Value.Trim () + "<br/>";
								}
								if (estatusVerificacionOfFMS.Name == Constants.ST_FIELD_VERIF_DONTKNOW) {
									aux += "<i class=\"icon-ban-circle grey  icon-only bigger-120\"></i>&nbsp;&nbsp;" + field.FieldName + ": " + foundFMS.Value.Trim () + "<br/>";
								}
							}
						}
						if (aux != "") {
							output = new Java.Lang.String (aux);
						}
					}
				}
			}catch(Exception e){
				Console.WriteLine ("catched exception in VerificationService method Example invoked javascript calling -> VerificationService.searchFieldVerification()");
				Console.WriteLine ("Exception message :::>"+e.Message);
				output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
			}
			finally{
				db.Commit ();
			}
			return output;
		}



		private FieldMeetingSource setObjectNameOfCatalog(FieldMeetingSource fms, String[] name, String value, FieldVerification fv) {
			int idCat = 0;
			if (name[name.Length - 1] == "id") {
				idCat = int.Parse(value);
			}
			CatalogDto ca = new CatalogDto();
			switch (fv.Type) {
			case "Country":
				Country c = db.Table<Country>().Where(dis => dis.Id == idCat).FirstOrDefault();
				ca.id = c.Id ;
				ca.name = c.Name;
				fms.Value = c.Name;
				fms.JsonValue = JsonConvert.SerializeObject(ca);
				break;
			case "ImmigrationDocument":
				ImmigrationDocument immd =db.Table<ImmigrationDocument>().Where(dis => dis.Id == idCat).FirstOrDefault();
				ca.id = immd.Id ;
				ca.name = immd.Name;
				fms.Value = immd.Name;
				fms.JsonValue = JsonConvert.SerializeObject(ca);
				break;
			case "MaritalStatus":
				MaritalStatus m = db.Table<MaritalStatus>().Where(dis => dis.Id == idCat).FirstOrDefault();
				ca.id = m.Id ;
				ca.name = m.Name;
				fms.Value = m.Name;
				fms.JsonValue = JsonConvert.SerializeObject(ca);
				break;
			case "Election":
				Election e = db.Table<Election>().Where(dis => dis.Id == idCat).FirstOrDefault();
				fms.Value = e.Name;
				CatalogDto cdto = new CatalogDto();
				cdto.id = e.Id ;
				cdto.name = e.Name;
				fms.JsonValue = JsonConvert.SerializeObject(cdto);
				break;
			case "RegisterType":
				RegisterType r = db.Table<RegisterType>().Where(dis => dis.Id == idCat).FirstOrDefault();
				ca.id = r.Id ;
				ca.name = r.Name;
				fms.Value = r.Name;
				fms.JsonValue = JsonConvert.SerializeObject(ca);
				break;
			case "Relationship":
				Relationship rel = db.Table<Relationship>().Where(dis => dis.Id == idCat).FirstOrDefault();
				ca.name = rel.Name;
				ca.id = rel.Id ;
				fms.Value = rel.Name;
				fms.JsonValue = JsonConvert.SerializeObject(ca);
				break;
			case "DocumentType":
				DocumentType doc = db.Table<DocumentType>().Where(dis => dis.Id == idCat).FirstOrDefault();
				ca.id = doc.Id ;
				ca.name = doc.Name;
				fms.Value = doc.Name;
				fms.JsonValue = JsonConvert.SerializeObject(ca);
				break;
			case "DrugType":
				DrugType dt = db.Table<DrugType>().Where(dis => dis.Id == idCat).FirstOrDefault();
				ca.name = dt.Name;
				ca.id = dt.Id ;
				fms.Value = dt.Name;
				fms.JsonValue = JsonConvert.SerializeObject(ca);
				break;
			case "Periodicity":
				Periodicity p = db.Table<Periodicity>().Where(dis => dis.Id == idCat).FirstOrDefault();
				ca.name = p.Name;
				ca.id = p.Id ;
				fms.Value = p.Name;
				fms.JsonValue = JsonConvert.SerializeObject(ca);
				break;
			case "Degree":
				Degree degree = db.Table<Degree>().Where(dis => dis.Id == idCat).FirstOrDefault();
				AcademicLevel academiclevel = db.Table<AcademicLevel>().Where(dis => dis.Id == degree.AcademicLevelId).FirstOrDefault();
				fms.Value = "Nivel: " + academiclevel.Name + " Grado: " + degree.Name;
				fms.JsonValue = JsonConvert.SerializeObject(new DegreeDto().dtoGrade(degree.Id, degree.Name));
				break;
			case "Date":
				String valueString = "", valueJson = "";
				try {
					DateTime myDate = DateTime.ParseExact(value, "yyyy/MM/dd",
						System.Globalization.CultureInfo.InvariantCulture);
					valueString = String.Format("{0:yyyy/MM/dd}", myDate);
					valueJson = myDate.ToString();
				} catch (Exception ex) {
					Console.WriteLine ("error al asignar value en setObjectNameOfCatalog error");
					Console.WriteLine ("Mesage => "+ex.Message);
					valueJson = "Error en conversión";
					valueString = value;
				} finally {
					fms.Value = valueString;
					fms.JsonValue = valueJson;
				}
				break;
			case "BooleanG":
				Boolean gender = value == "0";
				String genderString;
				if (gender == Constants.GENDER_FEMALE)
					genderString = "Femenino";
				else
					genderString = "Masculino";
				fms.Value = genderString;
				fms.JsonValue = value;
				break;
			case "Boolean":
				String acString = value == "0" ? "No" : "Si";
				fms.Value = acString;
				fms.JsonValue = value;
				break;
			case "Activity":
				try {
					List<RelActivity> relSE = JsonConvert.DeserializeObject<List<RelActivity>> (value.ToString());
					fms.JsonValue = value;
					String val = "";
					if (relSE != null) {
						foreach (RelActivity re in relSE) {
							var activityName = db.Table<ActivityCatalog>().Where(act=>act.Id == re.ActivityId).FirstOrDefault();
							val = val + activityName.Name;
							if (!string.IsNullOrEmpty (re.specification)) {
								val = val + ": " + re.specification + "; ";
							} else {
								val = val + "; ";
							}
						}
					}
					fms.Value = val;
				} catch (Exception ex) {
					Console.WriteLine(ex.Message);
				}

				break;
			case "HomeType":
				HomeType ht =  db.Table<HomeType>().Where(dis => dis.Id == idCat).FirstOrDefault();
				ca.name = ht.Name;
				ca.id = ht.Id ;
				fms.Value = ht.Name;
				fms.JsonValue = JsonConvert.SerializeObject(ca);
				break;
			case "Location":
				if (idCat != 0) {
					Location l = db.Table<Location> ().Where (loc => loc.Id == idCat).FirstOrDefault ();
					Municipality mu = db.Table<Municipality> ().Where (mun => mun.Id == l.MunicipalityId).FirstOrDefault ();
					State stait = db.Table<State> ().Where (mun => mun.Id == mu.StateId).FirstOrDefault ();
					ca.name = l.Name;
					ca.id = l.Id;
					fms.Value = "Estado: " + stait.Name + ", Municipio; " + mu.Name + ", Localidad: " + l.Name + ".";
					fms.JsonValue = JsonConvert.SerializeObject (ca);
				} else {
					return null;
				}
				break;
			default:
				fms.Value = value;
				fms.JsonValue = value;
				break;


			}
			return fms;

		}




		[Export("actividadesImputado")]
		public Java.Lang.String actividadesImputado(){
			var actividades = db.Table<ActivityCatalog> ().OrderBy (c=>c.Name).ToList ()??new List<ActivityCatalog> ();
			return new Java.Lang.String(JsonConvert.SerializeObject(actividades));
		}

	}//class end
}