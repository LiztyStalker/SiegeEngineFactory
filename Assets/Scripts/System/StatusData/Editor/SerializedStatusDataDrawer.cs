#if UNITY_EDITOR
namespace SEF.Status.Editor
{
    using UnityEngine;
    using UnityEditor;

    [CustomPropertyDrawer(typeof(SerializedStatusData))]
    public class SerializedStatusDataDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 4f + 8f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            position.height /= 4f;

            EditorGUI.indentLevel++;

            var typeField = property.FindPropertyRelative("_classTypeName");
            EditorGUI.PropertyField(position, typeField, new GUIContent("상태타입"));

            position.y += position.height + 2f;

            var typeStatusDataField = property.FindPropertyRelative("_typeStatusData");
            EditorGUI.PropertyField(position, typeStatusDataField, new GUIContent("상태값타입"), true);

            position.y += position.height + 2f;

            var startValueField = property.FindPropertyRelative("_startValue");
            EditorGUI.PropertyField(position, startValueField, new GUIContent("초기값"), true);

            position.y += position.height + 2f;

            var increaseValueField = property.FindPropertyRelative("_increaseValue");

            EditorGUI.PropertyField(position, increaseValueField, new GUIContent("증감값"));

            EditorGUI.indentLevel--;



        }
    }
}
#endif