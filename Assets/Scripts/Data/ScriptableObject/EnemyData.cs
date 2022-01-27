namespace SEF.Data
{
    using System.Collections.Generic;
    using UnityEngine;
    using Spine.Unity;

    public enum TYPE_LEVEL_THEME { Grass}

    public enum TYPE_ENEMY_GROUP { Normal, Boss, ThemeBoss}

    
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]    
    public class EnemyData : ScriptableObject
    {
        [SerializeField]
        private string _key;
        public string Key => _key;

        [SerializeField]
        private SkeletonDataAsset _skeletonDataAsset;

        public SkeletonDataAsset SkeletonDataAsset { 
            get => _skeletonDataAsset;
            set
            {
                _skeletonDataAsset = value;
                if (value != null)
                    _spineModelKey = value.name;
                else
                    _spineModelKey = null;
            }
        }

        [SerializeField]
        private string _spineModelKey;
        public string SpineModelKey => _spineModelKey;

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
        private float _scale = 1f;
        public float Scale { get => _scale; set => _scale = value; }

        [SerializeField]
        private HealthData _startHealthValue = NumberDataUtility.Create<HealthData>();
        public HealthData StartHealthValue => _startHealthValue;

        [SerializeField]
        private int _increaseLevelHealthValue;
        public int IncreaseLevelHealthValue { get => _increaseLevelHealthValue; set => _increaseLevelHealthValue = value; }

        [SerializeField]
        private float _increaseLevelHealthRate;
        public float IncreaseLevelHealthRate { get => _increaseLevelHealthRate; set => _increaseLevelHealthRate = value; }

        [SerializeField]
        private int _increaseWaveHealthValue;// = NumberDataUtility.Create<HealthData>();
        public int IncreaseWaveHealthValue { get => _increaseWaveHealthValue; set => _increaseWaveHealthValue = value; }

        [SerializeField]
        private float _increaseWaveHealthRate;
        public float IncreaseWaveHealthRate { get => _increaseWaveHealthRate; set => _increaseWaveHealthRate = value; }

        [SerializeField]
        private DamageData _startAttackValue = NumberDataUtility.Create<DamageData>();
        public DamageData StartAttackValue => _startAttackValue;

        [SerializeField]
        private int _increaseAttackValue;// = NumberDataUtility.Create<AttackData>();
        public int IncreaseAttackValue { get => _increaseAttackValue; set => _increaseAttackValue = value; }

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
        private UtilityManager.BulletData _attackBulletData;

        public UtilityManager.BulletData AttackBulletData { 
            get => _attackBulletData;
            set { 
                _attackBulletData = value;

                if (value != null)
                    _attackBulletKey = value.name;
                else
                    _attackBulletKey = "";
            } 
        }

        [SerializeField]
        private float _bulletScale = 1f;
        public float BulletScale { get => _bulletScale; set => _bulletScale = value; }






        [SerializeField]
        private List<AttackerData> _attackerDataList = new List<AttackerData>();
        public AttackerData[] AttackerDataArray => _attackerDataList.ToArray();


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public void AddAttackerData(AttackerData attackerData)
        {
            _attackerDataList.Add(attackerData);
        }

        public void RemoveAttackerData(AttackerData attackerData)
        {
            _attackerDataList.Remove(attackerData);
        }
#endif


        [SerializeField]
        private GoldAssetData _startRewardAssetValue = NumberDataUtility.Create<GoldAssetData>();
        public GoldAssetData StartRewardAssetValue => _startRewardAssetValue;

        [SerializeField]
        private int _increaseLevelRewardAssetValue;// = NumberDataUtility.Create<GoldAssetData>();
        public int IncreaseLevelRewardAssetValue { get => _increaseLevelRewardAssetValue; set => _increaseLevelRewardAssetValue = value; }

        [SerializeField]
        private float _increaseLevelRewardAssetRate;
        public float IncreaseLevelRewardAssetRate { get => _increaseLevelRewardAssetRate; set => _increaseLevelRewardAssetRate = value; }

        [SerializeField]
        private int _increaseWaveRewardAssetValue;// = NumberDataUtility.Create<GoldAssetData>();
        public int IncreaseWaveRewardAssetValue { get => _increaseWaveRewardAssetValue; set => _increaseWaveRewardAssetValue = value; }

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

            _startHealthValue = HealthData.Create_Test(100);
            _increaseLevelHealthValue = 2;// HealthData.Create_Test();
            _increaseLevelHealthRate = 0.125f;
            _increaseWaveHealthValue = 0;// HealthData.Create_Test();
            _increaseWaveHealthRate = 0.01f;
            _startAttackValue = DamageData.Create_Test(30);
            _increaseAttackValue = 1;// AttackData.Create_Test();
            _increaseAttackRate = 0.2f;
            _attackCount = 1;
            //_attackDelay = new float[1];
            //_attackDelay[0] = 1f;
            _attackDelay = 1f;

            _startRewardAssetValue = GoldAssetData.Create_Test(10);
            _increaseLevelRewardAssetValue = 2;// GoldAssetData.Create_Test();
            _increaseLevelRewardAssetRate = 0.125f;
            _increaseWaveRewardAssetValue = 0;// GoldAssetData.Create_Test();
            _increaseWaveRewardAssetRate = 0.1f;

            UnityEngine.Debug.LogWarning("테스트 적을 생성하였습니다");

        }

#endif
    }
}