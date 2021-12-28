namespace SEF.Data.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using UnityEditor.UIElements;
    using UnityEngine.UIElements;

    [CustomPropertyDrawer(typeof(HealthData))]
    [CanEditMultipleObjects]
    public class HealthDataPropertyDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("_valueText"), label);
        }

        //public override VisualElement CreatePropertyGUI(SerializedProperty property)
        //{
        //    var container = new VisualElement();
        //    var value = new PropertyField(property.FindPropertyRelative("_numberText"));
        //    container.Add(value);
        //    return container;

        //}
    }
}