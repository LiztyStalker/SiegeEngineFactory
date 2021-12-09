namespace SEF.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Toolkit;

    public class UICommon : MonoBehaviour
    {

        private UIPopup _uiPopup;


        public void Initialize()
        {
            _uiPopup = GetComponentInChildren<UIPopup>(true);
            _uiPopup.Initialize();
        }

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            _uiPopup.ShowPopup("UIPopup Apply Test", "»Æ¿Œ", delegate
            {
                Debug.Log("Apply Callback");
            });
        }

        public void CleanUp()
        {
            _uiPopup.CleanUp();
        }

    }
}