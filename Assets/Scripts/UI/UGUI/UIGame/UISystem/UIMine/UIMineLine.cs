namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using SEF.Entity;

    public class UIMineLine : MonoBehaviour
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
        private GameObject _inactivatePanel;
        [SerializeField]
        private Text _inactivateLabel;

        public void SetIndex(int index) => _index = index;

        private MineEntity _entity;

        public static UIMineLine Create()
        {
            var obj = new GameObject();
            obj.name = "UI@MineLine";
            obj.AddComponent<RectTransform>();
            return obj.AddComponent<UIMineLine>();
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


        public void RefreshMineLine(MineEntity entity)
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
            _contentLabel.text = entity.Ability;

            //            _uiFillable.FillAmount = nowTime / unitData.ProductTime;

            _upgradeValueLabel.text = entity.UpgradeAssetData.GetValue();

        }

        public void RefreshAssetEntity(AssetPackage assetEntity)
        {
            var isEnough = assetEntity.IsEnough(_entity.UpgradeAssetData);
            _upgradeButton.interactable = isEnough;
        }

        public void CleanUp()
        {
            _upgradeButton.onClick.RemoveListener(OnUpgradeEvent);
            _icon = null;
        }



#region ##### Listener #####


        private System.Action<int> _upgradeEvent;
        public void AddUpgradeListener(System.Action<int> act) => _upgradeEvent += act;
        public void RemoveUpgradeListener(System.Action<int> act) => _upgradeEvent -= act;
        private void OnUpgradeEvent()
        {
            _upgradeEvent?.Invoke(_index);
        }

#endregion
    }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    public class UIMineLine_Test : MonoBehaviour
    {
        private UIMineLine _instance;
        public UIMineLine Instance => _instance;

        public static UIMineLine_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UIMineLine_Test";
            return obj.AddComponent<UIMineLine_Test>();
        }

        public void Initialize()
        {
            _instance = UIMineLine.Create();
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