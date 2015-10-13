using SQLite;

namespace Umeca.Data
{
	public static class FactoryConn
	{
		public static SQLiteConnection GetConn(){
			return new SQLiteConnection(ConstantsDb.DB_PATH, false);
		}
	}
}

