namespace SEF.Unit
{
    using UnityEngine;
    using Spine.Unity;
    using Data;
    using Spine;
    using UtilityManager;

    [RequireComponent(typeof(SkeletonAnimation))]
    public class AttackActor : MonoBehaviour
    {
        #region ##### AttackCase #####
        private struct AttackCase
        {
            internal AttackerData AttackData;
            internal NumberData NumberData;

            private DamageData _damageData;
            internal DamageData DamageData
            {
                get
                {
                    if(_damageData == null)
                    {
                        _damageData = CalculateDamageData();
                    }
                    return _damageData;
                }
            }

            internal void InitializeDamageData() => _damageData = null;

            private DamageData CalculateDamageData()
            {
                var assetData = new DamageData();
                assetData.SetAssetData(AttackData.AttackData, (LevelWaveData)NumberData);
                return assetData;
            }

            private float _nowTime;
            internal void RunProcess(float deltaTime)
            {
                _nowTime += deltaTime;
                if(_nowTime > AttackData.AttackData.AttackDelay)
                {
                    AttackEvent?.Invoke("Attack", false);
                    _nowTime -= AttackData.AttackData.AttackDelay;
                }
            }
            internal event System.Action<string, bool> AttackEvent;
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

        private AttackCase _attackCase;

        private bool _isDestroy = false;
        public void Initialize()
        {
            _attackCase.AttackEvent += OnAttackEvent;
            Inactivate();
        }

        public void CleanUp()
        {
            _attackCase.AttackEvent -= OnAttackEvent;
            _attackCase = default;
        }

        public void SetData(SkeletonDataAsset skeletonDataAsset, AttackerData attackerData, NumberData numberData)
        {
            _isDestroy = false;

            _attackCase.AttackData = attackerData;
            _attackCase.NumberData = numberData;
            _attackCase.InitializeDamageData();

            SkeletonAnimation.skeletonDataAsset = skeletonDataAsset;
            SetSkeletonAnimationState(SkeletonAnimationState);

            transform.localPosition = attackerData.Position;
            transform.localScale = Vector3.one * attackerData.Scale;
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
            OnAttackTargetEvent(_attackCase.AttackData.AttackData.BulletDataKey, _attackCase.AttackData.AttackData.BulletScale, _attackCase.DamageData);
//            OnAttackEvent(_attackCase.AttackData.AttackData);
            AddAnimation("Idle", true);
        }


        private void OnAttackEvent(string name, bool isLoop)
        {
            if(SkeletonAnimationState != null)
            {
                PlayAnimation(name, isLoop);
            }
            else
            {
                OnAttackTargetEvent(_attackCase.AttackData.AttackData.BulletDataKey, _attackCase.AttackData.AttackData.BulletScale, _attackCase.DamageData);
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
                _attackCase.RunProcess(deltaTime);
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

        //private System.Action<AttackData> _attackEvent;
        //public void SetOnAttackListener(System.Action<AttackData> act) => _attackEvent = act;
        //private void OnAttackEvent(AttackData attackData) => _attackEvent?.Invoke(attackData);

        private System.Action _destroyedEvent;
        public void SetOnDestroyedListener(System.Action act) => _destroyedEvent = act;
        private void OnDestroyedEvent() => _destroyedEvent?.Invoke();


        private System.Action<Vector2, string, float, DamageData> _attackTargetEvent;
        public void SetOnAttackTargetListener(System.Action<Vector2, string, float, DamageData> act) => _attackTargetEvent = act;

        private void OnAttackTargetEvent(string bulletDataKey, float scale, DamageData damageData)
        {
            _attackTargetEvent?.Invoke(transform.position, bulletDataKey, scale, damageData);
        }

        //private System.Func<ITarget> _targetEvent;
        //public void SetOnTargetListener(System.Func<ITarget> act) => _targetEvent = act;

        #endregion

        public static AttackActor Create(Transform parent)
        {
            var obj = new GameObject();
            obj.name = "AttackActor";
            obj.transform.SetParent(parent);
            return obj.AddComponent<AttackActor>();
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static AttackActor Create_Test()
        {
            var obj = new GameObject();
            obj.name = "AttackActor_Test";
            return obj.AddComponent<AttackActor>();
        }
#endif
    }
}