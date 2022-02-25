namespace SEF.Data.Editor
{
    using UnityEditor;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;

    public class VillageAbilityDataEditor : VisualElement
    {
        public VillageAbilityDataEditor(SerializedProperty property)
        {
            UnityEngine.Debug.Log("Editor");

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Data/ScriptableObject/Village/Editor/VillageAbilityDataEditor.uxml");
            visualTree.CloneTree(this);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Data/ScriptableObject/Village/Editor/VillageAbilityDataEditor.uss");
            this.styleSheets.Add(styleSheet);


            var serializedStatusDataField = this.Query<PropertyField>("village-ability-type-status-data-field").First();
            serializedStatusDataField.BindProperty(property.FindPropertyRelative("_serializedStatusData"));


            var defaultMaxUpgradeValueField = this.Query<PropertyField>("village-ability-default-max-upgrade-field").First();
            defaultMaxUpgradeValueField.label = "�ִ���׷��̵�";
            defaultMaxUpgradeValueField.BindProperty(property.FindPropertyRelative("_defaultMaxUpgradeValue"));


            var serializedAssetDataField = this.Query<PropertyField>("village-ability-type-upgrade-asset-field").First();
            serializedAssetDataField.BindProperty(property.FindPropertyRelative("_serializedAssetData"));


            var increaseUpgradeValueField = this.Query<PropertyField>("village-ability-increase-upgrade-value-field").First();
            increaseUpgradeValueField.label = "������ȭ������";
            increaseUpgradeValueField.BindProperty(property.FindPropertyRelative("_increaseUpgradeValue"));


            var increaseUpgradeRateField = this.Query<PropertyField>("village-ability-increase-upgrade-rate-field").First();
            increaseUpgradeRateField.label = "������ȭ������";
            increaseUpgradeRateField.BindProperty(property.FindPropertyRelative("_increaseUpgradeRate"));

            var serializedTechAssetDataField = this.Query<PropertyField>("village-ability-type-tech-asset-field").First();
            serializedTechAssetDataField.label = "��ũ��ȭ";
            serializedTechAssetDataField.BindProperty(property.FindPropertyRelative("_serializedTechAssetData"));
        }
    }
}