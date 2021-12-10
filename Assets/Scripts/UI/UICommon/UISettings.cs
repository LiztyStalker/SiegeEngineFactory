namespace SEF.UI.Toolkit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Storage;

    [RequireComponent(typeof(UIDocument))]
    public class UISettings : MonoBehaviour
    {

        private readonly string SETTINGS_BGM_KEY = "BGM_VALUE";
        private readonly string SETTINGS_SFX_KEY = "SFX_VALUE";
        private readonly string SETTINGS_FRAME_KEY = "FRAME_VALUE";
        private readonly string SETTINGS_LANGUAGE_KEY = "LANGUAGE";
        private readonly string SETTINGS_HIT_KEY = "ISHIT";
        private readonly string SETTINGS_EFFECT_KEY = "ISEFFECT";


        private readonly string PATH_DEFAULT_SETTING = "Assets/UIToolkit/DefaultPanelSettings.asset";
        private readonly string PATH_UI_SETTINGS_UXML = "Assets/Scripts/UI/UICommon/UISettingsUXML.uxml";

        private VisualElement _root = null;

        private Label _versionLabel;

        private Slider _bgmSlider;
        private Label _bgmVolumeLabel;

        private Slider _sfxSlider;
        private Label _sfxVolumeLabel;

        private Slider _frameSlider;
        private Label _frameValueLabel;

        private Button _langLeftButton;
        private Button _langRightButton;
        private Label _langLabel;

        private Toggle _uiHitActivateToggle;
        private Toggle _effectActivateToggle;

        private Button _saveButton;
        private Button _loadButton;

        private Button _gpgsButton;
        private Button _qnaButton;

        private Button _exitButton;

        public static UISettings Create()
        {
            var obj = new GameObject();
            obj.name = "UI@Settings";
            return obj.AddComponent<UISettings>();
        }

        public void Initialize()
        {
            if (_root == null)
            {
#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
                var _uiDocument = GetComponent<UIDocument>();
                _uiDocument.panelSettings = DataStorage.LoadAssetAtPath<PanelSettings>(PATH_DEFAULT_SETTING);

                var asset = DataStorage.LoadAssetAtPath<VisualTreeAsset>(PATH_UI_SETTINGS_UXML);
                _uiDocument.visualTreeAsset = asset;
                _root = _uiDocument.rootVisualElement;
                Debug.Assert(_root != null, $"{PATH_UI_SETTINGS_UXML} UI를 구성하지 못했습니다");
#else
        
#endif
            }




            _versionLabel = _root.Q<Label>("version_label");

            _bgmSlider = _root.Q<Slider>("bgm_slider");
            _bgmVolumeLabel = _root.Q<Label>("bgm_slider_value_label");

            _sfxSlider = _root.Q<Slider>("sfx_slider");
            _sfxVolumeLabel = _root.Q<Label>("sfx_slider_value_label");

            _frameSlider = _root.Q<Slider>("frame_slider");
            _frameValueLabel = _root.Q<Label>("frame_slider_value_label");

            _langLeftButton = _root.Q<Button>("lang_left_button");
            _langRightButton = _root.Q<Button>("lang_right_button");
            _langLabel = _root.Q<Label>("lang_value_label");

            _uiHitActivateToggle = _root.Q<Toggle>("hit_toggle");
            _effectActivateToggle = _root.Q<Toggle>("effect_toggle");

            _saveButton = _root.Q<Button>("save_button");
            _loadButton = _root.Q<Button>("load_button");

            _gpgsButton = _root.Q<Button>("gpgs_button");
            _qnaButton = _root.Q<Button>("qna_button");

            _exitButton = _root.Q<Button>("exit_button");


            Debug.Assert(_versionLabel != null, $"{PATH_UI_SETTINGS_UXML} version_label 을 구성하지 못했습니다");
            Debug.Assert(_bgmSlider != null, $"{PATH_UI_SETTINGS_UXML} bgm_slider 을 구성하지 못했습니다");
            Debug.Assert(_bgmVolumeLabel != null, $"{PATH_UI_SETTINGS_UXML} bgm_slider_value_label 을 구성하지 못했습니다");
            Debug.Assert(_sfxSlider != null, $"{PATH_UI_SETTINGS_UXML} sfx_slider 을 구성하지 못했습니다");
            Debug.Assert(_sfxVolumeLabel != null, $"{PATH_UI_SETTINGS_UXML} sfx_slider_value_label 을 구성하지 못했습니다");
            Debug.Assert(_frameSlider != null, $"{PATH_UI_SETTINGS_UXML} frame_slider 을 구성하지 못했습니다");
            Debug.Assert(_frameValueLabel != null, $"{PATH_UI_SETTINGS_UXML} frame_slider_value_label 을 구성하지 못했습니다");
            Debug.Assert(_langLeftButton != null, $"{PATH_UI_SETTINGS_UXML} lang_left_button 을 구성하지 못했습니다");
            Debug.Assert(_langRightButton != null, $"{PATH_UI_SETTINGS_UXML} lang_right_button 을 구성하지 못했습니다");
            Debug.Assert(_langLabel != null, $"{PATH_UI_SETTINGS_UXML} lang_value_label 을 구성하지 못했습니다");
            Debug.Assert(_uiHitActivateToggle != null, $"{PATH_UI_SETTINGS_UXML} hit_toggle 을 구성하지 못했습니다");
            Debug.Assert(_effectActivateToggle != null, $"{PATH_UI_SETTINGS_UXML} effect_toggle 을 구성하지 못했습니다");
            Debug.Assert(_saveButton != null, $"{PATH_UI_SETTINGS_UXML} save_button 을 구성하지 못했습니다");
            Debug.Assert(_loadButton != null, $"{PATH_UI_SETTINGS_UXML} load_button 을 구성하지 못했습니다");
            Debug.Assert(_gpgsButton != null, $"{PATH_UI_SETTINGS_UXML} gpgs_button 을 구성하지 못했습니다");
            Debug.Assert(_qnaButton != null, $"{PATH_UI_SETTINGS_UXML} qna_button 을 구성하지 못했습니다");
            Debug.Assert(_exitButton != null, $"{PATH_UI_SETTINGS_UXML} exit_button 을 구성하지 못했습니다");

            Load();

            _versionLabel.text = Application.version;

            RegisterEvents();

            Hide();

        }

        public void CleanUp()
        {
            _closedEvent = null;

            UnRegisterEvents();

            _exitButton = null;
            _root = null;
        }

        private void RegisterEvents()
        {
            _bgmSlider.RegisterValueChangedCallback(e => OnBGMEvent(e.newValue));
            _sfxSlider.RegisterValueChangedCallback(e => OnSFXEvent(e.newValue));
            _frameSlider.RegisterValueChangedCallback(e => OnFrameEvent(e.newValue));

            _langLeftButton.RegisterCallback<ClickEvent>(e => OnLeftLanguageEvent());
            _langRightButton.RegisterCallback<ClickEvent>(e => OnRightLanguageEvent());

            _uiHitActivateToggle.RegisterValueChangedCallback(e => OnHitToggleEvent(e.newValue));
            _effectActivateToggle.RegisterValueChangedCallback(e => OnEffectToggleEvent(e.newValue));

            _saveButton.RegisterCallback<ClickEvent>(e => OnSaveEvent());
            _loadButton.RegisterCallback<ClickEvent>(e => OnLoadEvent());
            _gpgsButton.RegisterCallback<ClickEvent>(e => OnGPGSEvent());
            _qnaButton.RegisterCallback<ClickEvent>(e => OnQnAEvent());

            _exitButton.RegisterCallback<ClickEvent>(e => OnExitEvent());
        }

        private void UnRegisterEvents()
        {
            _bgmSlider.UnregisterValueChangedCallback(e => OnBGMEvent(e.newValue));
            _sfxSlider.UnregisterValueChangedCallback(e => OnSFXEvent(e.newValue));
            _frameSlider.UnregisterValueChangedCallback(e => OnFrameEvent(e.newValue));

            _langLeftButton.UnregisterCallback<ClickEvent>(e => OnLeftLanguageEvent());
            _langRightButton.UnregisterCallback<ClickEvent>(e => OnRightLanguageEvent());

            _uiHitActivateToggle.UnregisterValueChangedCallback(e => OnHitToggleEvent(e.newValue));
            _effectActivateToggle.UnregisterValueChangedCallback(e => OnEffectToggleEvent(e.newValue));

            _saveButton.UnregisterCallback<ClickEvent>(e => OnSaveEvent());
            _loadButton.UnregisterCallback<ClickEvent>(e => OnLoadEvent());
            _gpgsButton.UnregisterCallback<ClickEvent>(e => OnGPGSEvent());
            _qnaButton.UnregisterCallback<ClickEvent>(e => OnQnAEvent());

            _exitButton.UnregisterCallback<ClickEvent>(e => OnExitEvent());
        }

        private void OnBGMEvent(float value)
        {
            Debug.Log(value);
            _bgmVolumeLabel.text = value.ToString();
        }

        private void OnSFXEvent(float value)
        {
            _sfxVolumeLabel.text = value.ToString();
        }

        private void OnFrameEvent(float value)
        {
            _frameValueLabel.text = value.ToString();
        }

        private void OnLeftLanguageEvent() 
        {
            _langLabel.text = "";
            Debug.Log("Language Left Button");
        }
        private void OnRightLanguageEvent() 
        {
            _langLabel.text = "";
            Debug.Log("Language Right Button");
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


        public void Show(System.Action closedCallback = null)
        {
            _root.style.display = DisplayStyle.Flex;
            _closedEvent = closedCallback;
        }


        public void Hide()
        {
            _root.style.display = DisplayStyle.None;

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

            _langLabel.text = PlayerPrefs.GetString(SETTINGS_LANGUAGE_KEY, "Korean");

            _uiHitActivateToggle.value = (PlayerPrefs.GetInt(SETTINGS_HIT_KEY, 1) == 1) ? true : false;
            _effectActivateToggle.value = (PlayerPrefs.GetInt(SETTINGS_EFFECT_KEY, 1) == 1) ? true : false;
        }

        private void Save()
        {
            PlayerPrefs.SetInt(SETTINGS_BGM_KEY, (int)_bgmSlider.value);
            PlayerPrefs.SetInt(SETTINGS_SFX_KEY, (int)_sfxSlider.value);
            PlayerPrefs.SetInt(SETTINGS_FRAME_KEY, (int)_frameSlider.value);
            PlayerPrefs.SetString(SETTINGS_LANGUAGE_KEY, _langLabel.text);
            PlayerPrefs.SetInt(SETTINGS_HIT_KEY, (_uiHitActivateToggle.value) ? 1 : 0);
            PlayerPrefs.SetInt(SETTINGS_EFFECT_KEY, (_effectActivateToggle.value) ? 1 : 0);
        }
    }
}