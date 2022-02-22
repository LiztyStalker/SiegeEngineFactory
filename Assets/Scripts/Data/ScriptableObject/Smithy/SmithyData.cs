namespace SEF.Data
{
    using UnityEngine;
    using SEF.Status;
    using Utility.Data;

    [CreateAssetMenu(fileName = "BlacksmithData", menuName = "ScriptableObjects/BlacksmithData")]

    public class SmithyData : ScriptableObjectData
    {

        [SerializeField]
        private Sprite _icon;
        public Sprite Icon => _icon;

        [SerializeField]
        private SerializedStatusData _serializedStatusData;
        public IStatusData StatusData => _serializedStatusData.GetSerializeData();


        [SerializeField]
        private SerializedAssetData _serializedAssetData;
        public IAssetData StartUpgradeAssetData => _serializedAssetData.GetData();

        [SerializeField]
        private int _increaseUpgradeValue;
        public int IncreaseUpgradeValue => _increaseUpgradeValue;

        [SerializeField]
        private float _increaseUpgradeRate;
        public float IncreaseUpgradeRate => _increaseUpgradeRate;

        [SerializeField]
        private int _maxUpgradeValue;
        public float MaxUpgradeValue => _maxUpgradeValue;


        public IAssetData GetUpgradeAssetData(UpgradeData data)
        {
            var assetData = (IAssetData)StartUpgradeAssetData.Clone();
            assetData.SetCompoundInterest(_increaseUpgradeValue, IncreaseUpgradeRate, data.Value);
            return assetData;
            
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static SmithyData Create_Test(string key = null)
        {
            return new SmithyData(key);
        }
        private SmithyData(string key)
        {
            Key = key;
        }

        public override void SetData(string[] arr)
        {
            Key = arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.Key];

            name = $"{typeof(UnitData).Name}_{Key}";

            _serializedStatusData.SetData(
                arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.TypeStatusData], 
                arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.StartStatusValue], 
                arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseStatusValue]
                );

            _serializedAssetData.SetData(
                arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.TypeUpgradeAsset],
                arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.StartUpgradeValue]
                );

            _increaseUpgradeValue = int.Parse(arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgradeValue]);
            _increaseUpgradeRate = float.Parse(arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgrateRate]);
            _maxUpgradeValue = int.Parse(arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.MaxUpgardeValue]);

        }

        public override void AddData(string[] arr)
        {
        }


        public override string[] GetData()
        {           

            string[] arr = new string[System.Enum.GetValues(typeof(SmithyDataGenerator.TYPE_SHEET_COLUMNS)).Length];

            _serializedStatusData.GetData(out string typeStatusData, out string startStatusValue, out string increaseStatusValue);

            arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.TypeStatusData] = typeStatusData;
            arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.StartStatusValue] = startStatusValue;
            arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseStatusValue] = increaseStatusValue;

            _serializedAssetData.GetData(out string typeUpgradeAsset, out string startUpgradeValue);

            arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.TypeUpgradeAsset] = typeUpgradeAsset;
            arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.StartUpgradeValue] = startUpgradeValue;

            arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgradeValue] = _increaseUpgradeValue.ToString();
            arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.IncreaseUpgrateRate] = _increaseUpgradeRate.ToString();
            arr[(int)SmithyDataGenerator.TYPE_SHEET_COLUMNS.MaxUpgardeValue] = _maxUpgradeValue.ToString();

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