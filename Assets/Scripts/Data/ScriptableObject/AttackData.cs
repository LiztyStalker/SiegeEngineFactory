namespace SEF.Data
{
    using UnityEngine;
    using UtilityManager;

    [System.Serializable]
    public class AttackData
    {
        [SerializeField]
        private DamageData _attackValue;
        public DamageData AttackValue => _attackValue;


        [SerializeField]
        private int _increaseAttackValue;
        public int IncreaseAttackValue => _increaseAttackValue;


        [SerializeField]
        private float _increaseAttackRate;
        public float IncreaseAttackRate => _increaseAttackRate;


        [SerializeField]
        private int _attackCount;
        public int AttackCount => _attackCount;


        [SerializeField]
        private float _attackDelay;
        public float AttackDelay => _attackDelay;






        [SerializeField]
        private BulletData _bulletData;
        public BulletData BulletData => _bulletData;


        [SerializeField]
        private string _bulletDataKey;
        public string BulletDataKey => _bulletDataKey;


        [SerializeField]
        private float _bulletScale;
        public float BulletScale => _bulletScale;


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static AttackData Create_Test()
        {
            return new AttackData();
        }

        private AttackData()
        {
            _attackValue = DamageData.Create_Test();
            _increaseAttackValue = 1;
            _increaseAttackRate = 0.125f;

            _attackCount = 1;
            _attackDelay = 1f;

            _bulletDataKey = "BulletData_Arrow";
            _bulletScale = 0.3f;
        }
#endif

    }
}