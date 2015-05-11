using System;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;

namespace UmecaApp
{
	[Table("field_meeting_source")]
	public class FieldMeetingSource
	{

		public FieldMeetingSource ()
		{
		}

		public FieldMeetingSource(String value, String jsonValue) {
			this.Value = value;
			this.JsonValue = jsonValue;
		}

		public FieldMeetingSource(String value, String jsonValue, Boolean isFinal) {
			this.Value = value;
			this.JsonValue = jsonValue;
			this.IsFinal  = isFinal;
		}
		public FieldMeetingSource(String value, String jsonValue, int idFieldList) {
			this.Value = value;
			this.JsonValue = jsonValue;
			this.IdFieldList = idFieldList;
		}

		public FieldMeetingSource(String value, String jsonValue, Boolean aFinal, int idFieldList) {
			this.Value = value;
			this.JsonValue = jsonValue;
			this.IsFinal = aFinal;
			this.IdFieldList = idFieldList;
		}

		[PrimaryKey, AutoIncrement, Column("id_field_meeting_source\t")]
		public int Id{ get; set; }

		[ForeignKey(typeof(SourceVerification)),Column("id_source_verification")]
		public int? SourceVerificationId{ get; set; }

		[ForeignKey(typeof(FieldVerification)),Column("id_field")]
		public int? FieldVerificationId{ get; set; }

		[Column("value"),MaxLength(1000)]
		public String Value{ get; set; }

		[Column("value_json"),MaxLength(1000)]
		public String JsonValue{ get; set; }

		[ForeignKey(typeof(StatusFieldVerification)),Column("id_status")]
		public int? StatusFieldVerificationId{ get; set; }

		[Column("is_final")]
		public Boolean? IsFinal{get; set;}

		[Column("id_filed_list")]
		public int? IdFieldList{ get; set; }

		[Column("reason"),MaxLength(1000)]
		public String Reason{ get; set; }

	}
}

