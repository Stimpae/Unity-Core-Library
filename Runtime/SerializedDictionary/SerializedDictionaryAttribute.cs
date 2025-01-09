using System.Diagnostics;
using UnityEngine;

namespace Pastime.Core.SerializedDictionary{
    /// <summary>
    /// Attribute to mark a property as a serialized dictionary.
    /// This attribute is only active in the Unity Editor.
    /// </summary>
    [Conditional("UNITY_EDITOR")]
    public class SerializedDictionaryAttribute : PropertyAttribute {
        /// <summary>
        /// The name of the key type in the dictionary.
        /// </summary>
        public readonly string keyTypeName;

        /// <summary>
        /// The name of the value type in the dictionary.
        /// </summary>
        public readonly string valueTypeName;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerializedDictionaryAttribute"/> class with the specified key and value type names.
        /// </summary>
        /// <param name="keyTypeName">The name of the key type.</param>
        /// <param name="valueTypeName">The name of the value type.</param>
        public SerializedDictionaryAttribute(string keyTypeName, string valueTypeName) {
            this.keyTypeName = keyTypeName;
            this.valueTypeName = valueTypeName;
        }
    }
}