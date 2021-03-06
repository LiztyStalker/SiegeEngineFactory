#if INCLUDE_UI_TOOLKIT
namespace SEF.UI.Toolkit
{
    using UnityEngine;
    using UnityEngine.UIElements;
    using Unity.Mathematics;

    public class UIFillable : VisualElement
    {

        //public enum FillDirection
        //{
        //    LeftToRight = 0,
        //    RightToLeft = 1,
        //    BottomToTop = 2,
        //    TopToBottom = 3
        //}


        //https://docs.unity.cn/kr/2020.3/Manual/UIE-UXML.html
        //팩토리를 정의
        //UXML파일 에서 UIProgressbar를 사용 가능 (UIBuilder에서는 불가. 직접 UXML에 입력해야 함
        //ex : <SEF.UI.Toolkit.UIProgressbar> </SEF.UI.Toolkit.UIProgressbar>
        public new class UxmlFactory : UxmlFactory<UIFillable, UxmlTraits> { }

        //속성 정의
        //
        public new class UxmlTraits : VisualElement.UxmlTraits
        {

            //속성 정의
            UxmlFloatAttributeDescription _fillAmount = new UxmlFloatAttributeDescription { name = "FillAmount", defaultValue = 0.3f };
            UxmlEnumAttributeDescription<DisplayStyle> _typeLabelView = new UxmlEnumAttributeDescription<DisplayStyle> { name = "TypeLabelView", defaultValue = DisplayStyle.None };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                ((UIFillable)ve).FillAmount = _fillAmount.GetValueFromBag(bag, cc);
                ((UIFillable)ve).DisplayStyle = _typeLabelView.GetValueFromBag(bag, cc);
            }
        }


        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIUtility/UIFillable.uxml";

        private Label _fillableLabel;
        private VisualElement _background;
        private VisualElement _fillableBar;

        private float _fillAmount;

        ///ViewType (Hide, Percent - 40%, NowMax - (23/323))

        public float FillAmount
        {
            get { return _fillAmount; }
            set
            {
                _fillAmount = math.saturate(value);
                UpdateTypeLabelView();
                UpdateFillable();
            }
        }

        private DisplayStyle _displayStyle;

        public DisplayStyle DisplayStyle
        {
            get { return _displayStyle; }
            set
            {
                _displayStyle = value;
                UpdateTypeLabelView();
                UpdateFillable();
            }
        }

        public Label ProgressLabel => _fillableLabel;

        //FillAmount 이미지형으로 제작
        //개발중
        //        private FillDirection fillDirection;

        //        private Texture2D originalTexture;
        //        private Texture2D copyTexture;

        private void UpdateFillable()
        {
            UpdateFillableBar();
            //SetLabel();
        }

        private void UpdateFillableBar()
        {
            _fillableBar.style.width = new StyleLength(Length.Percent(FillAmount * 100f));
        }

        private void GenerateVisualContent(MeshGenerationContext context)
        {
            _fillableBar.style.width = new StyleLength(Length.Percent(FillAmount * 100f));

            //var rStyle = this.resolvedStyle;
            //var texture = rStyle.backgroundImage.texture;

            //if (texture != null)
            //{
            //    if (texture != copyTexture)
            //    {
            //        if (copyTexture != null)
            //        {
            //            Texture2D.Destroy(copyTexture);
            //            copyTexture = null;
            //        }
            //        originalTexture = texture;
            //        copyTexture = CreateCopyTexture(originalTexture, nowValue, fillDirection);

            //        //Must execute this later (perhaps the next frame or so) because we shouldn't dirty this VisualElement during the generateVisualContent callback!
            //        //    (If we did, it would cause an infinite loop, basically-speaking)
            //        schedule.Execute(() => {
            //            style.backgroundImage = new StyleBackground(copyTexture);
            //        });
            //    }
            //    else
            //    {
            //        UpdateTexture(copyTexture, originalTexture, nowValue, fillDirection);
            //    }
            //}
            //else
            //{
            //    if (copyTexture != null)
            //    {
            //        Texture2D.Destroy(copyTexture);
            //        copyTexture = null;
            //    }
            //}
            //SetLabel();
        }

        //private Texture2D CreateCopyTexture(Texture2D source, float fillAmount, FillDirection fillDirection)
        //{
        //    //Texture2D copy = new Texture2D(source.width, source.height, source.format, false);
        //    //source.GetRawTextureData();
        //    Texture2D copy = Texture2D.Instantiate(source);
        //    copy.name = source.name + " (Copy)";
        //    UpdateTexture(copy, source, fillAmount, fillDirection);
        //    return copy;
        //}

        //private void UpdateTexture(Texture2D target, Texture2D original, float fillAmount, FillDirection fillDirection)
        //{
        //    int width = target.width;
        //    int height = target.height;

        //    Rect uvRect = CalculateFilledRect(fillAmount, fillDirection);
        //    int2 minPixel = new int2((int)math.round(uvRect.xMin * (width - 1)), (int)math.round(uvRect.yMin * (height - 1)));
        //    int2 maxPixel = new int2((int)math.round(uvRect.xMax * (width - 1)), (int)math.round(uvRect.yMax * (height - 1)));

        //    switch (target.format)
        //    {
        //        case TextureFormat.RGBA32:
        //        case TextureFormat.BGRA32:
        //            {
        //                NativeArray<Color32> pixels = target.GetRawTextureData<Color32>();
        //                NativeArray<Color32> originalPixels = original.GetRawTextureData<Color32>();

        //                //TODO: Optimize this?
        //                int i = 0;
        //                for (int py = 0; py < height; py++)
        //                {
        //                    for (int px = 0; px < width; px++)
        //                    {
        //                        byte alpha = originalPixels[i].a;

        //                        bool inFilledArea = (px >= minPixel.x && px <= maxPixel.x && py >= minPixel.y && py <= maxPixel.y);
        //                        if (!inFilledArea)
        //                            alpha = 0;

        //                        Color32 c = pixels[i];
        //                        c.a = alpha;
        //                        pixels[i++] = c;
        //                    }
        //                }

        //                target.LoadRawTextureData(pixels);
        //                target.Apply();
        //            }
        //            break;
        //        default:
        //            Debug.LogError("Unsupported texture format: " + target.format + "!\n" +
        //                "If you'd like to add support for other texture formats, edit this!");
        //            break;
        //    }
        //}

        ///// <summary>
        ///// Calculates the normalized area in UV-space that a filled image should be showing within, based on <paramref name="fillAmount"/> and <paramref name="fillDirection"/>.
        ///// </summary>
        //private Rect CalculateFilledRect(float fillAmount, FillDirection fillDirection)
        //{
        //    fillAmount = math.saturate(fillAmount); //Saturate => keep it in range [0, 1]

        //    //These are coordinates using bottom-left (0, 0) like UVs
        //    float leftUV = 0;
        //    float rightUV = 1;
        //    float bottomUV = 0;
        //    float topUV = 1;

        //    switch (fillDirection)
        //    {
        //        case FillDirection.LeftToRight:
        //            rightUV = fillAmount;
        //            break;
        //        case FillDirection.RightToLeft:
        //            leftUV = 1 - fillAmount;
        //            break;
        //        case FillDirection.BottomToTop:
        //            topUV = fillAmount;
        //            break;
        //        case FillDirection.TopToBottom:
        //            bottomUV = 1 - fillAmount;
        //            break;
        //    }

        //    return new Rect(leftUV, bottomUV, (rightUV - leftUV), (topUV - bottomUV));
        //}


        //private void SetProgressValue()
        //{
        //    _progressbar.style.width = new StyleLength(Length.Percent(nowValue / maxValue));
        //}

        private void UpdateTypeLabelView()
        {
            _fillableLabel.style.display = _displayStyle;
        }

        public void SetLabel(string value)
        {
            if(_displayStyle == DisplayStyle.Flex)
                _fillableLabel.text = value;
        }

        public UIFillable()
        {

            pickingMode = PickingMode.Ignore;

            AddToClassList("unity-slider");
            AddToClassList("unity-base-slider");
            AddToClassList("unity-base-field");
            AddToClassList("unity-base-slider--horizontal");


            var progressbar = new VisualElement();
            progressbar.name = "fillable-panel";
            progressbar.AddToClassList("unity-base-field__input");
            progressbar.AddToClassList("unity-base-slider__input");
            progressbar.AddToClassList("unity-slider__input");
            progressbar.AddToClassList("unity-base-slider__drag-container");

            _background = new VisualElement() { name = "fillable-background" };
            _fillableBar = new VisualElement() { name = "fillable-bar" };
            _fillableLabel = new Label() { name = "fillable-label", focusable = false };
            _fillableLabel.AddToClassList("unity-base-field__label");
            _fillableLabel.AddToClassList("unity-base-slider__label");
            _fillableLabel.AddToClassList("unity-slider__label");

            progressbar.Add(_background);
            progressbar.Add(_fillableBar);
            Add(progressbar);
            Add(_fillableLabel);

            _fillableLabel.style.display = DisplayStyle.None;

            _fillableBar.generateVisualContent = GenerateVisualContent;

        }
    }


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS


    //통합 UIVisualElementTester
    //개발중
    //[RequireComponent(typeof(UIDocument))]

    //public class UIVisualElementTester : MonoBehaviour
    //{

    //}




    //public class UIVisualElementTester<T> where T : VisualElement
    //{
    //    private T _instance;

    //    public T Instance => _instance;

    //    public static UIVisualElementTester Create()
    //    {
    //        var obj = new GameObject();
    //        obj.name = typeof(T).Name;
    //        return obj.AddComponent<UIVisualElementTester>();
    //    }

    //    public static void Initialize(GameObject gameObject, string uxmlPath)
    //    {
    //        var root = UIUXML.GetVisualElement(gameObject, uxmlPath);
    //        _instance = root.Q<T>();
    //    }

    //    public void Dispose()
    //    {
    //        _instance = null;
    //    }
    //}




    [RequireComponent(typeof(UIDocument))]
    public class UIFillable_Test : MonoBehaviour
    {
        private UIFillable _instance;
        public UIFillable Instance => _instance;

        public static UIFillable_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UIFillable_Test";
            return obj.AddComponent<UIFillable_Test>();
        }

        public void Initialize()
        {
            var root = UIUXML.GetVisualElement(gameObject, UIFillable.PATH_UI_UXML);
            _instance = root.Q<UIFillable>();
        }

        public void Dispose()
        {
            _instance = null;
            DestroyImmediate(gameObject);
        }
    }
#endif
}
#endif