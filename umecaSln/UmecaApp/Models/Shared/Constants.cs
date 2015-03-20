using System;

namespace UmecaApp
{
	public class Constants {
		public static long ELECTION_YES = 1L;
		public static long ELECTION_NO = 2L;
		public static String VERIFICATION_STATUS_NEW_SOURCE = "NEW_SOURCE";
		public static String VERIFICATION_STATUS_AUTHORIZED = "AUTHORIZED";
		public static String VERIFICATION_STATUS_MEETING_COMPLETE = "MEETING_COMPLETE";

		public static String VALUE_NOT_KNOW_SOURCE = "La fuente desconoce la información.";
		public static String UNABLE_VERIF_TEXT = "No hay forma de verificar la información";
		public static String UNABLE_VERIF_TEXT_DOC = "No fue posible verificar.";
		public static String[] TABS_MEETING = {"imputed", "imputedHome", "socialNetwork", "reference", "job", "school", "drug", "leaveCountry"};
		public static String[] ENTITIES_MEETING = {"imputed", "Domicilio", "Persona", "Referencia", "Trabajo", "school", "Sustancia", "leaveCountry"};
		public static String[] NAMES_MEETING = {"imputed.", "imputedHomes.", "socialNetwork.", "references.", "jobs.", "school.", "drugs.", "leaveCountry."};
		public static String ST_REQUEST_CHANGE_SOURCE = "CHANGE_STATUS_SOURCE";
		public static String ST_REQUEST_CASE_OBSOLETE = "CASE_OBSOLETE";
		public static String ST_REQUEST_EDIT_MEETING = "EDIT_MEETING";
		public static String ST_REQUEST_EDIT_LEGAL_INFORMATION = "EDIT_LEGAL_INFORMATION";
		public static String ST_REQUEST_EDIT_TECHNICAL_REVIEW = "EDIT_TECHNICAL_REVIEW";
		public static String RESPONSE_TYPE_DRESSED = "DRESSED";
		public static String ST_REQUEST_CASE_OBSOLETE_SUPERVISION = "CASE_OBSOLETE_SUPERVISION";
		public static String CASE_STATUS_REQUEST_SUPERVISION = "ST_CASE_REQUEST_SUPERVISION";
		public static String TYPE_COMMENT_OBSOLETE_CASE_SUPERVISION = "OBSOLETE_CASE_SUPERVISION";
		public static String RESPONSE_OBSOLETE_CASE_SUPERVISION = "Respuesta a solicitud de eliminar un caso";
		public static String S_MEETING_INCOMPLETE = "INCOMPLETE";
		public static String S_MEETING_INCOMPLETE_LEGAL = "INCOMPLETE_LEGAL";
		public static String S_MEETING_COMPLETE = "COMPLETE";
		public static String S_MEETING_COMPLETE_VERIFICATION = "COMPLETE_VERIFICATION";
		public static Boolean GENDER_FEMALE = true;
		public static Boolean GENDER_MALE = false;

		public static long REGYSTER_TYPE_CURRENT = 1L;
		public static long REGYSTER_TYPE_SECONDARY = 2L;
		public static long REGYSTER_TYPE_PREVIOUS = 3L;

		public static String TECHNICAL_REVIEW_QUESTIONARY_CODE = "TECHNICAL_REVIEW";
		public static int CONDITIONAL_REPRIEVE_HEARING = 1;
		public static int MEETING_HEARING = 2;

		public static String KEY_CYPHER_APP = "1234567890123481";

		//status del caso
		public static String CASE_STATUS_MEETING = "ST_CASE_MEETING";
		public static String CASE_STATUS_SOURCE_VALIDATION = "ST_CASE_SOURCE_VALIDATION";
		public static String ALPHA2_MEXICO = "MX";
		public static String CASE_STATUS_HEARING_FORMAT_INCOMPLETE = "ST_CASE_HEARING_FORMAT_INCOMPLETE";
		public static String CASE_STATUS_HEARING_FORMAT_END = "ST_CASE_HEARING_FORMAT_END";
		public static String CASE_STATUS_VERIFICATION_COMPLETE = "ST_CASE_VERIFICATION_COMPLETE";
		public static String CASE_STATUS_TECHNICAL_REVIEW = "ST_CASE_TECHNICAL_REVIEW_COMPLETE";
		public static String CASE_STATUS_INCOMPLETE_TECHNICAL_REVIEW = "ST_CASE_TECHNICAL_REVIEW_INCOMPLETE";
		public static String CASE_STATUS_VERIFICATION = "ST_CASE_VERIFICATION";

		public static String CASE_STATUS_CONDITIONAL_REPRIEVE = "ST_CASE_CONDITIONAL_REPRIEVE";
		public static String CASE_STATUS_FRAMING_INCOMPLETE = "ST_CASE_FRAMING_MEETING_INCOMPLETE";
		public static String CASE_STATUS_FRAMING_COMPLETE = "ST_CASE_FRAMING_MEETING_COMPLETE";
		public static String CASE_STATUS_REQUEST = "ST_CASE_REQUEST";
		public static String CASE_STATUS_EDIT_TEC_REV = "ST_CASE_EDIT_TEC_REV";
		public static String CASE_STATUS_NOT_PROSECUTE = "ST_CASE_NOT_PROSECUTE";
		public static String CASE_STATUS_NOT_PROSECUTE_OPEN = "ST_CASE_NOT_PROSECUTE_OPEN";

		//sataus field verification
		public static String ST_FIELD_VERIF_DONTKNOW = "DONT_KNOW";
		public static String ST_FIELD_VERIF_EQUALS = "EQUALS";
		public static String ST_FIELD_VERIF_NOEQUALS = "NO_EQUALS";
		public static String ST_FIELD_VERIF_UNABLE = "UNABLE_VERIFICATION";

		public static String ST_FIELD_VERIF_IMPUTED = "IS_IMPUTED";
		public static String CASE_STATUS_PRE_CLOSED = "ST_CASE_PRE_CLOSED";
		public static String CASE_STATUS_CLOSED = "ST_CASE_CLOSED";
		public static String CASE_STATUS_PRISON_CLOSED = "ST_CASE_PRISON_CLOSED";

		public static String ROLE_ADMIN = "ROLE_ADMIN";
		public static String ROLE_REVIEWER = "ROLE_REVIEWER";
		public static String ROLE_EVALUATION_MANAGER = "ROLE_EVALUATION_MANAGER";
		public static String ROLE_SUPERVISOR = "ROLE_SUPERVISOR";
		public static String ROLE_SUPERVISOR_MANAGER = "ROLE_SUPERVISOR_MANAGER";
		public static String ROLE_NOTUSE = "ROLE_NOTUSE";
		public static String ROLE_ANONYMOUS = "ANONYMOUS";

		public static String FORMAT_CALENDAR_I = "dd/MM/yyyy HH:mm";
		public static String FORMAT_VERIFICATION_DATE = "yyyy-MM-dd hh:mm:ss.S";

		public static String SYSTEM_SETTINGS_ARCHIVE = "ARCHIVE";
		public static String SYSTEM_SETTINGS_ARCHIVE_MAX_NUMBER_FILES = "MaxNumberFiles";
		public static String SYSTEM_SETTINGS_ARCHIVE_MAX_SIZE_FILES = "MaxSizeFiles";
		public static String SYSTEM_SETTINGS_ARCHIVE_PATH_TO_SAVE = "PathToSave";

		public static String SYSTEM_SETTINGS_MONPLAN = "MONPLAN";
		public static String SYSTEM_SETTINGS_MONPLAN_HOURS_TO_AUTHORIZE = "HoursToAuthorize";

		public static String SYSTEM_SETTINGS_SCHEDULE_HEARING = "SCHEDULE_HEARING";
		public static String SYSTEM_SETTINGS_SCHEDULE_LST_IDS_ARRANGEMENT = "LstIdsArrangement";
		public static String SYSTEM_SETTINGS_SCHEDULE_LST_IDS_ARRANGEMENT_REMINDER = "LstIdsArrangementReminder";
		public static String SYSTEM_SETTINGS_SCHEDULE_HEARING_SUPERVISION_ACTIVITY_ID = "SupervisionActivityId";
		public static String SYSTEM_SETTINGS_SCHEDULE_HEARING_SUPERVISION_ACTIVITY_ID_REMINDER = "SupervisionActivityIdReminder";
		public static String SYSTEM_SETTINGS_SCHEDULE_HEARING_GOAL_ACTIVITY_ID = "GoalActivityId";
		public static String SYSTEM_SETTINGS_SCHEDULE_HEARING_GOAL_ACTIVITY_ID_REMINDER = "GoalActivityIdReminder";
		public static String SYSTEM_SETTINGS_SCHEDULE_HEARING_DAYS_BEFORE_FOR_REMINDER = "DaysBeforeForReminder";


		public static String VERIFICATION_STATUS_COMPLETE = "VERIFICATION_COMPLETE";

		public static String STR_REVIEWER_NOTIF_NO_SOURCES = "NO_SOURCES";
		public static String STR_REVIEWER_NOTIF_SOURCES_NO_MEETING = "SOURCES_NO_MEETING";

		public static String CODE_S1_TEC_REV = "OT_S1";
		public static String CODE_S2_TEC_REV = "OT_S2";
		public static String CODE_S3_TEC_REV = "OT_S3";
		public static String CODE_S4_TEC_REV = "OT_S4";
		public static String CODE_S5_TEC_REV = "OT_S5";

		public static String TEC_REV_HIGH_RISK = "Riesgo alto!: Libertad muy difícil de cumplir.";
		public static String TEC_REV_MEDIUM_RISK = "Riesgo medio!: Se puede recomendar combinación de medidas cautelares en libertad bajo niveles de supervisión.";
		public static String TEC_REV_LOW_RISK = "Riesgo bajo!: Se puede recomendar combinación de medidas cautelares en libertad bajo niveles de supervisión.";
		public static String TEC_REV_MINIMUM_RISK = "Riesgo mínimo!: Se puede recomendar combinación de medidas cautelares en libertad bajo niveles de supervisión.";

		public static int[] IDS_TABS_MEETING = {1, 2, 3, 4, 5, 6, 7, 8};

		public static String RESPONSE_TYPE_PENDING = "PENDING";
		public static String RESPONSE_TYPE_ACCEPTED = "ACCEPTED";
		public static String RESPONSE_TYPE_REJECTED = "REJECTED";
		public static String CASE_STATUS_OBSOLETE_EVALUATION = "ST_CASE_OBSOLETE_EVALUATION";
		public static String CASE_STATUS_OBSOLETE_SUPERVISION = "ST_CASE_OBSOLETE_SUPERVISION";


		public static long MARITAL_SINGLE = 1L;
		public static long MARITAL_MARRIED = 2L;
		public static long MARITAL_DIVORCED = 3L;
		public static long MARITAL_UNION_FREE = 4L;
		public static long MARITAL_WIDOWER = 5L;

		public static long AC_LVL_ILLITERATE = 1L;
		public static long AC_LVL_PRIMARY = 2L;
		public static long AC_LVL_HIGH_SCH = 3L;
		public static long AC_LVL_BACHELOR = 4L;
		public static long AC_LVL_UNIVERSITY = 5L;
		public static long AC_LVL_GRADUATE = 6L;
		public static long AC_LVL_OTHER = 7L;

		public static long DRUG_ALCOHOL = 1L;
		public static long DRUG_MARIHUANA = 2L;
		public static long DRUG_COCAIN = 3L;
		public static long DRUG_HEROIN = 4L;
		public static long DRUG_OPIUM = 5L;
		public static long DRUG_PBC = 6L;
		public static long DRUG_SOLV = 7L;
		public static long DRUG_CEME = 8L;
		public static long DRUG_LSD = 9L;
		public static long DRUG_AMPH = 10L;
		public static long DRUG_META = 11L;
		public static long DRUG_EXTA = 12L;
		public static long DRUG_MUSH = 13L;
		public static long DRUG_OTHER = 14L;


		public static String ST_REQUEST_AUTHORIZE_SOURCE = "AUTHORIZE_SOURCES";
		public static String ST_REQUEST_NOT_PROSECUTE = "NOT_PROSECUTE";
		public static String ST_REQUEST_MONPLAN_AUTH = "MONPLAN_AUTH";
		public static String ST_REQUEST_UPDATE_MONPLAN_AUTH = "UPDATE_MONPLAN_AUTH";

		public static String NAME_RELATIONSHIP_IMPUTED = "Imputado";
		public static String NAME_RELATIONSHIP_OTHER = "Otro";
		public static String CODE_FILE_TECH_REVIEW = "FILE_TECHNICAL_REVIEW";
		public static String CODE_FILE_IMPUTED_PHOTO = "IMPUTED_PHOTO";

		public static String NAME_RELATIONSHIP_NONE = "Ninguno";

		public static String S_MEETING_OBSOLETE = "OBSOLETE";
		public static String ROLE_DIRECTOR = "ROLE_DIRECTOR";

		public static String ACTION_AUTHORIZE_LOG_COMMENT = "AUTORIZAR ELIMINAR CASO";

		////////////////////////////HearingFormatsConstants/////////////////////////////

		public static int MEETING_PROCEDURAL_RISK = 1;
		public static int MEETING_CONDITIONAL_REPRIEVE = 2;


		public static int HEARING_TYPE_MC = 1;
		public static int HEARING_TYPE_SCP = 2;

		public static int PROCESS_VINC_YES = 1;
		public static int PROCESS_VINC_NO = 2;
		public static int PROCESS_VINC_NO_REGISTER = 3;


		public static int EXTENSION_72 = 1;
		public static int EXTENSION_144 = 2;
		public static int EXTENSION_NO = 3;

		public static String FOLDER_CONDITIONAL_REPRIEVE_PREFIX = "C/SCPP/";

		public static int CONT_DET_LEGAL = 1;
		public static int CONT_DET_ILEGAL = 2;
		public static int CONT_DET_NO_REGISTER = 3;

		public static int IMP_FORM_YES = 1;
		public static int IMP_FORM_NO = 2;
		public static int IMP_FORM_NO_REGISTER = 3;


		public static long ID_PRISON_ARRANGEMENT_NAC = 4L;
		public static long ID_PRISON_ARRANGEMENT_LOC = 36L;
		public static long ID_IMPUTED_PROMISE_ARRANGEMENT_LOC = 37L;

		public static int IMPUTED_PRESENCE_YES = 1;
		public static int IMPUTED_PRESENCE_NO = 2;

		public static String DESCRIPTION_ASSIGNED_ARRANGEMENT_BY_SYSTEM = "ASIGNADA POR SISTEMA";

		/////////////////////////////////ConsMessage///////////////////////////////////////////

		public static String MSG_SUCCESS_UPSERT = "Se ha guardado la información con éxito.";
		public static String MSG_SUCCESS_DELETE = "Se ha eliminado la información con éxito.";
		public static String MSG_ERROR_UPSERT = "Ha ocurrido un error al guardar la información. Intente más tarde.";
		public static String MSG_ERROR_DELETE = "Ha ocurrido un error al eliminar la información. Intente más tarde.";


	}
}

