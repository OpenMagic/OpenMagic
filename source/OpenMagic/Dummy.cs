using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OpenMagic
{
    public class Dummy : IDummy
    {
        protected readonly Dictionary<Type, Func<object>> ValueFactories;

        public Dummy()
        {
            ValueFactories = new Dictionary<Type, Func<object>>
            {
                {typeof (bool), () => RandomBoolean.Next()},
                {typeof (DateTime), () => RandomDateTime.Next()},
                {typeof (string), () => RandomString.Next(1, 100)},
                {typeof (byte), () => RandomNumber.NextByte()},
                {typeof (char), () => RandomNumber.NextChar()},
                {typeof (decimal), () => RandomNumber.NextDecimal()},
                {typeof (double), () => RandomNumber.NextDouble()},
                {typeof (float), () => RandomNumber.NextFloat()},
                {typeof (int), () => RandomNumber.NextInt()},
                {typeof (long), () => RandomNumber.NextLong()},
                {typeof (sbyte), () => RandomNumber.NextSByte()},
                {typeof (short), () => RandomNumber.NextShort()},
                {typeof (uint), () => RandomNumber.NextUInt()},
                {typeof (ulong), () => RandomNumber.NextULong()},
                {typeof (ushort), () => RandomNumber.NextUShort()}
            };
        }

        public virtual T Object<T>()
        {
            return (T)Object(typeof (T));
        }

        public virtual object Object(Type type)
        {
            var obj = CreateObjectInstance(type);

            foreach (var propertyInfo in obj.GetType().GetProperties().Where(p => p.CanWrite))
            {
                var value = Value(propertyInfo.PropertyType);

                propertyInfo.SetValue(obj, value);
            }

            return obj;
        }

        public virtual object Value(Type type)
        {
            Func<object> valueFactory;

            if (ValueFactories.TryGetValue(type, out valueFactory))
            {
                return valueFactory();
            }
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (List<>))
            {
                var itemType = type.GetGenericArguments()[0];
                var listType = typeof (List<>);
                var genericListType = listType.MakeGenericType(itemType);
                var list = (IList)Activator.CreateInstance(genericListType);

                for (var i = 0; i < RandomNumber.NextInt(0, 1000); i++)
                {
                    list.Add(Value(itemType));
                }

                return list;
            }
            if (type.IsClass)
            {
                return Object(type);
            }
            throw new NotImplementedException(string.Format("Dummy.Value({0}) is not implemented.", type));
        }

        protected virtual object CreateObjectInstance(Type type)
        {
            try
            {
                return Activator.CreateInstance(type);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Cannot create instance of {0}.", type), exception);
            }
        }
    }
}