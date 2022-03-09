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

        private UIMineExpand _expandLine;

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

            CreateExpandLine();
        }

        public void CleanUp()
        {
            _expandLine.RemoveOnExpandListener(OnExpandEvent);
            _expandLine.CleanUp();

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

        public void RefreshEntity(int index, MineEntity entity)
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
            ChangeExpandLine();
        }

        public void RefreshAssetEntity(AssetPackage assetEntity)
        {
            foreach (var value in _dic.Values)
            {
                value.RefreshAssetEntity(assetEntity);
            }
        }

        public void RefreshExpand(IAssetData assetData, bool isActive) => _expandLine.RefreshExpand(assetData, isActive);

        private void CreateExpandLine()
        {
            _expandLine = UIMineExpand.Create();
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

        private System.Action<int> _techEvent;
        public void AddOnUpTechListener(System.Action<int> act) => _techEvent += act;
        public void RemoveOnUpTechListener(System.Action<int> act) => _techEvent -= act;
        private void OnUpTechEvent(int index)
        {
            _techEvent?.Invoke(index);
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