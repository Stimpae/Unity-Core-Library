using System;
using System.Collections.Generic;
using System.Linq;

namespace Thundertale.Core.StateMachine {
    /// <summary>
    /// Represents a state machine that manages states and transitions.
    /// </summary>
    public class StateMachine : IDisposable {
        /// <summary>
        /// The current state node.
        /// </summary>
        StateNode m_currentNode;

        /// <summary>
        /// A dictionary of state nodes keyed by their type.
        /// </summary>
        readonly Dictionary<Type, StateNode> m_nodes = new();

        /// <summary>
        /// A set of transitions that can occur from any state.
        /// </summary>
        readonly HashSet<Transition> m_anyTransitions = new();
        
        /// <summary>
        /// Gets the current state.
        /// </summary>
        public IState CurrentState => m_currentNode.State;

        /// <summary>
        /// Updates the state machine every frame.
        /// </summary>
        public void Update() {
            if(CurrentState == null) return;
            var transition = GetTransition();

            if (transition != null) {
                ChangeState(transition.To);
                foreach (var node in m_nodes.Values) {
                    ResetPredicateFlags(node.Transitions);
                }
                ResetPredicateFlags(m_anyTransitions);
            }

            m_currentNode.State?.Update();
        }

        /// <summary>
        /// Resets the predicate flags for the given transitions.
        /// </summary>
        /// <param name="transitions">The transitions to reset.</param>
        private static void ResetPredicateFlags(IEnumerable<Transition> transitions) {
            var enumerable = transitions.ToList();
            foreach (var transition in enumerable.OfType<Transition<ActionPredicate>>()) {
                transition.condition.flag = false;
            }
            foreach (var transition in enumerable.OfType<Transition<EventPredicate>>()) {
                transition.condition.flag = false;
            }
        }

        /// <summary>
        /// Updates the state machine at fixed intervals.
        /// </summary>
        public void FixedUpdate() {
            if(CurrentState == null) return;
            m_currentNode.State?.FixedUpdate();
        }

        /// <summary>
        /// Sets the current state of the state machine.
        /// </summary>
        /// <param name="state">The state to set.</param>
        public void SetState(IState state) {
            m_currentNode = m_nodes[state.GetType()];
            m_currentNode.State?.OnEnter();
        }

        /// <summary>
        /// Changes the current state to the specified state.
        /// </summary>
        /// <param name="state">The state to change to.</param>
        void ChangeState(IState state) {
            if (state == m_currentNode.State)
                return;

            var previousState = m_currentNode.State;
            var nextState = m_nodes[state.GetType()].State;

            previousState?.OnExit();
            nextState.OnEnter();
            m_currentNode = m_nodes[state.GetType()];
        }

        /// <summary>
        /// Adds a transition between two states with a specified condition.
        /// </summary>
        /// <typeparam name="T">The type of the condition.</typeparam>
        /// <param name="from">The state to transition from.</param>
        /// <param name="to">The state to transition to.</param>
        /// <param name="condition">The condition for the transition.</param>
        public void AddTransition<T>(IState from, IState to, T condition) {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }

        /// <summary>
        /// Adds a transition from any state to a specified state with a specified condition.
        /// </summary>
        /// <typeparam name="T">The type of the condition.</typeparam>
        /// <param name="to">The state to transition to.</param>
        /// <param name="condition">The condition for the transition.</param>
        public void AddAnyTransition<T>(IState to, T condition) {
            m_anyTransitions.Add(new Transition<T>(GetOrAddNode(to).State, condition));
        }

        /// <summary>
        /// Gets the transition that should occur based on the current state and conditions.
        /// </summary>
        /// <returns>The transition to occur, or null if no transition should occur.</returns>
        Transition GetTransition() {
            foreach (var transition in m_anyTransitions)
                if (transition.Evaluate())
                    return transition;

            foreach (var transition in m_currentNode.Transitions) {
                if (transition.Evaluate())
                    return transition;
            }

            return null;
        }

        /// <summary>
        /// Gets or adds a state node for the specified state.
        /// </summary>
        /// <param name="state">The state to get or add a node for.</param>
        /// <returns>The state node for the specified state.</returns>
        StateNode GetOrAddNode(IState state) {
            var node = m_nodes.GetValueOrDefault(state.GetType());
            if (node == null) {
                node = new StateNode(state);
                m_nodes[state.GetType()] = node;
            }
            else {
                node.State = state; // set the state in case it was changed??
            }

            return node;
        }
        
        /// <summary>
        /// Disposes of the state machine and its resources.
        /// </summary>
        public void Dispose() {
            foreach (var transition in m_anyTransitions.OfType<Transition<EventPredicate>>()) { 
                transition.condition.Dispose();
            }
            foreach (var node in m_nodes.Values.SelectMany(n => n.Transitions.OfType<Transition<EventPredicate>>())) {
                node.condition.Dispose();
            }
        }
        
        /// <summary>
        /// Represents a node in the state machine.
        /// </summary>
        class StateNode {
            /// <summary>
            /// Gets or sets the state associated with this node.
            /// </summary>
            public IState State { get; set; }

            /// <summary>
            /// Gets the transitions from this node.
            /// </summary>
            public HashSet<Transition> Transitions { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="StateNode"/> class with the specified state.
            /// </summary>
            /// <param name="state">The state associated with this node.</param>
            public StateNode(IState state) {
                State = state;
                Transitions = new HashSet<Transition>();
            }

            /// <summary>
            /// Adds a transition from this node to another state with a specified condition.
            /// </summary>
            /// <typeparam name="T">The type of the condition.</typeparam>
            /// <param name="to">The state to transition to.</param>
            /// <param name="predicate">The condition for the transition.</param>
            public void AddTransition<T>(IState to, T predicate) {
                Transitions.Add(new Transition<T>(to, predicate));
            }
        }
    }
}