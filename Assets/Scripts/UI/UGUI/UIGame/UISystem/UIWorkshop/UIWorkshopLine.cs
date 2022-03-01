namespace SEF.UI
{
    using Entity;
    using UnityEngine;
    using UnityEngine.UI;
    using Data;
    using System.Collections.Generic;

    public class UIWorkshopLine : MonoBehaviour
    {

        private int _index;

        [SerializeField]
        private GameObject _activatePanel;

        [SerializeField]
        private Image _icon;

        [SerializeField]
        private Text _nameLabel;
        [SerializeField]
        private Text _groupLabel;
        [SerializeField]
        private Text _typeLabel;

        [SerializeField]
        private Text _levelValueLabel;
        [SerializeField]
        private Text _dpsSlideLabel;
        [SerializeField]
        private Text _dpsLabel;
        [SerializeField]
        private Text _dpsValueLabel;
        [SerializeField]
        private Text _dpsUpLabel;

        [SerializeField]
        private Text _healthLabel;
        [SerializeField]
        private Text _healthValueLabel;
        [SerializeField]
        private Text _healthUpLabel;

        [SerializeField]
        private Text _attackLabel;
        [SerializeField]
        private Text _attackValueLabel;
        [SerializeField]
        private Text _attackUpLabel;

        [SerializeField]
        private Text _productLabel;
        [SerializeField]
        private Text _productValueLabel;
        [SerializeField]
        private Text _attackDelayLabel;
        [SerializeField]
        private Text _attackDelayValueLabel;
        [SerializeField]
        private Text _attackCountLabel;
        [SerializeField]
        private Text _attackCountValueLabel;
        [SerializeField]
        private Text _attackTypeLabel;
        [SerializeField]
        private Text _attackTypeValueLabel;

        //private UIFillable _uiFillable;

        [SerializeField]
        private Button _upgradeButton;
        [SerializeField]
        private Image _upgradeAssetIcon;
        [SerializeField]
        private Text _upgradeValueLabel;
        [SerializeField]
        private Text _buttonLabel;
        //        private Button _techButton;



        [SerializeField]
        private GameObject _inactivatePanel;
        [SerializeField]
        private Button _expendButton;
        [SerializeField]
        private Image _expendAssetIcon;
        [SerializeField]
        private Text _expendValueLabel;

        [SerializeField]
        private GameObject _techPanel;
        [SerializeField]
        private GameObject _techLayout;
        [SerializeField]
        private Button _techCancelButton;

        private List<Button> _techButtons = new List<Button>();

        public void SetIndex(int index) => _index = index;

        public static UIWorkshopLine Create()
        {
            var obj = new GameObject();
            obj.name = "UI@WorkshopLine";
            obj.AddComponent<RectTransform>();
            return obj.AddComponent<UIWorkshopLine>();
        }

        public void Initialize()
        {
            Debug.Assert(_activatePanel != null, "_activatePanel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_icon != null, "icon element �� ã�� ���߽��ϴ�");
            Debug.Assert(_nameLabel != null, "_nameLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_groupLabel != null, "_groupLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_typeLabel != null, "_typeLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_levelValueLabel != null, "_levelValueLabel element �� ã�� ���߽��ϴ�");

            Debug.Assert(_dpsSlideLabel != null, "_dpsSlideLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_dpsLabel != null, "_dpsLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_dpsValueLabel != null, "_dpsValueLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_dpsUpLabel != null, "_dpsUpLabel element �� ã�� ���߽��ϴ�");

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
            //Debug.Assert(_techButton != null, "_techButton element �� ã�� ���߽��ϴ�");
            //Debug.Assert(_uiFillable != null, "_uiProgressbar element �� ã�� ���߽��ϴ�");

            Debug.Assert(_inactivatePanel != null, "_inactivatePanel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_expendButton != null, "_expendButton element �� ã�� ���߽��ϴ�");
            Debug.Assert(_expendAssetIcon != null, "_expendAssetIcon element �� ã�� ���߽��ϴ�");
            Debug.Assert(_expendValueLabel != null, "_expendValueLabel element �� ã�� ���߽��ϴ�");


            Debug.Assert(_techPanel != null, "_techPanel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_techLayout != null, "_techLayout element �� ã�� ���߽��ϴ�");

            //_icon

            _nameLabel.text = "�̸�";
            _groupLabel.text = "�׷�";
            _typeLabel.text = "Ÿ��";
            _healthLabel.text = "ü��";
            _healthValueLabel.text = "0";
            _healthUpLabel.text = "(0)";
            _attackLabel.text = "����";
            _attackValueLabel.text = "0";
            _attackUpLabel.text = "(0)";
            _productLabel.text = "����";
            _productValueLabel.text = "1.000s";
            _attackDelayLabel.text = "���ݵ�����";
            _attackDelayValueLabel.text = "1.000s";
            _attackCountLabel.text = "����Ƚ��";
            _attackCountValueLabel.text = "1";
            _attackTypeLabel.text = "����Ÿ��";
            _attackTypeValueLabel.text = "�Ϲ�";
            //_uiFillable.FillAmount = 0;

            _dpsValueLabel.text = "0";
            _dpsUpLabel.text = "(0)";

            //_expendAssetIcon = Texture
            _expendValueLabel.text = "0";

            _levelValueLabel.text = "1";


            _upgradeButton.onClick.AddListener(OnUpgradeEvent);
            _expendButton.onClick.AddListener(OnUpgradeEvent);
            _techCancelButton.onClick.AddListener(OnUpgradeEvent);

            _activatePanel.SetActive(false);
            _inactivatePanel.SetActive(true);

            _attackUpLabel.gameObject.SetActive(false);
            _healthUpLabel.gameObject.SetActive(false);
            _dpsSlideLabel.gameObject.SetActive(false);
            _dpsLabel.gameObject.SetActive(false);
            _dpsValueLabel.gameObject.SetActive(false);
            _dpsUpLabel.gameObject.SetActive(false);

            HideTechSelector();
        }


        private UnitEntity _entity;
        //UI�� ������Ʈ�� ������ ������ �ȵ�
        //���Ŀ� WorkshopManager���� �������� ���� ��ǥ�� ��
        //RefreshAssetEntity -> RefreshAssetData�� ���� ����

        private bool isUpgrade = false;
        private bool isEndTech = false;

        public void RefreshUnit(UnitEntity entity, float nowTime)
        {
            if (!_activatePanel.activeSelf)
            {
                _activatePanel.SetActive(true);
                _inactivatePanel.SetActive(false);
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

                //���� ��ũ ����
                if (entity.IsNextTech())
                {
                    _upgradeValueLabel.text = "-";//entity.TechAssetData.GetValue();
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
                    //������ ��ũ ����
                    isEnough = true;
                }
                //�� ��ư Ȱ�� ��Ȱ��
                //else
                //{
                //    isEnough = assetEntity.IsEnough(_entity.TechAssetData);
                //}
            }

            _upgradeButton.interactable = isEnough;
//            _upgradeButton.SetEnabled(isEnough);

            //Debug.Log("Refresh " + assetEntity);
//            var isEnough = assetEntity.IsEnough(_entity.UpgradeAssetData);
//            _upgradeButton.SetEnabled(isEnough);
        }

        public void RefreshExpend(IAssetData assetData, bool isActive)
        {
            //_expendAssetIcon = 
            _expendValueLabel.text = assetData.GetValue();
            _expendButton.interactable = isActive;
//            _expendButton.SetEnabled(isActive);
        }

        public void CleanUp()
        {
            _upgradeButton.onClick.RemoveListener(OnUpgradeEvent);
            _expendButton.onClick.RemoveListener(OnExpendEvent);
            _techCancelButton.onClick.RemoveListener(OnCancelTechEvent);

            _icon = null;
        }


        private void ShowTechSelector(UnitTechData[] arr)
        {
            for (int i = 0; i < _techButtons.Count; i++)
            {
                _techButtons[i].gameObject.SetActive(false);
//                _techButtons[i].style.display = DisplayStyle.None;
            }

            for (int i = 0; i < arr.Length; i++)
            {
                if(i >= _techButtons.Count)
                {
                    var button = CreateButton();
                    button.transform.SetParent(_techLayout.transform);
                    _techButtons.Add(button);
                    button.onClick.AddListener(() => OnUpTechEvent(button));
                }
                _techButtons[i].GetComponentInChildren<Text>().text = arr[i].TechUnitKey;
                _techButtons[i].gameObject.SetActive(true);
            }
            _techPanel.SetActive(true);
        }

        private Button CreateButton()
        {
            var obj = new GameObject();
            obj.name = "Btn@Tech";
            obj.AddComponent<RectTransform>();
            return obj.AddComponent<Button>();
        }

        private void HideTechSelector()
        {
            _techPanel.SetActive(false);
        }

#region ##### Listener #####


        private System.Action<int> _upgradeEvent;
        public void AddUpgradeListener(System.Action<int> act) => _upgradeEvent += act;
        public void RemoveUpgradeListener(System.Action<int> act) => _upgradeEvent -= act;
        private void OnUpgradeEvent()
        {
            if (!(isUpgrade || isEndTech))
            {
                //��ũâ ������
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
        private void OnExpendEvent()
        {
            _expendEvent?.Invoke();
        }

        private void OnCancelTechEvent()
        {
            HideTechSelector();
        }

#endregion
    }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
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
            _instance = UIWorkshopLine.Create();
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