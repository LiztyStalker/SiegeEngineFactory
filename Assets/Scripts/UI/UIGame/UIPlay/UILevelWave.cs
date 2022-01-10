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
                UpdateHandlePosition();
            }
        }
        public int MinValue { 
            get => _minValue;
            set
            {
                _minValue = value;
                ShowLabel();
                UpdateHandlePosition();
            }
        }
        public int MaxValue { 
            get => _maxValue;
            set
            {
                _maxValue = value;
                ShowLabel();
                UpdateHandlePosition();
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
            var value = _wave;
            var lowValue = _minValue;
            var highValue = _maxValue;
            var dragElement = _handle;
            var dragContainer = _sliderBar;

            float normalizedPosition = SliderNormalizeValue(value, lowValue, highValue);
            float directionalNormalizedPosition = normalizedPosition;// inverted ? 1f - normalizedPosition : normalizedPosition;
            float halfPixel = 0.5f;// scaledPixelsPerPoint * 0.5f;

            float dragElementWidth = dragElement.resolvedStyle.width;

            // This is the main calculation for the location of the thumbs / dragging element
            float offsetForThumbFullWidth = -dragElement.resolvedStyle.marginLeft - dragElement.resolvedStyle.marginRight;
            float totalWidth = dragContainer.layout.width - dragElementWidth + offsetForThumbFullWidth;
            float newLeft = directionalNormalizedPosition * totalWidth;

            if (float.IsNaN(newLeft)) //This can happen when layout is not computed yet
                return;

            float currentLeft = dragElement.transform.position.x;

            if (!SameValues(currentLeft, newLeft, halfPixel))
            {
                var newPos = new Vector3(newLeft, 0, 0);
                dragElement.transform.position = newPos;
                //dragBorderElement.transform.position = newPos;
            }
        }

        private bool SameValues(float a, float b, float epsilon)
        {
            return Mathf.Abs(b - a) < epsilon;
        }

        private float SliderNormalizeValue(int currentValue, int lowerValue, int higherValue)
        {
            return ((float)currentValue - (float)lowerValue) / ((float)higherValue - (float)lowerValue);
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