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

            UIGame.ProcessEvent += RunProcess;
        }

        public void CleanUp()
        {
            UIGame.ProcessEvent -= RunProcess;

            _bossPanel = null;
            _themebossPanel = null;
        }

        public void ShowAlarm(SEF.Data.TYPE_ENEMY_GROUP typeEnemyGroup)
        {
            switch (typeEnemyGroup)
            {
                case Data.TYPE_ENEMY_GROUP.Boss:
                    ShowBoss();
                    break;
                case Data.TYPE_ENEMY_GROUP.ThemeBoss:
                    ShowThemeBoss();
                    break;
                case Data.TYPE_ENEMY_GROUP.Normal:
                    break;
                default:
                    Debug.LogError($"{typeEnemyGroup} 타입이 지정되지 않았습니다");
                    break;
            }
        }

        private void ShowThemeBoss()
        {
            _bossPanel.style.display = DisplayStyle.None;
            _themebossPanel.style.display = DisplayStyle.Flex;
            this.style.display = DisplayStyle.Flex;
            
        }

        private void ShowBoss()
        {
            _bossPanel.style.display = DisplayStyle.Flex;
            _themebossPanel.style.display = DisplayStyle.None;
            this.style.display = DisplayStyle.Flex;            
        }

        private float _nowTime = 0;
        private void RunProcess(float deltaTime)
        {
            if (this.style.display == DisplayStyle.Flex)
            {
                _nowTime += Time.deltaTime;
                if (_nowTime > 1f)
                {
                    this.style.display = DisplayStyle.None;
                    _closedEvent?.Invoke();
                }
            }
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        /// <summary>
        /// 테스트용
        /// </summary>
        /// <param name="deltaTime"></param>
        public void RunProcess_Test(float deltaTime)
        {
            RunProcess(deltaTime);
        }
#endif


        #region ##### Listener #####

        private System.Action _closedEvent;

        public void SetOnClosedEvent(System.Action act) => _closedEvent = act;

        #endregion

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