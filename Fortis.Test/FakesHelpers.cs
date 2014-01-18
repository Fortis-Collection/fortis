using Sitecore.Collections;
using Sitecore.Collections.Fakes;
using Sitecore.Data.Fields;
using Sitecore.Data.Fields.Fakes;
using Sitecore.Data.Items;
using Sitecore.Data.Items.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fortis.Test
{
	public static class FakesHelpers
	{
		public static Item CreateTestItem(string id = null, string name = "Test Item", Guid templateId = default(Guid), FieldCollection fields = null)
		{
			Item item = new ShimItem()
			{
				NameGet = () => name ?? "Test Item",
				IDGet = () => new Sitecore.Data.ID(id ?? new Guid().ToString()),
				TemplateIDGet = () => new Sitecore.Data.ID(templateId),
				FieldsGet = () => fields ?? CreateTestFields(),
			};

			new ShimBaseItem(item)
			{
				ItemGetString = fieldName => item.Fields[fieldName].Value
			};

			return item;
		}

		public static FieldCollection CreateTestFields(List<Field> fields = null)
		{
			if (fields == null)
			{
				fields = new List<Field>();

				fields.Add(CreateTestTextField());
			}

			return new ShimFieldCollection()
			{
				ItemGetString = name => { return fields.FirstOrDefault(f => f.Name.Equals(name)); },
				ItemGetID = id => { return fields.FirstOrDefault(f => f.ID.Equals(id)); },
			};
		}

		public static Field CreateTestTextField(string id = null, string name = null, string value = null)
		{
			return new ShimField()
			{
				IDGet = () => new Sitecore.Data.ID(id ?? new Guid().ToString()),
				NameGet = () => name ?? "Text Field",
				ValueGet = () => value ?? "Text Field Value",
			};
		}
	}
}
