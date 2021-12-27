namespace SEF.Data
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "UnitData", menuName = "ScriptableObjects/UnitData")]    
    public class UnitData : ScriptableObject
    {
        public enum TYPE_UNIT_GROUP { Thrower, Ram, Ballista, Catapult, MuzzleLoading, BreechLoading, RunOutCylinder, Missile}
        public enum TYPE_ATTACK_RANGE { Gun, Howitzer, Mortar, Melee}

        [SerializeField]
        private string _key;
        public string Name => _key;

        [SerializeField]
        private string _spineModelKey;
        public string SpineModelKey => _spineModelKey;

        [SerializeField]
        private string _spineSkinKey;
        public string SpineSkinKey => _spineSkinKey;

        [SerializeField]
        private TYPE_UNIT_GROUP _group;
        public TYPE_UNIT_GROUP Group => _group;

        [SerializeField]
        private HealthData _healthValue;
        public HealthData HealthValue => _healthValue;

        [SerializeField]
        private HealthData _increaseHealthValue;
        public HealthData IncreaseHealthValue => _increaseHealthValue;

        [SerializeField]
        private float _increaseHealthRate;
        public float IncreaseHealthRate => _increaseHealthRate;

        [SerializeField]
        private float _productTime;
        public float ProductTime => _productTime;

        [SerializeField]
        private AttackData _attackValue;
        public AttackData AttackValue => _attackValue;

        [SerializeField]
        private AttackData _increaseAttackValue;
        public AttackData IncreaseAttackValue => _increaseAttackValue;

        [SerializeField]
        private float _increaseAttackRate;
        public float IncreaseAttackRate => _increaseAttackRate;

        [SerializeField]
        private TYPE_ATTACK_RANGE _typeAttackRange;
        public TYPE_ATTACK_RANGE TypeAttackRange => _typeAttackRange;

        [SerializeField]
        private int _attackPopulation;
        public int AttackPopulation => _attackPopulation;

        [SerializeField]
        private int _attackCount;
        public int AttackCount => _attackCount;

        [SerializeField]
        private float[] _attackDelay;
        public float[] AttackDelay => _attackDelay;

        [SerializeField]
        private string _attackBulletKey;

        public string AttackBulletKey => _attackBulletKey;

        [SerializeField]
        private AssetData _startUpgradeAsset;
        public AssetData StartUpgradeAsset => _startUpgradeAsset;

        [SerializeField]
        private AssetData _increaseUpgradeAssetValue;
        public AssetData IncreaseUpgradeAssetValue => _increaseUpgradeAssetValue;

        [SerializeField]
        private float _increaseUpgradeAssetRate;
        public float IncreaseUpgradeAssetRate => _increaseUpgradeAssetRate;

        //private string[] _conditionTechTree;
        //public string[] ConditionTechTree => _conditionTechTree;

        //private AssetData[] _conditionTechTreeValue;
        //public AssetData[] ConditionTechTreeValue => _conditionTechTreeValue;

        [SerializeField]
        private AssetData _techTreeAsset;
        public AssetData TechTreeAsset => _techTreeAsset;

        [SerializeField]
        private string[] _techTreeKeys;
        public string[] TechTreeKeys => _techTreeKeys;



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

            _healthValue = HealthData.Create_Test();
            _increaseHealthValue = HealthData.Create_Test();
            _increaseHealthRate = 0f;
            _productTime = 1f;
            _attackValue = AttackData.Create_Test();
            _increaseAttackValue = AttackData.Create_Test();
            _increaseAttackRate = 0.01f;
            _typeAttackRange = TYPE_ATTACK_RANGE.Gun;
            _attackPopulation = 1;
            _attackCount = 1;
            _attackDelay = new float[1];
            _attackDelay[0] = 1f;
            _startUpgradeAsset = AssetData.Create_Test();
            _increaseUpgradeAssetValue = AssetData.Create_Test();
            _increaseUpgradeAssetRate = 0.01f;
            //private string[] _conditionTechTree;
            //private AssetData[] _conditionTechTreeValue;
            _techTreeAsset = AssetData.Create_Test();
            _techTreeKeys = new string[0];

            UnityEngine.Debug.LogWarning("테스트 유닛을 생성하였습니다");

        }

#endif
    }
}