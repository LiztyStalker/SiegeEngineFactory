namespace SEF.Unit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public enum TYPE_UNIT_STATE { Idle, Appear, Action, Destory}

    public abstract class PlayActor : MonoBehaviour
    {
        protected readonly static Vector2 ACTOR_CREATE_POSITION = new Vector2(10f, 3f);

        private ITarget _target;
        protected ITarget Target => _target;


        private TYPE_UNIT_STATE _typeUnitState;
        protected TYPE_UNIT_STATE TypeUnitState => _typeUnitState;

        public void SetTarget(ITarget target)
        {
            _target = target;
        }

        protected void SetTypeUnitState(TYPE_UNIT_STATE typeUnitState)
        {
            _typeUnitState = typeUnitState;
        }

        public bool IsActionState() => _typeUnitState == TYPE_UNIT_STATE.Action;


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

        #region ##### Data #####
        //public AccountData GetData()
        //{

        //}
        #endregion


        //public AssetData GetAssetData()
        //{

        //}

        public bool IsHasEnemy()
        {
            return false;
        }

        public void DecreaseHealth(/*AttackData*/)
        {
            _hitEvent?.Invoke(this);
            //체력이 0이면
            DestoryActor();
        }


        protected void DestoryActor()
        {
            //애니메이션 후 destoryEvent 실행
            _destoryEvent?.Invoke(this);
        }


        #region ##### Listener #####

        protected System.Action<PlayActor> _hitEvent;
        public void AddOnHitListener(System.Action<PlayActor> act/*AttackData*/) => _hitEvent += act;
        public void RemoveOnHitListener(System.Action<PlayActor> act/*AttackData*/) => _hitEvent -= act;


        protected System.Action<PlayActor> _destoryEvent;
        public void AddOnDestoryListener(System.Action<PlayActor> act/*ITarget*/) => _destoryEvent += act;
        public void RemoveOnDestoryListener(System.Action<PlayActor> act /*ITarget*/) => _destoryEvent -= act;

        #endregion

    }
}