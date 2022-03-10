namespace Storage.UI
{
    using UnityEngine.UI;
    using UnityEngine;

    public class UITextTranslator : MonoBehaviour
    {
        [SerializeField]
        private string _title;

        [SerializeField]
        private string _key;

        [SerializeField]
        private string _verb;

        [SerializeField]
        private int _index = 0;

        private Text _text;

        private Text @Text
        {
            get
            {
                if(_text == null)
                    _text = GetComponentInChildren<Text>();
                return _text;
            }
        }

        private void OnEnable()
        {
            TranslateStorage.Instance.AddOnChangedTranslateListener(SetText);
            SetText();
        }

        private void OnDisable()
        {
            TranslateStorage.Instance.RemoveOnChangedTranslateListener(SetText);
        }

        public void SetText()
        {
            Text.text = TranslateStorage.Instance.GetTranslateData(_title, _key, _verb, _index);
        }
    }
}