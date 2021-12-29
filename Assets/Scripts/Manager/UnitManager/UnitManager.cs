namespace SEF.Unit
{
    using System.Linq;
    using System.Collections.Generic;
    using PoolSystem;
    using UnityEngine;
    using Entity;
    using Data;
    using Storage;


    public class UnitManager
    {

        #region ##### EnemyQueueData #####

        private struct EnemyQueueData
        {
            private PoolSystem<EnemyActor> _poolEnemyActor;

            //private EnemyActor _nowEnemy;
            //private EnemyActor _readyEnemy;
            //private EnemyActor _idleEnemy;

            private List<EnemyActor> _list;

            public EnemyActor NowEnemy => _list[0];

            private LevelWaveData _nowLevelWaveData;

            public int Count { 
                get
                {
                    return _list.Count;
                    //var count = 0;
                    //if (_readyEnemy != null) count++;
                    //if (_idleEnemy != null) count++;
                    //return count;
                } 
            }


            private bool IsFull()
            {
                return Count == 3;
//                return _nowEnemy != null && _readyEnemy != null && _idleEnemy != null;
            }

            private bool IsEmpty()
            {
                return Count == 0;
//                return _nowEnemy == null && _readyEnemy == null && _idleEnemy == null;
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
            }

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
                var arr = DataStorage.Instance.GetAllDataArrayOrZero<EnemyData>();
                var data = arr[Random.Range(0, arr.Length)];

                Debug.Log(data);
                var enemyActor = _poolEnemyActor.GiveElement();

                EnemyEntity enemyEntity = new EnemyEntity();
                enemyEntity.SetData(data, _nowLevelWaveData);
                enemyActor.SetData(enemyEntity);
                enemyActor.SetParent(parent);
                enemyActor.Activate();

                //_list.Add(enemyActor);

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

        //LevelWaveData - max LevelWaveData waitEnemyQueue

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

        public void Initialize_Empty()
        {
            CreateGameObject();
            CreatePoolSystem();
            _poolUnitActor.Initialize(UnitActor.Create);

            _unitDic = new Dictionary<int, UnitActor>();
            _enemyQueueData.InitializePoolSystem();
        }

        public void InitializeUnitManager_Test()
        {
            InitializeUnitManager();
        }

        public void InitializeUnitActor_Test()
        {
            InitializeUnitActor();
        }

        public void InitializeEnemyActor_Test()
        {
            InitializeEnemyActor();
        }

        public void InitializeUnitManager_DummyTest()
        {
            CreateGameObject();
            CreatePoolSystem();
            _poolUnitActor.Initialize(UnitActor.Create_Test);

            _unitDic = new Dictionary<int, UnitActor>();

            _enemyQueueData.InitializePoolSystem();

        }
#endif



        private void InitializeUnitManager()
        {
            CreateGameObject();
            CreatePoolSystem();
            _poolUnitActor.Initialize(UnitActor.Create);

            _unitDic = new Dictionary<int, UnitActor>();

//            _enemyQueueData.Initialize(_gameObject.transform);
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
        }


        //UnitActor 생산 - new or load

        private void InitializeUnitActor(/*AccountData*/)
        {

        }

        //EnemyActor 생산 - new or load
        private void InitializeEnemyActor(/*AccountData*/)
        {
            _enemyQueueData.Initialize(/*AccountData*/_gameObject.transform);
            SetNowEnemyListener();
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
            unitActor.AddOnDestoryedListener(OnDestroyEvent);

            _unitDic.Add(unitActor.GetHashCode(), unitActor);

            unitActor.Activate();

            unitActor.SetTarget(_enemyQueueData.NowEnemy);

            return unitActor;

        }


        public void RetrieveUnitActor(UnitActor unitActor)
        {

            unitActor.RemoveOnHitListener(OnHitEvent);
            unitActor.RemoveOnDestoryedListener(OnDestroyEvent);
            unitActor.InActivate();

            _unitDic.Remove(unitActor.GetHashCode());

            _poolUnitActor.RetrieveElement(unitActor);
        }

        public void RetrieveEnemyActor(EnemyActor enemyActor)
        {
            enemyActor.RemoveOnHitListener(OnHitEvent);
            enemyActor.RemoveOnDestoryedListener(OnDestroyEvent);
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
            //LevelWave++;
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

            foreach (var value in _unitDic.Values)
            {
                value.SetTarget(_enemyQueueData.NowEnemy);
            }
        }

        private void SetNowEnemyListener()
        {

            _enemyQueueData.NowEnemy.AddOnHitListener(OnHitEvent);
            _enemyQueueData.NowEnemy.AddOnDestoryedListener(OnDestroyEvent);
            _enemyQueueData.NowEnemy.SetOnFindTargetListener(FindUnitActor);

        }

        private UnitActor FindUnitActor()
        {
            var arr = _unitDic.Values.ToArray();
            return arr[UnityEngine.Random.Range(0, arr.Length)];
        }


        #region ##### Listener #####

        public System.Action<PlayActor> _hitEvent;

        //IUnitActor, AttackData
        public void AddOnHitListener(System.Action<PlayActor> act) => _hitEvent += act;
        public void RemoveOnHitListener(System.Action<PlayActor> act) => _hitEvent -= act;

        private void OnHitEvent(PlayActor playActor) 
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
            _hitEvent?.Invoke(playActor);
        }


        private System.Action<PlayActor> _destroyEvent;

        //IUnitActor
        public void AddOnDestoryListener(System.Action<PlayActor> act) => _destroyEvent += act;
        public void RemoveOnDestoryListener(System.Action<PlayActor> act) => _destroyEvent -= act;

        private void OnDestroyEvent(PlayActor playActor)
        {
            switch (playActor)
            {
                case UnitActor unitActor:
                    RetrieveUnitActor(unitActor);
                    break;
                case EnemyActor enemyActor:
                    RetrieveEnemyActor(enemyActor);
                    CreateAndChangeEnemyActor();
                    break;
                default:
                    Debug.LogError("적용되지 않은 PlayActor 클래스 입니다");
                    break;
            }
            _destroyEvent?.Invoke(playActor);
        }


        #endregion


        #region ##### Data #####

        //public AccountData GetData()
        //{

        //}
        #endregion


    }
}