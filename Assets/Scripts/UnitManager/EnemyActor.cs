namespace SEF.Unit { 
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using PoolSystem;

    public class EnemyActor : PlayActor, ITarget, IPoolElement
    {
        private readonly static Vector2 ENEMY_APPEAR_POSITION = new Vector2(4f, 3f);
        private readonly static Vector2 ENEMY_READY_POSITION = new Vector2(3f, 3f);
        private readonly static Vector2 ENEMY_ACTION_POSITION = new Vector2(2f, 3f);



        public static EnemyActor Create()
        {
            var obj = new GameObject();
            obj.name = "Actor@Enemy";
            obj.AddComponent<SpriteRenderer>();
            var enemyActor = obj.AddComponent<EnemyActor>();
            enemyActor.SetPosition(ACTOR_CREATE_POSITION);
            enemyActor.InActivate();
            return enemyActor;
        }

        public override void Activate()
        {
            base.Activate();
            SetPosition(ENEMY_APPEAR_POSITION);
        }

        public override void RunProcess(float deltaTime)
        {
            //ป๓ลย
            //Idle
            //Appear
            //Action
            //Destroy
        }

        public void SetData()
        {

        }

        protected override void ActionRunProcess(float deltaTime)
        {
        }

        protected override void AppearRunProcess(float deltaTime)
        {
        }
    }
}