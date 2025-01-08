using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Thundertale.Core.SerializedDictionary {
    /// <summary>
    /// A serializable dictionary that can be used in Unity's inspector.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    [Serializable]
    public class SerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver {
        /// <summary>
        /// The list of serialized key-value pairs.
        /// </summary>
        [SerializeField] private List<SerializedKeyValuePair<TKey, TValue>> serializedPairList
            = new List<SerializedKeyValuePair<TKey, TValue>>();

        /// <summary>
        /// Method called after deserialization to populate the dictionary.
        /// </summary>
        public void OnAfterDeserialize() {
            Clear();
            foreach (var kvp in serializedPairList) {
#if UNITY_EDITOR
                if (!ContainsKey(kvp.key))
                    Add(kvp.key, kvp.value);
#else
                    Add(kvp.key, kvp.value);
#endif
            }

#if UNITY_EDITOR

#else
            serializedPairList.Clear();
#endif
        }

        /// <summary>
        /// Method called before serialization.
        /// Required by the <see cref="ISerializationCallbackReceiver"/> interface.
        /// </summary>
        public void OnBeforeSerialize() {
        }
    }
}