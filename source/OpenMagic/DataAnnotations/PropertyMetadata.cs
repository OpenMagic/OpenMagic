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
            property.MustNotBeNull(nameof(property));

            PropertyInfo = property;
            IsPublic = isPublic;
            DisplayFactory = new Lazy<DisplayAttribute>(() => GetCustomAttribute(() => new DisplayAttribute()));
        }

        public DisplayAttribute Display => DisplayFactory.Value;

        public PropertyInfo PropertyInfo { get; }

        public bool IsPublic { get; }

        public bool IsNotPublic => !IsPublic;

        private T GetCustomAttribute<T>(Func<T> defaultValueFactory)
        {
            var attribute = PropertyInfo.GetCustomAttribute<T>();

            return attribute ?? defaultValueFactory();
        }
    }
}