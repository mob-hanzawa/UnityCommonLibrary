﻿using UnityCommonLibrary.Attributes;
using UnityEditor;
using UnityEngine;

namespace UnityCommonEditorLibrary.Inspectors
{
    /// <summary>
    /// The custom drawer for the RelativesOnlyAttribute.
    /// Also enforces rules set by the attribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(RelativesOnlyAttribute))]
    public class RelativesOnlyAttributeDrawer : PropertyDrawer
    {
        /// <summary>
        /// The error message shown when the rules set are violated.
        /// </summary>
        private const string ERROR_MSG = "This object field only allows assignments from the following relatives:\n\n{0}\n\nThe current value will be unassigned.";

        /// <summary>
        /// Our strongly typed attribute.
        /// </summary>
        private RelativesOnlyAttribute target;

        private void OnEnable()
        {
            target = attribute as RelativesOnlyAttribute;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Draw the normal property
            EditorGUI.PropertyField(position, property, label);

            // Is null if no value assigned (or not a Component)
            var component = property.objectReferenceValue as Component;
            if (component != null)
            {
                var isValid = ProcessRules(property, component.gameObject);
                if (!isValid)
                {
                    // Show error message and unassign value
                    EditorUtility.DisplayDialog("Invalid Reference", string.Format(ERROR_MSG, target.validRelatives), "OK");
                    property.objectReferenceValue = null;
                }
            }
        }

        /// <summary>
        /// Ensures that the rules set by the attribute are being followed.
        /// </summary>
        /// <param name="property">The inspected field.</param>
        /// <param name="obj">The GameObject that the assigned value exists on.</param>
        /// <returns>True if the rules are being followed, false otherwise.</returns>
        private bool ProcessRules(SerializedProperty property, GameObject obj)
        {
            var propertyGameObject = (property.serializedObject.targetObject as Component).gameObject;

            // First check hard-set rules
            // Children and Parent rules use transform.IsChildOf(Transform), which
            // returns true if it is on the same GameObject also
            // so we need to check for both
            if (target.IsOnlyRuleSet(ValidRelatives.SameGameObject))
            {
                return obj == propertyGameObject;
            }
            if (target.IsOnlyRuleSet(ValidRelatives.Children))
            {
                return obj.transform.IsChildOf(propertyGameObject.transform) && obj != propertyGameObject;
            }
            if (target.IsOnlyRuleSet(ValidRelatives.Parents))
            {
                return propertyGameObject.transform.IsChildOf(obj.transform) && obj != propertyGameObject;
            }

            // Then check multi-applicable rules
            if (target.IsRuleSet(ValidRelatives.SameGameObject) && obj == propertyGameObject)
            {
                return true;
            }
            if (target.IsRuleSet(ValidRelatives.Children) && obj.transform.IsChildOf(propertyGameObject.transform) && obj != propertyGameObject)
            {
                return true;
            }
            if (target.IsRuleSet(ValidRelatives.Parents) && propertyGameObject.transform.IsChildOf(obj.transform) && obj != propertyGameObject)
            {
                return true;
            }
            return false;
        }
    }
}