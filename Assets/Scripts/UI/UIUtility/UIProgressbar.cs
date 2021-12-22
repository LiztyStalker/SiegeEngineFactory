namespace SEF.UI.Toolkit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class UIProgressbar : VisualElement
    {
        //https://docs.unity.cn/kr/2020.3/Manual/UIE-UXML.html
        //���丮�� ����
        //UXML���� ���� UIProgressbar�� ��� ���� (UIBuilder������ �Ұ�. ���� UXML�� �Է��ؾ� ��
        //ex : <SEF.UI.Toolkit.UIProgressbar> </SEF.UI.Toolkit.UIProgressbar>
        public new class UxmlFactory : UxmlFactory<UIProgressbar, UxmlTraits> { }

        //�Ӽ� ����
        //
        public new class UxmlTraits : VisualElement.UxmlTraits {

            //�Ӽ� ����
            UxmlFloatAttributeDescription _nowValue = new UxmlFloatAttributeDescription { name = "nowValue" };
            UxmlFloatAttributeDescription _minValue = new UxmlFloatAttributeDescription { name = "minValue" };
            UxmlFloatAttributeDescription _maxValue = new UxmlFloatAttributeDescription { name = "maxValue" };

            public UxmlTraits()
            {
                _maxValue.defaultValue = 100f;
            }

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                ((UIProgressbar)ve).nowValue = _nowValue.GetValueFromBag(bag, cc);
                ((UIProgressbar)ve).minValue = _minValue.GetValueFromBag(bag, cc);
                ((UIProgressbar)ve).maxValue = _maxValue.GetValueFromBag(bag, cc);
            }
        }

        private Label _progressLabel;
        private VisualElement _background;
        private VisualElement _progressbar;

        private float _nowValue;
        private float _minValue;
        private float _maxValue;

        ///ViewType (Hide, Percent - 40%, NowMax - (23/323))

        public float nowValue { 
            get { return _nowValue; } 
            set {
                var tmpValue = value;
                if (tmpValue < _minValue) tmpValue = _minValue;
                if (tmpValue > _maxValue) tmpValue = _maxValue;
                _nowValue = tmpValue;
                SetProgressValue();
                SetLabel();
            } 
        }
        public float minValue { 
            get { return _minValue; }
            set {
                _minValue = value;
                SetProgressValue();
                SetLabel();
            } 
        }

        public float maxValue {
            get { return _maxValue; }
            set {
                _maxValue = value;
                SetProgressValue();
                SetLabel();
            }
        }

        
        private void SetProgressValue()
        {
            // nowValue / maxValue;
        }
        private void SetLabel()
        {
            _progressLabel.text = _nowValue.ToString();
        }

        public UIProgressbar()
        {
            pickingMode = PickingMode.Ignore;

            AddToClassList("unity-slider");
            AddToClassList("unity-base-slider");
            AddToClassList("unity-base-field");
            AddToClassList("unity-base-slider--horizontal");


            var progressbar = new VisualElement();
            progressbar.name = "progressbar_panel";
            progressbar.AddToClassList("unity-base-field__input");
            progressbar.AddToClassList("unity-base-slider__input");
            progressbar.AddToClassList("unity-slider__input");
            progressbar.AddToClassList("unity-base-slider__drag-container");

            _background = new VisualElement() { name = "progressbar_background" };
            _progressbar = new VisualElement() { name = "progressbar" };
            _progressLabel = new Label() { name = "progressbar_label", focusable = false };
            _progressLabel.AddToClassList("unity-base-field__label");
            _progressLabel.AddToClassList("unity-base-slider__label");
            _progressLabel.AddToClassList("unity-slider__label");

            progressbar.Add(_background);
            progressbar.Add(_progressbar);
            Add(progressbar);
            Add(_progressLabel);

            _progressLabel.style.display = DisplayStyle.None;
        }
    }
}