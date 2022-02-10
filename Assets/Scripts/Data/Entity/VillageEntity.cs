namespace SEF.Entity
{
    using SEF.Data;
    using SEF.Process;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public struct VillageEntity : IProcessProvider
    {
        private VillageData _data;
        private UpgradeData _upgradeData;
        private IAssetData _upgradeAssetData;

        //번역 작업 필요 - TranslatorStorage
        public string Name => _data.Key;
        public string Content => _data.Key;
        public string Ability => _data.Key;

        public int UpgradeValue => _upgradeData.Value;

        public IAssetData UpgradeAssetData
        {
            get
            {
                if (_upgradeAssetData == null)
                {
                    _upgradeAssetData = CalculateUpgradeData();
                }
                return _upgradeAssetData;
            }
        }

        public void Initialize()
        {
            _upgradeData = NumberDataUtility.Create<UpgradeData>();
        }
        public void CleanUp()
        {
            _data = null;
            _upgradeData = null;
        }

        public void SetData(VillageData data)
        {
            _data = data;
        }

        public void SetData(VillageData data, UpgradeData upgradeData)
        {
            _data = data;
            _upgradeData = upgradeData;
        }

        public void Upgrade()
        {
            _upgradeData.IncreaseNumber();
            _upgradeAssetData = null;

            //ProcessPackage에 등록

            OnProcessEntityEvent(this);
        }

        private IAssetData CalculateUpgradeData()
        {
            var assetData = (IAssetData)_data.StartUpgradeAssetData.Clone();
            assetData.SetCompoundInterest(_data.IncreaseUpgradeValue, _data.IncreaseUpgradeRate, _upgradeData.Value);
            return assetData;
        }

        #region ##### Listener #####

        private System.Action<IProcessProvider, ProcessEntity> _processEntityEvent;
        public void SetOnProcessEntityListener(System.Action<IProcessProvider, ProcessEntity> act) => _processEntityEvent = act;

        private void OnProcessEntityEvent(IProcessProvider provider)
        {
            var entity = new ProcessEntity(_data.ProcessData, _upgradeData);
            _processEntityEvent?.Invoke(provider, entity);
        }

        #endregion

    }
}