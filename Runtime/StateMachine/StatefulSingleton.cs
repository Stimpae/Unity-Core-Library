using UnityEngine;

namespace Pastime.Core.StateMachine {
    /// <summary>
    /// Represents a singleton entity with a state machine.
    /// </summary>
    /// <typeparam name="T">The type of the singleton component.</typeparam>
    public abstract class StatefulSingleton<T> : StatefulEntity where T : Component {
        /// <summary>
        /// The singleton instance.
        /// </summary>
        protected static T instance;

        /// <summary>
        /// Gets a value indicating whether the singleton instance exists.
        /// </summary>
        public static bool HasInstance => instance != null;

        /// <summary>
        /// Tries to get the singleton instance.
        /// </summary>
        /// <returns>The singleton instance if it exists; otherwise, null.</returns>
        public static T TryGetInstance() => instance;

        /// <summary>
        /// Gets the current singleton instance.
        /// </summary>
        public static T Current => instance;

        /// <summary>
        /// Gets the singleton instance, creating it if it does not already exist.
        /// </summary>
        public static T Instance {
            get {
                if (instance == null) {
                    instance = FindAnyObjectByType<T>() ?? CreateNewInstance();
                }
                return instance;
            }
        }

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        protected override void Awake() {
            InitializeSingleton();
            stateMachine = new StateMachine();
        }

        /// <summary>
        /// Initializes the singleton instance.
        /// </summary>
        protected virtual void InitializeSingleton() {
            if (Application.isPlaying) {
                instance = this as T;
            }
        }

        /// <summary>
        /// Creates a new instance of the singleton component.
        /// </summary>
        /// <returns>The newly created singleton instance.</returns>
        private static T CreateNewInstance() {
            Debug.LogWarning($"No instance of {typeof(T).Name} found. Creating a new one.");
            GameObject obj = new GameObject($"{typeof(T).Name}_AutoCreated");
            return obj.AddComponent<T>();
        }
    }
}