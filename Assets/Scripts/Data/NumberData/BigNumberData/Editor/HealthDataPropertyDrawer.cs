#if UNITY_EDITOR
namespace SEF.Data.Editor
{
    using UnityEngine;
    using UnityEditor;

    [CustomPropertyDrawer(typeof(HealthData))]
    [CanEditMultipleObjects]
    public class HealthDataPropertyDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("_valueText"), label);
        }
    }
}
#endif