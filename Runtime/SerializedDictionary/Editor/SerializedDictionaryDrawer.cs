using System.Reflection;
using Pastime.Core.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Pastime.Core.SerializedDictionary.Editor {
    /// <summary>
    /// Custom property drawer for SerializedDictionary.
    /// </summary>
    [CustomPropertyDrawer(typeof(SerializedDictionary<,>), true)]
    public class SerializedDictionaryDrawer : PropertyDrawer {
        /// <summary>
        /// Creates the property GUI for the serialized dictionary.
        /// </summary>
        /// <param name="property">The serialized property.</param>
        /// <returns>A VisualElement representing the property GUI.</returns>
        public override VisualElement CreatePropertyGUI(SerializedProperty property) {
            var container = new VisualElement {
                style = { marginTop = 5, marginBottom = 5 }
            };
            
            var fieldAttribute = fieldInfo.GetCustomAttribute(typeof(SerializedDictionaryAttribute));
            string keyName = fieldAttribute != null ? ((SerializedDictionaryAttribute)fieldAttribute).keyTypeName : "Key";
            string valueName = fieldAttribute != null ? ((SerializedDictionaryAttribute)fieldAttribute).valueTypeName : "Value";

            var listView = EditorStyleUtils.CreateMultiListView("serializedPairList", property.displayName)
                .AddColumn("key", keyName)
                .AddColumn("value", valueName);
            
            listView.Bind(property.serializedObject);
            container.Add(listView);

            return container;
        }
    }
}