namespace SEF.Unit
{
    using UnityEngine;
    using Spine.Unity;
    using Data;
    using Spine;

    [RequireComponent(typeof(SkeletonAnimation))]
    public class AttackActor : MonoBehaviour
    {
        #region ##### AttackCase #####
        private struct AttackCase
        {
            internal AttackerData _attackerData;
            internal NumberData _numberData;

            private float _nowTime;
            internal void RunProcess(float deltaTime)
            {
                Debug.Log("RunProcess " + _nowTime);
                _nowTime += deltaTime;
                if(_nowTime > _attackerData.AttackData.AttackDelay)
                {
                    //�����ϱ�
                    //AttackEvent?.Invoke(_attackerData.AttackData);
                    //PlayAnimation("Attack");
                    AttackEvent?.Invoke("Attack", false);
                    _nowTime -= _attackerData.AttackData.AttackDelay;
                }
            }

            //internal event System.Action<AttackData> AttackEvent;
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

        private bool _isDead = false;

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
            _isDead = false;

            _attackCase._attackerData = attackerData;
            _attackCase._numberData = numberData;

            SkeletonAnimation.skeletonDataAsset = skeletonDataAsset;
            SetSkeletonAnimationState(SkeletonAnimationState);
        }

        private void SetSkeletonAnimationState(Spine.AnimationState animationState)
        {
            if (animationState != null)
            {
                animationState.Start += delegate { };
                animationState.Event += OnSpineEvent;
                animationState.Complete += OnEndEvent;
                animationState.Dispose += delegate { };
                //_skeletonAnimationState.End += OnEndEvent;
            }
        }

        private void OnEndEvent(TrackEntry trackEntry)
        {
            if (trackEntry.Animation.Name == "Dead")
                OnDestroyedEvent();
        }

        private void OnSpineEvent(TrackEntry trackEntry, Spine.Event e)
        {
            OnAttackEvent(_attackCase._attackerData.AttackData);
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
                OnAttackEvent(_attackCase._attackerData.AttackData);
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
            _attackCase.RunProcess(deltaTime);
        }

        public void Destory()
        {
            _isDead = true;
            if (SkeletonAnimationState != null)
                PlayAnimation("Dead", false);
            else
                OnDestroyedEvent();
        }

        #region ##### Listener #####

        private System.Action<AttackData> _attackEvent;
        public void SetOnAttackListener(System.Action<AttackData> act) => _attackEvent = act;
        private void OnAttackEvent(AttackData attackData) => _attackEvent?.Invoke(attackData);

        private System.Action _destroyedEvent;
        public void SetOnDestroyedListener(System.Action act) => _destroyedEvent = act;
        private void OnDestroyedEvent() => _destroyedEvent?.Invoke();

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