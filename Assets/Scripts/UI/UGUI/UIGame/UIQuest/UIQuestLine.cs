namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using SEF.Entity;

    public class UIQuestLine : MonoBehaviour
    {

        [SerializeField]
        private GameObject _activatePanel;

        [SerializeField]
        private Image _icon;

        [SerializeField]
        private Text _contentLabel;

        [SerializeField]
        private Slider _slider;

        [SerializeField]
        private Text _sliderText;

        [SerializeField]
        private Button _rewardButton;

        [SerializeField]
        private Image _rewardAssetIcon;

        [SerializeField]
        private Text _rewardValueLabel;

        [SerializeField]
        private GameObject _rewardedPanel;

        [SerializeField]
        private Text _rewardedLabel;

        private string _questKey;
        private string _addressKey;
        private bool _hasGoal = false;

        public static UIQuestLine Create()
        {
            var obj = new GameObject();
            obj.name = "UI@QuestLine";
            return obj.AddComponent<UIQuestLine>();
        }

        public void Initialize()
        {

            Debug.Assert(_activatePanel != null, "_activatePanel element 를 찾지 못했습니다");
            Debug.Assert(_icon != null, "icon element 를 찾지 못했습니다");

            Debug.Assert(_contentLabel != null, "_contentLabel element 를 찾지 못했습니다");

            Debug.Assert(_slider != null, "_slider element 를 찾지 못했습니다");

            Debug.Assert(_rewardButton != null, "_rewardButton element 를 찾지 못했습니다");
            Debug.Assert(_rewardAssetIcon != null, "_rewardAssetIcon element 를 찾지 못했습니다");
            Debug.Assert(_rewardValueLabel != null, "_rewardValueLabel element 를 찾지 못했습니다");

            Debug.Assert(_rewardedPanel != null, "_rewardedPanel element 를 찾지 못했습니다");


            //_icon
            _contentLabel.text = "설명";

            _rewardButton.onClick.AddListener(OnRewardClickedEvent);

            _activatePanel.SetActive(true);
            _activatePanel.SetActive(false);
        }


        public void RefreshQuestLine(QuestEntity entity)
        {
            _questKey = entity.Key;
            _addressKey = entity.AddressKey;
            _hasGoal = entity.HasQuestGoal();

            if (entity.HasRewarded)
            {
                _rewardedPanel.SetActive(true);
                _rewardButton.interactable = false;
                _rewardedLabel.text = "완료";
            }
            else
            {
                _rewardedPanel.SetActive(false);
                _rewardButton.interactable = (!string.IsNullOrEmpty(_addressKey));

                bool isRewardLabel = (_hasGoal || string.IsNullOrEmpty(_addressKey));
                _rewardedLabel.text = (isRewardLabel) ? "보상" : "이동";
            }

            _contentLabel.text = entity.Key;

            _sliderText.text = $"{entity.NowValue} / {entity.GoalValue}";

        }

        public void CleanUp()
        {
            _rewardButton.onClick.RemoveAllListeners();
            _icon = null;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }



#region ##### Listener #####


        private System.Action<string, string, bool> _rewardEvent;
        public void AddOnRewardListener(System.Action<string, string, bool> act) => _rewardEvent += act;
        public void RemoveOnRewardListener(System.Action<string, string, bool> act) => _rewardEvent -= act;
        private void OnRewardClickedEvent()
        {
            _rewardEvent?.Invoke(_questKey, _addressKey, _hasGoal);
        }

#endregion
    }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
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
            _instance = UIQuestLine.Create();
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