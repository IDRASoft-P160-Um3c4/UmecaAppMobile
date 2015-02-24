using System;
using System.IO;
namespace UmecaApp
{
	public static class ConstantsDB
	{
	
		public static String BASE_NAME="umecaTest.db3";
		public static String CONTENT_FOLDER_CATALOG = "dbCatalogs";
		public static String DB_PATH = Path.Combine (
			Environment.GetFolderPath (Environment.SpecialFolder.Personal),
			ConstantsDB.BASE_NAME);

	}
}

