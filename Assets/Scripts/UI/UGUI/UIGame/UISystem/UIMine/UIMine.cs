namespace SEF.UI
{

    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using Entity;

    public class UIMine : MonoBehaviour, ISystemPanel
    {
        private Dictionary<int, UIMineLine> _dic = new Dictionary<int, UIMineLine>();

        private int _lineCount = 0;

        [SerializeField]
        private ScrollRect _scrollView;

        public static UIMine Create()
        {
            var obj = new GameObject();
            obj.name = "UI@Mine";
            obj.AddComponent<RectTransform>();
            return obj.AddComponent<UIMine>();
        }

        public void Initialize()
        {
            if (_scrollView == null)
            {
                _scrollView = GetComponentInChildren<ScrollRect>();
            }
            Debug.Assert(_scrollView != null, "_scrollView element 를 찾지 못했습니다");
        }

        public void CleanUp()
        {
            foreach (var value in _dic.Values)
            {
                value.RemoveUpgradeListener(OnUpgradeEvent);
                value.CleanUp();
            }
            _dic.Clear();
            _scrollView = null;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        //BlacksmithEntity
        public void RefreshMine(int index, MineEntity entity)
        {
            if (!_dic.ContainsKey(index))
            {
                var line = UIMineLine.Create();
                line.Initialize();
                line.SetIndex(index);
                line.AddUpgradeListener(OnUpgradeEvent);
                line.transform.SetParent(_scrollView.content);
                _dic.Add(index, line);
            }
            _dic[index].RefreshMineLine(entity);
        }

        public void RefreshAssetEntity(AssetPackage assetEntity)
        {
            foreach (var value in _dic.Values)
            {
                value.RefreshAssetEntity(assetEntity);
            }
        }

#region ##### Listener #####


        private System.Action<int> _upgradeEvent;
        public void AddUpgradeListener(System.Action<int> act) => _upgradeEvent += act;
        public void RemoveUpgradeListener(System.Action<int> act) => _upgradeEvent -= act;
        private void OnUpgradeEvent(int index)
        {
            _upgradeEvent?.Invoke(index);
        }

#endregion


    }




#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    public class UIMine_Test : MonoBehaviour
    {
        private UIMine _instance;
        public UIMine Instance => _instance;

        public static UIMine_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UIMine_Test";
            return obj.AddComponent<UIMine_Test>();
        }

        public void Initialize()
        {
            _instance = UIMine.Create();
            _instance.Initialize();
        }

        public void Dispose()
        {
            _instance = null;
            DestroyImmediate(gameObject);
        }
    }
#endif
}