namespace SEF.UI.Toolkit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Storage;

    public class UIStart : MonoBehaviour
    {
#if UNITY_EDITOR
        private readonly static string NAME_UI_START_UXML = "UIStartUXML";
        private readonly static string PATH_UI_START_UXML = $"Assets/Scripts/UI/UIMain/{NAME_UI_START_UXML}.uxml";
#endif

        private VisualElement _root = null;

        private Button _startButton;

        private System.Action _startEvent;


        public static UIStart Create()
        {
            var obj = new GameObject();
            obj.name = "UI@Start";
            return obj.AddComponent<UIStart>();
        }

        public void Initialize(VisualElement parent)
        {

            _root = parent.Q<TemplateContainer>(NAME_UI_START_UXML);


            if (_root == null)
            {
                _root = UIUXML.GetVisualElement(gameObject, PATH_UI_START_UXML);
                if (parent != null)
                {
                    parent.Add(_root);
                }
            }

            _startButton = _root.Q<Button>("start_button");

            _startButton.RegisterCallback<ClickEvent>(e => OnStartEvent());

            Hide();
        }

        public void CleanUp()
        {
            _startButton.UnregisterCallback<ClickEvent>(e => OnStartEvent());
            _startButton = null;
            _root = null;
        }

        public void ShowStart(System.Action startCallback)
        {
            _root.style.display = DisplayStyle.Flex;
            _startEvent = startCallback;
        }

        public void Hide()
        {
            _root.style.display = DisplayStyle.None;
        }

        private void OnStartEvent()
        {
            _startEvent?.Invoke();
            Debug.Log("Load 이후 Scene_Game으로 넘어감");
        }
    }
}