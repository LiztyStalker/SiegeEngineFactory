#if UNITY_EDITOR
namespace SEF.Data.Editor
{
    using UnityEngine;
    using UnityEditor;

    [CustomPropertyDrawer(typeof(SerializedConditionQuestData))]
    public class SerializedConditionQuestDataDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var field = property.FindPropertyRelative("_classTypeName");
            EditorGUI.PropertyField(position, field, new GUIContent("Á¶°Ç"));
        }
    }
}
#endif