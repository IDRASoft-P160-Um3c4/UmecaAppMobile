using System;

namespace UmecaApp
{
	public class CatalogDto
	{
		public int id{ get; set;}
		public String name{ get; set;}
		public Boolean? specification{ get; set;}
		public String code{ get; set;}
		public String content{ get; set;}

		public CatalogDto(int id, String name) {
			this.id = id;
			this.name = name;
		}

		public CatalogDto(int id, String name, Boolean specification, String code) {
			this.id = id;
			this.name = name;
			this.specification = specification;
			this.code = code;
		}

		public CatalogDto(String name, String content) {
			this.name = name;
			this.content = content;
		}

		public CatalogDto(int id, String name, String content, String code) {
			this.id = id;
			this.name = name;
			this.code = code;
			this.content = content;
		}

		public CatalogDto() {
		}

		public CatalogDto(int id, String name, Boolean specification) {
			this.id = id;
			this.name = name;
			this.specification = specification;
		}

	}
}

