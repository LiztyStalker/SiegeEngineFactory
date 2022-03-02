namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UILevelWave : MonoBehaviour
    {
        private readonly static string UGUI_NAME = "UI@LevelWave";


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
            _sliderBar.maxValue = 9;
            _sliderBar.minValue = 0;
        }

        public void CleanUp()
        {
        }

        public void ShowLevelWave(int level, int wave)
        {
            _sliderBar.value = wave;
            //UpdateHandlePosition();
            ShowLabel(level, wave);
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

        private void ShowLabel(int level, int wave)
        {
            //Debug.Log("ShowLabel");
            _levelLabel.text = level.ToString();
            _waveLabel.text = $"{wave + 1}/{_sliderBar.maxValue + 1}";
        }

        public static UILevelWave Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UILevelWave>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.name = UGUI_NAME;
                return obj.AddComponent<UILevelWave>();
            }
#else
            Debug.LogWarning($"{UGUI_NAME}을 찾을 수 없습니다");
            return null;
#endif

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
            _instance = UILevelWave.Create();
            _instance.Initialize();
        }

        public void Dispose()
        {
            _instance = null;
            DestroyImmediate(gameObject);
        }
    }
#endif
}