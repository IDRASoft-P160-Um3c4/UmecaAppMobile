
using SQLite.Net.Attributes;

namespace UmecaApp
{
	public class User
	{
		[PrimaryKey, AutoIncrement, Column("id_user")]
		public int Id { get; set; }

		//		@NotEmpty(message="El usuario es un campo requerido")
		[Column("username"),MaxLength(200)]
		public string username { get; set; }	

		//		@NotEmpty(message="Contraseña es un campo requerido")
		[Column("password"),MaxLength(1000)]
		public string FirstName { get; set; }

//		@NotEmpty(message="Confirmación es un campo requerido")
		public string confirm{ get; set; }

		[Column("fullname"),MaxLength(500)]
//		@NotEmpty(message="Nombre completo es un campo requerido")
		public string fullname{ get; set; }

		[Column("email"),MaxLength(1000)]
//		@NotEmpty(message="Correo electrónico es un campo requerido")
		public string email{ get; set; }

		[Column("enabled")]
		public bool enabled{ get; set; }

		private bool hasChangePass{ get; set; }

		public User ()
		{
		}
	}
}

