using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using OpenMagic.Collections.Generic;
using OpenMagic.Reflection;

namespace OpenMagic.DataAnnotations
{
    public class ClassMetadata : IClassMetadata
    {
        private static readonly TypeCache<IClassMetadata> Cache = new TypeCache<IClassMetadata>();

        public ClassMetadata(Type type)
        {
            Type = type;
            Properties = new Lazy<IEnumerable<IPropertyMetadata>>(GetProperties);
        }

        public Lazy<IEnumerable<IPropertyMetadata>> Properties { get; private set; }
        public Type Type { get; private set; }

        public IPropertyMetadata GetProperty(string propertyName)
        {
            propertyName.MustNotBeNullOrWhiteSpace("propertyName");

            var property = Properties.Value.SingleOrDefault(p => p.PropertyInfo.Name == propertyName);

            if (property != null)
            {
                return property;
            }

            throw new ArgumentException(string.Format("Cannot find {0} property for {1}.", propertyName, Type), "propertyName");
        }

        public IPropertyMetadata GetProperty(PropertyInfo property)
        {
            return GetProperty(property.Name);
        }

        public static IClassMetadata Get<TModel>()
        {
            return Cache.Get<TModel>(() => new ClassMetadata(typeof(TModel)));
        }

        public static IPropertyMetadata GetProperty<TModel, TProperty>(Expression<Func<TModel, TProperty>> property)
        {
            var modelMetadata = Get<TModel>();
            var propertyInfo = Type<TModel>.Property(property);
            var propertyMetadata = modelMetadata.GetProperty(propertyInfo);

            return propertyMetadata;
        }

        private IEnumerable<IPropertyMetadata> GetProperties()
        {
            var publicProperties = from p in Type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                select new PropertyMetadata(p, true);

            var privateProperties = from p in Type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance)
                select new PropertyMetadata(p, false);

            return publicProperties.Concat(privateProperties);
        }
    }
}