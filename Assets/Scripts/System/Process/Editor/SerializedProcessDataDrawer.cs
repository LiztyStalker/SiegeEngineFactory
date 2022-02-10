#if UNITY_EDITOR
namespace SEF.Process.Editor
{
    using UnityEngine;
    using UnityEditor;

    [CustomPropertyDrawer(typeof(SerializedProcessData))]
    public class SerializedProcessDataDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 6f + 10f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            position.height /= 6f;

            EditorGUI.indentLevel++;

            var typeField = property.FindPropertyRelative("_classTypeName");
            GUI.enabled = false;
            typeField.stringValue = typeof(AssetProcessData).FullName;
            EditorGUI.PropertyField(position, typeField, new GUIContent("���μ���Ÿ��"));
            GUI.enabled = true;

            position.y += position.height + 2f;
            position.height *= 2f;

            var assetDataField = property.FindPropertyRelative("_processAssetData");
            EditorGUI.PropertyField(position, assetDataField, new GUIContent("���μ�������"), true);

            position.height /= 2f;
            position.y += position.height * 2f + 2f;

            var increaseValueField = property.FindPropertyRelative("_increaseValue");
            EditorGUI.PropertyField(position, increaseValueField, new GUIContent("����������"), true);

            position.y += position.height + 2f;

            var increaseRateField = property.FindPropertyRelative("_increaseRate");
            EditorGUI.PropertyField(position, increaseRateField, new GUIContent("����������"), true);

            position.y += position.height + 2f;

            var processTimeField = property.FindPropertyRelative("_processTime");

            EditorGUI.PropertyField(position, processTimeField, new GUIContent("����ð�"));

            EditorGUI.indentLevel--;



        }
    }
}
#endif