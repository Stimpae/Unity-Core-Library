using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Thundertale.Core.EventBus.Editor {
    /// <summary>
    /// Custom property drawer for the <see cref="SubclassSelectorAttribute"/>.
    /// </summary>
    [CustomPropertyDrawer(typeof(SubclassSelectorAttribute))]
    public class SubclassSelectorDrawer : PropertyDrawer {
        /// <summary>
        /// Creates the property GUI for the subclass selector.
        /// </summary>
        /// <param name="property">The serialized property.</param>
        /// <returns>A VisualElement representing the property GUI.</returns>
        public override VisualElement CreatePropertyGUI(SerializedProperty property) {
            // Create a container for the property
            var container = new VisualElement();

            // Create a default PropertyField for the serialized property
            var propertyField = new PropertyField(property);
            propertyField.Bind(property.serializedObject);
            container.Add(propertyField);

            // Create a DropdownField for selecting the subclass type
            var dropdownField = new DropdownField("Subclass Type");
            container.Add(dropdownField);
            
            // Populate the dropdown with subclass options
            Type baseType = fieldInfo.FieldType;
            var subclassTypes = GetTypes(baseType).ToList();
            dropdownField.choices = subclassTypes.Select((type, i) => type.Name).ToList();

            // Set initial value
            string currentTypeName = property.managedReferenceValue?.GetType().Name ?? "Not set";
            dropdownField.value = currentTypeName;

            // Handle selection change
            dropdownField.RegisterValueChangedCallback(evt => {
                string selectedTypeName = evt.newValue;
                var selectedType = subclassTypes.FirstOrDefault(t => t.Name == selectedTypeName);

                if (selectedType != null) {
                    //property.managedReferenceValue = selectedType.GetConstructor(Type.EmptyTypes)?.Invoke(null);
                    property.managedReferenceValue = System.Activator.CreateInstance(selectedType);
                    
                    property.serializedObject.ApplyModifiedProperties();
                }
            });

            return container;
        }

        /// <summary>
        /// Gets the types that are subclasses of the specified base type.
        /// </summary>
        /// <param name="baseType">The base type.</param>
        /// <returns>An enumerable of types that are subclasses of the base type.</returns>
        IEnumerable<Type> GetTypes(Type baseType) {
            return Assembly.GetAssembly(baseType).GetTypes()
                .Where(t => (t.IsClass || t.IsValueType) && !t.IsAbstract && baseType.IsAssignableFrom(t));
        }
    }
}