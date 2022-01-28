namespace SEF.Data.Editor
{
    using UnityEditor;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(QuestConditionData))]
    public class QuestConditionDataEditor : PropertyDrawer
    {

        private VisualElement _element;

        private PropertyField _conditionField;
        private IntegerField _conditionValueField;
        private PropertyField _rewardAssetDataField;
        private Button _removeButton;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Data/ScriptableObject/Quest/Editor/QuestConditionDataEditor.uxml");
            _element = visualTree.CloneTree(property.propertyPath);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Data/ScriptableObject/Quest/Editor/QuestConditionDataEditor.uss");
            container.styleSheets.Add(styleSheet);

            _conditionField = _element.Query<PropertyField>("quest-condition-data-field").First();
            _conditionField.BindProperty(property.FindPropertyRelative("_serializedConditionQuestData"));

            _conditionValueField = _element.Query<IntegerField>("quest-condition-value-field").First();
            _conditionValueField.BindProperty(property.FindPropertyRelative("_conditionValue"));
            _conditionValueField.label = "Á¶°Ç°ª";

            _rewardAssetDataField = _element.Query<PropertyField>("quest-reward-assetdata-field").First();
            _rewardAssetDataField.BindProperty(property.FindPropertyRelative("_serializedAssetData"));

            //_removeButton = _element.Query<Button>("quest-remove-button").First();
            //_removeButton.RegisterCallback<ClickEvent>();

            container.Add(_element);

            return container;
        }


    }
}