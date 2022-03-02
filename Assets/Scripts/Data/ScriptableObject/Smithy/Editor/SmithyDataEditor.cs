#if UNITY_EDITOR
namespace SEF.Data.Editor
{
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;

    [CustomEditor(typeof(SmithyData))]
    public class SmithyDataEditor : Editor
    {
        private VisualElement _root;

        private bool _isModified = false;

        private ObjectField _iconField;
        private TextField _keyField;
        private PropertyField _statusDataField;
        private PropertyField _assetDataField;
        private FloatField _increaseValueField;
        private FloatField _increaseRateField;

        private VisualElement _multipleLayout;
        private Button _addButton;
        private Button _removeButton;


        public void OnEnable()
        {
            _root = new VisualElement();

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Data/ScriptableObject/Smithy/Editor/SmithyDataEditor.uxml");
            visualTree.CloneTree(_root);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Data/ScriptableObject/Smithy/Editor/SmithyDataEditor.uss");
            _root.styleSheets.Add(styleSheet);

        }

        public override VisualElement CreateInspectorGUI()
        {
            _iconField = _root.Query<ObjectField>("smithy-icon-field").First();
            _iconField.label = "아이콘";
            _iconField.objectType = typeof(Sprite);
            _iconField.BindProperty(serializedObject.FindProperty("_icon"));

            _keyField = _root.Query<TextField>("smithy-key-field").First();
            _keyField.label = "키";
            _keyField.BindProperty(serializedObject.FindProperty("_key"));

            _multipleLayout = _root.Query<VisualElement>("smithy-multiple-layout").First();


            //_addButton = _root.Query<Button>("smithy-multiple-add-button");
            //_removeButton = _root.Query<Button>("smithy-multiple-remove-button");

            //_addButton.RegisterCallback<ClickEvent>(AddData);
            //_removeButton.RegisterCallback<ClickEvent>(RemoveData);

            UpdateLayout(null);

            return _root;
        }


        private void AddData(ClickEvent e)
        {
            var property = serializedObject.FindProperty("_smithyAbilityDataArray");
            property.arraySize++;
            serializedObject.ApplyModifiedProperties();
            UpdateLayout(null);
        }

        private void RemoveData(ClickEvent e)
        {
            var property = serializedObject.FindProperty("_smithyAbilityDataArray");
            property.arraySize--;
            serializedObject.ApplyModifiedProperties();
            UpdateLayout(null);
        }

        private void UpdateLayout(ClickEvent e)
        {
            UpdateQuestConditionData(_multipleLayout, serializedObject.FindProperty("_smithyAbilityDataArray"));
        }

        private void UpdateQuestConditionData(VisualElement layout, SerializedProperty property)
        {
            layout.Clear();
            for (int i = 0; i < property.arraySize; i++)
            {
                layout.Add(new SmithyAbilityDataEditor(property.GetArrayElementAtIndex(i)));
            }
        }

        private void OnDisable()
        {
            //if (_multipleConditionLayout != null)
            //    _multipleConditionLayout.Clear();

            //if (_singleConditionLayout != null)
            //    _singleConditionLayout.Clear();

            //if (_multipleToggle != null)
            //    _multipleToggle.UnregisterCallback<ClickEvent>(UpdateLayout);
        }
    }
}
#endif