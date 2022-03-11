namespace SEF.Entity
{
    using Data;


    #region ##### StorableData #####

    //UnitEntity
    //EnemyEntity °ø¿ë
    [System.Serializable]
    public class EnemyEntityStorableData : Utility.IO.StorableData
    {
        [UnityEngine.SerializeField] private string _unitKey;
        [UnityEngine.SerializeField] private int _levelWaveValue;

        public string UnitKey => _unitKey;
        public int LevelWaveValue => _levelWaveValue;

        internal void SetData(string key, int value)
        {
            _unitKey = key;
            _levelWaveValue = value;
            Children = null;
        }
    }

    #endregion

    public struct EnemyEntity : IEntity
    {
        private EnemyData _data;
        private LevelWaveData _levelWaveData;
        public EnemyData EnemyData => _data;

        private HealthData _healthData;
        private DamageData _attackData;
        private IAssetData _rewardAssetData;

        public string Name => Storage.TranslateStorage.Instance.GetTranslateData("Enemy_Data_Tr", _data.Key, "Name");

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
            }
        }


        public void Initialize()
        {
            _levelWaveData = NumberDataUtility.Create<LevelWaveData>();            
        }
        public void CleanUp()
        {
            _data = null;
            _levelWaveData = null;
        }

        public void SetData(EnemyData enemyData, LevelWaveData levelWaveData)
        {
            _data = enemyData;
            _levelWaveData = levelWaveData;
            _attackData = null;
            _healthData = null;
            _rewardAssetData = null;
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public void SetData_Test(EnemyData enemyData)
        {
            _data = enemyData;
            _attackData = null;
            _healthData = null;
            _rewardAssetData = null;
        }
        public void SetData_Test(LevelWaveData levelWaveData)
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
            assetData.SetAssetData(_data, _levelWaveData);
            return assetData;
        }

        private DamageData CalculateAttackData()
        {
            var assetData = new DamageData();
            assetData.SetAssetData(_data, _levelWaveData);
            return assetData;
        }
        private IAssetData CalculateRewardAssetData()
        {
            var assetData = new GoldAssetData();
            assetData.SetAssetData(_data, _levelWaveData);
            return assetData;
        }

        #region ##### StorableData #####
        public Utility.IO.StorableData GetStorableData()
        {
            var data = new EnemyEntityStorableData();
            data.SetData(_data.Key, _levelWaveData.Value);
            return data;
        }
        #endregion
    }
}