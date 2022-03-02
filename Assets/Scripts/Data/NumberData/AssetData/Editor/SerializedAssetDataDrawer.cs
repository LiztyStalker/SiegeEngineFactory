#if UNITY_EDITOR
namespace SEF.Data.Editor
{
    using UnityEngine;
    using UnityEditor;

    [CustomPropertyDrawer(typeof(SerializedAssetData))]
    public class SerializedAssetDataDrawer : PropertyDrawer
    {

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2f + 2f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height /= 2f;

            EditorGUI.indentLevel++;

            var assetData = property.FindPropertyRelative("_assetData");
            EditorGUI.PropertyField(position, assetData, new GUIContent("재화"), true);

            position.y += position.height + 2f;

            var assetValue = property.FindPropertyRelative("_assetValue");
            EditorGUI.PropertyField(position, assetValue, new GUIContent("재화값"));
            EditorGUI.indentLevel--;
        }
    }
}
#endif