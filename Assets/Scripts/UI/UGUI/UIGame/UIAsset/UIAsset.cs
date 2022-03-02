namespace SEF.UI
{
    using Data;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Canvas))]
    public class UIAsset : MonoBehaviour
    {
        private readonly static string UGUI_NAME = "UI@Asset";

        private string[] assetArray = new string[]
        {
            typeof(GoldAssetData).Name,
            typeof(ResourceAssetData).Name,
            typeof(MeteoriteAssetData).Name,
            typeof(ResearchAssetData).Name,
            typeof(PopulationAssetData).Name
        };

        private Dictionary<string, UIAssetBlock> _dic = new Dictionary<string, UIAssetBlock>();

        [SerializeField]
        private Transform _layout;



        public static UIAsset Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UIAsset>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.name = UGUI_NAME;
                return obj.AddComponent<UIAsset>();
            }
#else
            Debug.LogWarning($"{UGUI_NAME}을 찾을 수 없습니다");
#endif
        }


        public void Initialize()
        {
            for (int i = 0; i < assetArray.Length; i++)
            {
                var block = UIAssetBlock.Create();
                block.Initialize(assetArray[i]);
                block.transform.SetParent(_layout);
                _dic.Add(assetArray[i], block);
            }
        }

        public void RefreshAssetData(IAssetData data)
        {
            var typeName = data.GetType().Name;
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

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    #region ##### Test #####
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
    #endregion
#endif
}