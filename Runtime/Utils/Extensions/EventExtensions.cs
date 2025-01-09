using Pastime.Core.EventBus;

namespace Pastime.Core.Utils {
    /// <summary>
    /// Provides extension methods for registering, raising, deregistering, and clearing events.
    /// </summary>
    public static class EventExtensions {
        /// <summary>
        /// Registers an event with a specified action.
        /// </summary>
        /// <typeparam name="T">The type of the event.</typeparam>
        /// <param name="event">The event to register.</param>
        /// <param name="binding">The action to bind to the event.</param>
        public static void Register<T>(this T @event, System.Action binding) where T : IEvent {
            if (@event == null) return;

            var eventType = @event.GetType();
            var registerMethod = typeof(EventBus<>)
                .MakeGenericType(eventType)
                .GetMethod("Register", new[] { typeof(System.Action) });

            registerMethod?.Invoke(null, new object[] { binding });
        }

        /// <summary>
        /// Raises an event.
        /// </summary>
        /// <typeparam name="T">The type of the event.</typeparam>
        /// <param name="event">The event to raise.</param>
        public static void Raise<T>(this T @event) where T : IEvent {
            EventBus<T>.Raise(@event);
        }

        /// <summary>
        /// Deregisters an event with a specified action.
        /// </summary>
        /// <typeparam name="T">The type of the event.</typeparam>
        /// <param name="event">The event to deregister.</param>
        /// <param name="binding">The action to unbind from the event.</param>
        public static void DeRegister<T>(this T @event, System.Action binding) where T : IEvent {
            if (@event == null) return;

            var eventType = @event.GetType();
            var deregisterMethod = typeof(EventBus<>)
                .MakeGenericType(eventType)
                .GetMethod("DeRegister", new[] { typeof(System.Action) });

            deregisterMethod?.Invoke(null, new object[] { binding });
        }

        /// <summary>
        /// Clears all bindings for an event.
        /// </summary>
        /// <typeparam name="T">The type of the event.</typeparam>
        /// <param name="event">The event to clear.</param>
        public static void Clear<T>(this T @event) where T : IEvent {
            EventBus<T>.Clear();
        }
    }
}