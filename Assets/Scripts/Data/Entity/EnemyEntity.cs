namespace SEF.Entity
{
    using Data;
    public struct EnemyEntity
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
    }
}