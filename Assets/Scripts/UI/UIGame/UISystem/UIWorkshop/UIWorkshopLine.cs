namespace SEF.UI.Toolkit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Entity;

    public class UIWorkshopLine : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<UIWorkshopLine, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        internal static readonly string PATH_UI_WORKSHOP_LINE_UXML = "Assets/Scripts/UI/UIGame/UISystem/UIWorkshop/UIWorkshopLineUXML.uxml";

        private VisualElement _icon;

        private Label _nameLabel;
        private Label _groupLabel;
        private Label _typeLabel;

        private Label _dpsValueLabel;
        private Label _dpsUpLabel;

        private Label _healthLabel;
        private Label _healthValueLabel;
        private Label _healthUpLabel;

        private Label _attackLabel;
        private Label _attackValueLabel;
        private Label _attackUpLabel;

        private Label _productLabel;
        private Label _productValueLabel;
        private Label _attackDelayLabel;
        private Label _attackDelayValueLabel;
        private Label _attackCountLabel;
        private Label _attackCountValueLabel;
        private Label _attackTypeLabel;
        private Label _attackTypeValueLabel;

        private UIProgressbar _uiProgressbar;

        private Button _upgradeButton;
        private VisualElement _upgradeAssetIcon;
        private Label _upgradeValueLabel;
        private Button _techButton;

        public static UIWorkshopLine Create()
        {
             return UIUXML.GetVisualElement<UIWorkshopLine>(PATH_UI_WORKSHOP_LINE_UXML);
        }

        public void Initialize()
        {

            _icon = this.Q<VisualElement>("asset_icon");

            _nameLabel = this.Q<Label>("unit_name_label");
            _groupLabel = this.Q<Label>("unit_group_label");
            _typeLabel = this.Q<Label>("unit_type_label");

            _dpsValueLabel = this.Q<Label>("dps_value_label");
            _dpsUpLabel = this.Q<Label>("dps_up_label");

            _healthLabel = this.Q<Label>("health_label");
            _healthValueLabel = this.Q<Label>("health_value_label");
            _healthUpLabel = this.Q<Label>("health_up_label");

            _attackLabel = this.Q<Label>("attack_label");
            _attackValueLabel = this.Q<Label>("attack_value_label");
            _attackUpLabel = this.Q<Label>("attack_up_label");

            _productLabel = this.Q<Label>("product_label");
            _productValueLabel = this.Q<Label>("product_value_label");

            _attackTypeLabel = this.Q<Label>("attacktype_label");
            _attackTypeValueLabel = this.Q<Label>("attacktype_value_label");

            _attackCountLabel = this.Q<Label>("attack_count_label");
            _attackCountValueLabel = this.Q<Label>("attack_count_value_label");

            _attackDelayLabel = this.Q<Label>("attack_delay_label");
            _attackDelayValueLabel = this.Q<Label>("attack_delay_value_label");

            _uiProgressbar = this.Q<UIProgressbar>();

            _upgradeButton = this.Q<Button>("upgrade_button");
            _upgradeAssetIcon = this.Q<VisualElement>("upgrade_asset_icon");
            _upgradeValueLabel = this.Q<Label>("upgrade_asset_value_label");
            _techButton = this.Q<Button>("tech_button");

            Debug.Assert(_icon != null, "icon element �� ã�� ���߽��ϴ�");
            Debug.Assert(_nameLabel != null, "_nameLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_groupLabel != null, "_groupLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_typeLabel != null, "_typeLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_healthLabel != null, "_healthLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_healthValueLabel != null, "_healthValueLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_attackLabel != null, "_attackLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_attackValueLabel != null, "_attackValueLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_productLabel != null, "_productLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_productValueLabel != null, "_productValueLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_attackDelayLabel != null, "_attackDelayLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_attackDelayValueLabel != null, "_attackDelayValueLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_attackCountLabel != null, "_attackCountLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_attackCountValueLabel != null, "_attackCountValueLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_attackTypeLabel != null, "_attackTypeLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_attackTypeValueLabel != null, "_attackTypeValueLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_upgradeButton != null, "_upgradeButton element �� ã�� ���߽��ϴ�");
            Debug.Assert(_upgradeAssetIcon != null, "_upgradeAssetIcon element �� ã�� ���߽��ϴ�");
            Debug.Assert(_upgradeValueLabel != null, "_upgradeValueLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_techButton != null, "_techButton element �� ã�� ���߽��ϴ�");
            Debug.Assert(_uiProgressbar != null, "_uiProgressbar element �� ã�� ���߽��ϴ�");


            //_icon

            _nameLabel.text = "�̸�";
            _groupLabel.text = "�׷�";
            _typeLabel.text = "Ÿ��";
            _healthLabel.text = "ü��";
            _healthValueLabel.text = "0";
            _attackLabel.text = "����";
            _attackValueLabel.text = "0";
            _productLabel.text = "����";
            _productValueLabel.text = "1.000s";
            _attackDelayLabel.text = "���ݵ�����";
            _attackDelayValueLabel.text = "1.000s";
            _attackCountLabel.text = "����Ƚ��";
            _attackCountValueLabel.text = "1";
            _attackTypeLabel.text = "����Ÿ��";
            _attackTypeValueLabel.text = "�Ϲ�";
            _uiProgressbar.FillAmount = 0;

            _upgradeButton.RegisterCallback<ClickEvent>(OnUpgradeEvent);
            _techButton.RegisterCallback<ClickEvent>(OnUpTechEvent);
        }

        public void RefreshUnit(UnitEntity unitEntity, float nowTime)
        {
            var unitData = unitEntity.UnitData;
            _nameLabel.text = unitData.name;
            _groupLabel.text = unitData.Group.ToString();
            _healthValueLabel.text = unitData.HealthValue.GetValue();
            _attackValueLabel.text = unitData.AttackValue.GetValue();
            _productValueLabel.text = unitData.ProductTime.ToString();
            _attackDelayValueLabel.text = unitData.AttackDelay.ToString();
            _attackCountValueLabel.text = unitData.AttackCount.ToString();
            _attackTypeValueLabel.text = unitData.TypeAttackRange.ToString();

            _uiProgressbar.FillAmount = nowTime / unitData.ProductTime;
        }

        public void CleanUp()
        {
            _upgradeButton.UnregisterCallback<ClickEvent>(OnUpgradeEvent);
            _techButton.UnregisterCallback<ClickEvent>(OnUpTechEvent);
            _icon = null;
        }



        #region ##### Listener #####
        private void OnUpgradeEvent(ClickEvent e)
        {
            Debug.Log("OnUpgradeEvent");
        }

        private void OnUpTechEvent(ClickEvent e) 
        {
            Debug.Log("OnUpTechEvent");
        }
        #endregion
    }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    [RequireComponent(typeof(UIDocument))]
    public class UIWorkshopLine_Test : MonoBehaviour
    {
        private UIWorkshopLine _instance;
        public UIWorkshopLine Instance => _instance;

        public static UIWorkshopLine_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UIWorkshopLine_Test";
            return obj.AddComponent<UIWorkshopLine_Test>();
        }

        public void Initialize()
        {
            var root = UIUXML.GetVisualElement(gameObject, UIWorkshopLine.PATH_UI_WORKSHOP_LINE_UXML);
            _instance = root.Q<UIWorkshopLine>();
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