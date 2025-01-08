using System;

namespace Thundertale.Core.StateMachine {
    /// <summary>
    /// Represents a predicate that is triggered by an action.
    /// </summary>
    public class ActionPredicate : IPredicate {
        /// <summary>
        /// A flag indicating whether the action has been triggered.
        /// </summary>
        public bool flag;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionPredicate"/> class and subscribes to the specified action.
        /// </summary>
        /// <param name="eventReaction">The action to subscribe to.</param>
        public ActionPredicate(ref Action eventReaction) => eventReaction += () => flag = true;

        /// <summary>
        /// Evaluates the predicate.
        /// </summary>
        /// <returns>True if the action has been triggered; otherwise, false.</returns>
        public bool Evaluate() {
            bool result = flag;
            flag = false;
            return result;
        }
    }
}