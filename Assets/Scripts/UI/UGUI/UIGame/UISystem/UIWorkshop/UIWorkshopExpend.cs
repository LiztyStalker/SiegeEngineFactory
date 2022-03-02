namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using Data;

    public class UIWorkshopExpend : MonoBehaviour
    {
        private readonly static string UGUI_NAME = "UI@WorkshopExpend";

        [SerializeField]
        private Button _expendButton;
        [SerializeField]
        private Image _expendAssetIcon;
        [SerializeField]
        private Text _expendValueLabel;

        public static UIWorkshopExpend Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UIWorkshopExpend>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.name = UGUI_NAME;
                return obj.AddComponent<UIWorkshopExpend>();
            }
#else
            Debug.LogWarning($"{UGUI_NAME}을 찾을 수 없습니다");
#endif
        }


        public void Initialize()
        {
            _expendValueLabel.text = "0";
            _expendButton.onClick.AddListener(OnExpendEvent);
        }

        public void RefreshExpend(IAssetData assetData, bool isActive)
        {
            //_expendAssetIcon.sprite = null;
            _expendValueLabel.text = assetData.GetValue();
            _expendButton.interactable = isActive;
        }

        public void CleanUp()
        {
            _expendButton.onClick.RemoveListener(OnExpendEvent);
        }



#region ##### Listener #####


        private System.Action _expendEvent;
        public void AddExpendListener(System.Action act) => _expendEvent += act;
        public void RemoveExpendListener(System.Action act) => _expendEvent -= act;
        private void OnExpendEvent()
        {
            _expendEvent?.Invoke();
        }

#endregion
    }

}