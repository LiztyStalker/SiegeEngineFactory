namespace SEF.Entity
{
    using SEF.Data;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public struct BlacksmithEntity
    {
        private BlacksmithData _data;
        private UpgradeData _upgradeData;
        private IAssetData _upgradeAssetData;

        public StatusPackage StatusPackage => GetStatusPackage();

        //번역 작업 필요 - TranslatorStorage
        public string Name => _data.Key;
        public string Content => _data.Key;

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

        public void SetData(BlacksmithData data)
        {
            _data = data;            
        }

        public void SetData(BlacksmithData data, UpgradeData upgradeData)
        {
            _data = data;
            _upgradeData = upgradeData;
        }

        public void Upgrade()
        {
            _upgradeData.IncreaseNumber();
            _upgradeAssetData = null;
        }

        private IAssetData CalculateUpgradeData()
        {
            var assetData = new GoldAssetData();
            assetData.SetAssetData(_data.StartUpgradeValue, _data.IncreaseUpgradeValue, _data.IncreaseUpgradeRate, _upgradeData.Value);
            return assetData;
        }



        #region ##### Listener #####
        private System.Func<StatusPackage> _statusPackageEvent;
        public void SetOnStatusPackageListener(System.Func<StatusPackage> act) => _statusPackageEvent = act;
        private StatusPackage GetStatusPackage() => _statusPackageEvent();
        #endregion

    }
}