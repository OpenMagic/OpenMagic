using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using OpenMagic.Extensions;

namespace OpenMagic.Reflection
{
    public static class ObjectExtensions
    {
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
        ///     Get PropertyInfo a property via LINQ expression.
        /// </summary>
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

        public static T FromXml<T>(this string xml) where T : class
        {
            return (T)xml.FromXml(typeof(T));
        }

        public static object FromXml(this string xml, Type type)
        {
            var serializer = new XmlSerializer(type);

            using (var stringReader = new StringReader(xml))
            {
                var reader = XmlReader.Create(stringReader);
                var obj = serializer.Deserialize(reader);

                return obj;
            }
        }

        public static T GetPrivateFieldValue<T>(this object obj, string privateFieldName)
        {
            var field = obj.GetType().GetPrivateField(privateFieldName);
            var value = field.GetValue(obj);

            return (T)value;
        }

        public static void SetPrivateField(this object obj, string privateFieldName, object value)
        {
            var field = obj.GetType().GetPrivateField(privateFieldName);

            field.SetValue(obj, value);
        }

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

        public static T XmlClone<T>(this T obj) where T : class
        {
            var xml = obj.ToXml();
            var value = xml.FromXml(obj.GetType());

            return (T)value;
        }
    }
}