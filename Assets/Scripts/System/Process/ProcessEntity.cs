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
            //�ð� ����
            if (_nowTime >= _data.ProcessTime)
            {
                //�ð� �����ϸ� ���μ��� �̺�Ʈ ����
                //IProcessData�� ������ ����
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