using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using OpenMagic.Extensions;

namespace OpenMagic.Reflection
{
    /// <summary>
    ///     Provides extension methods for working with objects.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        ///     Gets the <see cref="MethodInfo" /> of a method specified by a LINQ expression.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="method">The LINQ expression representing the method.</param>
        /// <returns>The <see cref="MethodInfo" /> of the specified method.</returns>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="method" /> is not a valid LINQ expression representing a method call.</exception>
        // ReSharper disable once UnusedParameter.Global because its an extension method
        public static MethodInfo Method<TObject>(this TObject obj, Expression<Action<TObject>> method)
        {
            // unit test for this argument test.
            if (method.NodeType != ExpressionType.Lambda)
            {
                throw new ArgumentException($"method must be NodeType '{ExpressionType.Lambda}', not '{method.NodeType}'.", nameof(method));
            }

            // unit test for this argument test.
            if (method.Body.NodeType != ExpressionType.Call)
            {
                throw new ArgumentException($"method's Body.NodeType must be '{ExpressionType.Call}', not '{method.Body.NodeType}'.", nameof(method));
            }

            var methodCall = (MethodCallExpression)method.Body;
            var methodInfo = methodCall.Method;

            return methodInfo;
        }

        /// <summary>
        ///     Gets the <see cref="PropertyInfo" /> of a property specified by a LINQ expression.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="property">The LINQ expression representing the property.</param>
        /// <returns>The <see cref="PropertyInfo" /> of the specified property.</returns>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="property" /> is not a valid LINQ expression representing a property access.</exception>
        // ReSharper disable once MemberCanBePrivate.Global because its an extension method
        // ReSharper disable once UnusedParameter.Global because its an extension method
        public static PropertyInfo Property<TObject, TProperty>(this TObject obj, Expression<Func<TObject, TProperty>> property)
        {
            if (property.NodeType != ExpressionType.Lambda)
            {
                throw new ArgumentException($"Property must be NodeType '{ExpressionType.Lambda}', not '{property.NodeType}'.", nameof(property));
            }

            if (property.Body.NodeType != ExpressionType.MemberAccess)
            {
                throw new ArgumentException($"Property's Body.NodeType must be '{ExpressionType.MemberAccess}', not '{property.Body.NodeType}'.", nameof(property));
            }

            return (PropertyInfo)((MemberExpression)property.Body).Member;
        }

        /// <summary>
        ///     Deserializes an XML string into an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="xml">The XML string to deserialize.</param>
        /// <returns>The deserialized object.</returns>
        public static T? FromXml<T>(this string xml) where T : class
        {
            return (T?)xml.FromXml(typeof(T));
        }

        /// <summary>
        ///     Deserializes an XML string into an object of the specified type.
        /// </summary>
        /// <param name="xml">The XML string to deserialize.</param>
        /// <param name="type">The type of the object.</param>
        /// <returns>The deserialized object.</returns>
        // ReSharper disable once MemberCanBePrivate.Global because this is a part of the public API
        public static object? FromXml(this string xml, Type type)
        {
            var serializer = new XmlSerializer(type);

            using (var stringReader = new StringReader(xml))
            {
                var reader = XmlReader.Create(stringReader);
                var obj = serializer.Deserialize(reader);

                return obj;
            }
        }

        /// <summary>
        ///     Gets the value of a private field of an object.
        /// </summary>
        /// <typeparam name="T">The type of the field value.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="privateFieldName">The name of the private field.</param>
        /// <returns>The value of the private field.</returns>
        public static T? GetPrivateFieldValue<T>(this object obj, string privateFieldName)
        {
            var field = obj.GetType().GetPrivateField(privateFieldName);
            var value = field.GetValue(obj);

            return (T?)value;
        }

        /// <summary>
        ///     Sets the value of a private field of an object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="privateFieldName">The name of the private field.</param>
        /// <param name="value">The value to set.</param>
        public static void SetPrivateField(this object obj, string privateFieldName, object value)
        {
            var field = obj.GetType().GetPrivateField(privateFieldName);

            field.SetValue(obj, value);
        }

        /// <summary>
        ///     Sets the value of a property of an object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="value">The value to set.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the <paramref name="propertyName" /> is not found in the object's type.</exception>
        public static void SetProperty(this object obj, string propertyName, object value)
        {
            var property = obj.GetType().GetProperty(propertyName);

            if (property != null)
            {
                property.SetValue(obj, value);
                return;
            }

            var type = obj.GetType();
            var exception = new ArgumentOutOfRangeException(nameof(propertyName), $"Cannot find {propertyName} in {type}.");

            exception.Data.Add("type", type);
            exception.Data.Add("propertyName", propertyName);

            throw exception;
        }

        /// <summary>
        ///     Serializes an object to an XML string.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>The XML string representation of the object.</returns>
        // ReSharper disable once MemberCanBePrivate.Global because this is a part of the public API
        public static string ToXml(this object obj)
        {
            var serializer = new XmlSerializer(obj.GetType());

            using (var stringWriter = new StringWriter())
            {
                var writer = XmlWriter.Create(stringWriter);
                serializer.Serialize(writer, obj);

                return stringWriter.ToString();
            }
        }

        /// <summary>
        ///     Creates a deep copy of an object by serializing it to XML and then deserializing it.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object to clone.</param>
        /// <returns>A deep copy of the object.</returns>
        public static T? XmlClone<T>(this T obj) where T : class
        {
            var xml = obj.ToXml();
            var value = xml.FromXml(obj.GetType());

            return value as T;
        }
    }
}