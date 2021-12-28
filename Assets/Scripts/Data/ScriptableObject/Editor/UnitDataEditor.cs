#if UNITY_EDITOR
namespace SEF.Data.Editor
{
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;
    using Data;

    [CustomEditor(typeof(UnitData))]
    public class UnitDataEditor : Editor
    {

        //public override void OnInspectorGUI()
        //{
        //    base.OnInspectorGUI();

        //    //var property = serializedObject.FindProperty("_healthValue");
        //    //EditorGUILayout.PropertyField(property, true);
        //}


        private UnitData _unitData;
        private VisualElement _root;

        private int _nowUpgrade = 1;
        private bool _isModified = false;

        private VisualElement _iconField;
        private ObjectField _spriteField;

        private EnumField _groupField;

        private FloatField _productField;



        private TextField _startHealthValueField;
        private TextField _increaseHealthValueField;
        private FloatField _increaseHealthRateField;

        private TextField _startAttackValueField;
        private TextField _increaseAttackValueField;
        private FloatField _increaseAttackRateField;
        private IntegerField _attackCountField;
        private EnumField _typeAttackRangeField;
        private EnumField _typeAttackActionField;
        private FloatField _attackDelayField;

        private TextField _startUpgradeValueField;
        private TextField _increaseUpgradeValueField;
        private FloatField _increaseUpgradeRateField;
        private IntegerField _maximumUpgradeValueField;

        private ObjectField _skeletonDataAssetField;
        private TextField _spineModelKeyField;
        private TextField _spineSkinKeyField;

        public void OnEnable()
        {
            _unitData = target as UnitData;
            _root = new VisualElement();

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Data/ScriptableObject/Editor/UnitDataEditor.uxml");
            visualTree.CloneTree(_root);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Data/ScriptableObject/Editor/UnitDataEditor.uss");
            _root.styleSheets.Add(styleSheet);

        }



        public void UpdateFields()
        {
            _spriteField.SetEnabled(_isModified);
            _groupField.SetEnabled(_isModified);
            _productField.SetEnabled(_isModified);

            _startHealthValueField.SetEnabled(_isModified);
            _increaseHealthValueField.SetEnabled(_isModified);
            _increaseHealthRateField.SetEnabled(_isModified);


            _startAttackValueField.SetEnabled(_isModified);
            _increaseAttackValueField.SetEnabled(_isModified);
            _increaseAttackRateField.SetEnabled(_isModified);
            _attackCountField.SetEnabled(_isModified);
            _typeAttackRangeField.SetEnabled(_isModified);
            _typeAttackActionField.SetEnabled(false);
            _attackDelayField.SetEnabled(_isModified);

            _startUpgradeValueField.SetEnabled(_isModified);
            _increaseUpgradeValueField.SetEnabled(_isModified);
            _increaseUpgradeRateField.SetEnabled(_isModified);
            _maximumUpgradeValueField.SetEnabled(_isModified);

            _skeletonDataAssetField.SetEnabled(_isModified);
            _spineModelKeyField.SetEnabled(_isModified);
            _spineSkinKeyField.SetEnabled(_isModified);
        }


        public override VisualElement CreateInspectorGUI()
        {

            _iconField = _root.Query<VisualElement>("icon_field").First();
            _iconField.style.backgroundImage = _unitData.icon ? _unitData.icon.texture : null;



            _spriteField = _root.Query<ObjectField>("icon_objectfield").First();
            _spriteField.objectType = typeof(Sprite);
            _spriteField.value = _unitData.icon;
            _spriteField.RegisterCallback<ChangeEvent<Object>>(
                e =>
                {
                    _unitData.icon = (Sprite)e.newValue;
                    _iconField.style.backgroundImage = _unitData.icon.texture;
                    EditorUtility.SetDirty(_unitData);
                }
            );

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
            keyField.value = _unitData.Key;
            keyField.SetEnabled(false);
            keyField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _unitData.name = e.newValue;
                    EditorUtility.SetDirty(_unitData);
                }
            );

            _groupField = _root.Query<EnumField>("group_enumfield").First();
            _groupField.label = "그룹";
            _groupField.value = _unitData.Group;
            _groupField.RegisterCallback<ChangeEvent<System.Enum>>(
                e =>
                {
                    _groupField.value = e.newValue;
                    EditorUtility.SetDirty(_unitData);
                }
            );

            IntegerField nowUpgradeField = _root.Query<IntegerField>("upgrade_intfield").First();
            nowUpgradeField.label = "업글";
            nowUpgradeField.value = _nowUpgrade;
            nowUpgradeField.RegisterCallback<ChangeEvent<int>>(
                e =>
                {
                    _nowUpgrade = e.newValue;
                    EditorUtility.SetDirty(_unitData);
                            //Dps, nextUpgrade, health 변경됨
                        }
            );


            TextField totalHealthValue = _root.Query<TextField>("health_textfield").First();
            totalHealthValue.SetEnabled(false);
            totalHealthValue.label = "총체력";
            totalHealthValue.value = _unitData.StartHealthValue.GetValue();

            TextField dpsField = _root.Query<TextField>("dps_textfield").First();
            dpsField.SetEnabled(false);
            dpsField.label = "DPS";
            //            dpsField.value = _unitData.dpsValue.GetValue();

            TextField nextUpgradeAssetField = _root.Query<TextField>("next_upgrade_textfield").First();
            nextUpgradeAssetField.SetEnabled(false);
            nextUpgradeAssetField.label = "업글비용";
            nextUpgradeAssetField.value = _unitData.StartUpgradeAsset.GetValue();

            _productField = _root.Query<FloatField>("product_floatfield").First();
            _productField.label = "생산시간";
            _productField.value = _unitData.ProductTime;
            _productField.RegisterCallback<ChangeEvent<int>>(
                e =>
                {
                    _unitData.ProductTime = e.newValue;
                    EditorUtility.SetDirty(_unitData);
                }
            );







            TextField summaryHealthValueField = _root.Query<TextField>("summary_healthvalue_textfield").First();
            summaryHealthValueField.SetEnabled(false);
            summaryHealthValueField.label = "체력 요약";
            summaryHealthValueField.value = _unitData.StartHealthValue.GetValue();

            _startHealthValueField = _root.Query<TextField>("starthealthvalue_textfield").First();
            _startHealthValueField.label = "시작체력";
            _startHealthValueField.value = _unitData.StartHealthValue.GetValue();
            _startHealthValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _unitData.StartHealthValue.SetValue(e.newValue);
                    EditorUtility.SetDirty(_unitData);
                }
            );

            _increaseHealthValueField = _root.Query<TextField>("increasehealthvalue_textfield").First();
            _increaseHealthValueField.label = "체력증가량";
            _increaseHealthValueField.value = _unitData.IncreaseHealthValue.GetValue();
            _increaseHealthValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _unitData.IncreaseHealthValue.SetValue(e.newValue);
                    EditorUtility.SetDirty(_unitData);
                }
            );

            _increaseHealthRateField = _root.Query<FloatField>("increasehealthrate_textfield").First();
            _increaseHealthRateField.label = "체력증가비";
            _increaseHealthRateField.value = _unitData.IncreaseAttackRate;
            _increaseHealthRateField.RegisterCallback<ChangeEvent<float>>(
                e =>
                {
                    _unitData.IncreaseHealthRate = e.newValue;
                    EditorUtility.SetDirty(_unitData);
                }
            );




            TextField summaryAttackValueField = _root.Query<TextField>("summary_attackvalue_textfield").First();
            summaryAttackValueField.SetEnabled(false);
            summaryAttackValueField.label = "공격 요약";
            summaryAttackValueField.value = _unitData.StartAttackValue.GetValue();


            _startAttackValueField = _root.Query<TextField>("startattackvalue_textfield").First();
            _startAttackValueField.label = "기본공격력";
            _startAttackValueField.value = _unitData.StartAttackValue.GetValue();
            _startAttackValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _unitData.StartAttackValue.SetValue(e.newValue);
                    EditorUtility.SetDirty(_unitData);
                }
            );

            _increaseAttackValueField = _root.Query<TextField>("increaseattackvalue_textfield").First();
            _increaseAttackValueField.label = "공격증가량";
            _increaseAttackValueField.value = _unitData.IncreaseAttackValue.GetValue();
            _increaseAttackValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _unitData.IncreaseAttackValue.SetValue(e.newValue);
                    EditorUtility.SetDirty(_unitData);
                }
            );

            _increaseAttackRateField = _root.Query<FloatField>("increaseattackrate_textfield").First();
            _increaseAttackRateField.label = "공격증가비";
            _increaseAttackRateField.value = _unitData.IncreaseHealthRate;
            _increaseAttackRateField.RegisterCallback<ChangeEvent<float>>(
                e =>
                {
                    _unitData.IncreaseHealthRate = e.newValue;
                    EditorUtility.SetDirty(_unitData);
                }
            );


            _attackCountField = _root.Query<IntegerField>("attackcount_intfield").First();
            _attackCountField.label = "공격횟수";
            _attackCountField.value = _unitData.AttackCount;
            _attackCountField.RegisterCallback<ChangeEvent<int>>(
                e =>
                {
                    _unitData.AttackCount = e.newValue;
                    EditorUtility.SetDirty(_unitData);
                }
            );

            _typeAttackRangeField = _root.Query<EnumField>("typeattackrange_enumfield").First();
            _typeAttackRangeField.label = "공격타입";
            _typeAttackRangeField.value = _unitData.TypeAttackRange;
            _typeAttackRangeField.RegisterCallback<ChangeEvent<System.Enum>>(
                e =>
                {
                    _unitData.TypeAttackRange = (UnitData.TYPE_ATTACK_RANGE)e.newValue;
                    EditorUtility.SetDirty(_unitData);
                }
            );


            _typeAttackActionField = _root.Query<EnumField>("typeattackaction_enumfield").First();
            _typeAttackActionField.SetEnabled(false);
            _typeAttackActionField.label = "공격방식";
            //_typeAttackActionField.value = _unitData.TypeAttackAction;
            //_typeAttackActionField.RegisterCallback<ChangeEvent<System.Enum>>(
            //    e =>
            //    {
            //        _unitData.TypeAttackAction = (UnitData.TYPE_ATTACK_ACTION)e.newValue;
            //        EditorUtility.SetDirty(_unitData);
            //    }
            //);


            _attackDelayField = _root.Query<FloatField>("attackdelay_floatfield").First();
            _attackDelayField.label = "공격딜레이";
            _attackDelayField.value = _unitData.AttackDelay;
            _attackDelayField.RegisterCallback<ChangeEvent<float>>(
                e =>
                {
                    _unitData.AttackDelay = e.newValue;
                    EditorUtility.SetDirty(_unitData);
                }
            );






            TextField summaryUpgradeValueField = _root.Query<TextField>("summary_upgradevalue_textfield").First();
            summaryUpgradeValueField.SetEnabled(false);
            summaryUpgradeValueField.label = "업글요약";
            summaryUpgradeValueField.value = _unitData.StartUpgradeAsset.GetValue();


            _startUpgradeValueField = _root.Query<TextField>("startupgradevalue_textfield").First();
            _startUpgradeValueField.label = "기본업글자원";
            _startUpgradeValueField.value = _unitData.StartUpgradeAsset.GetValue();
            _startUpgradeValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _unitData.StartUpgradeAsset.SetValue(e.newValue);
                    EditorUtility.SetDirty(_unitData);
                }
            );

            _increaseUpgradeValueField = _root.Query<TextField>("increaseupgradevalue_textfield").First();
            _increaseUpgradeValueField.label = "업글증가량";
            _increaseUpgradeValueField.value = _unitData.IncreaseUpgradeAssetValue.GetValue();
            _increaseUpgradeValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _unitData.IncreaseUpgradeAssetValue.SetValue(e.newValue);
                    EditorUtility.SetDirty(_unitData);
                }
            );

            _increaseUpgradeRateField = _root.Query<FloatField>("increaseupgraderate_floatfield").First();
            _increaseUpgradeRateField.label = "업글증가비";
            _increaseUpgradeRateField.value = _unitData.IncreaseUpgradeAssetRate;
            _increaseUpgradeRateField.RegisterCallback<ChangeEvent<float>>(
                e =>
                {
                    _unitData.IncreaseUpgradeAssetRate = e.newValue;
                    EditorUtility.SetDirty(_unitData);
                }
            );


            _maximumUpgradeValueField = _root.Query<IntegerField>("maximumupgradevalue_intfield").First();
            _maximumUpgradeValueField.label = "최대업글량";
            _maximumUpgradeValueField.value = _unitData.MaximumUpgradeValue;
            _maximumUpgradeValueField.RegisterCallback<ChangeEvent<int>>(
                e =>
                {
                    _unitData.MaximumUpgradeValue = e.newValue;
                    EditorUtility.SetDirty(_unitData);
                }
            );




            _skeletonDataAssetField = _root.Query<ObjectField>("skeletondatasset_objectfield").First();
            _skeletonDataAssetField.label = "모델";
            _skeletonDataAssetField.objectType = typeof(Spine.Unity.SkeletonDataAsset);
            _skeletonDataAssetField.value = _unitData.SkeletonDataAsset;
            _skeletonDataAssetField.RegisterCallback<ChangeEvent<Object>>(
                e =>
                {
                    _unitData.SkeletonDataAsset = (Spine.Unity.SkeletonDataAsset)e.newValue;
                    // Set StarSystem as being dirty. This tells the editor that there have been changes made to the asset and that it requires a save. 
                    EditorUtility.SetDirty(_unitData);
                }
            );

            _spineModelKeyField = _root.Query<TextField>("spinemodelkey_textfield").First();
            _spineModelKeyField.label = "모델키";
            _spineModelKeyField.value = _unitData.SpineModelKey;
            _spineModelKeyField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _unitData.SpineModelKey = e.newValue;
                    EditorUtility.SetDirty(_unitData);
                }
            );

            _spineSkinKeyField = _root.Query<TextField>("spineskinkey_textfield").First();
            _spineSkinKeyField.label = "스킨";
            _spineSkinKeyField.value = _unitData.SpineSkinKey;
            _spineSkinKeyField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _unitData.SpineSkinKey = e.newValue;
                    EditorUtility.SetDirty(_unitData);
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