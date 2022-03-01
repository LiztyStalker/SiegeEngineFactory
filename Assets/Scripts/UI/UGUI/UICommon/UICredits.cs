namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UICredits : MonoBehaviour
    {

        private readonly static string UGUI_NAME = "UI@Credits";

        [SerializeField]
        private Button _exitButton;
        public static UICredits Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UICredits>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.AddComponent<Canvas>();
                obj.name = "UI@Credits";
                return obj.AddComponent<UICredits>();
            }
#else
            Debug.LogWarning($"{UGUI_NAME}을 찾을 수 없습니다");
#endif

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
