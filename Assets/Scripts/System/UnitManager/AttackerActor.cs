namespace SEF.Unit
{
    using UnityEngine;
    using Spine.Unity;
    using Data;
    using Spine;
    using PoolSystem;
    using Status;

    [RequireComponent(typeof(SkeletonAnimation))]
    public class AttackerActor : MonoBehaviour, IPoolElement
    {

        #region ##### PoolSystem #####

        private static GameObject _gameObject;

        private static PoolSystem<AttackerActor> _pool;
        private static void Initialize(System.Func<AttackerActor> createCallback)
        {
            _pool = new PoolSystem<AttackerActor>();
            _pool.Initialize(createCallback);

            _gameObject = new GameObject();
            _gameObject.name = "Storage@AttackerActor";
        }
        public static AttackerActor GetAttackerActor(System.Func<AttackerActor> createCallback)
        {
            if (_pool == null) Initialize(createCallback);
            return _pool.GiveElement();
        }
        public static void RetrieveActor(AttackerActor attackerActor)
        {
            Debug.Assert(_pool != null, "AttackerActor PoolSystem이 초기화 되지 않았습니다");
            attackerActor.SetParent(_gameObject.transform);
            _pool.RetrieveElement(attackerActor);
        }

        #endregion

        #region ##### AttackerCase #####
        private struct AttackerCase
        {
            internal AttackerData AttackerData;
            internal NumberData NumberData;

            //private StatusPackage _statusPackage;
            //internal void SetStatusPackage(StatusPackage statusPackage) => _statusPackage = statusPackage;

            private DamageData _damageData;
            internal DamageData DamageData
            {
                get
                {
                    //유닛만 사용
                    if(NumberData is UpgradeData)
                        return StatusPackage.Current.GetStatusDataToBigNumberData<AttackerDamageValueStatusData, DamageData>(_damageData);
                    //적군은 미사용
                    return _damageData;
                }
            }

            internal float AttackDalay
            {
                get
                {
                    if (NumberData is UpgradeData)
                    {
                        var data = StatusPackage.Current.GetStatusDataToBigNumberData<AttackerDamageDelayStatusData, UniversalBigNumberData>(new UniversalBigNumberData(AttackerData.AttackData.AttackDelay));
                        return (float)data.Value;
                    }
                    //유닛만 사용
                    //if (NumberData is UpgradeData)
                    //{
                    //    var data = _statusPackage.GetStatusDataToBigNumberData<AttackerDamageDelayStatusData, UniversalBigNumberData>(new UniversalBigNumberData(AttackerData.AttackData.AttackDelay));
                    //    return (float)data.GetDecimalValue();
                    //}
                    //적군은 미사용
                    return AttackerData.AttackData.AttackDelay;
                }
            }

            internal void CleanUp()
            {
                AttackerData = null;
                NumberData = null;
                //_statusPackage = null;
                _damageData = null;

            }

            internal void CalculateDamageData()
            {
                if(_damageData == null)
                    _damageData = new DamageData();

                _damageData.Clear();

                switch (NumberData)
                {
                    case UpgradeData upgradeData:
                        _damageData.SetAssetData(AttackerData.AttackData, upgradeData);
                        break;
                    case LevelWaveData levelWaveData:
                        _damageData.SetAssetData(AttackerData.AttackData, levelWaveData);
                        break;
                }
            }

            private float _nowTime;
            internal void RunProcess(float deltaTime, System.Action<string, bool> attackCallback)
            {
                _nowTime += deltaTime;
                if (_nowTime > AttackDalay)
                {
                    attackCallback?.Invoke("Attack", false);
                    _nowTime -= AttackerData.AttackData.AttackDelay;
                }
            }
        }

        #endregion




        [SerializeField]
        private SkeletonAnimation _skeletonAnimation;

        private SkeletonAnimation SkeletonAnimation
        {
            get
            {
                if(_skeletonAnimation == null)
                {
                    _skeletonAnimation = GetComponent<SkeletonAnimation>();
                }
                return _skeletonAnimation;
            }
        }

        private Spine.AnimationState SkeletonAnimationState => _skeletonAnimation.AnimationState;

        private AttackerCase _attackCase;

        private bool _isDestroy = false;

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

        public DamageData DamageData => _attackCase.DamageData;
        public float AttackDelay => _attackCase.AttackDalay;
#endif

        public void Initialize()
        {
            Inactivate();
        }

        public void CleanUp()
        {
            _attackCase.CleanUp();
        }

        public void SetData(SkeletonDataAsset skeletonDataAsset, AttackerData attackerData, NumberData numberData)
        {
            _isDestroy = false;

            _attackCase.AttackerData = attackerData;
            _attackCase.NumberData = numberData;
            _attackCase.CalculateDamageData();

            SkeletonAnimation.skeletonDataAsset = skeletonDataAsset;
            SetSkeletonAnimationState(SkeletonAnimationState);

            transform.localPosition = attackerData.Position;
            transform.localScale = Vector3.one * attackerData.Scale;
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public void SetData_Test(AttackerData attackerData, NumberData numberData)
        {
            _isDestroy = false;

            _attackCase.AttackerData = attackerData;
            _attackCase.NumberData = numberData;
            _attackCase.CalculateDamageData();
        }
#endif

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }

        private void SetSkeletonAnimationState(Spine.AnimationState animationState)
        {
            if (animationState != null)
            {
                animationState.Start += delegate { };
                animationState.Event += OnSpineEvent;
                animationState.Complete += OnCompleteEvent;
                animationState.Dispose += delegate { };
                //_skeletonAnimationState.End += OnEndEvent;
            }
        }

        private void OnCompleteEvent(TrackEntry trackEntry)
        {
            if (trackEntry.Animation.Name == "Dead")
                OnDestroyedEvent();
        }

        private void OnSpineEvent(TrackEntry trackEntry, Spine.Event e)
        {
            OnAttackTargetEvent(_attackCase.AttackerData.AttackData.BulletDataKey, _attackCase.AttackerData.AttackData.BulletScale, _attackCase.DamageData);
            AddAnimation("Idle", true);
        }


        internal void OnAttackEvent(string name, bool isLoop)
        {
            if(SkeletonAnimationState != null)
            {
                PlayAnimation(name, isLoop);
            }
            else
            {
                OnAttackTargetEvent(_attackCase.AttackerData.AttackData.BulletDataKey, _attackCase.AttackerData.AttackData.BulletScale, _attackCase.DamageData);
            }
        }

        private void PlayAnimation(string name, bool isLoop = false)
        {
            if (IsHasAnimation(name))
                SetAnimation(name, isLoop);
        }
        
        private void AddAnimation(string name, bool isLoop = false)
        {
            if (IsHasAnimation(name))
                AddAnimation(name, isLoop, 0f);
        }

        private bool IsHasAnimation(string name)
        {
            if (SkeletonAnimation != null && SkeletonAnimationState != null)
            {
                var animation = SkeletonAnimation.AnimationState.Data.SkeletonData.FindAnimation(name);
                return animation != null;
            }
            return false;
        }

        private void SetAnimation(string name, bool isLoop = false)
        {
            SkeletonAnimationState.SetAnimation(0, name, isLoop);
        }

        private void AddAnimation(string name, bool isLoop, float delay)
        {
            SkeletonAnimationState.AddAnimation(0, name, isLoop, delay);
        }
       

        public void Activate()
        {
            PlayAnimation("Idle", true);
            gameObject.SetActive(true);
        }

        public void Inactivate()
        {
            gameObject.SetActive(false);
        }

        public void RunProcess(float deltaTime)
        {
            if (!_isDestroy)
            {
                _attackCase.RunProcess(deltaTime, OnAttackEvent);
            }
        }

        public void Destory()
        {
            _isDestroy = true;
            if (SkeletonAnimationState != null)
                PlayAnimation("Dead", false);
            else
                OnDestroyedEvent();
        }

        #region ##### Listener #####

        private System.Action _destroyedEvent;
        public void SetOnDestroyedListener(System.Action act) => _destroyedEvent = act;
        private void OnDestroyedEvent() => _destroyedEvent?.Invoke();


        private System.Action<Vector2, string, float, DamageData> _attackTargetEvent;
        public void SetOnAttackTargetListener(System.Action<Vector2, string, float, DamageData> act) => _attackTargetEvent = act;

        private void OnAttackTargetEvent(string bulletDataKey, float scale, DamageData damageData)
        {
            _attackTargetEvent?.Invoke(transform.position, bulletDataKey, scale, damageData);
        }

        #endregion

        public static AttackerActor Create(Transform parent)
        {
            var obj = new GameObject();
            obj.name = "AttackActor";
            obj.transform.SetParent(parent);
            return obj.AddComponent<AttackerActor>();
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static AttackerActor Create_Test()
        {
            var obj = new GameObject();
            obj.name = "AttackActor_Test";
            return obj.AddComponent<AttackerActor>();
        }
#endif


    }
}