using System;

namespace UmecaApp
{
	public class ResponseMessage
	{
		public ResponseMessage ()
		{
		}

		public Boolean?  hasError{ get; set; }
		public String message{ get; set; }
		public String urlToGo{ get; set; }
		public String title{ get; set; }
		public Object returnData{ get; set; }

		public ResponseMessage(Boolean?  hasError, String message){
			this.hasError = hasError;
			this.message = message;
		}

		public ResponseMessage(Boolean?  hasError, String message, String title) {
			this.hasError = hasError;
			this.message = message;
			this.title = title;
		}

	}
}

