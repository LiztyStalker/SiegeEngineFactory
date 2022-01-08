namespace SEF.UI.Toolkit
{
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;
    using PoolSystem;

    [RequireComponent(typeof(UIDocument))]
    public class UIHealthBar : MonoBehaviour, IPoolElement
    {
        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UIPlay/UIHealthBar.uxml";


        private VisualElement _root;
        private UIProgressbar _fillBar;

        public void Initialize()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _fillBar = _root.Q<UIProgressbar>();
            Inactivate();
        }

        public void CleanUp()
        {
            _retrieveEvent = null;
            Inactivate();
        }


        public void ShowHealth(float fillAmount, Vector2 position)
        {
            _root.transform.position = position;
            _fillBar.FillAmount = fillAmount;
        }

        //static void MoveAndScaleToWorldPosition(VisualElement element, Vector3 worldPosition, Vector2 worldSize)
        //{
        //    //Rect rect = RuntimePanelUtils.CameraTransformWorldToPanelRect(element.panel, worldPosition, worldSize, Camera.main);
        //    //Vector2 layoutSize = element.layout.size;

        //    //// Don't set scale to 0 or a negative number.
        //    //Vector2 scale = layoutSize.x > 0 && layoutSize.y > 0 ? rect.size / layoutSize : Vector2.one * 1e-5f;

        //    element.transform.position = worldPosition;
        //    element.transform.scale = Vector3.one;// new Vector3(scale.x, scale.y, 1);
        //}

        public void Activate()
        {
            gameObject.SetActive(true);
            //MoveAndScaleToWorldPosition(_root, Vector2.one * 50, Vector2.one);
            //Debug.Log(_root.transform.position);
        }

        public void Inactivate()
        {
            gameObject.SetActive(false);
        }

        private float nowTime = 0;
        private void Update()
        {
            nowTime += Time.deltaTime;
            if (nowTime > 1f)
            {
                nowTime = 0;
                _retrieveEvent?.Invoke(this);
            }
        }

        #region ##### Listener #####

        private System.Action<UIHealthBar> _retrieveEvent;
        public void SetOnRetrieveBlockListener(System.Action<UIHealthBar> act) => _retrieveEvent = act;

        #endregion



#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static UIHealthBar Create()
        {
            var obj = new GameObject();
            obj.name = "UIHealthBar";
            var block = obj.AddComponent<UIHealthBar>();
            UIUXML.GetVisualElement(obj, PATH_UI_UXML);
            return block;
        }
#endif
    }
}