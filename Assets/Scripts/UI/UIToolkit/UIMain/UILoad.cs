#if INCLUDE_UI_TOOLKIT
namespace SEF.UI.Toolkit
{
    using UnityEngine;
    using UnityEngine.UIElements;

    public class UILoad : MonoBehaviour
    {

#if UNITY_EDITOR
        private readonly static string NAME_UI_LOAD_UXML = "UILoadUXML";
        private readonly static string PATH_UI_LOAD_UXML = $"Assets/Scripts/UI/UIMain/{NAME_UI_LOAD_UXML}.uxml";
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
            _root = parent.Q<TemplateContainer>(NAME_UI_LOAD_UXML);

            if (_root == null)
            {
                _root = UIUXML.GetVisualElement(gameObject, PATH_UI_LOAD_UXML);
                if (parent != null)
                {
                    parent.Add(_root);
                }
            }

            _loadLabel = _root.Q<Label>("load_label");
            _loadValueLabel = _root.Q<Label>("load_value_label");

            _loadLabel.text = "·ÎµùÁß";
            _loadValueLabel.text = "0";

            Hide();

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
#endif