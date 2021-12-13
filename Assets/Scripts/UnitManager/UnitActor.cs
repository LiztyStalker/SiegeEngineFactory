namespace SEF.Unit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using PoolSystem;

    public class UnitActor : PlayActor, ITarget, IPoolElement
    {

        public static UnitActor Create()
        {
            var obj = new GameObject();
            obj.name = "Actor@Unit";
            var unitActor = obj.AddComponent<UnitActor>();
            unitActor.InActivate();
            return unitActor;

        }

        public void SetData()
        {

        }



    }
}