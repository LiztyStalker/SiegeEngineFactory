namespace SEF.UI
{
    using Data;
    using Entity;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class UIWorkshopLine : MonoBehaviour
    {
        private readonly static string UGUI_NAME = "UI@WorkshopLine";

        private int _index;

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

        [SerializeField]
        private Text _healthValueLabel;

        [SerializeField]
        private Text _attackValueLabel;

        [SerializeField]
        private Text _productValueLabel;
        [SerializeField]
        private Text _attackDelayValueLabel;
        [SerializeField]
        private Text _attackCountValueLabel;
        [SerializeField]
        private Text _attackTypeValueLabel;

        [SerializeField]
        private UIAssetButton _upgradeBtn;

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
            Debug.LogWarning($"{UGUI_NAME}을 찾을 수 없습니다");
            return null;
#endif
        }


        public void Initialize()
        {
            Debug.Assert(_icon != null, "icon element 를 찾지 못했습니다");
            Debug.Assert(_nameLabel != null, "_nameLabel element 를 찾지 못했습니다");
            Debug.Assert(_groupLabel != null, "_groupLabel element 를 찾지 못했습니다");
            Debug.Assert(_typeLabel != null, "_typeLabel element 를 찾지 못했습니다");
            Debug.Assert(_levelValueLabel != null, "_levelValueLabel element 를 찾지 못했습니다");

            Debug.Assert(_healthValueLabel != null, "_healthValueLabel element 를 찾지 못했습니다");
            Debug.Assert(_attackValueLabel != null, "_attackValueLabel element 를 찾지 못했습니다");
            Debug.Assert(_productValueLabel != null, "_productValueLabel element 를 찾지 못했습니다");
            Debug.Assert(_attackDelayValueLabel != null, "_attackDelayValueLabel element 를 찾지 못했습니다");
            Debug.Assert(_attackCountValueLabel != null, "_attackCountValueLabel element 를 찾지 못했습니다");
            Debug.Assert(_attackTypeValueLabel != null, "_attackTypeValueLabel element 를 찾지 못했습니다");
            Debug.Assert(_upgradeBtn != null, "_upgradeButton element 를 찾지 못했습니다");

            Debug.Assert(_techPanel != null, "_techPanel element 를 찾지 못했습니다");
            Debug.Assert(_techLayout != null, "_techLayout element 를 찾지 못했습니다");

            //_icon

            _nameLabel.text = "이름";
            _groupLabel.text = "그룹";
            _typeLabel.text = "";
            _healthValueLabel.text = "0";
            _attackValueLabel.text = "0";
            _productValueLabel.text = "1.000s";
            _attackDelayValueLabel.text = "1.000s";
            _attackCountValueLabel.text = "1";
            _attackTypeValueLabel.text = "일반";

            _levelValueLabel.text = "1";

            _upgradeBtn.onClick.AddListener(OnUpgradeEvent);
            _techCancelButton.onClick.AddListener(OnCancelTechEvent);

            _upgradeBtn.SetRepeat(true);

            HideTechSelector();
        }


        private UnitEntity _entity;
        //UI가 오브젝트를 가지고 있으면 안됨
        //차후에 WorkshopManager에서 가져오는 것을 목표로 함
        //RefreshAssetEntity -> RefreshAssetData로 변경 예정

        private bool isUpgrade = false;
        private bool isEndTech = false;
        private bool isUpgradable = false;

        public void RefreshUnit(UnitEntity entity, float nowTime)
        {
            _entity = entity;

            var unitData = entity.UnitData;
            _nameLabel.text = unitData.name;
            _groupLabel.text = unitData.Group.ToString();
            _healthValueLabel.text = $"체력 : { _entity.HealthData.GetValue()}";
            _attackValueLabel.text = $"공격력 : {_entity.DamageData.GetValue()}";
            _productValueLabel.text = $"생산시간 : {unitData.ProductTime}s";
            _attackDelayValueLabel.text = $"공격딜레이 : {unitData.AttackDelay}s";
            _attackCountValueLabel.text = $"공격횟수 {unitData.AttackCount}";
            _attackTypeValueLabel.text = $"공격타입 {unitData.TypeAttackRange}";

            _levelValueLabel.text = $"Lv : {entity.NowUpgradeValue}/{entity.MaxUpgradeValue}";

            _productSlider.value = nowTime / entity.ProductTime;

            isEndTech = false;

            if (entity.IsMaxUpgrade())
            {
                isUpgrade = false;

                _upgradeBtn.SetEmpty();
                //다음 테크 있음
                if (entity.IsNextTech())
                {
                    _upgradeBtn.SetLabel("테크");
                }
                //최종 테크
                else
                {
                    isEndTech = true;
                }
            }
            else
            {
                isUpgrade = true;
                _upgradeBtn.SetData(entity.UpgradeAssetData);
                if (entity.IsUpgradable())
                {
                    isUpgradable = true;
                    _upgradeBtn.SetLabel("업그레이드");
                }
                else
                {
                    isUpgradable = false;
                    _upgradeBtn.SetLabel("한계 도달");
                }
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
                    if (isUpgradable)
                    {
                        isEnough = assetEntity.IsEnough(_entity.UpgradeAssetData);
                    }
                    else
                    {
                        isEnough = false;
                    }
                }
                else
                {
                    isEnough = true;
                }
            }

            _upgradeBtn.interactable = isEnough;
        }


        public void CleanUp()
        {
            _upgradeBtn.onClick.RemoveListener(OnUpgradeEvent);
            _techCancelButton.onClick.RemoveListener(OnCancelTechEvent);
        }


        private void ShowTechSelector(UnitTechData[] arr)
        {
            for (int i = 0; i < _techButtons.Count; i++)
            {
                _techButtons[i].gameObject.SetActive(false);
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
                //테크창 열리기
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
            HideTechSelector();
        }

        private void OnCancelTechEvent()
        {
            HideTechSelector();
        }

#endregion
    }

}