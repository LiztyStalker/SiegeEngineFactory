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
    using Data;

    public class UnitActor : PlayActor, ITarget, IPoolElement
    {
        private readonly static Vector2 UNIT_APPEAR_POSITION = new Vector2(-3.5f, 2f);
        private readonly static Vector2 UNIT_ACTION_POSITION = new Vector2(-1.5f, 2f);

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public readonly static Vector2 UNIT_APPEAR_POSITION_TEST = UNIT_APPEAR_POSITION;
        public readonly static Vector2 UNIT_ACTION_POSITION_TEST = UNIT_ACTION_POSITION;
#endif

        public override float NowHealthRate() => (float)NowHealthData.Value / (float)_unitEntity.HealthData.Value;
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
                    if (SkeletonAnimation.AnimationState != null)
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

        public int Population => _unitEntity.Population;


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
            PlayAnimation("Idle", true);
        }

        public override void InActivate()
        {
            base.InActivate();
            SetPosition(UNIT_APPEAR_POSITION);
            SetTypeUnitState(TYPE_UNIT_STATE.Idle);
        }


        public void SetData(UnitEntity unitEntity)
        {
            _unitEntity = unitEntity;

            SetHealthData(unitEntity.HealthData);

            SetAttackerData(unitEntity.UnitData.AttackerDataArray, unitEntity.UpgradeData);

            if (SkeletonAnimation != null)
            {
                //���� ����
                if (unitEntity.UnitData.SkeletonDataAsset == null)
                    SkeletonAnimation.skeletonDataAsset = DataStorage.Instance.GetDataOrNull<SkeletonDataAsset>(unitEntity.UnitData.SpineModelKey, null, null);
                else
                    SkeletonAnimation.skeletonDataAsset = unitEntity.UnitData.SkeletonDataAsset;
            }

            transform.localScale = Vector3.one * unitEntity.UnitData.Scale;
        }


        //UnitActor, EnemyActor, AttackActor���� ���
        //������ �ʿ䰡 ������ �ǹ�

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

        public override void RunProcess(float deltaTime)
        {

            switch (TypeUnitState)
            {
                case TYPE_UNIT_STATE.Idle:
                    SetTypeUnitState(TYPE_UNIT_STATE.Appear);
                    PlayAnimation("Forward", true);
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



        protected override void AppearRunProcess(float deltaTime)
        {
            if (!IsArriveAction())
            {
                //��ǥ���� �̵�
                SetPosition(Vector2.MoveTowards(transform.position, UNIT_ACTION_POSITION, deltaTime));
            }
            else
            {
                //��ǥ�� ���������� Action���� ��ȯ
                SetTypeUnitState(TYPE_UNIT_STATE.Action);
                PlayAnimation("Idle", true);
            }
        }


        private float _nowAttackTime = 0f;
        protected override void ActionRunProcess(float deltaTime)
        {
            if (HasTarget())
            {
                base.ActionRunProcess(deltaTime);

                //�ڽ� ����
                //�ڽ��� �����ϸ� ����
                _nowAttackTime += deltaTime;
                if (_nowAttackTime > _unitEntity.AttackDelay)
                {
                    if (IsHasAnimation("Attack"))
                    {
                        SetAnimation("Attack");
                    }
                    else
                    {
                        OnAttackTargetEvent(transform.position, _unitEntity.UnitData.AttackBulletKey, _unitEntity.UnitData.BulletScale, _unitEntity.DamageData);
                    }
                    _nowAttackTime = 0f;
                }
            }
        }

        protected override void DestoryActor()
        {
            base.DestoryActor();
            PlayAnimation("Dead");
        }

        #region ##### Spine Event #####
        private void OnSpineEvent(TrackEntry trackEntry, Spine.Event e)
        {
            OnAttackTargetEvent(transform.position, _unitEntity.UnitData.AttackBulletKey, _unitEntity.UnitData.BulletScale, _unitEntity.DamageData);
        }

        private void OnCompleteEvent(TrackEntry trackEntry)
        {
            if (trackEntry.Animation.Name == "Dead")
                OnDestroyedEvent();
        }
        #endregion


        
    }
}