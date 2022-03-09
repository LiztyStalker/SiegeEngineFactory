namespace SEF.UI
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using Data;
    using Entity;

    public class UIWorkshop : MonoBehaviour, ISystemPanel
    {
        private readonly static string UGUI_NAME = "UI@Workshop";


        private Dictionary<int, UIWorkshopLine> _dic = new Dictionary<int, UIWorkshopLine>();

        private int _lineCount = 0;

        private UIWorkshopExpand _expandLine;

        [SerializeField]
        private ScrollRect _scrollView;

        public static UIWorkshop Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UIWorkshop>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.name = UGUI_NAME;
                return obj.AddComponent<UIWorkshop>();
            }
#else
            Debug.LogWarning($"{UGUI_NAME}을 찾을 수 없습니다");
            return null;
#endif
        }


        public void Initialize()
        {
            if (_scrollView == null)
            {
                _scrollView = GetComponentInChildren<ScrollRect>();
            }
            Debug.Assert(_scrollView != null, "_scrollView element 를 찾지 못했습니다");

            CreateExpandLine();
        }

        public void CleanUp()
        {
            _expandLine.RemoveOnExpandListener(OnExpandEvent);
            _expandLine.CleanUp();

            foreach (var value in _dic.Values)
            {
                value.RemoveUpgradeListener(OnUpgradeEvent);
                value.RemoveUpTechListener(OnUpTechEvent);
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

        public void RefreshEntity(int index, UnitEntity unitEntity, float nowTime)
        {
            if (!_dic.ContainsKey(index))
            {
                var line = UIWorkshopLine.Create();
                line.Initialize();
                line.SetIndex(index);
                line.AddUpgradeListener(OnUpgradeEvent);
                line.AddUpTechListener(OnUpTechEvent);
                line.transform.SetParent(_scrollView.content);
                _dic.Add(index, line);
            }
            _dic[index].RefreshUnit(unitEntity, nowTime);
            ChangeExpandLine();
        }

        public void RefreshAssetEntity(AssetPackage assetEntity)
        {
            foreach(var value in _dic.Values)
            {
                value.RefreshAssetEntity(assetEntity);
            }
        }

        public void RefreshExpand(IAssetData assetData, bool isActive) => _expandLine.RefreshExpand(assetData, isActive);

        private void CreateExpandLine()
        {
            _expandLine = UIWorkshopExpand.Create();
            _expandLine.Initialize();
            _expandLine.AddOnExpandListener(OnExpandEvent);
            _expandLine.transform.SetParent(_scrollView.content);
            _expandLine.transform.SetAsLastSibling();
        }

        private void ChangeExpandLine()
        {
            if (_lineCount != _scrollView.content.childCount)
            {
                _expandLine.transform.SetAsLastSibling();
            }
            _lineCount = _scrollView.content.childCount;
        }

#region ##### Listener #####


        private System.Action<int> _upgradeEvent;
        public void AddOnUpgradeListener(System.Action<int> act) => _upgradeEvent += act;
        public void RemoveOnUpgradeListener(System.Action<int> act) => _upgradeEvent -= act;
        private void OnUpgradeEvent(int index)
        {
            _upgradeEvent?.Invoke(index);
        }

        private System.Action<int, UnitTechData> _uptechEvent;
        public void AddOnUpTechListener(System.Action<int, UnitTechData> act) => _uptechEvent += act;
        public void RemoveOnUpTechListener(System.Action<int, UnitTechData> act) => _uptechEvent -= act;
        private void OnUpTechEvent(int index, UnitTechData data)
        {
            _uptechEvent?.Invoke(index, data);
        }

        private System.Action _expandEvent;
        public void AddOnExpandListener(System.Action act) => _expandEvent += act;
        public void RemoveOnExpandListener(System.Action act) => _expandEvent -= act;
        private void OnExpandEvent()
        {
            _expandEvent?.Invoke();
        }

#endregion


    }




#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    public class UIWorkshop_Test : MonoBehaviour
    {
        private UIWorkshop _instance;
        public UIWorkshop Instance => _instance;

        public static UIWorkshop_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UIWorkshop_Test";
            return obj.AddComponent<UIWorkshop_Test>();
        }

        public void Initialize()
        {
            _instance = UIWorkshop.Create();
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
