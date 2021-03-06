namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UISettings : MonoBehaviour
    {
        private readonly static string UGUI_NAME = "UI@Settings";

        private readonly string SETTINGS_BGM_KEY = "BGM_VALUE";
        private readonly string SETTINGS_SFX_KEY = "SFX_VALUE";
        private readonly string SETTINGS_FRAME_KEY = "FRAME_VALUE";
        private readonly string SETTINGS_HIT_KEY = "ISHIT";
        private readonly string SETTINGS_EFFECT_KEY = "ISEFFECT";

        [SerializeField]
        private Text _versionLabel;

        [SerializeField]
        private Slider _bgmSlider;
        [SerializeField]
        private Text _bgmVolumeLabel;

        [SerializeField]
        private Slider _sfxSlider;
        [SerializeField]
        private Text _sfxVolumeLabel;

        [SerializeField]
        private Slider _frameSlider;
        [SerializeField]
        private Text _frameValueLabel;

        [SerializeField]
        private Button _langLeftButton;
        [SerializeField]
        private Button _langRightButton;
        [SerializeField]
        private Text _langLabel;

        [SerializeField]
        private Toggle _uiHitActivateToggle;
        [SerializeField]
        private Toggle _effectActivateToggle;

        [SerializeField]
        private Button _saveButton;
        [SerializeField]
        private Button _loadButton;

        [SerializeField]
        private Button _gpgsButton;
        [SerializeField]
        private Button _qnaButton;

        [SerializeField]
        private Button _exitButton;

        public static UISettings Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UISettings>());
            }
#if UNITY_EDITOR
            else
            { 
                var obj = new GameObject();
                obj.name = "UI@Settings";
                return obj.AddComponent<UISettings>();
            }
#else
            Debug.LogWarning($"{UGUI_NAME}?? ???? ?? ????????");
            return null;
#endif

        }

        public void Initialize()
        {

            Debug.Assert(_versionLabel != null, $"_versionLabel ?? ???????? ??????????");
            Debug.Assert(_bgmSlider != null, $"_bgmSlider ?? ???????? ??????????");
            Debug.Assert(_bgmVolumeLabel != null, $"_bgmVolumeLabel ???????? ??????????");
            Debug.Assert(_sfxSlider != null, $"_sfxSlider ???????? ??????????");
            Debug.Assert(_sfxVolumeLabel != null, $"_sfxVolumeLabel ?? ???????? ??????????");
            Debug.Assert(_frameSlider != null, $"_frameSlider ?? ???????? ??????????");
            Debug.Assert(_frameValueLabel != null, $"_frameValueLabel ???????? ??????????");
            Debug.Assert(_langLeftButton != null, $"_langLeftButton ???????? ??????????");
            Debug.Assert(_langRightButton != null, $"_langRightButton ?? ???????? ??????????");
            Debug.Assert(_langLabel != null, $"_langLabel ?? ???????? ??????????");
            Debug.Assert(_uiHitActivateToggle != null, $"_uiHitActivateToggle ???????? ??????????");
            Debug.Assert(_effectActivateToggle != null, $"_effectActivateToggle ???????? ??????????");
            Debug.Assert(_saveButton != null, $"_saveButton ???????? ??????????");
            Debug.Assert(_loadButton != null, $"_loadButton ???????? ??????????");
            Debug.Assert(_gpgsButton != null, $"_gpgsButton ???????? ??????????");
            Debug.Assert(_qnaButton != null, $"_qnaButton ???????? ??????????");
            Debug.Assert(_exitButton != null, $"_exitButton ???????? ??????????");

            Load();

            _versionLabel.text = Application.version;

            RegisterEvents();

            Hide();

        }

        public void CleanUp()
        {
            _closedEvent = null;

            UnRegisterEvents();
        }


        private void OnEnable()
        {
            Storage.TranslateStorage.Instance.AddOnChangedTranslateListener(SetText);
        }

        public void OnDisable()
        {
            Storage.TranslateStorage.Instance.RemoveOnChangedTranslateListener(SetText);
        }

        private void RegisterEvents()
        {
            _bgmSlider.onValueChanged.AddListener(OnBGMEvent);
            _sfxSlider.onValueChanged.AddListener(OnSFXEvent);
            _frameSlider.onValueChanged.AddListener(OnFrameEvent);

            _langLeftButton.onClick.AddListener(OnLeftLanguageEvent);
            _langRightButton.onClick.AddListener(OnRightLanguageEvent);


            _uiHitActivateToggle.onValueChanged.AddListener(OnHitToggleEvent);
            _effectActivateToggle.onValueChanged.AddListener(OnEffectToggleEvent);


            _saveButton.onClick.AddListener(OnSaveEvent);
            _loadButton.onClick.AddListener(OnLoadEvent);
            _gpgsButton.onClick.AddListener(OnGPGSEvent);
            _qnaButton.onClick.AddListener(OnQnAEvent);

            _exitButton.onClick.AddListener(OnExitEvent);
        }

        private void UnRegisterEvents()
        {
            _bgmSlider.onValueChanged.RemoveListener(OnBGMEvent);
            _sfxSlider.onValueChanged.RemoveListener(OnSFXEvent);
            _frameSlider.onValueChanged.RemoveListener(OnFrameEvent);

            _langLeftButton.onClick.RemoveListener(OnLeftLanguageEvent);
            _langRightButton.onClick.RemoveListener(OnRightLanguageEvent);


            _uiHitActivateToggle.onValueChanged.RemoveListener(OnHitToggleEvent);
            _effectActivateToggle.onValueChanged.RemoveListener(OnEffectToggleEvent);


            _saveButton.onClick.RemoveListener(OnSaveEvent);
            _loadButton.onClick.RemoveListener(OnLoadEvent);
            _gpgsButton.onClick.RemoveListener(OnGPGSEvent);
            _qnaButton.onClick.RemoveListener(OnQnAEvent);

            _exitButton.onClick.RemoveListener(OnExitEvent);
        }

        private void OnBGMEvent(float value)
        {
            _bgmVolumeLabel.text = string.Format("{0:d0}", (value * 100f));
        }

        private void OnSFXEvent(float value)
        {
            _sfxVolumeLabel.text = string.Format("{0:d0}", (value * 100f));
        }

        private void OnFrameEvent(float value)
        {
            _frameValueLabel.text = string.Format("{0:d0}", (value * 100f));
        }

        private void OnLeftLanguageEvent() 
        {
            Storage.TranslateStorage.Instance.PrevLanguageIndex();
        }

        private void OnRightLanguageEvent() 
        {
            Storage.TranslateStorage.Instance.NextLanguageIndex();
        }

        private void SetLanguageLabel()
        {
            _langLabel.text = Storage.TranslateStorage.Instance.GetTranslateData("System_Tr", "Sys_Settings_Language");
        }

        private void SetToggleText()
        {
            _uiHitActivateToggle.GetComponentInChildren<Text>().text = Storage.TranslateStorage.Instance.GetTranslateData("System_Tr", "Sys_Settings_Hit"); ;
            _effectActivateToggle.GetComponentInChildren<Text>().text = Storage.TranslateStorage.Instance.GetTranslateData("System_Tr", "Sys_Settings_Effect"); ;
        }

        private void OnHitToggleEvent(bool isOn) 
        {
            Debug.Log($"OnHitToggleEvent {isOn}");
        }
        private void OnEffectToggleEvent(bool isOn) 
        {
            Debug.Log($"OnEffectToggleEvent {isOn}");
        }

        private void OnSaveEvent() 
        {
            Debug.Log("OnSaveEvent");
        }

        private void OnLoadEvent()
        {
            Debug.Log("OnLoadEvent");
        }

        private void OnGPGSEvent() 
        {
            Debug.Log("OnGPGSEvent");
        }

        private void OnQnAEvent() 
        {
            Debug.Log("OnQnAEvent");
        }


        private void SetText()
        {
            SetLanguageLabel();
            SetToggleText();
        }

        public void Show(System.Action closedCallback = null)
        {
            gameObject.SetActive(true);
            SetText();
            _closedEvent = closedCallback;
        }


        public void Hide()
        {
            gameObject.SetActive(false);

            OnClosedEvent();
            Save();

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


        private void Load()
        {
            _bgmSlider.value = (float)PlayerPrefs.GetInt(SETTINGS_BGM_KEY, 100);
            _bgmVolumeLabel.text = _bgmSlider.value.ToString();

            _sfxSlider.value = (float)PlayerPrefs.GetInt(SETTINGS_SFX_KEY, 100);
            _sfxVolumeLabel.text = _sfxSlider.value.ToString();

            _frameSlider.value = (float)PlayerPrefs.GetInt(SETTINGS_FRAME_KEY, 30);
            _frameValueLabel.text = _frameSlider.value.ToString();

            //Translator???? ???????? - ???? ???????? ????
            Storage.TranslateStorage.Instance.ChangedLanguage();

            _uiHitActivateToggle.isOn = (PlayerPrefs.GetInt(SETTINGS_HIT_KEY, 1) == 1) ? true : false;
            _effectActivateToggle.isOn = (PlayerPrefs.GetInt(SETTINGS_EFFECT_KEY, 1) == 1) ? true : false;
        }

        private void Save()
        {
            PlayerPrefs.SetInt(SETTINGS_BGM_KEY, (int)_bgmSlider.value);
            PlayerPrefs.SetInt(SETTINGS_SFX_KEY, (int)_sfxSlider.value);
            PlayerPrefs.SetInt(SETTINGS_FRAME_KEY, (int)_frameSlider.value);

            //Translator???? ????????
            Storage.TranslateStorage.Instance.Save();

            PlayerPrefs.SetInt(SETTINGS_HIT_KEY, (_uiHitActivateToggle.isOn) ? 1 : 0);
            PlayerPrefs.SetInt(SETTINGS_EFFECT_KEY, (_effectActivateToggle.isOn) ? 1 : 0);
        }
    }
}