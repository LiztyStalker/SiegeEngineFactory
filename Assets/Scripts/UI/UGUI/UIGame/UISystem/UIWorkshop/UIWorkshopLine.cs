namespace SEF.UI
{
    using Entity;
    using UnityEngine;
    using UnityEngine.UI;
    using Data;
    using System.Collections.Generic;

    public class UIWorkshopLine : MonoBehaviour
    {
        private readonly static string UGUI_NAME = "UI@WorkshopLine";

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
        private Slider _productSlider;

        [SerializeField]
        private Text _levelValueLabel;
        //[SerializeField]
        //private Text _dpsSlideLabel;
        //[SerializeField]
        //private Text _dpsLabel;
        //[SerializeField]
        //private Text _dpsValueLabel;
        //[SerializeField]
        //private Text _dpsUpLabel;

        //[SerializeField]
        //private Text _healthLabel;
        [SerializeField]
        private Text _healthValueLabel;
        //[SerializeField]
        //private Text _healthUpLabel;

        //[SerializeField]
        //private Text _attackLabel;
        [SerializeField]
        private Text _attackValueLabel;
        //[SerializeField]
        //private Text _attackUpLabel;

        //[SerializeField]
        //private Text _productLabel;
        [SerializeField]
        private Text _productValueLabel;
        //[SerializeField]
        //private Text _attackDelayLabel;
        [SerializeField]
        private Text _attackDelayValueLabel;
        //[SerializeField]
        //private Text _attackCountLabel;
        [SerializeField]
        private Text _attackCountValueLabel;
        //[SerializeField]
        //private Text _attackTypeLabel;
        [SerializeField]
        private Text _attackTypeValueLabel;

        //private UIFillable _uiFillable;

        [SerializeField]
        private UIAssetButton _upgradeButton;

        [SerializeField]
        private GameObject _techPanel;
        [SerializeField]
        private GameObject _techLayout;
        [SerializeField]
        private Button _techCancelButton;

        private List<UIAssetButton> _techButtons = new List<UIAssetButton>();

        public void SetIndex(int index) => _index = index;


        public static UIWorkshopLine Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UIWorkshopLine>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.name = UGUI_NAME;
                return obj.AddComponent<UIWorkshopLine>();
            }
#else
            Debug.LogWarning($"{UGUI_NAME}�� ã�� �� �����ϴ�");
            return null;
#endif
        }


        public void Initialize()
        {
            Debug.Assert(_activatePanel != null, "_activatePanel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_icon != null, "icon element �� ã�� ���߽��ϴ�");
            Debug.Assert(_nameLabel != null, "_nameLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_groupLabel != null, "_groupLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_typeLabel != null, "_typeLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_levelValueLabel != null, "_levelValueLabel element �� ã�� ���߽��ϴ�");

            //Debug.Assert(_healthLabel != null, "_healthLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_healthValueLabel != null, "_healthValueLabel element �� ã�� ���߽��ϴ�");
            //Debug.Assert(_attackLabel != null, "_attackLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_attackValueLabel != null, "_attackValueLabel element �� ã�� ���߽��ϴ�");
            //Debug.Assert(_productLabel != null, "_productLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_productValueLabel != null, "_productValueLabel element �� ã�� ���߽��ϴ�");
            //Debug.Assert(_attackDelayLabel != null, "_attackDelayLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_attackDelayValueLabel != null, "_attackDelayValueLabel element �� ã�� ���߽��ϴ�");
            //Debug.Assert(_attackCountLabel != null, "_attackCountLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_attackCountValueLabel != null, "_attackCountValueLabel element �� ã�� ���߽��ϴ�");
            //Debug.Assert(_attackTypeLabel != null, "_attackTypeLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_attackTypeValueLabel != null, "_attackTypeValueLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_upgradeButton != null, "_upgradeButton element �� ã�� ���߽��ϴ�");

            Debug.Assert(_techPanel != null, "_techPanel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_techLayout != null, "_techLayout element �� ã�� ���߽��ϴ�");

            //_icon

            _nameLabel.text = "�̸�";
            _groupLabel.text = "�׷�";
            _typeLabel.text = "Ÿ��";
            //_healthLabel.text = "ü��";
            _healthValueLabel.text = "0";
            //_healthUpLabel.text = "(0)";
            //_attackLabel.text = "����";
            _attackValueLabel.text = "0";
            //_attackUpLabel.text = "(0)";
            //_productLabel.text = "����";
            _productValueLabel.text = "1.000s";
            //_attackDelayLabel.text = "���ݵ�����";
            _attackDelayValueLabel.text = "1.000s";
            //_attackCountLabel.text = "����Ƚ��";
            _attackCountValueLabel.text = "1";
            //_attackTypeLabel.text = "����Ÿ��";
            _attackTypeValueLabel.text = "�Ϲ�";
            //_uiFillable.FillAmount = 0;

            //_dpsValueLabel.text = "0";
            //_dpsUpLabel.text = "(0)";

            //_expendAssetIcon = Texture

            _levelValueLabel.text = "1";


            _upgradeButton.onClick.AddListener(OnUpgradeEvent);
            _techCancelButton.onClick.AddListener(OnCancelTechEvent);

            _activatePanel.SetActive(true);

            //_attackUpLabel.gameObject.SetActive(false);
            //_healthUpLabel.gameObject.SetActive(false);
            //_dpsSlideLabel.gameObject.SetActive(false);
            //_dpsLabel.gameObject.SetActive(false);
            //_dpsValueLabel.gameObject.SetActive(false);
            //_dpsUpLabel.gameObject.SetActive(false);

            _upgradeButton.SetRepeat(true);

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

            _productSlider.value = nowTime / entity.ProductTime;

            //_upgradeValueLabel.text = _unitEntity.UpgradeAssetData.GetValue();



            isEndTech = false;

            if (entity.IsMaxUpgrade())
            {
                isUpgrade = false;

                //���� ��ũ ����
                if (entity.IsNextTech())
                {
                    _upgradeButton.SetLabel("��ũ");
                    _upgradeButton.SetEmpty();
                }
                //���� ��ũ
                else
                {
                    isEndTech = true;
                    _upgradeButton.SetLabel("-");
                    _upgradeButton.SetEmpty();
                }
            }
            else
            {
                isUpgrade = true;
                _upgradeButton.SetLabel("���׷��̵�");
                _upgradeButton.SetData(entity.UpgradeAssetData);
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


        public void CleanUp()
        {
            _upgradeButton.onClick.RemoveListener(OnUpgradeEvent);
            _techCancelButton.onClick.RemoveListener(OnCancelTechEvent);
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
                    var button = UIAssetButton.Create();
                    button.transform.SetParent(_techLayout.transform);
                    _techButtons.Add(button);
                    button.onClick.AddListener(() => OnUpTechEvent(button));
                }
                _techButtons[i].SetLabel(arr[i].TechUnitKey);
                _techButtons[i].SetData(arr[i].TechAssetData);
                _techButtons[i].gameObject.SetActive(true);
            }
            _techPanel.SetActive(true);
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

}