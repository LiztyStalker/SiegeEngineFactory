namespace SEF.UI.Toolkit {
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;


    public class UILevelWave : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<UILevelWave, UxmlTraits> { }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {

            //속성 정의
            UxmlIntAttributeDescription _level = new UxmlIntAttributeDescription { name = "level", defaultValue = 1 };
            UxmlIntAttributeDescription _wave = new UxmlIntAttributeDescription { name = "wave", defaultValue = 1 };
            UxmlIntAttributeDescription _min = new UxmlIntAttributeDescription { name = "min", defaultValue = 10 };
            UxmlIntAttributeDescription _max = new UxmlIntAttributeDescription { name = "max", defaultValue = 10 };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                ((UILevelWave)ve).Level = _level.GetValueFromBag(bag, cc);
                ((UILevelWave)ve).Wave = _wave.GetValueFromBag(bag, cc);
                ((UILevelWave)ve).MinValue = _min.GetValueFromBag(bag, cc);
                ((UILevelWave)ve).MaxValue = _max.GetValueFromBag(bag, cc);
            }
        }

        private int _level;
        private int _wave;
        private int _minValue;
        private int _maxValue;

        public int Level { 
            get => _level;
            set
            {
                _level = value;
                ShowLabel();
            }
        }
        public int Wave { 
            get => _wave;
            set
            {
                if (_wave > _maxValue)
                    _wave = MaxValue;
                else
                    _wave = value;
                ShowLabel();
            }
        }
        public int MinValue { 
            get => _minValue;
            set
            {
                _minValue = value;
                ShowLabel();
            }
        }
        public int MaxValue { 
            get => _maxValue;
            set
            {
                _maxValue = value;
                ShowLabel();
            }
        }


        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UIPlay/UILevelWave.uxml";


        private VisualElement _sliderBar;
        private VisualElement _handle;
        private VisualElement _bossIcon;
        private Label _levelLabel;
        private Label _waveLabel;

        public UILevelWave()
        {
            AddToClassList("unity-slider");
            AddToClassList("unity-base-slider");
            AddToClassList("unity-base-field");
            AddToClassList("unity-base-slider--horizontal");

            pickingMode = PickingMode.Ignore;

            _sliderBar = new VisualElement() { name = "sliderbar" };
            _handle = new VisualElement() { name = "handle" };
            _waveLabel = new Label() { name = "wavelabel" };
            _levelLabel = new Label() { name = "levellabel" };
            _bossIcon = new VisualElement() { name = "bossicon" };

            _handle.Add(_levelLabel);
            _handle.Add(_waveLabel);

            _sliderBar.Add(_bossIcon);
            _sliderBar.Add(_handle);

            Add(_sliderBar);

            //generateVisualContent = GenerateVisualContent;
        }
       
        private void UpdateHandlePosition()
        {
            //_handle.transform.position = Vector2.zero;
        }

        private void ShowLabel()
        {
            Debug.Log("ShowLabel");
            _levelLabel.text = _level.ToString();
            _waveLabel.text = $"{_wave}/{_maxValue}";
        }
    }



#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

    [RequireComponent(typeof(UIDocument))]
    public class UILevelWave_Test : MonoBehaviour
    {
        private UILevelWave _instance;
        public UILevelWave Instance => _instance;

        public static UILevelWave_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UILevelWave_Test";
            return obj.AddComponent<UILevelWave_Test>();
        }

        public void Initialize()
        {
            var root = UIUXML.GetVisualElement(gameObject, UILevelWave.PATH_UI_UXML);
            _instance = root.Q<UILevelWave>();
        }

        public void Dispose()
        {
            _instance = null;
            DestroyImmediate(gameObject);
        }
    }
#endif
}