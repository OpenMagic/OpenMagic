using System;
using System.Reflection;

namespace OpenMagic.DataAnnotations
{
    public class PropertyMetadata : IPropertyMetadata
    {
        public PropertyMetadata(PropertyInfo property, bool isPublic)
        {
            this.PropertyInfo = property;
            this.IsPublic = isPublic;
        }

        public PropertyInfo PropertyInfo { get; private set; }

        public bool IsPublic { get; private set; }

        public bool IsNotPublic
        {
            get { return !this.IsPublic; }
        }
    }
}
