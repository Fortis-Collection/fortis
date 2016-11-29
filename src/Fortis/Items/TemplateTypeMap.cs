using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fortis.Items
{
	public class TemplateTypeMap : ITemplateTypeMap
	{
		protected readonly ITypesSource TypesSource;

		private Dictionary<Guid, Type> TemplateType = new Dictionary<Guid, Type>();
		private Dictionary<Type, Guid> TypeTemplate = new Dictionary<Type, Guid>();

		public TemplateTypeMap(
			ITypesSource typesSource)
		{
			TypesSource = typesSource;

			ProcessMap();
		}

		public bool Contains(Guid templateId)
		{
			return TemplateType.ContainsKey(templateId);
		}

		public bool Contains(Type type)
		{
			return TypeTemplate.ContainsKey(type);
		}

		public Guid Find(Type type)
		{
			return Contains(type) ? TypeTemplate[type] : default(Guid);
		}

		public Type Find(Guid templateId)
		{
			return Contains(templateId) ? TemplateType[templateId] : null;
		}

		public void ProcessMap()
		{
			foreach (var type in TypesSource.Types)
			{
				var templateAttribute = type.GetCustomAttribute<TemplateAttribute>(false);

				if (templateAttribute == null)
				{
					continue;
				}

				if (TemplateType.ContainsKey(templateAttribute.TemplateId))
				{
					throw new Exception($"Fortis: template has already been added | Type: {type.FullName} | ID: {templateAttribute.TemplateId}");
				}

				TemplateType.Add(templateAttribute.TemplateId, type);
			}

			TemplateType.Keys.ToList().ForEach(templateId => TypeTemplate.Add(TemplateType[templateId], templateId));
		}
	}
}
