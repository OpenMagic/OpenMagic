using System;

namespace OpenMagic
{
    public interface IDummy
    {
        T Object<T>();
        object Object(Type type);
        object Value(Type type);
    }
}