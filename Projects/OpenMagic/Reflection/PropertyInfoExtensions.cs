using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace OpenMagic.Reflection
{
    public static class PropertyInfoExtensions
    {
        public static bool IsDecoratedWith<T>(this PropertyInfo value)
        {
            value.MustNotBeNull("value");

            return value.GetCustomAttributes(typeof(T), true).Any();
        }

        public static bool IsRequired(this PropertyInfo value)
        {
            value.MustNotBeNull("value");

            return value.IsDecoratedWith<RequiredAttribute>();
        }
    }
}
