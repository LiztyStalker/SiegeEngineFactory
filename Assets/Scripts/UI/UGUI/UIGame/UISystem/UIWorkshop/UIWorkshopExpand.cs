namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using Data;

    public class UIWorkshopExpand : MonoBehaviour
    {
        private readonly static string UGUI_NAME = "UI@WorkshopExpand";

        [SerializeField]
        private UIAssetButton _expandBtn;

        public static UIWorkshopExpand Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UIWorkshopExpand>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.name = UGUI_NAME;
                return obj.AddComponent<UIWorkshopExpand>();
            }
#else
            Debug.LogWarning($"{UGUI_NAME}을 찾을 수 없습니다");
            return null;
#endif
        }


        public void Initialize()
        {
            _expandBtn.onClick.AddListener(OnExpandEvent);
        }

        public void RefreshExpand(IAssetData assetData, bool isActive)
        {
            _expandBtn.SetData(assetData);
            _expandBtn.interactable = isActive;
            _expandBtn.SetLabel(Storage.TranslateStorage.Instance.GetTranslateData("System_Tr", "Sys_Expand"));
        }

        public void CleanUp()
        {
            _expandBtn.onClick.RemoveListener(OnExpandEvent);
        }



#region ##### Listener #####


        private System.Action _expandEvent;
        public void AddOnExpandListener(System.Action act) => _expandEvent += act;
        public void RemoveOnExpandListener(System.Action act) => _expandEvent -= act;
        private void OnExpandEvent()
        {
            _expandEvent?.Invoke();
        }

#endregion
    }

}