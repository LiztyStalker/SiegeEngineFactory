namespace SEF.UI.Toolkit { 
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;
    using Entity;
    using SEF.Data;

    public class UISmithyLine : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<UISmithyLine, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UISystem/UISmithy/UISmithyLine.uxml";
        internal static readonly string PATH_UI_USS = "Assets/Scripts/UI/UIGame/UISystem/UISmithy/UISmithyLine.uss";


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
        private Label _buttonLabel;

        private VisualElement _inactivatePanel;
        private Label _inactivateLabel;

        public void SetIndex(int index) => _index = index;

        private SmithyEntity _entity;

        public static UISmithyLine Create()
        {
            return UIUXML.GetVisualElement<UISmithyLine>(PATH_UI_UXML, PATH_UI_USS);
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
            _buttonLabel = this.Q<Label>("upgrade-label");
            _inactivatePanel = this.Q<VisualElement>("inactivate-panel");

            Debug.Assert(_activatePanel != null, "_activatePanel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_icon != null, "icon element �� ã�� ���߽��ϴ�");
            Debug.Assert(_nameLabel != null, "_nameLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_levelValueLabel != null, "_levelValueLabel element �� ã�� ���߽��ϴ�");

            Debug.Assert(_contentLabel != null, "_contentLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_abilityLabel != null, "_abilityLabel element �� ã�� ���߽��ϴ�");

            Debug.Assert(_upgradeButton != null, "_upgradeButton element �� ã�� ���߽��ϴ�");
            Debug.Assert(_upgradeAssetIcon != null, "_upgradeAssetIcon element �� ã�� ���߽��ϴ�");
            Debug.Assert(_upgradeValueLabel != null, "_upgradeValueLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_buttonLabel != null, "_buttonLabel element �� ã�� ���߽��ϴ�");

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




        private bool isUpgrade = false;
        private bool isEndTech = false;

        public void RefreshSmithyLine(SmithyEntity entity)
        {
            if (_activatePanel.style.display == DisplayStyle.None)
            {
                _activatePanel.style.display = DisplayStyle.Flex;
                _inactivatePanel.style.display = DisplayStyle.None;
            }

            _entity = entity;

            _nameLabel.text = entity.Name;
            _levelValueLabel.text = entity.NowUpgradeValue.ToString();
            _contentLabel.text = entity.Content;
            _abilityLabel.text = entity.Ability;

            //            _uiFillable.FillAmount = nowTime / unitData.ProductTime;

            //MaxUpgrade�̸� ��ũ�� �����
            isEndTech = false;
            if (entity.IsMaxUpgrade())
            {
                isUpgrade = false;

                //���� ��ũ ����
                if (entity.IsNextTech())
                {
                    _upgradeValueLabel.text = entity.TechAssetData.GetValue();
                    _buttonLabel.text = "��ũ";
                }
                //���� ��ũ
                else
                {
                    isEndTech = true;
                    _upgradeValueLabel.text = "-";
                    _buttonLabel.text = "-";
                }
            }
            else
            {
                isUpgrade = true;
                _upgradeValueLabel.text = entity.UpgradeAssetData.GetValue();
                _buttonLabel.text = "���׷��̵�";
            }
        }

        public void RefreshAssetEntity(AssetPackage assetEntity)
        {
            bool isEnough = false;
            //
            if (!isEndTech)
            {
                if (isUpgrade)
                {
                    isEnough = assetEntity.IsEnough(_entity.UpgradeAssetData);
                }
                else
                {
                    isEnough = assetEntity.IsEnough(_entity.TechAssetData);
                }
            }
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
            if (!(isUpgrade || isEndTech))
                OnUpTechEvent();
            else
                _upgradeEvent?.Invoke(_index);

        }


        private System.Action<int> _uptechEvent;
        public void AddOnUpTechListener(System.Action<int> act) => _uptechEvent += act;
        public void RemoveOnUpTechListener(System.Action<int> act) => _uptechEvent -= act;
        private void OnUpTechEvent()
        {
            UICommon.Current.ShowPopup("��ũ�� �����Ͻðڽ��ϱ�?", "��", "�ƴϿ�", () => {
                _uptechEvent?.Invoke(_index);
                Debug.Log("OK");
            });
        }

        #endregion
    }

    #if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    [RequireComponent(typeof(UIDocument))]
    public class UISmithyLine_Test : MonoBehaviour
    {
        private UISmithyLine _instance;
        public UISmithyLine Instance => _instance;

        public static UISmithyLine_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UISmithyLine_Test";
            return obj.AddComponent<UISmithyLine_Test>();
        }

        public void Initialize()
        {
            var root = UIUXML.GetVisualElement(gameObject, UISmithyLine.PATH_UI_UXML);
            _instance = root.Q<UISmithyLine>();
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
