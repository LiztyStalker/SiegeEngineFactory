namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using Data;

    public class UIAssetBlock 
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private Text _valueLabel;

        public static UIAssetBlock Create()
        {
            return new UIAssetBlock();
        }

        public void Initialize(string key)
        {
            var sprite = Storage.DataStorage.Instance.GetDataOrNull<Sprite>($"Icon_{key}", null, null);
            _icon.sprite = sprite;
            _valueLabel.text = "0";
        }

        public void RefreshAssetData(IAssetData assetData)
        {
            _valueLabel.text = assetData.GetValue();
        }

        public void CleanUp()
        {
        }
    }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    public class UIAssetBlock_Test : MonoBehaviour
    {
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
            _uiAssetBlock.Initialize(null);
        }

        public void Dispose()
        {
            _uiAssetBlock = null;
            DestroyImmediate(gameObject);
        }
    }
#endif
}
