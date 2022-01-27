namespace SEF.Unit { 
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using PoolSystem;
    using Spine.Unity;
    using Spine;
    using UtilityManager;
    using Entity;
    using Storage;
    using Data;

    public class EnemyActor : PlayActor, ITarget, IPoolElement
    {
        private readonly static Vector2 ENEMY_IDLE_POSITION = new Vector2(3.5f, 2f);
        private readonly static Vector2 ENEMY_READY_POSITION = new Vector2(2.5f, 2f);
        private readonly static Vector2 ENEMY_ACTION_POSITION = new Vector2(1.5f, 2f);

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public readonly static Vector2 ENEMY_IDLE_POSITION_TEST = ENEMY_IDLE_POSITION;
        public readonly static Vector2 ENEMY_READY_POSITION_TEST = ENEMY_READY_POSITION;
        public readonly static Vector2 ENEMY_ACTION_POSITION_TEST = ENEMY_ACTION_POSITION;
#endif

        private EnemyEntity _enemyEntity;
        public string Key => _enemyEntity.EnemyData.Key;

        private SkeletonAnimation _skeletonAnimation;

        private SkeletonAnimation SkeletonAnimation
        {
            get
            {
                if (_skeletonAnimation == null)
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
                if (_skeletonAnimationState == null)
                {
                    if (SkeletonAnimation != null)
                    {
                        _skeletonAnimationState = SkeletonAnimation.AnimationState;
                        _skeletonAnimationState.Start += delegate { };
                        _skeletonAnimationState.Event += OnSpineEvent;
                        _skeletonAnimationState.Complete += OnCompleteEvent;
                        _skeletonAnimationState.Dispose += delegate { };
                        //_skeletonAnimationState.End += OnEndEvent;
                    }
                }
                return _skeletonAnimationState;
            }
        }

        public override float NowHealthRate() => (float)NowHealthData.Value / (float)_enemyEntity.HealthData.Value;
        public string NowHealthValue() => NowHealthData.GetValue();
        public bool IsArriveReady() => (Vector2.Distance(transform.position, ENEMY_READY_POSITION) < ACTOR_ARRIVE_DISTANCE);
        public bool IsArriveAction() => (Vector2.Distance(transform.position, ENEMY_ACTION_POSITION) < ACTOR_ARRIVE_DISTANCE);
        public LevelWaveData GetLevelWaveData() => _enemyEntity.GetLevelWaveData();
        public IAssetData GetRewardAssetData() => _enemyEntity.GetRewardAssetData();
        public static EnemyActor Create()
        {
            var obj = new GameObject();
            obj.name = "Actor@Enemy";
            obj.AddComponent<SkeletonAnimation>();
            var enemyActor = obj.AddComponent<EnemyActor>();
            enemyActor.SetPosition(ACTOR_CREATE_POSITION);
            enemyActor.InActivate();
            return enemyActor;
        }


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        private static Sprite _instanceSprite = null;

        public static EnemyActor Create_Test()
        {
            var obj = new GameObject();
            obj.name = "Actor@Enemy";

            var enemyActor = obj.AddComponent<EnemyActor>();
            enemyActor.SetPosition(ACTOR_CREATE_POSITION);
            enemyActor.InActivate();

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
            return enemyActor;
        }

#endif

        public override void Activate()
        {
            base.Activate();
            SetPosition(ENEMY_IDLE_POSITION);
            SetTypeUnitState(TYPE_UNIT_STATE.Idle);
            PlayAnimation("Idle", true);
            isDead = false;
        }

        public override void InActivate()
        {
            base.InActivate();
            SetPosition(ENEMY_IDLE_POSITION);
            SetTypeUnitState(TYPE_UNIT_STATE.Idle);
        }

        public override void RunProcess(float deltaTime)
        {
            switch (TypeUnitState)
            {
                case TYPE_UNIT_STATE.Idle:
                    //Idle                    
                    break;
                case TYPE_UNIT_STATE.Ready:
                    ReadyRunProcess(deltaTime);
                    break;
                case TYPE_UNIT_STATE.Appear:
                    //Appear
                    AppearRunProcess(deltaTime);
                    break;
                case TYPE_UNIT_STATE.Action:
                    //Action
                    ActionRunProcess(deltaTime);
                    break;
                case TYPE_UNIT_STATE.Destroy:
                    //Destroy
                    break;
            }
        }


        private bool isDead = false;

        protected override void DestoryActor()
        {
            if (!isDead)
            {
                base.DestoryActor();
                PlayAnimation("Dead");
                isDead = true;
            }
        }

        public void SetData(EnemyEntity enemyEntity)
        {
            _enemyEntity = enemyEntity;

            SetHealthData(_enemyEntity.HealthData);

            SetAttackerData(enemyEntity.EnemyData.AttackerDataArray, GetLevelWaveData());

            if (SkeletonAnimation != null)
            {
                //유닛 생성
                if (enemyEntity.EnemyData.SkeletonDataAsset != null)
                    SkeletonAnimation.skeletonDataAsset = enemyEntity.EnemyData.SkeletonDataAsset;
                else
                    SkeletonAnimation.skeletonDataAsset = DataStorage.Instance.GetDataOrNull<SkeletonDataAsset>(enemyEntity.EnemyData.SpineModelKey, null, null);
            }
        }

        private void ReadyRunProcess(float deltaTime)
        {
            if (!IsArriveReady())
            {
                //목표까지 이동
                SetPosition(Vector2.MoveTowards(transform.position, ENEMY_READY_POSITION, deltaTime));
            }
        }

        protected override void AppearRunProcess(float deltaTime)
        {
            if (!IsArriveAction())
            {
                //목표까지 이동
                SetPosition(Vector2.MoveTowards(transform.position, ENEMY_ACTION_POSITION, deltaTime));
            }
            else
            {
                //목표에 도달했으면 Action으로 변환
                SetTypeUnitState(TYPE_UNIT_STATE.Action);
            }
        }


        private float _nowActionTime = 0f;

        protected override void ActionRunProcess(float deltaTime)
        {
            if (HasTarget())
            {
                //적 공격자 행동
                base.ActionRunProcess(deltaTime);

                //자신 공격
                //자신이 공격 가능하면
                _nowActionTime += deltaTime;
                if (_nowActionTime > 1f)
                {
                    if (IsHasAnimation("Attack"))
                    {
                        SetAnimation("Attack");
                    }
                    else
                    {
                        OnAttackTargetEvent(transform.position, _enemyEntity.EnemyData.AttackBulletKey, _enemyEntity.EnemyData.BulletScale, _enemyEntity.AttackData);
                    }
                    _nowActionTime = 0f;
                }
            }
        }


        private void PlayAnimation(string name, bool isLoop = false)
        {
            if (IsHasAnimation(name))
                SetAnimation(name, isLoop);
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

        
        #region ##### Spine Event #####
        private void OnSpineEvent(TrackEntry trackEntry, Spine.Event e)
        {
            OnAttackTargetEvent(transform.position, _enemyEntity.EnemyData.AttackBulletKey, _enemyEntity.EnemyData.BulletScale, _enemyEntity.AttackData);
        }

        private void OnCompleteEvent(TrackEntry trackEntry)
        {
            if (trackEntry.Animation.Name == "Dead")
                OnDestroyedEvent();
        }
        #endregion

    }
}