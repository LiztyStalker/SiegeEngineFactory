namespace SEF.UI
{

    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using Entity;
    using SEF.Data;

    public class UIMine : MonoBehaviour, ISystemPanel
    {
        private readonly static string UGUI_NAME = "UI@Mine";

        private Dictionary<int, UIMineLine> _dic = new Dictionary<int, UIMineLine>();

        private int _lineCount = 0;

        private UIMineExpend _uiMineExpend;

        [SerializeField]
        private ScrollRect _scrollView;


        public static UIMine Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UIMine>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.name = UGUI_NAME;
                return obj.AddComponent<UIMine>();
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

            CreateExpendLine();
        }

        public void CleanUp()
        {
            _uiMineExpend.RemoveOnExpendListener(OnExpendEvent);
            _uiMineExpend.CleanUp();

            foreach (var value in _dic.Values)
            {
                value.RemoveOnUpgradeListener(OnUpgradeEvent);
                value.RemoveOnUpTechListener(OnUpTechEvent);
                value.CleanUp();
            }
            _dic.Clear();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void RefreshMine(int index, MineEntity entity)
        {
            if (!_dic.ContainsKey(index))
            {
                var line = UIMineLine.Create();
                line.Initialize();
                line.SetIndex(index);
                line.AddOnUpgradeListener(OnUpgradeEvent);
                line.AddOnUpTechListener(OnUpTechEvent);
                line.transform.SetParent(_scrollView.content);
                _dic.Add(index, line);
            }
            _dic[index].RefreshMineLine(entity);
            ChangeExpendLine();
        }

        public void RefreshAssetEntity(AssetPackage assetEntity)
        {
            foreach (var value in _dic.Values)
            {
                value.RefreshAssetEntity(assetEntity);
            }
        }

        public void RefreshExpend(IAssetData assetData, bool isActive) => _uiMineExpend.RefreshExpend(assetData, isActive);

        private void CreateExpendLine()
        {
            _uiMineExpend = UIMineExpend.Create();
            _uiMineExpend.Initialize();
            _uiMineExpend.AddOnExpendListener(OnExpendEvent);
            _uiMineExpend.transform.SetParent(_scrollView.content);
            _uiMineExpend.transform.SetAsLastSibling();
        }

        private void ChangeExpendLine()
        {
            if (_lineCount != _scrollView.content.childCount)
            {
                _uiMineExpend.transform.SetAsLastSibling();
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

        private System.Action<int> _techEvent;
        public void AddOnUpTechListener(System.Action<int> act) => _techEvent += act;
        public void RemoveOnUpTechListener(System.Action<int> act) => _techEvent -= act;
        private void OnUpTechEvent(int index)
        {
            _techEvent?.Invoke(index);
        }


        private System.Action _expendEvent;
        public void AddOnExpendListener(System.Action act) => _expendEvent += act;
        public void RemoveOnExpendListener(System.Action act) => _expendEvent -= act;
        private void OnExpendEvent()
        {
            _expendEvent?.Invoke();
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