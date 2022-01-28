namespace SEF.Data.Editor
{
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;

    [CustomEditor(typeof(QuestData))]
    public class QuestDataEditor : Editor
    {
        private VisualElement _root;

        private bool _isModified = false;

        private ObjectField _iconField;
        private TextField _keyField;
        private EnumField _groupField;
        private Toggle _multipleToggle;
        private VisualElement _singleConditionLayout;
        private VisualElement _multipleConditionLayout;
        private Button _addButton;
        private Button _removeButton;


        public void OnEnable()
        {
            _root = new VisualElement();

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Data/ScriptableObject/Quest/Editor/QuestDataEditor.uxml");
            visualTree.CloneTree(_root);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Data/ScriptableObject/Quest/Editor/QuestDataEditor.uss");
            _root.styleSheets.Add(styleSheet);

        }

        public override VisualElement CreateInspectorGUI()
        {
            _iconField = _root.Query<ObjectField>("quest-icon-field").First();
            _iconField.label = "아이콘";
            _iconField.objectType = typeof(Sprite);
            _iconField.BindProperty(serializedObject.FindProperty("_icon"));

            _keyField = _root.Query<TextField>("quest-key-field");
            _keyField.label = "최대업글량";
            _keyField.BindProperty(serializedObject.FindProperty("_key"));


            _groupField = _root.Query<EnumField>("quest-type-group-field");
            _groupField.label = "그룹";
            _groupField.BindProperty(serializedObject.FindProperty("_typeQuestGroup"));

            _multipleToggle = _root.Query<Toggle>("quest-multiple-toggle");
            _multipleToggle.label = "연계퀘스트";
            _multipleToggle.BindProperty(serializedObject.FindProperty("_isMultipleQuest"));
            _multipleToggle.RegisterCallback<ClickEvent>(UpdateLayout);

            _multipleConditionLayout = _root.Query<VisualElement>("quest-multiple-condition-layout");
            _singleConditionLayout = _root.Query<VisualElement>("quest-single-condition-layout");

            _addButton = _root.Query<Button>("quest-multiple-add-button");
            _removeButton = _root.Query<Button>("quest-multiple-remove-button");

            //_addButton.RegisterCallback<ClickEvent>();
            //_removeButton.RegisterCallback<ClickEvent>();


            UpdateQuestConditionData(_multipleConditionLayout, serializedObject.FindProperty("_questConditionDataArray"));
            var property = serializedObject.FindProperty("_questConditionData");
            UpdateQuestConditionData(_singleConditionLayout, property);

            UpdateLayout(null);

            return _root;
        }

        private void UpdateLayout(ClickEvent e)
        {
            _multipleConditionLayout.style.display = DisplayStyle.None;
            _singleConditionLayout.style.display = DisplayStyle.None;

            if (_multipleToggle.value)
                _multipleConditionLayout.style.display = DisplayStyle.Flex;
            else
                _singleConditionLayout.style.display = DisplayStyle.Flex;
        }

        private void UpdateQuestConditionData(VisualElement layout, SerializedProperty conditionProperty)
        {
            if (conditionProperty.isArray)
            {
                for (int i = 0; i < conditionProperty.arraySize; i++)
                {
                    layout.Add(new PropertyField(conditionProperty.GetArrayElementAtIndex(i)));
                }
            }
            else
            {
                var property = new PropertyField(conditionProperty);
                layout.Add(property);                
            }
        }

        private void OnDisable()
        {
            _multipleConditionLayout.Clear();
            _singleConditionLayout.Clear();
            _multipleToggle.UnregisterCallback<ClickEvent>(UpdateLayout);
        }
    }
}