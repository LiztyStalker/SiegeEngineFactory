namespace SEF.UI.Toolkit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Data;

    public class UIAssetBlock
    {
        private VisualElement _root;

        private TYPE_ASSET _typeAsset;
        public TYPE_ASSET TypeAsset => _typeAsset;

        public static UIAssetBlock Create()
        {
            return new UIAssetBlock();
        }

        public void Initialize(VisualElement root)
        {
            _root = root;
        }

    }
}