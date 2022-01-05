namespace SEF.Entity
{
    using System.Numerics;
    using Data;

    public struct EnemyEntity// : IEntity
    {
        private EnemyData _enemyData;
        private LevelWaveData _levelWaveData;
        public EnemyData EnemyData => _enemyData;

        private HealthData _healthData;
        private AttackData _attackData;
        private IAssetData _rewardAssetData;


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

        public AttackData AttackData
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
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public void SetData(LevelWaveData levelWaveData)
        {
            _levelWaveData = levelWaveData;
        }
#endif

        public IAssetData GetRewardAssetData()
        {
            return _enemyData.StartRewardAssetValue;
        }

        public LevelWaveData GetLevelWaveData() => _levelWaveData;



        private HealthData CalculateHealthData()
        {
            var assetData = new HealthData();
            assetData.SetAssetData(_enemyData, _levelWaveData);
            return assetData;
        }

        private AttackData CalculateAttackData()
        {
            var assetData = new AttackData();
            assetData.SetAssetData(_enemyData, _levelWaveData);
            return assetData;
        }
        private IAssetData CalculateRewardAssetData()
        {
            var assetData = new GoldAssetData();
            assetData.SetAssetData(_enemyData, _levelWaveData);
            return assetData;
        }
    }
}