#if UNITY_EDITOR
namespace SEF.Data.Editor
{
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;
    using Data;

    [CustomEditor(typeof(EnemyData))]
    public class EnemyDataEditor : Editor
    {

        private EnemyData _enemyData;
        private VisualElement _root;


        private LevelWaveData _levelWaveData;

        private int _nowLevel = 1;
        private int _nowWave = 1;
        private bool _isModified = false;

        private EnumField _groupField;
        private EnumField _themeField;
        private FloatField _scaleField;

        private TextField _startHealthValueField;
        private IntegerField _increaseLevelHealthValueField;
        private FloatField _increaseLevelHealthRateField;
        private IntegerField _increaseWaveHealthValueField;
        private FloatField _increaseWaveHealthRateField;

        private VisualElement _attackPanel;
        private TextField _startAttackValueField;
        private IntegerField _increaseAttackValueField;
        private FloatField _increaseAttackRateField;
        private IntegerField _attackCountField;
        private FloatField _attackDelayField;
        private ObjectField _bulletField;
        private FloatField _bulletScaleField;

        private VisualElement _attackerLayout;
        private Button _attackerAddButton;

        private TextField _startRewardAssetValueField;
        private IntegerField _increaseLevelRewardAssetValueField;
        private FloatField _increaseLevelRewardAssetRateField;
        private IntegerField _increaseWaveRewardAssetValueField;
        private FloatField _increaseWaveRewardAssetRateField;

        private ObjectField _skeletonDataAssetField;
        private TextField _spineModelKeyField;
        private TextField _spineSkinKeyField;

        public void OnEnable()
        {
            _enemyData = target as EnemyData;
            _root = new VisualElement();

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Data/ScriptableObject/Editor/EnemyDataEditor.uxml");
            visualTree.CloneTree(_root);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Data/ScriptableObject/Editor/EnemyDataEditor.uss");
            _root.styleSheets.Add(styleSheet);

        }



        public void UpdateFields()
        {
            _groupField.SetEnabled(_isModified);
            _themeField.SetEnabled(_isModified);
            _scaleField.SetEnabled(_isModified);

            _startHealthValueField.SetEnabled(_isModified);
            _increaseLevelHealthValueField.SetEnabled(_isModified);
            _increaseLevelHealthRateField.SetEnabled(_isModified);
            _increaseWaveHealthValueField.SetEnabled(_isModified);
            _increaseWaveHealthRateField.SetEnabled(_isModified);


            _startAttackValueField.SetEnabled(_isModified);
            _increaseAttackValueField.SetEnabled(_isModified);
            _increaseAttackRateField.SetEnabled(_isModified);
            _attackCountField.SetEnabled(_isModified);
            _attackDelayField.SetEnabled(_isModified);
            _bulletField.SetEnabled(_isModified);
            _bulletScaleField.SetEnabled(_isModified);

            _startRewardAssetValueField.SetEnabled(_isModified);
            _increaseLevelRewardAssetValueField.SetEnabled(_isModified);
            _increaseLevelRewardAssetRateField.SetEnabled(_isModified);
            _increaseWaveRewardAssetValueField.SetEnabled(_isModified);
            _increaseWaveRewardAssetRateField.SetEnabled(_isModified);

            _skeletonDataAssetField.SetEnabled(_isModified);
            _spineModelKeyField.SetEnabled(false);
            _spineSkinKeyField.SetEnabled(_isModified);

            
        }


        public override VisualElement CreateInspectorGUI()
        {

            Toggle modifiedToggle = _root.Query<Toggle>("modified_toggle").First();
            modifiedToggle.label = "수정";
            modifiedToggle.value = _isModified;
            modifiedToggle.RegisterCallback<ChangeEvent<bool>>(
                e =>
                {
                    modifiedToggle.value = e.newValue;
                    _isModified = modifiedToggle.value;
                    UpdateFields();
                }
            );


            TextField keyField = _root.Query<TextField>("name_textfield").First();
            keyField.label = "키";
            keyField.value = _enemyData.Key;
            keyField.SetEnabled(false);
            keyField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _enemyData.name = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                }
            );

            _groupField = _root.Query<EnumField>("group_enumfield").First();
            _groupField.BindProperty(serializedObject.FindProperty("_group"));
            _groupField.label = "타입";
            _groupField.value = _enemyData.Group;
            _groupField.RegisterCallback<ChangeEvent<System.Enum>>(
                e =>
                {
                    _groupField.value = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                }
            );

            _themeField = _root.Query<EnumField>("theme_enumfield").First();
            _themeField.BindProperty(serializedObject.FindProperty("_typeLevelTheme"));
            _themeField.label = "테마";
            _themeField.value = _enemyData.Group;
            _themeField.RegisterCallback<ChangeEvent<System.Enum>>(
                e =>
                {
                    _themeField.value = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                }
            );


            _scaleField = _root.Query<FloatField>("scale-floatfield").First();
            _scaleField.label = "크기";
            _scaleField.value = _enemyData.Scale;
            _scaleField.RegisterCallback<ChangeEvent<float>>(
                e =>
                {
                    _enemyData.Scale = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                }
            );

            IntegerField nowLevelField = _root.Query<IntegerField>("level_intfield").First();
            nowLevelField.label = "레벨";
            nowLevelField.value = _nowLevel;
            nowLevelField.RegisterCallback<ChangeEvent<int>>(
                e =>
                {
                    _nowLevel = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                    //Dps, nextUpgrade, health 변경됨
                }
            );

            Slider nowWaveField = _root.Query<Slider>("wave_sliderfield").First();
            nowWaveField.label = "웨이브";
            nowWaveField.value = _nowWave;
            nowWaveField.RegisterCallback<ChangeEvent<int>>(
                e =>
                {
                    _nowWave = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                    //Dps, nextUpgrade, health 변경됨
                }
            );


            TextField totalHealthValue = _root.Query<TextField>("health_textfield").First();
            totalHealthValue.SetEnabled(false);
            totalHealthValue.label = "총체력";
            totalHealthValue.value = "";// _enemyData.StartHealthValue.;

            TextField dpsField = _root.Query<TextField>("dps_textfield").First();
            dpsField.SetEnabled(false);
            dpsField.label = "DPS";
            //            dpsField.value = _unitData.dpsValue.GetValue();






            TextField summaryHealthValueField = _root.Query<TextField>("summary_healthvalue_textfield").First();
            summaryHealthValueField.SetEnabled(false);
            summaryHealthValueField.label = "체력 요약";
            summaryHealthValueField.value = "";// _enemyData.StartHealthValue.GetValue();

            _startHealthValueField = _root.Query<TextField>("starthealthvalue_textfield").First();
            _startHealthValueField.label = "시작체력";
            _startHealthValueField.value = _enemyData.StartHealthValue.ValueText;
            _startHealthValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _enemyData.StartHealthValue.ValueText = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                    Debug.Log(_enemyData.StartHealthValue.GetValue());
                }
            );

            _increaseLevelHealthValueField = _root.Query<IntegerField>("increaselevelhealthvalue_textfield").First();
            _increaseLevelHealthValueField.label = "체력레벨증가량";
            _increaseLevelHealthValueField.value = _enemyData.IncreaseLevelHealthValue;
            _increaseLevelHealthValueField.RegisterCallback<ChangeEvent<int>>(
                e =>
                {
                    _enemyData.IncreaseLevelHealthValue = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                }
            );

            _increaseLevelHealthRateField = _root.Query<FloatField>("increaselevelhealthrate_textfield").First();
            _increaseLevelHealthRateField.label = "체력레벨증가비";
            _increaseLevelHealthRateField.value = _enemyData.IncreaseLevelHealthRate;
            _increaseLevelHealthRateField.RegisterCallback<ChangeEvent<float>>(
                e =>
                {
                    _enemyData.IncreaseLevelHealthRate = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                }
            );

            _increaseWaveHealthValueField = _root.Query<IntegerField>("increasewavehealthvalue_textfield").First();
            _increaseWaveHealthValueField.label = "체력웨이브증가량";
            _increaseWaveHealthValueField.value = _enemyData.IncreaseWaveHealthValue;
            _increaseWaveHealthValueField.RegisterCallback<ChangeEvent<int>>(
                e =>
                {
                    _enemyData.IncreaseWaveHealthValue = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                }
            );

            _increaseWaveHealthRateField = _root.Query<FloatField>("increasewavehealthrate_textfield").First();
            _increaseWaveHealthRateField.label = "체력웨이브증가비";
            _increaseWaveHealthRateField.value = _enemyData.IncreaseWaveHealthRate;
            _increaseWaveHealthRateField.RegisterCallback<ChangeEvent<float>>(
                e =>
                {
                    _enemyData.IncreaseWaveHealthRate = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                }
            );


            TextField summaryAttackValueField = _root.Query<TextField>("summary_attackvalue_textfield").First();
            summaryAttackValueField.SetEnabled(false);
            summaryAttackValueField.label = "공격 요약";
            summaryAttackValueField.value = _enemyData.StartAttackValue.ValueText;


            _startAttackValueField = _root.Query<TextField>("startattackvalue_textfield").First();
            _startAttackValueField.label = "기본공격력";
            _startAttackValueField.value = _enemyData.StartAttackValue.ValueText;
            _startAttackValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _enemyData.StartAttackValue.ValueText = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                }
            );

            _increaseAttackValueField = _root.Query<IntegerField>("increaseattackvalue_textfield").First();
            _increaseAttackValueField.label = "공격증가량";
            _increaseAttackValueField.value = _enemyData.IncreaseAttackValue;
            _increaseAttackValueField.RegisterCallback<ChangeEvent<int>>(
                e =>
                {
                    _enemyData.IncreaseAttackValue = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                }
            );

            _increaseAttackRateField = _root.Query<FloatField>("increaseattackrate_textfield").First();
            _increaseAttackRateField.label = "공격증가비";
            _increaseAttackRateField.value = _enemyData.IncreaseAttackRate;
            _increaseAttackRateField.RegisterCallback<ChangeEvent<float>>(
                e =>
                {
                    _enemyData.IncreaseAttackRate = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                }
            );


            _attackCountField = _root.Query<IntegerField>("attackcount_intfield").First();
            _attackCountField.label = "공격횟수";
            _attackCountField.value = _enemyData.AttackCount;
            _attackCountField.RegisterCallback<ChangeEvent<int>>(
                e =>
                {
                    _enemyData.AttackCount = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                }
            );

            _attackDelayField = _root.Query<FloatField>("attackdelay_floatfield").First();
            _attackDelayField.label = "공격딜레이";
            _attackDelayField.value = _enemyData.AttackDelay;
            _attackDelayField.RegisterCallback<ChangeEvent<float>>(
                e =>
                {
                    _enemyData.AttackDelay = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                }
            );

            _bulletField = _root.Query<ObjectField>("bullet-objectfield").First();
            _bulletField.objectType = typeof(UtilityManager.BulletData);
            _bulletField.label = "탄환크기";
            _bulletField.value = _enemyData.AttackBulletData;
            _bulletField.RegisterCallback<ChangeEvent<Object>>(
                e =>
                {
                    _enemyData.AttackBulletData = e.newValue as UtilityManager.BulletData;
                    EditorUtility.SetDirty(_enemyData);
                }
            );


            _bulletScaleField = _root.Query<FloatField>("bullet-scale-floatfield").First();
            _bulletScaleField.label = "탄환크기";
            _bulletScaleField.value = _enemyData.BulletScale;
            _bulletScaleField.RegisterCallback<ChangeEvent<float>>(
                e =>
                {
                    _enemyData.BulletScale = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                }
            );



            _attackerAddButton = _root.Query<Button>("attacker-add-button").First();
            _attackerAddButton.clicked += AddAttackerData;
            //Button attackerRemoveButton = _root.Query<Button>("attacker-add-button").First();
            //attackerRemoveButton.clicked += RemoveAttackerData;

            _attackerLayout = _root.Query<VisualElement>("attacker-layout").First();
            UpdateAttackerData(_attackerLayout, _enemyData.AttackerDataArray);








            TextField summaryRewardAssetValueField = _root.Query<TextField>("summary_rewardassetvalue_textfield").First();
            summaryRewardAssetValueField.SetEnabled(false);
            summaryRewardAssetValueField.label = "재화요약";
            summaryRewardAssetValueField.value = _enemyData.StartRewardAssetValue.ValueText;


            _startRewardAssetValueField = _root.Query<TextField>("startrewardassetvalue_textfield").First();
            _startRewardAssetValueField.label = "기본획득재화";
            _startRewardAssetValueField.value = _enemyData.StartRewardAssetValue.ValueText;
            _startRewardAssetValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _enemyData.StartRewardAssetValue.ValueText = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                }
            );

            _increaseLevelRewardAssetValueField = _root.Query<IntegerField>("increaselevelrewardassetvalue_textfield").First();
            _increaseLevelRewardAssetValueField.label = "레벨재화증가량";
            _increaseLevelRewardAssetValueField.value = _enemyData.IncreaseLevelRewardAssetValue;
            _increaseLevelRewardAssetValueField.RegisterCallback<ChangeEvent<int>>(
                e =>
                {
                    _enemyData.IncreaseLevelRewardAssetValue = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                }
            );

            _increaseLevelRewardAssetRateField = _root.Query<FloatField>("increaselevelrewardassetrate_floatfield").First();
            _increaseLevelRewardAssetRateField.label = "레벨재화증가비";
            _increaseLevelRewardAssetRateField.value = _enemyData.IncreaseLevelRewardAssetRate;
            _increaseLevelRewardAssetRateField.RegisterCallback<ChangeEvent<float>>(
                e =>
                {
                    _enemyData.IncreaseLevelRewardAssetRate = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                }
            );


            _increaseWaveRewardAssetValueField = _root.Query<IntegerField>("increasewaverewardassetvalue_textfield").First();
            _increaseWaveRewardAssetValueField.label = "웨이브재화증가량";
            _increaseWaveRewardAssetValueField.value = _enemyData.IncreaseWaveRewardAssetValue;
            _increaseWaveRewardAssetValueField.RegisterCallback<ChangeEvent<int>>(
                e =>
                {
                    _enemyData.IncreaseWaveRewardAssetValue = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                }
            );

            _increaseWaveRewardAssetRateField = _root.Query<FloatField>("increasewaverewardassetrate_floatfield").First();
            _increaseWaveRewardAssetRateField.label = "웨이브재화증가비";
            _increaseWaveRewardAssetRateField.value = _enemyData.IncreaseWaveRewardAssetRate;
            _increaseWaveRewardAssetRateField.RegisterCallback<ChangeEvent<float>>(
                e =>
                {
                    _enemyData.IncreaseWaveRewardAssetRate = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                }
            );




            _spineModelKeyField = _root.Query<TextField>("spinemodelkey_textfield").First();
            _spineModelKeyField.label = "모델키";
            _spineModelKeyField.value = _enemyData.SpineModelKey;



            _skeletonDataAssetField = _root.Query<ObjectField>("skeletondatasset_objectfield").First();
            _skeletonDataAssetField.label = "모델";
            _skeletonDataAssetField.objectType = typeof(Spine.Unity.SkeletonDataAsset);
            _skeletonDataAssetField.value = _enemyData.SkeletonDataAsset;
            _skeletonDataAssetField.RegisterCallback<ChangeEvent<Object>>(
                e =>
                {
                    _enemyData.SkeletonDataAsset = (Spine.Unity.SkeletonDataAsset)e.newValue;
                    _spineModelKeyField.value = _enemyData.SpineModelKey;
                    EditorUtility.SetDirty(_enemyData);
                }
            );

            _spineSkinKeyField = _root.Query<TextField>("spineskinkey_textfield").First();
            _spineSkinKeyField.label = "스킨";
            _spineSkinKeyField.value = _enemyData.SpineSkinKey;
            _spineSkinKeyField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _enemyData.SpineSkinKey = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                }
            );
             
            UpdateFields();

            return _root;
        }

        private void OnDisable()
        {
            _attackerAddButton.clicked -= AddAttackerData;
        }

        private void UpdateAttackerData(VisualElement layout, AttackerData[] datas)
        {
            layout.Clear();
            for(int i = 0; i < datas.Length; i++)
            {
                var attackerDataEditor = new AttackerDataEditor(datas[i]);
                attackerDataEditor.SetOnRemoveListener(RemoveAttackerData);
                layout.Add(attackerDataEditor);
            }
        }

        private void AddAttackerData() {
            var attackerData = AttackerData.Create_Test();
            _enemyData.AddAttackerData(attackerData);
            UpdateAttackerData(_attackerLayout, _enemyData.AttackerDataArray);
        }
        private void RemoveAttackerData(AttackerData attackerData) {
            _enemyData.RemoveAttackerData(attackerData);
            UpdateAttackerData(_attackerLayout, _enemyData.AttackerDataArray);
        }
    }
}
#endif