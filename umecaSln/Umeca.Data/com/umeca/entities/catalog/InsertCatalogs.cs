using System;
using Android.App;
using Android.Content.Res;
using Android.OS;
using System.IO;
using System.Linq;
using Environment = System.Environment;
using System.Collections.Generic;
using Umeca.Data;
using System.Threading.Tasks;
using SQLite;

namespace UmecaApp
{


	public class InsertCatalogs
	{

		public void insertAllCatalogs(Activity act){

			var t = Task.Run(() =>
				{
					InsertUserRoles (act);

					InsertRelationship (act);
					InsertDegree (act);
					InsertActivity (act);
					InsertDocumentType (act);
					InsertDrugType (act);
					InsertElection (act);
					InsertFieldVerification (act);
					InsertHearingType (act);
					InsertHomeType (act);
					InsertImmigrationDocument (act);
					InsertMaritalStatus (act);
					InsertPeriodicity (act);

					InsertStatusCase (act);
					InsertStatusFieldVerfication (act);
					InsertStatusMeeting (act);
					InsertStatusVerification (act);
					InsertLocationCat (act);

					InsertHomeType (act);
					InsertRegisterType (act);

					InsertArrangement (act);
					InsertGroupCrime (act);
					InsertCrimeCatalog (act);

					InsertInformationAviability(act);
					InsertDistrict(act);

					CreateTablesToConsult ();

					InsertVerMethod (act);
				});

			Task.WaitAll(t);



		}

		public void CreateTablesToConsult(){
			using (var db = FactoryConn.GetConn ()) {
				db.BeginTransaction ();
				db.CreateTable<Case> ();
				db.CreateTable<Meeting> ();	
				db.CreateTable<Imputed> ();
				db.CreateTable<User> ();
				db.CreateTable<Verification> ();
				db.CreateTable<StatusVerification> ();
				db.CreateTable<SourceVerification> ();
				db.CreateTable<SocialEnvironment> ();
				db.CreateTable<RelActivity> ();
				db.CreateTable<HearingFormatImputed> ();
				db.CreateTable<HearingFormat> ();
				db.CreateTable<Arrangement> ();
				db.CreateTable<AssignedArrangement> ();
				db.CreateTable<ContactData> ();
				db.CreateTable<Crime> ();
				db.CreateTable<CrimeCatalog> ();
				db.CreateTable<HearingFormatSpecs> ();
				db.CreateTable<HearingType> ();
				db.CreateTable<LogCase> ();
				db.CreateTable<FieldVerification> ();
				db.CreateTable<Address> ();
				db.CreateTable<FieldMeetingSource> ();
				db.CreateTable<SocialNetwork> ();
				db.CreateTable<PersonSocialNetwork> ();
				db.CreateTable<Reference> ();
				db.CreateTable<School> ();
				db.CreateTable<Drug> ();
				db.CreateTable<Schedule> ();
				db.CreateTable<LeaveCountry> ();
				db.CreateTable<ImputedHome> ();
				db.CreateTable<Job> ();
				db.CreateTable<Role> ();
				db.CreateTable<AcademicLevel> ();
				db.CreateTable<Degree> ();
				db.CreateTable<ActivityCatalog> ();
				db.CreateTable<DocumentType> ();
				db.CreateTable<Election> ();
				db.CreateTable<StatusCase> ();
				db.CreateTable<StatusMeeting> ();
				db.Commit ();

				db.CreateTable<FieldVerification> ();
				db.CreateTable<FieldMeetingSource> ();
				db.CreateTable<SocialEnvironment> ();
				db.CreateTable<ImputedHome> ();
				db.CreateTable<SocialEnvironment> ();
				db.CreateTable<RelActivity> ();
				db.CreateTable<SocialNetwork> ();
				db.CreateTable<PersonSocialNetwork> ();
				db.CreateTable<Reference> ();
				db.CreateTable<School> ();
				db.CreateTable<Drug> ();
				db.CreateTable<Schedule> ();
				db.CreateTable<LeaveCountry> ();
				db.CreateTable<ImputedHome> ();
				db.CreateTable<Job> ();
				db.CreateTable<SourceVerification> ();
				db.CreateTable<User> ();
				db.CreateTable<Configuracion> ();

				db.Close ();
			}
		}

		public void InsertUserRoles(Activity act){
			//			var db = new SQLiteConnection (ConstantsDb.DB_PATH);
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<Role> ();
				if (db.Table<Role> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/role.txt", act);
					List<Role> entities = new List<Role> (); 
					foreach (String[] line in data) {
						try {
							Role model = new Role ();
							model.Id = int.Parse (line [0]);
							model.role = line [1];
							model.Description = line [2];
							entities.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("RoleCatalog error " + e.Message);
						}
					}
					db.InsertAll (entities);
					var content = db.Table<Role> ().ToList ();
					Console.WriteLine ("Se inserto en tabla Role:");
					foreach (Role m in content) {
						Console.WriteLine ("Id: " + m.Id + " Role:" + m.role);
					}
				}
				db.Close ();
			}
		}

		public void InsertDegree(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<AcademicLevel> ();
				if (db.Table<AcademicLevel> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/academic_level.txt", act);
					List<AcademicLevel> entities = new List<AcademicLevel> (); 
					foreach (String[] line in data) {
						try {
							AcademicLevel model = new AcademicLevel ();
							model.Id = int.Parse (line [0]);
							model.Name = line [1];
							model.IsObsolete = line [2].Equals ("1");
							entities.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("AcademicLevel error " + e.Message);
						}
					}
					db.InsertAll (entities);
					IEnumerable<String[]> dataDegree = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/degree.txt", act);
					List<Degree> entitiesDegree = new List<Degree> (); 
					foreach (String[] line in dataDegree) {
						try {
							Degree model = new Degree ();
							model.Id = int.Parse (line [0]);
							model.Name = line [1];
							model.IsObsolete = line [3].Equals ("1");
							model.AcademicLevelId = int.Parse (line [2]);
							entitiesDegree.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("Degree error " + e.Message);
						}
					}
					db.CreateTable<Degree> ();
					db.InsertAll (entitiesDegree);
					var content = db.Table<AcademicLevel> ().ToList ();
					Console.WriteLine ("Se inserto en tabla AcademicLevel:");
					foreach (AcademicLevel m in content) {
						Console.WriteLine ("Id: " + m.Id + " Name:" + m.Name);
						Console.WriteLine ("    Degree");
						var Degrees = (from c in db.Table<Degree> ()
						              where c.AcademicLevelId.Equals (m.Id)
						              select c).ToList ();
						foreach (Degree d in Degrees) {
							Console.WriteLine ("Id: " + d.Id + " Name:" + d.Name);
						}
					}
				}
				db.Close ();
			}
		}

		public void InsertActivity(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<ActivityCatalog> ();
				if (db.Table<ActivityCatalog> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/activity.txt", act);
					List<ActivityCatalog> entities = new List<ActivityCatalog> (); 
					foreach (String[] line in data) {
						try {
							ActivityCatalog model = new ActivityCatalog ();
							model.Id = int.Parse (line [0]);
							model.Name = line [1];
							model.Specification = line [2].Equals ("1");
							model.IsObsolete = line [3].Equals ("1");
							entities.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("ActivityCatalog error " + e.Message);
						}
					}
					db.InsertAll (entities);
					var content = db.Table<ActivityCatalog> ().ToList ();
					Console.WriteLine ("Se inserto en tabla ActivityCatalog:");
					foreach (ActivityCatalog m in content) {
						Console.WriteLine ("Id: " + m.Id + " Name:" + m.Name);
					}
				}
				db.Close ();
			}
		}


		public void InsertDocumentType(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<DocumentType> (); // (createFlags: SQLite.Interop.CreateFlags.AutoIncPK);
				if (db.Table<DocumentType> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/document_type.txt", act);
					List<DocumentType> entities = new List<DocumentType> (); 
					foreach (String[] line in data) {
						try {
							DocumentType model = new DocumentType ();
							model.Id = int.Parse (line [0]);
							model.Name = line [1];
							model.IsObsolete = line [3].Equals ("1");
							model.Specification = line [2].Equals ("1");
							entities.Add (model);
							//db.Insert(model);
						} catch (Exception e) {
							Console.WriteLine ("DocumentType error " + e.Message);
						}
					}
					db.InsertAll (entities);
					//db.Commit ();
					var content = db.Table<DocumentType> ().ToList ();
					Console.WriteLine ("Se inserto en tabla DocumentType:");
					foreach (DocumentType m in content) {
						Console.WriteLine ("Id: " + m.Id + " Name:" + m.Name);
					}
				}
				db.Close ();
			}

			using (var db = FactoryConn.GetConn ()) {
				var e = db.Table<DocumentType> ().ToList ();
				foreach (DocumentType m in e) {
					Console.WriteLine ("Id: " + m.Id + " Name:" + m.Name);
				}
			}

		}

		public void InsertDrugType(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<DrugType> ();
				if (db.Table<DrugType> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/drug_type.txt", act);
					List<DrugType> entities = new List<DrugType> (); 
					foreach (String[] line in data) {
						try {
							DrugType model = new DrugType ();
							model.Id = int.Parse (line [0]);
							model.Name = line [1];
							model.IsObsolete = line [2].Equals ("1");
							model.Specification = line [3].Equals ("1");
							entities.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("DrugType error " + e.Message);
						}
					}
					db.InsertAll (entities);
					var content = db.Table<DrugType> ().ToList ();
					Console.WriteLine ("Se inserto en tabla DrugType:");
					foreach (DrugType m in content) {
						Console.WriteLine ("Id: " + m.Id + " Name:" + m.Name);
					}
				}
				db.Close ();
			}
		}

		public void InsertElection(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<Election> ();
				if (db.Table<Election> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/election.txt", act);
					List<Election> entities = new List<Election> (); 
					foreach (String[] line in data) {
						try {
							Election model = new Election ();
							model.Id = int.Parse (line [0]);
							model.Name = line [1];
							entities.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("Election error " + e.Message);
						}
					}
					db.InsertAll (entities);
					var content = db.Table<Election> ().ToList ();
					Console.WriteLine ("Se inserto en tabla Election:");
					foreach (Election m in content) {
						Console.WriteLine ("Id: " + m.Id + " Name:" + m.Name);
					}
				}
				db.Close ();
			}
		}

		public void InsertFieldVerification(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<FieldVerification> ();
				if (db.Table<FieldVerification> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/field_verification.txt", act);
					List<FieldVerification> entities = new List<FieldVerification> (); 
					foreach (String[] line in data) {
						try {
							FieldVerification model = new FieldVerification ();
							model.Id = int.Parse (line [0]);
							model.Code = line [1];
							model.Section = line [2];
							model.SectionCode = int.Parse (line [3]);
							model.FieldName = line [4];
							model.IndexField = int.Parse (line [5]);
							model.IsObsolete = line [6].Equals ("1");
							model.IdSubsection = int.Parse (line [7]);
							model.Type = line [8];
							entities.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("FieldVerfication error " + e.Message);
						}
					}
					db.InsertAll (entities);
					var content = db.Table<FieldVerification> ().ToList ();
					Console.WriteLine ("Se inserto en tabla FieldVerfication:");
					foreach (FieldVerification m in content) {
						Console.WriteLine ("Id: " + m.Id + " Name:" + m.Code);
					}
				}
				db.Close ();
			}
		}

		public void InsertHearingType(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<HearingType> ();
				if (db.Table<HearingType> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/hearing_type.txt", act);
					List<HearingType> entities = new List<HearingType> (); 
					foreach (String[] line in data) {
						try {
							HearingType model = new HearingType ();
							model.Id = int.Parse (line [0]);
							model.Description = line [1];
							model.IsObsolete = line [2].Equals ("1");
							model.Lock = line [3].Equals ("1");
							model.Specification = line [4].Equals ("1");
							entities.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("HearingType error " + e.Message);
						}
					}
					db.InsertAll (entities);
					var content = db.Table<HearingType> ().ToList ();
					Console.WriteLine ("Se inserto en tabla HearingType:");
					foreach (HearingType m in content) {
						Console.WriteLine ("Id: " + m.Id + " Name:" + m.Description);
					}
				}
				db.Close ();
			}
		}

		public void InsertArrangement(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<Arrangement> ();
				if (db.Table<Arrangement> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/arrangement.txt", act);
					List<Arrangement> entities = new List<Arrangement> (); 
					foreach (String[] line in data) { 
						try {
							Arrangement model = new Arrangement ();
							model.Id = int.Parse (line [0]);
							model.Description = line [1];
							model.Type = int.Parse (line [2]);
							model.Index = int.Parse (line [3]);
							model.IsObsolete = line [4].Equals ("1");
							model.IsNational = line [5].Equals ("1");
							model.IsDefault = line [6].Equals ("1");
							model.IsExclusive = line [7].Equals ("1");
							entities.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("arrangement error " + e.Message);
						}
					}
					db.InsertAll (entities);
					var content = db.Table<HearingType> ().ToList ();
					Console.WriteLine ("Se inserto en tabla arrangement:");
					foreach (HearingType m in content) {
						Console.WriteLine ("Id: " + m.Id + " Name:" + m.Description);
					}
				}
				db.Close ();
			}
		}


		public void InsertGroupCrime(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<GroupCrime> ();
				if (db.Table<GroupCrime> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/group_crime.txt", act);
					List<GroupCrime> entities = new List<GroupCrime> (); 
					foreach (String[] line in data) { 
						try {
							GroupCrime model = new GroupCrime ();
							model.Id = int.Parse (line [0]);
							model.Name = line [1];
							model.Description = line [2];
							model.IsObsolete = line [3].Equals ("1");
							entities.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("group_crime error " + e.Message);
						}
					}
					db.InsertAll (entities);
					var content = db.Table<HearingType> ().ToList ();
					Console.WriteLine ("Se inserto en tabla group_crime:");
					foreach (HearingType m in content) {
						Console.WriteLine ("Id: " + m.Id + " Name:" + m.Description);
					}
				}
				db.Close ();
			}
		}

		public void InsertCrimeCatalog(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<CrimeCatalog> ();
				if (db.Table<CrimeCatalog> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/crime.txt", act);
					List<CrimeCatalog> entities = new List<CrimeCatalog> (); 
					foreach (String[] line in data) { 
						try {
							CrimeCatalog model = new CrimeCatalog ();
							model.Id = int.Parse (line [0]);
							model.Name = line [1];
							model.Description = line [2];
							model.IsObsolete = line [3].Equals ("1");
							model.GroupCrimeId = int.Parse (line [4]);
							entities.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("crime error " + e.Message);
						}
					}
					db.InsertAll (entities);
					var content = db.Table<HearingType> ().ToList ();
					Console.WriteLine ("Se inserto en tabla crime:");
					foreach (HearingType m in content) {
						Console.WriteLine ("Id: " + m.Id + " Name:" + m.Description);
					}
				}
				db.Close ();
			}
		}

		public void InsertHomeType(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<HomeType> ();
				if (db.Table<HomeType> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/home_type.txt", act);
					List<HomeType> entities = new List<HomeType> (); 
					foreach (String[] line in data) {
						try {
							HomeType model = new HomeType ();
							model.Id = int.Parse (line [0]);
							model.Name = line [1];
							model.Specification = line [2].Equals ("1");
							model.IsObsolete = line [3].Equals ("1");
							entities.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("HomeType error " + e.Message);
						}
					}
					db.InsertAll (entities);
					var content = db.Table<HomeType> ().ToList ();
					Console.WriteLine ("Se inserto en tabla HomeType:");
					foreach (HomeType m in content) {
						Console.WriteLine ("Id: " + m.Id + " Name:" + m.Name);
					}
				}
				db.Close ();
			}
		}

		public void InsertImmigrationDocument(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<ImmigrationDocument> ();
				if (db.Table<ImmigrationDocument> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/immigrationDocument.txt", act);
					List<ImmigrationDocument> entities = new List<ImmigrationDocument> (); 
					foreach (String[] line in data) {
						try {
							ImmigrationDocument model = new ImmigrationDocument ();
							model.Id = int.Parse (line [0]);
							model.Name = line [1];
							model.Specification = line [2].Equals ("1");
							model.IsObsolete = line [3].Equals ("1");
							entities.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("ImmigrationDocument error " + e.Message);
						}
					}
					db.InsertAll (entities);
					var content = db.Table<ImmigrationDocument> ().ToList ();
					Console.WriteLine ("Se inserto en tabla ImmigrationDocument:");
					foreach (ImmigrationDocument m in content) {
						Console.WriteLine ("Id: " + m.Id + " Name:" + m.Name);
					}
				}
				db.Close ();
			}
		}

		public void InsertMaritalStatus(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<MaritalStatus> ();
				if (db.Table<MaritalStatus> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/marital_status.txt", act);
					List<MaritalStatus> entities = new List<MaritalStatus> (); 
					foreach (String[] line in data) {
						try {
							MaritalStatus model = new MaritalStatus ();
							model.Id = int.Parse (line [0]);
							model.Name = line [1];
							entities.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("MaritalStatus error " + e.Message);
						}
					}
					db.InsertAll (entities);
					var content = db.Table<MaritalStatus> ().ToList ();
					Console.WriteLine ("Se inserto en tabla MaritalStatus:");
					foreach (MaritalStatus m in content) {
						Console.WriteLine ("Id: " + m.Id + " Name:" + m.Name);
					}
				}
				db.Close ();
			}
		}


		public void InsertPeriodicity(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<Periodicity> ();
				if (db.Table<Periodicity> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/periodicity.txt", act);
					List<Periodicity> entities = new List<Periodicity> (); 
					foreach (String[] line in data) {
						try {
							Periodicity model = new Periodicity ();
							model.Id = int.Parse (line [0]);
							model.Name = line [1];
							model.Specification = line [2].Equals ("1");
							model.IsObsolete = line [3].Equals ("1");
							entities.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("Periodicity error " + e.Message);
						}
					}
					db.InsertAll (entities);
					var content = db.Table<Periodicity> ().ToList ();
					Console.WriteLine ("Se inserto en tabla Periodicity:");
					foreach (Periodicity m in content) {
						Console.WriteLine ("Id: " + m.Id + " Name:" + m.Name);
					}
				}
				db.Close ();
			}
		}

		public void InsertRegisterType(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<RegisterType> ();
				if (db.Table<RegisterType> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/register_type.txt", act);
					List<RegisterType> entities = new List<RegisterType> (); 
					foreach (String[] line in data) {
						try {
							RegisterType model = new RegisterType ();
							model.Id = int.Parse (line [0]);
							model.Name = line [1];
							entities.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("RegisterType error " + e.Message);
						}
					}
					db.InsertAll (entities);
					var content = db.Table<RegisterType> ().ToList ();
					Console.WriteLine ("Se inserto en tabla RegisterType:");
					foreach (RegisterType m in content) {
						Console.WriteLine ("Id: " + m.Id + " Name:" + m.Name);
					}
				}
				db.Close ();
			}
		}


		public void InsertRelationship(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<Relationship> ();
				if (db.Table<Relationship> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/relationship.txt", act);
					List<Relationship> entities = new List<Relationship> (); 
					foreach (String[] line in data) {
						try {
							Relationship r = new Relationship ();
							r.Id = int.Parse (line [0]);
							r.Name = line [1];
							r.IsObsolete = line [2].Equals ("1");
							r.Specification = line [3].Equals ("1");
							entities.Add (r);
						} catch (Exception e) {
							Console.WriteLine ("Relationship error " + e.Message);
						}
					}
					db.InsertAll (entities);
					var content = db.Table<Relationship> ().ToList ();
					Console.WriteLine ("Se inserto en tabla Relationship:");
					foreach (Relationship m in content) {
						Console.WriteLine ("Id: " + m.Id + " Name:" + m.Name);
					}
				}
				db.Close ();
			}
		}

		public void InsertStatusCase(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<StatusCase> ();
				if (db.Table<StatusCase> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/status_case.txt", act);
					List<StatusCase> entities = new List<StatusCase> (); 
					foreach (String[] line in data) {
						try {
							StatusCase model = new StatusCase ();
							model.Id = int.Parse (line [0]);
							model.Name = line [1];
							model.Description = line [2];
							entities.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("StatusCase error " + e.Message);
						}
					}
					db.InsertAll (entities);
					var content = db.Table<StatusCase> ().ToList ();
					Console.WriteLine ("Se inserto en tabla StatusCase:");
					foreach (StatusCase m in content) {
						Console.WriteLine ("Id: " + m.Id + " Name:" + m.Name);
					}
				}
				db.Close ();
			}
		}

		public void InsertStatusFieldVerfication(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<StatusFieldVerification> ();
				if (db.Table<StatusFieldVerification> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/status_field_verification.txt", act);
					List<StatusFieldVerification> entities = new List<StatusFieldVerification> (); 
					foreach (String[] line in data) {
						try {
							StatusFieldVerification model = new StatusFieldVerification ();
							model.Id = int.Parse (line [0]);
							model.Name = line [1];
							model.Description = line [2];
							entities.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("StatusFieldVerification error " + e.Message);
						}
					}
					db.InsertAll (entities);
					var content = db.Table<StatusFieldVerification> ().ToList ();
					Console.WriteLine ("Se inserto en tabla StatusFieldVerification:");
					foreach (StatusFieldVerification m in content) {
						Console.WriteLine ("Id: " + m.Id + " Name:" + m.Name);
					}
				}
				db.Close ();
			}
		}


		public void InsertStatusMeeting(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<StatusMeeting> ();
				if (db.Table<StatusMeeting> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/status_meeting.txt", act);
					List<StatusMeeting> entities = new List<StatusMeeting> (); 
					foreach (String[] line in data) {
						try {
							StatusMeeting model = new StatusMeeting ();
							model.Id = int.Parse (line [0]);
							model.Status = line [1];
							model.Description = line [2];
							entities.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("StatusMeeting error " + e.Message);
						}
					}
					db.InsertAll (entities);
					var content = db.Table<StatusMeeting> ().ToList ();
					Console.WriteLine ("Se inserto en tabla StatusMeeting:");
					foreach (StatusMeeting m in content) {
						Console.WriteLine ("Id: " + m.Id + " Name:" + m.Status);
					}
				}
				db.Close ();
			}
		}


		public void InsertStatusVerification(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<StatusVerification> ();
				if (db.Table<StatusVerification> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/status_verification.txt", act);
					List<StatusVerification> entities = new List<StatusVerification> (); 
					foreach (String[] line in data) {
						try {
							StatusVerification model = new StatusVerification ();
							model.Id = int.Parse (line [0]);
							model.Name = line [1];
							model.Description = line [2];
							entities.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("StatusVerification error " + e.Message);
						}
					}
					db.InsertAll (entities);
					var content = db.Table<StatusVerification> ().ToList ();
					Console.WriteLine ("Se inserto en tabla StatusVerification:");
					foreach (StatusVerification m in content) {
						Console.WriteLine ("Id: " + m.Id + " Name:" + m.Name);
					}
				}
				db.Close ();
			}
		}


		public void InsertVerMethod(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<VerificationMethod> ();
				if (db.Table<VerificationMethod> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/verification_method.txt", act);
					List<VerificationMethod> entities = new List<VerificationMethod> (); 
					foreach (String[] line in data) {
						try {
							VerificationMethod model = new VerificationMethod ();
							model.Id = int.Parse (line [0]);
							model.Name = line [1];
							model.IsObsolete = line [2].Equals ("1");
							entities.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("error " + e.Message);
						}
					}
					db.InsertAll (entities);
					var content = db.Table<VerificationMethod> ().ToList ();
					Console.WriteLine ("Se inserto en tabla verification_method:");
					foreach (VerificationMethod m in content) {
						Console.WriteLine ("Id: " + m.Id + " Name:" + m.Name);
					}
				}
				db.Close ();
			}
		}


		public void InsertLocationCat(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<Location> ();
				if (db.Table<Location> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/location.txt", act);
					List<Location> entities = new List<Location> (); 
					foreach (String[] line in data) {
						try {
							Location model = new Location ();
							model.Id = int.Parse (line [0]);
							model.MunicipalityId = long.Parse (line [1]);
							model.Name = line [2];
							model.Abbreviation = line [3];
							model.Description = line [4];
							model.ZipCode = line [5];
							entities.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("Location error ***" + e.Message);
						}
					}
					db.InsertAll (entities);
//					var content = db.Table<Location> ().ToList();
//					Console.WriteLine ("Se inserto en tabla Location:" + content.Count);
//					foreach (Location m in content) {
//						Console.WriteLine ("Id: "+m.Id+" Name:"+m.Name);
//					}
				}
				db.Close ();
			}
		}


		public void InsertInformationAviability(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<InformationAviability> ();
				if (db.Table<InformationAviability> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/informationAvailability.txt", act);
					List<InformationAviability> entities = new List<InformationAviability> (); 
					foreach (String[] line in data) {
						try {
							InformationAviability model = new InformationAviability ();
							model.Id = int.Parse (line [0]);
							model.Name = line [1];
							model.IsObsolete = line [2].Equals ("1");
							model.Specification = line [3].Equals ("1");
							entities.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("informationAvailability error " + e.Message);
						}
					}
					db.InsertAll (entities);
					var content = db.Table<InformationAviability> ().ToList ();
					Console.WriteLine ("Se inserto en tabla InformationAviability:");
					foreach (InformationAviability m in content) {
						Console.WriteLine ("Id: " + m.Id + " Name:" + m.Name);
					}
				}
				db.Close ();
			}
		}


		public void InsertDistrict(Activity act){
			using (var db = FactoryConn.GetConn ()) {
				db.CreateTable<District> ();
				if (db.Table<District> ().Count () == 0) {
					IEnumerable<String[]> data = GetDataOfFile (ConstantsDb.CONTENT_FOLDER_CATALOG + "/district.txt", act);
					List<District> entities = new List<District> (); 
					foreach (String[] line in data) {
						try {
							District model = new District ();
							model.Id = int.Parse (line [0]);
							model.Name = line [1];
							model.IsObsolete = line [2].Equals ("1");
							entities.Add (model);
						} catch (Exception e) {
							Console.WriteLine ("error " + e.Message);
						}
					}
					db.InsertAll (entities);
					var content = db.Table<District> ().ToList ();
					Console.WriteLine ("Se inserto en tabla District:");
					foreach (District m in content) {
						Console.WriteLine ("Id: " + m.Id + " Name:" + m.Name);
					}
				}
				db.Close ();
			}
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