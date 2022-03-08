namespace SEF.Data
{
    using UnityEngine;
    using Process;
    using Utility.Data;
    using System.Collections.Generic;

    [CreateAssetMenu(fileName = "MineData", menuName = "ScriptableObjects/MineData")]
    public class MineData : ScriptableObjectData
    {
        [SerializeField]
        private Sprite _icon;
        public Sprite Icon => _icon;

        [SerializeField]
        private MineAbilityData[] _mineAbilityDataArray;

        public int MaximumIndex => _mineAbilityDataArray.Length;
        public IAssetData GetTechAssetData(int index) => _mineAbilityDataArray[index].TechAssetData;
        public int GetMaxUpgradeData(int index) => _mineAbilityDataArray[index].DefaultMaxUpgradeValue;
        public IProcessData GetProcessData(int index) => _mineAbilityDataArray[index].ProcessData;
        public IAssetData GetUpgradeAssetData(int index, UpgradeData data) => _mineAbilityDataArray[index].GetUpgradeAssetData(data);


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static MineData Create_Test(string key = null)
        {
            return new MineData(key);
        }

        private MineData(string key)
        {
            Key = (string.IsNullOrEmpty(key)) ? "Test" : key;
            _mineAbilityDataArray = new MineAbilityData[1];
            _mineAbilityDataArray[0] = MineAbilityData.Create_Test();

        }


        public override void SetData(string[] arr)
        {
            Key = arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.Key];

            name = $"{typeof(UnitData).Name}_{Key}";

            _mineAbilityDataArray = new MineAbilityData[1];
            _mineAbilityDataArray[0] = new MineAbilityData();
            _mineAbilityDataArray[0].SetData(arr);
        }

        public override void AddData(string[] arr)
        {
            var list = new List<MineAbilityData>(_mineAbilityDataArray);

            var abilityData = new MineAbilityData();
            abilityData.SetData(arr);
            list.Add(abilityData);

            _mineAbilityDataArray = list.ToArray();
        }

        public override string[] GetData()
        {
            Debug.LogWarning("사용하지 않음");
            return null;
        }

        public override bool HasDataArray() => true;
        public override string[][] GetDataArray()
        {
            var list = new List<string[]>();
            for (int i = 0; i < _mineAbilityDataArray.Length; i++)
            {
                string[] arr = _mineAbilityDataArray[i].GetData();
                list.Add(arr);
            }

            return list.ToArray();
        }

#endif


        [System.Serializable]
        public struct MineAbilityData
        {

            [SerializeField]
            private SerializedProcessData _serializedProcessData;
            public IProcessData ProcessData => _serializedProcessData.GetData();


            [SerializeField]
            private SerializedAssetData _serializedStartUpgradeAssetData;
            public IAssetData StartUpgradeAssetData => _serializedStartUpgradeAssetData.GetData();

            [SerializeField]
            private float _increaseUpgradeValue;
            public float IncreaseUpgradeValue => _increaseUpgradeValue;

            [SerializeField]
            private float _increaseUpgradeRate;
            public float IncreaseUpgradeRate => _increaseUpgradeRate;

            [SerializeField]
            private int _defaultMaxUpgradeValue;
            public int DefaultMaxUpgradeValue => _defaultMaxUpgradeValue;

            [SerializeField]
            private SerializedAssetData _serializedTechAssetData;
            public IAssetData TechAssetData => _serializedTechAssetData.GetData();


            //ConditionUnlockData
            //ConditionUnlockValue


            public IAssetData GetUpgradeAssetData(UpgradeData data)
            {
                var assetData = (IAssetData)StartUpgradeAssetData.Clone();
                assetData.SetIsolationInterest(_increaseUpgradeValue, IncreaseUpgradeRate, data.Value);
                return assetData;
            }


#if UNITY_EDITOR
            public static MineAbilityData Create_Test(string key = null) => new MineAbilityData(key);

            public MineAbilityData(string key = "")
            {
                _serializedProcessData = SerializedProcessData.Create_Test(typeof(AssetProcessData));
                _serializedStartUpgradeAssetData = SerializedAssetData.Create_Test(SerializedAssetData.TYPE_ASSET_DATA_ATTRIBUTE.Gold, "100");
                _increaseUpgradeValue = 1;
                _increaseUpgradeRate = 0.125f;
                _defaultMaxUpgradeValue = 10;
                _serializedTechAssetData = new SerializedAssetData();
            }

            public void SetData(string[] arr)
            {
                _serializedProcessData.SetData(
               arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.TypeProcess],
               arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.TypeProcessAsset],
               arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.StartProcessAssetValue],
               arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.IncreaseProcessAssetValue],
               arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.ProcessTime]
               );
                _serializedStartUpgradeAssetData.SetData(arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.TypeUpgradeAsset], arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.StartUpgradeValue]);

                _increaseUpgradeValue = int.Parse(arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgradeValue]);
                _increaseUpgradeRate = float.Parse(arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgradeRate]);

                _defaultMaxUpgradeValue = int.Parse(arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.DefaultMaxUpgardeValue]);

                if (arr.Length >= (int)MineDataGenerator.TYPE_SHEET_COLUMNS.TypeTechAsset)
                {
                    _serializedTechAssetData.SetData(
                                arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.TypeTechAsset],
                                arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.TechAssetValue]
                                );
                }
            }


            public string[] GetData()
            {
                string[] arr = new string[System.Enum.GetValues(typeof(MineDataGenerator.TYPE_SHEET_COLUMNS)).Length];

                _serializedProcessData.GetData(
                    out string classTypeName,
                    out string typeAssetData,
                    out string assetValue,
                    out string increaseValue,
                    out string processTime
                    );

                arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.TypeProcess] = classTypeName;
                arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.TypeProcessAsset] = typeAssetData;
                arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.StartProcessAssetValue] = assetValue;
                arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.IncreaseProcessAssetValue] = increaseValue;
                arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.ProcessTime] = processTime;

                _serializedStartUpgradeAssetData.GetData(
                    out string upgradeTypeAssetData,
                    out string upgradeAssetValue
                    );

                arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.TypeUpgradeAsset] = upgradeTypeAssetData;
                arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.StartUpgradeValue] = upgradeAssetValue;

                arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgradeValue] = _increaseUpgradeValue.ToString();
                arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgradeRate] = _increaseUpgradeRate.ToString();

                arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.DefaultMaxUpgardeValue] = _defaultMaxUpgradeValue.ToString();

                _serializedTechAssetData.GetData(
                       out string typeTechAsset,
                       out string techAssetValue
                                   );

                arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.TypeTechAsset] = typeTechAsset;
                arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.TechAssetValue] = techAssetValue;

                return arr;
            }
#endif

        }
    }
}