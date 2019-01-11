using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Text;

namespace LoxSmoke.DocXml.Reflection
{
    public class TypeCollection
    {
        public class References
        {
            /// <summary>
            /// The type that this class describes
            /// </summary>
            public Type Type { get; set; }
            /// <summary>
            /// Other types referencing this type.
            /// </summary>
            public HashSet<Type> ReferencesIn { get; set; }
            /// <summary>
            /// Other types referenced by this type.
            /// </summary>
            public HashSet<Type> ReferencesOut { get; set; }

            public void AddReferenceIn(Type type)
            {
                if (type == null) return;
                if (ReferencesIn == null) ReferencesIn = new HashSet<Type>();
                ReferencesIn.Add(type);
            }
            public void AddReferenceOut(Type type)
            {
                if (type == null) return;
                if (ReferencesOut == null) ReferencesOut = new HashSet<Type>();
                ReferencesOut.Add(type);
            }
        }

        /// <summary>
        /// Reflection settings that should be used when looking for referenced types.
        /// </summary>
        public ReflectionSettings Settings { get; set; } = ReflectionSettings.Default;

        /// <summary>
        /// All referenced types.
        /// </summary>
        public Dictionary<Type, References> ReferencedTypes { get; set; } 
            = new Dictionary<Type, References>();
        
        /// <summary>
        /// Types that had their data and functions examined.
        /// </summary>
        protected HashSet<Type> VisitedPropTypes { get; set; } = new HashSet<Type>();
        /// <summary>
        /// Types that need to have their properties, methods and fields examined.
        /// </summary>
        protected Queue<Type> PendingPropTypes { get; set; } = new Queue<Type>();

        /// <summary>
        /// Cached information from ExamineAssemblies call.
        /// Contains the set of assemblies that should be checked or ignored.
        /// </summary>
        protected Dictionary<Assembly, bool> CheckAssemblies { get; set; } = new Dictionary<Assembly, bool>();
        /// <summary>
        /// Cached information from the ExamineTypes call.
        /// Contains the set of types that should be ignored.
        /// </summary>
        protected HashSet<Type> IgnoreTypes { get; set; } = new HashSet<Type>();

        public void GetReferencedTypes(Type type, ReflectionSettings settings = null)
        {
            Settings = settings ?? ReflectionSettings.Default;
            ReferencedTypes = new Dictionary<Type, References>();
            VisitedPropTypes = new HashSet<Type>();
            PendingPropTypes = new Queue<Type>();
            PendingPropTypes.Enqueue(type);
            CheckAssemblies = new Dictionary<Assembly, bool>();
            IgnoreTypes = new HashSet<Type>();

            while (PendingPropTypes.Count > 0)
            {
                var theType = PendingPropTypes.Dequeue();
                UnwrapType(null, theType);
                GetReferencedTypes(theType);
            }
        }

        /// <summary>
        /// Returns true for types that should be checked.
        /// </summary>
        /// <param name="type">Check this type?</param>
        /// <returns></returns>
        bool CheckType(Type type)
        {
            if (IgnoreTypes.Contains(type)) return false;
            // Check if type assembly should be checked or ignored
            if (Settings.ExamineAssemblies != null)
            {
                if (!CheckAssemblies.ContainsKey(type.Assembly))
                {
                    CheckAssemblies.Add(type.Assembly, Settings.ExamineAssemblies(type.Assembly));
                }
                if (!CheckAssemblies[type.Assembly]) return false;
            }
            // If we have filtering function then ask if type is OK
            if (Settings.ExamineTypes == null ||
                Settings.ExamineTypes(type)) return true;
            IgnoreTypes.Add(type);
            return false;
        }

        protected void GetReferencedTypes(Type type)
        {
            if (VisitedPropTypes.Contains(type))
            {
                // todo: add referenced by
                return;
            }
            if (!CheckType(type)) return;
            VisitedPropTypes.Add(type);
            foreach (var info in type.GetProperties(Settings.PropertyFlags))
            {
                UnwrapType(type, info.PropertyType);
                if (info.GetMethod?.GetParameters()?.Length > 0)
                {
                    UnwrapType(type, info.GetMethod.GetParameters()[0].ParameterType);
                }
                else if (info.SetMethod?.GetParameters()?.Length > 1)
                {
                    UnwrapType(type, info.SetMethod.GetParameters()[1].ParameterType);
                }
            }
            foreach (var info in type.GetMethods(Settings.MethodFlags))
            {
                UnwrapType(type, info.ReturnType);
                if (!(info.GetParameters()?.Length > 0)) continue;
                foreach(var parameter in info.GetParameters()) UnwrapType(type, parameter.ParameterType);
            }
            foreach (var info in type.GetFields(Settings.FieldFlags))
            {
                UnwrapType(type, info.FieldType);
            }

            foreach (var info in type.GetNestedTypes(Settings.NestedTypeFlags))
            {
                UnwrapType(type, info);
            }
        }

        /// <summary>
        /// Recursivelly "unwrap" the type.
        /// </summary>
        /// <param name="type"></param>
        public void UnwrapType(Type parentType, Type type)
        {
            if (ReferencedTypes.ContainsKey(type))
            {
                if (parentType != null)
                {
                    ReferencedTypes[type].AddReferenceIn(parentType);
                    ReferencedTypes[parentType].AddReferenceOut(type);
                }
                return;
            }
            // Some types could be wrapped in generic types that should not be checked
            var checkType = CheckType(type);

            if (type.IsConstructedGenericType) // List<int>
            {
                UnwrapType(parentType, type.GetGenericTypeDefinition());
                if (!(type.GenericTypeArguments?.Length > 0)) return;
                foreach (var argType in type.GenericTypeArguments) UnwrapType(parentType, argType);
            }
            else if (type.IsGenericParameter)  // void Method<T>()   <-- T in generic class
            {
                return;
            }
            else if (type.IsGenericTypeDefinition) // List<>
            {
                if (checkType) AddTypeToCheckProps(parentType, type);
            }
            else if (type.IsGenericType) // List<int>
            {
                if (type.ContainsGenericParameters)
                {
                    foreach (var argType in type.GenericTypeArguments) UnwrapType(parentType, argType);
                }
                return;
            }
            else if (type.IsArray) // SomeType[]
            {
                UnwrapType(parentType, type.GetElementType());
            }
            else
            {
                if (checkType) AddTypeToCheckProps(parentType, type);
            }
        }

        void AddTypeToCheckProps(Type parentType, Type type)
        {
            var newRef = new References() { Type = type };
            newRef.AddReferenceIn(parentType);
            if (parentType != null) ReferencedTypes[parentType].AddReferenceOut(type);
            ReferencedTypes.Add(type, newRef);
            PendingPropTypes.Enqueue(type);
        }
    }
}
