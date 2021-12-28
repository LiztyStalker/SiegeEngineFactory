namespace SEF.Unit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Data;

    public enum TYPE_UNIT_STATE { Idle, Ready, Appear, Action, Destory}

    public abstract class PlayActor : MonoBehaviour
    {
        protected readonly static Vector2 ACTOR_CREATE_POSITION = new Vector2(10f, 2f);

        protected readonly static float ACTOR_ARRIVE_DISTANCE = 0.1f;

        private ITarget _target;
        protected ITarget Target => _target;


        private TYPE_UNIT_STATE _typeUnitState;
        protected TYPE_UNIT_STATE TypeUnitState => _typeUnitState;
        public Vector2 NowPosition => transform.position;

        private HealthData _nowHealthData;

        protected HealthData NowHealthData => _nowHealthData;

        protected void SetHealthData(HealthData healthData)
        {
            if(healthData != null)
                _nowHealthData = healthData.Clone() as HealthData;
        }

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
            _typeUnitState = TYPE_UNIT_STATE.Idle;
            gameObject.SetActive(true);
        }

        public void InActivate()
        {
            gameObject.SetActive(false);
        }



        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }


        public abstract void RunProcess(float deltaTime);
        protected abstract void AppearRunProcess(float deltaTime);
        protected abstract void ActionRunProcess(float deltaTime);


        public void CleanUp()
        {
            DestroyImmediate(gameObject);
        }



        //public AssetData GetAssetData()
        //{

        //}

        public bool IsHasEnemy()
        {
            return false;
        }

        public void DecreaseHealth(AttackData attackData)
        {
            _hitEvent?.Invoke(this);
            _nowHealthData.Subject(attackData);

            if (_nowHealthData.IsZero())
            {
                DestoryActor();
            }
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
            //애니메이션 후 destoryEvent 실행
            SetTypeUnitState(TYPE_UNIT_STATE.Destory);
            //OnDestroyedEvent();
        }

        protected void OnDestroyedEvent()
        {
            _destoryedEvent?.Invoke(this);
        }

        #region ##### Listener #####

        protected System.Action<PlayActor> _hitEvent;
        public void AddOnHitListener(System.Action<PlayActor> act/*AttackData*/) => _hitEvent += act;
        public void RemoveOnHitListener(System.Action<PlayActor> act/*AttackData*/) => _hitEvent -= act;


        protected System.Action<PlayActor> _destoryedEvent;
        public void AddOnDestoryedListener(System.Action<PlayActor> act/*ITarget*/) => _destoryedEvent += act;
        public void RemoveOnDestoryedListener(System.Action<PlayActor> act /*ITarget*/) => _destoryedEvent -= act;

        #endregion

    }
}