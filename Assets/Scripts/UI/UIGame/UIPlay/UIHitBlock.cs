namespace SEF.UI.Toolkit
{
    using UnityEngine;
    using UnityEngine.UIElements;
    using PoolSystem;

    [RequireComponent(typeof(UIDocument))]
    public class UIHitBlock : MonoBehaviour, IPoolElement
    {
        private UIDocument _uiDocument;
        private VisualElement _root;
        private Label _label;

        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UIPlay/UIHitBlock.uxml";

        public void Initialize() {
            _uiDocument = GetComponent<UIDocument>();
            _root = _uiDocument.rootVisualElement;
            Inactivate();
        }

        public void CleanUp() 
        {
            _retrieveEvent = null;
            Inactivate();
        }


        public void ShowHit(string value, Vector2 position)
        {
            transform.name = value;
            transform.position = position;
        }


        public void Activate()
        {
            gameObject.SetActive(true);
            SetPosition();
            SetLabel();
        }

        public void Inactivate()
        {
            gameObject.SetActive(false);
        }


        private void SetPosition()
        {
            var screenPos = Camera.main.WorldToScreenPoint(transform.position);
            screenPos.y = Screen.height - screenPos.y;
            screenPos.z = 0;
            _uiDocument.rootVisualElement.transform.position = screenPos;
        }

        private void SetLabel()
        {
            //Inactivate되면 기존에 연결한 VisualElement가 해제되므로 
            //Activate후 다시 연결할 필요 있음
            _label = _uiDocument.rootVisualElement.Q<Label>("hit_label");
            _label.text = transform.name;
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

        private System.Action<UIHitBlock> _retrieveEvent;
        public void SetOnRetrieveBlockListener(System.Action<UIHitBlock> act) => _retrieveEvent = act;

        #endregion



        public static UIHitBlock Create()
        {
            var obj = new GameObject();
            obj.name = "UIHitBlock";
            var block = obj.AddComponent<UIHitBlock>();
            var root = UIUXML.GetVisualElement(obj, PATH_UI_UXML);
            //Debug.Log(root.name);
            return block;
        }
    }
}