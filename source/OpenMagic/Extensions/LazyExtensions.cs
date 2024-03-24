using System;

namespace OpenMagic.Extensions;

public static class LazyExtensions
{
    public static void DisposeValueIfCreated<T>(this Lazy<T> obj) where T : IDisposable
    {
        if (obj.IsValueCreated)
        {
            obj.Value.Dispose();
        }
    }
}