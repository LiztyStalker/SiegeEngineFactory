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
    using Utility.IO;
    using System;


    #region ##### StorableData #####

    [System.Serializable]
    public struct SerializeVector2
    {
        [SerializeField] public float x;
        [SerializeField] public float y;

        public SerializeVector2(Vector2 vec)
        {
            x = vec.x;
            y = vec.y;
        }

        public static implicit operator Vector2(SerializeVector2 sVec) => new Vector2(sVec.x, sVec.y);
        public static implicit operator Vector3(SerializeVector2 sVec) => new Vector3(sVec.x, sVec.y);
    }

    [System.Serializable]
    public class UnitActorStorableData : StorableData
    {
        [SerializeField] private SerializeVector2 _position;
        [SerializeField] private SerializeBigNumberData _nowHealthData;

        internal SerializeVector2 Position => _position;
        internal SerializeBigNumberData NowHealthData => _nowHealthData;

        internal void SetData(Vector2 position, BigNumberData data, StorableData entity)
        {
            _position = new SerializeVector2(position);
            _nowHealthData = data.GetSerializeData();
            Children = new Utility.IO.StorableData[1];
            Children[0] = entity;
        }
    }
    #endregion

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
        public override string Key => _unitEntity.UnitData.Key;

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
                //유닛 생성
                if (unitEntity.UnitData.SkeletonDataAsset != null)
                {
                    SkeletonAnimation.skeletonDataAsset = unitEntity.UnitData.SkeletonDataAsset;
                }
                else
                {
                    var data = DataStorage.Instance.GetDataOrNull<SkeletonDataAsset>(unitEntity.UnitData.SpineModelKey, null, null);
#if UNITY_EDITOR
                    if (data == null)
                        data = DataStorage.Instance.GetDataOrNull<SkeletonDataAsset>("BowSoldier_SkeletonData", null, null);
#endif
                    SkeletonAnimation.skeletonDataAsset = data;
                }

                transform.localScale = Vector3.one * unitEntity.UnitData.Scale;
            }

            name = $"Actor@{unitEntity.UnitData.Key}";
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

        public void SetData_Test(UnitEntity unitEntity)
        {
            _unitEntity = unitEntity;

            SetHealthData(unitEntity.HealthData);

            SetAttackerData(unitEntity.UnitData.AttackerDataArray, unitEntity.UpgradeData);
        }
#endif


        //UnitActor, EnemyActor, AttackActor에서 사용
        //통합할 필요가 있을지 의문

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
                    if (_typeUnitCycle == TYPE_UNIT_ACTION_CYCLE.Move)
                    {
                        Vector2.MoveTowards(transform.position, UNIT_APPEAR_POSITION, deltaTime);
                    }
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

                //행동 사이클 - 이동중이 아니면 멈춤
                if (_typeUnitCycle != TYPE_UNIT_ACTION_CYCLE.Move)
                    PlayAnimation("Idle", true);
            }
        }


        private float _nowAttackTime = 0f;
        protected override void ActionRunProcess(float deltaTime)
        {

            if (_typeUnitCycle == TYPE_UNIT_ACTION_CYCLE.Action)
            {
                if (HasTarget())
                {
                    base.ActionRunProcess(deltaTime);

                    //자신 공격
                    //자신이 공격하면 가능
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
        }

        protected override void DestoryActor()
        {
            base.DestoryActor();
            PlayAnimation("Dead");
        }

        public enum TYPE_UNIT_ACTION_CYCLE { Idle, Move, Action }

        private TYPE_UNIT_ACTION_CYCLE _typeUnitCycle;
        public void SetMovement(TYPE_UNIT_ACTION_CYCLE typeUnitCycle)
        {
            _typeUnitCycle = typeUnitCycle;
            if (TypeUnitState == TYPE_UNIT_STATE.Action)
            {
                _nowAttackTime = 0f;
                switch (typeUnitCycle)
                {
                    default:
                        PlayAnimation("Idle", true);
                        break;
                    case TYPE_UNIT_ACTION_CYCLE.Move:
                        PlayAnimation("Forward", true);
                        break;
                }
            }
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




        #region ##### StorableData #####
        public StorableData GetStorableData()
        {
            var data = new UnitActorStorableData();
            data.SetData(transform.position, NowHealthData, _unitEntity.GetStorableData());
            return data;
        }

        public void SetStorableData(StorableData data)
        {
            var storableData = (UnitActorStorableData)data;
            transform.position = storableData.Position;
            NowHealthData = (HealthData)storableData.NowHealthData.GetDeserializeData();
        }
        #endregion

    }
}