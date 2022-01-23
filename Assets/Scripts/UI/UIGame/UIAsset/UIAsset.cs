namespace SEF.UI.Toolkit
{
    using Data;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class UIAsset //VisualElement로 교체 예정
    {

        private string[] assetArray = new string[]
        {
            typeof(GoldAssetData).Name,
            typeof(ResourceAssetData).Name,
            typeof(MeteoriteAssetData).Name,
            typeof(ResearchAssetData).Name,
            typeof(PopulationAssetData).Name
        };

        private Dictionary<string, UIAssetBlock> _dic = new Dictionary<string, UIAssetBlock>();

        private VisualElement _root;

        public static UIAsset Create() => new UIAsset();

        public void Initialize(VisualElement root)
        {
            _root = root;

            var tr = _root.Query<VisualElement>().First()[0];

            for (int i = 0; i < assetArray.Length; i++)
            {
                var block = UIAssetBlock.Create();
                block.Initialize(tr[i]);
                _dic.Add(assetArray[i], block);
            }
        }

        public void RefreshAssetData(IAssetData data)
        {
            var typeName = data.GetType().Name;
            //Debug.Log(typeName);
            if (_dic.ContainsKey(typeName))
            {
                _dic[typeName].RefreshAssetData(data);
            }
        }


        public void CleanUp()
        {
            _dic.Clear();
            _root = null;
        }

    }

    #region ##### Test #####
#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    public class UIAsset_Test : MonoBehaviour
    {
        private readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UIAsset/UIAsset.uxml";

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
            _uiAsset.Initialize(UIUXML.GetVisualElement(gameObject, PATH_UI_UXML));
        }

        public void Dispose()
        {
            _uiAsset = null;
            DestroyImmediate(gameObject);
        }
    }
#endif
    #endregion
}