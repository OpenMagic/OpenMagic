using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using OpenMagic.Reflection;

namespace OpenMagic.DataAnnotations
{
    public class PropertyMetadata : IPropertyMetadata
    {
        private readonly Lazy<DisplayAttribute> DisplayFactory;

        public PropertyMetadata(PropertyInfo property, bool isPublic)
        {
            PropertyInfo = property;
            IsPublic = isPublic;
            DisplayFactory = new Lazy<DisplayAttribute>(() => GetCustomAttribute(() => new DisplayAttribute()));
        }

        public DisplayAttribute Display
        {
            get { return DisplayFactory.Value; }
        }

        public PropertyInfo PropertyInfo { get; private set; }

        public bool IsPublic { get; private set; }

        public bool IsNotPublic
        {
            get { return !IsPublic; }
        }

        private T GetCustomAttribute<T>(Func<T> defaultValueFactory)
        {
            var attribute = PropertyInfo.GetCustomAttribute<T>();

            // ReSharper disable once CompareNonConstrainedGenericWithNull
            if (attribute != null)
            {
                return attribute;
            }

            return defaultValueFactory();
        }
    }
}