#if UNITY_EDITOR
namespace SEF.Data.Editor
{
    using UnityEditor;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;

    public class QuestConditionDataEditor : VisualElement
    {
        public QuestConditionDataEditor(SerializedProperty property)
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Data/ScriptableObject/Quest/Editor/QuestConditionDataEditor.uxml");
            visualTree.CloneTree(this);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Data/ScriptableObject/Quest/Editor/QuestConditionDataEditor.uss");
            this.styleSheets.Add(styleSheet);

            PropertyField conditionField = this.Query<PropertyField>("quest-condition-data-field").First();
            conditionField.BindProperty(property.FindPropertyRelative("_serializedConditionQuestData"));

            PropertyField conditionValueField = this.Query<PropertyField>("quest-condition-value-field").First();
            conditionValueField.BindProperty(property.FindPropertyRelative("_conditionValue"));
            conditionValueField.label = "Á¶°Ç°ª";

            PropertyField rewardAssetDataField = this.Query<PropertyField>("quest-reward-assetdata-field").First();
            rewardAssetDataField.BindProperty(property.FindPropertyRelative("_serializedAssetData"));
        }
    }
}
#endif