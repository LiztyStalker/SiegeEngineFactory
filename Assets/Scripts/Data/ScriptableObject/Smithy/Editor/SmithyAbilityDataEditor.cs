namespace SEF.Data.Editor
{
    using UnityEditor;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;

    public class SmithyAbilityDataEditor : VisualElement
    {
        public SmithyAbilityDataEditor(SerializedProperty property)
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Data/ScriptableObject/Smithy/Editor/SmithyAbilityDataEditor.uxml");
            visualTree.CloneTree(this);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Data/ScriptableObject/Smithy/Editor/SmithyAbilityDataEditor.uss");
            this.styleSheets.Add(styleSheet);


            var serializedStatusDataField = this.Query<PropertyField>("smithy-ability-type-status-data-field").First();
            serializedStatusDataField.BindProperty(property.FindPropertyRelative("_serializedStatusData"));


            var defaultMaxUpgradeValueField = this.Query<PropertyField>("smithy-ability-default-max-upgrade-field").First();
            defaultMaxUpgradeValueField.label = "최대업그레이드";
            defaultMaxUpgradeValueField.BindProperty(property.FindPropertyRelative("_defaultMaxUpgradeValue"));


            var serializedAssetDataField = this.Query<PropertyField>("smithy-ability-type-upgrade-asset-field").First();
            serializedAssetDataField.BindProperty(property.FindPropertyRelative("_serializedAssetData"));


            var increaseUpgradeValueField = this.Query<PropertyField>("smithy-ability-increase-upgrade-value-field").First();
            increaseUpgradeValueField.label = "업글재화증가값";
            increaseUpgradeValueField.BindProperty(property.FindPropertyRelative("_increaseUpgradeValue"));


            var increaseUpgradeRateField = this.Query<PropertyField>("smithy-ability-increase-upgrade-rate-field").First();
            increaseUpgradeRateField.label = "업글재화증가율";
            increaseUpgradeRateField.BindProperty(property.FindPropertyRelative("_increaseUpgradeRate"));

            var serializedTechAssetDataField = this.Query<PropertyField>("smithy-ability-type-tech-asset-field").First();
            serializedTechAssetDataField.label = "테크재화";
            serializedTechAssetDataField.BindProperty(property.FindPropertyRelative("_serializedTechAssetData"));
        }
    }
}