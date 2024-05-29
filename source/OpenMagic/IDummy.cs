using System;

namespace OpenMagic
{
    public interface IDummy
    {
        T Value<T>();
        object Value(Type type);
    }
}