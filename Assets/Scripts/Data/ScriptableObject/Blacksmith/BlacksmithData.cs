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
        public int IncreaseHealthValue => _increaseUpgradeValue;

        [SerializeField]
        private float _increaseUpgradeRate;
        public float IncreaseHealthRate => _increaseUpgradeRate;


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static BlacksmithData Create_Test()
        {
            return new BlacksmithData();
        }

        private BlacksmithData()
        {
        }
#endif 
    }
}