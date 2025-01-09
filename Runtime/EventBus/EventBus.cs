using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Pastime.Core.EventBus {
    /// <summary>
    /// A static class that manages event bindings and raising events for a specific event type.
    /// </summary>
    /// <typeparam name="T">The type of event.</typeparam>
    public static class EventBus<T> where T : IEvent {
        // In this case, we specifically want a static member in a generic type.
        // We want a different bindings-hashSet in each instance of different close constructed type.
        // ReSharper disable once StaticMemberInGenericType
        private static readonly HashSet<Action> BindingsWithoutArguments = new();
        private static readonly HashSet<Action<T>> BindingsWithArguments = new();

        /// <summary>
        /// Registers an event binding with an argument.
        /// </summary>
        /// <param name="binding">The action to bind to the event.</param>
        public static void Register(Action<T> binding) => BindingsWithArguments.Add(binding);

        /// <summary>
        /// Registers an event binding without an argument.
        /// </summary>
        /// <param name="binding">The action to bind to the event.</param>
        public static void Register(Action binding) => BindingsWithoutArguments.Add(binding);

        /// <summary>
        /// Deregisters an event binding with an argument.
        /// </summary>
        /// <param name="binding">The action to unbind from the event.</param>
        public static void DeRegister(Action<T> binding) => BindingsWithArguments.Remove(binding);

        /// <summary>
        /// Deregisters an event binding without an argument.
        /// </summary>
        /// <param name="binding">The action to unbind from the event.</param>
        public static void DeRegister(Action binding) => BindingsWithoutArguments.Remove(binding);

        /// <summary>
        /// Raises the event, invoking all registered bindings.
        /// </summary>
        /// <param name="event">The event to raise.</param>
        public static void Raise(T @event) {
            foreach (var binding in BindingsWithArguments) {
                binding?.Invoke(@event);
            }

            foreach (var binding in BindingsWithoutArguments) {
                binding?.Invoke();
            }
        }

        /// <summary>
        /// Removes all listeners from the event bus.
        /// </summary>
        [UsedImplicitly] // Used via reflection in EventBusUtilities.ClearAllBuses()
        public static void Clear() {
            Debug.Log($"Clearing {typeof(T).Name} bindings (removing all listeners)");
            BindingsWithArguments.Clear();
            BindingsWithoutArguments.Clear();
        }
    }
}