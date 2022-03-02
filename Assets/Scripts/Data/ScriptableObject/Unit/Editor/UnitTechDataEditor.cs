#if UNITY_EDITOR
namespace SEF.Data.Editor
{
    using UnityEditor;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;

    public class UnitTechDataEditor : VisualElement
    {
        public UnitTechDataEditor(SerializedProperty property)
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Data/ScriptableObject/Unit/Editor/UnitTechDataEditor.uxml");
            visualTree.CloneTree(this);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Data/ScriptableObject/Unit/Editor/UnitTechDataEditor.uss");
            this.styleSheets.Add(styleSheet);


            var typeTechUnitField = this.Query<PropertyField>("unit-tech-type-field").First();
            typeTechUnitField.label = "��ũŸ��";
            typeTechUnitField.BindProperty(property.FindPropertyRelative("_typeTechTree"));


            var techUnitKeyField = this.Query<PropertyField>("unit-tech-key-field").First();
            techUnitKeyField.label = "��ũ����Ű";
            techUnitKeyField.BindProperty(property.FindPropertyRelative("_techUnitKey"));


            var techAssetField = this.Query<PropertyField>("unit-tech-asset-field").First();
            techAssetField.BindProperty(property.FindPropertyRelative("_serializedTechAssetData"));
        }
    }
}
#endif