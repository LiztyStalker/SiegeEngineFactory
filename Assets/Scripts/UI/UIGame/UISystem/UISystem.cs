namespace SEF.UI.Toolkit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Entity;

    public class UISystem : VisualElement 
    {

        internal static readonly string PATH_UI_SYSTEM_UXML = "Assets/Scripts/UI/UIGame/UISystem/UISystemUXML.uxml";

        public new class UxmlFactory : UxmlFactory<UISystem, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }



        private UIWorkshop _uiWorkshop;
        //UIBlacksmith
        //UIVillage
        //UIResearch


        public static UISystem Create()
        {
            return UIUXML.GetVisualElement<UISystem>(PATH_UI_SYSTEM_UXML);
        }

        public void Initialize()
        {
            _uiWorkshop = this.Q<UIWorkshop>();

            Debug.Assert(_uiWorkshop != null, "_uiWorkshop 이 등록되지 않았습니다");

            _uiWorkshop.Initialize();
        }

        public void CleanUp()
        {
            _uiWorkshop.CleanUp();
            _uiWorkshop = null;
        }



        public void RefreshUnit(int index, UnitEntity unitEntity, float nowTime)
        {
            _uiWorkshop.RefreshUnit(index, unitEntity, nowTime);
        }
    }



#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    [RequireComponent(typeof(UIDocument))]
    public class UISystem_Test : MonoBehaviour
    {
        private UISystem _instance;
        public UISystem Instance => _instance;

        public static UISystem_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UISystem_Test";
            return obj.AddComponent<UISystem_Test>();
        }

        public void Initialize()
        {
            var root = UIUXML.GetVisualElement(gameObject, UISystem.PATH_UI_SYSTEM_UXML);
            _instance = root.Q<UISystem>();
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