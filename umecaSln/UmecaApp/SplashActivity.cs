using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace UmecaApp
{
	[Activity (Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true, ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
	public class SplashActivity : AppCompatActivity
	{

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.LaunchScreen);

			new LaunchTask (Application.Context, this).Execute ();

		}

	}

	public class LaunchTask : AsyncTask
	{

		private readonly Context _context;
		private string _condition;
		private readonly Activity _activity;
		private TextView _textView;

		public LaunchTask (Context context, Activity activity)
		{
			_context = context;
			_activity = activity;
		}

		protected override void OnPreExecute ()
		{
			base.OnPreExecute ();
			_textView = _activity.FindViewById<TextView> (Resource.Id.textLaunch);
		}

		protected override Java.Lang.Object DoInBackground (params Java.Lang.Object[] @params)
		{
			try {
				
				InsertCatalogs catalogInserter = new InsertCatalogs ();

				PublishProgress (_context.GetString (Resource.String.S1));
				catalogInserter.InsertUserRoles (_activity);

				PublishProgress (_context.GetString (Resource.String.S2));
				catalogInserter.InsertRelationship (_activity);

				PublishProgress (_context.GetString (Resource.String.S3));
				catalogInserter.InsertDegree (_activity);

				PublishProgress (_context.GetString (Resource.String.S4));
				catalogInserter.InsertActivity (_activity);

				PublishProgress (_context.GetString (Resource.String.S5));
				catalogInserter.InsertDocumentType (_activity);

				PublishProgress (_context.GetString (Resource.String.S6));
				catalogInserter.InsertDrugType (_activity);

				PublishProgress (_context.GetString (Resource.String.S7));
				catalogInserter.InsertElection (_activity);

				PublishProgress (_context.GetString (Resource.String.S8));
				catalogInserter.InsertFieldVerification (_activity);

				PublishProgress (_context.GetString (Resource.String.S9));
				catalogInserter.InsertHearingType (_activity);

				PublishProgress (_context.GetString (Resource.String.S10));
				catalogInserter.InsertHomeType (_activity);

				PublishProgress (_context.GetString (Resource.String.S11));
				catalogInserter.InsertImmigrationDocument (_activity);

				PublishProgress (_context.GetString (Resource.String.S12));
				catalogInserter.InsertMaritalStatus (_activity);

				PublishProgress (_context.GetString (Resource.String.S13));
				catalogInserter.InsertPeriodicity (_activity);

				PublishProgress (_context.GetString (Resource.String.S14));
				catalogInserter.InsertStatusCase (_activity);

				PublishProgress (_context.GetString (Resource.String.S15));
				catalogInserter.InsertStatusFieldVerfication (_activity);

				PublishProgress (_context.GetString (Resource.String.S16));
				catalogInserter.InsertStatusMeeting (_activity);

				PublishProgress (_context.GetString (Resource.String.S17));
				catalogInserter.InsertStatusVerification (_activity);

				PublishProgress (_context.GetString (Resource.String.S18));
				catalogInserter.InsertLocationCat (_activity);

				PublishProgress (_context.GetString (Resource.String.S19));
				catalogInserter.InsertRegisterType (_activity);

				PublishProgress (_context.GetString (Resource.String.S20));
				catalogInserter.InsertArrangement (_activity);

				PublishProgress (_context.GetString (Resource.String.S21));
				catalogInserter.InsertGroupCrime (_activity);

				PublishProgress (_context.GetString (Resource.String.S22));
				catalogInserter.InsertCrimeCatalog (_activity);

				PublishProgress (_context.GetString (Resource.String.S25));
				catalogInserter.CreateTablesToConsult ();

				PublishProgress (_context.GetString (Resource.String.S26));
				catalogInserter.InsertVerMethod (_activity);

				CatalogServiceController services = new CatalogServiceController ();

				PublishProgress (_context.GetString (Resource.String.T1));
				services.tablesInit ();

				PublishProgress (_context.GetString (Resource.String.T2));
				services.CreateStatusCaseCatalog ();
				PublishProgress (_context.GetString (Resource.String.T3));
				services.CreateStatusMeetingCatalog ();
				PublishProgress (_context.GetString (Resource.String.T4));
				services.CreateElection ();
				PublishProgress (_context.GetString (Resource.String.T5));
				services.CreateCountryCatalog ();
				PublishProgress (_context.GetString (Resource.String.T6));
				services.CreateStateCatalog ();
				PublishProgress (_context.GetString (Resource.String.T7));
				services.CreateMunicipalityCatalog ();

			} catch (Exception e) {
				Console.WriteLine ("catched error at Main activity, InsertCatalogs.insertAllCatalogs() ");
				Console.WriteLine ("Error ::> " + e.Message);
				_condition = "Se detectó un error al crear las tablas y catálogos de la base de datos, la información está corrupta por favor instale de nuevo esta aplicación o contacte a soporte técnico.";
			}

			return true;
		}

		protected override void OnProgressUpdate (params Java.Lang.Object[] values)
		{
			base.OnProgressUpdate (values);
			_textView.Text = values [0].ToString ();
		}


		protected override void OnPostExecute (Java.Lang.Object result)
		{
			base.OnPostExecute (result);

			_textView.Text = _context.GetString (Resource.String.SyncEnd);
			var mainActivity = new Intent (_context, typeof(MainActivity));
			mainActivity.PutExtra ("condition", _condition);
			_activity.StartActivity (mainActivity);

		}
	}


}