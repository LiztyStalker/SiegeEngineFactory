namespace SEF.UI.Toolkit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using PoolSystem;

    [RequireComponent(typeof(UIDocument))]
    public class UIHitContainer : MonoBehaviour
    {
        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UIPlay/UIHitContainer.uxml";

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
            block.Inactivate();
            _pool.RetrieveElement(block);
        }

        private UIHitBlock Create()
        {
            var block = UIHitBlock.Create();
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
#endif
    }
}