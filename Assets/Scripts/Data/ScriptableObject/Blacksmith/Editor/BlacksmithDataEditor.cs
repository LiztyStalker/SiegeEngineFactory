namespace SEF.Data.Editor
{
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;

    [CustomEditor(typeof(BlacksmithData))]
    public class BlacksmithDataEditor : Editor
    {
        private VisualElement _root;

        private bool _isModified = false;

        private ObjectField _iconField;
        private TextField _keyField;
        private PropertyField _statusField;
        private EnumField _typeField;
        private TextField _startUpgradeField;
        private IntegerField  _upgradeValueField;
        private FloatField _upgradeRateField;


        public void OnEnable()
        {
            _root = new VisualElement();

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Data/ScriptableObject/Blacksmith/Editor/BlacksmithDataEditor.uxml");
            visualTree.CloneTree(_root);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Data/ScriptableObject/Blacksmith/Editor/BlacksmithDataEditor.uss");
            _root.styleSheets.Add(styleSheet);

        }

        public override VisualElement CreateInspectorGUI()
        {
            _iconField = _root.Query<ObjectField>("blacksmith-icon-field").First();
            _iconField.label = "아이콘";
            _iconField.objectType = typeof(Sprite);
            _iconField.BindProperty(serializedObject.FindProperty("_icon"));

            _keyField = _root.Query<TextField>("blacksmith-key-field").First();
            _keyField.label = "키";
            _keyField.BindProperty(serializedObject.FindProperty("_key"));

            _statusField = _root.Query<PropertyField>("blacksmith-status-field").First();
            _statusField.BindProperty(serializedObject.FindProperty("_serializedStatusData"));

            _startUpgradeField = _root.Query<TextField>("blacksmith-start-upgrade-field").First();
            _startUpgradeField.label = "초기업글비용";
            _startUpgradeField.BindProperty(serializedObject.FindProperty("_startUpgradeValue"));

            _upgradeValueField = _root.Query<IntegerField>("blacksmith-upgrade-value-field").First();
            _upgradeValueField.label = "증가량";
            _upgradeValueField.BindProperty(serializedObject.FindProperty("_increaseUpgradeValue"));

            _upgradeRateField = _root.Query<FloatField>("blacksmith-rate-value-field").First();
            _upgradeRateField.label = "증가비";
            _upgradeRateField.BindProperty(serializedObject.FindProperty("_increaseUpgradeRate"));

            return _root;
        }



    }
}