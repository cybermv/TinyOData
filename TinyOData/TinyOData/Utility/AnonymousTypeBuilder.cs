namespace TinyOData.Utility
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Threading;

    /// <summary>
    /// Utility class that creates anonymous types
    /// </summary>
    internal static class AnonymousTypeBuilder
    {
        private static readonly ModuleBuilder ModuleBuilder;
        private static readonly ConcurrentDictionary<string, Type> CachedTypes;

        static AnonymousTypeBuilder()
        {
            AssemblyName assemblyName = new AssemblyName("RuntimeCreatedTypes");
            ModuleBuilder =
                Thread.GetDomain()
                    .DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run)
                    .DefineDynamicModule(assemblyName.Name);
            CachedTypes = new ConcurrentDictionary<string, Type>();
        }

        /// <summary>
        /// Gets a <see cref="Type"/> of a class which contains all the given properties
        /// </summary>
        /// <param name="properties">The properties to add to the type</param>
        /// <returns>The created type</returns>
        public static Type From(List<EntityPropertyInformation> properties)
        {
            if (properties == null || !properties.Any())
            {
                throw new ArgumentOutOfRangeException("properties", "The type must contain at least one property");
            }

            string typeName = String.Join("_", properties.OrderBy(p => p.Name).Select(p => p.Name));

            return CachedTypes.GetOrAdd(typeName, key => CreateType(key, properties));
        }

        /// <summary>
        /// Creates a <see cref="Type"/> that contains the given properties
        /// </summary>
        /// <param name="typeName">Name of the new type</param>
        /// <param name="properties">The properties of the created runtime type</param>
        /// <returns>The created type</returns>
        private static Type CreateType(string typeName, IEnumerable<EntityPropertyInformation> properties)
        {
            TypeBuilder typeBuilder = ModuleBuilder.DefineType(typeName,
                TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Serializable);

            foreach (EntityPropertyInformation property in properties)
            {
                typeBuilder.DefineField(property.Name, property.Type, FieldAttributes.Public);
            }

            return typeBuilder.CreateType();
        }
    }
}