namespace SEF.UI
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using Data;
    using Entity;

    public class UIQuest : MonoBehaviour
    {
        private readonly static string UGUI_NAME = "UI@Quest";

        private Dictionary<string, UIQuestLine> _dic = new Dictionary<string, UIQuestLine>();

        [SerializeField]
        private ScrollRect _scrollView;

        [SerializeField]
        private Button _exitButton;

        [SerializeField]
        private Button _dailyButton;
        [SerializeField]
        private Button _weeklyButton;
        [SerializeField]
        private Button _challengeButton;
        [SerializeField]
        private Button _goalButton;

        private QuestData.TYPE_QUEST_GROUP _typeQuestGroup;


        public static UIQuest Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UIQuest>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.name = UGUI_NAME;
                return obj.AddComponent<UIQuest>();
            }
#else
            Debug.LogWarning($"{UGUI_NAME}을 찾을 수 없습니다");
            return null;
#endif
        }


        public void Initialize()
        {
            Debug.Assert(_scrollView != null, "_scrollView 이 등록되지 않았습니다");
            Debug.Assert(_exitButton != null, "_exitButton 이 등록되지 않았습니다");
            Debug.Assert(_dailyButton != null, "_dailyButton 이 등록되지 않았습니다");
            Debug.Assert(_weeklyButton != null, "_weeklyButton 이 등록되지 않았습니다");
            Debug.Assert(_challengeButton != null, "_challengeButton 이 등록되지 않았습니다");
            Debug.Assert(_goalButton != null, "_goalButton 이 등록되지 않았습니다");

            _dailyButton.onClick.AddListener(() => { RefreshTypeQuestData(QuestData.TYPE_QUEST_GROUP.Daily); });
            _weeklyButton.onClick.AddListener(() => { RefreshTypeQuestData(QuestData.TYPE_QUEST_GROUP.Weekly); });
            _challengeButton.onClick.AddListener(() => { RefreshTypeQuestData(QuestData.TYPE_QUEST_GROUP.Challenge); });
            _goalButton.onClick.AddListener(() => { RefreshTypeQuestData(QuestData.TYPE_QUEST_GROUP.Goal); });

            _exitButton.onClick.AddListener(Hide);

            Hide();
        }

        public void CleanUp()
        {
            foreach (var value in _dic.Values)
            {
                value.RemoveOnRewardListener(OnRewardEvent);
                value.CleanUp();
            }

            _dic.Clear();

            _dailyButton.onClick.RemoveAllListeners();
            _weeklyButton.onClick.RemoveAllListeners();
            _challengeButton.onClick.RemoveAllListeners();
            _goalButton.onClick.RemoveAllListeners();

            _exitButton.onClick.RemoveListener(Hide);

            _closedEvent = null;
        }

        private void ClearDictionary()
        {
            foreach (var value in _dic.Values)
            {
                value.Hide();
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            OnClosedEvent();
        }

        public void RefreshQuest(QuestEntity entity)
        {            
            if (entity.TypeQuestGroup == _typeQuestGroup)
            {
                var key = entity.Key;
                if (!_dic.ContainsKey(key))
                {
                    var line = UIQuestLine.Create();
                    line.Initialize();
                    line.AddOnRewardListener(OnRewardEvent);
                    line.transform.SetParent(_scrollView.content);
                    _dic.Add(key, line);
                }
                _dic[key].RefreshQuestLine(entity);
                _dic[key].Show();
            }
        }

#region ##### Listener #####


        private System.Action<QuestData.TYPE_QUEST_GROUP, string, string, bool> _rewardEvent;
        public void AddOnRewardListener(System.Action<QuestData.TYPE_QUEST_GROUP, string, string, bool> act) => _rewardEvent += act;
        public void RemoveOnRewardListener(System.Action<QuestData.TYPE_QUEST_GROUP, string, string, bool> act) => _rewardEvent -= act;
        private void OnRewardEvent(string questKey, string addressKey, bool hasGoal)
        {
            _rewardEvent?.Invoke(_typeQuestGroup, questKey, addressKey, hasGoal);
        }

        private System.Action<QuestData.TYPE_QUEST_GROUP> _refreshEvent;
        public void AddOnRefreshListener(System.Action<QuestData.TYPE_QUEST_GROUP> act) => _refreshEvent += act;
        public void RemoveOnRefreshListener(System.Action<QuestData.TYPE_QUEST_GROUP> act) => _refreshEvent -= act;
        private void RefreshTypeQuestData(QuestData.TYPE_QUEST_GROUP typeQuestData)
        {
            _typeQuestGroup = typeQuestData;
            ClearDictionary();
            _refreshEvent?.Invoke(_typeQuestGroup);
        }

        private System.Action _closedEvent;
        public void AddOnClosedListener(System.Action act) => _closedEvent += act;
        public void RemoveOnClosedListener(System.Action act) => _closedEvent -= act;

        private void OnClosedEvent()
        {
            _closedEvent?.Invoke();
        }


        #endregion


    }




#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    public class UIQuest_Test : MonoBehaviour
    {
        private UIQuest _instance;
        public UIQuest Instance => _instance;

        public static UIQuest_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UIQuest_Test";
            return obj.AddComponent<UIQuest_Test>();
        }

        public void Initialize()
        {
            _instance = UIQuest.Create();
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