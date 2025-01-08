using UnityEngine;

namespace Thundertale.Core.StateMachine {
    /// <summary>
    /// Represents an entity with a state machine.
    /// </summary>
    public abstract class StatefulEntity : MonoBehaviour {
        /// <summary>
        /// The state machine managing the entity's states.
        /// </summary>
        protected StateMachine stateMachine;
        
        /// <summary>
        /// Gets the state machine.
        /// </summary>
        public StateMachine StateMachine => stateMachine;
        
        /// <summary>
        /// Awake or Start can be used to declare all states and transitions.
        /// </summary>
        /// <example>
        /// <code>
        /// protected override void Awake() {
        ///     base.Awake();
        /// 
        ///     var state = new State1(this);
        ///     var anotherState = new State2(this);
        ///
        ///     At(state, anotherState, () => true);
        ///     At(state, anotherState, myFunc);
        ///     At(state, anotherState, myPredicate);
        /// 
        ///     Any(anotherState, () => true);
        ///
        ///     stateMachine.SetState(state);
        /// }
        /// </code> 
        /// </example>
        protected virtual void Awake() {
            stateMachine = new StateMachine();
        }

        /// <summary>
        /// Updates the state machine every frame.
        /// </summary>
        protected virtual void Update() => stateMachine.Update();

        /// <summary>
        /// Updates the state machine at fixed intervals.
        /// </summary>
        protected virtual void FixedUpdate() => stateMachine.FixedUpdate();

        /// <summary>
        /// Adds a transition between two states with a specified condition.
        /// </summary>
        /// <typeparam name="T">The type of the condition.</typeparam>
        /// <param name="from">The state to transition from.</param>
        /// <param name="to">The state to transition to.</param>
        /// <param name="condition">The condition for the transition.</param>
        protected void At<T>(IState from, IState to, T condition) => stateMachine.AddTransition(from, to, condition);

        /// <summary>
        /// Adds a transition from any state to a specified state with a specified condition.
        /// </summary>
        /// <typeparam name="T">The type of the condition.</typeparam>
        /// <param name="to">The state to transition to.</param>
        /// <param name="condition">The condition for the transition.</param>
        protected void Any<T>(IState to, T condition) => stateMachine.AddAnyTransition(to, condition);
    }
}