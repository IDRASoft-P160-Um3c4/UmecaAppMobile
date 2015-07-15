using System;

namespace UmecaApp
{
	public class TabletAssignmentInfo
	{
		public TabletAssignmentInfo ()
		{
		}

		public TabletAssignmentInfo(long id, String assignmentTypeCode) {
			this.id = id;
			this.assignmentTypeCode = assignmentTypeCode;
		}

		public long id{ get; set; }
		public String assignmentTypeCode{ get; set; }
	}
}

