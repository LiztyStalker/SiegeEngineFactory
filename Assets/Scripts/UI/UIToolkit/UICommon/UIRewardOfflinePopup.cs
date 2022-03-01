namespace SEF.UI.Toolkit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Storage;

    [RequireComponent(typeof(UIDocument))]
    public class UIRewardOfflinePopup : MonoBehaviour
    {

        private readonly string PATH_DEFAULT_SETTING = "Assets/UIToolkit/DefaultPanelSettings.asset";
        private readonly string PATH_UI_UXML = "Assets/Scripts/UI/UICommon/UIRewardOfflinePopup.uxml";
        private readonly string PATH_UI_USS = "Assets/Scripts/UI/UICommon/UIRewardOfflinePopup.uss";

        private VisualElement _root = null;

        private Label _msgLabel;

        private Button _rewardButton;
        private Button _advertisementButton;


        public static UIRewardOfflinePopup Create()
        {
            var obj = new GameObject();
            obj.name = "UI@RewardOffline";
            return obj.AddComponent<UIRewardOfflinePopup>();            
        }

        public void Initialize()
        {
            _root = UIUXML.GetVisualElement(gameObject, PATH_UI_UXML, PATH_UI_USS);

            _msgLabel = _root.Q<Label>("msg_text");
            _rewardButton = _root.Q<Button>("reward_button");
            _advertisementButton = _root.Q<Button>("advertisement_button");
            //_exitButton = _root.Q<Button>("exit_button");

            Debug.Assert(_rewardButton != null, $"{PATH_UI_UXML} _rewardButton 구성하지 못했습니다");
            Debug.Assert(_advertisementButton != null, $"{PATH_UI_UXML} _advertisementButton 구성하지 못했습니다");
            //Debug.Assert(_exitButton != null, $"{PATH_UI_POPUP_UXML} exit_button을 구성하지 못했습니다");

            _rewardButton.RegisterCallback<ClickEvent>(e => OnRewardEvent());
            _advertisementButton.RegisterCallback<ClickEvent>(e => OnAdvertisementEvent());
            //_exitButton.RegisterCallback<ClickEvent>(e => OnExitEvent());

            _rewardButton.style.display = DisplayStyle.Flex;
            _advertisementButton.style.display = DisplayStyle.Flex;
            //_exitButton.style.display = DisplayStyle.None;

            Hide();

        }

        public void CleanUp()
        {
            _rewardEvent = null;
            _advertisementEvent = null;

            _rewardButton.UnregisterCallback<ClickEvent>(e => OnRewardEvent());
            _advertisementButton.UnregisterCallback<ClickEvent>(e => OnAdvertisementEvent());
            //_exitButton.UnregisterCallback<ClickEvent>(e => OnExitEvent());
            _rewardButton = null;
            _advertisementButton = null;
            //_exitButton = null;
            _root = null;
        }


        public void ShowRewardOfflinePopup(string msg, string reward, string advertisement, System.Action rewardCallback, System.Action advertisementCallback)
        {
            SetPopup(msg, reward, advertisement, rewardCallback, advertisementCallback);
            //_exitButton.style.display = DisplayStyle.Flex;
            Activate();
        }

        private void SetPopup(string msg, string reward, string advertisement, System.Action rewardCallback, System.Action advertisementCallback)
        {
            _msgLabel.text = msg;
            _rewardButton.text = reward;
            _rewardEvent = rewardCallback;
            _advertisementButton.text = advertisement;
            _advertisementEvent = advertisementCallback;
        }

        private void Activate()
        {
            _root.style.display = DisplayStyle.Flex;
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
            _root.style.display = DisplayStyle.None;

            _rewardEvent = null;
            _advertisementEvent = null;

        }

    }
}