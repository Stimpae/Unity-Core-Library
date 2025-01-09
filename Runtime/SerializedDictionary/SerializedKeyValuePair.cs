namespace Pastime.Core.SerializedDictionary {
    /// <summary>
    /// Represents a serializable key-value pair.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    [System.Serializable]
    public class SerializedKeyValuePair<TKey, TValue> {
        /// <summary>
        /// The key of the key-value pair.
        /// </summary>
        public TKey key;

        /// <summary>
        /// The value of the key-value pair.
        /// </summary>
        public TValue value;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerializedKeyValuePair{TKey, TValue}"/> class with the specified key and value.
        /// </summary>
        /// <param name="key">The key of the key-value pair.</param>
        /// <param name="value">The value of the key-value pair.</param>
        public SerializedKeyValuePair(TKey key, TValue value) {
            this.key = key;
            this.value = value;
        }
    }
}