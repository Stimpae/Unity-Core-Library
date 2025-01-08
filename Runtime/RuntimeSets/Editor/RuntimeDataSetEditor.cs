using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Thundertale.Core.Editor;
using Thundertale.Core.RuntimeSet;


namespace Thundertale.Core.RuntimeSet.Editor {
    /// <summary>
    /// Custom editor for ScriptableObjects that displays a list of items in a multi-column list view.
    /// </summary>
    [CustomEditor(typeof(RuntimeSet<>), true)]
    public class RuntimeDataSetEditor : UnityEditor.Editor {
        private VisualElement m_root;
        private MultiColumnListView m_multiColumnListView;
        private SerializedProperty m_itemsProperty;
        private List<SerializedProperty> m_itemList;
        

        /// <summary>
        /// Creates the custom inspector GUI.
        /// </summary>
        /// <returns>The root VisualElement of the custom inspector GUI.</returns>
        public override VisualElement CreateInspectorGUI() {
            m_root = new VisualElement();
            
            m_itemsProperty = serializedObject.FindProperty("items");

            if (m_itemsProperty == null || !m_itemsProperty.isArray) {
                m_root.Add(new Label("The 'items' property could not be found or is not an array."));
                return m_root;
            }

            UpdateItemList();

            m_multiColumnListView = new MultiColumnListView {
                showAlternatingRowBackgrounds = AlternatingRowBackground.All,
                showBoundCollectionSize = false,
                virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                reorderMode = ListViewReorderMode.Animated,
                reorderable = true,
                showBorder = true,
                showAddRemoveFooter = false,
                fixedItemHeight = 25,
                itemsSource = m_itemList
            };

            m_multiColumnListView.columns.Add(new Column {
                title = "ID",
                makeCell = () => new Label(),
                bindCell = (cell, index) => {
                    var idProperty = m_itemList[index].FindPropertyRelative("id");
                    ((Label)cell).text = idProperty != null ? idProperty.stringValue : "N/A";
                },
                stretchable = true
            });

            m_multiColumnListView.columns.Add(new Column {
                title = "Item",
                makeCell = () => new ObjectField { objectType = typeof(Object), allowSceneObjects = true },
                bindCell = (cell, index) => {
                    var itemProperty = m_itemList[index].FindPropertyRelative("item");
                    var objectField = cell as ObjectField;

                    if (itemProperty != null) {
                        if (objectField != null) {
                            objectField.value = itemProperty.objectReferenceValue;
                            objectField.RegisterValueChangedCallback(evt => {
                                itemProperty.objectReferenceValue = evt.newValue;
                                serializedObject.ApplyModifiedProperties();
                            });
                        }
                    }
                },
                stretchable = true
            });

            m_root.Add(m_multiColumnListView);
            EditorApplication.update += RefreshOnChange;

            return m_root;
        }

        /// <summary>
        /// Refreshes the list view when the serialized object has modified properties.
        /// </summary>
        private void RefreshOnChange() {
            if (serializedObject.hasModifiedProperties || serializedObject.UpdateIfRequiredOrScript()) {
                UpdateItemList();
                m_multiColumnListView.itemsSource = m_itemList;
                m_multiColumnListView.Rebuild();
            }
        }

        /// <summary>
        /// Updates the item list from the serialized property.
        /// </summary>
        private void UpdateItemList() {
            m_itemList = new List<SerializedProperty>();
            for (int i = 0; i < m_itemsProperty.arraySize; i++) {
                m_itemList.Add(m_itemsProperty.GetArrayElementAtIndex(i));
            }
        }

        /// <summary>
        /// Unregisters the refresh callback when the editor is disabled.
        /// </summary>
        private void OnDisable() {
            EditorApplication.update -= RefreshOnChange;
        }
    }
}