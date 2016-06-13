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

            try
            {
                if (ValueFactories.TryGetValue(type, out valueFactory))
                {
                    return valueFactory();
                }
                if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(List<>) || type.GetGenericTypeDefinition() == typeof(IList<>)))
                {
                    return CreateListOfT(type);
                }
                if (type == typeof(IList))
                {
                    return CreateListOfT(typeof(string));
                }
                if (type.IsArray)
                {
                    return CreateArray(type);
                }
                if (type == typeof(IDictionary))
                {
                    return CreateDictionary();
                }
                if (type.IsClass)
                {
                    return Object(type);
                }
            }
            catch (Exception exception)
            {
                var message = string.Format("Cannot create dummy value for type '{0}'.", type);
                throw new Exception(message, exception);
            }
            throw new NotImplementedException(string.Format("Dummy.Value({0}) is not implemented.", type));
        }

        private IDictionary CreateDictionary()
        {
            var keys = CreateValues(typeof(string)).Cast<string>();

            return keys.ToDictionary(key => Value<string>());
        }

        private object Object(Type type)
        {
            var obj = CreateObjectInstance(type);

            foreach (var propertyInfo in obj.GetType().GetProperties().Where(p => p.CanWrite))
            {
                try
                {
                    var value = Value(propertyInfo.PropertyType);

                    propertyInfo.SetValue(obj, value);
                }
                catch (Exception exception)
                {
                    var message = string.Format("Cannot set property value '{0}.{1}'.", type, propertyInfo.Name);
                    throw new Exception(message, exception);
                }
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
                var message = string.Format("Cannot create instance of {0}.", type);
                throw new Exception(message, exception);
            }
        }

        protected virtual IEnumerable CreateValues(Type itemType)
        {
            return CreateValues(itemType, RandomNumber.NextInt(0, 1000));
        }

        protected virtual IEnumerable CreateValues(Type itemType, int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return Value(itemType);
            }
        }

        private object CreateArray(Type arrayType)
        {
            try
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
            catch (Exception exception)
            {
                var message = string.Format("Cannot create array of type '{0}'.", arrayType);
                throw new Exception(message, exception);
            }
        }

        private object CreateListOfT(Type type)
        {
            try
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
            catch (Exception exception)
            {
                var message = string.Format("Cannot create list of type '{0}'.", type);
                throw new Exception(message, exception);
            }
        }
    }
}