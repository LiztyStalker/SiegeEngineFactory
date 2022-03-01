#if INCLUDE_UI_TOOLKIT
namespace SEF.UI.Toolkit
{
    using UnityEngine;
    using UnityEngine.UIElements;

    [RequireComponent(typeof(UIDocument))]
    public class UIPopup : MonoBehaviour
    {

        private readonly string PATH_DEFAULT_SETTING = "Assets/UIToolkit/DefaultPanelSettings.asset";
        private readonly string PATH_UI_POPUP_UXML = "Assets/Scripts/UI/UICommon/UIPopupUXML.uxml";

        private VisualElement _root = null;

        private Label _msgLabel;

        private Button _applyButton;
        private Button _cancelButton;
        private Button _exitButton;


        public static UIPopup Create()
        {
            var obj = new GameObject();
            obj.name = "UI@Popup";
            return obj.AddComponent<UIPopup>();            
        }

        public void Initialize()
        {
            _root = UIUXML.GetVisualElement(gameObject, PATH_UI_POPUP_UXML);

            _msgLabel = _root.Q<Label>("msg_text");
            _applyButton = _root.Q<Button>("apply_button");
            _cancelButton = _root.Q<Button>("cancel_button");
            _exitButton = _root.Q<Button>("exit_button");

            Debug.Assert(_applyButton != null, $"{PATH_UI_POPUP_UXML} apply_button를 구성하지 못했습니다");
            Debug.Assert(_cancelButton != null, $"{PATH_UI_POPUP_UXML} cancel_button를 구성하지 못했습니다");
            Debug.Assert(_exitButton != null, $"{PATH_UI_POPUP_UXML} exit_button을 구성하지 못했습니다");

            _applyButton.RegisterCallback<ClickEvent>(e => OnApplyEvent());
            _cancelButton.RegisterCallback<ClickEvent>(e => OnCancelEvent());
            _exitButton.RegisterCallback<ClickEvent>(e => OnExitEvent());

            _applyButton.style.display = DisplayStyle.None;
            _cancelButton.style.display = DisplayStyle.None;
            _exitButton.style.display = DisplayStyle.None;

            Hide();

        }

        public void CleanUp()
        {
            _applyEvent = null;
            _cancelEvent = null;
            _closedEvent = null;

            _applyButton.UnregisterCallback<ClickEvent>(e => OnApplyEvent());
            _cancelButton.UnregisterCallback<ClickEvent>(e => OnCancelEvent());
            _exitButton.UnregisterCallback<ClickEvent>(e => OnExitEvent());
            _applyButton = null;
            _cancelButton = null;
            _exitButton = null;
            _root = null;
        }


        public void ShowPopup(string msg, System.Action closedCallback = null)
        {
            SetPopup(msg, closedCallback);
            _exitButton.style.display = DisplayStyle.Flex;
            Activate();
        }

        public void ShowPopup(string msg, string applyText, System.Action applyCallback = null, System.Action closedCallback = null)
        {
            SetPopup(msg, applyText, applyCallback, closedCallback);
            _applyButton.style.display = DisplayStyle.Flex;
            _exitButton.style.display = DisplayStyle.None;
            Activate();
        }
        public void ShowPopup(string msg, string applyText, string cancelText, System.Action applyCallback = null, System.Action cancelCallback = null, System.Action closedCallback = null)
        {
            SetPopup(msg, applyText, cancelText, applyCallback, cancelCallback, closedCallback);
            _applyButton.style.display = DisplayStyle.Flex;
            _cancelButton.style.display = DisplayStyle.Flex;
            _exitButton.style.display = DisplayStyle.None;
            Activate();
        }

        private void SetPopup(string msg, System.Action closedCallback = null)
        {
            _msgLabel.text = msg;
            _closedEvent = closedCallback;
        }

        private void SetPopup(string msg, string applyText, System.Action applyCallback = null, System.Action closedCallback = null)
        {
            _applyButton.text = applyText;
            _applyEvent = applyCallback;
            SetPopup(msg, closedCallback);
        }

        private void SetPopup(string msg, string applyText, string cancelText, System.Action applyCallback = null, System.Action cancelCallback = null, System.Action closedCallback = null)
        {
            _cancelButton.text = cancelText;
            _cancelEvent = cancelCallback;
            SetPopup(msg, applyText, applyCallback, closedCallback);
        }

        private void Activate()
        {
            _root.style.display = DisplayStyle.Flex;
        }


        #region ##### Event #####

        private System.Action _applyEvent;
        private System.Action _cancelEvent;
        private System.Action _closedEvent;
        private void OnApplyEvent()
        {
            _applyEvent?.Invoke();
            Hide();
        }

        private void OnCancelEvent()
        {
            _cancelEvent?.Invoke();
            Hide();
        }

        private void OnExitEvent()
        {
            Hide();
        }

        private void OnClosedEvent()
        {
            _closedEvent?.Invoke();
        }

        #endregion

        public void Hide()
        {
            _applyButton.style.display = DisplayStyle.None;
            _cancelButton.style.display = DisplayStyle.None;
            _exitButton.style.display = DisplayStyle.None;

            _root.style.display = DisplayStyle.None;

            OnClosedEvent();

            _applyEvent = null;
            _cancelEvent = null;
            _closedEvent = null;

        }

    }
}
#endif