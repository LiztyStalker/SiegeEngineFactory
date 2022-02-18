namespace SEF.Manager
{
    using Account;
    using Data;
    using Entity;
    using Process;
    using Utility.IO;

    #region ##### StorableData #####

    [System.Serializable]
    public class VillageLineStorableData : StorableData
    {
        [UnityEngine.SerializeField] private int _index;
        public int Index => _index;
        internal void SetData(int index, StorableData children)
        {
            _index = index;
            Children = new StorableData[1];
            Children[0] = children;
        }
    }

    #endregion


    public class VillageLine
    {
        private int _index;
        private VillageEntity _entity;

        public static VillageLine Create()
        {
            return new VillageLine();
        }

        public void Initialize()
        {
            _entity.Initialize();
            _entity.SetOnProcessEntityListener(OnProcessEntityEvent);
        }
        public void CleanUp()
        {
            _entity.SetOnProcessEntityListener(null);
            _entity.CleanUp();
            _refreshEvent = null;
        }

        public void SetIndex(int index)
        {
            _index = index;
        }

        public void SetData(VillageData data)
        {
            _entity.SetData(data);
        }

        public void Refresh()
        {
            OnRefreshEvent();
        }

        public IAssetData Upgrade()
        {
            var assetData = _entity.UpgradeAssetData;
            _entity.Upgrade();
            
            
            OnRefreshEvent();


            return assetData;
        }

        #region ##### Listener #####


        //private System.Func<UnitEntity, bool> _conditionProductEvent;
        //public void SetOnConditionProductUnitListener(System.Func<UnitEntity, bool> act) => _conditionProductEvent = act;
        //private bool OnConditionProductUnitEvent(UnitEntity unitEntity)
        //{
        //    return _conditionProductEvent(unitEntity);
        //}

        private System.Action<int, VillageEntity> _refreshEvent;
        public void SetOnRefreshListener(System.Action<int, VillageEntity> act) => _refreshEvent = act;
        private void OnRefreshEvent()
        {
            _refreshEvent?.Invoke(_index, _entity);
        }

        private System.Action<IProcessProvider, ProcessEntity> _processEntityEvent;
        public void SetOnProcessEntityListener(System.Action<IProcessProvider, ProcessEntity> act) => _processEntityEvent = act;
        private void OnProcessEntityEvent(IProcessProvider provider, ProcessEntity entity) => _processEntityEvent?.Invoke(provider, entity);
        #endregion


        #region ##### StorableData #####
        public StorableData GetStorableData()
        {
            var data = new VillageLineStorableData();
            data.SetData(_index, _entity.GetStorableData());
            return data;
        }

        public void SetStorableData(StorableData data)
        {
            var storableData = (VillageLineStorableData)data;

            _index = storableData.Index;

            var entityStorableData = (VillageEntityStorableData)storableData.Children[0];

            var upgradeData = new UpgradeData();
            upgradeData.SetValue(entityStorableData.UpgradeValue);

            _entity.SetStorableData(upgradeData);
            Refresh();
        }

        public bool Contains(StorableData data)
        {
            var storableData = (VillageLineStorableData)data;
            var entityStorableData = (VillageEntityStorableData)storableData.Children[0];
            //UnityEngine.Debug.Log(_entity + " " + entityStorableData);
            return entityStorableData.Key == _entity.Key;

        }
        #endregion



    }
}