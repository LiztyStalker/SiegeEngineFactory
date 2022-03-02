namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using PoolSystem;

    public class UIHealthBar : MonoBehaviour, IPoolElement
    {
        private readonly static string UGUI_NAME = "UI@HealthBar";

        [SerializeField]
        private Slider _slider;

        public void Initialize()
        {
            Inactivate();
        }

        public void CleanUp()
        {
            _retrieveEvent = null;
            Inactivate();
        }


        public void ShowHealth(float fillAmount, Vector2 position)
        {
            transform.position = position;
            _slider.value = fillAmount;
        }


        public void Activate()
        {
            gameObject.SetActive(true);
            SetPosition();
        }

        public void Inactivate()
        {
            gameObject.SetActive(false);
        }


        private void SetPosition()
        {
            var screenPos = Camera.main.WorldToScreenPoint(transform.position);
            screenPos.y = Screen.height - screenPos.y;
            screenPos.z = 0;
            transform.position = screenPos;
        }

        private float nowTime = 0;
        private void Update()
        {
            nowTime += Time.deltaTime;
            if (nowTime > 1f)
            {
                nowTime = 0;
                _retrieveEvent?.Invoke(this);
                Inactivate();
            }
        }

        #region ##### Listener #####

        private System.Action<UIHealthBar> _retrieveEvent;
        public void SetOnRetrieveBlockListener(System.Action<UIHealthBar> act) => _retrieveEvent = act;

        #endregion



        public static UIHealthBar Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UIHealthBar>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.name = UGUI_NAME;
                return obj.AddComponent<UIHealthBar>();
            }
#else
            Debug.LogWarning($"{UGUI_NAME}을 찾을 수 없습니다");
            return null;
#endif

        }
    }
}