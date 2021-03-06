#if INCLUDE_UI_TOOLKIT
namespace SEF.UI.Toolkit
{
    using UnityEngine;
    using UnityEngine.UIElements;
    using PoolSystem;

    [RequireComponent(typeof(UIDocument))]
    public class UIHealthBar : MonoBehaviour, IPoolElement
    {
        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UIPlay/UIHealthBar.uxml";

        private UIDocument _uiDocument;
        private VisualElement _root;
        private UIFillable _uiFillable;

        private float _fillAmount;

        public void Initialize()
        {
            _uiDocument = GetComponent<UIDocument>();
            _root = _uiDocument.rootVisualElement;
            Inactivate();
        }

        public void CleanUp()
        {
            _retrieveEvent = null;
            Inactivate();
        }


        public void ShowHealth(float fillAmount, Vector2 position)
        {
            transform.position = position;
            _fillAmount = fillAmount;
        }


        public void Activate()
        {
            _root.style.display = DisplayStyle.Flex;
            SetPosition();
            SetFillAmount();

        }

        public void Inactivate()
        {
            _root.style.display = DisplayStyle.None;
        }


        private void SetPosition()
        {
            var screenPos = Camera.main.WorldToScreenPoint(transform.position);
            screenPos.y = Screen.height - screenPos.y;
            screenPos.z = 0;
            _uiDocument.rootVisualElement.transform.position = screenPos;
        }

        private void SetFillAmount()
        {
            //Inactivate되면 기존에 연결한 VisualElement가 해제되므로 
            //Activate후 다시 연결할 필요 있음
            _uiFillable = _root.Q<UIFillable>();
            _uiFillable.FillAmount = _fillAmount;
        }

        private float nowTime = 0;
        private void Update()
        {
            nowTime += Time.deltaTime;
            if (nowTime > 1f)
            {
                nowTime = 0;
                _retrieveEvent?.Invoke(this);
                Inactivate();
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
#endif