using System;
//listas
using System.Collections.Generic;

namespace UmecaApp
{
	public class TerminateMeetingMessageDto
	{

		public TerminateMeetingMessageDto() {
			this.groupMessage = new List<GroupMessageMeetingDto>();
		}

		public List<GroupMessageMeetingDto> groupMessage{ get; set; }
		public String template = "entity es un campo requerido." ;
		public String templateVerification = "De entity field falta por ser verificado." ;
		public String templateVerificationSingle = "entity falta por ser verificado." ;


		public Boolean  existsMessageProperties(){
			for (var i = 0; i < this.groupMessage.Count; i++) {
				if(this.groupMessage[i].listString.Count>0){
					return true;
				}
			}
			return false;
		}

		public void formatMessages() {
			for (var i = 0; i < this.groupMessage.Count; i++) {
				this.groupMessage[i].messages="";
				foreach(String s in this.groupMessage[i].listString){
					this.groupMessage[i].messages=this.groupMessage[i].messages+s+ "<br/>";
				}
				this.groupMessage[i].listString=null;
			}
		}
	}
}

