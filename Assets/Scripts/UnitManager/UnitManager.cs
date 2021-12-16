namespace SEF.Unit
{
    using System.Collections.Generic;
    using PoolSystem;
    using UnityEngine;

    public class UnitManager
    {

        #region ##### EnemyQueueData #####

        private struct EnemyQueueData
        {
            private PoolSystem<EnemyActor> _poolEnemyActor;

            private EnemyActor _nowEnemy;
            private EnemyActor _readyEnemy;
            private EnemyActor _idleEnemy;

            public EnemyActor NowEnemy => _nowEnemy;

            public int Count { 
                get
                {
                    var count = 0;
                    if (_readyEnemy != null) count++;
                    if (_idleEnemy != null) count++;
                    return count;
                } 
            }


            private bool IsFull()
            {
                return _nowEnemy != null && _readyEnemy != null && _idleEnemy != null;
            }

            public void Initialize(/* LevelWaveData */Transform parent)
            {
                _poolEnemyActor = PoolSystem<EnemyActor>.Create();
                _poolEnemyActor.Initialize(EnemyActor.Create);

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

            public void ChangeEnemyActor(EnemyActor enemyActor)
            {
                _nowEnemy = _readyEnemy;
                if(_nowEnemy != null) _nowEnemy.SetTypeUnitState(TYPE_UNIT_STATE.Appear);
                _readyEnemy = _idleEnemy;
                if(_readyEnemy != null) _readyEnemy.SetTypeUnitState(TYPE_UNIT_STATE.Ready);
                _idleEnemy = enemyActor;
            }

            public void RunProcess(float deltaTime)
            {
                if (IsFull())
                {
                    _nowEnemy.RunProcess(deltaTime);
                    _readyEnemy.RunProcess(deltaTime);
                    _idleEnemy.RunProcess(deltaTime);
                }
            }



            public EnemyActor CreateEnemyActor(/*EnemyData*/Transform parent)
            {
                var enemyActor = _poolEnemyActor.GiveElement();

                enemyActor.SetData(); // EnemyData, LevelWaveData
                enemyActor.SetParent(parent);

                enemyActor.Activate();

                return enemyActor;
            }

            public void RetrieveEnemyActor(EnemyActor enemyActor)
            {
                _poolEnemyActor.RetrieveElement(enemyActor);
            }

            public void CleanUp()
            {
                _poolEnemyActor.CleanUp();
                _nowEnemy = null;
                _readyEnemy = null;
                _idleEnemy = null;
            }
        }

        #endregion

        private GameObject _gameObject;

        private PoolSystem<UnitActor> _poolUnitActor;

        private Dictionary<int, UnitActor> _unitDic;
        private EnemyQueueData _enemyQueueData;

        //LevelWaveData - max LevelWaveData waitEnemyQueue

        public int UnitCount => _unitDic.Count;
        public int WaitEnemyCount => _enemyQueueData.Count;

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

        public void InitializeUnitManager_PositionTest()
        {          
            _gameObject = new GameObject();
            _gameObject.name = "Manager@UnitManager";
            _gameObject.transform.position = Vector3.zero;
            _gameObject.transform.localScale = Vector3.one;

            _poolUnitActor = PoolSystem<UnitActor>.Create();
            _poolUnitActor.Initialize(UnitActor.Create_Test);

            _unitDic = new Dictionary<int, UnitActor>();
            _enemyQueueData.Initialize(_gameObject.transform);

        }
#endif



        private void InitializeUnitManager()
        {
            _gameObject = new GameObject();
            _gameObject.name = "Manager@UnitManager";
            _gameObject.transform.position = Vector3.zero;
            _gameObject.transform.localScale = Vector3.one;

            _poolUnitActor = PoolSystem<UnitActor>.Create();
            _poolUnitActor.Initialize(UnitActor.Create);

            _unitDic = new Dictionary<int, UnitActor>();
        }

        //UnitActor 생산 - new or load

        private void InitializeUnitActor(/*AccountData*/)
        {

        }

        //EnemyActor 생산 - new or load
        private void InitializeEnemyActor(/*AccountData*/)
        {
            _enemyQueueData.Initialize(/*AccountData*/_gameObject.transform);
        }

        public void CleanUp()
        {
            _poolUnitActor.CleanUp();

            _unitDic.Clear();
            _enemyQueueData.CleanUp();

            Object.DestroyImmediate(_gameObject);
        }


        public UnitActor CreateUnitActor(/*UnityEntity*/)
        {
            var unitActor = _poolUnitActor.GiveElement();

            unitActor.SetData();
            unitActor.SetParent(_gameObject.transform);
            unitActor.AddOnHitListener(OnHitEvent);
            unitActor.AddOnDestoryListener(OnDestroyEvent);

            _unitDic.Add(unitActor.GetHashCode(), unitActor);

            unitActor.Activate();

            return unitActor;

        }


        public void RetrieveUnitActor(UnitActor unitActor)
        {
            unitActor.InActivate();

            unitActor.RemoveOnHitListener(OnHitEvent);
            unitActor.RemoveOnDestoryListener(OnDestroyEvent);

            _unitDic.Remove(unitActor.GetHashCode());

            _poolUnitActor.RetrieveElement(unitActor);
        }

        public void RetrieveEnemyActor(EnemyActor enemyActor)
        {
            enemyActor.InActivate();
            enemyActor.RemoveOnHitListener(OnHitEvent);
            enemyActor.RemoveOnDestoryListener(OnDestroyEvent);

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
            CreateEnemyActor();
            ChangeNowEnemy();
            //LevelWave++;
        }

        public EnemyActor CreateEnemyActor()
        {
            return _enemyQueueData.CreateEnemyActor(_gameObject.transform);
        }

        public void ChangeNowEnemy()
        {
            var enemyActor = _enemyQueueData.CreateEnemyActor(_gameObject.transform);
            _enemyQueueData.ChangeEnemyActor(enemyActor);

            _enemyQueueData.NowEnemy.AddOnHitListener(OnHitEvent);
            _enemyQueueData.NowEnemy.AddOnDestoryListener(OnDestroyEvent);
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
                    break;
                default:
                    Debug.LogError("적용되지 않은 PlayActor 클래스 입니다");
                    break;
            }
            _destroyEvent?.Invoke(playActor);
            CreateAndChangeEnemyActor();
        }
        #endregion


        #region ##### Data #####

        //public AccountData GetData()
        //{

        //}
        #endregion


    }
}