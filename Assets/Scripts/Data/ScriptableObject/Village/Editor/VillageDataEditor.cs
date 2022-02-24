namespace SEF.Data.Editor
{
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;

    [CustomEditor(typeof(VillageData))]
    public class VillageDataEditor : Editor
    {
        private VisualElement _root;

        private bool _isModified = false;

        private ObjectField _iconField;
        private TextField _keyField;

        private PropertyField _processDataField;
        private PropertyField _assetDataField;
        private FloatField _increaseValueField;
        private FloatField _increaseRateField;

        public void OnEnable()
        {
            _root = new VisualElement();

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Data/ScriptableObject/Village/Editor/VillageDataEditor.uxml");
            visualTree.CloneTree(_root);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Data/ScriptableObject/Village/Editor/VillageDataEditor.uss");
            _root.styleSheets.Add(styleSheet);

        }

        public override VisualElement CreateInspectorGUI()
        {
            _iconField = _root.Query<ObjectField>("village-icon-field").First();
            _iconField.label = "아이콘";
            _iconField.objectType = typeof(Sprite);
            _iconField.BindProperty(serializedObject.FindProperty("_icon"));

            _keyField = _root.Query<TextField>("village-key-field").First();
            _keyField.label = "키";
            _keyField.BindProperty(serializedObject.FindProperty("_key"));

            _processDataField = _root.Query<PropertyField>("village-process-data-field").First();
            _processDataField.BindProperty(serializedObject.FindProperty("_serializedProcessData"));

            _assetDataField = _root.Query<PropertyField>("village-asset-data-field").First();
            _assetDataField.BindProperty(serializedObject.FindProperty("_serializedStartUpgradeAssetData"));

            _increaseValueField = _root.Query<FloatField>("village-increase-upgrade-value-field").First();
            _increaseValueField.label = "업글증가량";
            _increaseValueField.BindProperty(serializedObject.FindProperty("_increaseUpgradeValue"));

            _increaseRateField = _root.Query<FloatField>("village-increase-upgrade-rate-field").First();
            _increaseRateField.label = "업글증가비";
            _increaseRateField.BindProperty(serializedObject.FindProperty("_increaseUpgradeRate"));

            
            return _root;
        }



    }
}