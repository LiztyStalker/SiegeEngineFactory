namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using Data;

    public class UIWorkshopExpend : MonoBehaviour
    {
        private readonly static string UGUI_NAME = "UI@WorkshopExpend";

        [SerializeField]
        private UIAssetButton _expendBtn;

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
            return null;
#endif
        }


        public void Initialize()
        {
            _expendBtn.onClick.AddListener(OnExpendEvent);
        }

        public void RefreshExpend(IAssetData assetData, bool isActive)
        {
            _expendBtn.SetData(assetData);
            _expendBtn.interactable = isActive;
            _expendBtn.SetLabel("확장");
        }

        public void CleanUp()
        {
            _expendBtn.onClick.RemoveListener(OnExpendEvent);
        }



#region ##### Listener #####


        private System.Action _expendEvent;
        public void AddOnExpendListener(System.Action act) => _expendEvent += act;
        public void RemoveOnExpendListener(System.Action act) => _expendEvent -= act;
        private void OnExpendEvent()
        {
            _expendEvent?.Invoke();
        }

#endregion
    }

}