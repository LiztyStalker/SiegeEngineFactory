namespace SEF.Manager
{
    using Account;
    using Data;
    using Entity;
    using Process;
    using Utility.IO;

    #region ##### StorableData #####

    [System.Serializable]
    public class MineLineStorableData : StorableData
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




    public class MineLine
    {
        private MineEntity _entity;
        private int _index;


        public static MineLine Create()
        {
            return new MineLine();
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

        public void SetData(MineData data)
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


        public IAssetData UpTech()
        {
            var assetData = _entity.TechAssetData;
            _entity.UpTech();
            OnRefreshEvent();
            return assetData;
        }

        public IAssetData RewardOffline(System.TimeSpan timeSpan) => _entity.RewardOffline(timeSpan);


        #region ##### Listener #####

        private System.Action<int, MineEntity> _refreshEvent;
        public void SetOnRefreshListener(System.Action<int, MineEntity> act) => _refreshEvent = act;
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
            var data = new MineLineStorableData();
            data.SetData(_index, _entity.GetStorableData());
            return data;
        }

        public void SetStorableData(StorableData data)
        {
            var storableData = (MineLineStorableData)data;

            _index = storableData.Index;

            var entityStorableData = (MineEntityStorableData)storableData.Children[0];

            var upgradeData = new UpgradeData();
            upgradeData.SetValue(entityStorableData.UpgradeValue);

            _entity.SetStorableData(upgradeData);
            Refresh();
        }

        public bool Contains(StorableData data)
        {
            var storableData = (MineLineStorableData)data;
            var entityStorableData = (MineEntityStorableData)storableData.Children[0];
            return entityStorableData.Key == _entity.Key;

        }
        #endregion



    }
}