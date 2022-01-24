namespace SEF.UI.Toolkit { 
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;
    using Entity;
    using SEF.Data;

    public class UIBlacksmithLine : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<UIBlacksmithLine, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UISystem/UIBlacksmith/UIBlacksmithLine.uxml";


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

        private BlacksmithEntity _entity;

        public static UIBlacksmithLine Create()
        {
            return UIUXML.GetVisualElement<UIBlacksmithLine>(PATH_UI_UXML);
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

            _inactivatePanel = this.Q<VisualElement>("inactivate-panel");

            Debug.Assert(_activatePanel != null, "_activatePanel element 를 찾지 못했습니다");
            Debug.Assert(_icon != null, "icon element 를 찾지 못했습니다");
            Debug.Assert(_nameLabel != null, "_nameLabel element 를 찾지 못했습니다");
            Debug.Assert(_levelValueLabel != null, "_levelValueLabel element 를 찾지 못했습니다");

            Debug.Assert(_contentLabel != null, "_contentLabel element 를 찾지 못했습니다");
            Debug.Assert(_abilityLabel != null, "_abilityLabel element 를 찾지 못했습니다");

            Debug.Assert(_upgradeButton != null, "_upgradeButton element 를 찾지 못했습니다");
            Debug.Assert(_upgradeAssetIcon != null, "_upgradeAssetIcon element 를 찾지 못했습니다");
            Debug.Assert(_upgradeValueLabel != null, "_upgradeValueLabel element 를 찾지 못했습니다");

            Debug.Assert(_inactivatePanel != null, "_inactivatePanel element 를 찾지 못했습니다");


            //_icon

            _nameLabel.text = "이름";
            _levelValueLabel.text = "1";
            _contentLabel.text = "설명";
            _abilityLabel.text = "능력";

            _upgradeButton.RegisterCallback<ClickEvent>(OnUpgradeEvent);

            _activatePanel.style.display = DisplayStyle.None;
            _inactivatePanel.style.display = DisplayStyle.Flex;

        }


        public void RefreshBlacksmithLine(BlacksmithEntity entity)
        {
            if (_activatePanel.style.display == DisplayStyle.None)
            {
                _activatePanel.style.display = DisplayStyle.Flex;
                _inactivatePanel.style.display = DisplayStyle.None;
            }

            _entity = entity;

            _nameLabel.text = entity.Name;
            _levelValueLabel.text = entity.UpgradeValue.ToString();

//            _uiFillable.FillAmount = nowTime / unitData.ProductTime;

            _upgradeValueLabel.text = entity.UpgradeAssetData.GetValue();
        }

        public void RefreshAssetEntity(AssetEntity assetEntity)
        {
            var isEnough = assetEntity.IsEnough(_entity.UpgradeAssetData);
            _upgradeButton.SetEnabled(isEnough);
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
    public class UIBlacksmithLine_Test : MonoBehaviour
    {
        private UIBlacksmithLine _instance;
        public UIBlacksmithLine Instance => _instance;

        public static UIBlacksmithLine_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UIBlacksmithLine_Test";
            return obj.AddComponent<UIBlacksmithLine_Test>();
        }

        public void Initialize()
        {
            var root = UIUXML.GetVisualElement(gameObject, UIBlacksmithLine.PATH_UI_UXML);
            _instance = root.Q<UIBlacksmithLine>();
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
