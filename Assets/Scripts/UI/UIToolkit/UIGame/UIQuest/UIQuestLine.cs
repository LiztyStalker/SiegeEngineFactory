#if INCLUDE_UI_TOOLKIT
namespace SEF.UI.Toolkit
{
    using UnityEngine;
    using UnityEngine.UIElements;
    using SEF.Entity;

    public class UIQuestLine : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<UIQuestLine, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UIQuest/UIQuestLine.uxml";

        internal static readonly string PATH_UI_USS = "Assets/Scripts/UI/UIGame/UIQuest/UIQuestLine.uss";

        private VisualElement _activatePanel;

        private VisualElement _icon;

        private Label _contentLabel;

        private UIFillable _fillable;

        private Button _rewardButton;
        private VisualElement _rewardAssetIcon;
        private Label _rewardValueLabel;

        private VisualElement _rewardedPanel;
        private Label _rewardedLabel;

        private string _questKey;
        private string _addressKey;
        private bool _hasGoal = false;

        public static UIQuestLine Create()
        {
            return UIUXML.GetVisualElement<UIQuestLine>(PATH_UI_UXML);
        }

        public void Initialize()
        {
            _activatePanel = this.Q<VisualElement>("quest-activate-panel");

            _icon = this.Q<VisualElement>("line-icon");

            _contentLabel = this.Q<Label>("content-label");

            _fillable = this.Q<UIFillable>();

            _rewardButton = this.Q<Button>("reward-button");
            _rewardAssetIcon = this.Q<VisualElement>("reward-asset-icon");
            _rewardValueLabel = this.Q<Label>("reward-asset-value-label");
            _rewardedLabel = this.Q<Label>("reward-label");

            _rewardedPanel = this.Q<VisualElement>("quest-rewarded-panel");

            Debug.Assert(_activatePanel != null, "_activatePanel element 를 찾지 못했습니다");
            Debug.Assert(_icon != null, "icon element 를 찾지 못했습니다");

            Debug.Assert(_contentLabel != null, "_contentLabel element 를 찾지 못했습니다");

            Debug.Assert(_fillable != null, "_fillable element 를 찾지 못했습니다");

            Debug.Assert(_rewardButton != null, "_rewardButton element 를 찾지 못했습니다");
            Debug.Assert(_rewardAssetIcon != null, "_rewardAssetIcon element 를 찾지 못했습니다");
            Debug.Assert(_rewardValueLabel != null, "_rewardValueLabel element 를 찾지 못했습니다");

            Debug.Assert(_rewardedPanel != null, "_rewardedPanel element 를 찾지 못했습니다");


            //_icon
            _contentLabel.text = "설명";

            _rewardButton.RegisterCallback<ClickEvent>(OnRewardClickedEvent);

            _activatePanel.style.display = DisplayStyle.Flex;
            _rewardedPanel.style.display = DisplayStyle.None;
            _fillable.DisplayStyle = DisplayStyle.Flex;
        }


        public void RefreshQuestLine(QuestEntity entity)
        {
            _questKey = entity.Key;
            _addressKey = entity.AddressKey;
            _hasGoal = entity.HasQuestGoal();

            if (entity.HasRewarded)
            {
                _rewardedPanel.style.display = DisplayStyle.Flex;
                _rewardButton.SetEnabled(false);
                _rewardedLabel.text = "완료";
            }
            else
            {
                _rewardedPanel.style.display = DisplayStyle.None;
                _rewardButton.SetEnabled(!string.IsNullOrEmpty(_addressKey));

                bool isRewardLabel = (_hasGoal || string.IsNullOrEmpty(_addressKey));
                _rewardedLabel.text = (isRewardLabel) ? "보상" : "이동";
            }

            _contentLabel.text = entity.Key;
            _fillable.SetLabel($"{entity.NowValue} / {entity.GoalValue}");

        }

        public void CleanUp()
        {
            _rewardButton.UnregisterCallback<ClickEvent>(OnRewardClickedEvent);
            _icon = null;
        }



#region ##### Listener #####


        private System.Action<string, string, bool> _rewardEvent;
        public void AddOnRewardListener(System.Action<string, string, bool> act) => _rewardEvent += act;
        public void RemoveOnRewardListener(System.Action<string, string, bool> act) => _rewardEvent -= act;
        private void OnRewardClickedEvent(ClickEvent e)
        {
            _rewardEvent?.Invoke(_questKey, _addressKey, _hasGoal);
        }

#endregion
    }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    [RequireComponent(typeof(UIDocument))]
    public class UIQuestLine_Test : MonoBehaviour
    {
        private UIQuestLine _instance;
        public UIQuestLine Instance => _instance;

        public static UIQuestLine_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UIQuestLine_Test";
            return obj.AddComponent<UIQuestLine_Test>();
        }

        public void Initialize()
        {
            var root = UIUXML.GetVisualElement(gameObject, UIQuestLine.PATH_UI_UXML);
            _instance = root.Q<UIQuestLine>();
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
#endif