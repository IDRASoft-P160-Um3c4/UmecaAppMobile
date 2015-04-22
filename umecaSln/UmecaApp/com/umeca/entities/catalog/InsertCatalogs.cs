using System;
using Android.App;
using Android.Content.Res;
using Android.OS;
using System.IO;
using SQLite.Net;
using System.Linq;
using Environment = System.Environment;
using System.Collections.Generic;
namespace UmecaApp
{


	public class InsertCatalogs
	{

		public void insertAllCatalogs(Activity act){
			InsertRelationship (act);
			InsertDegree (act);
			InsertActivity (act);
			InsertCrime (act);
			InsertDocumentType (act);
			InsertDrugType (act);
			InsertElection (act);
			InsertFieldVerification (act);
			InsertHearingFormatType (act);
			InsertHomeType (act);
			InsertImmigrationDocument (act);
			InsertMaritalStatus (act);
			InsertPeriodicity (act);
			//InserRole (act);
			InsertStatusCase (act);
			InsertStatusFieldVerfication (act);
			InsertStatusMeeting (act);
			InsertStatusVerification (act);
			InsertLocationCat (act);
			//
			InsertHomeType(act);
			InsertRegisterType (act);
		}

		public void InsertDegree(Activity act){
//			var db = new SQLiteConnection (ConstantsDB.DB_PATH);
			var db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			db.CreateTable<AcademicLevel> ();
			if (db.Table<AcademicLevel> ().Count () == 0) {
				IEnumerable<String[]> data = GetDataOfFile (ConstantsDB.CONTENT_FOLDER_CATALOG+"/academic_level.txt", act);
				List<AcademicLevel> entities=new List<AcademicLevel>(); 
				foreach (String[] line in data) {
					try{
						AcademicLevel model = new AcademicLevel ();
						model.Id = int.Parse(line [0]);
						model.Name = line [1];
						model.IsObsolete = line [2].Equals ("1");
						entities.Add(model);
					}catch(Exception e){
						Console.WriteLine ("AcademicLevel error "+e.Message);
					}
				}
				db.InsertAll (entities);
				IEnumerable<String[]> dataDegree = GetDataOfFile (ConstantsDB.CONTENT_FOLDER_CATALOG+"/degree.txt", act);
				List<Degree> entitiesDegree=new List<Degree>(); 
				foreach (String[] line in dataDegree) {
					try{
						Degree model = new Degree ();
						model.Id = int.Parse(line [0]);
						model.Name = line [1];
						model.IsObsolete = line [3].Equals ("1");
						model.AcademicLevelId = int.Parse(line[2]);
						entitiesDegree.Add(model);
					}catch(Exception e){
						Console.WriteLine ("Degree error "+e.Message);
					}
				}
				db.CreateTable<Degree> ();
				db.InsertAll (entitiesDegree);
				var content = db.Table<AcademicLevel> ().ToList();
				Console.WriteLine ("Se inserto en tabla AcademicLevel:");
				foreach (AcademicLevel m in content) {
					Console.WriteLine ("Id: "+m.Id+" Name:"+m.Name);
					Console.WriteLine ("    Degree");
					var Degrees = (from c in db.Table<Degree>()
							where c.AcademicLevelId.Equals(m.Id)
						select c).ToList();
					foreach (Degree d in Degrees) {
						Console.WriteLine ("Id: "+d.Id+" Name:"+d.Name);
					}
				}
			}
			db.Close ();
		}

		public void InsertActivity(Activity act){
//			var db = new SQLiteConnection (ConstantsDB.DB_PATH);
			var db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			db.CreateTable<ActivityCatalog> ();
			if (db.Table<ActivityCatalog> ().Count () == 0) {
				IEnumerable<String[]> data = GetDataOfFile (ConstantsDB.CONTENT_FOLDER_CATALOG+"/activity.txt", act);
				List<ActivityCatalog> entities=new List<ActivityCatalog>(); 
				foreach (String[] line in data) {
					try{
						ActivityCatalog model = new ActivityCatalog ();
						model.Id = int.Parse(line [0]);
						model.Name = line [1];
						model.IsObsolete = line [2].Equals ("1");
						model.Specification = line [3].Equals ("1");
						entities.Add(model);
					}catch(Exception e){
						Console.WriteLine ("ActivityCatalog error "+e.Message);
					}
				}
				db.InsertAll (entities);
				var content = db.Table<ActivityCatalog> ().ToList();
				Console.WriteLine ("Se inserto en tabla ActivityCatalog:");
				foreach (ActivityCatalog m in content) {
					Console.WriteLine ("Id: "+m.Id+" Name:"+m.Name);
				}
			}
			db.Close ();
		}

		public void InsertCrime(Activity act){
//			var db = new SQLiteConnection (ConstantsDB.DB_PATH);
			var db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			db.CreateTable<GroupCrime> ();
			if (db.Table<GroupCrime> ().Count () == 0) {
			
				IEnumerable<String[]> data = GetDataOfFile (ConstantsDB.CONTENT_FOLDER_CATALOG+"/group_crime.txt", act);
				List<GroupCrime> entities=new List<GroupCrime>(); 
				foreach (String[] line in data) {
					try{
						GroupCrime model = new GroupCrime ();
						model.Id = int.Parse(line [0]);
						model.Name = line [1];
						model.IsObsolete = line [2].Equals ("1");
						entities.Add(model);
					}catch(Exception e){
						Console.WriteLine ("GroupCrime error "+e.Message);
					}
				}
				db.InsertAll (entities);
				IEnumerable<String[]> dataCrime = GetDataOfFile (ConstantsDB.CONTENT_FOLDER_CATALOG+"/crime.txt", act);
				List<CrimeCatalog> entitiesCrime=new List<CrimeCatalog>(); 
				foreach (String[] line in data) {
					try{
						CrimeCatalog model = new CrimeCatalog ();
						model.Id = int.Parse(line [0]);
						model.Name = line [1];
						model.Description = line[2];
						model.IsObsolete = line [3].Equals ("1");
						model.GroupCrimeId = int.Parse(line[4]);
						entitiesCrime.Add(model);
					}catch(Exception e){
						Console.WriteLine ("CrimeCatalog error "+e.Message);
					}
				}
				db.CreateTable<CrimeCatalog> ();
				db.InsertAll (entitiesCrime);

				var content = db.Table<GroupCrime> ().ToList();
				Console.WriteLine ("Se inserto en tabla GroupCrime:");
				foreach (GroupCrime m in content) {
					Console.WriteLine ("Id: "+m.Id+" Name:"+m.Name);
					Console.WriteLine ("    Crime");
					var CrimeCatalogs = (from c in db.Table<CrimeCatalog>()
						where c.GroupCrimeId.Equals(m.Id)
						select c).ToList();
					foreach (CrimeCatalog d in CrimeCatalogs) {
						Console.WriteLine ("Id: "+d.Id+" Name:"+d.Name);
					}
				}
			}
			db.Close ();
		}


		public void InsertDocumentType(Activity act){
//			var db = new SQLiteConnection (ConstantsDB.DB_PATH);
			var db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			db.CreateTable<DocumentType> ();
			if (db.Table<DocumentType> ().Count () == 0) {
				IEnumerable<String[]> data = GetDataOfFile (ConstantsDB.CONTENT_FOLDER_CATALOG+"/document_type.txt", act);
				List<DocumentType> entities=new List<DocumentType>(); 
				foreach (String[] line in data) {
					try{
						DocumentType model = new DocumentType ();
						model.Id = int.Parse(line [0]);
						model.Name = line [1];
						model.IsObsolete = line [2].Equals ("1");
						model.Specification = line [3].Equals ("1");
						entities.Add(model);
					}catch(Exception e){
						Console.WriteLine ("DocumentType error "+e.Message);
					}
				}
				db.InsertAll (entities);
				var content = db.Table<DocumentType> ().ToList();
				Console.WriteLine ("Se inserto en tabla DocumentType:");
				foreach (DocumentType m in content) {
					Console.WriteLine ("Id: "+m.Id+" Name:"+m.Name);
				}
			}
			db.Close ();
		}

		public void InsertDrugType(Activity act){
//			var db = new SQLiteConnection (ConstantsDB.DB_PATH);
			var db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			db.CreateTable<DrugType> ();
			if (db.Table<DrugType> ().Count () == 0) {
				IEnumerable<String[]> data = GetDataOfFile (ConstantsDB.CONTENT_FOLDER_CATALOG+"/drug_type.txt", act);
				List<DrugType> entities=new List<DrugType>(); 
				foreach (String[] line in data) {
					try{
						DrugType model = new DrugType ();
						model.Id = int.Parse(line [0]);
						model.Name = line [1];
						model.IsObsolete = line [2].Equals ("1");
						model.Specification = line [3].Equals ("1");
						entities.Add(model);
					}catch(Exception e){
						Console.WriteLine ("DrugType error "+e.Message);
					}
				}
				db.InsertAll (entities);
				var content = db.Table<DrugType> ().ToList();
				Console.WriteLine ("Se inserto en tabla DrugType:");
				foreach (DrugType m in content) {
					Console.WriteLine ("Id: "+m.Id+" Name:"+m.Name);
				}
			}
			db.Close ();
		}

		public void InsertElection(Activity act){
//			var db = new SQLiteConnection (ConstantsDB.DB_PATH);
			var db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			db.CreateTable<Election> ();
			if (db.Table<Election> ().Count () == 0) {
				IEnumerable<String[]> data = GetDataOfFile (ConstantsDB.CONTENT_FOLDER_CATALOG+"/election.txt", act);
				List<Election> entities=new List<Election>(); 
				foreach (String[] line in data) {
					try{
						Election model = new Election ();
						model.Id = int.Parse(line [0]);
						model.Name = line [1];
						entities.Add(model);
					}catch(Exception e){
						Console.WriteLine ("Election error "+e.Message);
					}
				}
				db.InsertAll (entities);
				var content = db.Table<Election> ().ToList();
				Console.WriteLine ("Se inserto en tabla Election:");
				foreach (Election m in content) {
					Console.WriteLine ("Id: "+m.Id+" Name:"+m.Name);
				}
			}
			db.Close ();
		}

		public void InsertFieldVerification(Activity act){
//			var db = new SQLiteConnection (ConstantsDB.DB_PATH);
			var db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			db.CreateTable<FieldVerfication> ();
			if (db.Table<FieldVerfication> ().Count () == 0) {
				IEnumerable<String[]> data = GetDataOfFile (ConstantsDB.CONTENT_FOLDER_CATALOG+"/field_verification.txt", act);
				List<FieldVerfication> entities=new List<FieldVerfication>(); 
				foreach (String[] line in data) {
					try{
						FieldVerfication model = new FieldVerfication ();
						model.Id = int.Parse(line [0]);
						model.Code = line[1];
						model.SectionName = line[2];
						model.SectionCode = int.Parse(line[3]);
						model.FieldName = line[4];
						model.IndexField = int.Parse(line[5]);
						model.IsObsolete = line[6].Equals("1");
						model.IdSubsection= int.Parse(line[7]);
						model.Type = line[8];
						entities.Add(model);
					}catch(Exception e){
						Console.WriteLine ("FieldVerfication error "+e.Message);
					}
				}
				db.InsertAll (entities);
				var content = db.Table<FieldVerfication> ().ToList();
				Console.WriteLine ("Se inserto en tabla FieldVerfication:");
				foreach (FieldVerfication m in content) {
					Console.WriteLine ("Id: "+m.Id+" Name:"+m.Code);
				}
			}
			db.Close ();
		}

		public void InsertHearingFormatType(Activity act){
//			var db = new SQLiteConnection (ConstantsDB.DB_PATH);
			var db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			db.CreateTable<HearingType> ();
			if (db.Table<HearingType> ().Count () == 0) {
				IEnumerable<String[]> data = GetDataOfFile (ConstantsDB.CONTENT_FOLDER_CATALOG+"/hearing_format_type.txt", act);
				List<HearingType> entities=new List<HearingType>(); 
				foreach (String[] line in data) {
					try{
						HearingType model = new HearingType ();
						model.Id = int.Parse(line [0]);
						model.Name = line[1];
						model.Description = line[2];
						model.IsObsolete = line[3].Equals("1");
						entities.Add(model);
					}catch(Exception e){
						Console.WriteLine ("HearingType error "+e.Message);
					}
				}
				db.InsertAll (entities);
				var content = db.Table<HearingType> ().ToList();
				Console.WriteLine ("Se inserto en tabla HearingType:");
				foreach (HearingType m in content) {
					Console.WriteLine ("Id: "+m.Id+" Name:"+m.Name);
				}
			}
			db.Close ();
		}

		public void InsertHomeType(Activity act){
//			var db = new SQLiteConnection (ConstantsDB.DB_PATH);
			var db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			db.CreateTable<HomeType> ();
			if (db.Table<HomeType> ().Count () == 0) {
				IEnumerable<String[]> data = GetDataOfFile (ConstantsDB.CONTENT_FOLDER_CATALOG+"/home_type.txt", act);
				List<HomeType> entities=new List<HomeType>(); 
				foreach (String[] line in data) {
					try{
						HomeType model = new HomeType ();
						model.Id = int.Parse(line [0]);
						model.Name = line[1];
						model.Specification = line[2].Equals("1");
						model.IsObsolete = line[3].Equals("1");
						entities.Add(model);
					}catch(Exception e){
						Console.WriteLine ("HomeType error "+e.Message);
					}
				}
				db.InsertAll (entities);
				var content = db.Table<HomeType> ().ToList();
				Console.WriteLine ("Se inserto en tabla HomeType:");
				foreach (HomeType m in content) {
					Console.WriteLine ("Id: "+m.Id+" Name:"+m.Name);
				}
			}
			db.Close ();
		}

		public void InsertImmigrationDocument(Activity act){
//			var db = new SQLiteConnection (ConstantsDB.DB_PATH);
			var db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			db.CreateTable<ImmigrationDocument> ();
			if (db.Table<ImmigrationDocument> ().Count () == 0) {
				IEnumerable<String[]> data = GetDataOfFile (ConstantsDB.CONTENT_FOLDER_CATALOG+"/immigrationDocument.txt", act);
				List<ImmigrationDocument> entities=new List<ImmigrationDocument>(); 
				foreach (String[] line in data) {
					try{
						ImmigrationDocument model = new ImmigrationDocument ();
						model.Id = int.Parse(line [0]);
						model.Name = line[1];
						model.Specification = line[2].Equals("1");
						model.IsObsolete = line[3].Equals("1");
						entities.Add(model);
					}catch(Exception e){
						Console.WriteLine ("ImmigrationDocument error "+e.Message);
					}
				}
				db.InsertAll (entities);
				var content = db.Table<ImmigrationDocument> ().ToList();
				Console.WriteLine ("Se inserto en tabla ImmigrationDocument:");
				foreach (ImmigrationDocument m in content) {
					Console.WriteLine ("Id: "+m.Id+" Name:"+m.Name);
				}
			}
			db.Close ();
		}

		public void InsertMaritalStatus(Activity act){
//			var db = new SQLiteConnection (ConstantsDB.DB_PATH);
			var db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			db.CreateTable<MaritalStatus> ();
			if (db.Table<MaritalStatus> ().Count () == 0) {
				IEnumerable<String[]> data = GetDataOfFile (ConstantsDB.CONTENT_FOLDER_CATALOG+"/marital_status.txt", act);
				List<MaritalStatus> entities=new List<MaritalStatus>(); 
				foreach (String[] line in data) {
					try{
						MaritalStatus model = new MaritalStatus ();
						model.Id = int.Parse(line [0]);
						model.Name = line[1];
						entities.Add(model);
					}catch(Exception e){
						Console.WriteLine ("MaritalStatus error "+e.Message);
					}
				}
				db.InsertAll (entities);
				var content = db.Table<MaritalStatus> ().ToList();
				Console.WriteLine ("Se inserto en tabla ImmigrationDocument:");
				foreach (MaritalStatus m in content) {
					Console.WriteLine ("Id: "+m.Id+" Name:"+m.Name);
				}
			}
			db.Close ();
		}


		public void InsertPeriodicity(Activity act){
//			var db = new SQLiteConnection (ConstantsDB.DB_PATH);
			var db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			db.CreateTable<Periodicity> ();
			if (db.Table<Periodicity> ().Count () == 0) {
				IEnumerable<String[]> data = GetDataOfFile (ConstantsDB.CONTENT_FOLDER_CATALOG+"/periodicity.txt", act);
				List<Periodicity> entities=new List<Periodicity>(); 
				foreach (String[] line in data) {
					try{
						Periodicity model = new Periodicity ();
						model.Id = int.Parse(line [0]);
						model.Name = line[1];
						model.Specification = line[2].Equals("1");
						model.IsObsolete = line[3].Equals("1");
						entities.Add(model);
					}catch(Exception e){
						Console.WriteLine ("error "+e.Message);
					}
				}
				db.InsertAll (entities);
				var content = db.Table<Periodicity> ().ToList();
				Console.WriteLine ("Se inserto en tabla Periodicity:");
				foreach (Periodicity m in content) {
					Console.WriteLine ("Id: "+m.Id+" Name:"+m.Name);
				}
			}
			db.Close ();
		}

		public void InsertRegisterType(Activity act){
//			var db = new SQLiteConnection (ConstantsDB.DB_PATH);
			var db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			db.CreateTable<RegisterType> ();
			if (db.Table<RegisterType> ().Count () == 0) {
				IEnumerable<String[]> data = GetDataOfFile (ConstantsDB.CONTENT_FOLDER_CATALOG+"/register_type.txt", act);
				List<RegisterType> entities=new List<RegisterType>(); 
				foreach (String[] line in data) {
					try{
						RegisterType model = new RegisterType ();
						model.Id = int.Parse(line [0]);
						model.Name = line[1];
						entities.Add(model);
					}catch(Exception e){
						Console.WriteLine ("RegisterType error "+e.Message);
					}
				}
				db.InsertAll (entities);
				var content = db.Table<RegisterType> ().ToList();
				Console.WriteLine ("Se inserto en tabla RegisterType:");
				foreach (RegisterType m in content) {
					Console.WriteLine ("Id: "+m.Id+" Name:"+m.Name);
				}
			}
			db.Close ();
		}


		public void InsertRelationship(Activity act){
//			var db = new SQLiteConnection (ConstantsDB.DB_PATH);
			var db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			db.CreateTable<Relationship> ();
			if (db.Table<Relationship> ().Count () == 0) {
				IEnumerable<String[]> data = GetDataOfFile (ConstantsDB.CONTENT_FOLDER_CATALOG+"/relationship.txt", act);
				List<Relationship> entities=new List<Relationship>(); 
				foreach (String[] line in data) {
					try{
					Relationship r = new Relationship ();
						r.Id = int.Parse(line [0]);
						r.Name = line [1];
						r.IsObsolete = line [2].Equals ("1");
						r.Specification = line [3].Equals ("1");
						entities.Add(r);
					}catch(Exception e){
						Console.WriteLine ("Relationship error "+e.Message);
					}
				}
				db.InsertAll (entities);
				var content = db.Table<Relationship> ().ToList();
				Console.WriteLine ("Se inserto en tabla Relationship:");
				foreach (Relationship m in content) {
					Console.WriteLine ("Id: "+m.Id+" Name:"+m.Name);
				}
			}
			db.Close ();
		}

		public void InsertStatusCase(Activity act){
//			var db = new SQLiteConnection (ConstantsDB.DB_PATH);
			var db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			db.CreateTable<StatusCase> ();
			if (db.Table<StatusCase> ().Count () == 0) {
				IEnumerable<String[]> data = GetDataOfFile (ConstantsDB.CONTENT_FOLDER_CATALOG+"/status_case.txt", act);
				List<StatusCase> entities=new List<StatusCase>(); 
				foreach (String[] line in data) {
					try{
						StatusCase model = new StatusCase ();
						model.Id = int.Parse(line [0]);
						model.Name = line [1];
						model.Description = line[2];
						entities.Add(model);
					}catch(Exception e){
						Console.WriteLine ("error "+e.Message);
					}
				}
				db.InsertAll (entities);
				var content = db.Table<StatusCase> ().ToList();
				Console.WriteLine ("Se inserto en tabla StatusCase:");
				foreach (StatusCase m in content) {
					Console.WriteLine ("Id: "+m.Id+" Name:"+m.Name);
				}
			}
			db.Close ();
		}

		public void InsertStatusFieldVerfication(Activity act){
//			var db = new SQLiteConnection (ConstantsDB.DB_PATH);
			var db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			db.CreateTable<StatusFieldVerification> ();
			if (db.Table<StatusFieldVerification> ().Count () == 0) {
				IEnumerable<String[]> data = GetDataOfFile (ConstantsDB.CONTENT_FOLDER_CATALOG+"/status_field_verification.txt", act);
				List<StatusFieldVerification> entities=new List<StatusFieldVerification>(); 
				foreach (String[] line in data) {
					try{
						StatusFieldVerification model = new StatusFieldVerification ();
						model.Id = int.Parse(line [0]);
						model.Name = line [1];
						model.Description = line[2];
						entities.Add(model);
					}catch(Exception e){
						Console.WriteLine ("StatusFieldVerification error "+e.Message);
					}
				}
				db.InsertAll (entities);
				var content = db.Table<StatusFieldVerification> ().ToList();
				Console.WriteLine ("Se inserto en tabla StatusFieldVerification:");
				foreach (StatusFieldVerification m in content) {
					Console.WriteLine ("Id: "+m.Id+" Name:"+m.Name);
				}
			}
			db.Close ();
		}


		public void InsertStatusMeeting(Activity act){
//			var db = new SQLiteConnection (ConstantsDB.DB_PATH);
			var db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			db.CreateTable<StatusMeeting> ();
			if (db.Table<StatusMeeting> ().Count () == 0) {
				IEnumerable<String[]> data = GetDataOfFile (ConstantsDB.CONTENT_FOLDER_CATALOG+"/status_meeting.txt", act);
				List<StatusMeeting> entities=new List<StatusMeeting>(); 
				foreach (String[] line in data) {
					try{
						StatusMeeting model = new StatusMeeting ();
						model.Id = int.Parse(line [0]);
						model.Status = line [1];
						model.Description = line[2];
						entities.Add(model);
					}catch(Exception e){
						Console.WriteLine ("StatusMeeting error "+e.Message);
					}
				}
				db.InsertAll (entities);
				var content = db.Table<StatusMeeting> ().ToList();
				Console.WriteLine ("Se inserto en tabla StatusMeeting:");
				foreach (StatusMeeting m in content) {
					Console.WriteLine ("Id: "+m.Id+" Name:"+m.Status);
				}
			}
			db.Close ();
		}


		public void InsertStatusVerification(Activity act){
//			var db = new SQLiteConnection (ConstantsDB.DB_PATH);
			var db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			db.CreateTable<StatusVerification> ();
			if (db.Table<StatusVerification> ().Count () == 0) {
				IEnumerable<String[]> data = GetDataOfFile (ConstantsDB.CONTENT_FOLDER_CATALOG+"/status_verification.txt", act);
				List<StatusVerification> entities=new List<StatusVerification>(); 
				foreach (String[] line in data) {
					try{
						StatusVerification model = new StatusVerification ();
						model.Id = int.Parse(line [0]);
						model.Name = line [1];
						model.Description = line[2];
						entities.Add(model);
					}catch(Exception e){
						Console.WriteLine ("StatusVerification error "+e.Message);
					}
				}
				db.InsertAll (entities);
				var content = db.Table<StatusVerification> ().ToList();
				Console.WriteLine ("Se inserto en tabla StatusVerification:");
				foreach (StatusVerification m in content) {
					Console.WriteLine ("Id: "+m.Id+" Name:"+m.Name);
				}
			}
			db.Close ();
		}



		public void InsertLocationCat(Activity act){
			//			var db = new SQLiteConnection (ConstantsDB.DB_PATH);
			var db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
			db.CreateTable<Location> ();
			if (db.Table<Location> ().Count () == 0) {
				IEnumerable<String[]> data = GetDataOfFile (ConstantsDB.CONTENT_FOLDER_CATALOG+"/location.txt", act);
				List<Location> entities=new List<Location>(); 
				foreach (String[] line in data) {
					try{
						Location model = new Location ();
						model.Id = int.Parse(line [0]);
						model.MunicipalityId = long.Parse(line [1]);
						model.Name = line[2];
						model.Abbreviation = line[3];
						model.Description = line[4];
						model.ZipCode = line[5];
						entities.Add(model);
					}catch(Exception e){
						Console.WriteLine ("Location error "+e.Message);
					}
				}
				db.InsertAll (entities);
//				var content = db.Table<Location> ();
				Console.WriteLine ("Se inserto en tabla Location:"+entities.Count);
//				foreach (StatusVerification m in content) {
//					Console.WriteLine ("Id: "+m.Id+" Name:"+m.Name);
//				}
			}
			db.Close ();
		}

		//file loader
		private List<String[]> GetDataOfFile(string fileName, Activity act){
			List<String[]> result = new List<String[]>();
			using (StreamReader sr = new StreamReader (act.Assets.Open (fileName)))
			{
				String line;
				while ((line = sr.ReadLine ()) != null) {
					String[] lineSplit = line.Split('|');
					result.Add(lineSplit);
				}

			}
			return result;
		}
	}//class end

}//namespace delimiter