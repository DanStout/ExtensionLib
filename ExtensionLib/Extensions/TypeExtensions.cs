using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionLib
{
    /// <summary>
    /// Extensions for Type
    /// </summary>
    public static class TypeExtensions
    {
        ///<summary>Return whether this type is an instance of the other type or its nullable version</summary>
        public static bool Is(this Type type, Type otherType)
        {
            return
            (
                type == otherType ||
                type == otherType.GetNullableType() ||
                type.GetNullableType() == otherType
            );
        }

        ///<summary>Returns whether this type is able to do a proper equality comparison</summary>
        public static bool IsEquatable(this Type type)
        {
            return type.IsPrimitive || type.IsValueType || type.Implements(typeof(IComparable)) || type.Implements(typeof(IEquatable<>));
        }

        ///<summary>Return whether a type implements a given interface</summary>
        public static bool Implements(this Type type, Type interfaceType)
        {
            Debug.Assert(interfaceType.IsInterface);

            if (interfaceType.IsGenericType)
                return type
                .GetInterfaces()
                .Where(i => i.IsGenericType)
                .Any(i => i.GetGenericTypeDefinition() == interfaceType);
            else return interfaceType.IsAssignableFrom(type);
        }

        ///<summary>Get the nullable version of this type (returns the type itself for non-value types)</summary>
        public static Type GetNullableType(this Type type)
        {
            var nullVersion = Nullable.GetUnderlyingType(type);
            if (nullVersion != null) return type; //it's already nullable
            if (type.IsValueType) //therefore it can be nullable
                return typeof(Nullable<>).MakeGenericType(type);
            return type;
        }
    }
}
