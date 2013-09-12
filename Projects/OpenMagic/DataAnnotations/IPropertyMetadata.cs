using System;
using System.Reflection;

namespace OpenMagic.DataAnnotations
{
    public interface IPropertyMetadata
    {
        bool IsPublic { get; }
        bool IsNotPublic { get; }
        string Name { get; }
        PropertyInfo PropertyInfo { get;  }
    }
}
