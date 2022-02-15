namespace SEF.Manager
{
    using Entity;
    using Account;
    using Data;
    using Utility.IO;


    #region ##### Serialize #####
    [System.Serializable]
    public class WorkshopLineStorableData : StorableData
    {
        [UnityEngine.SerializeField] private int _index;
        [UnityEngine.SerializeField] private float _nowTime;

        public int Index => _index;
        public float NowTime => _nowTime;

        public void SaveData(int index, float nowTime, StorableData[] children)
        {
            _index = index;
            _nowTime = nowTime;
            Children = children;
        }
    }
    #endregion

    public class WorkshopLine
    {
        private int _index;
        private UnitEntity _unitEntity;
        private float _nowTime;

        //private WorkshopLineStorableData _storableData;

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


        public void SetStorableData(StorableData data)
        {
            var storableData = (WorkshopLineStorableData)data;

            _index = storableData.Index;
            _nowTime = storableData.NowTime;

            if (storableData.Children != null && storableData.Children.Length > 0) {

                var children = (UnitEntityStorableData)storableData.Children[0];

                var unitData = Storage.DataStorage.Instance.GetDataOrNull<UnitData>(children.UnitKey, null, null);
                var upgradeData = new UpgradeData();
                upgradeData.SetValue_Test(children.UpgradeValue);

                _unitEntity.SetStorableData(unitData, upgradeData);
            }
            else
            {
#if UNITY_EDITOR
                UnityEngine.Debug.LogWarning("UnitEntity를 구축하지 못했습니다");
#endif
            }
            OnRefreshEvent();
        }

        public StorableData GetStorableData()
        {
            var _storableData = new WorkshopLineStorableData();
            _storableData.SaveData(_index, _nowTime, GetChildren());
            return _storableData;
        }

        private StorableData[] GetChildren()
        {
            StorableData[] arr = new StorableData[1];
            arr[0] = _unitEntity.GetStorableData();
            return arr;
        }

        #endregion


    }
}