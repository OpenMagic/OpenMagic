using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using OpenMagic.Reflection;

namespace OpenMagic.DataAnnotations
{
    public class PropertyMetadata : IPropertyMetadata
    {
        private Lazy<DisplayAttribute> DisplayFactory;

        public PropertyMetadata(PropertyInfo property, bool isPublic)
        {
            this.PropertyInfo = property;
            this.IsPublic = isPublic;
            this.DisplayFactory = new Lazy<DisplayAttribute>(() => this.GetCustomAttribute<DisplayAttribute>(() => new DisplayAttribute()));
        }

        public DisplayAttribute Display
        {
            get { return this.DisplayFactory.Value; }
        }

        public PropertyInfo PropertyInfo { get; private set; }

        public bool IsPublic { get; private set; }

        public bool IsNotPublic
        {
            get { return !this.IsPublic; }
        }

        private T GetCustomAttribute<T>(Func<T> defaultValueFactory)
        {
            var attribute = this.PropertyInfo.GetCustomAttribute<T>();

            if (attribute != null)
            {
                return attribute;    
            }

            return defaultValueFactory();
        }
    }
}
