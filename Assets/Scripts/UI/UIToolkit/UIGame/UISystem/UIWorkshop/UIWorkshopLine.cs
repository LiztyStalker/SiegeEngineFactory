#if INCLUDE_UI_TOOLKIT
namespace SEF.UI.Toolkit
{
    using Entity;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Data;
    using System.Collections.Generic;

    public class UIWorkshopLine : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<UIWorkshopLine, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UISystem/UIWorkshop/UIWorkshopLine.uxml";
        internal static readonly string PATH_UI_USS = "Assets/Scripts/UI/UIGame/UISystem/UIWorkshop/UIWorkshopLine.uss";

        private int _index;

        private VisualElement _activatePanel;

        private VisualElement _icon;

        private Label _nameLabel;
        private Label _groupLabel;
        private Label _typeLabel;

        private Label _levelValueLabel;
        private Label _dpsSlideLabel;
        private Label _dpsLabel;
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

        //private UIFillable _uiFillable;

        private Button _upgradeButton;
        private VisualElement _upgradeAssetIcon;
        private Label _upgradeValueLabel;
        private Label _buttonLabel;
        //        private Button _techButton;



        private VisualElement _inactivatePanel;
        private Button _expendButton;
        private VisualElement _expendAssetIcon;
        private Label _expendValueLabel;

        private VisualElement _techPanel;
        private VisualElement _techLayout;
        private Button _techCancelButton;

        private List<Button> _techButtons = new List<Button>();

        public void SetIndex(int index) => _index = index;

        public static UIWorkshopLine Create()
        {
             return UIUXML.GetVisualElement<UIWorkshopLine>(PATH_UI_UXML);
        }

        public void Initialize()
        {
            _activatePanel = this.Q<VisualElement>("activate_panel");

            _icon = this.Q<VisualElement>("asset_icon");

            _nameLabel = this.Q<Label>("unit_name_label");
            _groupLabel = this.Q<Label>("unit_group_label");
            _typeLabel = this.Q<Label>("unit_type_label");

            _levelValueLabel = this.Q<Label>("unit_lv_value_Label");

            _dpsSlideLabel = this.Q<Label>("dps-slide");
            _dpsLabel = this.Q<Label>("dps-label");
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

            //_uiFillable = this.Q<UIFillable>();

            _upgradeButton = this.Q<Button>("upgrade_button");
            _upgradeAssetIcon = this.Q<VisualElement>("upgrade_asset_icon");
            _upgradeValueLabel = this.Q<Label>("upgrade_asset_value_label");
            _buttonLabel = this.Q<Label>("upgrade_label");
            //_techButton = this.Q<Button>("tech_button");



            _inactivatePanel = this.Q<VisualElement>("inactivate_panel");
            _expendButton = this.Q<Button>("expend_button");
            _expendAssetIcon = this.Q<VisualElement>("expend_asset_icon");
            _expendValueLabel = this.Q<Label>("expend_asset_value_label");

            _techPanel = this.Q<VisualElement>("ui-workshop-tech-panel");
            _techLayout = this.Q<VisualElement>("ui-workshop-tech-layout");
            _techCancelButton = this.Q<Button>("ui-workshop-tech-cancel_button");

            Debug.Assert(_activatePanel != null, "_activatePanel element ?? ???? ??????????");
            Debug.Assert(_icon != null, "icon element ?? ???? ??????????");
            Debug.Assert(_nameLabel != null, "_nameLabel element ?? ???? ??????????");
            Debug.Assert(_groupLabel != null, "_groupLabel element ?? ???? ??????????");
            Debug.Assert(_typeLabel != null, "_typeLabel element ?? ???? ??????????");
            Debug.Assert(_levelValueLabel != null, "_levelValueLabel element ?? ???? ??????????");

            Debug.Assert(_dpsSlideLabel != null, "_dpsSlideLabel element ?? ???? ??????????");
            Debug.Assert(_dpsLabel != null, "_dpsLabel element ?? ???? ??????????");
            Debug.Assert(_dpsValueLabel != null, "_dpsValueLabel element ?? ???? ??????????");
            Debug.Assert(_dpsUpLabel != null, "_dpsUpLabel element ?? ???? ??????????");

            Debug.Assert(_healthLabel != null, "_healthLabel element ?? ???? ??????????");
            Debug.Assert(_healthValueLabel != null, "_healthValueLabel element ?? ???? ??????????");
            Debug.Assert(_attackLabel != null, "_attackLabel element ?? ???? ??????????");
            Debug.Assert(_attackValueLabel != null, "_attackValueLabel element ?? ???? ??????????");
            Debug.Assert(_productLabel != null, "_productLabel element ?? ???? ??????????");
            Debug.Assert(_productValueLabel != null, "_productValueLabel element ?? ???? ??????????");
            Debug.Assert(_attackDelayLabel != null, "_attackDelayLabel element ?? ???? ??????????");
            Debug.Assert(_attackDelayValueLabel != null, "_attackDelayValueLabel element ?? ???? ??????????");
            Debug.Assert(_attackCountLabel != null, "_attackCountLabel element ?? ???? ??????????");
            Debug.Assert(_attackCountValueLabel != null, "_attackCountValueLabel element ?? ???? ??????????");
            Debug.Assert(_attackTypeLabel != null, "_attackTypeLabel element ?? ???? ??????????");
            Debug.Assert(_attackTypeValueLabel != null, "_attackTypeValueLabel element ?? ???? ??????????");
            Debug.Assert(_upgradeButton != null, "_upgradeButton element ?? ???? ??????????");
            Debug.Assert(_upgradeAssetIcon != null, "_upgradeAssetIcon element ?? ???? ??????????");
            Debug.Assert(_upgradeValueLabel != null, "_upgradeValueLabel element ?? ???? ??????????");
            //Debug.Assert(_techButton != null, "_techButton element ?? ???? ??????????");
            //Debug.Assert(_uiFillable != null, "_uiProgressbar element ?? ???? ??????????");

            Debug.Assert(_inactivatePanel != null, "_inactivatePanel element ?? ???? ??????????");
            Debug.Assert(_expendButton != null, "_expendButton element ?? ???? ??????????");
            Debug.Assert(_expendAssetIcon != null, "_expendAssetIcon element ?? ???? ??????????");
            Debug.Assert(_expendValueLabel != null, "_expendValueLabel element ?? ???? ??????????");


            Debug.Assert(_techPanel != null, "_techPanel element ?? ???? ??????????");
            Debug.Assert(_techLayout != null, "_techLayout element ?? ???? ??????????");

            //_icon

            _nameLabel.text = "????";
            _groupLabel.text = "????";
            _typeLabel.text = "????";
            _healthLabel.text = "????";
            _healthValueLabel.text = "0";
            _healthUpLabel.text = "(0)";
            _attackLabel.text = "????";
            _attackValueLabel.text = "0";
            _attackUpLabel.text = "(0)";
            _productLabel.text = "????";
            _productValueLabel.text = "1.000s";
            _attackDelayLabel.text = "??????????";
            _attackDelayValueLabel.text = "1.000s";
            _attackCountLabel.text = "????????";
            _attackCountValueLabel.text = "1";
            _attackTypeLabel.text = "????????";
            _attackTypeValueLabel.text = "????";
            //_uiFillable.FillAmount = 0;

            _dpsValueLabel.text = "0";
            _dpsUpLabel.text = "(0)";

            //_expendAssetIcon = Texture
            _expendValueLabel.text = "0";

            _levelValueLabel.text = "1";

            _upgradeButton.RegisterCallback<ClickEvent>(OnUpgradeEvent);
            //_techButton.RegisterCallback<ClickEvent>(OnUpTechEvent);
            _expendButton.RegisterCallback<ClickEvent>(OnExpendEvent);

            _techCancelButton.RegisterCallback<ClickEvent>(OnCancelTechEvent);


            _activatePanel.style.display = DisplayStyle.None;
            _inactivatePanel.style.display = DisplayStyle.Flex;

            _attackUpLabel.style.display = DisplayStyle.None;
            _healthUpLabel.style.display = DisplayStyle.None;
            _dpsSlideLabel.style.display = DisplayStyle.None;
            _dpsLabel.style.display = DisplayStyle.None;
            _dpsValueLabel.style.display = DisplayStyle.None;
            _dpsUpLabel.style.display = DisplayStyle.None;

            HideTechSelector();
        }


        private UnitEntity _entity;
        //UI?? ?????????? ?????? ?????? ????
        //?????? WorkshopManager???? ???????? ???? ?????? ??
        //RefreshAssetEntity -> RefreshAssetData?? ???? ????

        private bool isUpgrade = false;
        private bool isEndTech = false;

        public void RefreshUnit(UnitEntity entity, float nowTime)
        {
            if(_activatePanel.style.display == DisplayStyle.None)
            {
                _activatePanel.style.display = DisplayStyle.Flex;
                _inactivatePanel.style.display = DisplayStyle.None;
            }

            _entity = entity;

            var unitData = entity.UnitData;
            _nameLabel.text = unitData.name;
            _groupLabel.text = unitData.Group.ToString();
            _healthValueLabel.text = _entity.HealthData.GetValue();
            _attackValueLabel.text = _entity.DamageData.GetValue();
            _productValueLabel.text = $"{unitData.ProductTime}s";
            _attackDelayValueLabel.text = $"{unitData.AttackDelay}s";
            _attackCountValueLabel.text = unitData.AttackCount.ToString();
            _attackTypeValueLabel.text = unitData.TypeAttackRange.ToString();

            _levelValueLabel.text = $"{entity.NowUpgradeValue}/{entity.MaxUpgradeValue}";

            //_uiFillable.FillAmount = nowTime / unitData.ProductTime;

            //_upgradeValueLabel.text = _unitEntity.UpgradeAssetData.GetValue();



            isEndTech = false;

            if (entity.IsMaxUpgrade())
            {
                isUpgrade = false;

                //???? ???? ????
                if (entity.IsNextTech())
                {
                    _upgradeValueLabel.text = "-";//entity.TechAssetData.GetValue();
                    _buttonLabel.text = "????";
                }
                //???? ????
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
                _buttonLabel.text = "??????????";
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
                    //?????? ???? ????
                    isEnough = true;
                }
                //?? ???? ???? ??????
                //else
                //{
                //    isEnough = assetEntity.IsEnough(_entity.TechAssetData);
                //}
            }
            _upgradeButton.SetEnabled(isEnough);

            //Debug.Log("Refresh " + assetEntity);
//            var isEnough = assetEntity.IsEnough(_entity.UpgradeAssetData);
//            _upgradeButton.SetEnabled(isEnough);
        }

        public void RefreshExpend(IAssetData assetData, bool isActive)
        {
            //_expendAssetIcon = 
            _expendValueLabel.text = assetData.GetValue();
            _expendButton.SetEnabled(isActive);
        }

        public void CleanUp()
        {
            _upgradeButton.UnregisterCallback<ClickEvent>(OnUpgradeEvent);
            //_techButton.UnregisterCallback<ClickEvent>(OnUpTechEvent);
            _expendButton.UnregisterCallback<ClickEvent>(OnExpendEvent);
            _icon = null;
        }


        private void ShowTechSelector(UnitTechData[] arr)
        {
            for (int i = 0; i < _techButtons.Count; i++)
            {
                _techButtons[i].style.display = DisplayStyle.None;
            }

            for (int i = 0; i < arr.Length; i++)
            {
                if(i >= _techButtons.Count)
                {
                    var button = new Button();
                    button.focusable = true;
                    _techLayout.Add(button);
                    _techButtons.Add(button);
                    button.RegisterCallback<ClickEvent>(e => OnUpTechEvent(button));
                }
                _techButtons[i].text = arr[i].TechUnitKey;
                _techButtons[i].style.display = DisplayStyle.Flex;
            }


            _techPanel.style.display = DisplayStyle.Flex;
        }

        private void HideTechSelector()
        {
            _techPanel.style.display = DisplayStyle.None;
        }

#region ##### Listener #####


        private System.Action<int> _upgradeEvent;
        public void AddUpgradeListener(System.Action<int> act) => _upgradeEvent += act;
        public void RemoveUpgradeListener(System.Action<int> act) => _upgradeEvent -= act;
        private void OnUpgradeEvent(ClickEvent e)
        {
            if (!(isUpgrade || isEndTech))
            {
                //?????? ??????
                ShowTechSelector(_entity.UnitData.UnitTechDataArray);
            }
            else
                _upgradeEvent?.Invoke(_index);
        }


        private System.Action<int, UnitTechData> _uptechEvent;
        public void AddUpTechListener(System.Action<int, UnitTechData> act) => _uptechEvent += act;
        public void RemoveUpTechListener(System.Action<int, UnitTechData> act) => _uptechEvent -= act;
        private void OnUpTechEvent(Button button) 
        {

            for (int i = 0; i < _techButtons.Count; i++)
            {
                if(_techButtons[i] == button)
                {
                    _uptechEvent?.Invoke(_index, _entity.UnitData.UnitTechDataArray[i]);
                }
            }


            //_uptechEvent?.Invoke(_index, UnitData.Create_Test());
            HideTechSelector();
        }

        private System.Action _expendEvent;
        public void AddExpendListener(System.Action act) => _expendEvent += act;
        public void RemoveExpendListener(System.Action act) => _expendEvent -= act;
        private void OnExpendEvent(ClickEvent e)
        {
            _expendEvent?.Invoke();
        }

        private void OnCancelTechEvent(ClickEvent e)
        {
            HideTechSelector();
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
            var root = UIUXML.GetVisualElement(gameObject, UIWorkshopLine.PATH_UI_UXML);
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
#endif