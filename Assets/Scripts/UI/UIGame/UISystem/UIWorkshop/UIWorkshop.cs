namespace SEF.UI.Toolkit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Data;
    using Entity;

    public class UIWorkshop : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<UIWorkshop, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        internal static readonly string PATH_UI_WORKSHOP_UXML = "Assets/Scripts/UI/UIGame/UISystem/UIWorkshop/UIWorkshopUXML.uxml";

        private Dictionary<int, UIWorkshopLine> _dic = new Dictionary<int, UIWorkshopLine>();

        private int _lineCount = 0;

        private UIWorkshopLine _expendWorkshopLine;
        //prviate UIWorkshopExpendLine // 분리 필요

        private ScrollView _scrollView;

        public static UIWorkshop Create()
        {
            return UIUXML.GetVisualElement<UIWorkshop>(PATH_UI_WORKSHOP_UXML);
        }

        public void Initialize()
        {
            _scrollView = this.Q<ScrollView>();
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


        public void RefreshUnit(int index, UnitEntity unitEntity, float nowTime)
        {
            if (!_dic.ContainsKey(index))
            {
                var line = UIWorkshopLine.Create();
                line.Initialize();
                line.SetIndex(index);
                line.AddUpgradeListener(OnUpgradeEvent);
                line.AddUpTechListener(OnUpTechEvent);
                _scrollView.Add(line);
                _dic.Add(index, line);
            }
            _dic[index].RefreshUnit(unitEntity, nowTime);
            ChangeExpendWorkshopLine();
        }

        public void RefreshAssetEntity(AssetEntity assetEntity)
        {
            foreach(var value in _dic.Values)
            {
                value.RefreshAssetEntity(assetEntity);
            }
        }

        private void CreateExpendWorkshopLine()
        {
            _expendWorkshopLine = UIWorkshopLine.Create();
            _expendWorkshopLine.Initialize();
            _expendWorkshopLine.AddExpendListener(OnExpendEvent);
            _scrollView.Insert(_scrollView.childCount, _expendWorkshopLine);
        }

        private void ChangeExpendWorkshopLine()
        {
            if (_lineCount != _scrollView.childCount)
            {
                _scrollView.Remove(_expendWorkshopLine);
                _scrollView.Insert(_scrollView.childCount, _expendWorkshopLine);
            }
            _lineCount = _scrollView.childCount;
        }

        #region ##### Listener #####


        private System.Action<int> _upgradeEvent;
        public void AddUpgradeListener(System.Action<int> act) => _upgradeEvent += act;
        public void RemoveUpgradeListener(System.Action<int> act) => _upgradeEvent -= act;
        private void OnUpgradeEvent(int index)
        {
            _upgradeEvent?.Invoke(index);
        }

        private System.Action<int, UnitData> _uptechEvent;
        public void AddUpTechListener(System.Action<int, UnitData> act) => _uptechEvent += act;
        public void RemoveUpTechListener(System.Action<int, UnitData> act) => _uptechEvent -= act;
        private void OnUpTechEvent(int index, UnitData unitData)
        {
            _uptechEvent?.Invoke(index, unitData);
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
    [RequireComponent(typeof(UIDocument))]
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
            var root = UIUXML.GetVisualElement(gameObject, UIWorkshop.PATH_UI_WORKSHOP_UXML);
            _instance = root.Q<UIWorkshop>();
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
