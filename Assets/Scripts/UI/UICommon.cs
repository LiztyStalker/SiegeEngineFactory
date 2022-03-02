namespace SEF.UI
{

#if INCLUDE_UI_TOOLKIT
    using Toolkit;
#endif

    using UnityEngine;

    public class UICommon : MonoBehaviour
    {
        private readonly static string UGUI_NAME = "UI@Common";

        private static UICommon _current;


        public static UICommon Current
        {
            get
            {
                if(_current == null)
                {
                    _current = Create();
                    _current.Initialize();
                }
                return _current;
            }
        }

        private UIPopup _uiPopup;
        private UICredits _uiCredits;
        private UISettings _uiSettings;
        private UIRewardOfflinePopup _uiRewardOffline;
        private static UICommon Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UICommon>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.name = "UI@Common";
                return obj.AddComponent<UICommon>();
            }
#else
            Debug.LogWarning($"{UGUI_NAME}을 찾을 수 없습니다");               
            return null;
#endif
        }

        public void Initialize()
        {

            _uiPopup = GetComponentInChildren<UIPopup>(true);

            if (_uiPopup == null)
            {
                _uiPopup = UIPopup.Create();
                _uiPopup.transform.SetParent(transform);
            }

            _uiPopup.Initialize();



            _uiCredits = GetComponentInChildren<UICredits>(true);

            if (_uiCredits == null)
            {
                _uiCredits = UICredits.Create();
                _uiCredits.transform.SetParent(transform);
            }
            _uiCredits.Initialize();


            _uiSettings = GetComponentInChildren<UISettings>(true);

            if (_uiSettings == null)
            {
                _uiSettings = UISettings.Create();
                _uiSettings.transform.SetParent(transform);
            }
            _uiSettings.Initialize();


            _uiRewardOffline = GetComponentInChildren<UIRewardOfflinePopup>();
            if (_uiRewardOffline == null)
            {
                _uiRewardOffline = UIRewardOfflinePopup.Create();
                _uiRewardOffline.transform.SetParent(transform);
            }
            _uiRewardOffline.Initialize();

        }


        public void CleanUp()
        {
            _uiPopup.CleanUp();
            _uiSettings.CleanUp();
            _uiCredits.CleanUp();
            _uiRewardOffline.CleanUp();
            _current = null;
        }





        public void ShowPopup(string msg, System.Action closedCallback = null)
        {
            _uiPopup.ShowPopup(msg, closedCallback);
        }


        public void ShowPopup(string msg, string applyText, System.Action applyCallback = null, System.Action closedCallback = null)
        {
            _uiPopup.ShowPopup(msg, applyText, applyCallback, closedCallback);
        }

        public void ShowPopup(string msg, string applyText, string cancelText, System.Action applyCallback = null, System.Action cancelCallback = null, System.Action closedCallback = null)
        {
            _uiPopup.ShowPopup(msg, applyText, cancelText, applyCallback, cancelCallback, closedCallback);
        }

        public void ShowSettings(System.Action closedCallback = null)
        {
            _uiSettings.Show(closedCallback);
        }

        public void ShowCredits(System.Action closedCallback = null)
        {
            _uiCredits.Show(closedCallback);
        }

        public void ShowRewardOffline(string msg, string reward, string advertisement, System.Action rewardCallback, System.Action advertisementCallback)
        {
            _uiRewardOffline.ShowRewardOfflinePopup(msg, reward, advertisement, rewardCallback, advertisementCallback);
        }

    }
}