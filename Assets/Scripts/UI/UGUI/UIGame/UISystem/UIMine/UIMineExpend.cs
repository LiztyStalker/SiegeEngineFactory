namespace SEF.UI
{
    using UnityEngine;
    using SEF.Data;

    public class UIMineExpend : MonoBehaviour
    {
        private readonly static string UGUI_NAME = "UI@MineExpend";

        [SerializeField]
        private UIAssetButton _expendBtn;

        public static UIMineExpend Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UIMineExpend>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.name = UGUI_NAME;
                return obj.AddComponent<UIMineExpend>();
            }
#else
            Debug.LogWarning($"{UGUI_NAME}�� ã�� �� �����ϴ�");
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
            _expendBtn.SetLabel("Ȯ��");
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