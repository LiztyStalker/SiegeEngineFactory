namespace SEF.UI
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using Entity;

    public class UISmithy : MonoBehaviour, ISystemPanel
    {
        private readonly static string UGUI_NAME = "UI@Smithy";

        private Dictionary<int, UISmithyLine> _dic = new Dictionary<int, UISmithyLine>();

        [SerializeField]
        private ScrollRect _scrollView;


        public static UISmithy Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UISmithy>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.name = UGUI_NAME;
                return obj.AddComponent<UISmithy>();
            }
#else
            Debug.LogWarning($"{UGUI_NAME}을 찾을 수 없습니다");
            return null;
#endif
        }

        public void Initialize()
        {
            if(_scrollView == null)
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
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        //private bool IsView() => this.style.display == DisplayStyle.Flex;

        public void RefreshSmithy(int index, SmithyEntity entity)
        {
            if (!_dic.ContainsKey(index))
            {
                var line = UISmithyLine.Create();
                line.Initialize();
                line.SetIndex(index);
                line.AddUpgradeListener(OnUpgradeEvent);
                line.AddOnUpTechListener(OnUpTechEvent);
                line.transform.SetParent(_scrollView.content);
                _dic.Add(index, line);
            }
            _dic[index].RefreshSmithyLine(entity);
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


        private System.Action<int> _uptechEvent;
        public void AddOnUpTechListener(System.Action<int> act) => _uptechEvent += act;
        public void RemoveOnUpTechListener(System.Action<int> act) => _uptechEvent -= act;
        private void OnUpTechEvent(int index)
        {
            _uptechEvent?.Invoke(index);
        }

#endregion


    }




#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    public class UISmithy_Test : MonoBehaviour
    {
        private UISmithy _instance;
        public UISmithy Instance => _instance;

        public static UISmithy_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UISmithy_Test";
            return obj.AddComponent<UISmithy_Test>();
        }

        public void Initialize()
        {
            _instance = UISmithy.Create();
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