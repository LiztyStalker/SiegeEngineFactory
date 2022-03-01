namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UILevelWave : MonoBehaviour
    {

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
                //UpdateHandlePosition();
            }
        }
        public int MinValue { 
            get => _minValue;
            set
            {
                _minValue = value;
            }
        }
        public int MaxValue { 
            get => _maxValue;
            set
            {
                _maxValue = value;
            }
        }



        [SerializeField]
        private Slider _sliderBar;

        [SerializeField]
        private Image _handle;

        [SerializeField]
        private Image _bossIcon;

        [SerializeField]
        private Text _levelLabel;

        [SerializeField]
        private Text _waveLabel;


        public void Initialize()
        {
            _minValue = 0;
            _maxValue = 9;
        }

        public void CleanUp()
        {
        }

        public void ShowLevelWave(int level, int wave)
        {
            _level = level;
            _wave = wave;
            //UpdateHandlePosition();
            ShowLabel();
        }

        //private void UpdateHandlePosition()
        //{
        //    var value = _wave;
        //    var lowValue = _minValue;
        //    var highValue = _maxValue;
        //    var dragElement = _handle;
        //    var dragContainer = _sliderBar;

        //    float normalizedPosition = SliderNormalizeValue(value, lowValue, highValue);
        //    float directionalNormalizedPosition = normalizedPosition;// inverted ? 1f - normalizedPosition : normalizedPosition;
        //    float halfPixel = 0.5f;// scaledPixelsPerPoint * 0.5f;

        //    float dragElementWidth = dragElement.resolvedStyle.width;

        //    // This is the main calculation for the location of the thumbs / dragging element
        //    float offsetForThumbFullWidth = -dragElement.resolvedStyle.marginLeft - dragElement.resolvedStyle.marginRight;
        //    float totalWidth = dragContainer.layout.width - dragElementWidth + offsetForThumbFullWidth;
        //    float newLeft = directionalNormalizedPosition * totalWidth;

        //    if (float.IsNaN(newLeft)) //This can happen when layout is not computed yet
        //        return;

        //    float currentLeft = dragElement.transform.position.x;

        //    if (!SameValues(currentLeft, newLeft, halfPixel))
        //    {
        //        var newPos = new Vector3(newLeft, 0, 0);
        //        dragElement.transform.position = newPos;
        //        //dragBorderElement.transform.position = newPos;
        //    }
        //}

        //private bool SameValues(float a, float b, float epsilon)
        //{
        //    return Mathf.Abs(b - a) < epsilon;
        //}

        //private float SliderNormalizeValue(int currentValue, int lowerValue, int higherValue)
        //{
        //    return ((float)currentValue - (float)lowerValue) / ((float)higherValue - (float)lowerValue);
        //}

        private void ShowLabel()
        {
            //Debug.Log("ShowLabel");
            _levelLabel.text = _level.ToString();
            _waveLabel.text = $"{_wave + 1}/{_maxValue + 1}";
        }
    }



#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

    //[RequireComponent(typeof(UIDocument))]
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
            //var root = UIUXML.GetVisualElement(gameObject, UILevelWave.PATH_UI_UXML);
            //_instance = root.Q<UILevelWave>();
        }

        public void Dispose()
        {
            _instance = null;
            DestroyImmediate(gameObject);
        }
    }
#endif
}