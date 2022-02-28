namespace SEF.Data
{
    using System.Collections.Generic;
    using UnityEngine;
    using Spine.Unity;
    using Utility.Data;

    public enum TYPE_THEME_GROUP { Grass, Forest}

    public enum TYPE_ENEMY_GROUP { Normal, Resource, Boss, ThemeBoss}

    
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]    
    public class EnemyData : ScriptableObjectData
    {
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
#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public string SpineModelKey => (string.IsNullOrEmpty(_spineModelKey)) ? "BowSoldier_SkeletonData" : _spineModelKey;
#else
        public string SpineModelKey => _spineModelKey;
#endif

        [SerializeField]
        private string _spineSkinKey;
        public string SpineSkinKey { get => _spineSkinKey; set => _spineSkinKey = value; }


        [SerializeField]
        private TYPE_ENEMY_GROUP _group;
        public TYPE_ENEMY_GROUP Group { get => _group; set => _group = value; }

        [SerializeField]
        private TYPE_THEME_GROUP _typeLevelTheme;
        public TYPE_THEME_GROUP TypeLevelTheme { get => _typeLevelTheme; set => _typeLevelTheme = value; }

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
        private DamageData _startDamageValue = NumberDataUtility.Create<DamageData>();
        public DamageData StartAttackValue => _startDamageValue;

        [SerializeField]
        private int _increaseDamageValue;// = NumberDataUtility.Create<AttackData>();
        public int IncreaseAttackValue { get => _increaseDamageValue; set => _increaseDamageValue = value; }

        [SerializeField]
        private float _increaseDamageRate;
        public float IncreaseAttackRate { get => _increaseDamageRate; set => _increaseDamageRate = value; }


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

            Key = "Test";
            _spineModelKey = "BowSoldier_SkeletonData";
            _group = TYPE_ENEMY_GROUP.Normal;
            _typeLevelTheme = TYPE_THEME_GROUP.Grass;

            _attackBulletKey = "Arrow";

            _startHealthValue = HealthData.Create_Test(100);
            _increaseLevelHealthValue = 2;// HealthData.Create_Test();
            _increaseLevelHealthRate = 0.125f;
            _increaseWaveHealthValue = 1;// HealthData.Create_Test();
            _increaseWaveHealthRate = 0.125f;
            _startDamageValue = DamageData.Create_Test(30);
            _increaseDamageValue = 1;// AttackData.Create_Test();
            _increaseDamageRate = 0.125f;
            _attackCount = 1;
            //_attackDelay = new float[1];
            //_attackDelay[0] = 1f;
            _attackDelay = 1f;

            _startRewardAssetValue = GoldAssetData.Create_Test(10);
            _increaseLevelRewardAssetValue = 1;// GoldAssetData.Create_Test();
            _increaseLevelRewardAssetRate = 0.125f;
            _increaseWaveRewardAssetValue = 1;// GoldAssetData.Create_Test();
            _increaseWaveRewardAssetRate = 0.125f;

            UnityEngine.Debug.LogWarning("테스트 적을 생성하였습니다");

        }


        public override void SetData(string[] arr)
        {

            Key = arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.Key];

            name = $"{typeof(UnitData).Name}_{Key}";

            _spineModelKey = $"{Key}_SkeletonData";
            _group = (TYPE_ENEMY_GROUP)System.Enum.Parse(typeof(TYPE_ENEMY_GROUP), arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.TypeEnemyGroup]);
            _typeLevelTheme = (TYPE_THEME_GROUP)System.Enum.Parse(typeof(TYPE_THEME_GROUP), arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.TypeThemeGroup]);
            _scale = 1f;

            _attackBulletKey = arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.AttackBulletKey];

            _startHealthValue = HealthData.Create(arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.StartHealthValue]);
            _increaseLevelHealthValue = int.Parse(arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseLevelHealthValue]);
            _increaseLevelHealthRate = float.Parse(arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseLevelHealthRate]);
            _increaseWaveHealthValue = int.Parse(arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseWaveHealthValue]);
            _increaseWaveHealthRate = float.Parse(arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseWaveHealthRate]);
            _startDamageValue = DamageData.Create(arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.StartDamageValue]);
            _increaseDamageValue = int.Parse(arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseLevelDamageValue]);
            _increaseDamageRate = float.Parse(arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseLevelDamageRate]);
            _attackCount = int.Parse(arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.AttackCount]);
            _attackDelay = float.Parse(arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.AttackDelay]);

            _startRewardAssetValue = GoldAssetData.Create(arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.StartRewardAssetValue]);
            _increaseLevelRewardAssetValue = int.Parse(arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseRewardLevelAssetValue]);
            _increaseLevelRewardAssetRate = float.Parse(arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseRewardLevelAssetRate]);
            _increaseWaveRewardAssetValue = int.Parse(arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseRewardWaveAssetValue]);
            _increaseWaveRewardAssetRate = float.Parse(arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseRewardWaveAssetRate]);
        }

        public override string[] GetData()
        {

            string[] arr = new string[System.Enum.GetValues(typeof(EnemyDataGenerator.TYPE_SHEET_COLUMNS)).Length];

            arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.TypeEnemyGroup] = _group.ToString();
            arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.TypeThemeGroup] = _typeLevelTheme.ToString();
            _scale = 1f;

            arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.AttackBulletKey] = _attackBulletKey;

            arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.StartHealthValue] = _startHealthValue.GetValue();
            arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseLevelHealthValue] = _increaseLevelHealthValue.ToString();
            arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseLevelHealthRate] = _increaseLevelHealthRate.ToString();
            arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseWaveHealthValue] = _increaseWaveHealthValue.ToString();
            arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseWaveHealthRate] = _increaseWaveHealthRate.ToString();
            arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.StartDamageValue] = _startDamageValue.GetValue();
            arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseLevelDamageValue] = _increaseDamageValue.ToString();
            arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseLevelDamageRate] = _increaseDamageRate.ToString();
            arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.AttackCount] = _attackCount.ToString();
            arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.AttackDelay] = _attackDelay.ToString();

            //arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.TypeRewardAsset;
            arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.StartRewardAssetValue] = _startRewardAssetValue.GetValue();
            arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseRewardLevelAssetValue] = _increaseLevelRewardAssetValue.ToString();
            arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseRewardLevelAssetRate] = _increaseLevelRewardAssetRate.ToString();
            arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseRewardWaveAssetValue] = _increaseWaveRewardAssetValue.ToString();
            arr[(int)EnemyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseRewardWaveAssetRate] = _increaseWaveRewardAssetRate.ToString();

            return arr;
        }

        public override void AddData(string[] arr)
        {
        }

        public override string[][] GetDataArray()
        {
            return null;
        }

        public override bool HasDataArray()
        {
            return false;
        }

#endif
    }
}