namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UICredits : MonoBehaviour
    {
        [SerializeField]
        private Button _exitButton;

        public static UICredits Create()
        {
            var obj = new GameObject();
            obj.AddComponent<Canvas>();
            obj.name = "UI@Credits";
            return obj.AddComponent<UICredits>();
        }

        public void Initialize()
        {
            Debug.Assert(_exitButton != null, $"_exitButton 을 구성하지 못했습니다");
            _exitButton.onClick.AddListener(OnExitEvent);
            Hide();


        }

        public void CleanUp()
        {
            _closedEvent = null;
            _exitButton = null;
        }


        public void Show(System.Action closedCallback = null)
        {
            _closedEvent = closedCallback;
            gameObject.SetActive(true);
        }


        public void Hide()
        {
            OnClosedEvent();
            _closedEvent = null;
            gameObject.SetActive(false);
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
