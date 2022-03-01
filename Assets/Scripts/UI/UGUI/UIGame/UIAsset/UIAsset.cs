namespace SEF.UI
{
    using Data;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class UIAsset : MonoBehaviour
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

        public static UIAsset Create() => new UIAsset();

        public void Initialize()
        {
            for (int i = 0; i < assetArray.Length; i++)
            {
                var block = UIAssetBlock.Create();
                block.Initialize(assetArray[i]);
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
        }

    }

    #region ##### Test #####
#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    public class UIAsset_Test : MonoBehaviour
    {

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
            _uiAsset.Initialize();
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