/*------------------------------------------------------------------------------
File:       EnumFuncMapDrawer.cs 
Project:    AlchemicalFlux Utilities
Overview:   Custom drawer for classes that inherit the UnityEnumFuncMapBase
            class.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-20 14:48:23 
------------------------------------------------------------------------------*/
using UnityEditor;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    [CustomPropertyDrawer(typeof(UnityEnumFuncMapDrawerBase), true)]
    public class EnumFuncMapDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, 
            GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var valueProperty = property.FindPropertyRelative("_curEnum");
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

        public override float GetPropertyHeight(SerializedProperty property, 
            GUIContent label)
        {
            var valueProperty = property.FindPropertyRelative("_curEnum");
            if(valueProperty != null)
            {
                return EditorGUI.GetPropertyHeight(valueProperty, label, true);
            }
            return base.GetPropertyHeight(property, label);
        }
    }
}