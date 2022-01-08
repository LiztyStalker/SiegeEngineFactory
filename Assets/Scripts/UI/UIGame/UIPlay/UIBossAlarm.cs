namespace SEF.UI.Toolkit
{
    using UnityEngine;
    using UnityEngine.UIElements;
    public class UIBossAlarm : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<UIBossAlarm, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UIPlay/UIBossAlarm.uxml";


        private VisualElement _bossPanel;
        private VisualElement _themebossPanel;

        public static UIWorkshopLine Create()
        {
            return UIUXML.GetVisualElement<UIWorkshopLine>(PATH_UI_UXML);
        }

        public void Initialize()
        {
            _bossPanel = this.Q<VisualElement>("boss_panel");
            _themebossPanel = this.Q<VisualElement>("theme_boss_panel");

            _bossPanel.style.display = DisplayStyle.None;
            _themebossPanel.style.display = DisplayStyle.None;
            this.style.display = DisplayStyle.None;
        }

        public void CleanUp()
        {

        }

        public void ShowThemeBoss()
        {
            _bossPanel.style.display = DisplayStyle.None;
            _themebossPanel.style.display = DisplayStyle.Flex;
            this.style.display = DisplayStyle.Flex;
        }

        public void ShowBoss()
        {
            _bossPanel.style.display = DisplayStyle.Flex;
            _themebossPanel.style.display = DisplayStyle.None;
            this.style.display = DisplayStyle.Flex;            
        }
    }


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    [RequireComponent(typeof(UIDocument))]
    public class UIBossAlarm_Test : MonoBehaviour
    {
        private UIBossAlarm _instance;
        public UIBossAlarm Instance => _instance;

        public static UIBossAlarm_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UIBossAlarm_Test";
            return obj.AddComponent<UIBossAlarm_Test>();
        }

        public void Initialize()
        {
            var root = UIUXML.GetVisualElement(gameObject, UIBossAlarm.PATH_UI_UXML);
            _instance = root.Q<UIBossAlarm>();
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