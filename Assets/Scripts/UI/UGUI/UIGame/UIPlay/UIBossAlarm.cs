namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    public class UIBossAlarm : MonoBehaviour
    {

        private readonly static string UGUI_NAME = "UI@BossAlarm";

        [SerializeField]
        private GameObject _bossPanel;

        [SerializeField]
        private GameObject _themebossPanel;

        public void Initialize()
        {
            _bossPanel.SetActive(false);
            _themebossPanel.SetActive(false);
            gameObject.SetActive(false);

            UIGame.ProcessEvent += RunProcess;
        }

        public void CleanUp()
        {
            UIGame.ProcessEvent -= RunProcess;
        }

        public void ShowAlarm(Data.TYPE_ENEMY_GROUP typeEnemyGroup)
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
            _themebossPanel.SetActive(true);
            _bossPanel.SetActive(false);
            gameObject.SetActive(true);
            
        }

        private void ShowBoss()
        {
            _themebossPanel.SetActive(false);
            _bossPanel.SetActive(true);
            gameObject.SetActive(true);
        }

        private float _nowTime = 0;
        private void RunProcess(float deltaTime)
        {
            if (gameObject.activeSelf)
            {
                _nowTime += Time.deltaTime;
                if (_nowTime > 1f)
                {
                    gameObject.SetActive(false);
                    _closedEvent?.Invoke();
                    _nowTime = 0;
                }
            }
        }


        public static UIBossAlarm Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UIBossAlarm>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.name = UGUI_NAME;
                return obj.AddComponent<UIBossAlarm>();
            }
#else
            Debug.LogWarning($"{UGUI_NAME}을 찾을 수 없습니다");
#endif
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
            _instance = UIBossAlarm.Create();
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