namespace SEF.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Process;
    using Utility.Data;

    [CreateAssetMenu(fileName = "VillageData", menuName = "ScriptableObjects/VillageData")]
    public class VillageData : ScriptableObjectData
    {
        [SerializeField]
        private string _key;
        public string Key => _key;

        [SerializeField]
        private Sprite _icon;
        public Sprite Icon => _icon;


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

        //ConditionUnlockData
        //ConditionUnlockValue
        //MaxUpgardeValue
 
#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static VillageData Create_Test()
        {
            return new VillageData();
        }

        private VillageData()
        {
            _serializedProcessData = SerializedProcessData.Create_Test(typeof(AssetProcessData));
            _serializedStartUpgradeAssetData = SerializedAssetData.Create_Test(SerializedAssetData.TYPE_ASSET_DATA_ATTRIBUTE.Gold, "100");
            _increaseUpgradeValue = 1;
            _increaseUpgradeRate = 0.125f;
        }


        public override void SetData(string[] arr)
        {
            _key = arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.Key];

            name = $"{typeof(UnitData).Name}_{_key}";

            _serializedProcessData.SetData(
                arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.TypeProcess],
                arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.TypeAsset],
                arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.StartProcessAssetValue],
                arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.IncreaseProcessAssetValue],
                arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.IncreaseProcessAssetRate],
                arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.ProcessTime]
                );
            _serializedStartUpgradeAssetData.SetData(arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.TypeUpgradeAsset], arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.StartUpgradeValue]);

            _increaseUpgradeValue = int.Parse(arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgradeValue]);
            _increaseUpgradeRate = float.Parse(arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgradeRate]);
           
        }

        public override string[] GetData()
        {

            string[] arr = new string[System.Enum.GetValues(typeof(VillageDataGenerator.TYPE_SHEET_COLUMNS)).Length];

            _serializedProcessData.GetData(
                out string classTypeName,
                out string typeAssetData,
                out string assetValue,
                out string increaseValue,
                out string increaseRate,
                out string processTime
                );

            arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.TypeProcess] = classTypeName;
            arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.TypeAsset] = typeAssetData;
            arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.StartProcessAssetValue] = assetValue;
            arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.IncreaseProcessAssetValue] = increaseValue;
            arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.IncreaseProcessAssetRate] = increaseRate;
            arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.ProcessTime] = processTime;

            _serializedStartUpgradeAssetData.GetData(out string upgradeTypeAssetData, out string upgradeAssetValue);

            arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.TypeUpgradeAsset] = upgradeTypeAssetData;
            arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.StartUpgradeValue] = upgradeAssetValue;

            arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgradeValue] = _increaseUpgradeValue.ToString();
            arr[(int)VillageDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgradeRate] = _increaseUpgradeRate.ToString();

            return arr;
        }

#endif 
    }
}