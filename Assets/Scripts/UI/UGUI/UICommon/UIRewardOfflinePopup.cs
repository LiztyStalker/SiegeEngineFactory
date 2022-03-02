namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UIRewardOfflinePopup : MonoBehaviour
    {
        private readonly static string UGUI_NAME = "UI@RewardOfflinePopup";

        [SerializeField]
        private Text _msgLabel;

        [SerializeField]
        private Text _timeLabel;

        [SerializeField]
        private Button _rewardButton;

        [SerializeField]
        private Button _advertisementButton;


        public static UIRewardOfflinePopup Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UIRewardOfflinePopup>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.name = "UI@RewardOfflinePopup";
                return obj.AddComponent<UIRewardOfflinePopup>();
            }
#else
            Debug.LogWarning($"{UGUI_NAME}을 찾을 수 없습니다");
            return null;
#endif

        }

        public void Initialize()
        {
            Debug.Assert(_msgLabel != null, $"_msgLabel 을 구성하지 못했습니다");
            Debug.Assert(_timeLabel != null, $"_timeLabel 을 구성하지 못했습니다");
            Debug.Assert(_rewardButton != null, $"_rewardButton 구성하지 못했습니다");
            Debug.Assert(_advertisementButton != null, $"_advertisementButton 구성하지 못했습니다");

            _rewardButton.onClick.AddListener(OnRewardEvent);
            _advertisementButton.onClick.AddListener(OnAdvertisementEvent);

            Hide();

        }

        public void CleanUp()
        {
            _rewardEvent = null;
            _advertisementEvent = null;

            _rewardButton.onClick.RemoveListener(OnRewardEvent);
            _advertisementButton.onClick.RemoveListener(OnAdvertisementEvent);
        }


        public void ShowRewardOfflinePopup(string msg, string reward, string advertisement, System.Action rewardCallback, System.Action advertisementCallback)
        {
            SetPopup(msg, reward, advertisement, rewardCallback, advertisementCallback);
            Activate();
        }

        private void SetPopup(string msg, string reward, string advertisement, System.Action rewardCallback, System.Action advertisementCallback)
        {
            _msgLabel.text = msg;
            _rewardButton.GetComponentInChildren<Text>().text = reward;
            _rewardEvent = rewardCallback;
            _advertisementButton.GetComponentInChildren<Text>().text = advertisement;
            _advertisementEvent = advertisementCallback;
        }

        private void Activate()
        {
            gameObject.SetActive(true);
        }


#region ##### Event #####

        private System.Action _rewardEvent;
        private System.Action _advertisementEvent;
        private void OnRewardEvent()
        {
            _rewardEvent?.Invoke();
            Hide();
        }

        private void OnAdvertisementEvent()
        {
            _advertisementEvent?.Invoke();
            Hide();
        }

#endregion

        public void Hide()
        {
            gameObject.SetActive(false);

            _rewardEvent = null;
            _advertisementEvent = null;

        }

    }
}