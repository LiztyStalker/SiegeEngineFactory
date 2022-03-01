namespace SEF.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using Data;
    using SEF.Unit;

    public class UIPlay : MonoBehaviour
    {        
        private UILevelWave _uiLevelWave;

        private UIHealthContainer _uiHealthContainer;

        private UIBossAlarm _uiBossAlarm;

        private UIHitContainer _uiHitContainer;
        
        public static UIPlay Create()
        {
            var obj = new GameObject();
            obj.name = "UI@Play";
            obj.AddComponent<RectTransform>();
            return obj.AddComponent<UIPlay>();
        }

        public void Initialize() {
            _uiLevelWave = GetComponentInChildren<UILevelWave>();
            _uiBossAlarm = GetComponentInChildren<UIBossAlarm>();

            _uiHealthContainer = GetComponentInChildren<UIHealthContainer>();
            _uiHitContainer = GetComponentInChildren<UIHitContainer>();

            Debug.Assert(_uiLevelWave != null, "_uiLevelWave 이 등록되지 않았습니다");
            Debug.Assert(_uiBossAlarm != null, "_uiBossAlarm 이 등록되지 않았습니다");
            Debug.Assert(_uiHealthContainer != null, "_uiHealthContainer 이 등록되지 않았습니다");
            Debug.Assert(_uiHitContainer != null, "_uiHitContainer 이 등록되지 않았습니다");

            _uiLevelWave.Initialize();
            _uiBossAlarm.Initialize();
            _uiHealthContainer.Initialize();
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
            //var root = UIUXML.GetVisualElement(gameObject, UIPlay.PATH_UI_UXML);
            //_instance = root.Q<UIPlay>();
            _instance = UIPlay.Create();
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