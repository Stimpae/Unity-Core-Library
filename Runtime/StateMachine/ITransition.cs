namespace Pastime.Core.StateMachine {
    /// <summary>
    /// Defines the interface for a transition in the state machine.
    /// </summary>
    public interface ITransition {
        /// <summary>
        /// Gets the state to transition to.
        /// </summary>
        IState To { get; }

        /// <summary>
        /// Gets the condition that must be met for the transition to occur.
        /// </summary>
        IPredicate Condition { get; }
    }
}