namespace SEF.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Process;

    [CreateAssetMenu(fileName = "VillageData", menuName = "ScriptableObjects/VillageData")]
    public class VillageData : ScriptableObject
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
#endif 
    }
}