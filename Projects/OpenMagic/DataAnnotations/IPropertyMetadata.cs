using System;
using System.Reflection;

namespace OpenMagic.DataAnnotations
{
    public interface IPropertyMetadata
    {
        bool IsPublic { get; }
        bool IsNotPublic { get; }
        PropertyInfo PropertyInfo { get;  }
    }
}
