namespace SEF.Unit
{
    using System.Linq;
    using System.Collections.Generic;
    using PoolSystem;
    using UnityEngine;
    using Entity;
    using Data;
    using Storage;
    using UtilityManager;

    public class UnitManager
    {

        #region ##### EnemyQueueData #####

        private struct EnemyQueueData
        {
            private PoolSystem<EnemyActor> _poolEnemyActor;

            private List<EnemyActor> _list;

            public EnemyActor NowEnemy => (_list.Count > 0) ? _list[0] : null;

            private LevelWaveData _levelWaveData;

            public LevelWaveData NowLevelWaveData => NowEnemy.GetLevelWaveData();

            public int Count { 
                get
                {
                    return _list.Count;
                } 
            }


            private bool IsFull()
            {
                return Count == 3;
            }

            private bool IsEmpty()
            {
                return Count == 0;
            }

            public void Initialize(/* LevelWaveData */Transform parent)
            {
                InitializePoolSystem();

                while (true)
                {
                    var enemyActor = CreateEnemyActor(parent);//SetPosition
                    ChangeEnemyActor(enemyActor);
                    if (IsFull())
                    {
                        break;
                    }
                    //LevelWave++;
                }


            }

            public void InitializePoolSystem()
            {
                _list = new List<EnemyActor>();
                _poolEnemyActor = PoolSystem<EnemyActor>.Create();
                _poolEnemyActor.Initialize(EnemyActor.Create);

                _levelWaveData = NumberDataUtility.Create<LevelWaveData>();
            }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
                       

            public void InitializePoolSystem_Test()
            {
                _list = new List<EnemyActor>();
                _poolEnemyActor = PoolSystem<EnemyActor>.Create();
                _poolEnemyActor.Initialize(EnemyActor.Create_Test);

                _levelWaveData = NumberDataUtility.Create<LevelWaveData>();
            }
#endif

            public void ChangeEnemyActor(EnemyActor enemyActor)
            {
                _list.Add(enemyActor);

                if (_list.Count > 0)
                    _list[0].SetTypeUnitState(TYPE_UNIT_STATE.Appear);
                if (_list.Count > 1)
                    _list[1].SetTypeUnitState(TYPE_UNIT_STATE.Ready);
            }

            public void RunProcess(float deltaTime)
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    _list[i].RunProcess(deltaTime);
                }
            }



            public EnemyActor CreateEnemyActor_Test(EnemyEntity enemyEntity, Transform parent)
            {
                var enemyActor = _poolEnemyActor.GiveElement();
                enemyActor.SetData(enemyEntity); // EnemyData, LevelWaveData
                enemyActor.SetParent(parent);
                enemyActor.Activate();

                _list.Add(enemyActor);

                return enemyActor;
            }

            public EnemyActor CreateEnemyActor(Transform parent)
            {
                EnemyEntity enemyEntity = new EnemyEntity();
                var levelWaveData = _levelWaveData.Clone() as LevelWaveData;


                //일반, 보스, 테마보스 찾기
                var arr = DataStorage.Instance.GetAllDataArrayOrZero<EnemyData>();
                var themeArray = arr.Where(data => (int)data.TypeLevelTheme == levelWaveData.GetTheme());
                EnemyData data;
                if (levelWaveData.IsThemeBoss())
                {
                    data = themeArray.Where(data => data.Group == TYPE_ENEMY_GROUP.ThemeBoss).Single();
                }
                else if (levelWaveData.IsBoss()) 
                {
                    var bossArray = themeArray.Where(data => data.Group == TYPE_ENEMY_GROUP.Boss).ToArray();
                    data = bossArray[UnityEngine.Random.Range(0, bossArray.Length)];
                }
                else
                {
                    var enemyArray = themeArray.Where(data => data.Group == TYPE_ENEMY_GROUP.Normal).ToArray();
                    data = enemyArray[UnityEngine.Random.Range(0, enemyArray.Length)];
                }

                enemyEntity.SetData(data, levelWaveData);

                var enemyActor = _poolEnemyActor.GiveElement();
                enemyActor.SetData(enemyEntity);
                enemyActor.SetParent(parent);
                enemyActor.Activate();

                _levelWaveData.IncreaseNumber();

                return enemyActor;
            }

            public void RetrieveEnemyActor(EnemyActor enemyActor)
            {
                _list.Remove(enemyActor);
                _poolEnemyActor.RetrieveElement(enemyActor);
            }

            public void CleanUp()
            {
                _poolEnemyActor?.CleanUp();
                _list.Clear();
            }
        }
        #endregion

        private GameObject _gameObject;

        private PoolSystem<UnitActor> _poolUnitActor;

        private Dictionary<int, UnitActor> _unitDic;
        private EnemyQueueData _enemyQueueData;

        public int UnitCount => _unitDic.Count;
        public int EnemyCount => _enemyQueueData.Count;

        public EnemyActor NowEnemy => _enemyQueueData.NowEnemy;
        

        public static UnitManager Create()
        {
            return new UnitManager();
        }

        public void Initialize(/*AccountData*/)
        {
            InitializeUnitManager();

            InitializeUnitActor(/*AccountData*/);

            InitializeEnemyActor(/*AccountData*/);

        }


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

        
        /// <summary>
        /// 모든 개체가 비어있습니다
        /// 더미를 사용하지 않습니다
        /// </summary>
        public void Initialize_Empty()
        {
            CreateGameObject();
            CreatePoolSystem();

            InitializeUnitActor();

            _enemyQueueData.InitializePoolSystem();
        }

        /// <summary>
        /// 모든 개체가 비어있습니다
        /// 더미 데이터를 사용할 수 있습니다
        /// </summary>
        public void Initialize_Empty_DummyTest()
        {
            CreateGameObject();
            CreatePoolSystem_Test();

            InitializeUnitActor();

            _enemyQueueData.InitializePoolSystem_Test();

        }
#endif
        private void InitializeUnitManager()
        {
            CreateGameObject();
            CreatePoolSystem();
        }


        private void CreateGameObject()
        {
            _gameObject = new GameObject();
            _gameObject.name = "Manager@UnitManager";
            _gameObject.transform.position = Vector3.zero;
            _gameObject.transform.localScale = Vector3.one;
        }

        private void CreatePoolSystem()
        {
            _poolUnitActor = PoolSystem<UnitActor>.Create();
            _poolUnitActor.Initialize(UnitActor.Create);
        }

#if UNITY_EDITOR
        private void CreatePoolSystem_Test()
        {
            _poolUnitActor = PoolSystem<UnitActor>.Create();
            _poolUnitActor.Initialize(UnitActor.Create_Test);
        }
#endif


        //UnitActor 생산 - new or load

        private void InitializeUnitActor(/*AccountData*/)
        {
            _unitDic = new Dictionary<int, UnitActor>();
        }

        //EnemyActor 생산 - new or load
        private void InitializeEnemyActor(/*AccountData*/)
        {
            _enemyQueueData.Initialize(/*AccountData*/_gameObject.transform);
            SetNowEnemyListener();
            OnNextEnemyEvent(_enemyQueueData.NowEnemy, _enemyQueueData.NowLevelWaveData);
        }

        public void CleanUp()
        {
            _poolUnitActor.CleanUp();

            _unitDic.Clear();
            _enemyQueueData.CleanUp();

            Object.DestroyImmediate(_gameObject);
        }


        public void ProductUnitActor(UnitEntity unitEntity)
        {
            CreateUnitActor(unitEntity);
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public UnitActor ProductUnitActor_Test(UnitEntity unitEntity) => CreateUnitActor(unitEntity);
#endif

        private UnitActor CreateUnitActor(UnitEntity unitEntity)
        {
            var unitActor = _poolUnitActor.GiveElement();

            unitActor.SetData(unitEntity);
            unitActor.SetParent(_gameObject.transform);
            unitActor.AddOnHitListener(OnHitEvent);
            unitActor.AddOnDestoryedListener(OnDestroyedEvent);
            unitActor.SetOnAttackTargetListener(OnAttackTargetEvent);
            unitActor.SetOnHasAttackTargetListener(HasAttackTarget);

            _unitDic.Add(unitActor.GetHashCode(), unitActor);

            unitActor.Activate();

            OnRefreshPopulationEvent(NowPopulation());          
            return unitActor;
        }


        public void RetrieveUnitActor(UnitActor unitActor)
        {
            unitActor.RemoveOnHitListener(OnHitEvent);
            unitActor.RemoveOnDestoryedListener(OnDestroyedEvent);
            unitActor.SetOnAttackTargetListener(null);
            unitActor.SetOnHasAttackTargetListener(null);
            unitActor.InActivate();

            _unitDic.Remove(unitActor.GetHashCode());

            _poolUnitActor.RetrieveElement(unitActor);
        }

        public void RetrieveEnemyActor(EnemyActor enemyActor)
        {
            enemyActor.RemoveOnHitListener(OnHitEvent);
            enemyActor.RemoveOnDestoryedListener(OnDestroyedEvent);
            enemyActor.SetOnAttackTargetListener(null);
            enemyActor.SetOnHasAttackTargetListener(null);
            enemyActor.InActivate();
            
            _enemyQueueData.RetrieveEnemyActor(enemyActor);
        }


        public void RunProcess(float deltaTime)
        {
            foreach(var unit in _unitDic.Values)
            {
                unit.RunProcess(deltaTime);
            }

            _enemyQueueData.RunProcess(deltaTime);
        }

        private void CreateAndChangeEnemyActor()
        {
            var enemyActor = CreateEnemyActor();
            ChangeNowEnemy(enemyActor);
        }

        public EnemyActor CreateEnemyActor()
        {
            return _enemyQueueData.CreateEnemyActor(_gameObject.transform);
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public EnemyActor CreateEnemyActor_Test(EnemyEntity enemyEntity)
        {
            return _enemyQueueData.CreateEnemyActor_Test(enemyEntity, _gameObject.transform);
        }
#endif

        public void ChangeNowEnemy(EnemyActor enemyActor)
        {
            _enemyQueueData.ChangeEnemyActor(enemyActor);

            SetNowEnemyListener();
        }

        private void SetNowEnemyListener()
        {
            _enemyQueueData.NowEnemy.AddOnHitListener(OnHitEvent);
            _enemyQueueData.NowEnemy.AddOnDestoryedListener(OnDestroyedEvent);
            _enemyQueueData.NowEnemy.SetOnAttackTargetListener(OnAttackTargetEvent);
            _enemyQueueData.NowEnemy.SetOnHasAttackTargetListener(HasAttackTarget);
        }

        private bool HasAttackTarget(PlayActor playActor)
        {
            ITarget target = null;
            switch (playActor)
            {
                case UnitActor unitActor:
                    target = _enemyQueueData.NowEnemy;
                    break;
                case EnemyActor enemyActor:
                    target = FindUnitActor();
                    break;
                default:
                    break;
            }
            return (target != null);
        }

        private void OnAttackTargetEvent(PlayActor playActor, Vector2 attackPos, string bulletDataKey, float scale, DamageData damageData)
        {
            BulletData bulletData = null;
            if (!string.IsNullOrEmpty(bulletDataKey))
                bulletData = DataStorage.Instance.GetDataOrNull<BulletData>(bulletDataKey, null, null);

            switch (playActor)
            {
                case UnitActor unitActor:
                    //StatusPackage GetStatusDataAssetValue 적용
                    AttackTarget(_enemyQueueData.NowEnemy, attackPos, bulletData, scale, damageData);
                    break;
                case EnemyActor enemyActor:
                    var uActor = FindUnitActor();
                    if (uActor != null)
                    {
                        AttackTarget(uActor, attackPos, bulletData, scale, damageData);
                    }
                    break;
                default:
                    break;
            }
        }

        private void AttackTarget(ITarget target, Vector2 attackPos, BulletData bulletData, float scale, DamageData damageData)
        {
            if(bulletData != null)
            {
                BulletManager.Current.Activate(bulletData, scale, attackPos, target.NowPosition, delegate { target.DecreaseHealth(damageData); });
            }
            else
            {
                target.DecreaseHealth(damageData);
            }
        }

        private UnitActor FindUnitActor()
        {
            var arr = _unitDic.Values.ToArray();
            if(arr.Length > 0)
                return arr[UnityEngine.Random.Range(0, arr.Length)];
            return null;
        }

        private int NowPopulation()
        {
            return _unitDic.Values.Sum(actor => actor.Population);
        }

        public void Refresh()
        {
            OnNextEnemyEvent(_enemyQueueData.NowEnemy, _enemyQueueData.NowLevelWaveData);
        }

        #region ##### Listener #####

        public System.Action<PlayActor, DamageData> _hitEvent;
        public void AddOnHitListener(System.Action<PlayActor, DamageData> act) => _hitEvent += act;
        public void RemoveOnHitListener(System.Action<PlayActor, DamageData> act) => _hitEvent -= act;
        private void OnHitEvent(PlayActor playActor, DamageData attackData) 
        {
            //switch (playActor)
            //{
            //    case UnitActor unitActor:
            //        break;
            //    case EnemyActor enemyActor:
            //        break;
            //    default:

            //        Debug.LogError("적용되지 않은 PlayActor 클래스 입니다");
            //        break;
            //}
            _hitEvent?.Invoke(playActor, attackData);
        }


        private System.Action<PlayActor> _destroyedEvent;
        public void AddOnDestoryedListener(System.Action<PlayActor> act) => _destroyedEvent += act;
        public void RemoveOnDestoryedListener(System.Action<PlayActor> act) => _destroyedEvent -= act;

        private void OnDestroyedEvent(PlayActor playActor)
        {
            _destroyedEvent?.Invoke(playActor);
            switch (playActor)
            {
                case UnitActor unitActor:
                    RetrieveUnitActor(unitActor);
                    OnRefreshPopulationEvent(NowPopulation());
                    //파괴 이벤트
                    break;
                case EnemyActor enemyActor:
                    RetrieveEnemyActor(enemyActor);
                    CreateAndChangeEnemyActor();
                    OnNextEnemyEvent(_enemyQueueData.NowEnemy, _enemyQueueData.NowLevelWaveData);
                    break;
                default:
                    Debug.LogError("적용되지 않은 PlayActor 클래스 입니다");
                    break;
            }
        }



        private System.Action<EnemyActor, LevelWaveData> _nextEvent;

        public void AddOnNextEnemyListener(System.Action<EnemyActor, LevelWaveData> act) => _nextEvent += act;
        public void RemoveOnNextEnemyListener(System.Action<EnemyActor, LevelWaveData> act) => _nextEvent += act;

        private void OnNextEnemyEvent(EnemyActor enemyActor, LevelWaveData levelWaveData)
        {
            _nextEvent?.Invoke(enemyActor, levelWaveData);
        }



        private System.Action<IAssetData> _refreshPopulationEvent;
        public void AddOnRefreshPopulationListener(System.Action<IAssetData> act) => _refreshPopulationEvent += act;
        public void RemoveOnRefreshPopulationListener(System.Action<IAssetData> act) => _refreshPopulationEvent -= act;
        public void OnRefreshPopulationEvent(int nowPopulation)
        {
            var data = new PopulationAssetData(nowPopulation);
            _refreshPopulationEvent?.Invoke(data);
        }

        #endregion


        #region ##### Data #####

        //public AccountData GetData()
        //{

        //}
        #endregion


    }
}