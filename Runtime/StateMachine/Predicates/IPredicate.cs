namespace Pastime.Core.StateMachine {
    /// <summary>
    /// Defines a predicate interface for evaluating conditions.
    /// </summary>
    public interface IPredicate {
        /// <summary>
        /// Evaluates the predicate.
        /// </summary>
        /// <returns>True if the condition is met; otherwise, false.</returns>
        bool Evaluate();
    }
}