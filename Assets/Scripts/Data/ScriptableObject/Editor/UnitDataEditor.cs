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

        private int nowUpgrade = 0;


        public void OnEnable()
        {
            _unitData = target as UnitData;
            _root = new VisualElement();

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Data/ScriptableObject/Editor/UnitDataEditor.uxml");
            visualTree.CloneTree(_root);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Data/ScriptableObject/Editor/UnitDataEditor.uss");
            _root.styleSheets.Add(styleSheet);

        }

        public override VisualElement CreateInspectorGUI()
        {

            VisualElement iconBackground = _root.Query<VisualElement>("icon_background").First();
            iconBackground.style.backgroundImage = _unitData.icon ? _unitData.icon.texture : null;



            ObjectField spriteField = _root.Query<ObjectField>("icon_objectfield").First();
            spriteField.objectType = typeof(Sprite);
            spriteField.value = _unitData.icon;
            spriteField.RegisterCallback<ChangeEvent<Object>>(
                e =>
                {
                    _unitData.icon = (Sprite)e.newValue;
                    iconBackground.style.backgroundImage = _unitData.icon.texture;
                            // Set StarSystem as being dirty. This tells the editor that there have been changes made to the asset and that it requires a save. 
                            EditorUtility.SetDirty(_unitData);
                }
            );

            Toggle toggle = _root.Query<Toggle>("modified_toggle").First();
            toggle.label = "수정";
            toggle.value = false;
            toggle.RegisterCallback<ChangeEvent<bool>>(
                e =>
                {
                    toggle.value = e.newValue;
                }
            );


            TextField nameField = _root.Query<TextField>("name_textfield").First();
            nameField.label = "이름";
            nameField.value = _unitData.Key;
            nameField.RegisterCallback<ChangeEvent<string>>(
                e =>
                {
                    _unitData.name = e.newValue;
                    EditorUtility.SetDirty(_unitData);
                }
            );

            EnumField groupField = _root.Query<EnumField>("group_enumfield").First();
            groupField.label = "그룹";
            groupField.value = _unitData.Group;            
            groupField.RegisterCallback<ChangeEvent<System.Enum>>(
                e =>
                {
                    groupField.value = e.newValue;
                    EditorUtility.SetDirty(_unitData);
                }
            );

            IntegerField nowUpgradeField = _root.Query<IntegerField>("upgrade_intfield").First();
            nowUpgradeField.label = "업글";
            nowUpgradeField.value = nowUpgrade;
            nowUpgradeField.RegisterCallback<ChangeEvent<int>>(
                e =>
                {
                    nowUpgrade = e.newValue;
                    EditorUtility.SetDirty(_unitData);
                            //Dps, nextUpgrade, health 변경됨
                        }
            );


            TextField totalHealthValue = _root.Query<TextField>("health_textfield").First();
            totalHealthValue.label = "총체력";
            totalHealthValue.value = _unitData.HealthValue.GetValue();

            TextField dpsField = _root.Query<TextField>("dps_textfield").First();
            dpsField.label = "DPS";
            //            dpsField.value = _unitData.dpsValue.GetValue();

            TextField nextUpgradeAssetField = _root.Query<TextField>("next_upgrade_textfield").First();
            nextUpgradeAssetField.label = "업글비용";
            nextUpgradeAssetField.value = _unitData.StartUpgradeAsset.GetValue();

            FloatField productField = _root.Query<FloatField>("product_floatfield").First();
            productField.label = "생산시간";
            productField.value = _unitData.ProductTime;
            productField.RegisterCallback<ChangeEvent<int>>(
                e =>
                {
                    _unitData.ProductTime = e.newValue;
                    EditorUtility.SetDirty(_unitData);
                }
            );













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