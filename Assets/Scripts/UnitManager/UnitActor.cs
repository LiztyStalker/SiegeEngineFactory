namespace SEF.Unit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using PoolSystem;

    public class UnitActor : PlayActor, ITarget, IPoolElement
    {
        private readonly static Vector2 UNIT_APPEAR_POSITION = new Vector2(-3.5f, 3f);
        private readonly static Vector2 UNIT_ACTION_POSITION = new Vector2(-2f, 3f);


        public static UnitActor Create()
        {
            var obj = new GameObject();
            obj.transform.position = ACTOR_CREATE_POSITION;
            obj.name = "Actor@Unit";
            var unitActor = obj.AddComponent<UnitActor>();
            obj.AddComponent<SpriteRenderer>();
            unitActor.InActivate();
            return unitActor;
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

        private static Sprite _instanceSprite = null;

        public static UnitActor Create_Test()
        {
            var obj = new GameObject();
            obj.transform.position = ACTOR_CREATE_POSITION;
            obj.name = "Actor@Unit";
            var unitActor = obj.AddComponent<UnitActor>();
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
                _instanceSprite = Sprite.Create(texture, new Rect(0, 0, 100, 100), Vector2.zero);
            }

            sprite.sprite = _instanceSprite;
            unitActor.InActivate();
            return unitActor;
        }
#endif


        public override void Activate()
        {
            base.Activate();
            transform.position = UNIT_APPEAR_POSITION;
        }

        public void SetData()
        {
        }

        public override void RunProcess(float deltaTime)
        {

            switch (TypeUnitState)
            {
                case TYPE_UNIT_STATE.Idle:
                    SetTypeUnitState(TYPE_UNIT_STATE.Appear);
                    break;
                case TYPE_UNIT_STATE.Appear:
                    AppearRunProcess(deltaTime);
                    break;
                case TYPE_UNIT_STATE.Action:
                    ActionRunProcess(deltaTime);
                    break;
                case TYPE_UNIT_STATE.Destory:
                    break;
            }
            //상태
            //Appear
            //Action
            //Destroy
        }



        protected override void AppearRunProcess(float deltaTime)
        {

            if (Vector2.Distance(transform.position, UNIT_ACTION_POSITION) > 0.1f)
            {
                //목표까지 이동
                transform.position = Vector2.MoveTowards(transform.position, UNIT_ACTION_POSITION, deltaTime);
            }
            else
            {
                //목표에 도달했으면 Action으로 변환
                SetTypeUnitState(TYPE_UNIT_STATE.Action);
            }

        }


        protected override void ActionRunProcess(float deltaTime)
        {
            if(Target != null)
            {
                Debug.Log("Attack");
            }
            //적 공격
        }

    }
}