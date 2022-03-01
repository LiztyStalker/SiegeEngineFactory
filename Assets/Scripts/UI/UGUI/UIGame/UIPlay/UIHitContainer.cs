namespace SEF.UI
{
    using UnityEngine;
    using PoolSystem;

    public class UIHitContainer : MonoBehaviour
    {

        private PoolSystem<UIHitBlock> _pool;

        public void Initialize()
        {
            _pool = new PoolSystem<UIHitBlock>();
            _pool.Initialize(Create);
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

        private UIHitBlock Create()
        {
            var block = UIHitBlock.Create();
            block.Initialize();
            block.SetOnRetrieveBlockListener(Retrieve);
            return block;
        }


#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

        public static UIHitContainer Create(Transform parent)
        {
            var obj = new GameObject();
            obj.name = "UIHitContainer";
            var container = obj.AddComponent<UIHitContainer>();
            container.transform.SetParent(parent);
            return container;
        }


        /// <summary>
        /// ShowHit �׽�Ʈ��
        /// Release������ ��� �Ұ�
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