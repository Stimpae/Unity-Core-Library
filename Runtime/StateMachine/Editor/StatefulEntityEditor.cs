using Thundertale.Core.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Thundertale.Core.StateMachine.Editor {
    /// <summary>
    /// Custom editor for StatefulEntity, providing a custom inspector GUI.
    /// </summary>
    [CustomEditor(typeof(StatefulEntity), true)]
    public class StatefulEntityEditor : UnityEditor.Editor {
        /// <summary>
        /// Creates the custom inspector GUI.
        /// </summary>
        /// <returns>A VisualElement representing the custom inspector GUI.</returns>
        public override VisualElement CreateInspectorGUI() {
            // Create a container for the custom UI
            var root = new VisualElement();

            // Add the default inspector
            var defaultInspector = new IMGUIContainer(() => DrawDefaultInspector());
            root.Add(defaultInspector);

            // Add a splitter for visual separation
            var splitter = EditorStyleUtils.CreateSplitter(10, 20);
            root.Add(splitter);

            // Add a runtime state label
            var currentStateLabel = new Label("Current State: None");
            root.Add(currentStateLabel);

            // Add a message for non-Play Mode
            var playModeMessage = new HelpBox("Enter Play Mode to debug the current state.", HelpBoxMessageType.Info);
            root.Add(playModeMessage);

            // Register an update callback for runtime state
            root.schedule.Execute(() => {
                if (Application.isPlaying) {
                    playModeMessage.style.display = DisplayStyle.None;

                    var statefulEntity = (StatefulEntity)target;
                    if (statefulEntity.StateMachine != null) {
                        var currentState = statefulEntity.StateMachine.CurrentState;
                        currentStateLabel.text =
                            $"Current State: {(currentState != null ? currentState.GetType().Name : "None")}";
                    } else {
                        currentStateLabel.text = "Current State: Not Initialized";
                    }
                } else {
                    playModeMessage.style.display = DisplayStyle.Flex;
                }
            }).Every(200); // Update every 100ms

            return root;
        }
    }
}