namespace SEF.Unit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Data;
    using Spine.Unity;
    using UtilityManager;

    public enum TYPE_UNIT_STATE { Idle, Ready, Appear, Action, Destory}

    public abstract class PlayActor : MonoBehaviour
    {
        protected readonly static Vector2 ACTOR_CREATE_POSITION = new Vector2(10f, 2f);

        protected readonly static float ACTOR_ARRIVE_DISTANCE = 0.1f;

        private ITarget _target;
        protected ITarget Target => _target;


        private List<AttackActor> _attackActorList = new List<AttackActor>();
        public AttackActor[] AttackActorArray => _attackActorList.ToArray();



        private TYPE_UNIT_STATE _typeUnitState;
        protected TYPE_UNIT_STATE TypeUnitState => _typeUnitState;
        public Vector2 NowPosition => transform.position;

        private HealthData _nowHealthData;
        protected HealthData NowHealthData => _nowHealthData;

        public abstract float NowHealthRate();

        protected void SetHealthData(HealthData healthData)
        {
            _nowHealthData = healthData.Clone() as HealthData;
        }

        [System.Obsolete("사용하지 않음")]
        public void SetTarget(ITarget target)
        {
            _target = target;
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

        public void SetPosition_Test(Vector2 position)
        {
            SetPosition(position);
        }
#endif

        public void SetTypeUnitState(TYPE_UNIT_STATE typeUnitState)
        {
            _typeUnitState = typeUnitState;
        }

        protected void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public virtual void Activate()
        {
            ActivateAttackActor();
            _typeUnitState = TYPE_UNIT_STATE.Idle;
            gameObject.SetActive(true);
        }

        private void ActivateAttackActor()
        {
            for (int i = 0; i < _attackActorList.Count; i++)
            {
                _attackActorList[i].Activate();
            }
        }

        public virtual void InActivate()
        {
            InactivateAttackActor();
            gameObject.SetActive(false);
        }

        private void InactivateAttackActor()
        {
            for (int i = 0; i < _attackActorList.Count; i++)
            {
                _attackActorList[i].Inactivate();
            }
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }

        public abstract void RunProcess(float deltaTime);
        protected abstract void AppearRunProcess(float deltaTime);
        protected virtual void ActionRunProcess(float deltaTime)
        {
            for (int i = 0; i < _attackActorList.Count; i++)
            {
                _attackActorList[i].RunProcess(deltaTime);
            }
        }


        public void CleanUp()
        {
            DestroyImmediate(gameObject);
        }

        private void CleanUpAttackActor()
        {
            for (int i = 0; i < _attackActorList.Count; i++)
            {
                _attackActorList[i].CleanUp();
            }
        }


        //public AssetData GetAssetData()
        //{

        //}

        protected void SetAttackerData(AttackerData[] attackerDataArray, NumberData numberData)
        {
            if(attackerDataArray.Length > 0)
            {
                for(int i = 0; i < attackerDataArray.Length; i++)
                {
                    var attackerData = attackerDataArray[i];
                    var attackActor = AttackActor.Create(transform);
                    var skeletonDataAsset = FindSkeletonDataAsset(attackerData.SkeletonDataAssetKey);
                    attackActor.SetData(skeletonDataAsset, attackerData, numberData);
                    attackActor.SetOnAttackTargetListener(OnAttackTargetEvent);
                    attackActor.Initialize();
                    _attackActorList.Add(attackActor);
                }
            }
        }

        protected SkeletonDataAsset FindSkeletonDataAsset(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                return Storage.DataStorage.Instance.GetDataOrNull<SkeletonDataAsset>(key, null, null);
            }
            return null;
        }

        public void DecreaseHealth(DamageData attackData)
        {
            _nowHealthData.Subject(attackData);

            if (_nowHealthData.IsZero())
            {
                DestoryActor();
            }
            OnHitEvent(attackData);
        }

        #region ##### Data #####
        //public AccountData GetData()
        //{

        //}
        #endregion


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

        public void Destory_Test_Spine()
        {
            DestoryActor();
        }

        public void Destroy_Test(System.Action endCallback)
        {
            DestoryActor_Test(endCallback);
        }

        protected void DestoryActor_Test(System.Action endCallback)
        {
            StartCoroutine(DestroyCoroutine(endCallback));
        }

        private IEnumerator DestroyCoroutine(System.Action endCallback)
        {
            Debug.Log("Destroy Start");
            var deadTime = 0f;
            while (true)
            {
                deadTime += Time.deltaTime;
                if (deadTime > 1f)
                {
                    break;
                }
                yield return null;
            }
            Debug.Log("Destroy End");
            OnDestroyedEvent();
            endCallback?.Invoke();
            yield return null;
        }
#endif


        protected virtual void DestoryActor()
        {
            //애니메이션 후 destoryEvent 실행 됨
            SetTypeUnitState(TYPE_UNIT_STATE.Destory);
            DestoryAttackActor();
        }

        protected void OnDestroyedEvent()
        {
            _destoryedEvent?.Invoke(this);
        }

        private void DestoryAttackActor()
        {
            for(int i = 0; i < _attackActorList.Count; i++)
            {
                _attackActorList[i].Destory();
            }
        }

        #region ##### Listener #####

        protected System.Action<PlayActor, DamageData> _hitEvent;
        public void AddOnHitListener(System.Action<PlayActor, DamageData> act) => _hitEvent += act;
        public void RemoveOnHitListener(System.Action<PlayActor, DamageData> act) => _hitEvent -= act;
        private void OnHitEvent(DamageData attackData)
        {
            _hitEvent?.Invoke(this, attackData);
        }

        protected System.Action<PlayActor> _destoryedEvent;
        public void AddOnDestoryedListener(System.Action<PlayActor> act/*ITarget*/) => _destoryedEvent += act;
        public void RemoveOnDestoryedListener(System.Action<PlayActor> act /*ITarget*/) => _destoryedEvent -= act;


        private System.Action<PlayActor, Vector2, string, float, DamageData> _attackTargetEvent;
        public void SetOnAttackTargetListener(System.Action<PlayActor, Vector2, string, float, DamageData> act) => _attackTargetEvent = act;

        protected void OnAttackTargetEvent(Vector2 attackPos, string bulletDataKey, float scale, DamageData damageData)
        {
            _attackTargetEvent?.Invoke(this, attackPos, bulletDataKey, scale, damageData);
        }

        #endregion

    }
}