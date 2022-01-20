namespace SEF.Entity
{
    using System.Numerics;
    using Data;

    public struct EnemyEntity : IEntity
    {
        private EnemyData _enemyData;
        private LevelWaveData _levelWaveData;
        public EnemyData EnemyData => _enemyData;

        private HealthData _healthData;
        private DamageData _attackData;
        private IAssetData _rewardAssetData;
        public StatusPackage StatusPackage => GetStatusPackage();

        public HealthData HealthData
        {
            get
            {
                if (_healthData == null)
                {
                    _healthData = CalculateHealthData();
                }
                return _healthData;
            }
        }

        public DamageData AttackData
        {
            get
            {
                if (_attackData == null)
                {
                    _attackData = CalculateAttackData();
                }
                return _attackData;
            }
        }

        public IAssetData RewardAssetData
        {
            get
            {
                if (_rewardAssetData == null)
                {
                    _rewardAssetData = CalculateRewardAssetData();
                }
                return _rewardAssetData;
//                return StatusPackage.GetStatusDataToBigNumberData<RewardAssetStatusData, IAssetData>(_rewardAssetData);
            }
        }


        public void Initialize()
        {
            _levelWaveData = NumberDataUtility.Create<LevelWaveData>();            
        }
        public void CleanUp()
        {
            _enemyData = null;
            _levelWaveData = null;
        }

        public void SetData(EnemyData enemyData, LevelWaveData levelWaveData)
        {
            _enemyData = enemyData;
            _levelWaveData = levelWaveData;
            _attackData = null;
            _healthData = null;
            _rewardAssetData = null;
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public void SetData(EnemyData enemyData)
        {
            _enemyData = enemyData;
            _attackData = null;
            _healthData = null;
            _rewardAssetData = null;
        }
        public void SetData(LevelWaveData levelWaveData)
        {
            _levelWaveData = levelWaveData;
            _attackData = null;
            _healthData = null;
            _rewardAssetData = null;
        }
#endif

        public IAssetData GetRewardAssetData() => RewardAssetData;

        public LevelWaveData GetLevelWaveData() => _levelWaveData;

        private HealthData CalculateHealthData()
        {
            var assetData = new HealthData();
            assetData.SetAssetData(_enemyData, _levelWaveData);
            return assetData;
        }

        private DamageData CalculateAttackData()
        {
            var assetData = new DamageData();
            assetData.SetAssetData(_enemyData, _levelWaveData);
            return assetData;
        }
        private IAssetData CalculateRewardAssetData()
        {
            var assetData = new GoldAssetData();
            assetData.SetAssetData(_enemyData, _levelWaveData);
            return assetData;
        }

        #region ##### Listener #####
        private System.Func<StatusPackage> _statusPackageEvent;
        public void SetOnStatusPackageListener(System.Func<StatusPackage> act) => _statusPackageEvent = act;
        private StatusPackage GetStatusPackage() => _statusPackageEvent();
        #endregion
    }
}