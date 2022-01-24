namespace SEF.Manager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Data;
    using Entity;
    using SEF.Account;

    public class BlacksmithLine
    {
        private int _index;
        private BlacksmithEntity _entity;
//        private float _nowTime;

        public static BlacksmithLine Create()
        {
            return new BlacksmithLine();
        }
        
        public void Initialize()
        {
            _entity.Initialize();
            _entity.SetOnStatusPackageListener(GetStatusPackage);
            //_nowTime = 0;
        }
        public void CleanUp()
        {
            _entity.SetOnStatusPackageListener(null);
            _entity.CleanUp();
            //_nowTime = 0;            
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

        private System.Action<int, BlacksmithEntity> _refreshEvent;
        public void SetOnRefreshListener(System.Action<int, BlacksmithEntity> act) => _refreshEvent = act;
        private void OnRefreshEvent()
        {
            _refreshEvent?.Invoke(_index, _entity);
        }

        private System.Func<StatusPackage> _statusPackageEvent;
        public void SetOnStatusPackageListener(System.Func<StatusPackage> act) => _statusPackageEvent = act;
        private StatusPackage GetStatusPackage() => _statusPackageEvent();
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