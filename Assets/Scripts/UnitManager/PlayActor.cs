namespace SEF.Unit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class PlayActor : MonoBehaviour
    {

        public void Activate()
        {

        }

        public void InActivate()
        {

        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }


        public void RunProcess(float deltaTime)
        {

        }
               
        public void CleanUp()
        {

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