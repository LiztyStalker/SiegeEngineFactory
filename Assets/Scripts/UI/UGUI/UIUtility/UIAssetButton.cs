namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UIAssetButton : Button
    {
        private readonly static string UGUI_NAME = "UI@AssetButton";


        [SerializeField]
        private Image _icon;

        [SerializeField]
        private Text _valueLabel;

        [SerializeField]
        private Text _buttonText;

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
#endif
        }


        public void SetData(string text, Sprite icon, string value)
        {
            _icon.sprite = icon;
            _valueLabel.text = value;
            _buttonText.text = text;
        }
    }
}