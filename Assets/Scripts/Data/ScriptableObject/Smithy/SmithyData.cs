namespace SEF.Data
{
    using UnityEngine;
    using SEF.Status;
    using Utility.Data;
    using System.Collections.Generic;

    [CreateAssetMenu(fileName = "SmithyData", menuName = "ScriptableObjects/SmithyData")]

    public class SmithyData : ScriptableObjectData
    {

        [SerializeField]
        private Sprite _icon;
        public Sprite Icon => _icon;

        [SerializeField]
        private SmithyAbillityData[] _smithyAbilityDataArray;

        public int MaximumIndex => _smithyAbilityDataArray.Length;
        public IAssetData GetTechAssetData(int index) => _smithyAbilityDataArray[index].TechAssetData;
        public int GetMaxUpgradeData(int index) => _smithyAbilityDataArray[index].DefaultMaxUpgradeValue;
        public IStatusData GetStatusData(int index) => _smithyAbilityDataArray[index].StatusData;
        public IAssetData GetUpgradeAssetData(int index, UpgradeData data) => _smithyAbilityDataArray[index].GetUpgradeAssetData(data);

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static SmithyData Create_Test(string key = null)
        {
            return new SmithyData(key);
        }
        private SmithyData(string key)
        {
            Key = key;
            _smithyAbilityDataArray = new SmithyAbillityData[1];
            _smithyAbilityDataArray[0] = new SmithyAbillityData();
        }

        public override void SetData(string[] arr)
        {
            Key = arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.Key];

            name = $"{typeof(UnitData).Name}_{Key}";

            _smithyAbilityDataArray = new SmithyAbillityData[1];
            _smithyAbilityDataArray[0] = new SmithyAbillityData();
            _smithyAbilityDataArray[0].SetData(arr);
        }

        public override void AddData(string[] arr)
        {
            var list = new List<SmithyAbillityData>(_smithyAbilityDataArray);

            var abilityData = new SmithyAbillityData();
            abilityData.SetData(arr);
            list.Add(abilityData);

            _smithyAbilityDataArray = list.ToArray();
        }


        public override string[] GetData()
        {
            return null;
        }

        public override bool HasDataArray()
        {
            return true;
        }

        public override string[][] GetDataArray()
        {
            var list = new List<string[]>();
            for (int i = 0; i < _smithyAbilityDataArray.Length; i++)
            {
                string[] arr = _smithyAbilityDataArray[i].GetData();
                list.Add(arr);
            }

            return list.ToArray();
        }



#endif
    }


    [System.Serializable]
    public struct SmithyAbillityData
    {

        [SerializeField]
        private SerializedStatusData _serializedStatusData;
        public IStatusData StatusData => _serializedStatusData.GetSerializeData();


        [SerializeField]
        private SerializedAssetData _serializedAssetData;
        public IAssetData StartUpgradeAssetData => _serializedAssetData.GetData();

        [SerializeField]
        private int _defaultMaxUpgradeValue;
        public int DefaultMaxUpgradeValue => _defaultMaxUpgradeValue;

        [SerializeField]
        private int _increaseUpgradeValue;
        public int IncreaseUpgradeValue => _increaseUpgradeValue;

        [SerializeField]
        private float _increaseUpgradeRate;
        public float IncreaseUpgradeRate => _increaseUpgradeRate;


        [SerializeField]
        private SerializedAssetData _serializedTechAssetData;
        public IAssetData TechAssetData => _serializedTechAssetData.GetData();


        public IAssetData GetUpgradeAssetData(UpgradeData data)
        {
            var assetData = (IAssetData)StartUpgradeAssetData.Clone();
            //assetData.SetCompoundInterest(_increaseUpgradeValue, IncreaseUpgradeRate, data.Value);
            assetData.SetIsolationInterest(_increaseUpgradeValue, IncreaseUpgradeRate, data.Value);
            return assetData;
        }


#if UNITY_EDITOR
        public static SmithyAbillityData Create_Test(string key) => new SmithyAbillityData(key);

        public SmithyAbillityData(string key = "")
        {
            _serializedStatusData = SerializedStatusData.Create_Test(typeof(UnitDamageValueStatusData));
            _serializedAssetData = SerializedAssetData.Create_Test(SerializedAssetData.TYPE_ASSET_DATA_ATTRIBUTE.Gold, "100");
            _increaseUpgradeValue = 1;
            _increaseUpgradeRate = 0.125f;
            _defaultMaxUpgradeValue = 10;
            _serializedTechAssetData = new SerializedAssetData();
        }

        public void SetData(string[] arr)
        {
            _serializedStatusData.SetData(
                arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.TypeStatusData],
                arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.TypeStatusValue],
                arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.StartStatusValue],
                arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseStatusValue]
                );

            _serializedAssetData.SetData(
                arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.TypeUpgradeAsset],
                arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.StartUpgradeValue]
                );

            _increaseUpgradeValue = int.Parse(arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgradeValue]);
            _increaseUpgradeRate = float.Parse(arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgrateRate]);
            _defaultMaxUpgradeValue = int.Parse(arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.DefaultMaxUpgardeValue]);

            if (arr.Length >= (int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.TypeTechAsset)
            {
                _serializedTechAssetData.SetData(
                            arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.TypeTechAsset],
                            arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.TechAssetValue]
                            );
            }           
            
        }


        public string[] GetData()
        {
            string[] arr = new string[System.Enum.GetValues(typeof(SmithyDataGenerator.TYPE_SHEET_COLUMNS)).Length];

            _serializedStatusData.GetData(out string typeStatusData, out string typeStatusValue, out string startStatusValue, out string increaseStatusValue);

            arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.TypeStatusData] = typeStatusData;
            arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.TypeStatusValue] = typeStatusValue;
            arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.StartStatusValue] = startStatusValue;
            arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseStatusValue] = increaseStatusValue;

            _serializedAssetData.GetData(out string typeUpgradeAsset, out string startUpgradeValue);

            arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.TypeUpgradeAsset] = typeUpgradeAsset;
            arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.StartUpgradeValue] = startUpgradeValue;

            arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgradeValue] = _increaseUpgradeValue.ToString();
            arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgrateRate] = _increaseUpgradeRate.ToString();
            arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.DefaultMaxUpgardeValue] = _defaultMaxUpgradeValue.ToString();

            _serializedTechAssetData.GetData(
                out string typeTechAsset,
                out string techAssetValue
                            );

            arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.TypeTechAsset] = typeTechAsset;
            arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.TechAssetValue] = techAssetValue;

            return arr;
        }
#endif

    }
}