using System;
using SQLite.Net;
//insert with children etc
using SQLiteNetExtensions.Extensions;
//query to list
using System.Linq;
//listas
using System.Collections.Generic;

namespace UmecaApp
{
	public class CatalogServiceController
	{

		readonly SQLiteConnection db;

		public CatalogServiceController(SQLiteConnection dbConection)
		{
			this.db = dbConection;
		}
		public CatalogServiceController ()
		{
			db = new SQLiteConnection(
				new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
				ConstantsDB.DB_PATH,
				true,
				null // (can be null in which case you will need to provide tables that only use supported data types)
			);
		}

		public void CreateStatusCaseCatalog(){
			db.DropTable<StatusCase> ();
			db.CreateTable<StatusCase> ();

			StatusCase catalogo1  = new StatusCase(); catalogo1 .Description="Verificación finalizada"                                ; catalogo1                .Name="ST_CASE_VERIFICATION_COMPLETE";                           db.Insert(catalogo1 );
			StatusCase catalogo2  = new StatusCase(); catalogo2 .Description="Instrumento de evaluación finalizado"                   ; catalogo2                             .Name="ST_CASE_TECHNICAL_REVIEW_COMPLETE";          db.Insert(catalogo2 );
			StatusCase catalogo3  = new StatusCase(); catalogo3 .Description="Entrevista de riesgos procesales"                       ; catalogo3                         .Name="ST_CASE_MEETING";                                db.Insert(catalogo3 );
			StatusCase catalogo4  = new StatusCase(); catalogo4 .Description="Formato de audiencia finalizado."                       ; catalogo4                         .Name="ST_CASE_HEARING_FORMAT_END";                     db.Insert(catalogo4 );
			StatusCase catalogo5  = new StatusCase(); catalogo5 .Description="Validación de fuentes"                                  ; catalogo5              .Name="ST_CASE_SOURCE_VALIDATION";                                 db.Insert(catalogo5 );
			StatusCase catalogo6  = new StatusCase(); catalogo6 .Description="Entrevista de encuadre incompleta."                     ; catalogo6                           .Name="ST_CASE_FRAMING_MEETING_INCOMPLETE";           db.Insert(catalogo6 );
			StatusCase catalogo7  = new StatusCase(); catalogo7 .Description="Entrevista de encuadre completa."                       ; catalogo7                         .Name="ST_CASE_FRAMING_MEETING_COMPLETE";               db.Insert(catalogo7 );
			StatusCase catalogo8  = new StatusCase(); catalogo8 .Description="Nuevo caso."                                            ; catalogo8    .Name="ST_CASE_CONDITIONAL_REPRIEVE";                                        db.Insert(catalogo8 );
			StatusCase catalogo9  = new StatusCase(); catalogo9 .Description="En verificación"                                        ; catalogo9        .Name="ST_CASE_VERIFICATION";                                            db.Insert(catalogo9 );
			StatusCase catalogo10 = new StatusCase(); catalogo10.Description="Pre-cerrado por vinculación a proceso NO."              ; catalogo10                                 .Name="ST_CASE_PRE_CLOSED";                    db.Insert(catalogo10);
			StatusCase catalogo11 = new StatusCase(); catalogo11.Description="Cerrado."                                               ; catalogo11.Name="ST_CASE_CLOSED";                                                         db.Insert(catalogo11);
			StatusCase catalogo12 = new StatusCase(); catalogo12.Description="En solicitud pendiente"                                 ; catalogo12              .Name="ST_CASE_REQUEST";                                          db.Insert(catalogo12);
			StatusCase catalogo13 = new StatusCase(); catalogo13.Description="Edición de instrumento de evaluación autorizada"        ; catalogo13                                       .Name="ST_CASE_EDIT_TEC_REV";            db.Insert(catalogo13);
			StatusCase catalogo14 = new StatusCase(); catalogo14.Description="Caso eliminado en evaluación"                           ; catalogo14                    .Name="ST_CASE_OBSOLETE_EVALUATION";                        db.Insert(catalogo14);
			StatusCase catalogo15 = new StatusCase(); catalogo15.Description="Formato de audiencia incompleto."                       ; catalogo15                        .Name="ST_CASE_HEARING_FORMAT_INCOMPLETE";              db.Insert(catalogo15);
			StatusCase catalogo16 = new StatusCase(); catalogo16.Description="Instrumento de evaluación incompleto."                  ; catalogo16                             .Name="ST_CASE_TECHNICAL_REVIEW_INCOMPLETE";       db.Insert(catalogo16);
			StatusCase catalogo17 = new StatusCase(); catalogo17.Description="No judializado."                                        ; catalogo17       .Name="ST_CASE_NOT_PROSECUTE";                                           db.Insert(catalogo17);
			StatusCase catalogo18 = new StatusCase(); catalogo18.Description="No judicializado abierto."                              ; catalogo18                 .Name="ST_CASE_NOT_PROSECUTE_OPEN";                            db.Insert(catalogo18);
			StatusCase catalogo19 = new StatusCase(); catalogo19.Description="Cerrado por prisión preventiva / promesa del imputado." ; catalogo19                                              .Name="ST_CASE_PRISON_CLOSED";    db.Insert(catalogo19);
			StatusCase catalogo20 = new StatusCase(); catalogo20.Description="Caso eliminado en supervisión"                          ; catalogo20                     .Name="ST_CASE_OBSOLETE_SUPERVISION";                      db.Insert(catalogo20);
			StatusCase catalogo21 = new StatusCase(); catalogo21.Description="En solicitud pendiente"                                 ; catalogo21              .Name="ST_CASE_REQUEST_SUPERVISION";                              db.Insert(catalogo21);

			var tableStatusCase = db.Table<StatusCase>().ToList<StatusCase>();
			Console.WriteLine("StatusCase select * query------------->");
			foreach(var c in tableStatusCase){
				Console.WriteLine("id:"+c.Id+", name:"+c.Name+", Descripcion:"+c.Description);
			}
		}

		public void CreateStatusMeetingCatalog(){
			db.DropTable<StatusMeeting> ();
			db.CreateTable<StatusMeeting> ();


			StatusMeeting incomplete = new StatusMeeting ();
			incomplete.Status = "INCOMPLETE";
			incomplete.Description = "Entrevista incompleta";
			db.Insert (incomplete);

			StatusMeeting incompleteL = new StatusMeeting ();
			incompleteL.Status = "INCOMPLETE_LEGAL";
			incompleteL.Description = "Por agregar información legal";
			db.Insert (incompleteL);

			StatusMeeting complete = new StatusMeeting ();
			complete.Status = "COMPLETE";
			complete.Description = "Finalizado";
			db.Insert (complete);

			StatusMeeting completeV = new StatusMeeting ();
			completeV.Status = "COMPLETE_VERIFICATION";
			completeV.Description = "Verificación finalizada";
			db.Insert (completeV);

			StatusMeeting obsolete = new StatusMeeting ();
			obsolete.Status = "OBSOLETE";
			obsolete.Description = "Caso eliminado";
			db.Insert (obsolete);

			var tableStatusMeeting = db.Table<StatusMeeting> ().ToList<StatusMeeting> ();
			Console.WriteLine ("StatusMeeting all query------------->");
			foreach (var c in tableStatusMeeting) {
				Console.WriteLine ("id:" + c.Id + ", Status:" + c.Status + ", Description:" + c.Description);
			}
		}

		public void CreateCountryCatalog(){
			db.DropTable<Country> ();
			db.CreateTable<Country> ();

			List<Country> nCntr = new List<Country>();
			nCntr.Add(new Country(1, "Mexico", "MX", "MEX", 23, -102));
			nCntr.Add(new Country(4, "Afghanistan", "AF", "AFG", 33, 65));
			nCntr.Add(new Country(8, "Albania", "AL", "ALB", 41, 20));
			nCntr.Add(new Country(10, "Antarctica", "AQ", "ATA", -90, 0));
			nCntr.Add(new Country(12, "Algeria", "DZ", "DZA", 28, 3));
			nCntr.Add(new Country(16, "American Samoa", "AS", "ASM", -14.3333, -170));
			nCntr.Add(new Country(20, "Andorra", "AD", "AND", 42.5, 1.6));
			nCntr.Add(new Country(24, "Anla", "AO", "A", -12.5, 18.5));
			nCntr.Add(new Country(28, "Antigua and Barbuda", "AG", "ATG", 17.05, -61.8));
			nCntr.Add(new Country(31, "Azerbaija", "AZ", "AZE", 40.5, 47.5));
			nCntr.Add(new Country(32, "Argentina", "AR", "ARG", -34, -64));
			nCntr.Add(new Country(36, "Australia", "AU", "AUS", -27, 133));
			nCntr.Add(new Country(40, "Austria", "AT", "AUT", 47.3333, 13.3333));
			nCntr.Add(new Country(44, "Bahamas", "BS", "BHS", 24.25, -76));
			nCntr.Add(new Country(48, "Bahrai", "BH", "BHR", 26, 50.55));
			nCntr.Add(new Country(50, "Bangladesh", "BD", "BGD", 24, 90));
			nCntr.Add(new Country(51, "Armenia", "AM", "ARM", 40, 45));
			nCntr.Add(new Country(52, "Barbados", "BB", "BRB", 13.1667, -59.5333));
			nCntr.Add(new Country(56, "Belgium", "BE", "BEL", 50.8333, 4));
			nCntr.Add(new Country(60, "Bermuda", "BM", "BMU", 32.3333, -64.75));
			nCntr.Add(new Country(64, "Bhuta", "BT", "BT", 27.5, 90.5));
			nCntr.Add(new Country(68, "Bolivia, Plurinational State of", "BO", "BOL", -17, -65));
			nCntr.Add(new Country(70, "Bosnia and Herzevina", "BA", "BIH", 44, 18));
			nCntr.Add(new Country(72, "Botswana", "BW", "BWA", -22, 24));
			nCntr.Add(new Country(74, "Bouvet Island", "BV", "BVT", -54.4333, 3.4));
			nCntr.Add(new Country(76, "Brazil", "BR", "BRA", -10, -55));
			nCntr.Add(new Country(84, "Belize", "BZ", "BLZ", 17.25, -88.75));
			nCntr.Add(new Country(86, "British Indian Ocean Territory", "IO", "IOT", -6, 71.5));
			nCntr.Add(new Country(90, "Solomon Islands", "SB", "SLB", -8, 159));
			nCntr.Add(new Country(92, "Virgin Islands, British", "VG", "VGB", 18.5, -64.5));
			nCntr.Add(new Country(96, "Brunei Darussalam", "B", "BR", 4.5, 114.6667));
			nCntr.Add(new Country(100, "Bulgaria", "BG", "BGR", 43, 25));
			nCntr.Add(new Country(104, "Myanmar", "MM", "MMR", 22, 98));
			nCntr.Add(new Country(108, "Burundi", "BI", "BDI", -3.5, 30));
			nCntr.Add(new Country(112, "Belarus", "BY", "BLR", 53, 28));
			nCntr.Add(new Country(116, "Cambodia", "KH", "KHM", 13, 105));
			nCntr.Add(new Country(120, "Cameroo", "CM", "CMR", 6, 12));
			nCntr.Add(new Country(124, "Canada", "CA", "CA", 60, -95));
			nCntr.Add(new Country(132, "Cape Verde", "CV", "CPV", 16, -24));
			nCntr.Add(new Country(136, "Cayman Islands", "KY", "CYM", 19.5, -80.5));
			nCntr.Add(new Country(140, "Central African Republic", "CF", "CAF", 7, 21));
			nCntr.Add(new Country(144, "Sri Lanka", "LK", "LKA", 7, 81));
			nCntr.Add(new Country(148, "Chad", "TD", "TCD", 15, 19));
			nCntr.Add(new Country(152, "Chile", "CL", "CHL", -30, -71));
			nCntr.Add(new Country(156, "China", "C", "CH", 35, 105));
			nCntr.Add(new Country(158, "Taiwan, Province of China", "TW", "TW", 23.5, 121));
			nCntr.Add(new Country(162, "Christmas Island", "CX", "CXR", -10.5, 105.6667));
			nCntr.Add(new Country(166, "Cocos (Keeling) Islands", "CC", "CCK", -12.5, 96.8333));
			nCntr.Add(new Country(170, "Colombia", "CO", "COL", 4, -72));
			nCntr.Add(new Country(174, "Comoros", "KM", "COM", -12.1667, 44.25));
			nCntr.Add(new Country(175, "Mayotte", "YT", "MYT", -12.8333, 45.1667));
			nCntr.Add(new Country(178, "Co", "CG", "COG", -1, 15));
			nCntr.Add(new Country(180, "Con, the Democratic Republic of the", "CD", "COD", 0, 25));
			nCntr.Add(new Country(184, "Cook Islands", "CK", "COK", -21.2333, -159.7667));
			nCntr.Add(new Country(188, "Costa Rica", "CR", "CRI", 10, -84));
			nCntr.Add(new Country(191, "Croatia", "HR", "HRV", 45.1667, 15.5));
			nCntr.Add(new Country(192, "Cuba", "CU", "CUB", 21.5, -80));
			nCntr.Add(new Country(196, "Cyprus", "CY", "CYP", 35, 33));
			nCntr.Add(new Country(203, "Czech Republic", "CZ", "CZE", 49.75, 15.5));
			nCntr.Add(new Country(204, "Beni", "BJ", "BE", 9.5, 2.25));
			nCntr.Add(new Country(208, "Denmark", "DK", "DNK", 56, 10));
			nCntr.Add(new Country(212, "Dominica", "DM", "DMA", 15.4167, -61.3333));
			nCntr.Add(new Country(214, "Dominican Republic", "DO", "DOM", 19, -70.6667));
			nCntr.Add(new Country(218, "Ecuador", "EC", "ECU", -2, -77.5));
			nCntr.Add(new Country(222, "El Salvador", "SV", "SLV", 13.8333, -88.9167));
			nCntr.Add(new Country(226, "Equatorial Guinea", "GQ", "GNQ", 2, 10));
			nCntr.Add(new Country(231, "Ethiopia", "ET", "ETH", 8, 38));
			nCntr.Add(new Country(232, "Eritrea", "ER", "ERI", 15, 39));
			nCntr.Add(new Country(233, "Estonia", "EE", "EST", 59, 26));
			nCntr.Add(new Country(234, "Faroe Islands", "FO", "FRO", 62, -7));
			nCntr.Add(new Country(238, "Falkland Islands (Malvinas)", "FK", "FLK", -51.75, -59));
			nCntr.Add(new Country(239, "South Georgia and the South Sandwich Islands", "GS", "SGS", -54.5, -37));
			nCntr.Add(new Country(242, "Fiji", "FJ", "FJI", -18, 175));
			nCntr.Add(new Country(246, "Finland", "FI", "FI", 64, 26));
			nCntr.Add(new Country(248, "Åland Islands", "AX", "ALA", 60.116667, 19.9));
			nCntr.Add(new Country(250, "France", "FR", "FRA", 46, 2));
			nCntr.Add(new Country(254, "French Guiana", "GF", "GUF", 4, -53));
			nCntr.Add(new Country(258, "French Polynesia", "PF", "PYF", -15, -140));
			nCntr.Add(new Country(260, "French Southern Territories", "TF", "ATF", -43, 67));
			nCntr.Add(new Country(262, "Djibouti", "DJ", "DJI", 11.5, 43));
			nCntr.Add(new Country(266, "Gabo", "GA", "GAB", -1, 11.75));
			nCntr.Add(new Country(268, "Georgia", "GE", "GEO", 42, 43.5));
			nCntr.Add(new Country(270, "Gambia", "GM", "GMB", 13.4667, -16.5667));
			nCntr.Add(new Country(275, "Palestinian Territory, Occupied", "PS", "PSE", 32, 35.25));
			nCntr.Add(new Country(276, "Germany", "DE", "DEU", 51, 9));
			nCntr.Add(new Country(288, "Ghana", "GH", "GHA", 8, -2));
			nCntr.Add(new Country(292, "Gibraltar", "GI", "GIB", 36.1833, -5.3667));
			nCntr.Add(new Country(296, "Kiribati", "KI", "KIR", 1.4167, 173));
			nCntr.Add(new Country(300, "Greece", "GR", "GRC", 39, 22));
			nCntr.Add(new Country(304, "Greenland", "GL", "GRL", 72, -40));
			nCntr.Add(new Country(308, "Grenada", "GD", "GRD", 12.1167, -61.6667));
			nCntr.Add(new Country(312, "Guadeloupe", "GP", "GLP", 16.25, -61.5833));
			nCntr.Add(new Country(316, "Guam", "GU", "GUM", 13.4667, 144.7833));
			nCntr.Add(new Country(320, "Guatemala", "GT", "GTM", 15.5, -90.25));
			nCntr.Add(new Country(324, "Guinea", "G", "GI", 11, -10));
			nCntr.Add(new Country(328, "Guyana", "GY", "GUY", 5, -59));
			nCntr.Add(new Country(332, "Haiti", "HT", "HTI", 19, -72.4167));
			nCntr.Add(new Country(334, "Heard Island and McDonald Islands", "HM", "HMD", -53.1, 72.5167));
			nCntr.Add(new Country(336, "Holy See (Vatican City State)", "VA", "VAT", 41.9, 12.45));
			nCntr.Add(new Country(340, "Honduras", "H", "HND", 15, -86.5));
			nCntr.Add(new Country(344, "Hong Kong", "HK", "HKG", 22.25, 114.1667));
			nCntr.Add(new Country(348, "Hungary", "HU", "HU", 47, 20));
			nCntr.Add(new Country(352, "Iceland", "IS", "ISL", 65, -18));
			nCntr.Add(new Country(356, "India", "I", "IND", 20, 77));
			nCntr.Add(new Country(360, "Indonesia", "ID", "ID", -5, 120));
			nCntr.Add(new Country(364, "Iran, Islamic Republic of", "IR", "IR", 32, 53));
			nCntr.Add(new Country(368, "Iraq", "IQ", "IRQ", 33, 44));
			nCntr.Add(new Country(372, "Ireland", "IE", "IRL", 53, -8));
			nCntr.Add(new Country(376, "Israel", "IL", "ISR", 31.5, 34.75));
			nCntr.Add(new Country(380, "Italy", "IT", "ITA", 42.8333, 12.8333));
			nCntr.Add(new Country(384, "Côte d'Ivoire", "CI", "CIV", 8, -5));
			nCntr.Add(new Country(388, "Jamaica", "JM", "JAM", 18.25, -77.5));
			nCntr.Add(new Country(392, "Japa", "JP", "JP", 36, 138));
			nCntr.Add(new Country(398, "Kazakhsta", "KZ", "KAZ", 48, 68));
			nCntr.Add(new Country(400, "Jorda", "JO", "JOR", 31, 36));
			nCntr.Add(new Country(404, "Kenya", "KE", "KE", 1, 38));
			nCntr.Add(new Country(408, "Korea, Democratic People's Republic of", "KP", "PRK", 40, 127));
			nCntr.Add(new Country(410, "Korea, Republic of", "KR", "KOR", 37, 127.5));
			nCntr.Add(new Country(414, "Kuwait", "KW", "KWT", 29.3375, 47.6581));
			nCntr.Add(new Country(417, "Kyrgyzsta", "KG", "KGZ", 41, 75));
			nCntr.Add(new Country(418, "Lao People's Democratic Republic", "LA", "LAO", 18, 105));
			nCntr.Add(new Country(422, "Lebano", "LB", "LB", 33.8333, 35.8333));
			nCntr.Add(new Country(426, "Lesotho", "LS", "LSO", -29.5, 28.5));
			nCntr.Add(new Country(428, "Latvia", "LV", "LVA", 57, 25));
			nCntr.Add(new Country(430, "Liberia", "LR", "LBR", 6.5, -9.5));
			nCntr.Add(new Country(434, "Libyan Arab Jamahiriya", "LY", "LBY", 25, 17));
			nCntr.Add(new Country(438, "Liechtenstei", "LI", "LIE", 47.1667, 9.5333));
			nCntr.Add(new Country(440, "Lithuania", "LT", "LTU", 56, 24));
			nCntr.Add(new Country(442, "Luxembourg", "LU", "LUX", 49.75, 6.1667));
			nCntr.Add(new Country(446, "Macao", "MO", "MAC", 22.1667, 113.55));
			nCntr.Add(new Country(450, "Madagascar", "MG", "MDG", -20, 47));
			nCntr.Add(new Country(454, "Malawi", "MW", "MWI", -13.5, 34));
			nCntr.Add(new Country(458, "Malaysia", "MY", "MYS", 2.5, 112.5));
			nCntr.Add(new Country(462, "Maldives", "MV", "MDV", 3.25, 73));
			nCntr.Add(new Country(466, "Mali", "ML", "MLI", 17, -4));
			nCntr.Add(new Country(470, "Malta", "MT", "MLT", 35.8333, 14.5833));
			nCntr.Add(new Country(474, "Martinique", "MQ", "MTQ", 14.6667, -61));
			nCntr.Add(new Country(478, "Mauritania", "MR", "MRT", 20, -12));
			nCntr.Add(new Country(480, "Mauritius", "MU", "MUS", -20.2833, 57.55));
			nCntr.Add(new Country(492, "Monaco", "MC", "MCO", 43.7333, 7.4));
			nCntr.Add(new Country(496, "Monlia", "M", "MNG", 46, 105));
			nCntr.Add(new Country(498, "Moldova, Republic of", "MD", "MDA", 47, 29));
			nCntr.Add(new Country(499, "Montenegro", "ME", "MNE", 42, 19));
			nCntr.Add(new Country(500, "Montserrat", "MS", "MSR", 16.75, -62.2));
			nCntr.Add(new Country(504, "Morocco", "MA", "MAR", 32, -5));
			nCntr.Add(new Country(508, "Mozambique", "MZ", "MOZ", -18.25, 35));
			nCntr.Add(new Country(512, "Oma", "OM", "OM", 21, 57));
			nCntr.Add(new Country(516, "Namibia", "NA", "NAM", -22, 17));
			nCntr.Add(new Country(520, "Nauru", "NR", "NRU", -0.5333, 166.9167));
			nCntr.Add(new Country(524, "Nepal", "NP", "NPL", 28, 84));
			nCntr.Add(new Country(528, "Netherlands", "NL", "NLD", 52.5, 5.75));
			nCntr.Add(new Country(530, "Netherlands Antilles", "A", "ANT", 12.25, -68.75));
			nCntr.Add(new Country(531, "Curaçao", "CW", "CUW", 12.116667, -68.933333));
			nCntr.Add(new Country(533, "Aruba", "AW", "ABW", 12.5, -69.9667));
			nCntr.Add(new Country(535, "Bonaire, Sint Eustatius and Saba", "BQ", "BES", 12.236392, -68.334445));
			nCntr.Add(new Country(540, "New Caledonia", "NC", "NCL", -21.5, 165.5));
			nCntr.Add(new Country(548, "Vanuatu", "VU", "VUT", -16, 167));
			nCntr.Add(new Country(554, "New Zealand", "NZ", "NZL", -41, 174));
			nCntr.Add(new Country(558, "Nicaragua", "NI", "NIC", 13, -85));
			nCntr.Add(new Country(562, "Niger", "NE", "NER", 16, 8));
			nCntr.Add(new Country(566, "Nigeria", "NG", "NGA", 10, 8));
			nCntr.Add(new Country(570, "Niue", "NU", "NIU", -19.0333, -169.8667));
			nCntr.Add(new Country(574, "Norfolk Island", "NF", "NFK", -29.0333, 167.95));
			nCntr.Add(new Country(578, "Norway", "NO", "NOR", 62, 10));
			nCntr.Add(new Country(580, "Northern Mariana Islands", "MP", "MNP", 15.2, 145.75));
			nCntr.Add(new Country(581, "United States Minor Outlying Islands", "UM", "UMI", 19.2833, 166.6));
			nCntr.Add(new Country(583, "Micronesia, Federated States of", "FM", "FSM", 6.9167, 158.25));
			nCntr.Add(new Country(584, "Marshall Islands", "MH", "MHL", 9, 168));
			nCntr.Add(new Country(585, "Palau", "PW", "PLW", 7.5, 134.5));
			nCntr.Add(new Country(586, "Pakista", "PK", "PAK", 30, 70));
			nCntr.Add(new Country(591, "Panama", "PA", "PA", 9, -80));
			nCntr.Add(new Country(598, "Papua New Guinea", "PG", "PNG", -6, 147));
			nCntr.Add(new Country(600, "Paraguay", "PY", "PRY", -23, -58));
			nCntr.Add(new Country(604, "Peru", "PE", "PER", -10, -76));
			nCntr.Add(new Country(608, "Philippines", "PH", "PHL", 13, 122));
			nCntr.Add(new Country(612, "Pitcair", "P", "PC", -24.7, -127.4));
			nCntr.Add(new Country(616, "Poland", "PL", "POL", 52, 20));
			nCntr.Add(new Country(620, "Portugal", "PT", "PRT", 39.5, -8));
			nCntr.Add(new Country(624, "Guinea-Bissau", "GW", "GNB", 12, -15));
			nCntr.Add(new Country(626, "Timor-Leste", "TL", "TLS", -8.55, 125.5167));
			nCntr.Add(new Country(630, "Puerto Rico", "PR", "PRI", 18.25, -66.5));
			nCntr.Add(new Country(634, "Qatar", "QA", "QAT", 25.5, 51.25));
			nCntr.Add(new Country(638, "Réunio", "RE", "REU", -21.1, 55.6));
			nCntr.Add(new Country(642, "Romania", "RO", "ROU", 46, 25));
			nCntr.Add(new Country(643, "Russian Federatio", "RU", "RUS", 60, 100));
			nCntr.Add(new Country(646, "Rwanda", "RW", "RWA", -2, 30));
			nCntr.Add(new Country(652, "Saint Barthélemy", "BL", "BLM", 17.9, -62.8333));
			nCntr.Add(new Country(654, "Saint Helena, Ascension and Tristan da Cunha", "SH", "SH", -15.9333, -5.7));
			nCntr.Add(new Country(659, "Saint Kitts and Nevis", "K", "KNA", 17.3333, -62.75));
			nCntr.Add(new Country(660, "Anguilla", "AI", "AIA", 18.25, -63.1667));
			nCntr.Add(new Country(662, "Saint Lucia", "LC", "LCA", 13, -61.1333));
			nCntr.Add(new Country(663, "Saint Martin (French part)", "MF", "MAF", 18.05, -63.08));
			nCntr.Add(new Country(666, "Saint Pierre and Miquelo", "PM", "SPM", 46.8333, -56.3333));
			nCntr.Add(new Country(670, "Saint Vincent and the Grenadines", "VC", "VCT", 13.25, -61.2));
			nCntr.Add(new Country(674, "San Marino", "SM", "SMR", 43.7667, 12.4167));
			nCntr.Add(new Country(678, "Sao Tome and Principe", "ST", "STP", 1, 7));
			nCntr.Add(new Country(682, "Saudi Arabia", "SA", "SAU", 25, 45));
			nCntr.Add(new Country(686, "Senegal", "S", "SE", 14, -14));
			nCntr.Add(new Country(688, "Serbia", "RS", "SRB", 44, 21));
			nCntr.Add(new Country(690, "Seychelles", "SC", "SYC", -4.5833, 55.6667));
			nCntr.Add(new Country(694, "Sierra Leone", "SL", "SLE", 8.5, -11.5));
			nCntr.Add(new Country(702, "Singapore", "SG", "SGP", 1.3667, 103.8));
			nCntr.Add(new Country(703, "Slovakia", "SK", "SVK", 48.6667, 19.5));
			nCntr.Add(new Country(704, "Viet Nam", "V", "VNM", 16, 106));
			nCntr.Add(new Country(705, "Slovenia", "SI", "SV", 46, 15));
			nCntr.Add(new Country(706, "Somalia", "SO", "SOM", 10, 49));
			nCntr.Add(new Country(710, "South Africa", "ZA", "ZAF", -29, 24));
			nCntr.Add(new Country(716, "Zimbabwe", "ZW", "ZWE", -20, 30));
			nCntr.Add(new Country(724, "Spai", "ES", "ESP", 40, -4));
			nCntr.Add(new Country(732, "Western Sahara", "EH", "ESH", 24.5, -13));
			nCntr.Add(new Country(736, "Suda", "SD", "SD", 15, 30));
			nCntr.Add(new Country(740, "Suriname", "SR", "SUR", 4, -56));
			nCntr.Add(new Country(744, "Svalbard and Jan Maye", "SJ", "SJM", 78, 20));
			nCntr.Add(new Country(748, "Swaziland", "SZ", "SWZ", -26.5, 31.5));
			nCntr.Add(new Country(752, "Swede", "SE", "SWE", 62, 15));
			nCntr.Add(new Country(756, "Switzerland", "CH", "CHE", 47, 8));
			nCntr.Add(new Country(760, "Syrian Arab Republic", "SY", "SYR", 35, 38));
			nCntr.Add(new Country(762, "Tajikista", "TJ", "TJK", 39, 71));
			nCntr.Add(new Country(764, "Thailand", "TH", "THA", 15, 100));
			nCntr.Add(new Country(768, "To", "TG", "T", 8, 1.1667));
			nCntr.Add(new Country(772, "Tokelau", "TK", "TKL", -9, -172));
			nCntr.Add(new Country(776, "Tonga", "TO", "TO", -20, -175));
			nCntr.Add(new Country(780, "Trinidad and Toba", "TT", "TTO", 11, -61));
			nCntr.Add(new Country(784, "United Arab Emirates", "AE", "ARE", 24, 54));
			nCntr.Add(new Country(788, "Tunisia", "T", "TU", 34, 9));
			nCntr.Add(new Country(792, "Turkey", "TR", "TUR", 39, 35));
			nCntr.Add(new Country(795, "Turkmenista", "TM", "TKM", 40, 60));
			nCntr.Add(new Country(796, "Turks and Caicos Islands", "TC", "TCA", 21.75, -71.5833));
			nCntr.Add(new Country(798, "Tuvalu", "TV", "TUV", -8, 178));
			nCntr.Add(new Country(800, "Uganda", "UG", "UGA", 1, 32));
			nCntr.Add(new Country(804, "Ukraine", "UA", "UKR", 49, 32));
			nCntr.Add(new Country(807, "Macedonia, the former Yuslav Republic of", "MK", "MKD", 41.8333, 22));
			nCntr.Add(new Country(818, "Egypt", "EG", "EGY", 27, 30));
			nCntr.Add(new Country(826, "United Kingdom", "GB", "GBR", 54, -2));
			nCntr.Add(new Country(831, "Guernsey", "GG", "GGY", 49.5, -2.56));
			nCntr.Add(new Country(832, "Jersey", "JE", "JEY", 49.21, -2.13));
			nCntr.Add(new Country(833, "Isle of Ma", "IM", "IM", 54.23, -4.55));
			nCntr.Add(new Country(834, "Tanzania, United Republic of", "TZ", "TZA", -6, 35));
			nCntr.Add(new Country(840, "United States", "US", "USA", 38, -97));
			nCntr.Add(new Country(850, "Virgin Islands, U.S.", "VI", "VIR", 18.3333, -64.8333));
			nCntr.Add(new Country(854, "Burkina Faso", "BF", "BFA", 13, -2));
			nCntr.Add(new Country(858, "Uruguay", "UY", "URY", -33, -56));
			nCntr.Add(new Country(860, "Uzbekista", "UZ", "UZB", 41, 64));
			nCntr.Add(new Country(862, "Venezuela, Bolivarian Republic of", "VE", "VE", 8, -66));
			nCntr.Add(new Country(876, "Wallis and Futuna", "WF", "WLF", -13.3, -176.2));
			nCntr.Add(new Country(882, "Samoa", "WS", "WSM", -13.5833, -172.3333));
			nCntr.Add(new Country(887, "Yeme", "YE", "YEM", 15, 48));
			nCntr.Add(new Country(894, "Zambia", "ZM", "ZMB", -15, 30));
			foreach(Country a in nCntr){
				db.Insert(a);
			}
		}

		public void createMeetingTest(){
			//			create new meeting
			db.DropTable<Case> ();
			db.CreateTable<Case> ();
			db.DropTable<Meeting> ();
			db.CreateTable<Meeting> ();
			db.DropTable<Imputed> ();
			db.CreateTable<Imputed> ();

			Case caseDetention = new Case ();
			Imputed newImputed = new Imputed ();
			newImputed.Name = "axl".Trim ();
			newImputed.LastNameP = "sanz".Trim ();
			newImputed.LastNameM = "perz".Trim ();
			newImputed.FoneticString = getFoneticByName ("axl", "sanz", "perz");
			newImputed.Gender = true;
			newImputed.BirthDate = DateTime.Today.AddYears (-27);
			caseDetention.Status = statusCasefindByCode (Constants.CASE_STATUS_MEETING);
			caseDetention.StatusCaseId = statusCasefindByCode (Constants.CASE_STATUS_MEETING).Id;
			caseDetention.IdFolder = "nuevo Folder example";
			caseDetention.DateCreate = DateTime.Today;
			db.InsertWithChildren (caseDetention);
			Meeting meeting = new Meeting ();
			meeting.MeetingType = Constants.MEETING_PROCEDURAL_RISK;
			meeting.CaseDetentionId = caseDetention.Id;
			meeting.CaseDetention = caseDetention;
			StatusMeeting statusMeeting = statusMeetingfindByCode (Constants.S_MEETING_INCOMPLETE);
			meeting.StatusMeetingId = statusMeeting.Id;
			meeting.StatusMeeting = statusMeeting;
			meeting.DateCreate = DateTime.Today;
			db.InsertWithChildren (meeting);
			newImputed.MeetingId = meeting.Id;
			newImputed.Meeting = meeting;
			db.InsertWithChildren (newImputed);
			db.UpdateWithChildren (meeting);
			db.UpdateWithChildren (caseDetention);
			Console.WriteLine ("caseDetention.Id> {0}", caseDetention.Id);
			var ese = db.GetWithChildren<Meeting> (meeting.Id);
			Console.WriteLine ("done execution>");
			Console.WriteLine ("ese.Id>" + ese.Id + "  _ese.CaseDetentionId=>" + ese.CaseDetentionId);
		}

		public String getFoneticByName(String name, String lastNameP, String lastNameM) {
			return name.Trim().ToLowerInvariant()+lastNameP.Trim().ToLowerInvariant()+lastNameM.Trim().ToLowerInvariant();
		}

		public int calculateAge(DateTime dateOfBirth)
		{
			DateTime now = DateTime.Today; 
			int age = now.Year - dateOfBirth.Year;
			if (now.DayOfYear<dateOfBirth.DayOfYear) 
				age--;
			return age; 
		}

		public StatusCase statusCasefindByCode(String code){
			//var tableStatus = db.Query<StatusCase>("SELECT s from StatusCase s where s.name = "+code);
			var tableStatus = db.Table<StatusCase> ().Where (s => s.Name == code).FirstOrDefault ();
			return tableStatus;
		}

		public StatusMeeting statusMeetingfindByCode(String code){
			var tableStatus = db.Table<StatusMeeting>().Where (s => s.Status == code).FirstOrDefault ();
			return tableStatus;
		}

		public StatusMeeting userRepositoryfindOne(String code){
			var tableStatus = db.Table<StatusMeeting>().Where (s => s.Status == code).FirstOrDefault ();
			return tableStatus;
		}

		public List<CountryDto> CountryFindAllOrderByName(){
			var tableStatus = db.Query<CountryDto>("select c.id as 'Id',c.name as 'Name' from cat_country as c order by c.name");
			return tableStatus;
		}



	}
}

