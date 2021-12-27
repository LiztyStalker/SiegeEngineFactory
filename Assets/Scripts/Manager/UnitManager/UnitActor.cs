namespace SEF.Unit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using PoolSystem;
    using Entity;
    using Spine.Unity;
    using Spine;
    using Storage;
    using UtilityManager;
    public class UnitActor : PlayActor, ITarget, IPoolElement
    {
        private readonly static Vector2 UNIT_APPEAR_POSITION = new Vector2(-3.5f, 2f);
        private readonly static Vector2 UNIT_ACTION_POSITION = new Vector2(-1.5f, 2f);

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public readonly static Vector2 UNIT_APPEAR_POSITION_TEST = UNIT_APPEAR_POSITION;
        public readonly static Vector2 UNIT_ACTION_POSITION_TEST = UNIT_ACTION_POSITION;
#endif
        public bool IsArriveAction() => (Vector2.Distance(transform.position, UNIT_ACTION_POSITION) < ACTOR_ARRIVE_DISTANCE);

        private UnitEntity _unitEntity;

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

        private Spine.AnimationState _skeletonAnimationState;
        
        private Spine.AnimationState SkeletonAnimationState
        {
            get
            {
                if(_skeletonAnimationState == null)
                {
                    _skeletonAnimationState = SkeletonAnimation.AnimationState;
                    _skeletonAnimationState.Start += delegate { };
                    _skeletonAnimationState.Event += OnSpineEvent;
                    _skeletonAnimationState.Complete += OnEndEvent;
                    _skeletonAnimationState.Dispose += delegate { };
                    //_skeletonAnimationState.End += OnEndEvent;
                }
                return _skeletonAnimationState;
            }
        }



        public static UnitActor Create()
        {
            var obj = new GameObject(); 
            obj.name = "Actor@Unit";
            obj.AddComponent<SkeletonAnimation>();
            var unitActor = obj.AddComponent<UnitActor>();
            unitActor.SetPosition(ACTOR_CREATE_POSITION);
            unitActor.InActivate();
            return unitActor;
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

        private static Sprite _instanceSprite = null;

        public static UnitActor Create_Test()
        {
            var obj = new GameObject();
            obj.name = "Actor@Unit";
            var unitActor = obj.AddComponent<UnitActor>();
            unitActor.SetPosition(ACTOR_CREATE_POSITION);
            var sprite = obj.AddComponent<SpriteRenderer>();

            if (_instanceSprite == null)
            {
                Texture2D texture = new Texture2D(100, 100);

                for (int y = 0; y < texture.height; y++)
                {
                    for (int x = 0; x < texture.width; x++)
                    {
                        texture.SetPixel(x, y, Color.white);
                    }
                }
                _instanceSprite = Sprite.Create(texture, new Rect(0, 0, 100, 100), Vector2.one * 0.5f);
            }

            sprite.sprite = _instanceSprite;
            unitActor.InActivate();
            return unitActor;
        }
#endif


        public override void Activate()
        {
            base.Activate();
            SetPosition(UNIT_APPEAR_POSITION);
            _nowAttackTime = 0f;
            SetAnimation("Idle", true);
        }

        public void SetData(UnitEntity unitEntity)
        {
            _unitEntity = unitEntity;

            if (SkeletonAnimation != null)
            {
                //유닛 생성
                SkeletonAnimation.skeletonDataAsset = DataStorage.Instance.GetDataOrNull<SkeletonDataAsset>(unitEntity.UnitData.SpineModelKey, null, null);
            }
        }

        private void SetAnimation(string name, bool isLoop = false)
        {
            var animation = SkeletonAnimation.AnimationState.Data.SkeletonData.FindAnimation(name);
            if (animation != null)
                SkeletonAnimationState.SetAnimation(0, animation, isLoop);
            else
                Debug.Log($"{name} 애니메이션을 찾을 수 없습니다");
        }

        public override void RunProcess(float deltaTime)
        {

            switch (TypeUnitState)
            {
                case TYPE_UNIT_STATE.Idle:
                    SetTypeUnitState(TYPE_UNIT_STATE.Appear);
                    SetAnimation("Forward", true);
                    break;
                case TYPE_UNIT_STATE.Appear:
                    //Appear
                    AppearRunProcess(deltaTime);
                    break;
                case TYPE_UNIT_STATE.Action:
                    //Action
                    ActionRunProcess(deltaTime);
                    break;
                case TYPE_UNIT_STATE.Destory:
                    //Destroy
                    break;
            }
        }



        protected override void AppearRunProcess(float deltaTime)
        {

            if (!IsArriveAction())
            {
                //목표까지 이동
                SetPosition(Vector2.MoveTowards(transform.position, UNIT_ACTION_POSITION, deltaTime));
            }
            else
            {
                //목표에 도달했으면 Action으로 변환
                SetTypeUnitState(TYPE_UNIT_STATE.Action);
                SetAnimation("Idle", true);
            }

        }


        private float _nowAttackTime = 0f;
        protected override void ActionRunProcess(float deltaTime)
        {
            if(Target != null)
            {
                _nowAttackTime += deltaTime;
                if (_nowAttackTime > 1f)
                {
                    SetAnimation("Attack", false);
                    _nowAttackTime = 0f;
                }
            }
        }

        protected override void DestoryActor()
        {
            base.DestoryActor();
            SetAnimation("Dead");
        }

        #region ##### Spine Event #####
        private void OnSpineEvent(TrackEntry trackEntry, Spine.Event e)
        {
            if (Target != null)
            {
                //원거리
                if (!string.IsNullOrEmpty(_unitEntity.UnitData.AttackBulletKey))
                {
                    var bullet = DataStorage.Instance.GetDataOrNull<BulletData>(_unitEntity.UnitData.AttackBulletKey);
                    BulletManager.Current.Activate(bullet, transform.position, Target.NowPosition, delegate { DecreaseHealth(_unitEntity.UnitData.AttackValue); });
                }
                //근거리
                else
                {
                    Target.DecreaseHealth(_unitEntity.UnitData.AttackValue);
                }
            }
        }

        private void OnEndEvent(TrackEntry trackEntry)
        {
            if (trackEntry.Animation.Name == "Dead")
                OnDestroyedEvent();
        }
        #endregion


    }
}