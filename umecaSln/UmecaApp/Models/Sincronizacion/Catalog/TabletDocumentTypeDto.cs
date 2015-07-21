﻿using System;

namespace UmecaApp
{
	public class TabletDocumentTypeDto
	{
		public TabletDocumentTypeDto ()
		{
		}

		public TabletDocumentTypeDto(int? id, String name, Boolean? isObsolete, Boolean? specification) {
			this.id = id;
			this.name = name;
			this.isObsolete = isObsolete;
			this.specification = specification;
		}

		public int? id{ get; set ; }
		public String name{ get; set ; }
		public Boolean? isObsolete{ get; set ; }
		public Boolean? specification{ get; set ; }

	}
}
