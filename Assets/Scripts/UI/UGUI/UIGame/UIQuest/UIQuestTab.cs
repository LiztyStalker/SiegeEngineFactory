
namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using SEF.Entity;
    using SEF.Data;

    public class UIQuestTab : MonoBehaviour
    {

        private readonly static string UGUI_NAME = "UI@QuestTab";

        [SerializeField]
        private Text _contentLabel;

        [SerializeField]
        private Slider _slider;

        [SerializeField]
        private Text _sliderText;

        [SerializeField]
        private Button _rewardButton;

        private string _questKey;
        private string _addressKey;
        private bool _hasGoal;

        public static UIQuestTab Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UIQuestTab>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.name = UGUI_NAME;
                return obj.AddComponent<UIQuestTab>();
            }
#else
            Debug.LogWarning($"{UGUI_NAME}�� ã�� �� �����ϴ�");
            return null;
#endif
        }

        public void Initialize()
        {            
            Debug.Assert(_contentLabel != null, "_contentLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_slider != null, "_slider element �� ã�� ���߽��ϴ�");
            Debug.Assert(_rewardButton != null, "_rewardButton element �� ã�� ���߽��ϴ�");

            _rewardButton.onClick.AddListener(OnRewardClickedEvent);

        }
        public void CleanUp()
        {
            _rewardButton.onClick.RemoveListener(OnRewardClickedEvent);
        }

        public void RefreshQuest(QuestEntity entity)
        {
            if (entity.HasRewarded)
            {
                _hasGoal = false;
                _contentLabel.text = "";
                _rewardButton.GetComponentInChildren<Text>().text = "�Ϸ�";
                _rewardButton.interactable = false;
                _sliderText.text = $"-";
                _slider.value = 1f;
            }
            else
            {
                _questKey = entity.Key;
                _addressKey = entity.AddressKey;
                _hasGoal = entity.HasQuestGoal();

                bool isRewardLabel = (_hasGoal || string.IsNullOrEmpty(_addressKey));
                _rewardButton.GetComponentInChildren<Text>().text = (isRewardLabel) ? "����" : "�̵�";
                _rewardButton.interactable = (_hasGoal || !string.IsNullOrEmpty(entity.AddressKey));
                _contentLabel.text = entity.Key;
                _sliderText.text = $"{entity.NowValue} / {entity.GoalValue}";
                _slider.value = (float)entity.NowValue / (float)entity.GoalValue;
            }
        }


#region ##### Listener #####

        private System.Action<QuestData.TYPE_QUEST_GROUP, string, string, bool> _rewardEvent;
        public void AddOnRewardListener(System.Action<QuestData.TYPE_QUEST_GROUP, string, string, bool> act) => _rewardEvent += act;
        public void RemoveOnRewardListener(System.Action<QuestData.TYPE_QUEST_GROUP, string, string, bool> act) => _rewardEvent -= act;
        private void OnRewardClickedEvent()
        {
            //�ٷΰ���
            //����ޱ�
            _rewardEvent?.Invoke(QuestData.TYPE_QUEST_GROUP.Goal, _questKey, _addressKey, _hasGoal);
        }

#endregion
    }
}