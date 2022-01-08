namespace SEF.UI.Toolkit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using PoolSystem;

    [RequireComponent(typeof(UIDocument))]
    public class UIHitBlock : MonoBehaviour, IPoolElement
    {

        private VisualElement _root;
        private Label _label;

        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UIPlay/UIHitBlock.uxml";

        public void Initialize() {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _label = _root.Q<Label>();
            Inactivate();
        }

        public void CleanUp() 
        {
            _retrieveEvent = null;
            Inactivate();
        }


        public void ShowHit(string value, Vector2 position)
        {
            Debug.Log(_label.transform.position);
            _label.text = value;
            _root.transform.position = position;
        }

        static void MoveAndScaleToWorldPosition(VisualElement element, Vector3 worldPosition, Vector2 worldSize)
        {
            //Rect rect = RuntimePanelUtils.CameraTransformWorldToPanelRect(element.panel, worldPosition, worldSize, Camera.main);
            //Vector2 layoutSize = element.layout.size;

            //// Don't set scale to 0 or a negative number.
            //Vector2 scale = layoutSize.x > 0 && layoutSize.y > 0 ? rect.size / layoutSize : Vector2.one * 1e-5f;

            element.transform.position = worldPosition;
            element.transform.scale = Vector3.one;// new Vector3(scale.x, scale.y, 1);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            MoveAndScaleToWorldPosition(_root, Vector2.one * 50, Vector2.one);
            Debug.Log(_root.transform.position);
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

        private System.Action<UIHitBlock> _retrieveEvent;
        public void SetOnRetrieveBlockListener(System.Action<UIHitBlock> act) => _retrieveEvent = act;

        #endregion



        public static UIHitBlock Create()
        {
            var obj = new GameObject();
            obj.name = "UIHitBlock";
            var block = obj.AddComponent<UIHitBlock>();
            UIUXML.GetVisualElement(obj, PATH_UI_UXML);
            return block;
        }
    }
}