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
            UICommon.Current.ShowPopup("��ũ�� �����Ͻðڽ��ϱ�?", "��", "�ƴϿ�", () => {
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