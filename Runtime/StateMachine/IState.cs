namespace Pastime.Core.StateMachine {
    /// <summary>
    /// Defines the interface for a state in the state machine.
    /// </summary>
    public interface IState {
        /// <summary>
        /// Called every frame to update the state.
        /// </summary>
        void Update() { }

        /// <summary>
        /// Called at a fixed interval to update the state.
        /// </summary>
        void FixedUpdate() { }

        /// <summary>
        /// Called when the state is entered.
        /// </summary>
        void OnEnter() { }

        /// <summary>
        /// Called when the state is exited.
        /// </summary>
        void OnExit() { }
    }
}