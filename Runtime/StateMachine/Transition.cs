using System;

namespace Thundertale.Core.StateMachine {
    /// <summary>
    /// Represents an abstract transition between states.
    /// </summary>
    public abstract class Transition {
        /// <summary>
        /// Gets the state to transition to.
        /// </summary>
        public IState To { get; protected set; }

        /// <summary>
        /// Evaluates whether the transition condition is met.
        /// </summary>
        /// <returns>True if the transition condition is met; otherwise, false.</returns>
        public abstract bool Evaluate();
    }

    /// <summary>
    /// Represents a transition between states with a specified condition.
    /// </summary>
    /// <typeparam name="T">The type of the condition.</typeparam>
    public class Transition<T> : Transition {
        /// <summary>
        /// The condition for the transition.
        /// </summary>
        public readonly T condition;

        /// <summary>
        /// Initializes a new instance of the <see cref="Transition{T}"/> class with the specified state and condition.
        /// </summary>
        /// <param name="to">The state to transition to.</param>
        /// <param name="condition">The condition for the transition.</param>
        public Transition(IState to, T condition) {
            To = to;
            this.condition = condition;
        }

        /// <summary>
        /// Evaluates whether the transition condition is met.
        /// </summary>
        /// <returns>True if the transition condition is met; otherwise, false.</returns>
        public override bool Evaluate() {
            return (condition as Func<bool>)?.Invoke() ??
                   (condition as TimedPredicate)?.Evaluate() ??
                   (condition as EventPredicate)?.Evaluate() ??
                   (condition as ActionPredicate)?.Evaluate() ??
                   (condition as IPredicate)?.Evaluate() ?? false;
        }
    }
}