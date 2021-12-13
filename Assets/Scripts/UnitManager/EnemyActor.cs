namespace SEF.Unit { 
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using PoolSystem;

    public class EnemyActor : PlayActor, ITarget, IPoolElement
    {
        public static EnemyActor Create()
        {
            var obj = new GameObject();
            obj.name = "Actor@Enemy";
            var enemyActor = obj.AddComponent<EnemyActor>();
            enemyActor.InActivate();
            return enemyActor;
        }

        public void SetData()
        {

        }

    }
}