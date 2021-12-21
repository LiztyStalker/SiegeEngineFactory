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

        private ScrollView _scrollView;

        public static UIWorkshop Create()
        {
            return UIUXML.GetVisualElement<UIWorkshop>(PATH_UI_WORKSHOP_UXML);
        }

        public void Initialize()
        {
            _scrollView = this.Q<ScrollView>();
        }

        public void RefreshUnit(int index, UnitEntity unitEntity, float nowTime)
        {
            if (!_dic.ContainsKey(index))
            {
                var line = UIWorkshopLine.Create();
                line.Initialize();
                _scrollView.Add(line);
                _dic.Add(index, line);
            }
            _dic[index].RefreshUnit(unitEntity, nowTime);
        }

        public void CleanUp()
        {
            _dic.Clear();
            _scrollView = null;
        }
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
