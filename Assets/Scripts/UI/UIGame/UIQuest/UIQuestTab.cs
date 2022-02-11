namespace SEF.UI.Toolkit
{
    using UnityEngine;
    using UnityEngine.UIElements;
    using SEF.Entity;
    using SEF.Data;

    public class UIQuestTab : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<UIQuestTab, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UIQuest/UIQuestTab.uxml";

        internal static readonly string PATH_UI_USS = "Assets/Scripts/UI/UIGame/UIQuest/UIQuestTab.uss";

        //private VisualElement _icon;

        private Label _contentLabel;

        private UIFillable _fillable;

        private Button _rewardButton;

        private string _questKey;
        private string _addressKey;
        private bool _hasGoal;

        public static UIQuestTab Create()
        {
            return UIUXML.GetVisualElement<UIQuestTab>(PATH_UI_UXML);
        }
        public void Initialize()
        {            
            //_icon = this.Q<VisualElement>("quest-tab-line-icon");
            _contentLabel = this.Q<Label>("quest-tab-content-label");
            _fillable = this.Q<UIFillable>();
            _rewardButton = this.Q<Button>("quest-tab-reward-button");

            //Debug.Assert(_icon != null, "icon element 를 찾지 못했습니다");
            Debug.Assert(_contentLabel != null, "_contentLabel element 를 찾지 못했습니다");
            Debug.Assert(_fillable != null, "_fillable element 를 찾지 못했습니다");
            Debug.Assert(_rewardButton != null, "_rewardButton element 를 찾지 못했습니다");

            ////_icon
            _rewardButton.RegisterCallback<ClickEvent>(OnRewardClickedEvent);
            _fillable.DisplayStyle = DisplayStyle.Flex;
        }
        public void CleanUp()
        {
            _rewardButton.UnregisterCallback<ClickEvent>(OnRewardClickedEvent);
            //_icon = null;
        }

        public void RefreshQuest(QuestEntity entity)
        {
            _questKey = entity.Key;
            _addressKey = entity.AddressKey;
            _hasGoal = entity.HasQuestGoal();
            _rewardButton.text = (_hasGoal) ? "보상" : "이동";
            _contentLabel.text = entity.Key;
            _fillable.SetLabel($"{entity.NowValue} / {entity.GoalValue}");
        }


        #region ##### Listener #####


        private System.Action<QuestData.TYPE_QUEST_GROUP, string, string, bool> _rewardEvent;
        public void AddOnRewardListener(System.Action<QuestData.TYPE_QUEST_GROUP, string, string, bool> act) => _rewardEvent += act;
        public void RemoveOnRewardListener(System.Action<QuestData.TYPE_QUEST_GROUP, string, string, bool> act) => _rewardEvent -= act;
        private void OnRewardClickedEvent(ClickEvent e)
        {
            //바로가기
            //보상받기
            _rewardEvent?.Invoke(QuestData.TYPE_QUEST_GROUP.Goal, _questKey, _addressKey, _hasGoal);
        }

        #endregion
    }
}