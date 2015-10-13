using System;
//listas
using Umeca.Data;
using System.Collections.Generic;

namespace UmecaApp
{
	public class VerificationMeetingSourceDto
	{

		public VerificationMeetingSourceDto ()
		{
		}

		public int CaseId { get; set; }

		public String IdFolder { get; set; }

		public int SourceId { get; set; }

		public int SourceAge { get; set; }

		public String SourceName{ get; set; }

		public String SourceRelationshipString{ get; set; }

		public String SourcePhone{ get; set; }

		public String SourceAddress{ get; set; }

		public int ImputedId { get; set; }

		public String Name{ get; set; }

		public String LastNameP{ get; set; }

		public String LastNameM{ get; set; }

		public String FoneticString{ get; set; }

		public Boolean? Gender{ get; set; }

		public DateTime BirthDate{ get; set; }

		public int ageString{ get; set; }

		public String CelPhone{ get; set; }

		public int? YearsMaritalStatus{ get; set; }

		public int? MaritalStatusId { get; set; }

		public MaritalStatus MaritalStatus { get; set; }

		public int? Boys{ get; set; }

		public int? DependentBoys{ get; set; }

		public int? BirthCountry{ get; set; }

		public String BirthMunicipality{ get; set; }

		public String BirthState{ get; set; }

		public String BirthLocation{ get; set; }

		public String Nickname{ get; set; }

		public int? LocationId { get; set; }

		public Location Location { get; set; }

		public int? MeetingId { get; set; }

		public Meeting Meeting { get; set; }
		//End of Imputed

		//SocialEnvironment get by id_meeting
		public int SocialEnvironmentId{ get; set; }

		public String PhysicalCondition{ get; set; }

		public String comment{ get; set; }
		//EndSocialEnvironment

		//listActivity
		public String Activities{ get; set; }
		//EndlistActivity

		public int? ReviewerId{ get; set; }


		public User Reviewer { get; set; }


		public int? StatusMeetingId { get; set; }


		public StatusMeeting StatusMeeting { get; set; }


		public int MeetingType{ get; set; } 


		public String CommentReference{ get; set; }


		public String CommentJob{ get; set; }


		public String CommentSchool{ get; set; }


		public String CommentCountry{ get; set; }

		//		[Column("comment_home"),MaxLength(500)]
		public String CommentHome{ get; set; }

		//		[Column("comment_drug"),MaxLength(500)]
		public String CommentDrug{ get; set; }


		//social network
		public String CommentSocialNetwork{ get; set; }

		//historial escolar
		public int SchoolId{ get; set; }

		public String SchoolName{ get; set; }

		public String SchoolPhone{ get; set; }

		public String SchoolAddress{ get; set; }

		public int SchoolDegreeId{ get; set; }

		public String SchoolSpecification{ get; set; }

		public Boolean SchoolBlock{ get; set; }

		public List<Schedule> ScheduleSchool{ get; set; }
		//END historial escolar


		///////////////identified by meeting///////////
		public int? OfficialDocumentationId{ get; set; }

		public int? LivedCountryId{ get; set; }

		public String timeAgo{ get; set; }

		public String Reason{ get; set; }

		public int? FamilyAnotherCountryId{ get; set; }

		public int? CommunicationFamilyId{ get; set; }

		public int? CountryId{ get; set; }

		public String State{ get; set; }

		public String Media{ get; set; }

		public String Address{ get; set; }

		public int? ImmigrationDocumentId{ get; set; }

		public int? RelationshipId{ get; set; }

		public String TimeResidence{ get; set; }

		public String SpecficationImmigranDoc{ get; set; }

		public String SpecificationRelationship{ get; set; }
		//////////////





		//		[Column("date_create")]
		public DateTime? DateCreate{ get; set; }

		//		[Column("date_terminate")]
		public DateTime? DateTerminate{ get; set; }

		public String JsonMeeting { get; set; }

		public String JsonCountrys { get; set; }
		public String JsonStates { get; set; }
		public String JsonMunycipality { get; set; }

		public String JsonActivities { get; set; }

		public List<DomiciliosVerificationDto> JsonDomicilios { get; set; }

		public List<PersonSocialNetworkVerificationDto> JsonPersonSN { get; set; }

		public List<Reference> JsonReferences { get; set; }

		public List<JobVerificationDto> JsonJobs { get; set; }

		public List<Drug> JsonDrugs { get; set; }

		public String JsonElection { get; set; }

		//RESOURCES

		public List<Election> ListaDeElection { get; set; }

		public List<Relationship> ListaDeRelaciones { get; set; }

		public List<DocumentType> ListaDeIdentificaciones { get; set; }

		public List<DrugType> ListaDeDrogas { get; set; }

		public List<Periodicity> ListaDePeriodicidad { get; set; }

		public List<RegisterType> ListaDeRegisterType { get; set; }

	}
}

