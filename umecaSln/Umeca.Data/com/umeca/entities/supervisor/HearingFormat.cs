using System;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;

namespace Umeca.Data
{
	[Table("hearing_format")]
	public class HearingFormat
	{
		public HearingFormat ()
		{
		}

		[PrimaryKey, AutoIncrement, Column("id_hearing_format")]
		public int Id{ get; set; }

		[Column("register_timestamp")]
		public DateTime? RegisterTime{ get; set; }

		[Column("id_folder")]
		public String IdFolder{ get; set; }

		[Column("id_judicial")]
		public String IdJudicial{ get; set; }

		[Column("room")]
		public String Room{ get; set; }

		[Column("appointment_date")]
		public DateTime? AppointmentDate{ get; set; }

		[Column("init_time")]
		public DateTime? InitTime{ get; set; }

		[Column("end_time")]
		public DateTime? EndTime{ get; set; }

		[Column("judge_name")]
		public String JudgeName{ get; set; }

		[Column("mp_name")]
		public String MpName{ get; set; }

		[Column("defender_name")]
		public String DefenderName{ get; set; }

		[Column("terms"),MaxLength(1000)]
		public String Terms{ get; set; }

		[Column("confirm_comment"),MaxLength(1000)]
		public String ConfirmComment{ get; set; }

		[Column("is_finished")]
		public Boolean IsFinished{ get; set; }

		[Column("comments"),MaxLength(1000)]
		public String Comments{ get; set; }

		[Column("umeca_date")]
		public DateTime? UmecaDate{ get; set; }

		[Column("umeca_time")]
		public DateTime? UmecaTime{ get; set; }

		[Column("id_user_umeca")]
		public int UmecaSupervisor{ get; set; }

		//HearingType id reference
		[Column("id_hearing_type")]
		public int HearingType{ get; set; }

		[Column("hearing_type_spec")]
		public String HearingTypeSpecification{ get; set; }

		[Column("imputed_presence")]
		public int ImputedPresence{ get; set; }

		[Column("hearing_result")]
		public String HearingResult{ get; set; }

		[Column("previous_hearing")]
		public int PreviousHearing{ get; set; }

		[Column("id_format_specs")]
		public int HearingFormatSpecs{ get; set; }

		[Column("id_case")]
		public int CaseDetention{ get; set; }

		[Column("id_user")]
		public int Supervisor{ get; set; }

//		HearingFormatImputed
		[Column("id_hearing_format_imputed")]
		public int hearingImputed{ get; set; }

		[Column("show_notification")]
		public Boolean ShowNotification{ get; set; }

		//		@OneToMany(mappedBy = "hearingFormat", fetch = FetchType.LAZY, cascade = CascadeType.ALL)
		//		private List<AssignedArrangement> assignedArrangements;
		//		@OneToMany(mappedBy = "hearingFormat", fetch = FetchType.LAZY, cascade = CascadeType.ALL)
		//		private List<ContactData> contacts;
		//		@OneToMany(mappedBy="hearingFormat", cascade={CascadeType.ALL})
		//		private List<Crime> crimeList;
	}
}