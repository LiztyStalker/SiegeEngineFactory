namespace SEF.UI
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using Entity;
    using Data;

    public interface ISystemPanel
    {
        void Initialize();
        void CleanUp();
        void Show();
        void Hide();
        void RefreshAssetEntity(AssetPackage assetEntity);
    }

    public class UISystem : MonoBehaviour
    {
        private readonly static string UGUI_NAME = "UI@System";

        [SerializeField]
        private Transform _systemLayout;
        [SerializeField]
        private Button _uiWorkshopButton;
        [SerializeField]
        private Button _uiSmithyButton;
        [SerializeField]
        private Button _uiVillageButton;
        [SerializeField]
        private Button _uiMineButton;
//        [SerializeField]
//        private Button _uiResearchButton;

        private List<ISystemPanel> _list = new List<ISystemPanel>();


        public static UISystem Create()
        {
            var ui = Storage.DataStorage.Instance.GetDataOrNull<GameObject>(UGUI_NAME, null, null);
            if (ui != null)
            {
                return Instantiate(ui.GetComponent<UISystem>());
            }
#if UNITY_EDITOR
            else
            {
                var obj = new GameObject();
                obj.name = UGUI_NAME;
                return obj.AddComponent<UISystem>();
            }
#else
            Debug.LogWarning($"{UGUI_NAME}을 찾을 수 없습니다");
            return null;
#endif
        }


        public void Initialize()
        {
            var uiWorkshop = GetComponentInChildren<UIWorkshop>(true);
            if(uiWorkshop == null) uiWorkshop = UIWorkshop.Create();
            Debug.Assert(uiWorkshop != null, "uiWorkshop 이 등록되지 않았습니다");
            _uiWorkshopButton.onClick.AddListener(() => { OnShowPanelEvent(uiWorkshop); });



            var uiSmithy = GetComponentInChildren<UISmithy>(true);
            if (uiSmithy == null) uiSmithy = UISmithy.Create();
            Debug.Assert(uiSmithy != null, "uiSmithy 이 등록되지 않았습니다");
            _uiSmithyButton.onClick.AddListener(() => { OnShowPanelEvent(uiSmithy); });



            var uiVillage = GetComponentInChildren<UIVillage>(true);
            if (uiVillage == null) uiVillage = UIVillage.Create();
            Debug.Assert(uiVillage != null, "uiVillage 이 등록되지 않았습니다");
            _uiVillageButton.onClick.AddListener(() => { OnShowPanelEvent(uiVillage); });


            var uiMine = GetComponentInChildren<UIMine>(true);
            if (uiMine == null) uiMine = UIMine.Create();
            Debug.Assert(uiMine != null, "uiMine 이 등록되지 않았습니다");
            _uiMineButton.onClick.AddListener(() => { OnShowPanelEvent(uiMine); });


            //var uiResearch = UIResearch.Create();
            //UISystem에 등록되어있지 않으면 자동 생성 필요
            //Debug.Assert(uiResearch != null, "uiResearch 이 등록되지 않았습니다");

            //_uiResearchButton.onClick.AddListener(() => { OnShowPanelEvent(uiResearch); });
            //_uiResearchButton.RegisterCallback<ClickEvent>(e => { OnShowPanelEvent(uiResearch); });


            _list.Add(uiWorkshop);
            _list.Add(uiSmithy);
            _list.Add(uiVillage);
            _list.Add(uiMine);
            //_list.Add(uiResearch);

            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].Initialize();
            }

            OnShowPanelEvent(uiWorkshop);
        }

        public KeyValuePair<string, System.Action>[] GetAddresses()
        {
            List<KeyValuePair<string, System.Action>> list = new List<KeyValuePair<string, System.Action>>();
            list.Add(new KeyValuePair<string, System.Action>(typeof(UIWorkshop).Name, () => OnShowPanelEvent(GetSystemPanel<UIWorkshop>())));
            list.Add(new KeyValuePair<string, System.Action>(typeof(UISmithy).Name, () => OnShowPanelEvent(GetSystemPanel<UISmithy>())));
            //list.Add(new KeyValuePair<string, System.Action>(typeof(UIResearch).Name, () => OnShowPanelEvent(GetSystemPanel<UIResearch>())));
            list.Add(new KeyValuePair<string, System.Action>(typeof(UIVillage).Name, () => OnShowPanelEvent(GetSystemPanel<UIVillage>())));
            list.Add(new KeyValuePair<string, System.Action>(typeof(UIMine).Name, () => OnShowPanelEvent(GetSystemPanel<UIMine>())));
            return list.ToArray();
        }


        private T GetSystemPanel<T>() where T : ISystemPanel
        {
            return (T)_list.Find(panel => panel is T);
        }

        public void CleanUp()
        {

            var uiWorkshop = GetSystemPanel<UIWorkshop>();
            _uiWorkshopButton.onClick.AddListener(() => { OnShowPanelEvent(uiWorkshop); });

            var uiSmithy = GetSystemPanel<UISmithy>();
            _uiSmithyButton.onClick.AddListener(() => { OnShowPanelEvent(uiSmithy); });

            var uiVillage = GetSystemPanel<UIVillage>();
            _uiVillageButton.onClick.AddListener(() => { OnShowPanelEvent(uiVillage); });

            var uiMine = GetSystemPanel<UIMine>();
            _uiMineButton.onClick.AddListener(() => { OnShowPanelEvent(uiMine); });

            //var uiResearch = GetSystemPanel<UIResearch>();
            //_uiResearchButton.onClick.AddListener(() => { OnShowPanelEvent(uiResearch); });

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


        public void RefreshBlacksmith(int index, SmithyEntity entity)
        {
            var ui = GetSystemPanel<UISmithy>();
            ui.RefreshSmithy(index, entity);
        }

        public void RefreshVillage(int index, VillageEntity entity)
        {
            var ui = GetSystemPanel<UIVillage>();
            ui.RefreshVillage(index, entity);
        }

        public void RefreshMine(int index, MineEntity entity)
        {
            var ui = GetSystemPanel<UIMine>();
            ui.RefreshMine(index, entity);
        }

        public void RefreshAssetEntity(AssetPackage assetEntity)
        {

            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].RefreshAssetEntity(assetEntity);
            }
        }

        public void RefreshExpend(IAssetData assetData, bool isActive)
        {
            var uiWorkshop = GetSystemPanel<UIWorkshop>();
            uiWorkshop.RefreshExpend(assetData, isActive);
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
        public void AddUpTechListener(System.Action<int, UnitTechData> act)
        {
            var uiWorkshop = GetSystemPanel<UIWorkshop>();
            uiWorkshop.AddUpTechListener(act);
        }
        public void RemoveUpTechListener(System.Action<int, UnitTechData> act)
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



        public void AddOnSmithyUpgradeListener(System.Action<int> act)
        {
            var ui = GetSystemPanel<UISmithy>();
            ui.AddUpgradeListener(act);
        }
        public void RemoveOnSmithyUpgradeListener(System.Action<int> act)
        {
            var ui = GetSystemPanel<UISmithy>();
            ui.RemoveUpgradeListener(act);
        }

        public void AddOnSmithyUpTechListener(System.Action<int> act)
        {
            var ui = GetSystemPanel<UISmithy>();
            ui.AddOnUpTechListener(act);
        }
        public void RemoveOnSmithyUpTechListener(System.Action<int> act)
        {
            var ui = GetSystemPanel<UISmithy>();
            ui.RemoveOnUpTechListener(act);
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


        public void AddOnVillageUpTechListener(System.Action<int> act)
        {
            var ui = GetSystemPanel<UIVillage>();
            ui.AddOnUpTechListener(act);
        }

        public void RemoveOnVillageUpTechListener(System.Action<int> act)
        {
            var ui = GetSystemPanel<UIVillage>();
            ui.RemoveOnUpTechListener(act);
        }


        public void AddOnMineUpgradeListener(System.Action<int> act)
        {
            var ui = GetSystemPanel<UIMine>();
            ui.AddUpgradeListener(act);
        }
        public void RemoveOnMineUpgradeListener(System.Action<int> act)
        {
            var ui = GetSystemPanel<UIMine>();
            ui.RemoveUpgradeListener(act);
        }


#endregion
    }



#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
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
            _instance = UISystem.Create();
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