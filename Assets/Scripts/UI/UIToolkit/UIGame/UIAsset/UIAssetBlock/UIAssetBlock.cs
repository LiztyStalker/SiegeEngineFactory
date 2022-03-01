#if INCLUDE_UI_TOOLKIT
namespace SEF.UI.Toolkit
{
    using UnityEngine;
    using UnityEngine.UIElements;
    using Data;

    public class UIAssetBlock // VisualElement로 교체 예정
    {
        private VisualElement _root;

        //private TYPE_ASSET _typeAsset;
        //public TYPE_ASSET TypeAsset => _typeAsset;

        private VisualElement _icon;

        private Label _nowValueLabel;

        //private Label _maxValueLabel;

        public static UIAssetBlock Create()
        {
            return new UIAssetBlock();
        }

        public void Initialize(VisualElement root, string key)
        {
            _root = root;
            _icon = _root.Q<VisualElement>("asset_icon");
            _nowValueLabel = _root.Q<Label>("now_value_label");
//            _maxValueLabel = _root.Q<Label>("max_value_label");

            Debug.Assert(_icon != null, "icon element 를 찾지 못했습니다");
            Debug.Assert(_nowValueLabel != null, "nowValueLabel element 를 찾지 못했습니다");
            //            Debug.Assert(_maxValueLabel != null, "maxValueLabel element 를 찾지 못했습니다");

            var sprite = Storage.DataStorage.Instance.GetDataOrNull<Sprite>($"Icon_{key}", null, null);
            _icon.style.backgroundImage = sprite.texture;
            _nowValueLabel.text = "0";
//            _maxValueLabel.text = "0";
        }

        public void RefreshAssetData(IAssetData assetData)
        {
            //Debug.Log(assetData.GetType().Name);
            _nowValueLabel.text = assetData.GetValue();
        }

        public void CleanUp()
        {
            _root = null;
            _icon = null;
            _nowValueLabel = null;
            //_maxValueLabel = null;
        }
    }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    [RequireComponent(typeof(UIDocument))]
    public class UIAssetBlock_Test : MonoBehaviour
    {
        private readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UIAsset/UIAssetBlock/UIAssetBlock.uxml";

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
            _uiAssetBlock.Initialize(UIUXML.GetVisualElement(gameObject, PATH_UI_UXML), null);
        }

        public void Dispose()
        {
            _uiAssetBlock = null;
            DestroyImmediate(gameObject);
        }
    }
#endif
}
#endif