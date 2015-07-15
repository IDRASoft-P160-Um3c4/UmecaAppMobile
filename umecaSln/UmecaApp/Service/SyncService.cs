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
						var caseAsignResponse = uwsl.getAssignedCaseByAssignmentId(revisor.username,guid,tbltAi.id,false);
						if(caseAsignResponse.hasError){
							return new Java.Lang.String ("{\"error\":true, \"response\":\""+caseAsignResponse.message+"\"}");
						}else{
							var dataString1 =  (System.Xml.XmlNode[]) caseAsignResponse.returnData;
							var getData1 = JsonConvert.DeserializeObject<TabletCaseDto>(dataString1[0].Value.ToString());
							Imputed imp = new Imputed();
							Meeting me = new Meeting();
							Case cs = new Case();
							cs = getData1.CaseToObject();
							db.Insert(cs);
							Verification ve = new Verification();

							if(getData1.meeting!=null){
								TabletMeetingDto tmd = new TabletMeetingDto();
								tmd = getData1.meeting;
								me = tmd.MeetingToObject();
								me.CaseDetentionId = cs.Id;
								db.Insert(me);
							}
							if(getData1.meeting!=null && getData1.meeting.imputed!=null ){
								TabletImputedDto tid = new TabletImputedDto();
								tid = getData1.meeting.imputed;
								imp.MeetingId = me.Id;
								imp = tid.ImputedDtoToObject();
							}
							 
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