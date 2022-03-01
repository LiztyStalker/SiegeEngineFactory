namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UIElements;
    using PoolSystem;

    public class UIHealthContainer : MonoBehaviour
    {
        private UIEnemyHealthBar _uiEnemyHealthBar;

        private PoolSystem<UIHealthBar> _pool;

        public void Initialize()
        {
            _pool = new PoolSystem<UIHealthBar>();
            _pool.Initialize(Create);

            //_uiEnemyHealthBar = parent.Q<UIEnemyHealthBar>();
        }

        public void CleanUp()
        {
            _pool.CleanUp();
        }

        public void ShowEnemyHealthData(string value, float rate)
        {
            _uiEnemyHealthBar.ShowHealth(value, rate);
        }

        public void ShowUnitHealthData(float rate, Vector2 position)
        {
            var block = _pool.GiveElement();
            block.ShowHealth(rate, position);
            block.Activate();
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        /// <summary>
        /// Test용
        /// Release에서 사용 불가
        /// </summary>
        /// <param name="rate"></param>
        /// <param name="position"></param>
        /// <param name="endCallback"></param>
        public void ShowUnitHealthData_Test(float rate, Vector2 position, System.Action<UIHealthBar> endCallback)
        {
            var block = _pool.GiveElement();
            block.SetOnRetrieveBlockListener(endCallback);
            block.ShowHealth(rate, position);
            block.Activate();
        }
#endif


        private void Retrieve(UIHealthBar block)
        {
            _pool.RetrieveElement(block);
        }

        private UIHealthBar Create()
        {
            var block = UIHealthBar.Create();
            block.Initialize();
            block.SetOnRetrieveBlockListener(Retrieve);
            return block;
        }

        public static UIHealthContainer Create(Transform parent)
        {
            var obj = new GameObject();
            obj.name = "UIHealthContainer";
            var container = obj.AddComponent<UIHealthContainer>();
            container.transform.SetParent(parent);
            return container;
        }
    }
}