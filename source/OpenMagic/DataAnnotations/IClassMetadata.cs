using System;
using System.Collections.Generic;
using System.Reflection;

namespace OpenMagic.DataAnnotations
{
    public interface IClassMetadata
    {
        Lazy<IEnumerable<IPropertyMetadata>> Properties { get; }
        Type Type { get; }

        IPropertyMetadata GetProperty(string propertyName);
        IPropertyMetadata GetProperty(PropertyInfo property);
    }
}