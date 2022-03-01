#if INCLUDE_UI_TOOLKIT
namespace SEF.UI.Toolkit
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Entity;

    public class UISmithy : VisualElement, ISystemPanel
    {
        public new class UxmlFactory : UxmlFactory<UISmithy, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UISystem/UISmithy/UISmithy.uxml";
        internal static readonly string PATH_UI_USS = "Assets/Scripts/UI/UIGame/UISystem/UISmithy/UISmithy.uss";

        private Dictionary<int, UISmithyLine> _dic = new Dictionary<int, UISmithyLine>();

        private ScrollView _scrollView;

        public static UISmithy Create()
        {
            return UIUXML.GetVisualElement<UISmithy>(PATH_UI_UXML, PATH_UI_USS);
        }

        public void Initialize()
        {
            _scrollView = this.Q<ScrollView>();
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
            this.style.display = DisplayStyle.Flex;
        }

        public void Hide()
        {
            this.style.display = DisplayStyle.None;
        }

        //private bool IsView() => this.style.display == DisplayStyle.Flex;

        public void RefreshSmithy(int index, SmithyEntity entity)
        {
            if (!_dic.ContainsKey(index))
            {
                var line = UISmithyLine.Create();
                Debug.Log("Create " + line);
                line.Initialize();
                line.SetIndex(index);
                line.AddUpgradeListener(OnUpgradeEvent);
                line.AddOnUpTechListener(OnUpTechEvent);
                _scrollView.Add(line);
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
    [RequireComponent(typeof(UIDocument))]
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
            var root = UIUXML.GetVisualElement(gameObject, UISmithy.PATH_UI_UXML);
            _instance = root.Q<UISmithy>();
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
#endif