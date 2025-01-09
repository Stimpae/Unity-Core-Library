using Pastime.Core.Editor;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Toggle = UnityEngine.UI.Toggle;

namespace Pastime.Core.ObjectPooler.Editor {
    [CustomEditor(typeof(ObjectPooler),true)]
    public class ObjectPoolerEditor : UnityEditor.Editor {
        private VisualElement m_root;
        /// <summary>
        /// Creates the custom inspector GUI.
        /// </summary>
        /// <returns>The root VisualElement of the custom inspector GUI.</returns>
        public override VisualElement CreateInspectorGUI() {
            m_root = new VisualElement();
            
            var poolOnAwake = new PropertyField(serializedObject.FindProperty("poolOnAwake"));
            poolOnAwake.style.marginTop = 5;
            
            var multiColumnListView = EditorStyleUtils.CreateMultiListView("itemsToPool", "Items To Pool")
                .AddColumn("prefab", "Prefab")
                .AddColumn("amount", "Amount");
            
            m_root.Add(poolOnAwake);
            m_root.Add(EditorStyleUtils.CreateSplitter(3,20));
            m_root.Add(multiColumnListView);
            return m_root;
        }
    }
}