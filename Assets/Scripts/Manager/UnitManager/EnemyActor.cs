namespace SEF.Unit { 
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using PoolSystem;

    public class EnemyActor : PlayActor, ITarget, IPoolElement
    {
        private readonly static Vector2 ENEMY_IDLE_POSITION = new Vector2(3.5f, 2f);
        private readonly static Vector2 ENEMY_READY_POSITION = new Vector2(2.5f, 2f);
        private readonly static Vector2 ENEMY_ACTION_POSITION = new Vector2(1.5f, 2f);

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public readonly static Vector2 ENEMY_IDLE_POSITION_TEST = ENEMY_IDLE_POSITION;
        public readonly static Vector2 ENEMY_READY_POSITION_TEST = ENEMY_READY_POSITION;
        public readonly static Vector2 ENEMY_ACTION_POSITION_TEST = ENEMY_ACTION_POSITION;
#endif

        public bool IsArriveReady() => (Vector2.Distance(transform.position, ENEMY_READY_POSITION) < ACTOR_ARRIVE_DISTANCE);
        public bool IsArriveAction() => (Vector2.Distance(transform.position, ENEMY_ACTION_POSITION) < ACTOR_ARRIVE_DISTANCE);

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


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        private static Sprite _instanceSprite = null;

        public static EnemyActor Create_Test()
        {
            var obj = new GameObject();
            obj.name = "Actor@Enemy";

            var enemyActor = obj.AddComponent<EnemyActor>();
            enemyActor.SetPosition(ACTOR_CREATE_POSITION);
            enemyActor.InActivate();

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
            return enemyActor;
        }
#endif

        public override void Activate()
        {
            base.Activate();
            SetPosition(ENEMY_IDLE_POSITION);
        }

        public override void RunProcess(float deltaTime)
        {
            switch (TypeUnitState)
            {
                case TYPE_UNIT_STATE.Idle:
                    //Idle
                    break;
                case TYPE_UNIT_STATE.Ready:
                    ReadyRunProcess(deltaTime);
                    break;
                case TYPE_UNIT_STATE.Appear:
                    //Appear
                    AppearRunProcess(deltaTime);
                    break;
                case TYPE_UNIT_STATE.Action:
                    //Action
                    ActionRunProcess(deltaTime);
                    break;
                case TYPE_UNIT_STATE.Destory:
                    //Destroy
                    break;
            }
            //Action
            //Destroy
        }

        public void SetData()
        {

        }
        private void ReadyRunProcess(float deltaTime)
        {
            if (!IsArriveReady())
            {
                //목표까지 이동
                SetPosition(Vector2.MoveTowards(transform.position, ENEMY_READY_POSITION, deltaTime));
            }
            else
            {
                Debug.Log("Ready에 도달함");
            }
        }

        protected override void AppearRunProcess(float deltaTime)
        {
            if (!IsArriveAction())
            {
                //목표까지 이동
                SetPosition(Vector2.MoveTowards(transform.position, ENEMY_ACTION_POSITION, deltaTime));
            }
            else
            {
                //목표에 도달했으면 Action으로 변환
                Debug.Log("Action에 도달함");
                SetTypeUnitState(TYPE_UNIT_STATE.Action);
            }
        }


        private float _nowActionTime = 0f;

        protected override void ActionRunProcess(float deltaTime)
        {
            //적 공격자 행동


            //TestCode
            if (Target == null)
            {
                Debug.Assert(_findTargetEvent != null, "FindTargetEvent가 비어있습니다");
                SetTarget(_findTargetEvent());
            }
            else
            {
                _nowActionTime += deltaTime;
                if (_nowActionTime > 1f)
                {
                    Target.DecreaseHealth(null);
                    _nowActionTime = 0f;
                }
                
            }
        }


        #region ##### Listener #####

        private System.Func<ITarget> _findTargetEvent;
        public void SetOnFindTargetListener(System.Func<ITarget> act) => _findTargetEvent = act;


        #endregion

    }
}