namespace SEF.UI.Toolkit
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Data;
    using Entity;
    using Utility.Address;

    public class UIResearch : VisualElement, ISystemPanel
    {
        public new class UxmlFactory : UxmlFactory<UIResearch, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UISystem/UIResearch/UIResearch.uxml";

        private Dictionary<int, UIResearchBlock> _dic = new Dictionary<int, UIResearchBlock>();

        private int _lineCount = 0;

        private ScrollView _scrollView;

        public static UIResearchBlock Create()
        {
            return UIUXML.GetVisualElement<UIResearchBlock>(PATH_UI_UXML);
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

        //BlacksmithEntity
        public void RefreshBlacksmith(int index)
        {
            if (!_dic.ContainsKey(index))
            {
                var line = UIResearchBlock.Create();
                line.Initialize();
                line.SetIndex(index);
                line.AddUpgradeListener(OnUpgradeEvent);
                _scrollView.Add(line);
                _dic.Add(index, line);
            }
            _dic[index].RefreshResearchLine();
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
    [RequireComponent(typeof(UIDocument))]
    public class UIResearch_Test : MonoBehaviour
    {
        private UIResearch _instance;
        public UIResearch Instance => _instance;

        public static UIResearch_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UIResearch_Test";
            return obj.AddComponent<UIResearch_Test>();
        }

        public void Initialize()
        {
            var root = UIUXML.GetVisualElement(gameObject, UIResearch.PATH_UI_UXML);
            _instance = root.Q<UIResearch>();
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