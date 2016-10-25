using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Fortis.Dynamics
{
	public class FortisDynamicObject : DynamicObject, IFortisDynamicObject
	{
		private readonly Dictionary<string, object> dynamicProperties = new Dictionary<string, object>();

		public void AddDynamicProperty(string key, object value)
		{
			if (dynamicProperties.ContainsKey(key))
			{
				throw new Exception("Property with this key has already been added.");
			}

			dynamicProperties[key] = value;
		}

		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			var propertyName = $"{binder.Name}_get";
			var getExists = DynamicPropertyExists(propertyName);
			var exists = getExists ? true : DynamicPropertyExists(binder.Name);

			if (!exists && !getExists)
			{
				result = null;

				return false;
			}

			if (!getExists)
			{
				propertyName = binder.Name;
			}

			result = exists ? GetDyanmicPropertyValue(propertyName) : null;

			return exists;
		}

		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			var propertyName = $"{binder.Name}_get";
			var setExists = DynamicPropertyExists(propertyName);
			var exists = setExists ? true : DynamicPropertyExists(binder.Name);

			if (!exists)
			{
				return false;
			}

			if (!setExists)
			{
				propertyName = binder.Name;
			}

			SetDynamicValueProperty(propertyName, value);

			return true;
		}

		public bool DynamicPropertyExists(string key)
		{
			return dynamicProperties.ContainsKey(key);
		}

		public object GetDyanmicPropertyValue(string name)
		{
			var value = dynamicProperties[name];

			if (value is Delegate)
			{
				return ((dynamic)value)();
			}

			return value;
		}

		public void SetDynamicValueProperty(string name, object value)
		{
			var currentValue = dynamicProperties[name];

			if (currentValue is Delegate)
			{
				((dynamic)currentValue)(value);
			}
			else
			{
				dynamicProperties[name] = value;
			}
		}
	}
}
