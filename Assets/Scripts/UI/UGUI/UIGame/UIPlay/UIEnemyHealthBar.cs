namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;


    public class UIEnemyHealthBar : MonoBehaviour
    {
        [SerializeField]
        private Text _value;

        [SerializeField]
        private Slider _slider;

        public void ShowHealth(string value, float rate)
        {
            _value.text = value;
            _slider.value = rate;
        }


    }
#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

    public class UIEnemyHealthBar_Test : MonoBehaviour
    {
        private UIEnemyHealthBar _instance;
        public UIEnemyHealthBar Instance => _instance;

        public static UIEnemyHealthBar_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UIEnemyHealthBar_Test";
            return obj.AddComponent<UIEnemyHealthBar_Test>();
        }

        public void Initialize()
        {
            //var root = UIUXML.GetVisualElement(gameObject, UIEnemyHealthBar.PATH_UI_UXML);
            //_instance = root.Q<UIEnemyHealthBar>();
        }

        public void Dispose()
        {
            _instance = null;
            DestroyImmediate(gameObject);
        }
    }
#endif
}