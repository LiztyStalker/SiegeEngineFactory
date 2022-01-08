namespace SEF.UI.Toolkit
{
    using UnityEngine;
    using UnityEngine.UIElements;
    using PoolSystem;

    public class UIHealthContainer : MonoBehaviour
    {
        private UIEnemyHealthBar _uiEnemyHealthBar;

        private PoolSystem<UIHealthBar> _pool;

        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UIPlay/UIHitContainer.uxml";

        public void Initialize()
        {
            _pool = new PoolSystem<UIHealthBar>();
            _pool.Initialize(Create);
        }

        public void CleanUp()
        {
            _pool.CleanUp();
        }

        public void ShowEnemyHealth(string value, float rate)
        {
            _uiEnemyHealthBar.ShowHealth(value, rate);
        }

        public void ShowHealth(float rate, Vector2 position)
        {
            var hit = _pool.GiveElement();
            hit.ShowHealth(rate, position);
            hit.Activate();
        }


        private void Retrieve(UIHealthBar block)
        {
            block.Inactivate();
            _pool.RetrieveElement(block);
        }

        private UIHealthBar Create()
        {
            var block = UIHealthBar.Create();
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