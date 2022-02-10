namespace SEF.Status
{
    using SEF.Data;

    public struct StatusEntity
    {
        private IStatusData _statusData;
        private UpgradeData _upgradeData;

        public IStatusData StatusData => _statusData;
        public IStatusData.TYPE_STATUS_DATA TypeStatusData => _statusData.TypeStatusData;
        public StatusEntity(IStatusData statusData, UpgradeData upgradeData)
        {
            _statusData = statusData;
            _upgradeData = upgradeData;
        }

        public UniversalBigNumberData GetValue() => _statusData.GetValue(_upgradeData);
    }

}