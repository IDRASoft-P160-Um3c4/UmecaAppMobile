
using SQLite.Net.Attributes;

namespace UmecaApp
{
	[Table("config")]
	public class Configuracion
	{
		[PrimaryKey, Column("id")]
		public int Id { get; set; }

		[Column("url"),MaxLength(1000)]
		public string url { get; set; }

		[Column("description"),MaxLength(1024)]
		public string Description { get; set; }

	}
}

