using System;
using System.Linq.Expressions;
using System.Reflection;

namespace OpenMagic.Reflection
{
    public static class ObjectExtensions
    {
        public static MethodInfo Method<TObject>(this TObject obj, Expression<Action<TObject>> method)
        {
            // unit test for this argument test.
            if (method.NodeType != ExpressionType.Lambda)
            {
                throw new ArgumentException(String.Format("method must be NodeType '{0}', not '{1}'.", ExpressionType.Lambda, method.NodeType), "method");
            }

            // unit test for this argument test.
            if (method.Body.NodeType != ExpressionType.Call)
            {
                throw new ArgumentException(String.Format("method's Body.NodeType must be '{0}', not '{1}'.", ExpressionType.Call, method.Body.NodeType), "method");
            }

            var methodCall = (MethodCallExpression) method.Body;
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
                throw new ArgumentException(String.Format("Property must be NodeType '{0}', not '{1}'.", ExpressionType.Lambda, property.NodeType), "property");
            }

            if (property.Body.NodeType != ExpressionType.MemberAccess)
            {
                throw new ArgumentException(String.Format("Property's Body.NodeType must be '{0}', not '{1}'.", ExpressionType.MemberAccess, property.Body.NodeType), "property");
            }

            return (PropertyInfo) ((MemberExpression) property.Body).Member;
        }
    }
}