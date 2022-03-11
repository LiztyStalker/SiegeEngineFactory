namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using SEF.Entity;

    public class UIMineLine : MonoBehaviour
    {
        private readonly static string UGUI_NAME = "UI@MineLine";

        private int _index;

        [SerializeField]
        private GameObject _activatePanel;

        //[SerializeField]
        //private Image _icon;

        [SerializeField]
        private Text _nameLabel;
        [SerializeField]
        private Text _levelValueLabel;

        [SerializeField]
        private Text _contentLabel;
        [SerializeField]
        private Text _abilityLabel;

        [SerializeField]
        private UIAssetButton _upgradeBtn;

        public void SetIndex(int index) => _index = index;

        private MineEntity _entity;

        public static UIMineLine Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UIMineLine>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.name = UGUI_NAME;
                return obj.AddComponent<UIMineLine>();
            }
#else
            Debug.LogWarning($"{UGUI_NAME}을 찾을 수 없습니다");
            return null;
#endif
        }


        public void CleanUp()
        {
            _upgradeBtn.onClick.RemoveListener(OnUpgradeEvent);
        }


        private void OnEnable()
        {
            Storage.TranslateStorage.Instance.AddOnChangedTranslateListener(SetText);
        }

        private void OnDisable()
        {
            Storage.TranslateStorage.Instance.RemoveOnChangedTranslateListener(SetText);
        }


        public void Initialize()
        {            
            Debug.Assert(_activatePanel != null, "_activatePanel element 를 찾지 못했습니다");
            //Debug.Assert(_icon != null, "icon element 를 찾지 못했습니다");
            Debug.Assert(_nameLabel != null, "_nameLabel element 를 찾지 못했습니다");
            Debug.Assert(_levelValueLabel != null, "_levelValueLabel element 를 찾지 못했습니다");

            Debug.Assert(_contentLabel != null, "_contentLabel element 를 찾지 못했습니다");
            Debug.Assert(_abilityLabel != null, "_abilityLabel element 를 찾지 못했습니다");

            Debug.Assert(_upgradeBtn != null, "_upgradeBtn element 를 찾지 못했습니다");


            //_icon

            _nameLabel.text = "-";
            _levelValueLabel.text = "-";
            _contentLabel.text = "-";
            _abilityLabel.text = "-";

            _upgradeBtn.onClick.AddListener(OnUpgradeEvent);

            _upgradeBtn.SetRepeat(true);
            _activatePanel.SetActive(true);

        }

        private bool isUpgrade = false;
        private bool isEndTech = false;
        private bool isUpgradable = false;

        public void RefreshMineLine(MineEntity entity)
        {
            _entity = entity;
            SetText();
        }

        private void SetText()
        {

            _nameLabel.text = _entity.Name;
            _levelValueLabel.text = $"Lv : {_entity.NowUpgradeValue} / {_entity.MaxUpgradeValue}";
            _contentLabel.text = _entity.Description;
            _abilityLabel.text = $"Tech : {_entity.NowTechValue} / {_entity.MaxTechValue}";



            isEndTech = false;

            //최대 업그레이드
            if (_entity.IsMaxUpgrade())
            {
                isUpgrade = false;

                //다음 테크 있음
                if (_entity.IsNextTech())
                {
                    _upgradeBtn.SetData(_entity.TechAssetData);
                    _upgradeBtn.SetLabel(Storage.TranslateStorage.Instance.GetTranslateData("System_Tr", "Sys_Tech"));
                }
                //최종 테크
                else
                {
                    isEndTech = true;
                    _upgradeBtn.SetEmpty();
                }
            }
            else
            {
                isUpgrade = true;
                _upgradeBtn.SetData(_entity.UpgradeAssetData);
                if (_entity.IsUpgradable())
                {
                    isUpgradable = true;
                    _upgradeBtn.SetLabel(Storage.TranslateStorage.Instance.GetTranslateData("System_Tr", "Sys_Upgrade"));
                }
                else
                {
                    isUpgradable = false;
                    _upgradeBtn.SetLabel(Storage.TranslateStorage.Instance.GetTranslateData("System_Tr", "Sys_Limit"));
                }
            }
        }

        public void RefreshAssetEntity(AssetPackage assetEntity)
        {
            bool isEnough = false;

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
                    isEnough = assetEntity.IsEnough(_entity.TechAssetData);
                }
            }
            _upgradeBtn.interactable = isEnough;
        }



#region ##### Listener #####


        private System.Action<int> _upgradeEvent;
        public void AddOnUpgradeListener(System.Action<int> act) => _upgradeEvent += act;
        public void RemoveOnUpgradeListener(System.Action<int> act) => _upgradeEvent -= act;
        private void OnUpgradeEvent()
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
            UICommon.Current.ShowPopup("테크를 진행하시겠습니까?", "네", "아니오", () => {
                _uptechEvent?.Invoke(_index);
                Debug.Log("OK");
            });
        }

        #endregion
    }

}