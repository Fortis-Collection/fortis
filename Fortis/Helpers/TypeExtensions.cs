using System;
using System.Reflection;

namespace Fortis.Helpers
{
    public static class TypeExtensions
    {
        public static object GetValue(this MemberInfo member, object instance)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    return ((FieldInfo)member).GetValue(instance);
                case MemberTypes.Property:
                    return ((PropertyInfo)member).GetValue(instance, null);
                default:
                    throw new NotSupportedException(
                        Sitecore.StringExtensions.StringExtensions.FormatWith("Unsupported member type: {0}",
                            new object[]
                            {
                                member.MemberType
                            }));
            }
        }

        public static bool IsAssignableTo(this Type type, Type other)
        {
            return other.IsAssignableFrom(type);
        }
    }
}
