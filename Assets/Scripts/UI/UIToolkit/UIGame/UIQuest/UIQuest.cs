namespace SEF.UI.Toolkit
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Data;
    using Entity;

    public class UIQuest : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<UIQuest, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UIQuest/UIQuest.uxml";

        internal static readonly string PATH_UI_USS = "Assets/Scripts/UI/UIGame/UIQuest/UIQuest.uss";

        private Dictionary<string, UIQuestLine> _dic = new Dictionary<string, UIQuestLine>();

        private ScrollView _scrollView;

        private Button _exitButton;

        private Button _dailyButton;
        private Button _weeklyButton;
        private Button _challengeButton;
        private Button _goalButton;

        private QuestData.TYPE_QUEST_GROUP _typeQuestGroup;

        public static UIQuest Create()
        {
            return UIUXML.GetVisualElement<UIQuest>(PATH_UI_UXML);
        }

        public void Initialize()
        {
            _scrollView = this.Q<ScrollView>();
            _exitButton = this.Q<Button>("exit-button");

            _dailyButton = this.Q<Button>("quest-daily-button");
            _weeklyButton = this.Q<Button>("quest-weekly-button");
            _challengeButton = this.Q<Button>("quest-challenge-button");
            _goalButton = this.Q<Button>("quest-goal-button");

            _dailyButton.RegisterCallback<ClickEvent>(e => RefreshTypeQuestData(QuestData.TYPE_QUEST_GROUP.Daily));
            _weeklyButton.RegisterCallback<ClickEvent>(e => RefreshTypeQuestData(QuestData.TYPE_QUEST_GROUP.Weekly));
            _challengeButton.RegisterCallback<ClickEvent>(e => RefreshTypeQuestData(QuestData.TYPE_QUEST_GROUP.Challenge));
            _goalButton.RegisterCallback<ClickEvent>(e => RefreshTypeQuestData(QuestData.TYPE_QUEST_GROUP.Goal));

            _exitButton.RegisterCallback<ClickEvent>(e => { Hide(); });

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
            _scrollView = null;

            _dailyButton.UnregisterCallback<ClickEvent>(e => RefreshTypeQuestData(QuestData.TYPE_QUEST_GROUP.Daily));
            _weeklyButton.UnregisterCallback<ClickEvent>(e => RefreshTypeQuestData(QuestData.TYPE_QUEST_GROUP.Weekly));
            _challengeButton.UnregisterCallback<ClickEvent>(e => RefreshTypeQuestData(QuestData.TYPE_QUEST_GROUP.Challenge));
            _goalButton.UnregisterCallback<ClickEvent>(e => RefreshTypeQuestData(QuestData.TYPE_QUEST_GROUP.Goal));

            _exitButton.UnregisterCallback<ClickEvent>(e => { Hide(); });
        }

        private void ClearDictionary()
        {
            foreach (var value in _dic.Values)
            {
                value.style.display = DisplayStyle.None;
            }
        }

        public void Show()
        {
            this.parent.style.display = DisplayStyle.Flex;
            this.style.display = DisplayStyle.Flex;
        }

        public void Hide()
        {
            this.parent.style.display = DisplayStyle.None;
            this.style.display = DisplayStyle.None;
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
                    _scrollView.Add(line);
                    _dic.Add(key, line);
                }
                _dic[key].RefreshQuestLine(entity);
                _dic[key].style.display = DisplayStyle.Flex;
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

        #endregion


    }




#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    [RequireComponent(typeof(UIDocument))]
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
            var root = UIUXML.GetVisualElement(gameObject, UIQuest.PATH_UI_UXML);
            _instance = root.Q<UIQuest>();
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