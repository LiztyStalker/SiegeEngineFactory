namespace SEF.Data 
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "BlacksmithData", menuName = "ScriptableObjects/BlacksmithData")]

    public class BlacksmithData : ScriptableObject
    {
        public string Key => name;

        //Name
        //SerializedStatusData

        [SerializeField]
        private GoldAssetData _startUpgradeValue;
        public GoldAssetData StartUpgradeValue => _startUpgradeValue;


        [SerializeField]
        private int _increaseUpgradeValue;
        public int IncreaseUpgradeValue => _increaseUpgradeValue;


        [SerializeField]
        private float _increaseUpgradeRate;
        public float IncreaseUpgradeRate => _increaseUpgradeRate;


        //ConditionUnlock
        //ConditionValue

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static BlacksmithData Create_Test()
        {
            return new BlacksmithData();
        }

        private BlacksmithData()
        {
            _startUpgradeValue = GoldAssetData.Create_Test(100);
            _increaseUpgradeValue = 1;
            _increaseUpgradeRate = 0.125f;
        }
#endif 
    }
}