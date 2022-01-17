namespace SEF.Data
{
    using UnityEngine;
    using UtilityManager;

    [System.Serializable]
    public class AttackData
    {
        [SerializeField]
        private DamageData _damageValue = NumberDataUtility.Create<DamageData>();
        public DamageData DamageValue { get => _damageValue; set => _damageValue = value; }


        [SerializeField]
        private int _increaseDamageValue;
        public int IncreaseDamageValue { get => _increaseDamageValue; set => _increaseDamageValue = value; }


        [SerializeField]
        private float _increaseDamageRate;
        public float IncreaseDamageRate { get => _increaseDamageRate; set => _increaseDamageRate = value; }


        [SerializeField]
        private int _attackCount;
        public int AttackCount { get => _attackCount; set => _attackCount = value; }


        [SerializeField]
        private float _attackDelay;
        public float AttackDelay { get => _attackDelay; set => _attackDelay = value; }






        [SerializeField]
        private BulletData _bulletData;
        public BulletData BulletData { get => _bulletData; set => _bulletData = value; }


        [SerializeField]
        private string _bulletDataKey;
        public string BulletDataKey { get => _bulletDataKey; set => _bulletDataKey = value; }


        [SerializeField]
        private float _bulletScale;
        public float BulletScale { get => _bulletScale; set => _bulletScale = value; }


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static AttackData Create_Test()
        {
            return new AttackData();
        }

        private AttackData()
        {
            _damageValue = DamageData.Create_Test();
            _increaseDamageValue = 1;
            _increaseDamageRate = 0.125f;

            _attackCount = 1;
            _attackDelay = 1f;

            _bulletDataKey = "BulletData_Arrow";
            _bulletScale = 0.3f;
        }
#endif

    }
}