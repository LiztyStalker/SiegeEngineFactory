namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using SEF.Entity;

    public class UIQuestLine : MonoBehaviour
    {
        private readonly static string UGUI_NAME = "UI@QuestLine";

        [SerializeField]
        private GameObject _activatePanel;

//        [SerializeField]
        //private Image _icon;

        [SerializeField]
        private Text _contentLabel;

        [SerializeField]
        private Slider _slider;

        [SerializeField]
        private Text _sliderText;

        [SerializeField]
        private UIAssetButton _rewardButton;

        [SerializeField]
        private GameObject _rewardedPanel;

        private string _questKey;
        private string _addressKey;
        private bool _hasGoal = false;


        public static UIQuestLine Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UIQuestLine>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.name = UGUI_NAME;
                return obj.AddComponent<UIQuestLine>();
            }
#else
            Debug.LogWarning($"{UGUI_NAME}을 찾을 수 없습니다");
            return null;
#endif
        }


        public void Initialize()
        {

            Debug.Assert(_activatePanel != null, "_activatePanel element 를 찾지 못했습니다");
            //Debug.Assert(_icon != null, "icon element 를 찾지 못했습니다");

            Debug.Assert(_contentLabel != null, "_contentLabel element 를 찾지 못했습니다");

            Debug.Assert(_slider != null, "_slider element 를 찾지 못했습니다");

            Debug.Assert(_rewardButton != null, "_rewardButton element 를 찾지 못했습니다");

            Debug.Assert(_rewardedPanel != null, "_rewardedPanel element 를 찾지 못했습니다");


            //_icon
            _contentLabel.text = "설명";

            _rewardButton.onClick.AddListener(OnRewardClickedEvent);

            _activatePanel.SetActive(true);
            _rewardedPanel.SetActive(false);
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
                _rewardButton.SetLabel("완료");
                _rewardButton.SetEmpty();
            }
            else
            {
                _rewardedPanel.SetActive(false);
                _rewardButton.interactable = (!string.IsNullOrEmpty(_addressKey));

                bool isRewardLabel = (_hasGoal || string.IsNullOrEmpty(_addressKey));
                _rewardButton.SetLabel((isRewardLabel) ? "보상" : "이동");
                _rewardButton.SetData(entity.GetRewardAssetData());
            }

            _contentLabel.text = entity.Key;

            _sliderText.text = $"{entity.NowValue} / {entity.GoalValue}";
            _slider.value = (float)entity.NowValue / (float)entity.GoalValue;

        }

        public void CleanUp()
        {
            _rewardButton.onClick.RemoveAllListeners();
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
}