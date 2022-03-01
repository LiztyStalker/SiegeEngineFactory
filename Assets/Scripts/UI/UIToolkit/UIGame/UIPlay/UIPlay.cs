namespace SEF.UI.Toolkit
{
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;
    using Data;
    using SEF.Unit;

    public class UIPlay : VisualElement
    {
        internal static readonly string PATH_UI_UXML = "Assets/Scripts/UI/UIGame/UIPlay/UIPlay.uxml";

        public new class UxmlFactory : UxmlFactory<UIPlay, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }


        private UILevelWave _uiLevelWave;

        private UIHealthContainer _uiHealthContainer;

        private UIBossAlarm _uiBossAlarm;

        private UIHitContainer _uiHitContainer;
        
        public void Initialize(Transform parent) {
            _uiLevelWave = this.Query<UILevelWave>().First();
            _uiBossAlarm = this.Query<UIBossAlarm>().First();

            _uiHealthContainer = UIHealthContainer.Create(parent);
            _uiHitContainer = UIHitContainer.Create(parent);

            _uiLevelWave.Initialize();
            _uiBossAlarm.Initialize();
            _uiHealthContainer.Initialize(this);
            _uiHitContainer.Initialize();
        }

        public void CleanUp() 
        {
            _uiLevelWave.CleanUp();
            _uiBossAlarm.CleanUp();
            _uiHealthContainer.CleanUp();
            _uiHitContainer.CleanUp();
        }

        public void ShowHit(PlayActor playActor, DamageData data)
        {
            _uiHitContainer.ShowHit(data.GetValue(), playActor.transform.position);

            if (playActor is UnitActor)
                _uiHealthContainer.ShowUnitHealthData(playActor.NowHealthRate(), playActor.transform.position);
            else if (playActor is EnemyActor)
                _uiHealthContainer.ShowEnemyHealthData(((EnemyActor)playActor).NowHealthValue(), playActor.NowHealthRate());
        }

        
        //EnemyEntity, float, LevelWaveData
        public void RefreshNextEnemyUnit(EnemyActor enemyActor, LevelWaveData levelWaveData)
        {
            _uiHealthContainer.ShowEnemyHealthData(enemyActor.NowHealthValue(), enemyActor.NowHealthRate());
            _uiLevelWave.ShowLevelWave(levelWaveData.GetLevel(), levelWaveData.GetWave());

            TYPE_ENEMY_GROUP typeEnemyGroup = TYPE_ENEMY_GROUP.Normal;
            if (levelWaveData.IsBoss())
                typeEnemyGroup = TYPE_ENEMY_GROUP.Boss;
            else if (levelWaveData.IsThemeBoss())
                typeEnemyGroup = TYPE_ENEMY_GROUP.ThemeBoss;
            _uiBossAlarm.ShowAlarm(typeEnemyGroup);
        }

    }



#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    [RequireComponent(typeof(UIDocument))]
    public class UIPlay_Test : MonoBehaviour
    {
        private UIPlay _instance;
        public UIPlay Instance => _instance;

        public static UIPlay_Test Create()
        {
            var obj = new GameObject();
            obj.name = "UIPlay_Test";
            return obj.AddComponent<UIPlay_Test>();
        }

        public void Initialize()
        {
            var root = UIUXML.GetVisualElement(gameObject, UIPlay.PATH_UI_UXML);
            _instance = root.Q<UIPlay>();
            _instance.Initialize(transform);
        }

        public void Dispose()
        {
            _instance = null;
            DestroyImmediate(gameObject);
        }
    }
#endif
}