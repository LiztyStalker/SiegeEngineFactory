namespace SEF.UI
{
    using UnityEngine;
    using PoolSystem;

    public class UIHitContainer : MonoBehaviour
    {
        private readonly static string UGUI_NAME = "UI@HitContainer";

        private PoolSystem<UIHitBlock> _pool;

        public void Initialize()
        {
            _pool = new PoolSystem<UIHitBlock>();
            _pool.Initialize(CreateBlock);
        }

        public void CleanUp()
        {
            _pool.CleanUp();
        }

        public void ShowHit(string value, Vector2 position)
        {
            var hit = _pool.GiveElement();
            hit.ShowHit(value, position);
            hit.Activate();
        }

        private void Retrieve(UIHitBlock block)
        {
            _pool.RetrieveElement(block);
        }

        private UIHitBlock CreateBlock()
        {
            var block = UIHitBlock.Create();
            block.Initialize();
            block.SetOnRetrieveBlockListener(Retrieve);
            block.transform.SetParent(transform);
            return block;
        }

        public static UIHitContainer Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UIHitContainer>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.name = UGUI_NAME;
                return obj.AddComponent<UIHitContainer>();
            }
#else
            Debug.LogWarning($"{UGUI_NAME}을 찾을 수 없습니다");
#endif

        }


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

        /// <summary>
        /// ShowHit 테스트판
        /// Release에서는 사용 불가
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        /// <param name="endCallback"></param>
        public void ShowHit_Test(string value, Vector2 position, System.Action<UIHitBlock> endCallback)
        {
            var hit = _pool.GiveElement();
            hit.SetOnRetrieveBlockListener(endCallback);
            hit.ShowHit(value, position);
            hit.Activate();
        }


#endif
    }
}