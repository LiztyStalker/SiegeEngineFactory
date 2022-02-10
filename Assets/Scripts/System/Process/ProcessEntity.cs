namespace SEF.Process
{
    using SEF.Data;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public struct ProcessEntity
    {
        private IProcessData _data;
        private UpgradeData _upgradeData;

        private float _nowTime;

        public ProcessEntity(IProcessData data, UpgradeData upgradeData)
        {
            _data = data;
            _upgradeData = upgradeData;
            _nowTime = 0;
        }

        internal void RunProcess(float deltaTime, System.Action<ProcessEntity> processCallback)
        {
            _nowTime += deltaTime;
            //시간 진행
            if (_nowTime >= _data.ProcessTime)
            {
                //시간 도달하면 프로세스 이벤트 실행
                //IProcessData의 내역을 전송
                processCallback?.Invoke(this);
                _nowTime -= _data.ProcessTime;
            }
        }

        //internal IProcessData GetProcessData() => _data;
        public IAssetData GetAssetData()
        {
            if(_data is AssetProcessData)
            {
                var data = _data as AssetProcessData;
                return data.GetAssetData(_upgradeData);
            }
            return null;
        }
    }
}