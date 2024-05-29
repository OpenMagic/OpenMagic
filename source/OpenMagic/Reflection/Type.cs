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
                throw new ArgumentException($"Value must be NodeType '{ExpressionType.Lambda}', not '{value.NodeType}'.", nameof(value));
            }

            if (value.Body.NodeType != ExpressionType.MemberAccess)
            {
                throw new ArgumentException($"Value's Body.NodeType must be '{ExpressionType.MemberAccess}', not '{value.Body.NodeType}'.", nameof(value));
            }

            return (PropertyInfo)((MemberExpression)value.Body).Member;
        }
    }
}