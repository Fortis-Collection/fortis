using Sitecore.Collections;

namespace Fortis.Helpers
{
    internal class TypeHelper
    {
        public static void CopyProperties(object source, object target)
        {
            var type = target.GetType();
            foreach (var propertyInfo in source.GetType().GetProperties())
            {
                var property = type.GetProperty(propertyInfo.Name);
                if (!(property == null) && propertyInfo.PropertyType.IsAssignableTo(property.PropertyType))
                {
                    property.SetValue(target, propertyInfo.GetValue(source, null), null);
                }
            }
        }

        public static void CopyProperties(object source, SafeDictionary<string, string> target)
        {
            foreach (var propertyInfo in source.GetType().GetProperties())
            {
                var obj = propertyInfo.GetValue(source, null);
                if (obj != null)
                {
                    target[propertyInfo.Name.Replace("_", "-")] = obj.ToString();
                }
            }
        }
    }
}
