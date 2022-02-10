namespace SEF.Manager
{
    using SEF.Account;
    using SEF.Data;
    using SEF.Entity;
    using SEF.Process;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class VillageLine
    {
        private int _index;
        private VillageEntity _entity;
        //        private float _nowTime;

        public static VillageLine Create()
        {
            return new VillageLine();
        }

        public void Initialize()
        {
            _entity.Initialize();
            _entity.SetOnProcessEntityListener(OnProcessEntityEvent);
            //_entity.SetOnStatusPackageListener(GetStatusPackage);
            //_nowTime = 0;
        }
        public void CleanUp()
        {
            //_entity.SetOnStatusPackageListener(null);
            _entity.SetOnProcessEntityListener(null);
            _entity.CleanUp();
            //_nowTime = 0;            
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

        public void RunProcess(float deltaTime)
        {
            //if (OnConditionProductUnitEvent(_unitEntity))
            //{
            //    _nowTime += deltaTime;
            //    if (_nowTime > _unitEntity.ProductTime)
            //    {
            //        OnProductUnitEvent();
            //        _nowTime -= _unitEntity.UnitData.ProductTime;
            //    }
            //}
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


        private System.Func<UnitEntity, bool> _conditionProductEvent;
        public void SetOnConditionProductUnitListener(System.Func<UnitEntity, bool> act) => _conditionProductEvent = act;
        private bool OnConditionProductUnitEvent(UnitEntity unitEntity)
        {
            return _conditionProductEvent(unitEntity);
        }

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