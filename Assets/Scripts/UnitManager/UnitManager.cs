namespace SEF.Unit
{
    using System.Collections.Generic;
    using PoolSystem;
    using UnityEngine;

    public class UnitManager
    {

        private const int MAX_WAIT_ENEMY_COUNT = 2;

        private GameObject _gameObject;

        private PoolSystem<UnitActor> _poolUnitActor;
        private PoolSystem<EnemyActor> _poolEnemyActor;

        private Dictionary<int, UnitActor> _unitDic;
        private EnemyActor _nowEnemy;
        private Queue<EnemyActor> _waitEnemyQueue;

        //LevelWaveData - max LevelWaveData waitEnemyQueue

        public int UnitCount => _unitDic.Count;
        public int WaitEnemyCount => _waitEnemyQueue.Count;

        public EnemyActor NowEnemy => _nowEnemy;

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

            _poolEnemyActor = PoolSystem<EnemyActor>.Create();
            _poolEnemyActor.Initialize(EnemyActor.Create);

            _unitDic = new Dictionary<int, UnitActor>();
            _nowEnemy = null;
            _waitEnemyQueue = new Queue<EnemyActor>();

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

            _poolEnemyActor = PoolSystem<EnemyActor>.Create();
            _poolEnemyActor.Initialize(EnemyActor.Create);

            _unitDic = new Dictionary<int, UnitActor>();
            _nowEnemy = null;
            _waitEnemyQueue = new Queue<EnemyActor>();

        }

        //UnitActor 생산 - new or load

        private void InitializeUnitActor(/*AccountData*/)
        {

        }

        //EnemyActor 생산 - new or load
        private void InitializeEnemyActor(/*AccountData*/)
        {
            while (true)
            {
                CreateEnemyActor();//SetPosition
                ChangeNowEnemy();
                if (WaitEnemyCount == MAX_WAIT_ENEMY_COUNT)
                {
                    break;
                }
                //LevelWave++;
            }
        }

        public void CleanUp()
        {
            _poolUnitActor.CleanUp();
            _poolEnemyActor.CleanUp();

            _unitDic.Clear();
            _nowEnemy = null;
            _waitEnemyQueue.Clear();

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

        public EnemyActor CreateEnemyActor(/*EnemyData*/)
        {
            var enemyActor = _poolEnemyActor.GiveElement();

            enemyActor.SetData(); // EnemyData, LevelWaveData
            enemyActor.SetParent(_gameObject.transform);

            _waitEnemyQueue.Enqueue(enemyActor);

            enemyActor.InActivate();

            return enemyActor;
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

            _poolEnemyActor.RetrieveElement(enemyActor);
        }


        public void RunProcess(float deltaTime)
        {
            foreach(var unit in _unitDic.Values)
            {
                unit.RunProcess(deltaTime);
            }

            _nowEnemy.RunProcess(deltaTime);
        }

        private void CreateAndChangeEnemyActor()
        {
            CreateEnemyActor();
            ChangeNowEnemy();
            //LevelWave++;
        }

        public void ChangeNowEnemy()
        {
            if (_nowEnemy == null)
            {
                _nowEnemy = _waitEnemyQueue.Dequeue();

                _nowEnemy.AddOnHitListener(OnHitEvent);
                _nowEnemy.AddOnDestoryListener(OnDestroyEvent);

                _nowEnemy.Activate();
            }
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
            _nowEnemy = null;
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