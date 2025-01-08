using Thundertale.Core.SerializedDictionary;
using UnityEngine;

namespace Thundertale.Core.RuntimeSet {
    /// <summary>
    /// Manages the addition and removal of MonoBehaviour instances to a target runtime set.
    /// </summary>
    public class MonoSetMember : MonoBehaviour {
        /// <summary>
        /// The target runtime set to which the MonoBehaviour instances will be added or removed.
        /// </summary>
        [SerializeField] private MonoSet targetRuntimeDataSet;
    
        /// <summary>
        /// A dictionary of MonoBehaviour instances to be managed by the runtime set.
        /// </summary>
        [SerializedDictionary("ID", "Value")]
        [SerializeField] private SerializedDictionary<string, MonoBehaviour> setMembers = new SerializedDictionary<string, MonoBehaviour>();
    
        /// <summary>
        /// Adds the MonoBehaviour instances to the target runtime set when the object is enabled.
        /// </summary>
        private void OnEnable() {
            foreach (var setMember in setMembers) {
                targetRuntimeDataSet.Add(setMember.Key, setMember.Value);
            }
        }

        /// <summary>
        /// Removes the MonoBehaviour instances from the target runtime set when the object is disabled.
        /// </summary>
        private void OnDisable() {
            foreach (var setMember in setMembers) {
                targetRuntimeDataSet.Remove(setMember.Key, setMember.Value);
            }
        }
    }
}
