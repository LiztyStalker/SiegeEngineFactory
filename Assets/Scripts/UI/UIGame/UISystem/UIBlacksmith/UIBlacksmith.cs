namespace SEF.UI.Toolkit
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Data;
    using Entity;
    using Utility.Address;

    public class UIBlacksmith : VisualElement, ISystemPanel
    {
        public new class UxmlFactory : UxmlFactory<UIBlacksmith, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UISystem/UIBlacksmith/UIBlacksmith.uxml";

        private Dictionary<int, UIBlacksmithLine> _dic = new Dictionary<int, UIBlacksmithLine>();

        private ScrollView _scrollView;

        public static UIBlacksmith Create()
        {
            return UIUXML.GetVisualElement<UIBlacksmith>(PATH_UI_UXML);
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

        public void RefreshBlacksmith(int index, BlacksmithEntity entity)
        {
            if (!_dic.ContainsKey(index))
            {
                //Debug.Log("Create");
                var line = UIBlacksmithLine.Create();
                line.Initialize();
                line.SetIndex(index);
                line.AddUpgradeListener(OnUpgradeEvent);
                _scrollView.Add(line);
                _dic.Add(index, line);
            }
            _dic[index].RefreshBlacksmithLine(entity);
        }

        public void RefreshAssetEntity(AssetEntity assetEntity)
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
    [RequireComponent(typeof(UIDocument))]
    public class UIBlacksmith_Test : MonoBehaviour
    {
        private UIBlacksmith _instance;
        public UIBlacksmith Instance => _instance;

        public static UIBlacksmith_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UIBlacksmith_Test";
            return obj.AddComponent<UIBlacksmith_Test>();
        }

        public void Initialize()
        {
            var root = UIUXML.GetVisualElement(gameObject, UIBlacksmith.PATH_UI_UXML);
            _instance = root.Q<UIBlacksmith>();
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