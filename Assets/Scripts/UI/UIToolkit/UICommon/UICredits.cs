#if INCLUDE_UI_TOOLKIT
namespace SEF.UI.Toolkit
{
    using UnityEngine;
    using UnityEngine.UIElements;

    [RequireComponent(typeof(UIDocument))]

    public class UICredits : MonoBehaviour
    {

        private readonly string PATH_DEFAULT_SETTING = "Assets/UIToolkit/DefaultPanelSettings.asset";
        private readonly string PATH_UI_CREDITS_UXML = "Assets/Scripts/UI/UICommon/UICreditsUXML.uxml";

        private VisualElement _root = null;

        private Button _exitButton;

        public static UICredits Create()
        {
            var obj = new GameObject();
            obj.name = "UI@Credits";
            return obj.AddComponent<UICredits>();
        }

        public void Initialize()
        {
            _root = UIUXML.GetVisualElement(gameObject, PATH_UI_CREDITS_UXML);

            _exitButton = _root.Q<Button>("exit_button");

            Debug.Assert(_exitButton != null, $"{PATH_UI_CREDITS_UXML} exit_button 을 구성하지 못했습니다");

            _exitButton.RegisterCallback<ClickEvent>(e => OnExitEvent());

            Hide();


        }

        public void CleanUp()
        {
            _closedEvent = null;
            _exitButton = null;
            _root = null;
        }


        public void Show(System.Action closedCallback = null)
        {
            _root.style.display = DisplayStyle.Flex;
            _closedEvent = closedCallback;
        }


        public void Hide()
        {
            _root.style.display = DisplayStyle.None;

            OnClosedEvent();

            _closedEvent = null;
        }

        #region ##### Event #####


        private System.Action _closedEvent;


        private void OnExitEvent()
        {
            Hide();
        }

        private void OnClosedEvent()
        {
            _closedEvent?.Invoke();
        }

        #endregion

    }
}
#endif