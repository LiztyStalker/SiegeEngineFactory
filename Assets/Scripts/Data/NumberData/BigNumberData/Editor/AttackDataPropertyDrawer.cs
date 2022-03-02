#if UNITY_EDITOR
namespace SEF.Data.Editor
{
    using UnityEngine;
    using UnityEditor;

    [CustomPropertyDrawer(typeof(DamageData))]
    [CanEditMultipleObjects]
    public class AttackDataPropertyDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("_valueText"), label);
        }
    }
}
#endif