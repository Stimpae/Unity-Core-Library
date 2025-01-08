using System;
using UnityEngine;

namespace Thundertale.Core.ObjectPooler {
    /// <summary>
    /// Manages a pool of reusable GameObjects to optimize performance by reducing the need for frequent instantiation and destruction.
    /// </summary>
    public class ObjectPooler : MonoBehaviour {
        /// <summary>
        /// Represents an item to be pooled, including its prefab and the amount to pool.
        /// </summary>
        [Serializable]
        public struct PoolItem {
            /// <summary>
            /// The prefab to be pooled.
            /// </summary>
            public GameObject prefab;
            
            /// <summary>
            /// The amount of the prefab to pool.
            /// </summary>
            public int amount;
            
            /// <summary>
            /// Initializes a new instance of the <see cref="PoolItem"/> struct with the specified prefab and amount.
            /// </summary>
            /// <param name="prefab">The prefab to be pooled.</param>
            /// <param name="amount">The amount of the prefab to pool.</param>
            public PoolItem(GameObject prefab, int amount) {
                this.prefab = prefab;
                this.amount = amount;
            }
        }
        
        /// <summary>
        /// Whether to pool objects on Awake.
        /// </summary>
        [SerializeField] private bool poolOnAwake = false;
        
        /// <summary>
        /// The items to be pooled.
        /// </summary>
        [SerializeField] private PoolItem[] itemsToPool;
        
        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake() {
            if (poolOnAwake) {
                PoolObjects();
            }
        }
        
        /// <summary>
        /// Sets the items to be pooled.
        /// </summary>
        /// <param name="items">The items to be pooled.</param>
        public void SetItemsToPool(PoolItem[] items) {
            itemsToPool = items;
        }
    
        /// <summary>
        /// Pools the objects specified in the itemsToPool array.
        /// </summary>
        public void PoolObjects() {
            foreach (var item in itemsToPool) {
                item.prefab.Clear(true);
                item.prefab.Populate(item.amount);
            }
        }
    }
}