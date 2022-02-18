namespace SEF.UI.Toolkit
{
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;
    using SEF.Entity;

    public class UIResearchBlock : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<UIResearchBlock, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UISystem/UIResearch/UIResearchBlock.uxml";


        private int _index;

        private VisualElement _activatePanel;

        private VisualElement _icon;

        private Label _nameLabel;
        private Label _levelValueLabel;

        private Label _contentLabel;
        private Label _abilityLabel;

        private Button _upgradeButton;
        private VisualElement _upgradeAssetIcon;
        private Label _upgradeValueLabel;

        private VisualElement _inactivatePanel;
        private Label _inactivateLabel;

        public void SetIndex(int index) => _index = index;

        public static UIResearchBlock Create()
        {
            return UIUXML.GetVisualElement<UIResearchBlock>(PATH_UI_UXML);
        }

        public void Initialize()
        {
            _activatePanel = this.Q<VisualElement>("activate-panel");

            _icon = this.Q<VisualElement>("line-icon");

            _nameLabel = this.Q<Label>("name-label");
            _levelValueLabel = this.Q<Label>("level-value-label");

            _contentLabel = this.Q<Label>("content-label");
            _abilityLabel = this.Q<Label>("ability-label");

            _upgradeButton = this.Q<Button>("upgrade-button");
            _upgradeAssetIcon = this.Q<VisualElement>("upgrade-asset-icon");
            _upgradeValueLabel = this.Q<Label>("upgrade-asset-value-label");

            _inactivatePanel = this.Q<VisualElement>("inactivate_panel");

            Debug.Assert(_activatePanel != null, "_activatePanel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_icon != null, "icon element �� ã�� ���߽��ϴ�");
            Debug.Assert(_nameLabel != null, "_nameLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_levelValueLabel != null, "_levelValueLabel element �� ã�� ���߽��ϴ�");

            Debug.Assert(_contentLabel != null, "_contentLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_abilityLabel != null, "_abilityLabel element �� ã�� ���߽��ϴ�");

            Debug.Assert(_upgradeButton != null, "_upgradeButton element �� ã�� ���߽��ϴ�");
            Debug.Assert(_upgradeAssetIcon != null, "_upgradeAssetIcon element �� ã�� ���߽��ϴ�");
            Debug.Assert(_upgradeValueLabel != null, "_upgradeValueLabel element �� ã�� ���߽��ϴ�");

            Debug.Assert(_inactivatePanel != null, "_inactivatePanel element �� ã�� ���߽��ϴ�");


            //_icon

            _nameLabel.text = "�̸�";
            _levelValueLabel.text = "1";
            _contentLabel.text = "����";
            _abilityLabel.text = "�ɷ�";

            _upgradeButton.RegisterCallback<ClickEvent>(OnUpgradeEvent);

            _activatePanel.style.display = DisplayStyle.None;
            _inactivatePanel.style.display = DisplayStyle.Flex;

        }


        public void RefreshResearchLine()
        {
            if (_activatePanel.style.display == DisplayStyle.None)
            {
                _activatePanel.style.display = DisplayStyle.Flex;
                _inactivatePanel.style.display = DisplayStyle.None;
            }
        }

        public void RefreshAssetEntity(AssetPackage assetEntity)
        {
            //var isEnough = assetEntity.IsEnough(_unitEntity.UpgradeAssetData);
            //_upgradeButton.SetEnabled(isEnough);
        }

        public void CleanUp()
        {
            _upgradeButton.UnregisterCallback<ClickEvent>(OnUpgradeEvent);
            _icon = null;
        }



        #region ##### Listener #####


        private System.Action<int> _upgradeEvent;
        public void AddUpgradeListener(System.Action<int> act) => _upgradeEvent += act;
        public void RemoveUpgradeListener(System.Action<int> act) => _upgradeEvent -= act;
        private void OnUpgradeEvent(ClickEvent e)
        {
            _upgradeEvent?.Invoke(_index);
        }

        #endregion
    }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    [RequireComponent(typeof(UIDocument))]
    public class UIResearchBlock_Test : MonoBehaviour
    {
        private UIResearchBlock _instance;
        public UIResearchBlock Instance => _instance;

        public static UIResearchBlock_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UIResearchBlock_Test";
            return obj.AddComponent<UIResearchBlock_Test>();
        }

        public void Initialize()
        {
            var root = UIUXML.GetVisualElement(gameObject, UIResearchBlock.PATH_UI_UXML);
            _instance = root.Q<UIResearchBlock>();
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