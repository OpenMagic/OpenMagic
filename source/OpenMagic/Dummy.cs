using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NullGuard;

namespace OpenMagic
{
    public class Dummy : IDummy
    {
        protected readonly Dictionary<Type, Func<object>> InstanceFactories;
        protected readonly Dictionary<Type, Func<object>> ValueFactories;

        public Dummy()
        {
            InstanceFactories = new Dictionary<Type, Func<object>>();
            ValueFactories = new Dictionary<Type, Func<object>>
            {
                {typeof(bool), () => RandomBoolean.Next()},
                {typeof(DateTime), () => RandomDateTime.Next()},
                {typeof(string), () => RandomString.Next(1, 100)},
                {typeof(byte), () => RandomNumber.NextByte()},
                {typeof(char), () => RandomNumber.NextChar()},
                {typeof(decimal), () => RandomNumber.NextDecimal()},
                {typeof(decimal?), () => RandomNumber.NextNullableDecimal()},
                {typeof(double), () => RandomNumber.NextDouble()},
                {typeof(float), () => RandomNumber.NextFloat()},
                {typeof(int), () => RandomNumber.NextInt()},
                {typeof(long), () => RandomNumber.NextLong()},
                {typeof(sbyte), () => RandomNumber.NextSByte()},
                {typeof(short), () => RandomNumber.NextShort()},
                {typeof(uint), () => RandomNumber.NextUInt()},
                {typeof(ulong), () => RandomNumber.NextULong()},
                {typeof(ushort), () => RandomNumber.NextUShort()},
                {typeof(Guid), () => Guid.NewGuid()},
                {typeof(Guid?), () => RandomBoolean.Next() ? Guid.NewGuid() : (Guid?)null}
            };
        }

        [return: AllowNull]
        public T Value<T>()
        {
            return (T)Value(typeof(T));
        }

        [return: AllowNull]
        public virtual object Value(Type type)
        {
            Func<object> valueFactory;

            if (ValueFactories.TryGetValue(type, out valueFactory))
            {
                return valueFactory();
            }
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                return CreateListOfT(type);
            }
            if (type.IsArray)
            {
                return CreateArray(type);
            }
            if (type.IsClass)
            {
                return Object(type);
            }
            throw new NotImplementedException(string.Format("Dummy.Value({0}) is not implemented.", type));
        }

        private object Object(Type type)
        {
            var obj = CreateObjectInstance(type);

            foreach (var propertyInfo in obj.GetType().GetProperties().Where(p => p.CanWrite))
            {
                var value = Value(propertyInfo.PropertyType);

                propertyInfo.SetValue(obj, value);
            }

            return obj;
        }


        protected virtual object CreateObjectInstance(Type type)
        {
            try
            {
                Func<object> instanceFactory;

                return InstanceFactories.TryGetValue(type, out instanceFactory)
                    ? instanceFactory()
                    : Activator.CreateInstance(type);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Cannot create instance of {0}.", type), exception);
            }
        }

        protected virtual IEnumerable CreateValues(Type itemType)
        {
            for (var i = 0; i < RandomNumber.NextInt(0, 1000); i++)
            {
                yield return Value(itemType);
            }
        }

        private object CreateArray(Type arrayType)
        {
            var itemType = arrayType.GetElementType();
            var values = CreateValues(itemType);

            var method = typeof(Enumerable).GetMethod("Cast");
            var genericMethod = method.MakeGenericMethod(itemType);
            var enumerable = genericMethod.Invoke(this, new object[] {values});

            method = typeof(Enumerable).GetMethod("ToArray");
            genericMethod = method.MakeGenericMethod(itemType);
            var array = genericMethod.Invoke(this, new[] {enumerable});

            return array;
        }

        private object CreateListOfT(Type type)
        {
            var itemType = type.GetGenericArguments()[0];
            var values = CreateValues(itemType);
            var listType = typeof(List<>);
            var genericListType = listType.MakeGenericType(itemType);
            var list = (IList)Activator.CreateInstance(genericListType);

            foreach (var value in values)
            {
                list.Add(value);
            }

            return list;
        }
    }
}