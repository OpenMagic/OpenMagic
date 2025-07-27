using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OpenMagic
{
    public class Dummy : IDummy
    {
        private readonly Dictionary<Type, Func<object>> InstanceFactories = new();

        private readonly Dictionary<Type, Func<object>> ValueFactories = new()
        {
            { typeof(bool), () => RandomBoolean.Next() },
            { typeof(DateTime), () => RandomDateTime.Next() },
            { typeof(string), () => RandomString.Next(1, 100) },
            { typeof(byte), () => RandomNumber.NextByte() },
            { typeof(char), () => RandomNumber.NextChar() },
            { typeof(decimal), () => RandomNumber.NextDecimal() },
            { typeof(double), () => RandomNumber.NextDouble() },
            { typeof(float), () => RandomNumber.NextFloat() },
            { typeof(Guid), () => Guid.NewGuid() },
            { typeof(int), () => RandomNumber.NextInt() },
            { typeof(long), () => RandomNumber.NextLong() },
            { typeof(sbyte), () => RandomNumber.NextSByte() },
            { typeof(short), () => RandomNumber.NextShort() },
            { typeof(uint), () => RandomNumber.NextUInt() },
            { typeof(ulong), () => RandomNumber.NextULong() },
            { typeof(ushort), () => RandomNumber.NextUShort() },
            { typeof(bool?), () => RandomNullable(RandomBoolean.Next) },
            { typeof(DateTime?), () => RandomNullable(RandomDateTime.Next) },
            { typeof(byte?), () => RandomNullable(RandomNumber.NextByte) },
            { typeof(char?), () => RandomNullable(RandomNumber.NextChar) },
            { typeof(decimal?), () => RandomNullable(RandomNumber.NextDecimal) },
            { typeof(double?), () => RandomNullable(RandomNumber.NextDouble) },
            { typeof(float?), () => RandomNullable(RandomNumber.NextFloat) },
            { typeof(Guid?), () => RandomNullable(Guid.NewGuid) },
            { typeof(int?), () => RandomNullable(RandomNumber.NextInt) },
            { typeof(long?), () => RandomNullable(RandomNumber.NextLong) },
            { typeof(sbyte?), () => RandomNullable(RandomNumber.NextSByte) },
            { typeof(short?), () => RandomNullable(RandomNumber.NextShort) },
            { typeof(uint?), () => RandomNullable(RandomNumber.NextUInt) },
            { typeof(ulong?), () => RandomNullable(RandomNumber.NextULong) },
            { typeof(ushort?), () => RandomNullable(RandomNumber.NextUShort) }
        };


        public T Value<T>()
        {
            return (T)Value(typeof(T));
        }


        public virtual object Value(Type type)
        {
            try
            {
                if (ValueFactories.TryGetValue(type, out var valueFactory))
                {
                    return valueFactory();
                }

                if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(List<>) || type.GetGenericTypeDefinition() == typeof(IList<>)))
                {
                    return CreateListOfT(type);
                }

                if (type == typeof(IList))
                {
                    return CreateListOfT(typeof(List<string>));
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
                var message = $"Cannot create dummy value for type '{type}'.";
                throw new Exception(message, exception);
            }

            throw new NotImplementedException($"Dummy.Value({type}) is not implemented.");
        }

        protected virtual IDictionary CreateDictionary()
        {
            var values = CreateValues(typeof(string)).Cast<string>().Where(s => !string.IsNullOrWhiteSpace(s));

            return values.ToDictionary(value => Guid.NewGuid().ToString());
        }
        protected virtual object Object(Type type)
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
                    var message = $"Cannot set property value '{type}.{propertyInfo.Name}'.";
                    throw new Exception(message, exception);
                }
            }

            return obj;
        }

        protected virtual object CreateObjectInstance(Type type)
        {
            try
            {
                if (InstanceFactories.TryGetValue(type, out var instanceFactory) && instanceFactory != null)
                {
                    return instanceFactory();
                }
                else
                {
                    // Avoid repeated dictionary lookup and delegate allocation
                    var createdInstance = Activator.CreateInstance(type);
                    InstanceFactories[type] = () => createdInstance;
                    return createdInstance;
                }
            }
            catch (Exception exception)
            {
                var message = $"Cannot create instance of {type}.";
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

        protected virtual object[] CreateArray(Type arrayType)
        {
            try
            {
                var itemType = arrayType.GetElementType();
                if (itemType == null)
                {
                    throw new ArgumentException($"Array type '{arrayType}' does not have a valid element type.", nameof(arrayType));
                }
                var values = CreateValues(itemType);

                var method = typeof(Enumerable).GetMethod("Cast");
                var genericMethod = method!.MakeGenericMethod(itemType);
                var enumerable = genericMethod.Invoke(null, [values]);

                method = typeof(Enumerable).GetMethod("ToArray");
                genericMethod = method!.MakeGenericMethod(itemType);
                var array = genericMethod.Invoke(null, [enumerable]);

                if (array is null)
                {
                    throw new InvalidOperationException($"Failed to create array of type '{arrayType}'.");
                }

                return (object[])array;
            }
            catch (Exception exception)
            {
                var message = $"Cannot create array of type '{arrayType}'.";
                throw new Exception(message, exception);
            }
        }
        protected virtual IList CreateListOfT(Type type)
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
                var message = $"Cannot create list of type '{type}'.";
                throw new Exception(message, exception);
            }
        }

        private static T? RandomNullable<T>(Func<T> randomFactory)
        {
            return RandomNumber.NextInt(1, 5) == 1 ? default : randomFactory();
        }
    }
}