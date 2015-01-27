using System;
using System.Linq.Expressions;
using System.Reflection;

namespace OpenMagic.Reflection
{
    public static class Type<T>
    {
        /// <summary>
        ///     Get PropertyInfo a property via LINQ expression.
        /// </summary>
        public static PropertyInfo Property<TValue>(Expression<Func<T, TValue>> value)
        {
            if (value.NodeType != ExpressionType.Lambda)
            {
                throw new ArgumentException(String.Format("Value must be NodeType '{0}', not '{1}'.", ExpressionType.Lambda, value.NodeType), "value");
            }

            if (value.Body.NodeType != ExpressionType.MemberAccess)
            {
                throw new ArgumentException(String.Format("Value's Body.NodeType must be '{0}', not '{1}'.", ExpressionType.MemberAccess, value.Body.NodeType), "value");
            }

            return (PropertyInfo) ((MemberExpression) value.Body).Member;
        }
    }
}