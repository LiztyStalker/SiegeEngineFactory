namespace SEF.UI
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using Entity;
    using System;

    public class UIVillage : MonoBehaviour, ISystemPanel
    {
        private readonly static string UGUI_NAME = "UI@Village";

        private Dictionary<int, UIVillageLine> _dic = new Dictionary<int, UIVillageLine>();

        [SerializeField]
        private ScrollRect _scrollView;

        public static UIVillage Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UIVillage>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.name = UGUI_NAME;
                return obj.AddComponent<UIVillage>();
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
        }

        public void CleanUp()
        {
            foreach (var value in _dic.Values)
            {
                value.RemoveUpgradeListener(OnUpgradeEvent);
                value.RemoveOnUpTechListener(OnUpTechEvent);
                value.CleanUp();
            }
            _dic.Clear();
            _scrollView = null;
        }

        public void Show()
        {
            OnShowEvent();
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        //BlacksmithEntity
        public void RefreshEntity(int index, VillageEntity entity)
        {
            if (!_dic.ContainsKey(index))
            {
                var line = UIVillageLine.Create();
                line.Initialize();
                line.SetIndex(index);
                line.AddUpgradeListener(OnUpgradeEvent);
                line.AddOnUpTechListener(OnUpTechEvent);
                line.transform.SetParent(_scrollView.content);
                _dic.Add(index, line);
            }
            _dic[index].RefreshVillageLine(entity);
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
        public void AddOnUpgradeListener(System.Action<int> act) => _upgradeEvent += act;
        public void RemoveOnUpgradeListener(System.Action<int> act) => _upgradeEvent -= act;
        private void OnUpgradeEvent(int index)
        {
            _upgradeEvent?.Invoke(index);
        }


        private System.Action<int> _uptechEvent;
        public void AddOnUpTechListener(System.Action<int> act) => _uptechEvent += act;
        public void RemoveOnUpTechListener(System.Action<int> act) => _uptechEvent -= act;
        private void OnUpTechEvent(int index)
        {
            _uptechEvent?.Invoke(index);
        }

        private System.Action _showEvent;
        public void AddOnShowListener(Action act) => _showEvent += act;
        public void RemoveOnShowListener(Action act) => _showEvent -= act;
        private void OnShowEvent() => _showEvent?.Invoke();
        #endregion


    }




#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    public class UIVillage_Test : MonoBehaviour
    {
        private UIVillage _instance;
        public UIVillage Instance => _instance;

        public static UIVillage_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UIVillage_Test";
            return obj.AddComponent<UIVillage_Test>();
        }

        public void Initialize()
        {
            _instance = UIVillage.Create();
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