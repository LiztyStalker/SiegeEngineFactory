namespace SEF.UI.Toolkit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Storage;

    [RequireComponent(typeof(UIDocument))]
    public class UILoad : MonoBehaviour
    {

#if UNITY_EDITOR
        private readonly string PATH_UI_LOAD_UXML = "Assets/Scripts/UI/UIMain/UILoadUXML.uxml";
#endif


        private VisualElement _root = null;

        private Label _loadLabel;
        private Label _loadValueLabel;

        public static UILoad Create()
        {
            var obj = new GameObject();
            obj.name = "UI@Start";
            return obj.AddComponent<UILoad>();
        }

        public void Initialize(VisualElement parent)
        {
            _root = UIUXML.GetVisualElement(gameObject, PATH_UI_LOAD_UXML);

            _loadLabel = _root.Q<Label>("load_label");
            _loadValueLabel = _root.Q<Label>("load_value_label");

            _loadLabel.text = "·ÎµùÁß";
            _loadValueLabel.text = "0";

            if (parent != null) parent.Add(_root);

        }

        public void CleanUp()
        {
            _root = null;
        }

        public void ShowLoad(float progress)
        {
            _loadValueLabel.text = progress.ToString();
            _root.style.display = DisplayStyle.Flex;
        }

        public void Hide()
        {
            _root.style.display = DisplayStyle.None;
        }

    }
}