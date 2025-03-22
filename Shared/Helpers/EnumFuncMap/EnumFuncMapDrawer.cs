/*------------------------------------------------------------------------------
File:       EnumFuncMapDrawer.cs 
Project:    AlchemicalFlux Utilities
Overview:   Custom drawer for classes that inherit the UnityEnumFuncMapBase
            class.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-22 15:52:42 
------------------------------------------------------------------------------*/
using UnityEditor;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Custom property drawer for classes that inherit the UnityEnumFuncMapBase
    /// class.
    /// </summary>
    [CustomPropertyDrawer(typeof(IUnityEnumFuncMapDrawerBase), true)]
    public class EnumFuncMapDrawer : PropertyDrawer
    {
        #region Fields

        private SerializedProperty _valueProperty;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Draws the property GUI.
        /// </summary>
        /// <param name="position">
        /// Rectangle on the screen to use for the property GUI.
        /// </param>
        /// <param name="property">
        /// The SerializedProperty to make the custom GUI for.
        /// </param>
        /// <param name="label">The label of this property.</param>
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var valueProperty = GetValueProperty(property);
            if(valueProperty != null)
            {
                EditorGUI.PropertyField(position, valueProperty, label, true);
            }
            else
            {
                EditorGUI.LabelField(position, label, "Unsupported Type");
            }

            EditorGUI.EndProperty();
        }

        /// <summary>
        /// Gets the height of the property.
        /// </summary>
        /// <param name="property">
        /// The SerializedProperty to get the height of.
        /// </param>
        /// <param name="label">The label of this property.</param>
        /// <returns>The height of the property.</returns>
        public override float GetPropertyHeight(SerializedProperty property,
            GUIContent label)
        {
            var valueProperty = GetValueProperty(property);
            if(valueProperty != null)
            {
                return EditorGUI.GetPropertyHeight(valueProperty, label, true);
            }
            return base.GetPropertyHeight(property, label);
        }

        /// <summary>
        /// Gets the cached SerializedProperty for the current enum value.
        /// </summary>
        private SerializedProperty GetValueProperty(SerializedProperty property)
        {
            if(_valueProperty == null || 
                _valueProperty.serializedObject != property.serializedObject)
            {
                _valueProperty = property.FindPropertyRelative("_curEnum");
            }
            return _valueProperty;
        }

        #endregion Methods
    }
}
