namespace SEF.Data.Editor
{
    using UnityEngine;
    using UnityEditor;
    using UnityEditor.UIElements;

    [CustomPropertyDrawer(typeof(SerializedConditionQuestData))]
    public class SerializedConditionQuestDataDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var field = property.FindPropertyRelative("_className");
            EditorGUI.PropertyField(position, field, new GUIContent("Á¶°Ç"));
        }
    }
}