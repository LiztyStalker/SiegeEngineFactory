namespace SEF.UI.Toolkit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Data;

    public class UIAsset
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
        }

        public void RefreshAssetData(AssetData data)
        {
            if (_dic.ContainsKey(data.TypeAsset))
            {
                //_dic[data.TypeAsset]
            }
        }

        public void CleanUp()
        {

        }
    }
}