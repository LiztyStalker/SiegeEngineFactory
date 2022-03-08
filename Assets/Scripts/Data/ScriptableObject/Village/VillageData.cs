namespace SEF.Data
{
    using UnityEngine;
    using Utility.Data;
    using Status;
    using System.Collections.Generic;

    [CreateAssetMenu(fileName = "VillageData", menuName = "ScriptableObjects/VillageData")]
    public class VillageData : ScriptableObjectData
    {
        [SerializeField]
        private Sprite _icon;
        public Sprite Icon => _icon;

        [SerializeField]
        private VillageAbillityData[] _villageAbilityDataArray;

        public int MaximumIndex => _villageAbilityDataArray.Length;
        public IAssetData GetTechAssetData(int index) => _villageAbilityDataArray[index].TechAssetData;
        public int GetMaxUpgradeData(int index) => _villageAbilityDataArray[index].DefaultMaxUpgradeValue;
        public IStatusData GetStatusData(int index) => _villageAbilityDataArray[index].StatusData;
        public IAssetData GetUpgradeAssetData(int index, UpgradeData data) => _villageAbilityDataArray[index].GetUpgradeAssetData(data);



#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static VillageData Create_Test(string key = null)
        {
            return new VillageData(key);
        }
        private VillageData(string key)
        {
            Key = (string.IsNullOrEmpty(key)) ? "Test" : key;
            _villageAbilityDataArray = new VillageAbillityData[1];
            _villageAbilityDataArray[0] = new VillageAbillityData();           
        }

        public override void SetData(string[] arr)
        {

            Key = arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.Key];

            name = $"{typeof(UnitData).Name}_{Key}";

            _villageAbilityDataArray = new VillageAbillityData[1];
            _villageAbilityDataArray[0] = new VillageAbillityData();
            _villageAbilityDataArray[0].SetData(arr);
        }

        public override void AddData(string[] arr)
        {
            var list = new List<VillageAbillityData>(_villageAbilityDataArray);

            var abilityData = new VillageAbillityData();
            abilityData.SetData(arr);
            list.Add(abilityData);

            _villageAbilityDataArray = list.ToArray();
        }


        public override string[][] GetDataArray()
        {
            var list = new List<string[]>();
            for (int i = 0; i < _villageAbilityDataArray.Length; i++)
            {
                string[] arr = _villageAbilityDataArray[i].GetData();
                list.Add(arr);
            }

            return list.ToArray();
        }

        public override string[] GetData()
        {
            return null;
        }

        public override bool HasDataArray()
        {
            return true;
        }


#endif
    }



    [System.Serializable]
    public struct VillageAbillityData
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
            assetData.SetIsolationInterest(_increaseUpgradeValue, _increaseUpgradeRate, data.Value);
            return assetData;
        }

#if UNITY_EDITOR
        public static VillageAbillityData Create_Test(string key) => new VillageAbillityData(key);
        private VillageAbillityData(string key = "")
        {
            _serializedStatusData = SerializedStatusData.Create_Test(typeof(IncreaseMaxPopulationStatusData));
            _serializedAssetData = SerializedAssetData.Create_Test(SerializedAssetData.TYPE_ASSET_DATA_ATTRIBUTE.Gold, "100");
            _increaseUpgradeValue = 1;
            _increaseUpgradeRate = 0.125f;
            _defaultMaxUpgradeValue = 10;
            _serializedTechAssetData = new SerializedAssetData();
        }

        public void SetData(string[] arr)
        {

            _serializedStatusData.SetData(
                arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.TypeStatusData],
                arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.TypeStatusValue],
                arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.StartStatusValue],
                arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.IncreaseStatusValue]
                );

            _serializedAssetData.SetData(
                arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.TypeUpgradeAsset],
                arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.StartUpgradeValue]
                );

            _increaseUpgradeValue = int.Parse(arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgradeValue]);
            _increaseUpgradeRate = float.Parse(arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgrateRate]);
            _defaultMaxUpgradeValue = int.Parse(arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.DefaultMaxUpgardeValue]);

            if (arr.Length >= (int)VillageDataGenerator.TYPE_SHEET_COLUMNS.TypeTechAsset)
            {
                _serializedTechAssetData.SetData(
                            arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.TypeTechAsset],
                            arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.TechAssetValue]
                            );
            }
        }
        public string[] GetData()
        {

            string[] arr = new string[System.Enum.GetValues(typeof(VillageDataGenerator.TYPE_SHEET_COLUMNS)).Length];

            _serializedStatusData.GetData(out string typeStatusData, out string typeStatusValue, out string startStatusValue, out string increaseStatusValue);

            arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.TypeStatusData] = typeStatusData;
            arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.TypeStatusValue] = typeStatusValue;
            arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.StartStatusValue] = startStatusValue;
            arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.IncreaseStatusValue] = increaseStatusValue;

            _serializedAssetData.GetData(out string typeUpgradeAsset, out string startUpgradeValue);

            arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.TypeUpgradeAsset] = typeUpgradeAsset;
            arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.StartUpgradeValue] = startUpgradeValue;

            arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgradeValue] = _increaseUpgradeValue.ToString();
            arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgrateRate] = _increaseUpgradeRate.ToString();
            arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.DefaultMaxUpgardeValue] = _defaultMaxUpgradeValue.ToString();

            _serializedTechAssetData.GetData(
                out string typeTechAsset,
                out string techAssetValue
                            );

            arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.TypeTechAsset] = typeTechAsset;
            arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.TechAssetValue] = techAssetValue;

            return arr;
        }
#endif
    }
}