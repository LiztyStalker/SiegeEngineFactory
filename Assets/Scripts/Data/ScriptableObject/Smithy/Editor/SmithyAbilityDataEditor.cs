namespace SEF.Data.Editor
{
    using UnityEditor;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;

    public class SmithyAbilityDataEditor : VisualElement
    {
        public SmithyAbilityDataEditor(SerializedProperty property)
        {
            UnityEngine.Debug.Log("Editor");

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Data/ScriptableObject/Smithy/Editor/SmithyAbilityDataEditor.uxml");
            visualTree.CloneTree(this);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Data/ScriptableObject/Smithy/Editor/SmithyAbilityDataEditor.uss");
            this.styleSheets.Add(styleSheet);


            var serializedStatusDataField = this.Query<PropertyField>("smithy-ability-type-status-data-field").First();
            serializedStatusDataField.BindProperty(property.FindPropertyRelative("_serializedStatusData"));


            var defaultMaxUpgradeValueField = this.Query<PropertyField>("smithy-ability-default-max-upgrade-field").First();
            defaultMaxUpgradeValueField.BindProperty(property.FindPropertyRelative("_defaultMaxUpgradeValue"));


            var serializedAssetDataField = this.Query<PropertyField>("smithy-ability-type-upgrade-asset-field").First();
            serializedAssetDataField.BindProperty(property.FindPropertyRelative("_serializedAssetData"));


            var increaseUpgradeValueField = this.Query<PropertyField>("smithy-ability-increase-upgrade-value-field").First();
            increaseUpgradeValueField.BindProperty(property.FindPropertyRelative("_increaseUpgradeValue"));


            var increaseUpgradeRateField = this.Query<PropertyField>("smithy-ability-increase-upgrade-rate-field").First();
            increaseUpgradeRateField.BindProperty(property.FindPropertyRelative("_increaseUpgradeRate"));

            var serializedTechAssetDataField = this.Query<PropertyField>("smithy-ability-type-tech-asset-field").First();
            serializedTechAssetDataField.BindProperty(property.FindPropertyRelative("_serializedTechAssetData"));
        }
    }
}