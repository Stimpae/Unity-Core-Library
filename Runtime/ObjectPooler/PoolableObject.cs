using System;
using System.Collections;
using UnityEngine;

namespace Pastime.Core.ObjectPooler {
    /// <summary>
    /// This class is used to provide a callback to objects that are pooled.
    /// It is added automatically to objects that are pooled
    /// </summary>
    [DisallowMultipleComponent]
    public class PoolableObject : MonoBehaviour {
        private IPoolCallbackReceiver[] m_poolCallbackReceivers = Array.Empty<IPoolCallbackReceiver>();
        
        private float m_lifeTime;
        
        private void Awake() {
            m_poolCallbackReceivers = GetComponents<IPoolCallbackReceiver>();
        }
        
        public void OnReuse() {
            if (m_poolCallbackReceivers.Length == 0) return;
            foreach (var receiver in m_poolCallbackReceivers) {
                receiver.OnReuse();
            }
        }

        private void OnDestroy() {
            ObjectPool.Remove(gameObject);
        }

        public void SetLifeTime(float lifeTime) {
            if (m_lifeTime > 0) {
                StartCoroutine(LifeTimeCoroutine(lifeTime));
            }
        }

        private IEnumerator LifeTimeCoroutine(float lifeTime) {
            yield return new WaitForSeconds(lifeTime);
            Debug.Log("Object released due to lifetime");
            gameObject.Release();
        }

        public void OnRelease() {
            if (m_poolCallbackReceivers.Length == 0) return;
            foreach (var receiver in m_poolCallbackReceivers) {
                receiver.OnRelease();
            }
        }
    }
}