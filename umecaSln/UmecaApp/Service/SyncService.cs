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
				var respunse = uwsl.loginFromTablet (usuario,ecodedPass);
				ResponseMessage respuesta = JsonConvert.DeserializeObject<ResponseMessage>(respunse);
				if(respuesta.hasError??true){
					return new Java.Lang.String ("{\"error\":true, \"response\":\""+respuesta.message+"\"}");
				}else{
					var usuarios = db.Table<User>().ToList();
					foreach(User u in usuarios){
						db.Delete(u);
					}
					User asociado = new User();
					var getData = JsonConvert.DeserializeObject<TabletUserDto>(respuesta.returnData.ToString());
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
			localhostUmecaWs.UmecaWS uwsl = new UmecaApp.localhostUmecaWs.UmecaWS ();
			var ecodedPass = Crypto.HashPassword (pass.ToString());
			db.CreateTable<User> ();
			var usrList = db.Table<User> ().ToList ();
			if (usrList != null && usrList.Count > 0) {
				var savedUsr = usrList [0];
				try{
					var loginResult = uwsl.loginFromTablet (savedUsr.username, ecodedPass);
				}catch(Exception e){
					db.Rollback ();
					Console.WriteLine ("exception in userUpsert()");
					Console.WriteLine("Exception message :::>"+e.Message);
				}finally{
					db.Commit ();
				}

			} else {
				return new Java.Lang.String ("{\"error\":true, \"response\":\"No se encontro ningun usuario asociado\"}");
			}

		}




	}//class end


}