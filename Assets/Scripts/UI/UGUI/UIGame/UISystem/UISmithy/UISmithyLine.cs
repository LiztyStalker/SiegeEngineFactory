namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using Entity;

    public class UISmithyLine : MonoBehaviour
    {
        private readonly static string UGUI_NAME = "UI@SmithyLine";

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

        private SmithyEntity _entity;


        public static UISmithyLine Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UISmithyLine>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.name = UGUI_NAME;
                return obj.AddComponent<UISmithyLine>();
            }
#else
            Debug.LogWarning($"{UGUI_NAME}�� ã�� �� �����ϴ�");
            return null;
#endif
        }



        public void Initialize()
        {

            Debug.Assert(_activatePanel != null, "_activatePanel element �� ã�� ���߽��ϴ�");
            //Debug.Assert(_icon != null, "icon element �� ã�� ���߽��ϴ�");
            Debug.Assert(_nameLabel != null, "_nameLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_levelValueLabel != null, "_levelValueLabel element �� ã�� ���߽��ϴ�");

            Debug.Assert(_contentLabel != null, "_contentLabel element �� ã�� ���߽��ϴ�");
            Debug.Assert(_abilityLabel != null, "_abilityLabel element �� ã�� ���߽��ϴ�");

            Debug.Assert(_upgradeBtn != null, "_upgradeBtn element �� ã�� ���߽��ϴ�");


            //_icon

            _nameLabel.text = "�̸�";
            _levelValueLabel.text = "1";
            _contentLabel.text = "����";
            _abilityLabel.text = "�ɷ�";

            _upgradeBtn.onClick.AddListener(OnUpgradeEvent);
            _upgradeBtn.SetRepeat(true);
            _activatePanel.SetActive(true);
        }




        private bool isUpgrade = false;
        private bool isEndTech = false;

        public void RefreshSmithyLine(SmithyEntity entity)
        {
            _entity = entity;

            _nameLabel.text = entity.Name;
            _levelValueLabel.text = $"Lv : {entity.NowUpgradeValue} / {entity.MaxUpgradeValue}";
            _contentLabel.text = entity.Content;
            _abilityLabel.text = $"Tech : {entity.NowTechValue} / {entity.MaxTechValue}";

            //MaxUpgrade�̸� ��ũ�� �����
            isEndTech = false;
            if (entity.IsMaxUpgrade())
            {
                isUpgrade = false;

                //���� ��ũ ����
                if (entity.IsNextTech())
                {
                    _upgradeBtn.SetData(entity.TechAssetData);
                    _upgradeBtn.SetLabel("��ũ");
                }
                //���� ��ũ
                else
                {
                    isEndTech = true;
                    _upgradeBtn.SetEmpty();
                }
            }
            else
            {
                isUpgrade = true;
                _upgradeBtn.SetData(entity.UpgradeAssetData);
                _upgradeBtn.SetLabel("���׷��̵�");
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
            _upgradeBtn.interactable = isEnough;
        }

        public void CleanUp()
        {
            _upgradeBtn.onClick.RemoveListener(OnUpgradeEvent);
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