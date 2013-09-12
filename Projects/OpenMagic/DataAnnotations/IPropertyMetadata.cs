using System;
using System.Reflection;

namespace OpenMagic.DataAnnotations
{
    public interface IPropertyMetadata
    {
        bool IsPublic { get; }
        bool IsNotPublic { get; }
        string Label { get; }
        string Name { get; }
        string Placeholder { get; }
        PropertyInfo PropertyInfo { get;  }
    }
}
