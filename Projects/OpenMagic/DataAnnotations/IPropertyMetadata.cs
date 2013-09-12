using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace OpenMagic.DataAnnotations
{
    public interface IPropertyMetadata
    {
        DisplayAttribute Display { get; }
        bool IsPublic { get; }
        bool IsNotPublic { get; }
        PropertyInfo PropertyInfo { get; }
    }
}
