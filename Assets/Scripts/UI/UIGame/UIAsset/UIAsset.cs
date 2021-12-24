namespace SEF.UI.Toolkit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Data;

    public class UIAsset //VisualElement로 교체 예정
    {
        private Dictionary<TYPE_ASSET, UIAssetBlock> _dic = new Dictionary<TYPE_ASSET, UIAssetBlock>();

        private VisualElement _root;

        public static UIAsset Create()
        {
            return new UIAsset();
        }

        public void Initialize(VisualElement root)
        {
            _root = root;

            for(int i = 0; i < _root.childCount; i++)
            {
                var block = UIAssetBlock.Create();
                block.Initialize(_root[i]);
                _dic.Add((TYPE_ASSET)i, block);
            }

        }

        public void RefreshAssetData(AssetData data)
        {
            if (_dic.ContainsKey(data.TypeAsset))
            {
                _dic[data.TypeAsset].RefreshAssetData(data);
            }
        }

        public void CleanUp()
        {
            _dic.Clear();
            _root = null;
        }

    }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    public class UIAsset_Test : MonoBehaviour
    {
        private readonly string PATH_UI_ASSET_UXML = "Assets/Scripts/UI/UIGame/UIAsset/UIAssetUXML.uxml";

        private UIAsset _uiAsset;

        public UIAsset Instance => _uiAsset;

        public static UIAsset_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UIAsset_Test";
            return obj.AddComponent<UIAsset_Test>();
        }

        public void Initialize()
        {
            _uiAsset = UIAsset.Create();
            _uiAsset.Initialize(UIUXML.GetVisualElement(gameObject, PATH_UI_ASSET_UXML));
        }

        public void Dispose()
        {
            _uiAsset = null;
            DestroyImmediate(gameObject);
        }
    }
#endif
}