namespace SEF.Manager
{
    using Account;
    using Data;


    public struct UnitEntity
    {
        private UnitData _unitData;
        private UpgradeData _upgradeData;

        public UnitData UnitData => _unitData;
        public UpgradeData UpgradeData => _upgradeData;

        public void Initialize()
        {
            _upgradeData = NumberDataUtility.Create<UpgradeData>();
        }

        public void UpTech(UnitData unitData)
        {
            _unitData = unitData;
            _upgradeData.Initialize();
        }
    }

    public class WorkshopLine
    {
        private UnitEntity _unitEntity;
        private float _nowTime;

        public static WorkshopLine Create()
        {
            return new WorkshopLine();
        }

        private WorkshopLine()
        {
            _unitEntity.Initialize();
            _nowTime = 0;
        }

        public void Initialize(IAccountData accountData)
        {
            //null이면 무시 (이미 Create할때 초기화 함)
            //null이 아니면 accountData 적용

        }
        public void CleanUp()
        {
            
        }

        public void RunProcess(float deltaTime)
        {
            _nowTime += deltaTime;
            if(_nowTime > _unitEntity.UnitData.ProductTime)
            {
                OnProductUnitEvent();
                _nowTime -= _unitEntity.UnitData.ProductTime;
            }
            OnRefreshEvent();
        }

        public void Upgrade() 
        {
            _unitEntity.UpgradeData.IncreaseNumber();
            OnRefreshEvent();
        }

        public void Expend() 
        { 
            
        }

        public void UpTech(UnitData unitData) 
        {
            _unitEntity.UpTech(unitData);
            OnRefreshEvent();
        }

        public bool IsEnoughAsset(AssetData assetData) 
        {
            return false; 
        }


        #region ##### Listener #####

        private System.Action<UnitEntity> _createEvent;
        public void SetOnProductUnitListener(System.Action<UnitEntity> act) => _createEvent = act;
        private void OnProductUnitEvent()
        {
            _createEvent?.Invoke(_unitEntity);
        }


        private System.Action<UnitEntity> _refreshEvent;
        public void SetOnRefreshListener(System.Action<UnitEntity> act) => _refreshEvent = act;
        private void OnRefreshEvent()
        {
            _refreshEvent?.Invoke(_unitEntity);
        }
        #endregion


        #region ##### Data #####
        public IAccountData GetData()
        {
            return null;
        }
        #endregion

    }
}