namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using PoolSystem;

    public class UIHealthContainer : MonoBehaviour
    {

        private readonly static string UGUI_NAME = "UI@HealthContainer";

        [SerializeField]
        private UIEnemyHealthBar _uiEnemyHealthBar;

        private PoolSystem<UIHealthBar> _pool;

        public void Initialize()
        {
            _pool = new PoolSystem<UIHealthBar>();
            _pool.Initialize(CreateBar);
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

        private UIHealthBar CreateBar()
        {
            var block = UIHealthBar.Create();
            block.Initialize();
            block.SetOnRetrieveBlockListener(Retrieve);
            block.transform.SetParent(transform);
            return block;
        }



        public static UIHealthContainer Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UIHealthContainer>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.name = UGUI_NAME;
                return obj.AddComponent<UIHealthContainer>();
            }
#else
            Debug.LogWarning($"{UGUI_NAME}을 찾을 수 없습니다");
#endif

        }

    }
}