namespace SEF.UI.Toolkit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Data;

    public class UIAssetBlock // VisualElement로 교체 예정
    {
        private VisualElement _root;

        private TYPE_ASSET _typeAsset;
        public TYPE_ASSET TypeAsset => _typeAsset;

        private VisualElement _icon;

        private Label _nowValueLabel;

        private Label _maxValueLabel;

        public static UIAssetBlock Create()
        {
            return new UIAssetBlock();
        }

        public void Initialize(VisualElement root)
        {
            _root = root;
            _icon = _root.Q<VisualElement>("asset_icon");
            _nowValueLabel = _root.Q<Label>("now_value_label");
            _maxValueLabel = _root.Q<Label>("max_value_label");

            Debug.Assert(_icon != null, "icon element 를 찾지 못했습니다");
            Debug.Assert(_nowValueLabel != null, "nowValueLabel element 를 찾지 못했습니다");
            Debug.Assert(_maxValueLabel != null, "maxValueLabel element 를 찾지 못했습니다");

            _nowValueLabel.text = "0";
            _maxValueLabel.text = "0";
        }

        public void RefreshAssetData(AssetData assetData)
        {
            _nowValueLabel.text = assetData.GetValue();
        }

        public void CleanUp()
        {
            _root = null;
            _icon = null;
            _nowValueLabel = null;
            _maxValueLabel = null;
        }
    }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    [RequireComponent(typeof(UIDocument))]
    public class UIAssetBlock_Test : MonoBehaviour
    {
        private readonly string PATH_UI_ASSET_BLOCK_UXML = "Assets/Scripts/UI/UIGame/UIAsset/UIAssetBlock/UIAssetBlockUXML.uxml";

        private UIAssetBlock _uiAssetBlock;

        public UIAssetBlock Instance => _uiAssetBlock;

        public static UIAssetBlock_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UIAssetBlock_Test";
            return obj.AddComponent<UIAssetBlock_Test>();
        }

        public void Initialize()
        {
            _uiAssetBlock = UIAssetBlock.Create();
            _uiAssetBlock.Initialize(UIUXML.GetVisualElement(gameObject, PATH_UI_ASSET_BLOCK_UXML));
        }

        public void Dispose()
        {
            _uiAssetBlock = null;
            DestroyImmediate(gameObject);
        }
    }
#endif
}