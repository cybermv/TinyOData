namespace TinyOData.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Struct which represents data about a entity property; the data is used
    /// in the expression building process
    /// </summary>
    public class EntityPropertyInformation
    {
        public readonly string Name;
        public readonly Type Type;
        public readonly PropertyKind Kind;

        public EntityPropertyInformation(string name, Type type, PropertyKind kind)
        {
            this.Name = name;
            this.Type = type;
            this.Kind = kind;
        }

        /// <summary>
        /// Creates metadata from a given entity type
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity</typeparam>
        /// <returns>List of <see cref="EntityPropertyInformation"/> instances</returns>
        public static IEnumerable<EntityPropertyInformation> FromEntity<TEntity>()
            where TEntity : class, new()
        {
            return FromEntity(typeof(TEntity));
        }

        /// <summary>
        /// Creates metadata from a given entity type
        /// </summary>
        /// <param name="entityType">Type of the entity</param>
        /// <returns>List of <see cref="EntityPropertyInformation"/> instances</returns>
        public static IEnumerable<EntityPropertyInformation> FromEntity(Type entityType)
        {
            return entityType.GetProperties()
                .Where(p => p.PropertyType.IsPublic &&
                            (p.PropertyType.IsValueType ||
                             p.PropertyType.IsPrimitive ||
                             p.PropertyType == typeof(string)))
                .Select(propertyInfo => new EntityPropertyInformation(
                    propertyInfo.Name,
                    propertyInfo.PropertyType,
                    GetKindFromType(propertyInfo.PropertyType)));
        }

        /// <summary>
        /// Returns a property kind from a given property type
        /// </summary>
        /// <param name="type">Type of the property</param>
        /// <returns>The kind of the property</returns>
        private static PropertyKind GetKindFromType(Type type)
        {
            if (type == typeof(string))
            {
                return PropertyKind.String;
            }

            if (type == typeof(int) ||
                type == typeof(byte) ||
                type == typeof(short) ||
                type == typeof(long) ||
                type == typeof(double) ||
                type == typeof(float) ||
                type == typeof(decimal) ||
                type == typeof(uint) ||
                type == typeof(ushort) ||
                type == typeof(ulong))
            {
                return PropertyKind.Numeric;
            }

            if (type == typeof(bool))
            {
                return PropertyKind.Boolean;
            }
            return PropertyKind.Unsupported;
        }

        /// <summary>
        /// Enum that contains the possible kinds of properties on the entity
        /// </summary>
        public enum PropertyKind
        {
            Numeric,
            String,
            Boolean,
            Unsupported
        }
    }
}