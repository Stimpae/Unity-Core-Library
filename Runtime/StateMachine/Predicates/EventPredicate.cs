using System;
using Pastime.Core.EventBus;
using Pastime.Core.Utils;

namespace Pastime.Core.StateMachine {
    /// <summary>
    /// Represents a predicate that is triggered by an event and can be evaluated.
    /// </summary>
    public class EventPredicate : IPredicate, IDisposable {
        /// <summary>
        /// A flag indicating whether the event has been triggered.
        /// </summary>
        public bool flag;
        
        /// <summary>
        /// The event reaction that triggers the predicate.
        /// </summary>
        private readonly IEvent m_eventReaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventPredicate"/> class and subscribes to the specified event.
        /// </summary>
        /// <param name="eventReaction">The event to subscribe to.</param>
        public EventPredicate(IEvent eventReaction) {
            m_eventReaction = eventReaction;
            m_eventReaction.Register(() => flag = true);
        }

        /// <summary>
        /// Evaluates the predicate.
        /// </summary>
        /// <returns>True if the event has been triggered; otherwise, false.</returns>
        public bool Evaluate() {
            bool result = flag;
            flag = false;
            return result;
        }

        /// <summary>
        /// Disposes the predicate by deregistering the event reaction.
        /// </summary>
        public void Dispose() => m_eventReaction.DeRegister(() => flag = true);
    }
}