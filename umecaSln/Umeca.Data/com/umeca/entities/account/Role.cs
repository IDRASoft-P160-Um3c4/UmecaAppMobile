
 using SQLite;

namespace UmecaApp
{
	[Table("role")]
	public class Role
	{
		[PrimaryKey, Column("id_role")]
		public int Id { get; set; }

		[Column("role"),MaxLength(100)]
		public string role { get; set; }

		[Column("description"),MaxLength(1024)]
		public string Description { get; set; }

	}
}

