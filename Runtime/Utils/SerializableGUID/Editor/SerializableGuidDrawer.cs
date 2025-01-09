using System.Text;
using UnityEditor;
using UnityEngine;

namespace Pastime.Core.Utils.Editor {
    /// <summary>
    /// Custom property drawer for SerializableGuid.
    /// </summary>
    [CustomPropertyDrawer(typeof(SerializableGuid))]
    public class SerializableGuidDrawer : PropertyDrawer {
        /// <summary>
        /// The vertical separation between elements.
        /// </summary>
        private const float Y_SEP = 20;

        /// <summary>
        /// The size of the button.
        /// </summary>
        private float m_buttonSize;

        /// <summary>
        /// Draws the GUI for the SerializableGuid property.
        /// </summary>
        /// <param name="position">The position of the property.</param>
        /// <param name="property">The serialized property.</param>
        /// <param name="label">The label of the property.</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);
            var value0 = property.FindPropertyRelative("part1");
            var value1 = property.FindPropertyRelative("part2");
            var value2 = property.FindPropertyRelative("part3");
            var value3 = property.FindPropertyRelative("part4");

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            m_buttonSize = 80;
            const int padding = 5;

            // Buttons
            if(GUI.Button(new Rect(position.xMin - (m_buttonSize + padding), position.yMin, m_buttonSize, Y_SEP - 2), "New Guid")) {
                var newGuid = SerializableGuid.NewGuid();
                if (value0 != null) value0.uintValue = newGuid.part1;
                if (value1 != null) value1.uintValue = newGuid.part2;
                if (value2 != null) value2.uintValue = newGuid.part3;
                if (value3 != null) value3.uintValue = newGuid.part4;
            }

            if (value0 != null && value1 != null && value2 != null && value3 != null) {
                EditorGUI.SelectableLabel(position,
                    new StringBuilder()
                        .AppendFormat("{0:X8}", (uint) value0.intValue)
                        .AppendFormat("{0:X8}", (uint) value1.intValue)
                        .AppendFormat("{0:X8}", (uint) value2.intValue)
                        .AppendFormat("{0:X8}", (uint) value3.intValue)
                        .ToString());
            } else {
                EditorGUI.SelectableLabel(position, "GUID Not Initialized");
            }

            EditorGUI.EndProperty();
        }
    }
}