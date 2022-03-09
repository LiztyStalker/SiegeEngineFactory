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
            Debug.LogWarning($"{UGUI_NAME}�� ã�� �� �����ϴ�");
            return null;
#endif
        }


        public void Initialize()
        {

            Debug.Assert(_activatePanel != null, "_activatePanel element �� ã�� ���߽��ϴ�");
            //Debug.Assert(_icon != null, "icon element �� ã�� ���߽��ϴ�");

            Debug.Assert(_contentLabel != null, "_contentLabel element �� ã�� ���߽��ϴ�");

            Debug.Assert(_slider != null, "_slider element �� ã�� ���߽��ϴ�");

            Debug.Assert(_rewardButton != null, "_rewardButton element �� ã�� ���߽��ϴ�");

            Debug.Assert(_rewardedPanel != null, "_rewardedPanel element �� ã�� ���߽��ϴ�");


            //_icon
            _contentLabel.text = "����";

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
                _rewardButton.SetLabel("�Ϸ�");
                _rewardButton.SetEmpty();
            }
            else
            {
                _rewardedPanel.SetActive(false);
                _rewardButton.interactable = (!string.IsNullOrEmpty(_addressKey));

                bool isRewardLabel = (_hasGoal || string.IsNullOrEmpty(_addressKey));
                _rewardButton.SetLabel((isRewardLabel) ? "����" : "�̵�");
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