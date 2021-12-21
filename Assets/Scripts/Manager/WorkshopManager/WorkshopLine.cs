namespace SEF.Manager
{
    using Entity;
    using Account;
    using Data;
        
    public class WorkshopLine
    {
        private int _index;
        private UnitEntity _unitEntity;
        private float _nowTime;

        public static WorkshopLine Create()
        {
            return new WorkshopLine();
        }

        public void Initialize()
        {
            _unitEntity.Initialize();
            _nowTime = 0;
        }
        public void CleanUp()
        {
            _unitEntity.CleanUp();
            _nowTime = 0;
            _productUnitEvent = null;
            _refreshEvent = null;
        }

        public void SetIndex(int index)
        {
            _index = index;
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

        private System.Action<UnitEntity> _productUnitEvent;
        public void SetOnProductUnitListener(System.Action<UnitEntity> act) => _productUnitEvent = act;
        private void OnProductUnitEvent()
        {
            _productUnitEvent?.Invoke(_unitEntity);
        }


        private System.Action<int, UnitEntity, float> _refreshEvent;
        public void SetOnRefreshListener(System.Action<int, UnitEntity, float> act) => _refreshEvent = act;
        private void OnRefreshEvent()
        {
            _refreshEvent?.Invoke(_index, _unitEntity, _nowTime);
        }
        #endregion


        #region ##### Data #####
        public IAccountData GetData()
        {
            return null;
        }

        public void SetData(IAccountData accountData)
        {
            //null이면 무시 (이미 Create할때 초기화 함)
            //null이 아니면 accountData 적용

        }
        #endregion


    }
}