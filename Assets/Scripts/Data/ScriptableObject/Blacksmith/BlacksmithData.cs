namespace SEF.Data
{
    using UnityEngine;
    using SEF.Status;


    [CreateAssetMenu(fileName = "BlacksmithData", menuName = "ScriptableObjects/BlacksmithData")]

    public class BlacksmithData : ScriptableObject
    {

        [SerializeField]
        private Sprite _icon;
        public Sprite Icon => _icon;


        [SerializeField]
        private string _key;
        public string Key => _key;


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


        public IAssetData GetUpgradeAssetData(UpgradeData data)
        {
            var assetData = (IAssetData)StartUpgradeAssetData.Clone();
            assetData.SetCompoundInterest(_increaseUpgradeValue, IncreaseUpgradeRate, data.Value);
            return assetData;
            
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static BlacksmithData Create_Test(string key = null)
        {
            return new BlacksmithData(key);
        }

        private BlacksmithData(string key)
        {
            _key = key;
        }
#endif 
    }
}