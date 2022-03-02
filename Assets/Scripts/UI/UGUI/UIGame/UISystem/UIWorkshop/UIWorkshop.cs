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

        private UIWorkshopLine _expendWorkshopLine;
        //prviate UIWorkshopExpendLine // 분리 필요

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
#endif
        }


        public void Initialize()
        {
            if (_scrollView == null)
            {
                _scrollView = GetComponentInChildren<ScrollRect>();
            }
            Debug.Assert(_scrollView != null, "_scrollView element 를 찾지 못했습니다");

            CreateExpendWorkshopLine();
        }

        public void CleanUp()
        {
            _expendWorkshopLine.RemoveExpendListener(OnExpendEvent);
            _expendWorkshopLine.CleanUp();

            foreach (var value in _dic.Values)
            {
                value.RemoveUpgradeListener(OnUpgradeEvent);
                value.RemoveUpTechListener(OnUpTechEvent);
                value.RemoveExpendListener(OnExpendEvent);
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

        public void RefreshUnit(int index, UnitEntity unitEntity, float nowTime)
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
            ChangeExpendWorkshopLine();
        }

        public void RefreshAssetEntity(AssetPackage assetEntity)
        {
            foreach(var value in _dic.Values)
            {
                value.RefreshAssetEntity(assetEntity);
            }
        }

        public void RefreshExpend(IAssetData assetData, bool isActive) => _expendWorkshopLine.RefreshExpend(assetData, isActive);

        private void CreateExpendWorkshopLine()
        {
            _expendWorkshopLine = UIWorkshopLine.Create();
            _expendWorkshopLine.Initialize();
            _expendWorkshopLine.AddExpendListener(OnExpendEvent);
            _expendWorkshopLine.transform.SetAsLastSibling();
//            _scrollView.Insert(_scrollView.childCount, _expendWorkshopLine);
        }

        private void ChangeExpendWorkshopLine()
        {
            if (_lineCount != _scrollView.content.childCount)
            {
                _expendWorkshopLine.transform.SetAsLastSibling();
            }
            _lineCount = _scrollView.content.childCount;
        }

#region ##### Listener #####


        private System.Action<int> _upgradeEvent;
        public void AddUpgradeListener(System.Action<int> act) => _upgradeEvent += act;
        public void RemoveUpgradeListener(System.Action<int> act) => _upgradeEvent -= act;
        private void OnUpgradeEvent(int index)
        {
            _upgradeEvent?.Invoke(index);
        }

        private System.Action<int, UnitTechData> _uptechEvent;
        public void AddUpTechListener(System.Action<int, UnitTechData> act) => _uptechEvent += act;
        public void RemoveUpTechListener(System.Action<int, UnitTechData> act) => _uptechEvent -= act;
        private void OnUpTechEvent(int index, UnitTechData data)
        {
            _uptechEvent?.Invoke(index, data);
        }

        private System.Action _expendEvent;
        public void AddExpendListener(System.Action act) => _expendEvent += act;
        public void RemoveExpendListener(System.Action act) => _expendEvent -= act;
        private void OnExpendEvent()
        {
            _expendEvent?.Invoke();
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
