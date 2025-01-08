using Thundertale.Core.Editor;
using UnityEditor;
using UnityEngine.UIElements;

namespace Thundertale.Core.ObjectPooler.Editor {
    [CustomEditor(typeof(ObjectPooler),true)]
    public class ObjectPoolerEditor : UnityEditor.Editor {
        private VisualElement m_root;
        /// <summary>
        /// Creates the custom inspector GUI.
        /// </summary>
        /// <returns>The root VisualElement of the custom inspector GUI.</returns>
        public override VisualElement CreateInspectorGUI() {
            m_root = new VisualElement();
            var multiColumnListView = EditorStyleUtils.CreateMultiListView("itemsToPool", "Items To Pool")
                .AddColumn("prefab", "Prefab")
                .AddColumn("amount", "Amount");
            
            m_root.Add(multiColumnListView);
            return m_root;
        }
    }
}