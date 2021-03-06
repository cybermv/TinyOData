﻿namespace TinyOData.Utility
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class which represents data about a entity property; the data is used
    /// in the expression building process
    /// </summary>
    public class PropertyMetadata
    {
        private static readonly ConcurrentDictionary<string, IEnumerable<PropertyMetadata>> CachedMetadata;

        static PropertyMetadata()
        {
            CachedMetadata = new ConcurrentDictionary<string, IEnumerable<PropertyMetadata>>();
        }

        private PropertyMetadata()
        {
        }

        public string Name { get; private set; }

        public Type Type { get; private set; }

        public PropertyKind Kind { get; private set; }

        /// <summary>
        /// Creates metadata from a given entity type
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity</typeparam>
        /// <returns>List of <see cref="PropertyMetadata"/> instances</returns>
        public static IEnumerable<PropertyMetadata> FromEntity<TEntity>()
            where TEntity : class, new()
        {
            return FromEntity(typeof(TEntity));
        }

        /// <summary>
        /// Creates metadata from a given entity type
        /// </summary>
        /// <param name="entityType">Type of the entity</param>
        /// <returns>List of <see cref="PropertyMetadata"/> instances</returns>
        public static IEnumerable<PropertyMetadata> FromEntity(Type entityType)
        {
            return CachedMetadata.GetOrAdd(entityType.Name, _ => CreateMetadata(entityType));
        }

        private static IEnumerable<PropertyMetadata> CreateMetadata(Type entityType)
        {
            return entityType.GetProperties()
                .Where(p => p.PropertyType.IsPublic &&
                            (p.PropertyType.IsValueType ||
                             p.PropertyType.IsPrimitive ||
                             p.PropertyType == typeof(string)))
                .Select(p => new PropertyMetadata
                {
                    Name = p.Name,
                    Type = p.PropertyType,
                    Kind = GetKindFromType(p.PropertyType)
                });
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