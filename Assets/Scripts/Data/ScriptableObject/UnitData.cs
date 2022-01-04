namespace SEF.Data
{
    using UnityEngine;
    using Spine.Unity;

    [CreateAssetMenu(fileName = "UnitData", menuName = "ScriptableObjects/UnitData")]    
    public class UnitData : ScriptableObject
    {
        public enum TYPE_UNIT_GROUP { Thrower, Ram, Ballista, Catapult, MuzzleLoading, BreechLoading, RunOutCylinder, Missile}
        public enum TYPE_ATTACK_RANGE { Gun, Howitzer, Mortar, Melee}

        [SerializeField]
        private Sprite _icon;
        public Sprite icon { get => _icon; set => _icon = value; }

        [SerializeField]
        private string _key;
        public string Key { get => _key; set => _key = value; }

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
        private TYPE_UNIT_GROUP _group;
        public TYPE_UNIT_GROUP Group { get => _group; set => _group = value; }

        [SerializeField]
        private HealthData _startHealthValue = NumberDataUtility.Create<HealthData>();
        public HealthData StartHealthValue => _startHealthValue;

        [SerializeField]
        private HealthData _increaseHealthValue = NumberDataUtility.Create<HealthData>();
        public HealthData IncreaseHealthValue => _increaseHealthValue;

        [SerializeField]
        private float _increaseHealthRate;
        public float IncreaseHealthRate { get => _increaseHealthRate; set => _increaseHealthRate = value; }

        [SerializeField]
        private float _productTime;
        public float ProductTime { get => _productTime; set => _productTime = value; }

        [SerializeField]
        private AttackData _attackValue = NumberDataUtility.Create<AttackData>();
        public AttackData StartAttackValue => _attackValue;

        [SerializeField]
        private AttackData _increaseAttackValue = NumberDataUtility.Create<AttackData>();
        public AttackData IncreaseAttackValue => _increaseAttackValue;

        [SerializeField]
        private float _increaseAttackRate;
        public float IncreaseAttackRate { get => _increaseAttackRate; set => _increaseAttackRate = value; }

        [SerializeField]
        private TYPE_ATTACK_RANGE _typeAttackRange;
        public TYPE_ATTACK_RANGE TypeAttackRange { get => _typeAttackRange; set => _typeAttackRange = value; }

        [SerializeField]
        private int _attackPopulation;
        public int AttackPopulation { get => _attackPopulation; set => _attackPopulation = value; }

        [SerializeField]
        private int _attackCount;
        public int AttackCount { get => _attackCount; set => _attackCount = value; }

        [SerializeField]
        private float _attackDelay;
        public float AttackDelay { get => _attackDelay; set => _attackDelay = value; }

        [SerializeField]
        private string _attackBulletKey;

        public string AttackBulletKey { get => _attackBulletKey; set => _attackBulletKey = value; }

        [SerializeField]
        private GoldAssetData _startUpgradeAsset = NumberDataUtility.Create<GoldAssetData>();
        public GoldAssetData StartUpgradeAsset => _startUpgradeAsset;

        [SerializeField]
        private GoldAssetData _increaseUpgradeAssetValue = NumberDataUtility.Create<GoldAssetData>();
        public GoldAssetData IncreaseUpgradeAssetValue => _increaseUpgradeAssetValue;

        [SerializeField]
        private float _increaseUpgradeAssetRate;
        public float IncreaseUpgradeAssetRate { get => _increaseUpgradeAssetRate; set => _increaseUpgradeAssetRate = value; }

        [SerializeField]
        private int _maximumUpgradeValue;
        public int MaximumUpgradeValue { get => _maximumUpgradeValue; set => _maximumUpgradeValue = value; }
        //private string[] _conditionTechTree;
        //public string[] ConditionTechTree => _conditionTechTree;

        //private AssetData[] _conditionTechTreeValue;
        //public AssetData[] ConditionTechTreeValue => _conditionTechTreeValue;

        [SerializeField]
        private GoldAssetData _techTreeAsset = NumberDataUtility.Create<GoldAssetData>();
        public GoldAssetData TechTreeAsset => _techTreeAsset;

        [SerializeField]
        private string[] _techTreeKeys;
        public string[] TechTreeKeys { get => _techTreeKeys; set => _techTreeKeys = value; }



#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

        public static UnitData Create_Test()
        {
            return new UnitData();
        }

        private UnitData(string name = null)
        {

            _key = "Test";
            _spineModelKey = "BowSoldier_SkeletonData";
            _group = TYPE_UNIT_GROUP.Thrower;

            _attackBulletKey = "Arrow";

            _startHealthValue = HealthData.Create_Test();
            _increaseHealthValue = HealthData.Create_Test();
            _increaseHealthRate = 0f;
            _productTime = 1f;
            _attackValue = AttackData.Create_Test();
            _increaseAttackValue = AttackData.Create_Test();
            _increaseAttackRate = 0.01f;
            _typeAttackRange = TYPE_ATTACK_RANGE.Gun;
            _attackPopulation = 1;
            _attackCount = 1;
            //            _attackDelay = new float[1];
            //            _attackDelay[0] = 1f;
            _attackDelay = 1f;
            _startUpgradeAsset = GoldAssetData.Create_Test();
            _increaseUpgradeAssetValue = GoldAssetData.Create_Test();
            _increaseUpgradeAssetRate = 0.01f;
            //private string[] _conditionTechTree;
            //private AssetData[] _conditionTechTreeValue;
            _techTreeAsset = GoldAssetData.Create_Test();
            _techTreeKeys = new string[0];

            UnityEngine.Debug.LogWarning("테스트 유닛을 생성하였습니다");

        }

#endif
    }
}