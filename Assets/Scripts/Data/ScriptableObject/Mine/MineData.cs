namespace SEF.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Process;
    using Utility.Data;

    [CreateAssetMenu(fileName = "MineData", menuName = "ScriptableObjects/MineData")]
    public class MineData : ScriptableObjectData
    {
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


        [SerializeField]
        private int _defaultMaxUpgradeValue;
        private int DefaultMaxUpgradeValue => _defaultMaxUpgradeValue;

        //ConditionUnlockData
        //ConditionUnlockValue
 
#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static MineData Create_Test()
        {
            return new MineData();
        }

        private MineData()
        {
            _serializedProcessData = SerializedProcessData.Create_Test(typeof(AssetProcessData));
            _serializedStartUpgradeAssetData = SerializedAssetData.Create_Test(SerializedAssetData.TYPE_ASSET_DATA_ATTRIBUTE.Gold, "100");
            _increaseUpgradeValue = 1;
            _increaseUpgradeRate = 0.125f;
        }


        public override void SetData(string[] arr)
        {
            Key = arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.Key];

            name = $"{typeof(UnitData).Name}_{Key}";

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
           
        }

        public override void AddData(string[] arr)
        {
        }

        public override string[] GetData()
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

            _serializedStartUpgradeAssetData.GetData(out string upgradeTypeAssetData, out string upgradeAssetValue);

            arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.TypeUpgradeAsset] = upgradeTypeAssetData;
            arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.StartUpgradeValue] = upgradeAssetValue;

            arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgradeValue] = _increaseUpgradeValue.ToString();
            arr[(int)MineDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgradeRate] = _increaseUpgradeRate.ToString();

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
}