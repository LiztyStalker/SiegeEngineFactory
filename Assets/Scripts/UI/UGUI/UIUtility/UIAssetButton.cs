namespace SEF.UI
{
    using SEF.Data;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;

    public class UIAssetButton : Button, IPointerDownHandler, IPointerUpHandler
    {
        private readonly static string UGUI_NAME = "UI@AssetButton";


        [SerializeField]
        private Image _icon;

        [SerializeField]
        private Text _valueLabel;

        [SerializeField]
        private Text _buttonText;

        [SerializeField]
        private GameObject _assetLayout;

        public static UIAssetButton Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UIAssetButton>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.name = UGUI_NAME;
                return obj.AddComponent<UIAssetButton>();
            }
#else
            Debug.LogWarning($"{UGUI_NAME}을 찾을 수 없습니다");
            return null;
#endif
        }

        public void SetData(IAssetData data)
        {
            var icon = Storage.DataStorage.Instance.GetDataOrNull<Sprite>($"Icon_{data.GetType().Name}", null, null);
            _icon.sprite = icon;
            _valueLabel.text = data.GetValue();
            _assetLayout.gameObject.SetActive(true);
        }

        public void SetLabel(string text)
        {
            _buttonText.text = text;
        }

        public void SetEmpty()
        {
            _assetLayout.gameObject.SetActive(false);
        }

        private bool _isDown = false;
        private float _nowTime = 0f;

        private void Update()
        {
            if (_isDown)
            {

                if (!interactable && !_assetLayout.activeSelf) _isDown = false;

                _nowTime += Time.deltaTime;
                if(_nowTime > 1f)
                {
                    OnSubmit(null);
                    _nowTime = 0.95f;
                }
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            //Debug.Log("down");
            _isDown = true;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (_isDown)
            {
                //Debug.Log("up");
                _isDown = false;
                _nowTime = 0f;
            }
        }
    }
}