using UnityEngine;

namespace Pastime.Core.Utils {
    /// <summary>
    /// A component that holds a SerializableGuid.
    /// </summary>
    [DisallowMultipleComponent]
    public class GuidComponent : MonoBehaviour {
        /// <summary>
        /// The SerializableGuid associated with this component.
        /// </summary>
        [SerializeField] private SerializableGuid guid = SerializableGuid.NewGuid();
        
        /// <summary>
        /// Gets the SerializableGuid associated with this component.
        /// </summary>
        public SerializableGuid Guid => guid;
        
        /// <summary>
        /// Resets the SerializableGuid to a new value.
        /// </summary>
        public virtual void Reset() {
            guid = SerializableGuid.NewGuid();
        }
        
        /// <summary>
        /// Generates a new SerializableGuid.
        /// </summary>
        public virtual void GenerateNewGuid() {
            guid = SerializableGuid.NewGuid();
        }
        
        /// <summary>
        /// Generates a new SerializableGuid if the current one is empty.
        /// </summary>
        public virtual void GenerateNewGuidIfEmpty() {
            if (guid == SerializableGuid.Empty) {
                guid = SerializableGuid.NewGuid();
            }
        }
    }
}