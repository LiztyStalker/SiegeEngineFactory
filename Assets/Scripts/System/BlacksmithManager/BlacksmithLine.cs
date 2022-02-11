namespace SEF.Manager
{
    using Data;
    using Entity;
    using SEF.Account;

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