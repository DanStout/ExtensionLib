using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionLib
{
    /// <summary>
    /// Extensions for the unconstrained generic type
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        /// Clone an object by serializing it to a JSON string, and then deserializing that JSON back into the object
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="source">Source object</param>
        /// <returns>A clone of the source object</returns>
        public static T CloneByJson<T>(this T source)
        {
            if (object.ReferenceEquals(source, null))
                return default(T);
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));
        }

        /// <summary>
        /// Get the public properties of this object as key-value pairs
        /// </summary>
        /// <typeparam name="T">The type of the object to get properties for</typeparam>
        /// <param name="obj">The object</param>
        /// <param name="ignoreProps">Lambda expressions indicating the properties to ignore</param>
        /// <returns>An enumeration of the key-value pairs</returns>
        public static IEnumerable<KeyValuePair<string, object>> GetPropertyNameValuePairs<T>(this T obj, params Expression<Func<T, object>>[] ignoreProps)
        {
            var props = obj
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .AsEnumerable();

            foreach (var ignoredProp in ignoreProps)
            {
                var propertyInfo = ((MemberExpression)ignoredProp.Body).Member as PropertyInfo;
                if (propertyInfo == null)
                    throw new ArgumentException("ignoreProp must be a property");

                props = props.Where(prop => prop != propertyInfo);
            }

            return props.Select(prop => new KeyValuePair<string, object>(prop.Name, prop.GetValue(obj)));
        }

        /// <summary>
        /// Uses reflection to make a ToString describing the non-null public properties of this object, optionally excluding given properties.
        /// </summary>
        /// <typeparam name="T">Type of object to make ToString for</typeparam>
        /// <param name="obj">This object</param>
        /// <param name="ignoreProp">Lambda expressions indicating properties to ignore</param>
        /// <returns>String describing the object</returns>
        public static string MakeToString<T>(this T obj, params Expression<Func<T, object>>[] ignoreProp)
        {
            var name = obj.GetType().Name;
            var props = obj.GetPropertyNameValuePairs(ignoreProp).ToList();
            StringBuilder builder = new StringBuilder(name + ": [");

            for (var i = 0; i < props.Count; i++)
            {
                var prop = props[i];
                var valueRep = prop.Value;
                if (valueRep == null)
                    valueRep = "null";
                else if (valueRep is string)
                    valueRep = valueRep.Wrap('"');
                builder.Append("{0}:{1}".FormatWith(prop.Key, valueRep));
                if (i < props.Count - 1) builder.Append(", ");
            }
            return builder.Append("]").ToString();
        }

        ///<summary>Wrap an object with a given object's ToString on each end</summary>
        public static string Wrap<T>(this T obj, object wrap)
        {
            return string.Format("{0}{1}{2}", wrap, obj, wrap);
        }
    }
}
