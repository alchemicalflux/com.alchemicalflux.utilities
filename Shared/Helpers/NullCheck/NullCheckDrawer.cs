/*------------------------------------------------------------------------------
File:       NullCheckDrawer.cs 
Project:    AlchemicalFlux Utilities
Overview:   Property drawer that manages the display for fields marked with the 
            NullCheck attribute.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-05 17:05:53 
------------------------------------------------------------------------------*/
#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{

    /// <summary>
    /// Custom property drawer for the NullCheck attribute, ensuring object 
    /// reference fields are 
    ///   not null.
    /// </summary>
    [CustomPropertyDrawer(typeof(NullCheck))]
    public class NullCheckDrawer : PropertyDrawer
    {
        #region Members

        /// <summary>
        /// Warning message displayed by non-object reference fields marked with
        /// NullCheck.
        /// </summary>
        private const string _warningMessage =
            nameof(NullCheck) + " only valid on ObjectReference fields.";

        /// <summary>
        /// Error message displayed when object reference field is null.
        /// </summary>
        private const string _errorMessage =
            "Missing object reference for " + nameof(NullCheck) + " property.";

        /// <summary>
        /// Height for the alert window to be displayed under the field.
        /// </summary>
        private const int _alertHeight = 30;

        /// <summary>
        /// Color applied to the field when it triggers an alert.
        /// </summary>
        private static readonly Color _alertColor = new(1, 0.4f, 0.2f);

        #endregion Members

        #region Methods

        /// <summary>
        /// Determines the final height of the field based on if an alert 
        /// message will be displayed.
        /// </summary>
        /// <param name="property">Serialized property.</param>
        /// <param name="label">Label for the property.</param>
        /// <returns>Height of the final property display.</returns>
        public override float GetPropertyHeight(SerializedProperty property, 
            GUIContent label)
        {
            var calculatedHeight = base.GetPropertyHeight(property, label); ;
            if(IsNotObjectRef(property) || ObjectRefNotSet(property))
            {
                calculatedHeight += _alertHeight;
            }
            return calculatedHeight;
        }

        /// <summary>
        /// Renders the property field, displaying alert messages if necessary.
        /// </summary>
        /// <param name="position">Position of the property field.</param>
        /// <param name="property">Serialized property.</param>
        /// <param name="label">Label for the property.</param>
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var objectReferenceHeight = base.GetPropertyHeight(property, label);
            position.height = objectReferenceHeight;
            BuildField(position, property, label);

            var warningRect = 
                new Rect(position.x, position.y + objectReferenceHeight,
                position.width, _alertHeight);
            BuildMessageBox(warningRect, property);

            EditorGUI.EndProperty();
        }

        /// <summary>
        /// Builds the property field, handling null references and disabled
        /// state for prefabs.
        /// </summary>
        /// <param name="drawArea">Area to draw the field.</param>
        /// <param name="property">Serialized property.</param>
        /// <param name="label">Label for the property.</param>
        private void BuildField(Rect drawArea, SerializedProperty property,
            GUIContent label)
        {
            var prevColor = GUI.color; // Maintain previous GUI color.

            // Non-object fields should be rendered normally.
            if(IsNotObjectRef(property)) 
            {
                EditorGUI.PropertyField(drawArea, property, label);
                return;
            }

            if(ObjectRefNotSet(property)) // Make null references easy to see.
            {
                GUI.color = _alertColor;
                label.text = $"!!! {label.text}";
            }

            if(IsPropertyNotNullInSceneAndPrefab(property))
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.ObjectField(drawArea, property, label);
                EditorGUI.EndDisabledGroup();
            }
            else
            {
                EditorGUI.ObjectField(drawArea, property, label);
            }

            GUI.color = prevColor; // Reset the GUI color.
        }

        /// <summary>
        /// Builds the alert message box based on the property's state.
        /// </summary>
        /// <param name="drawArea">Area to draw the message box.</param>
        /// <param name="property">Serialized property.</param>
        private void BuildMessageBox(Rect drawArea, SerializedProperty property)
        {
            if(IsNotObjectRef(property))
            {
                EditorGUI.HelpBox(drawArea, _warningMessage, 
                    MessageType.Warning);
            }
            else if(ObjectRefNotSet(property))
            {
                EditorGUI.HelpBox(drawArea, _errorMessage, MessageType.Error);
            }
        }

        /// <summary>
        /// Checks if the property is an object reference field  and is null.
        /// </summary>
        /// <param name="property">Serialized property.</param>
        /// <returns>
        /// True if the object reference is null, false otherwise.
        /// </returns>
        private bool ObjectRefNotSet(SerializedProperty property)
        {
            return !IsPropertyNotNullInSceneAndPrefab(property) &&
                property.objectReferenceValue == null;
        }

        /// <summary>
        /// Checks if the property is not null in both scene and prefab 
        /// instances.
        /// </summary>
        /// <param name="property">Serialized property.</param>
        /// <returns>
        /// True if property is not null in scene and prefab instances, false
        /// otherwise.
        /// </returns>
        private bool IsPropertyNotNullInSceneAndPrefab(
            SerializedProperty property)
        {
            return IsPropertyOnPrefab(property) && 
                ((NullCheck)attribute).IgnorePrefab;
        }

        /// <summary>
        /// Checks if the property belongs to a prefab instance.
        /// </summary>
        /// <param name="property">Serialized property.</param>
        /// <returns>
        /// True if the property belongs to a prefab instance, false otherwise.
        /// </returns>
        private static bool IsPropertyOnPrefab(SerializedProperty property)
        {
            return EditorUtility.IsPersistent(
                property.serializedObject.targetObject);
        }

        /// <summary>
        /// Checks if the property is not an object reference field.
        /// </summary>
        /// <param name="property">Serialized property.</param>
        /// <returns>
        /// True if the property is not an object reference field, false
        /// otherwise.
        /// </returns>
        private static bool IsNotObjectRef(SerializedProperty property)
        {
            return property.propertyType !=
                SerializedPropertyType.ObjectReference;
        }

        #endregion Methods
    }

}

#endif