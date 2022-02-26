namespace SEF.Data
{
    using System.Collections.Generic;
    using UnityEngine;
    using Spine.Unity;
    using Utility.Data;

    [CreateAssetMenu(fileName = "UnitData", menuName = "ScriptableObjects/UnitData")]    
    public class UnitData : ScriptableObjectData
    {
        public enum TYPE_UNIT_GROUP { Thrower, Ram, Ballista, Catapult, MuzzleLoading, Mortar, BreechLoading, RunOutCylinder, Missile}
        public enum TYPE_ATTACK_RANGE { Gun, Howitzer, Mortar, Melee}
        public enum TYPE_TECH_TREE { AttackValue, AttackDelay, HealthValue, RangeValue, AttackerAttackValue, AttackerDelayValue, AttackerCount }

        [SerializeField]
        private Sprite _icon;
        public Sprite icon { get => _icon; set => _icon = value; }

        [SerializeField]
        private SkeletonDataAsset _skeletonDataAsset;
        public SkeletonDataAsset SkeletonDataAsset
        {
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
        private float _scale = 1f;
        public float Scale { 
            get => _scale;
            set { _scale = value; UnityEngine.Debug.Log(value); }
        }

        [SerializeField]
        private TYPE_UNIT_GROUP _group;
        public TYPE_UNIT_GROUP Group { get => _group; set => _group = value; }

        [SerializeField]
        private HealthData _startHealthValue = NumberDataUtility.Create<HealthData>();
        public HealthData StartHealthValue => _startHealthValue;

        [SerializeField]
        private int _increaseHealthValue;// = NumberDataUtility.Create<HealthData>();
        public int IncreaseHealthValue { get => _increaseHealthValue; set => _increaseHealthValue = value; }

        [SerializeField]
        private float _increaseHealthRate;
        public float IncreaseHealthRate { get => _increaseHealthRate; set => _increaseHealthRate = value; }

        [SerializeField]
        private float _productTime;
        public float ProductTime { get => _productTime; set => _productTime = value; }

        [SerializeField]
        private bool _isAttack = true;

        public bool IsAttack { get => _isAttack; set => _isAttack = value; }

        [SerializeField]
        private DamageData _attackValue = NumberDataUtility.Create<DamageData>();
        public DamageData StartAttackValue => _attackValue;

        [SerializeField]
        private int _increaseAttackValue;// = NumberDataUtility.Create<AttackData>();
        public int IncreaseAttackValue { get => _increaseAttackValue; set => _increaseAttackValue = value; }

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
        private List<AttackerData> _attackerDataList = new List<AttackerData>();

        public AttackerData[] AttackerDataArray => _attackerDataList.ToArray();
        public void AddAttackerData(AttackerData attackerData) => _attackerDataList.Add(attackerData);
        public void RemoveAttackerData(AttackerData attackerData) => _attackerDataList.Remove(attackerData);

        [SerializeField]
        private UtilityManager.BulletData _attackBulletData;

        public UtilityManager.BulletData AttackBulletData 
        { 
            get => _attackBulletData;
            set { 
                _attackBulletData = value;
                if(value != null)
                    _attackBulletKey = value.name;
                else
                    _attackBulletKey = "";
            }
        }

        [SerializeField]
        private float _bulletScale = 1f;
        public float BulletScale { get => _bulletScale; set => _bulletScale = value; }

        [SerializeField]
        private GoldAssetData _startUpgradeAsset = NumberDataUtility.Create<GoldAssetData>();
        public GoldAssetData StartUpgradeAsset => _startUpgradeAsset;

        [SerializeField]
        private int _increaseUpgradeAssetValue;// = NumberDataUtility.Create<GoldAssetData>();
        public int IncreaseUpgradeAssetValue { get => _increaseUpgradeAssetValue; set => _increaseUpgradeAssetValue = value; }

        [SerializeField]
        private float _increaseUpgradeAssetRate;
        public float IncreaseUpgradeAssetRate { get => _increaseUpgradeAssetRate; set => _increaseUpgradeAssetRate = value; }

        [SerializeField]
        private int _defaultMaxUpgradeValue = 10;
        public int DefaultMaxUpgradeValue { get => _defaultMaxUpgradeValue; set => _defaultMaxUpgradeValue = value; }

        [SerializeField]
        private UnitTechData[] _unitTechDataArray;
        public UnitTechData[] UnitTechDataArray => _unitTechDataArray;
//        public void AddUnitTechData(UnitTechData data) => _unitTechDataArray.Add(data);
//        public void RemoveUnitTechData(UnitTechData data) => _unitTechDataArray.Remove(data);



#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

        public static UnitData Create_Test(string name = null)
        {
            return new UnitData(name);
        }

        private UnitData(string name = null)
        {

            Key = (string.IsNullOrEmpty(name)) ? "Test" : name;
            _spineModelKey = "BowSoldier_SkeletonData";
            _group = TYPE_UNIT_GROUP.Thrower;
            _scale = 1f;

            _attackBulletKey = "Arrow";

            _startHealthValue = HealthData.Create_Test(100);
            _increaseHealthValue = 1;
            _increaseHealthRate = 0.05f;
            _productTime = 1f;
            _attackValue = DamageData.Create_Test(15);
            _increaseAttackValue = 1;
            _increaseAttackRate = 0.05f;
            _typeAttackRange = TYPE_ATTACK_RANGE.Gun;
            _attackPopulation = 1;
            _attackCount = 1;
            _attackDelay = 1f;
            _startUpgradeAsset = GoldAssetData.Create_Test(10);
            _increaseUpgradeAssetValue = 1;
            _increaseUpgradeAssetRate = 0.05f;
            //private string[] _conditionTechTree;
            //private AssetData[] _conditionTechTreeValue;
            _defaultMaxUpgradeValue = 10;
            _unitTechDataArray = null;

            UnityEngine.Debug.LogWarning("테스트 유닛을 생성하였습니다");

        }




        public override void SetData(string[] arr)
        {

            Key = arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.Key];

            name = $"{typeof(UnitData).Name}_{Key}";

            _spineModelKey = $"{Key}_SkeletonData";
            _group = (TYPE_UNIT_GROUP)System.Enum.Parse(typeof(TYPE_UNIT_GROUP), arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.Group]);
            _scale = 1f; //Scale

            _attackBulletKey = arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.BulletDataKey];

            _startHealthValue = HealthData.Create(arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.StartHealthValue]);
            _increaseHealthValue = int.Parse(arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.IncreaseHealthValue]);
            _increaseHealthRate = float.Parse(arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.IncreaseHealthRate]);
            _productTime = float.Parse(arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.ProductTime]);
            _attackValue = DamageData.Create(arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.StartDamageValue]);
            _increaseAttackValue = int.Parse(arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.IncreaseDamageValue]);
            _increaseAttackRate = float.Parse(arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.IncreaseDamageRate]);
            _typeAttackRange = (TYPE_ATTACK_RANGE)System.Enum.Parse(typeof(TYPE_ATTACK_RANGE), arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.TypeAttackRange]);
            _attackPopulation = int.Parse(arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.AttackPopulation]);
            _attackCount = int.Parse(arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.AttackCount]);
            _attackDelay = float.Parse(arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.AttackDelay]);
            _startUpgradeAsset = GoldAssetData.Create(arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.StartUpgradeAsset]);
            _increaseUpgradeAssetValue = int.Parse(arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgradeAssetValue]);
            _increaseUpgradeAssetRate = float.Parse(arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgradeAssetRate]);

            Debug.Log(arr.Length + " " + (int)UnitDataGenerator.TYPE_SHEET_COLUMNS.TypeTechList);

            if (arr.Length > (int)UnitDataGenerator.TYPE_SHEET_COLUMNS.TypeTechList)
            {
                var typeTechList = arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.TypeTechList];
                var techUnitkeys = arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.TechUnitKeys];
                var typeTechAssets = arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.TypeTechAssets];
                var techAssetValues = arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.TechAssetValues];


                var list = typeTechList.Split('/');
                var keys = techUnitkeys.Split('/');
                var assets = typeTechAssets.Split('/');
                var values = techAssetValues.Split('/');


                if (list.Length == keys.Length && list.Length == assets.Length && list.Length == values.Length)
                {
                    _unitTechDataArray = new UnitTechData[list.Length];
                    for (int i = 0; i < list.Length; i++)
                    {
                        Debug.Log("Tech " + i);
                        UnitTechData data = new UnitTechData();
                        data.SetData(list[i], keys[i], assets[i], values[i]);
                        _unitTechDataArray[i] = data;
//                        _unitTechDataArray.Add(data);
                    }
                }
            }
        }

        public override void AddData(string[] arr)
        {
        }

        public override string[] GetData()
        {
            string[] arr = new string[System.Enum.GetValues(typeof(UnitDataGenerator.TYPE_SHEET_COLUMNS)).Length];

            //_key = arr[(int)UnitDataGenerator.TYPE_UNIT_SHEET_COLUMNS.Key];

            //name = $"{typeof(UnitData).Name}_{_key}";

            //_spineModelKey = $"{_key}_SkeletonData";
            arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.Group] = _group.ToString();
            //_scale = 1f; //Scale

            if (!string.IsNullOrEmpty(_attackBulletKey)) {
                var split = _attackBulletKey.Split('_');
                if (split.Length == 2)
                {
                    arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.BulletDataKey] = split[1];
                }
                else {
                    arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.BulletDataKey] = split[0];
                }
            }

            arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.StartHealthValue] = _startHealthValue.GetValue();
            arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.IncreaseHealthValue] = _increaseHealthValue.ToString();
            arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.IncreaseHealthRate] = _increaseHealthRate.ToString();
            arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.ProductTime] = _productTime.ToString();
            arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.StartDamageValue] = _attackValue.ValueText;
            arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.IncreaseDamageValue] = _increaseAttackValue.ToString();
            arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.IncreaseDamageRate] = _increaseAttackRate.ToString();
            arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.TypeAttackRange] = _typeAttackRange.ToString();
            arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.AttackPopulation] = _attackPopulation.ToString();
            arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.AttackCount] = _attackCount.ToString();
            arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.AttackDelay] = _attackDelay.ToString();
            arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.StartUpgradeAsset] = _startUpgradeAsset.ValueText;
            arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgradeAssetValue] = _increaseUpgradeAssetValue.ToString();
            arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgradeAssetRate] = _increaseUpgradeAssetRate.ToString();


            if(_unitTechDataArray.Length != 0)
            {
                var typeTechList = "";
                var techUnitkeys = "";
                var typeTechAssets = "";
                var techAssetValues = "";

                for (int i = 0; i < _unitTechDataArray.Length; i++)
                {
                    _unitTechDataArray[i].GetData(out string list, out string keys, out string assets, out string values);
                    typeTechList += list;
                    techUnitkeys += keys;
                    typeTechAssets += assets;
                    techAssetValues += values;

                    if(i != _unitTechDataArray.Length - 1)
                    {
                        typeTechList += "/";
                        techUnitkeys += "/";
                        typeTechAssets += "/";
                        techAssetValues += "/";
                    }
                }
                arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.TypeTechList] = typeTechList;
                arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.TechUnitKeys] = techUnitkeys;
                arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.TypeTechAssets] = typeTechAssets;
                arr[(int)UnitDataGenerator.TYPE_SHEET_COLUMNS.TechAssetValues] = techAssetValues;
            }

            return arr;
        }

        public override bool HasDataArray()
        {
            return false;
        }

        public override string[][] GetDataArray()
        {
            return null;
        }

#endif

    }

    [System.Serializable]
    public struct UnitTechData
    {
        [SerializeField]
        private UnitData.TYPE_TECH_TREE _typeTechTree;
        public UnitData.TYPE_TECH_TREE TypeTechTree => _typeTechTree;


        [SerializeField]
        private string _techUnitKey;
        public string TechUnitKey => _techUnitKey;


        [SerializeField]
        private SerializedAssetData _serializedTechAssetData;
        public IAssetData StartUpgradeAssetData => _serializedTechAssetData.GetData();


        public static UnitTechData Create_Test(string key = null)
        {
            return new UnitTechData(UnitData.TYPE_TECH_TREE.AttackValue, key, SerializedAssetData.TYPE_ASSET_DATA_ATTRIBUTE.Gold, "1000");
        }

        public static UnitTechData Create_Test(UnitData.TYPE_TECH_TREE typeTechTree, string techUnitkey, SerializedAssetData.TYPE_ASSET_DATA_ATTRIBUTE typeTechAsset, string assetValue)
        {
            return new UnitTechData(typeTechTree, techUnitkey, typeTechAsset, assetValue);
        }

        private UnitTechData(UnitData.TYPE_TECH_TREE typeTechTree, string techUnitkey, SerializedAssetData.TYPE_ASSET_DATA_ATTRIBUTE typeAssetData, string assetValue)
        {
            _techUnitKey = techUnitkey;
            _typeTechTree = typeTechTree;
            _techUnitKey = techUnitkey;
            _serializedTechAssetData = SerializedAssetData.Create_Test(typeAssetData, assetValue);
        }

        public void SetData(string typeTechTree, string techUnitkey, string typeAssetData, string assetValue)
        {
            _typeTechTree = (UnitData.TYPE_TECH_TREE)System.Enum.Parse(typeof(UnitData.TYPE_TECH_TREE), typeTechTree);
            _techUnitKey = techUnitkey;
            _serializedTechAssetData.SetData(typeAssetData, assetValue);
        }

        public void GetData(out string typeTechTree, out string techUnitkey, out string typeAssetData, out string assetValue)
        {
            typeTechTree = _typeTechTree.ToString();
            techUnitkey = _techUnitKey;
            _serializedTechAssetData.GetData(out typeAssetData, out assetValue);
        }
    }
}