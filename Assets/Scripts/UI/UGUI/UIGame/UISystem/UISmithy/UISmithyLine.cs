namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using Entity;

    public class UISmithyLine : MonoBehaviour
    {

        private int _index;

        [SerializeField]
        private GameObject _activatePanel;

        [SerializeField]
        private Image _icon;

        [SerializeField]
        private Text _nameLabel;
        [SerializeField]
        private Text _levelValueLabel;

        [SerializeField]
        private Text _contentLabel;
        [SerializeField]
        private Text _abilityLabel;

        [SerializeField]
        private Button _upgradeButton;
        [SerializeField]
        private Image _upgradeAssetIcon;
        [SerializeField]
        private Text _upgradeValueLabel;
        [SerializeField]
        private Text _buttonLabel;

        [SerializeField]
        private GameObject _inactivatePanel;
        [SerializeField]
        private Text _inactivateLabel;

        public void SetIndex(int index) => _index = index;

        private SmithyEntity _entity;

        public static UISmithyLine Create()
        {
            var obj = new GameObject();
            obj.name = "UI@SmithyLine";
            obj.AddComponent<RectTransform>();
            return obj.AddComponent<UISmithyLine>();
        }

        public void Initialize()
        {

            Debug.Assert(_activatePanel != null, "_activatePanel element 를 찾지 못했습니다");
            Debug.Assert(_icon != null, "icon element 를 찾지 못했습니다");
            Debug.Assert(_nameLabel != null, "_nameLabel element 를 찾지 못했습니다");
            Debug.Assert(_levelValueLabel != null, "_levelValueLabel element 를 찾지 못했습니다");

            Debug.Assert(_contentLabel != null, "_contentLabel element 를 찾지 못했습니다");
            Debug.Assert(_abilityLabel != null, "_abilityLabel element 를 찾지 못했습니다");

            Debug.Assert(_upgradeButton != null, "_upgradeButton element 를 찾지 못했습니다");
            Debug.Assert(_upgradeAssetIcon != null, "_upgradeAssetIcon element 를 찾지 못했습니다");
            Debug.Assert(_upgradeValueLabel != null, "_upgradeValueLabel element 를 찾지 못했습니다");
            Debug.Assert(_buttonLabel != null, "_buttonLabel element 를 찾지 못했습니다");

            Debug.Assert(_inactivatePanel != null, "_inactivatePanel element 를 찾지 못했습니다");


            //_icon

            _nameLabel.text = "이름";
            _levelValueLabel.text = "1";
            _contentLabel.text = "설명";
            _abilityLabel.text = "능력";

            _upgradeButton.onClick.AddListener(OnUpgradeEvent);

            _activatePanel.SetActive(false);
            _inactivatePanel.SetActive(true);
        }




        private bool isUpgrade = false;
        private bool isEndTech = false;

        public void RefreshSmithyLine(SmithyEntity entity)
        {
            if (!_activatePanel.activeSelf)
            {
                _activatePanel.SetActive(true);
                _inactivatePanel.SetActive(false);
            }

            _entity = entity;

            _nameLabel.text = entity.Name;
            _levelValueLabel.text = entity.NowUpgradeValue.ToString();
            _contentLabel.text = entity.Content;
            _abilityLabel.text = entity.Ability;

            //            _uiFillable.FillAmount = nowTime / unitData.ProductTime;

            //MaxUpgrade이면 테크로 변경됨
            isEndTech = false;
            if (entity.IsMaxUpgrade())
            {
                isUpgrade = false;

                //다음 테크 있음
                if (entity.IsNextTech())
                {
                    _upgradeValueLabel.text = entity.TechAssetData.GetValue();
                    _buttonLabel.text = "테크";
                }
                //최종 테크
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
                _buttonLabel.text = "업그레이드";
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
            _upgradeButton.interactable = isEnough;
            //            _upgradeButton.SetEnabled(isEnough);
        }

        public void CleanUp()
        {
            _upgradeButton.onClick.RemoveListener(OnUpgradeEvent);
            //_upgradeButton.UnregisterCallback<ClickEvent>(OnUpgradeEvent);
            _icon = null;
        }



        #region ##### Listener #####


        private System.Action<int> _upgradeEvent;
        public void AddUpgradeListener(System.Action<int> act) => _upgradeEvent += act;
        public void RemoveUpgradeListener(System.Action<int> act) => _upgradeEvent -= act;
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


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
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
            _instance = UISmithyLine.Create();
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