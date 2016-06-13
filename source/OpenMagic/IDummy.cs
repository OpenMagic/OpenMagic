using System;

namespace OpenMagic
{
    public interface IDummy
    {
        T Object<T>();
        object Object(Type type);
        T Value<T>();
        object Value(Type type);
    }
}