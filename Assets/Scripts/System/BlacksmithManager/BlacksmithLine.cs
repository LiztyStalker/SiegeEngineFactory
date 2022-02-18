namespace SEF.Manager
{
    using Data;
    using Entity;
    using SEF.Account;
    using Utility.IO;



    #region ##### StorableData #####

    [System.Serializable]
    public class SmithyLineStorableData : StorableData
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



    public class BlacksmithLine
    {
        private int _index;
        private BlacksmithEntity _entity;

        public static BlacksmithLine Create()
        {
            return new BlacksmithLine();
        }
        
        public void Initialize()
        {
            _entity.Initialize();
        }
        public void CleanUp()
        {
            _entity.CleanUp();
            _refreshEvent = null;
        }

        public void SetIndex(int index)
        {
            _index = index;
        }

        public void SetData(BlacksmithData data)
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

        private System.Action<int, BlacksmithEntity> _refreshEvent;
        public void SetOnRefreshListener(System.Action<int, BlacksmithEntity> act) => _refreshEvent = act;
        private void OnRefreshEvent()
        {
            _refreshEvent?.Invoke(_index, _entity);
        }
        #endregion




        #region ##### StorableData #####
        public StorableData GetStorableData()
        {
            var data = new SmithyLineStorableData();
            data.SetData(_index, _entity.GetStorableData());
            return data;
        }

        public void SetStorableData(StorableData data)
        {
            var storableData = (SmithyLineStorableData)data;

            _index = storableData.Index;

            var entityStorableData = (SmithyEntityStorableData)storableData.Children[0];
           
            var upgradeData = new UpgradeData();
            upgradeData.SetValue(entityStorableData.UpgradeValue);

            //UnityEngine.Debug.Log(upgradeData.Value);

            _entity.SetStorableData(upgradeData);
            Refresh();
        }

        public bool Contains(StorableData data)
        {
            var storableData = (SmithyLineStorableData)data;
            var entityStorableData = (SmithyEntityStorableData)storableData.Children[0];
            //UnityEngine.Debug.Log(_entity.Key + " " + entityStorableData.Key);
            return entityStorableData.Key == _entity.Key;
        }
        #endregion



    }
}