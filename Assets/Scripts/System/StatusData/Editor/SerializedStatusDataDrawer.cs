#if UNITY_EDITOR
namespace SEF.Status.Editor
{
    using UnityEngine;
    using UnityEditor;

    [CustomPropertyDrawer(typeof(SerializedStatusData))]
    public class SerializedStatusDataDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var field = property.FindPropertyRelative("_classTypeName");
            EditorGUI.PropertyField(position, field, new GUIContent("е╦ют"));
        }
    }
}
#endif