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
        private UnitData _unitData;
        private VisualElement _root;

        private int _nowUpgrade = 1;
        private bool _isModified = false;

        private VisualElement _iconField;
        private ObjectField _spriteField;

        private EnumField _groupField;

        private FloatField _scaleField;

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
        private ObjectField _bulletField;
        private FloatField _bulletScaleField;

        private VisualElement _attackerLayout;
        private Button _attackerAddButton;

        private TextField _startUpgradeValueField;
        private TextField _increaseUpgradeValueField;
        private FloatField _increaseUpgradeRateField;
        private IntegerField _maximumUpgradeValueField;

        private Button _techAddButton;
        private Button _techRemoveButton;
        private VisualElement _techLayout;

        private ObjectField _skeletonDataAssetField;
        private TextField _spineModelKeyField;
        private TextField _spineSkinKeyField;

        public void OnEnable()
        {
            _unitData = target as UnitData;
            _root = new VisualElement();

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Data/ScriptableObject/Unit/Editor/UnitDataEditor.uxml");
            visualTree.CloneTree(_root);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Data/ScriptableObject/Unit/Editor/UnitDataEditor.uss");
            _root.styleSheets.Add(styleSheet);

        }



        public void UpdateFields()
        {
            _spriteField.SetEnabled(_isModified);
            _groupField.SetEnabled(_isModified);
            _productField.SetEnabled(_isModified);
            _scaleField.SetEnabled(_isModified);

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
            _bulletField.SetEnabled(_isModified);
            _bulletScaleField.SetEnabled(_isModified);

            _startUpgradeValueField.SetEnabled(_isModified);
            _increaseUpgradeValueField.SetEnabled(_isModified);
            _increaseUpgradeRateField.SetEnabled(_isModified);
            _maximumUpgradeValueField.SetEnabled(_isModified);

            _skeletonDataAssetField.SetEnabled(_isModified);
            _spineModelKeyField.SetEnabled(false);
            _spineSkinKeyField.SetEnabled(_isModified);

            _techLayout.SetEnabled(_isModified);
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
            _groupField.BindProperty(serializedObject.FindProperty("_group"));
            _groupField.label = "그룹";
            _groupField.value = _unitData.Group;
            _groupField.RegisterCallback<ChangeEvent<System.Enum>>(
                e =>
                {
                    _groupField.value = e.newValue;
                    EditorUtility.SetDirty(_unitData);
                }
            );

            _scaleField = _root.Query<FloatField>("scale-floatfield").First();
            _scaleField.label = "크기";
            _scaleField.value = _unitData.Scale;
            _scaleField.RegisterCallback<ChangeEvent<float>>(
                e =>
                {
                    _unitData.Scale = e.newValue;
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
            totalHealthValue.value = "";// _unitData.StartHealthValue.GetValue();

            TextField dpsField = _root.Query<TextField>("dps_textfield").First();
            dpsField.SetEnabled(false);
            dpsField.label = "DPS";
            //            dpsField.value = _unitData.dpsValue.GetValue();

            TextField nextUpgradeAssetField = _root.Query<TextField>("next_upgrade_textfield").First();
            nextUpgradeAssetField.SetEnabled(false);
            nextUpgradeAssetField.label = "업글비용";
            nextUpgradeAssetField.value = "";// _unitData.StartUpgradeAsset.GetValue();

            _productField = _root.Query<FloatField>("product_floatfield").First();
            _productField.label = "생산시간";
            _productField.value = _unitData.ProductTime;
            _productField.RegisterCallback<ChangeEvent<float>>(
                e =>
                {
                    _unitData.ProductTime = e.newValue;
                    EditorUtility.SetDirty(_unitData);
                }
            );







            TextField summaryHealthValueField = _root.Query<TextField>("summary_healthvalue_textfield").First();
            summaryHealthValueField.SetEnabled(false);
            summaryHealthValueField.label = "체력 요약";
            summaryHealthValueField.value = "";// _unitData.StartHealthValue.GetValue();

            _startHealthValueField = _root.Query<TextField>("starthealthvalue_textfield").First();
            _startHealthValueField.label = "시작체력";
            _startHealthValueField.value = _unitData.StartHealthValue.ValueText;
            _startHealthValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _unitData.StartHealthValue.ValueText = e.newValue;
                    EditorUtility.SetDirty(_unitData);
                }
            );

            _increaseHealthValueField = _root.Query<TextField>("increasehealthvalue_textfield").First();
            _increaseHealthValueField.label = "체력증가량";
            _increaseHealthValueField.value = _unitData.IncreaseHealthValue.ToString();
            _increaseHealthValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _unitData.IncreaseHealthValue = int.Parse(e.newValue);
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
            summaryAttackValueField.value = "";// _unitData.StartAttackValue.GetValue();


            _startAttackValueField = _root.Query<TextField>("startattackvalue_textfield").First();
            _startAttackValueField.label = "기본공격력";
            _startAttackValueField.value = _unitData.StartAttackValue.ValueText;
            _startAttackValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _unitData.StartAttackValue.ValueText = e.newValue;
                    EditorUtility.SetDirty(_unitData);
                }
            );

            _increaseAttackValueField = _root.Query<TextField>("increaseattackvalue_textfield").First();
            _increaseAttackValueField.label = "공격증가량";
            _increaseAttackValueField.value = _unitData.IncreaseAttackValue.ToString();
            _increaseAttackValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _unitData.IncreaseAttackValue = int.Parse(e.newValue);
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
            _typeAttackRangeField.BindProperty(serializedObject.FindProperty("_typeAttackRange"));
            _typeAttackRangeField.label = "공격타입";
            //_typeAttackRangeField.value = _unitData.TypeAttackRange;
            //_typeAttackRangeField.RegisterCallback<ChangeEvent<System.Enum>>(
            //    e =>
            //    {
            //        _unitData.TypeAttackRange = (UnitData.TYPE_ATTACK_RANGE)e.newValue;
            //        EditorUtility.SetDirty(_unitData);
            //    }
            //);


            _typeAttackActionField = _root.Query<EnumField>("typeattackaction_enumfield").First();
            //_typeAttackActionField.BindProperty(serializedObject.FindProperty("_typeAttackAction"));
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

            _bulletField = _root.Query<ObjectField>("bullet-objectfield").First();
            _bulletField.objectType = typeof(UtilityManager.BulletData);
            _bulletField.label = "탄환";
            _bulletField.value = _unitData.AttackBulletData;
            _bulletField.RegisterCallback<ChangeEvent<Object>>(
                e =>
                {
                    _unitData.AttackBulletData = e.newValue as UtilityManager.BulletData;
                    EditorUtility.SetDirty(_unitData);
                }
            );

            _bulletScaleField = _root.Query<FloatField>("bullet-scale-floatfield").First();
            _bulletScaleField.label = "탄환크기";
            _bulletScaleField.value = _unitData.BulletScale;
            _bulletScaleField.RegisterCallback<ChangeEvent<float>>(
                e =>
                {
                    _unitData.BulletScale = e.newValue;
                    EditorUtility.SetDirty(_unitData);
                }
            );


            _attackerAddButton = _root.Query<Button>("attacker-add-button").First();
            _attackerAddButton.clicked += AddAttackerData;
            //Button attackerRemoveButton = _root.Query<Button>("attacker-add-button").First();
            //attackerRemoveButton.clicked += RemoveAttackerData;

            _attackerLayout = _root.Query<VisualElement>("attacker-layout").First();
            if(_unitData.AttackerDataArray != null) UpdateAttackerData(_attackerLayout, _unitData.AttackerDataArray);



            TextField summaryUpgradeValueField = _root.Query<TextField>("summary_upgradevalue_textfield").First();
            summaryUpgradeValueField.SetEnabled(false);
            summaryUpgradeValueField.label = "업글요약";
            summaryUpgradeValueField.value = _unitData.StartUpgradeAsset.GetValue();


            _startUpgradeValueField = _root.Query<TextField>("startupgradevalue_textfield").First();
            _startUpgradeValueField.label = "기본업글자원";
            _startUpgradeValueField.value = _unitData.StartUpgradeAsset.ValueText;
            _startUpgradeValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _unitData.StartUpgradeAsset.ValueText = e.newValue;
                    EditorUtility.SetDirty(_unitData);
                }
            );

            _increaseUpgradeValueField = _root.Query<TextField>("increaseupgradevalue_textfield").First();
            _increaseUpgradeValueField.label = "업글증가량";
            _increaseUpgradeValueField.value = _unitData.IncreaseUpgradeAssetValue.ToString();//.ValueText;
            _increaseUpgradeValueField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _unitData.IncreaseUpgradeAssetValue = int.Parse(e.newValue);
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
            _maximumUpgradeValueField.value = _unitData.DefaultMaxUpgradeValue;
            _maximumUpgradeValueField.BindProperty(serializedObject.FindProperty("_defaultMaxUpgradeValue"));
            //_maximumUpgradeValueField.RegisterCallback<ChangeEvent<int>>(
            //    e =>
            //    {
            //        _unitData.DefaultMaxUpgradeValue = e.newValue;
            //        EditorUtility.SetDirty(_unitData);
            //    }
            //);




            _techAddButton = _root.Query<Button>("unit-tech-add-button").First();
            //_techAddButton.RegisterCallback<ClickEvent>();

            _techRemoveButton = _root.Query<Button>("unit-tech-remove-button").First();
            //_techRemoveButton.RegisterCallback<ClickEvent>();

            _techLayout = _root.Query<VisualElement>("unit-tech-layout");

            var unitTechDataProperty = serializedObject.FindProperty("_unitTechDataArray");
            if(unitTechDataProperty != null) UpdateTechData(_techLayout, unitTechDataProperty);


            _spineModelKeyField = _root.Query<TextField>("spinemodelkey_textfield").First();
            _spineModelKeyField.label = "모델키";
            _spineModelKeyField.value = _unitData.SpineModelKey;
            //_spineModelKeyField.RegisterCallback<ChangeEvent<string>>(
            //    e =>
            //    {
            //        _unitData.SpineModelKey = e.newValue;
            //        EditorUtility.SetDirty(_unitData);
            //    }
            //);

            _skeletonDataAssetField = _root.Query<ObjectField>("skeletondatasset_objectfield").First();
            _skeletonDataAssetField.label = "모델";
            _skeletonDataAssetField.objectType = typeof(Spine.Unity.SkeletonDataAsset);
            _skeletonDataAssetField.value = _unitData.SkeletonDataAsset;
            _skeletonDataAssetField.RegisterCallback<ChangeEvent<Object>>(
                e =>
                {
                    _unitData.SkeletonDataAsset = (Spine.Unity.SkeletonDataAsset)e.newValue;
                    _spineModelKeyField.value = _unitData.SpineModelKey;
                    // Set StarSystem as being dirty. This tells the editor that there have been changes made to the asset and that it requires a save. 
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

            serializedObject.ApplyModifiedProperties();

            return _root;
        }


        private void OnDisable()
        {
            //_attackerAddButton.clicked -= AddAttackerData;
        }

        private void UpdateTechData(VisualElement layout, SerializedProperty property)
        {
            layout.Clear();
            for (int i = 0; i < property.arraySize; i++)
            {
                var editor = new UnitTechDataEditor(property.GetArrayElementAtIndex(i));
                layout.Add(editor);
            }
        }

        //private void UpdateTechData(VisualElement layout, UnitTechData[] arr)
        //{
        //    layout.Clear();
        //    for (int i = 0; i < arr.Length; i++)
        //    {
        //        var editor = new UnitTechDataEditor(arr[i]);
        //        editor.SetOnRemoveListener(RemoveUnitTechData);
        //        layout.Add(editor);
        //    }
        //}


        //private void AddUnitTechData()
        //{
        //    var data = UnitTechData.Create_Test();
        //    _unitData.AddUnitTechData(data);
        //    UpdateTechData(_techLayout, _unitData.UnitTechDataArray);
        //}
        //private void RemoveUnitTechData(UnitTechData attackerData)
        //{
        //    _unitData.RemoveUnitTechData(attackerData);
        //    UpdateTechData(_techLayout, _unitData.UnitTechDataArray);
        //}

        private void UpdateAttackerData(VisualElement layout, AttackerData[] arr)
        {
            if (arr != null)
            {
                layout.Clear();
                for (int i = 0; i < arr.Length; i++)
                {
                    var attackerDataEditor = new AttackerDataEditor(arr[i]);
                    attackerDataEditor.SetOnRemoveListener(RemoveAttackerData);
                    layout.Add(attackerDataEditor);
                }
            }
        }

        private void AddAttackerData()
        {
            var attackerData = AttackerData.Create_Test();
            _unitData.AddAttackerData(attackerData);
            UpdateAttackerData(_attackerLayout, _unitData.AttackerDataArray);
        }
        private void RemoveAttackerData(AttackerData attackerData)
        {
            _unitData.RemoveAttackerData(attackerData);
            UpdateAttackerData(_attackerLayout, _unitData.AttackerDataArray);
        }
    }
}
#endif