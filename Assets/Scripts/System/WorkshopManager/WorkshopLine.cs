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
            //_unitEntity.SetOnStatusPackageListener(GetStatusPackage);
            _nowTime = 0;
        }
        public void CleanUp()
        {
            //_unitEntity.SetOnStatusPackageListener(null);
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
            if (OnConditionProductUnitEvent(_unitEntity))
            {
                _nowTime += deltaTime;
                if (_nowTime > _unitEntity.ProductTime)
                {
                    OnProductUnitEvent();
                    _nowTime -= _unitEntity.UnitData.ProductTime;
                }
            }
            OnRefreshEvent();
        }


        public IAssetData Upgrade(out string key)
        {
            var assetData = _unitEntity.UpgradeAssetData;
            _unitEntity.Upgrade();
            key = _unitEntity.UnitData.Key;
            OnRefreshEvent();
            return assetData;
        }

        public void UpTech(UnitData unitData) 
        {
            _unitEntity.UpTech(unitData);
            OnRefreshEvent();
        }


        #region ##### Listener #####

        private System.Action<UnitEntity> _productUnitEvent;
        public void SetOnProductUnitListener(System.Action<UnitEntity> act) => _productUnitEvent = act;
        private void OnProductUnitEvent()
        {
            _productUnitEvent?.Invoke(_unitEntity);
        }

        private System.Func<UnitEntity, bool> _conditionProductEvent;
        public void SetOnConditionProductUnitListener(System.Func<UnitEntity, bool> act) => _conditionProductEvent = act;
        private bool OnConditionProductUnitEvent(UnitEntity unitEntity)
        {
            return _conditionProductEvent(unitEntity);
        }

        private System.Action<int, UnitEntity, float> _refreshEvent;
        public void SetOnRefreshListener(System.Action<int, UnitEntity, float> act) => _refreshEvent = act;
        private void OnRefreshEvent()
        {
            _refreshEvent?.Invoke(_index, _unitEntity, _nowTime);
        }

        //private System.Func<StatusPackage> _statusPackageEvent;
        //public void SetOnStatusPackageListener(System.Func<StatusPackage> act) => _statusPackageEvent = act;
        //private StatusPackage GetStatusPackage() => _statusPackageEvent();
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