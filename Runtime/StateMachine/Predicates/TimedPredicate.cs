namespace Pastime.Core.StateMachine {
    /// <summary>
    /// Represents a predicate that is evaluated based on a timer.
    /// </summary>
    public class TimedPredicate : IPredicate {
        /// <summary>
        /// The duration for the timer.
        /// </summary>
        private readonly float m_duration;

        /// <summary>
        /// The current timer value.
        /// </summary>
        private float m_timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimedPredicate"/> class with the specified duration.
        /// </summary>
        /// <param name="duration">The duration for the timer.</param>
        public TimedPredicate(float duration) => m_duration = duration;

        /// <summary>
        /// Evaluates the predicate based on the timer.
        /// </summary>
        /// <returns>True if the timer has reached the duration; otherwise, false.</returns>
        public bool Evaluate() {
            m_timer += UnityEngine.Time.deltaTime;
            if (m_timer >= m_duration) {
                m_timer = 0;
                return true;
            }
            return false;
        }
    }
}