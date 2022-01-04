namespace SEF.Data
{
    using UnityEngine;
    using Spine.Unity;

    public enum TYPE_LEVEL_THEME { Grass}

    public enum TYPE_ENEMY_GROUP { Normal, Boss, ThemeBoss}

    
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]    
    public class EnemyData : ScriptableObject
    {
        [SerializeField]
        private string _key;
        public string Key
        {
            get
            {
                _key = name;
                return _key;
            }
        }

        [SerializeField]
        private SkeletonDataAsset _skeletonDataAsset;

        public SkeletonDataAsset SkeletonDataAsset { get => _skeletonDataAsset; set => _skeletonDataAsset = value; }

        [SerializeField]
        private string _spineModelKey;
        public string SpineModelKey { get => _spineModelKey; set => _spineModelKey = value; }

        [SerializeField]
        private string _spineSkinKey;
        public string SpineSkinKey { get => _spineSkinKey; set => _spineSkinKey = value; }

        [SerializeField]
        private TYPE_ENEMY_GROUP _group;
        public TYPE_ENEMY_GROUP Group { get => _group; set => _group = value; }

        [SerializeField]
        private TYPE_LEVEL_THEME _typeLevelTheme;
        public TYPE_LEVEL_THEME TypeLevelTheme { get => _typeLevelTheme; set => _typeLevelTheme = value; }

        [SerializeField]
        private HealthData _startHealthValue = NumberDataUtility.Create<HealthData>();
        public HealthData StartHealthValue => _startHealthValue;

        [SerializeField]
        private HealthData _increaseLevelHealthValue = NumberDataUtility.Create<HealthData>();
        public HealthData IncreaseLevelHealthValue => _increaseLevelHealthValue;

        [SerializeField]
        private float _increaseLevelHealthRate;
        public float IncreaseLevelHealthRate { get => _increaseLevelHealthRate; set => _increaseLevelHealthRate = value; }

        [SerializeField]
        private HealthData _increaseWaveHealthValue = NumberDataUtility.Create<HealthData>();
        public HealthData IncreaseWaveHealthValue => _increaseWaveHealthValue;

        [SerializeField]
        private float _increaseWaveHealthRate;
        public float IncreaseWaveHealthRate { get => _increaseWaveHealthRate; set => _increaseWaveHealthRate = value; }

        [SerializeField]
        private AttackData _startAttackValue = NumberDataUtility.Create<AttackData>();
        public AttackData StartAttackValue => _startAttackValue;

        [SerializeField]
        private AttackData _increaseAttackValue = NumberDataUtility.Create<AttackData>();
        public AttackData IncreaseAttackValue => _increaseAttackValue; 

        [SerializeField]
        private float _increaseAttackRate;
        public float IncreaseAttackRate { get => _increaseAttackRate; set => _increaseAttackRate = value; }


        [SerializeField]
        private int _attackCount;
        public int AttackCount { get => _attackCount; set => _attackCount = value; }

        //[SerializeField]
        //private float[] _attackDelay;
        //public float[] AttackDelay { get => _attackDelay; set => _attackDelay = value; }

        [SerializeField]
        private float _attackDelay;
        public float AttackDelay { get => _attackDelay; set => _attackDelay = value; }


        [SerializeField]
        private string _attackBulletKey;

        public string AttackBulletKey { get => _attackBulletKey; set => _attackBulletKey = value; }

        [SerializeField]
        private GoldAssetData _startRewardAssetValue = NumberDataUtility.Create<GoldAssetData>();
        public GoldAssetData StartRewardAssetValue => _startRewardAssetValue;

        [SerializeField]
        private GoldAssetData _increaseLevelRewardAssetValue = NumberDataUtility.Create<GoldAssetData>();
        public GoldAssetData IncreaseLevelRewardAssetValue => _increaseLevelRewardAssetValue; 

        [SerializeField]
        private float _increaseLevelRewardAssetRate;
        public float IncreaseLevelRewardAssetRate { get => _increaseLevelRewardAssetRate; set => _increaseLevelRewardAssetRate = value; }

        [SerializeField]
        private GoldAssetData _increaseWaveRewardAssetValue = NumberDataUtility.Create<GoldAssetData>();
        public GoldAssetData IncreaseWaveRewardAssetValue => _increaseWaveRewardAssetValue;

        [SerializeField]
        private float _increaseWaveRewardAssetRate;
        public float IncreaseWaveRewardAssetRate { get => _increaseWaveRewardAssetRate; set => _increaseWaveRewardAssetRate = value; }


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

        public static EnemyData Create_Test()
        {
            return new EnemyData();
        }

        private EnemyData(string name = null)
        {

            _key = "Test";
            _spineModelKey = "BowSoldier_SkeletonData";
            _group = TYPE_ENEMY_GROUP.Normal;
            _typeLevelTheme = TYPE_LEVEL_THEME.Grass;

            _attackBulletKey = "Arrow";

            _startHealthValue = HealthData.Create_Test();
            _increaseLevelHealthValue = HealthData.Create_Test();
            _increaseLevelHealthRate = 0f;
            _increaseWaveHealthValue = HealthData.Create_Test();
            _increaseWaveHealthRate = 0f;
            _startAttackValue = AttackData.Create_Test();
            _increaseAttackValue = AttackData.Create_Test();
            _increaseAttackRate = 0.01f;
            _attackCount = 1;
            //_attackDelay = new float[1];
            //_attackDelay[0] = 1f;
            _attackDelay = 1f;

            _startRewardAssetValue = GoldAssetData.Create_Test();
            _increaseLevelRewardAssetValue = GoldAssetData.Create_Test();
            _increaseLevelRewardAssetRate = 1f;
            _increaseWaveRewardAssetValue = GoldAssetData.Create_Test();
            _increaseWaveRewardAssetRate = 1f;

            UnityEngine.Debug.LogWarning("테스트 적을 생성하였습니다");

        }

#endif
    }
}