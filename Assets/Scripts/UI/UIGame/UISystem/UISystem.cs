namespace SEF.UI.Toolkit
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Entity;
    using Data;

    public interface ISystemPanel
    {
        void Initialize();
        void CleanUp();
        void Show();
        void Hide();

        void RefreshAssetEntity(AssetEntity assetEntity);
    }

    public class UISystem : VisualElement 
    {

        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UISystem/UISystem.uxml";

        public new class UxmlFactory : UxmlFactory<UISystem, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }


        private Button _uiWorkshopButton;
        private Button _uiBlacksmithButton;
        private Button _uiVillageButton;
        private Button _uiResearchButton;

        private List<ISystemPanel> _list = new List<ISystemPanel>();

        public void Initialize()
        {


            _uiWorkshopButton = this.Q<Button>("workshop-button");
            _uiBlacksmithButton = this.Q<Button>("blacksmith-button");
            _uiVillageButton = this.Q<Button>("village-button");
            _uiResearchButton = this.Q<Button>("research-button");



            //UISystem에 등록되어있지 않으면 자동 생성 필요
            UIWorkshop uiWorkshop = this.Q<UIWorkshop>();

            Debug.Assert(uiWorkshop != null, "uiWorkshop 이 등록되지 않았습니다");

            _uiWorkshopButton.RegisterCallback<ClickEvent>(e => { OnShowPanelEvent(uiWorkshop); });



            UIBlacksmith uiBlacksmith = this.Q<UIBlacksmith>();

            //UISystem에 등록되어있지 않으면 자동 생성 필요
            Debug.Assert(uiBlacksmith != null, "uiBlacksmith 이 등록되지 않았습니다");

            _uiBlacksmithButton.RegisterCallback<ClickEvent>(e => { OnShowPanelEvent(uiBlacksmith); });

            UIVillage uiVillage = this.Q<UIVillage>();

            //UISystem에 등록되어있지 않으면 자동 생성 필요
            Debug.Assert(uiVillage != null, "uiVillage 이 등록되지 않았습니다");

            _uiVillageButton.RegisterCallback<ClickEvent>(e => { OnShowPanelEvent(uiVillage); });


            UIResearch uiResearch = this.Q<UIResearch>();

            //UISystem에 등록되어있지 않으면 자동 생성 필요
            Debug.Assert(uiResearch != null, "uiResearch 이 등록되지 않았습니다");

            _uiResearchButton.RegisterCallback<ClickEvent>(e => { OnShowPanelEvent(uiResearch); });


            _list.Add(uiWorkshop);
            _list.Add(uiBlacksmith);
            _list.Add(uiVillage);
            _list.Add(uiResearch);

            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].Initialize();
            }

            OnShowPanelEvent(uiWorkshop);
        }

        private T GetSystemPanel<T>() where T : ISystemPanel
        {
            return (T)_list.Find(panel => panel is T);
        }

        public void CleanUp()
        {

            var uiWorkshop = GetSystemPanel<UIWorkshop>();
            _uiWorkshopButton.UnregisterCallback<ClickEvent>(e => { OnShowPanelEvent(uiWorkshop); });

            var uiBlacksmith = GetSystemPanel<UIBlacksmith>();
            _uiBlacksmithButton.UnregisterCallback<ClickEvent>(e => { OnShowPanelEvent(uiBlacksmith); });

            var uiVillage = GetSystemPanel<UIVillage>();
            _uiVillageButton.UnregisterCallback<ClickEvent>(e => { OnShowPanelEvent(uiVillage); });

            var uiResearch = GetSystemPanel<UIVillage>();
            _uiResearchButton.UnregisterCallback<ClickEvent>(e => { OnShowPanelEvent(uiResearch); });

            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].CleanUp();
            }
            _list.Clear();

        }


        public void OnShowPanelEvent(ISystemPanel element)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i] == element)
                    _list[i].Show();
                else
                    _list[i].Hide();
            }
        }


        #region ##### Entity #####

        public void RefreshUnit(int index, UnitEntity entity, float nowTime)
        {
            var uiWorkshop = GetSystemPanel<UIWorkshop>();
            uiWorkshop.RefreshUnit(index, entity, nowTime);
        }     


        public void RefreshBlacksmith(int index, BlacksmithEntity entity)
        {
            var ui = GetSystemPanel<UIBlacksmith>();
            ui.RefreshBlacksmith(index, entity);
        }

        public void RefreshVillage(int index, VillageEntity entity)
        {
            var ui = GetSystemPanel<UIVillage>();
            ui.RefreshVillage(index, entity);
        }

        public void RefreshAssetEntity(AssetEntity assetEntity)
        {

            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].RefreshAssetEntity(assetEntity);
            }
        }

        #endregion


        #region ##### Listener #####
        public void AddOnWorkshopUpgradeListener(System.Action<int> act)
        {
            var ui = GetSystemPanel<UIWorkshop>();
            ui.AddUpgradeListener(act);
        }
        public void RemoveOnWorkshopUpgradeListener(System.Action<int> act)
        {
            var ui = GetSystemPanel<UIWorkshop>();
            ui.RemoveUpgradeListener(act);
        }
        public void AddUpTechListener(System.Action<int, UnitData> act)
        {
            var uiWorkshop = GetSystemPanel<UIWorkshop>();
            uiWorkshop.AddUpTechListener(act);
        }
        public void RemoveUpTechListener(System.Action<int, UnitData> act)
        {
            var uiWorkshop = GetSystemPanel<UIWorkshop>();
            uiWorkshop.RemoveUpTechListener(act);
        }
        public void AddExpendListener(System.Action act)
        {
            var uiWorkshop = GetSystemPanel<UIWorkshop>();
            uiWorkshop.AddExpendListener(act);
        }
        public void RemoveExpendListener(System.Action act)
        {
            var uiWorkshop = GetSystemPanel<UIWorkshop>();
            uiWorkshop.RemoveExpendListener(act);
        }



        public void AddOnBlacksmithUpgradeListener(System.Action<int> act)
        {
            var ui = GetSystemPanel<UIBlacksmith>();
            ui.AddUpgradeListener(act);
        }
        public void RemoveOnBlacksmithUpgradeListener(System.Action<int> act)
        {
            var ui = GetSystemPanel<UIBlacksmith>();
            ui.RemoveUpgradeListener(act);
        }

        public void AddOnVillageUpgradeListener(System.Action<int> act)
        {
            var ui = GetSystemPanel<UIVillage>();
            ui.AddUpgradeListener(act);
        }
        public void RemoveOnVillageUpgradeListener(System.Action<int> act)
        {
            var ui = GetSystemPanel<UIVillage>();
            ui.RemoveUpgradeListener(act);
        }


        #endregion
    }



#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    [RequireComponent(typeof(UIDocument))]
    public class UISystem_Test : MonoBehaviour
    {
        private UISystem _instance;
        public UISystem Instance => _instance;

        public static UISystem_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UISystem_Test";
            return obj.AddComponent<UISystem_Test>();
        }

        public void Initialize()
        {
            var root = UIUXML.GetVisualElement(gameObject, UISystem.PATH_UI_UXML);
            _instance = root.Q<UISystem>();
            _instance.Initialize();
        }

        public void Dispose()
        {
            _instance = null;
            DestroyImmediate(gameObject);
        }
    }
#endif
}