using System;

//insert with children etc

//query to list
using System.Linq;
//listas
using System.Collections.Generic;

using Java.Interop;
using Newtonsoft.Json;
using Android.Content;

using SQLite;
using Umeca.Data;

namespace UmecaApp
{
	public class VerificationService  : Java.Lang.Object
	{

		readonly CatalogServiceController services;

		Context context;

		public VerificationService(Context context)
		{
			this.context = context;
			services = new CatalogServiceController ();
		}

		public VerificationService ()
		{
			services = new CatalogServiceController ();
		}

		[Export("saveFieldVerification")]
		public Java.Lang.String saveFieldVerification(Java.Lang.String val, Java.Lang.String idCase, Java.Lang.String idSource, Java.Lang.String idList){
			var output = new Java.Lang.String("");
			using (var db = FactoryConn.GetConn ()) {
				db.BeginTransaction ();
				try {
					var caseId = int.Parse (idCase.ToString ());
					var sourceId = int.Parse (idSource.ToString ());
					var listId = int.Parse (idList.ToString ());
					var model = JsonConvert.DeserializeObject<List<FieldVerified>> (val.ToString ());
					var caso = db.Table<Case> ().Where (cs => cs.Id == caseId).FirstOrDefault ();
					var estatusCaso = db.Table<StatusCase> ().Where (esc => esc.Id == caso.StatusCaseId).FirstOrDefault ();

					if (estatusCaso == null || estatusCaso.Name != Constants.CASE_STATUS_VERIFICATION) {
						output = new Java.Lang.String ("De acuerdo al estado del caso y la verificación no se puede realizar esta acción");
						return output;
					}
					var fuente = db.Table<SourceVerification> ().Where (sv => sv.Id == sourceId).FirstOrDefault ();
					var verifcacion = db.Table<Verification> ().Where (ver => ver.Id == fuente.VerificationId).FirstOrDefault ();
					var estatusVerificacion = db.Table<StatusVerification> ().Where (stv => stv.Id == verifcacion.StatusVerificationId).FirstOrDefault ();

					if (estatusVerificacion == null || estatusVerificacion.Name != Constants.VERIFICATION_STATUS_AUTHORIZED) {
						output = new Java.Lang.String ("De acuerdo al estado del caso y la verificación no se puede realizar esta acción");
						return output;
					}
					if (fuente.DateComplete != null) {
						output = new Java.Lang.String ("No se puede modificar la información de esta fuente");
						return output;
					}

					var estatusCampoVerificacionNotEq = db.Table<StatusFieldVerification> ().Where (ecv => ecv.Name == Constants.ST_FIELD_VERIF_NOEQUALS).FirstOrDefault ();
					if (model != null && model.Count > 0) {
						//encuentra el fielVerification by code y borra el valor anterior guardado
						for (var inx = 0; inx < model.Count; inx++) {
							var codigo = model [inx].name;
							var key = db.Table<FieldVerification> ().Where (ky => ky.Code == codigo).FirstOrDefault ();
							//var fieldVerificationTarget =  db.Table<FieldVerification>().Where (fvt => fvt.Code==field.name.Trim()).FirstOrDefault();
//						if(key == null)
//							Console.WriteLine("no se encontro el key ----- inx->"+inx+" ...model[inx]->"+model[inx]);
						
							var fmsToDelete = new List<FieldMeetingSource> ();
							//busca con id list o campo solo
							if (listId != 0) {
								fmsToDelete = db.Table<FieldMeetingSource> ().Where (fms => fms.SourceVerificationId == fuente.Id
								&& fms.FieldVerificationId == key.Id
								&& fms.IdFieldList == listId).ToList ();
							} else {
								fmsToDelete = db.Table<FieldMeetingSource> ().Where (fms => fms.SourceVerificationId == fuente.Id
								&& fms.FieldVerificationId == key.Id).ToList ();
							}
							if (fmsToDelete != null && fmsToDelete.Count > 0) {
								foreach (FieldMeetingSource fmtd in fmsToDelete) {
									db.Delete (fmtd);
								}
							}
						}
						//inserta los nuevos valores
						var insertions = 0;
						foreach (FieldVerified field in model) {
							var codigo = field.name.Trim ();
							var fieldVerificationTarget = db.Table<FieldVerification> ().Where (fv => fv.Code == codigo).FirstOrDefault ();
							var fmsToInsert = new FieldMeetingSource ();
							//crea los nuevos FMS con idlist o solo
							if (listId != 0) {
								fmsToInsert.FieldVerificationId = fieldVerificationTarget.Id;
								fmsToInsert.IdFieldList = listId;
								fmsToInsert.IsFinal = false;
								fmsToInsert.JsonValue = field.value;
								fmsToInsert.SourceVerificationId = fuente.Id;
								fmsToInsert.StatusFieldVerificationId = estatusCampoVerificacionNotEq.Id;
								fmsToInsert.Value = field.name;
								var vars = field.name.Split ('.');
								fmsToInsert = setObjectNameOfCatalog (fmsToInsert, vars, field.value, fieldVerificationTarget);
							} else {
								fmsToInsert.FieldVerificationId = fieldVerificationTarget.Id;
								fmsToInsert.IsFinal = false;
								fmsToInsert.JsonValue = field.value;
								fmsToInsert.SourceVerificationId = fuente.Id;
								fmsToInsert.StatusFieldVerificationId = estatusCampoVerificacionNotEq.Id;
								fmsToInsert.Value = field.name;
								var vars = field.name.Split ('.');
								fmsToInsert = setObjectNameOfCatalog (fmsToInsert, vars, field.value, fieldVerificationTarget);
							}
							if (fmsToInsert != null && !string.IsNullOrEmpty (fmsToInsert.Value)) {
								db.Insert (fmsToInsert);
								insertions++;
							}
						}
						if (insertions <= 0) { 
							output = new Java.Lang.String ("Ha ocurrido un error al crear la lista.");
							db.Rollback ();
							return output;
						}
					}//end model count
					output = new Java.Lang.String ("");
					db.Commit ();
				} catch (Exception e) {
					db.Rollback ();
					Console.WriteLine ("catched exception in VerificationService method Example invoked javascript calling -> VerificationService.saveFieldVerification() Exception message :::>" + e.Message);
					output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
				} finally {
					db.Commit ();
				}
				db.Close ();
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
			using (var db = FactoryConn.GetConn ()) {
				try {
					//trae la subseccion a la que pertenece
					var IdSubFVTarget = db.Table<FieldVerification> ().Where (fv => fv.Code == codigo).FirstOrDefault ();
					var fields = db.Table<FieldVerification> ().Where (fv => fv.IdSubsection == IdSubFVTarget.IdSubsection).ToList ();
					var fmsList = new List<FieldMeetingSource> (); 
					if (fields.Count > 0) {
						foreach (FieldVerification field in fields) {
							if (listId != 0) {
								var foundFMS = db.Table<FieldMeetingSource> ().Where (fms =>
								fms.FieldVerificationId == field.Id
								              && fms.SourceVerificationId == sourceId
								              && fms.IdFieldList == listId).FirstOrDefault ();
								if (foundFMS != null && foundFMS.Value != "") {
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
								if (foundFMS != null && foundFMS.Value != "") {
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
				} catch (Exception e) {
					Console.WriteLine ("catched exception in VerificationService method Example invoked javascript calling -> VerificationService.searchFieldVerification()");
					Console.WriteLine ("Exception message :::>" + e.Message);
					output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
				} finally {
					db.Commit ();
				}
				db.Close ();
			}
			return output;
		}



		private FieldMeetingSource setObjectNameOfCatalog(FieldMeetingSource fms, String[] name, String value, FieldVerification fv) {
			int idCat = 0;
			if (name[name.Length - 1] == "id") {
				idCat = int.Parse(value);
			}
			CatalogDto ca = new CatalogDto();

			using (var db = FactoryConn.GetConn ()) {
				switch (fv.Type) {
				case "Country":
					Country c = db.Table<Country> ().Where (dis => dis.Id == idCat).FirstOrDefault ();
					ca.id = c.Id;
					ca.name = c.Name;
					fms.Value = c.Name;
					fms.JsonValue = JsonConvert.SerializeObject (ca);
					break;
				case "ImmigrationDocument":
					ImmigrationDocument immd = db.Table<ImmigrationDocument> ().Where (dis => dis.Id == idCat).FirstOrDefault ();
					ca.id = immd.Id;
					ca.name = immd.Name;
					fms.Value = immd.Name;
					fms.JsonValue = JsonConvert.SerializeObject (ca);
					break;
				case "MaritalStatus":
					MaritalStatus m = db.Table<MaritalStatus> ().Where (dis => dis.Id == idCat).FirstOrDefault ();
					ca.id = m.Id;
					ca.name = m.Name;
					fms.Value = m.Name;
					fms.JsonValue = JsonConvert.SerializeObject (ca);
					break;
				case "Election":
					Election e = db.Table<Election> ().Where (dis => dis.Id == idCat).FirstOrDefault ();
					fms.Value = e.Name;
					CatalogDto cdto = new CatalogDto ();
					cdto.id = e.Id;
					cdto.name = e.Name;
					fms.JsonValue = JsonConvert.SerializeObject (cdto);
					break;
				case "RegisterType":
					RegisterType r = db.Table<RegisterType> ().Where (dis => dis.Id == idCat).FirstOrDefault ();
					ca.id = r.Id;
					ca.name = r.Name;
					fms.Value = r.Name;
					fms.JsonValue = JsonConvert.SerializeObject (ca);
					break;
				case "Relationship":
					Relationship rel = db.Table<Relationship> ().Where (dis => dis.Id == idCat).FirstOrDefault ();
					ca.name = rel.Name;
					ca.id = rel.Id;
					fms.Value = rel.Name;
					fms.JsonValue = JsonConvert.SerializeObject (ca);
					break;
				case "DocumentType":
					DocumentType doc = db.Table<DocumentType> ().Where (dis => dis.Id == idCat).FirstOrDefault ();
					ca.id = doc.Id;
					ca.name = doc.Name;
					fms.Value = doc.Name;
					fms.JsonValue = JsonConvert.SerializeObject (ca);
					break;
				case "DrugType":
					DrugType dt = db.Table<DrugType> ().Where (dis => dis.Id == idCat).FirstOrDefault ();
					ca.name = dt.Name;
					ca.id = dt.Id;
					fms.Value = dt.Name;
					fms.JsonValue = JsonConvert.SerializeObject (ca);
					break;
				case "Periodicity":
					Periodicity p = db.Table<Periodicity> ().Where (dis => dis.Id == idCat).FirstOrDefault ();
					ca.name = p.Name;
					ca.id = p.Id;
					fms.Value = p.Name;
					fms.JsonValue = JsonConvert.SerializeObject (ca);
					break;
				case "Degree":
					Degree degree = db.Table<Degree> ().Where (dis => dis.Id == idCat).FirstOrDefault ();
					AcademicLevel academiclevel = db.Table<AcademicLevel> ().Where (dis => dis.Id == degree.AcademicLevelId).FirstOrDefault ();
					fms.Value = "Nivel: " + academiclevel.Name + " Grado: " + degree.Name;
					fms.JsonValue = JsonConvert.SerializeObject (new DegreeDto ().dtoGrade (degree.Id, degree.Name));
					break;
				case "Date":
					String valueString = "", valueJson = "";
					try {
						DateTime myDate = DateTime.ParseExact (value, "yyyy/MM/dd",
							                 System.Globalization.CultureInfo.InvariantCulture);
						valueString = String.Format ("{0:yyyy/MM/dd}", myDate);
						valueJson = myDate.ToString ();
					} catch (Exception ex) {
						Console.WriteLine ("error al asignar value en setObjectNameOfCatalog error");
						Console.WriteLine ("Mesage => " + ex.Message);
						valueJson = "Error en conversión";
						valueString = value;
					} finally {
						fms.Value = valueString;
						fms.JsonValue = valueJson;
					}
					break;
				case "BooleanG":
					Boolean gender = false;
					if (value == "true") {
						gender = true;
					} else if (value == "false") {
						gender = false;
					} else {
						gender = value == "0";
					}
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
						List<RelActivity> relSE = JsonConvert.DeserializeObject<List<RelActivity>> (value.ToString ());
						fms.JsonValue = value;
						String val = "";
						if (relSE != null) {
							foreach (RelActivity re in relSE) {
								var activityName = db.Table<ActivityCatalog> ().Where (act => act.Id == re.ActivityId).FirstOrDefault ();
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
						Console.WriteLine (ex.Message);
					}

					break;
				case "HomeType":
					HomeType ht = db.Table<HomeType> ().Where (dis => dis.Id == idCat).FirstOrDefault ();
					ca.name = ht.Name;
					ca.id = ht.Id;
					fms.Value = ht.Name;
					fms.JsonValue = JsonConvert.SerializeObject (ca);
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
				db.Close ();
			}
			return fms;

		}




		[Export("actividadesImputado")]
		public Java.Lang.String actividadesImputado(){
			var actividades = new List<ActivityCatalog>();
			using (var db = FactoryConn.GetConn ()) {
				actividades = db.Table<ActivityCatalog> ().OrderBy (c=>c.Name).ToList ()??new List<ActivityCatalog> ();
				db.Close();
			}
			return new Java.Lang.String(JsonConvert.SerializeObject(actividades));
		}


		[Export("saveActivitiesVerification")]
		public Java.Lang.String saveActivitiesVerification(Java.Lang.String val, Java.Lang.String jsonVal, Java.Lang.String idCase, Java.Lang.String idSource){
			var output = new Java.Lang.String("");
			using (var db = FactoryConn.GetConn ()) {
				db.BeginTransaction ();
				try {
					var caseId = int.Parse (idCase.ToString ());
					var sourceId = int.Parse (idSource.ToString ());
					var caso = db.Table<Case> ().Where (cs => cs.Id == caseId).FirstOrDefault ();
					var estatusCaso = db.Table<StatusCase> ().Where (esc => esc.Id == caso.StatusCaseId).FirstOrDefault ();

					if (estatusCaso == null || estatusCaso.Name != Constants.CASE_STATUS_VERIFICATION) {
						output = new Java.Lang.String ("De acuerdo al estado del caso y la verificación no se puede realizar esta acción");
						return output;
					}
					var fuente = db.Table<SourceVerification> ().Where (sv => sv.Id == sourceId).FirstOrDefault ();
					var verifcacion = db.Table<Verification> ().Where (ver => ver.Id == fuente.VerificationId).FirstOrDefault ();
					var estatusVerificacion = db.Table<StatusVerification> ().Where (stv => stv.Id == verifcacion.StatusVerificationId).FirstOrDefault ();

					if (estatusVerificacion == null || estatusVerificacion.Name != Constants.VERIFICATION_STATUS_AUTHORIZED) {
						output = new Java.Lang.String ("De acuerdo al estado del caso y la verificación no se puede realizar esta acción");
						return output;
					}
					if (fuente.DateComplete != null) {
						output = new Java.Lang.String ("No se puede modificar la información de esta fuente");
						return output;
					}

					var estatusCampoVerificacionNotEq = db.Table<StatusFieldVerification> ().Where (ecv => ecv.Name == Constants.ST_FIELD_VERIF_NOEQUALS).FirstOrDefault ();

					var key = db.Table<FieldVerification> ().Where (ky => ky.Code == "socialEnvironment.activities").FirstOrDefault ();
					var fmsToDelete = new FieldMeetingSource ();
					fmsToDelete = db.Table<FieldMeetingSource> ().Where (fms => fms.SourceVerificationId == fuente.Id
					&& fms.FieldVerificationId == key.Id).FirstOrDefault ();
					if (fmsToDelete != null) {
						db.Delete (fmsToDelete);
					}

					if (val.ToString () == "" || jsonVal.ToString () == "") {
						output = new Java.Lang.String ("Ha ocurrido un error al crear la lista.");
						db.Rollback ();
						return output;
					}
					//inserta los nuevos valores
					var fmsToInsert = new FieldMeetingSource ();
					fmsToInsert.FieldVerificationId = key.Id;
					fmsToInsert.IsFinal = false;
					fmsToInsert.JsonValue = jsonVal.ToString ();
					fmsToInsert.SourceVerificationId = fuente.Id;
					fmsToInsert.StatusFieldVerificationId = estatusCampoVerificacionNotEq.Id;
					fmsToInsert.Value = val.ToString ();
					db.Insert (fmsToInsert);
					if (fmsToInsert == null || fmsToInsert.Id == 0) {
						output = new Java.Lang.String ("Ha ocurrido un error al crear la lista.");
						db.Rollback ();
						return output;
					}
					output = new Java.Lang.String ("");
				} catch (Exception e) {
					db.Rollback ();
					Console.WriteLine ("catched exception in VerificationService method Example invoked javascript calling -> VerificationService.saveActivitiesVerification() Exception message :::>" + e.Message);
					output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
				} finally {
					db.Commit ();
				}
				db.Close ();
			}
			return output;
		}


		[Export("saveScheduleVerification")]
		public Java.Lang.String saveScheduleVerification(Java.Lang.String val, Java.Lang.String jsonVal, Java.Lang.String idCase, Java.Lang.String idSource, Java.Lang.String codigo, Java.Lang.String idList){
			var output = new Java.Lang.String("");
			using (var db = FactoryConn.GetConn ()) {
				db.BeginTransaction ();
				try {
					var caseId = int.Parse (idCase.ToString ());
					var sourceId = int.Parse (idSource.ToString ());
					var caso = db.Table<Case> ().Where (cs => cs.Id == caseId).FirstOrDefault ();
					var estatusCaso = db.Table<StatusCase> ().Where (esc => esc.Id == caso.StatusCaseId).FirstOrDefault ();
					var listId = int.Parse (idList.ToString ());
					var coding = codigo.ToString ();

					if (estatusCaso == null || estatusCaso.Name != Constants.CASE_STATUS_VERIFICATION) {
						output = new Java.Lang.String ("De acuerdo al estado del caso y la verificación no se puede realizar esta acción");
						return output;
					}
					var fuente = db.Table<SourceVerification> ().Where (sv => sv.Id == sourceId).FirstOrDefault ();
					var verifcacion = db.Table<Verification> ().Where (ver => ver.Id == fuente.VerificationId).FirstOrDefault ();
					var estatusVerificacion = db.Table<StatusVerification> ().Where (stv => stv.Id == verifcacion.StatusVerificationId).FirstOrDefault ();

					if (estatusVerificacion == null || estatusVerificacion.Name != Constants.VERIFICATION_STATUS_AUTHORIZED) {
						output = new Java.Lang.String ("De acuerdo al estado del caso y la verificación no se puede realizar esta acción");
						return output;
					}
					if (fuente.DateComplete != null) {
						output = new Java.Lang.String ("No se puede modificar la información de esta fuente");
						return output;
					}

					var estatusCampoVerificacionNotEq = db.Table<StatusFieldVerification> ().Where (ecv => ecv.Name == Constants.ST_FIELD_VERIF_NOEQUALS).FirstOrDefault ();

					var key = db.Table<FieldVerification> ().Where (ky => ky.Code == coding).FirstOrDefault ();
					var fmsToDelete = new FieldMeetingSource ();
					if (listId == 0) {
						fmsToDelete = db.Table<FieldMeetingSource> ().Where (fms => fms.SourceVerificationId == fuente.Id
						&& fms.FieldVerificationId == key.Id).FirstOrDefault ();
					} else {
						fmsToDelete = db.Table<FieldMeetingSource> ().Where (fms => fms.SourceVerificationId == fuente.Id
						&& fms.FieldVerificationId == key.Id
						&& fms.IdFieldList == listId).FirstOrDefault ();
					}
					if (fmsToDelete != null) {
						db.Delete (fmsToDelete);
					}

					if (val.ToString () == "" || jsonVal.ToString () == "") {
						output = new Java.Lang.String ("Ha ocurrido un error al crear la lista.");
						db.Rollback ();
						return output;
					}
					//inserta los nuevos valores
					var fmsToInsert = new FieldMeetingSource ();
					fmsToInsert.FieldVerificationId = key.Id;
					fmsToInsert.IsFinal = false;
					fmsToInsert.JsonValue = jsonVal.ToString ();
					fmsToInsert.SourceVerificationId = fuente.Id;
					fmsToInsert.StatusFieldVerificationId = estatusCampoVerificacionNotEq.Id;
					fmsToInsert.Value = val.ToString ();
					if (listId != 0) {
						fmsToInsert.IdFieldList = listId;
					}
					db.Insert (fmsToInsert);
					if (fmsToInsert == null || fmsToInsert.Id == 0) {
						output = new Java.Lang.String ("Ha ocurrido un error al crear la lista.");
						db.Rollback ();
						return output;
					}
					output = new Java.Lang.String ("");
				} catch (Exception e) {
					db.Rollback ();
					Console.WriteLine ("catched exception in VerificationService method Example invoked javascript calling -> VerificationService.saveScheduleVerification() Exception message :::>" + e.Message);
					output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
				} finally {
					db.Commit ();
				}
				db.Close ();
			}
			return output;
		}






























		[Export("saveFieldDontKnow")]
		public Java.Lang.String saveFieldDontKnow(Java.Lang.String val, Java.Lang.String idCase, Java.Lang.String idSource, Java.Lang.String idList){
			var output = new Java.Lang.String("");
			using (var db = FactoryConn.GetConn ()) {
				db.BeginTransaction ();
				try {
					var caseId = int.Parse (idCase.ToString ());
					var sourceId = int.Parse (idSource.ToString ());
					var listId = int.Parse (idList.ToString ());
					var model = val.ToString ().Split (',');
					var caso = db.Table<Case> ().Where (cs => cs.Id == caseId).FirstOrDefault ();
					var estatusCaso = db.Table<StatusCase> ().Where (esc => esc.Id == caso.StatusCaseId).FirstOrDefault ();

					if (estatusCaso == null || estatusCaso.Name != Constants.CASE_STATUS_VERIFICATION) {
						output = new Java.Lang.String ("De acuerdo al estado del caso y la verificación no se puede realizar esta acción");
						return output;
					}
					var fuente = db.Table<SourceVerification> ().Where (sv => sv.Id == sourceId).FirstOrDefault ();
					var verifcacion = db.Table<Verification> ().Where (ver => ver.Id == fuente.VerificationId).FirstOrDefault ();
					var estatusVerificacion = db.Table<StatusVerification> ().Where (stv => stv.Id == verifcacion.StatusVerificationId).FirstOrDefault ();

					if (estatusVerificacion == null || estatusVerificacion.Name != Constants.VERIFICATION_STATUS_AUTHORIZED) {
						output = new Java.Lang.String ("De acuerdo al estado del caso y la verificación no se puede realizar esta acción");
						return output;
					}
					if (fuente.DateComplete != null) {
						output = new Java.Lang.String ("No se puede modificar la información de esta fuente");
						return output;
					}

					var estatusCampoVerificacionDontKnow = db.Table<StatusFieldVerification> ().Where (ecv => ecv.Name == Constants.ST_FIELD_VERIF_DONTKNOW).FirstOrDefault ();
					if (model != null && model.Length > 0) {
						//encuentra el fielVerification by code y borra el valor anterior guardado
						for (var inx = 0; inx < model.Length; inx++) {
							var codigo = model [inx];
							var key = db.Table<FieldVerification> ().Where (ky => ky.Code == codigo).FirstOrDefault ();
							//var fieldVerificationTarget =  db.Table<FieldVerification>().Where (fvt => fvt.Code==field.name.Trim()).FirstOrDefault();
//						if(key == null)
//							Console.WriteLine("inx->"+inx+" ...model[inx]->"+model[inx]);

							var fmsToDelete = new FieldMeetingSource ();
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
						foreach (String field in model) {
							var codigo = field.Trim ();
							var fieldVerificationTarget = db.Table<FieldVerification> ().Where (fv => fv.Code == codigo).FirstOrDefault ();
							var fmsToInsert = new FieldMeetingSource ();
							//crea los nuevos FMS con idlist o solo
							if (listId != 0) {
								fmsToInsert.FieldVerificationId = fieldVerificationTarget.Id;
								fmsToInsert.IdFieldList = listId;
								fmsToInsert.IsFinal = false;
								fmsToInsert.JsonValue = Constants.VALUE_NOT_KNOW_SOURCE;
								fmsToInsert.SourceVerificationId = fuente.Id;
								fmsToInsert.StatusFieldVerificationId = estatusCampoVerificacionDontKnow.Id;
								fmsToInsert.Value = Constants.VALUE_NOT_KNOW_SOURCE;
							} else {
								fmsToInsert.FieldVerificationId = fieldVerificationTarget.Id;
								fmsToInsert.IsFinal = false;
								fmsToInsert.JsonValue = Constants.VALUE_NOT_KNOW_SOURCE;
								fmsToInsert.SourceVerificationId = fuente.Id;
								fmsToInsert.StatusFieldVerificationId = estatusCampoVerificacionDontKnow.Id;
								fmsToInsert.Value = Constants.VALUE_NOT_KNOW_SOURCE;
							}
							if (fmsToInsert != null && !string.IsNullOrEmpty (fmsToInsert.Value)) {
								db.Insert (fmsToInsert);
								insertions++;
							}
						}
						if (insertions <= 0) {
							output = new Java.Lang.String ("Ha ocurrido un error al crear la lista.");
							db.Rollback ();
							return output;
						}
					}//end model count
					output = new Java.Lang.String ("");
					db.Commit ();
				} catch (Exception e) {
					db.Rollback ();
					Console.WriteLine ("catched exception in VerificationService method Example invoked javascript calling -> VerificationService.saveFieldDontKnow()");
					Console.WriteLine ("Exception message :::>" + e.Message);
					output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
				} finally {
					db.Commit ();
				}
				db.Close ();
			}
			return output;
		}





		[Export("saveFieldVerifiedEqual")]
		public Java.Lang.String saveFieldVerifiedEqual(Java.Lang.String val, Java.Lang.String idCase, Java.Lang.String idSource, Java.Lang.String idList){
			var output = new Java.Lang.String("");
			using (var db = FactoryConn.GetConn ()) {
				db.BeginTransaction ();
				try{
					var caseId = int.Parse (idCase.ToString ());
					var sourceId = int.Parse (idSource.ToString ());
					var listId = int.Parse (idList.ToString ());
					var model = val.ToString().Split(',');
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

					var estatusCampoVerificacionEq = db.Table<StatusFieldVerification> ().Where (ecv => ecv.Name == Constants.ST_FIELD_VERIF_EQUALS).FirstOrDefault ();
					if (model!=null&&model.Length > 0) {
						//encuentra el fielVerification by code y borra el valor anterior guardado
						for (var inx=0;inx<model.Length; inx++) {
							var codigo = model[inx];
							var key = db.Table<FieldVerification>().Where(ky=>ky.Code==codigo).FirstOrDefault();
							//var fieldVerificationTarget =  db.Table<FieldVerification>().Where (fvt => fvt.Code==field.name.Trim()).FirstOrDefault();
	//						if(key == null)
	//							Console.WriteLine("inx->"+inx+" ...model[inx]->"+model[inx]);
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
						foreach (String field in model) {
							var codigo = field.Trim();
							var fieldVerificationTarget =  db.Table<FieldVerification> ().Where (fv=>fv.Code==codigo).FirstOrDefault ();
							var fmsToInsert = new FieldMeetingSource();
							var FMSimputado = new FieldMeetingSource();
							var sourceImputado = db.Table<SourceVerification>().Where(soVe=>soVe.VerificationId==verifcacion.Id&&soVe.Visible==false).FirstOrDefault();



							if(sourceImputado==null){
								fmsToInsert.JsonValue = " \t\n\t ";
								fmsToInsert.Value = " \t\n\t ";
							}else if(listId != 0 && sourceImputado!=null){
								FMSimputado = db.Table<FieldMeetingSource>().Where(fmsi=>fmsi.FieldVerificationId==fieldVerificationTarget.Id
									&& fmsi.SourceVerificationId == sourceImputado.Id
									&& fmsi.IdFieldList == listId
								).FirstOrDefault();
								if(FMSimputado!=null){
									fmsToInsert.JsonValue = FMSimputado.JsonValue;
									fmsToInsert.Value = FMSimputado.Value;
								}
							}else if(sourceImputado!=null){
								FMSimputado = db.Table<FieldMeetingSource>().Where(fmsi=>fmsi.FieldVerificationId==fieldVerificationTarget.Id
									&& fmsi.SourceVerificationId == sourceImputado.Id).FirstOrDefault();
								if(FMSimputado!=null){
									fmsToInsert.JsonValue = FMSimputado.JsonValue;
									fmsToInsert.Value = FMSimputado.Value;
								}
							}


							//crea los nuevos FMS con idlist o solo
							if (listId != 0) {
								fmsToInsert.FieldVerificationId = fieldVerificationTarget.Id;
								fmsToInsert.IdFieldList = listId;
								fmsToInsert.IsFinal = false;
								fmsToInsert.SourceVerificationId = fuente.Id;
								fmsToInsert.StatusFieldVerificationId = estatusCampoVerificacionEq.Id;
							} else {
								fmsToInsert.FieldVerificationId = fieldVerificationTarget.Id;
								fmsToInsert.IsFinal = false;
								fmsToInsert.SourceVerificationId = fuente.Id;
								fmsToInsert.StatusFieldVerificationId = estatusCampoVerificacionEq.Id;
							}
							if (fmsToInsert!=null
	//							&&!string.IsNullOrEmpty (fmsToInsert.Value)
							) {	
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
					Console.WriteLine("catched exception in VerificationService method Example invoked javascript calling -> VerificationService.saveFieldDontKnow()");
					Console.WriteLine("Exception message :::>"+e.Message);
					output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
				}
				finally{
					db.Commit ();
				}
				db.Close ();
				return output;
			}
		}

		[Export("terminateMeetingSource")]
		public Java.Lang.String terminateMeetingSource(Java.Lang.String idCase, Java.Lang.String idSource){
			var output = new Java.Lang.String("");
			using (var db = FactoryConn.GetConn ()) {
				db.BeginTransaction ();
				try {
					var caseId = int.Parse (idCase.ToString ());
					var sourceId = int.Parse (idSource.ToString ());
					var caso = db.Table<Case> ().Where (cs => cs.Id == caseId).FirstOrDefault ();
					var estatusCaso = db.Table<StatusCase> ().Where (esc => esc.Id == caso.StatusCaseId).FirstOrDefault ();

					if (estatusCaso == null || estatusCaso.Name != Constants.CASE_STATUS_VERIFICATION) {
						output = new Java.Lang.String ("De acuerdo al estado del caso y la verificación no se puede realizar esta acción");
						return output;
					}
					var fuente = db.Table<SourceVerification> ().Where (sv => sv.Id == sourceId).FirstOrDefault ();

					if (fuente.CaseRequestId != caseId) {
						output = new Java.Lang.String ("Esta fuente no pertenece al caso");
						return output;
					}

					if (fuente.DateComplete != null) {
						output = new Java.Lang.String ("Esta entrevista ya fue terminada anteriormente");
						return output;
					}
					fuente.DateComplete = DateTime.Now;
					db.Update (fuente);
					output = new Java.Lang.String ("");
					db.Commit ();
				} catch (Exception e) {
					db.Rollback ();
					Console.WriteLine ("catched exception in VerificationService method Example invoked javascript calling -> VerificationService.terminateMeetingSource()");
					Console.WriteLine ("Exception message :::>" + e.Message);
					output = new Java.Lang.String ("Ha ocurrido un error al terminar la entrevista");
				} finally {
					db.Commit ();
				}
				db.Close ();
				return output;

			}
		}


		[Export("upsertVerificationSource")]
		public Java.Lang.String upsertVerificationSource(Java.Lang.String modelJson){
			var output = new Java.Lang.String("");
			var model = JsonConvert.DeserializeObject<SourceVerification> (modelJson.ToString());
			using (var db = FactoryConn.GetConn ()) {
				db.BeginTransaction ();
				try {
					db.CreateTable<SourceVerification> ();
					SourceVerification me = db.Table<SourceVerification> ().Where (mee => mee.Id == model.Id).FirstOrDefault ();
					if (me == null) {
						me = new SourceVerification ();
						me = model;
						me.CaseRequestId = me.IdCase;
						me.Visible = true;
						me.DateComplete = null;
						me.StatusString = "Entrevista de verificaci&oacute;n incompleta";
						db.Insert (me);
//					var verificacion = db.Table<Verification>().Where(ver=>ver.Id == me.VerificationId).FirstOrDefault();
//					if(verificacion!=null){
//						var verificacionSts = db.Table<StatusVerification>().Where(stv=>stv.Name == Constants.VERIFICATION_STATUS_NEW_SOURCE).FirstOrDefault();
//						int statusId = verificacionSts.Id??verificacion.StatusVerificationId;
//						verificacion.StatusVerificationId = statusId;
//						db.Update(verificacion);
//					}
					} else {
						db.Update (model);
					}
				} catch (Exception e) {
					db.Rollback ();
					Console.WriteLine ("catched exception in VerificationService method upsertVerificationSource invoked javascript calling -> VerificationService.upsertVerificationSource()");
					Console.WriteLine ("Exception message :::>" + e.Message);
					output = new Java.Lang.String (Constants.MSG_ERROR_UPSERT);
				} finally {
					db.Commit ();
				}
				db.Close ();
			}
			return output;
		}


	}//class end
}