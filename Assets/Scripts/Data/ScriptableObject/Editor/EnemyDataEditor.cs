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

        //public override void OnInspectorGUI()
        //{
        //    base.OnInspectorGUI();

        //    //var property = serializedObject.FindProperty("_healthValue");
        //    //EditorGUILayout.PropertyField(property, true);
        //}


        private EnemyData _enemyData;
        private VisualElement _root;



        private int _nowLevel = 1;
        private int _nowWave = 1;
        private bool _isModified = false;

        private EnumField _groupField;
        private EnumField _themeField;



        private TextField _startHealthValueField;
        private TextField _increaseLevelHealthValueField;
        private FloatField _increaseLevelHealthRateField;
        private TextField _increaseWaveHealthValueField;
        private FloatField _increaseWaveHealthRateField;

        private TextField _startAttackValueField;
        private TextField _increaseAttackValueField;
        private FloatField _increaseAttackRateField;
        private IntegerField _attackCountField;
        private FloatField _attackDelayField;

        private TextField _startRewardAssetValueField;
        private TextField _increaseLevelRewardAssetValueField;
        private FloatField _increaseLevelRewardAssetRateField;
        private TextField _increaseWaveRewardAssetValueField;
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

            _startRewardAssetValueField.SetEnabled(_isModified);
            _increaseLevelRewardAssetValueField.SetEnabled(_isModified);
            _increaseLevelRewardAssetRateField.SetEnabled(_isModified);
            _increaseWaveRewardAssetValueField.SetEnabled(_isModified);
            _increaseWaveRewardAssetRateField.SetEnabled(_isModified);

            _skeletonDataAssetField.SetEnabled(_isModified);
            _spineModelKeyField.SetEnabled(_isModified);
            _spineSkinKeyField.SetEnabled(_isModified);
        }


        public override VisualElement CreateInspectorGUI()
        {

//            _iconField = _root.Query<VisualElement>("icon_field").First();
//            _iconField.style.backgroundImage = _enemyData.icon ? _enemyData.icon.texture : null;



            //_spriteField = _root.Query<ObjectField>("icon_objectfield").First();
            //_spriteField.objectType = typeof(Sprite);
            //_spriteField.value = _enemyData.icon;
            //_spriteField.RegisterCallback<ChangeEvent<Object>>(
            //    e =>
            //    {
            //        _enemyData.icon = (Sprite)e.newValue;
            //        _iconField.style.backgroundImage = _enemyData.icon.texture;
            //        EditorUtility.SetDirty(_enemyData);
            //    }
            //);

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
            _groupField.label = "그룹";
            _groupField.value = _enemyData.Group;
            _groupField.RegisterCallback<ChangeEvent<System.Enum>>(
                e =>
                {
                    _groupField.value = e.newValue;
                    EditorUtility.SetDirty(_enemyData);
                }
            );

            _themeField = _root.Query<EnumField>("theme_enumfield").First();
            _themeField.label = "테마";
            _themeField.value = _enemyData.Group;
            _themeField.RegisterCallback<ChangeEvent<System.Enum>>(
                e =>
                {
                    _themeField.value = e.newValue;
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
            totalHealthValue.value = _enemyData.StartHealthValue.GetValue();

            TextField dpsField = _root.Query<TextField>("dps_textfield").First();
            dpsField.SetEnabled(false);
            dpsField.label = "DPS";
            //            dpsField.value = _unitData.dpsValue.GetValue();






            TextField summaryHealthValueField = _root.Query<TextField>("summary_healthvalue_textfield").First();
            summaryHealthValueField.SetEnabled(false);
            summaryHealthValueField.label = "체력 요약";
            summaryHealthValueField.value = _enemyData.StartHealthValue.GetValue();

            _startHealthValueField = _root.Query<TextField>("starthealthvalue_textfield").First();
            _startHealthValueField.label = "시작체력";
            _startHealthValueField.value = _enemyData.StartHealthValue.GetValue();
            _startHealthValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _enemyData.StartHealthValue.SetValue(e.newValue);
                    EditorUtility.SetDirty(_enemyData);
                }
            );

            _increaseLevelHealthValueField = _root.Query<TextField>("increaselevelhealthvalue_textfield").First();
            _increaseLevelHealthValueField.label = "체력레벨증가량";
            _increaseLevelHealthValueField.value = _enemyData.IncreaseLevelHealthValue.GetValue();
            _increaseLevelHealthValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _enemyData.IncreaseLevelHealthValue.SetValue(e.newValue);
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

            _increaseWaveHealthValueField = _root.Query<TextField>("increasewavehealthvalue_textfield").First();
            _increaseWaveHealthValueField.label = "체력웨이브증가량";
            _increaseWaveHealthValueField.value = _enemyData.IncreaseWaveHealthValue.GetValue();
            _increaseWaveHealthValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _enemyData.IncreaseWaveHealthValue.SetValue(e.newValue);
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
            summaryAttackValueField.value = _enemyData.StartAttackValue.GetValue();


            _startAttackValueField = _root.Query<TextField>("startattackvalue_textfield").First();
            _startAttackValueField.label = "기본공격력";
            _startAttackValueField.value = _enemyData.StartAttackValue.GetValue();
            _startAttackValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _enemyData.StartAttackValue.SetValue(e.newValue);
                    EditorUtility.SetDirty(_enemyData);
                }
            );

            _increaseAttackValueField = _root.Query<TextField>("increaseattackvalue_textfield").First();
            _increaseAttackValueField.label = "공격증가량";
            _increaseAttackValueField.value = _enemyData.IncreaseAttackValue.GetValue();
            _increaseAttackValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _enemyData.IncreaseAttackValue.SetValue(e.newValue);
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






            TextField summaryRewardAssetValueField = _root.Query<TextField>("summary_rewardassetvalue_textfield").First();
            summaryRewardAssetValueField.SetEnabled(false);
            summaryRewardAssetValueField.label = "재화요약";
            summaryRewardAssetValueField.value = _enemyData.StartRewardAssetValue.GetValue();


            _startRewardAssetValueField = _root.Query<TextField>("startrewardassetvalue_textfield").First();
            _startRewardAssetValueField.label = "기본획득재화";
            _startRewardAssetValueField.value = _enemyData.StartRewardAssetValue.GetValue();
            _startRewardAssetValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _enemyData.StartRewardAssetValue.SetValue(e.newValue);
                    EditorUtility.SetDirty(_enemyData);
                }
            );

            _increaseLevelRewardAssetValueField = _root.Query<TextField>("increaselevelrewardassetvalue_textfield").First();
            _increaseLevelRewardAssetValueField.label = "레벨재화증가량";
            _increaseLevelRewardAssetValueField.value = _enemyData.IncreaseLevelRewardAssetValue.GetValue();
            _increaseLevelRewardAssetValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _enemyData.IncreaseLevelRewardAssetValue.SetValue(e.newValue);
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


            _increaseWaveRewardAssetValueField = _root.Query<TextField>("increasewaverewardassetvalue_textfield").First();
            _increaseWaveRewardAssetValueField.label = "웨이브재화증가량";
            _increaseWaveRewardAssetValueField.value = _enemyData.IncreaseWaveRewardAssetValue.GetValue();
            _increaseWaveRewardAssetValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _enemyData.IncreaseWaveRewardAssetValue.SetValue(e.newValue);
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





            
            



            _skeletonDataAssetField = _root.Query<ObjectField>("skeletondatasset_objectfield").First();
            _skeletonDataAssetField.label = "모델";
            _skeletonDataAssetField.objectType = typeof(Spine.Unity.SkeletonDataAsset);
            _skeletonDataAssetField.value = _enemyData.SkeletonDataAsset;
            _skeletonDataAssetField.RegisterCallback<ChangeEvent<Object>>(
                e =>
                {
                    _enemyData.SkeletonDataAsset = (Spine.Unity.SkeletonDataAsset)e.newValue;
                    // Set StarSystem as being dirty. This tells the editor that there have been changes made to the asset and that it requires a save. 
                    EditorUtility.SetDirty(_enemyData);
                }
            );

            _spineModelKeyField = _root.Query<TextField>("spinemodelkey_textfield").First();
            _spineModelKeyField.label = "모델키";
            _spineModelKeyField.value = _enemyData.SpineModelKey;
            _spineModelKeyField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _enemyData.SpineModelKey = e.newValue;
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






            //< ui:VisualElement name = "icon_background" style = "width: 116px;" />

            //           < ui:VisualElement name = "icon_field" style = "width: 116px;" />

            //      < uie:ObjectField allow-scene - objects = "false" name = "icon_objectfield" class="icon_object_field" />
            //< ui:Toggle label = "Toggle" name = "modified_toggle" />

            //       < ui:TextField picking-mode = "Ignore" label = "Text Field" value = "filler text" text = "filler text" name = "name_textfield" />

            //                < uie:EnumField label = "Enum" value = "Center" name = "group_enumfield" />

            //                     < uie:IntegerField label = "Int Field" value = "42" name = "upgrade_intfield" />

            //                          < ui:TextField picking-mode = "Ignore" label = "Text Field" value = "filler text" text = "filler text" name = "health_textfield" />

            //                                   < ui:TextField picking-mode = "Ignore" label = "Text Field" value = "filler text" text = "filler text" name = "dps_textfield" />

            //                                            < ui:TextField picking-mode = "Ignore" label = "Text Field" value = "filler text" text = "filler text" name = "next_upgrade_textfield" />

            //                                                     < uie:FloatField label = "Float Field" value = "42.2" name = "product_floatfield" />



            //    #region Fields
            //    // Find the visual element with the name "systemSprite" and make it display the star system sprite if it has one.
            //    VisualElement systemSprite = rootElement.Query<VisualElement>("systemSprite").First();
            //    systemSprite.style.backgroundImage = starSystem.sprite ? starSystem.sprite.texture : null;

            //    // Find an object field with the name "systemSpriteField", set that it only accepts objects of type Sprite,
            //    // set its initial value and register a callback that will occur if the value of the filed changes.
            //    ObjectField spriteField = rootElement.Query<ObjectField>("systemSpriteField").First();
            //    spriteField.objectType = typeof(Sprite);
            //    spriteField.value = starSystem.sprite;
            //    spriteField.RegisterCallback<ChangeEvent<Object>>(
            //        e =>
            //        {
            //            starSystem.sprite = (Sprite)e.newValue;
            //            systemSprite.style.backgroundImage = starSystem.sprite.texture;
            //        // Set StarSystem as being dirty. This tells the editor that there have been changes made to the asset and that it requires a save. 
            //        EditorUtility.SetDirty(starSystem);
            //        }
            //    );

            //    FloatField scaleField = rootElement.Query<FloatField>("starScale").First();
            //    scaleField.value = starSystem.scale;
            //    scaleField.RegisterCallback<ChangeEvent<float>>(
            //        e => {
            //            starSystem.scale = e.newValue;
            //            EditorUtility.SetDirty(starSystem);
            //        }
            //    );
            //    #endregion

            //    #region Display Planet Data 
            //    // Store visual element that will contain the planet sub-inspectors.  
            //    planetList = rootElement.Query<VisualElement>("planetList").First();
            //    UpdatePlanets();
            //    #endregion

            //    #region Buttons
            //    // Assign methods to the click events of the two buttons.
            //    Button btnAddPlanet = rootElement.Query<Button>("btnAddNew").First();
            //    btnAddPlanet.clickable.clicked += AddPlanet;

            //    Button btnRemoveAllPlanets = rootElement.Query<Button>("btnRemoveAll").First();
            //    btnRemoveAllPlanets.clickable.clicked += RemoveAll;
            //    #endregion

            return _root;
        }
    }
}
#endif