namespace SEF.Entity
{
    using System.Numerics;
    using Data;

    public struct EnemyEntity// : IEntity
    {
        private EnemyData _enemyData;
        private LevelWaveData _levelWaveData;

        public EnemyData EnemyData => _enemyData;


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

        //public AssetData GetRewardAssetData()
        //{
        //    return _enemyData.StartRewardAssetValue;
        //}

        public IAssetData GetRewardAssetData()
        {
            return _enemyData.StartRewardAssetValue;
        }

        public LevelWaveData GetLevelWaveData() => _levelWaveData;
    }
}