using UnityEngine;

namespace Pastime.Core.ObjectPooler {
    /// <summary>
    /// Provides extension methods for the ObjectPool class to manage pooling operations on GameObjects.
    /// </summary>
    public static class ObjectPoolExtensions {
        /// <summary>
        /// Populates the pool with a specified number of instances of the prefab.
        /// </summary>
        /// <param name="prefab">The prefab to populate the pool with.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="createParent">Whether to create a parent for the instances.</param>
        public static void Populate(this GameObject prefab, int count, bool createParent = true) {
            ObjectPool.GetPoolByPrefab(prefab).Populate(count, createParent);
        }
        
        /// <summary>
        /// Populates the pool with a specified number of instances of the prefab, setting the parent of each instance.
        /// </summary>
        /// <param name="prefab">The prefab to populate the pool with.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="parent">The parent transform to set for each instance.</param>
        public static void Populate(this GameObject prefab, int count, Transform parent) {
            ObjectPool.GetPoolByPrefab(prefab).Populate(count, parent);
        }
        
        /// <summary>
        /// Returns whether the prefab is pooled.
        /// </summary>
        /// <param name="prefab">The prefab to check.</param>
        /// <returns>True if the prefab is pooled; otherwise, false.</returns>
        public static bool IsPooled(this GameObject prefab) {
            return ObjectPool.GetPoolByPrefab(prefab, false) != null;
        }

        /// <summary>
        /// Clears the pool associated with the prefab, optionally destroying active instances.
        /// </summary>
        /// <param name="prefab">The prefab whose pool is to be cleared.</param>
        /// <param name="destroyActive">Whether to destroy active instances.</param>
        public static void Clear(this GameObject prefab, bool destroyActive) {
            ObjectPool.GetPoolByPrefab(prefab, false)?.Clear(destroyActive);
        }

        /// <summary>
        /// Sets the lifetime of the reused instance.
        /// </summary>
        /// <param name="instance">The reused instance.</param>
        /// <param name="lifeTime">The lifetime to set.</param>
        /// <returns>The instance with the set lifetime.</returns>
        public static GameObject WithLifeTime(this GameObject instance, float lifeTime) {
            instance.GetComponent<PoolableObject>().SetLifeTime(lifeTime);
            return instance;
        }

        /// <summary>
        /// Reuses an instance from the pool.
        /// </summary>
        /// <param name="prefab">The prefab to reuse an instance of.</param>
        /// <returns>The reused GameObject instance.</returns>
        public static GameObject Reuse(this GameObject prefab) {
            return ObjectPool.GetPoolByPrefab(prefab).Reuse();
        }

        /// <summary>
        /// Reuses an instance from the pool, setting its parent.
        /// </summary>
        /// <param name="prefab">The prefab to reuse an instance of.</param>
        /// <param name="parent">The parent transform to set.</param>
        /// <returns>The reused GameObject instance.</returns>
        public static GameObject Reuse(this GameObject prefab, Transform parent) {
            return ObjectPool.GetPoolByPrefab(prefab).Reuse(parent);
        }

        /// <summary>
        /// Reuses an instance from the pool, setting its parent and world position stays flag.
        /// </summary>
        /// <param name="prefab">The prefab to reuse an instance of.</param>
        /// <param name="parent">The parent transform to set.</param>
        /// <param name="worldPositionStays">Whether to maintain the world position of the instance.</param>
        /// <returns>The reused GameObject instance.</returns>
        public static GameObject Reuse(this GameObject prefab, Transform parent, bool worldPositionStays) {
            return ObjectPool.GetPoolByPrefab(prefab).Reuse(parent, worldPositionStays);
        }

        /// <summary>
        /// Reuses an instance from the pool, setting its position and rotation.
        /// </summary>
        /// <param name="prefab">The prefab to reuse an instance of.</param>
        /// <param name="position">The position to set.</param>
        /// <param name="rotation">The rotation to set.</param>
        /// <returns>The reused GameObject instance.</returns>
        public static GameObject Reuse(this GameObject prefab, Vector3 position, Quaternion rotation) {
            return ObjectPool.GetPoolByPrefab(prefab).Reuse(position, rotation);
        }

        /// <summary>
        /// Reuses an instance from the pool, setting its position, rotation, and parent.
        /// </summary>
        /// <param name="prefab">The prefab to reuse an instance of.</param>
        /// <param name="position">The position to set.</param>
        /// <param name="rotation">The rotation to set.</param>
        /// <param name="parent">The parent transform to set.</param>
        /// <returns>The reused GameObject instance.</returns>
        public static GameObject Reuse(this GameObject prefab, Vector3 position, Quaternion rotation, Transform parent) {
            return ObjectPool.GetPoolByPrefab(prefab).Reuse(position, rotation, parent);
        }

        /// <summary>
        /// Releases an instance back to the pool or destroys it if it is not pooled.
        /// </summary>
        /// <param name="instance">The instance to release.</param>
        public static void Release(this GameObject instance) {
            if (ObjectPool.GetPoolByInstance(instance, out var pool)) {
                pool.Release(instance);
            }
            else {
                Object.Destroy(instance);
            }
        }
    }
}