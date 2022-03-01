namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UILoad : MonoBehaviour
    {

        [SerializeField]
        private Text _loadLabel;

        [SerializeField]
        private Text _loadValueLabel;

        public static UILoad Create()
        {
            var obj = new GameObject();
            obj.name = "UI@Load";
            obj.AddComponent<Canvas>();
            return obj.AddComponent<UILoad>();
        }

        public void Initialize()
        {
            _loadLabel.text = "·ÎµùÁß";
            _loadValueLabel.text = "0";

            Hide();

        }

        public void CleanUp()
        {
        }

        public void ShowLoad(float progress)
        {
            _loadValueLabel.text = progress.ToString();
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}