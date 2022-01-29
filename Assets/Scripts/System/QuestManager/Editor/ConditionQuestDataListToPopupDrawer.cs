#if UNITY_EDITOR
namespace SEF.Quest.Editor
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

    [CustomPropertyDrawer(typeof(ConditionQuestDataListToPopupAttribute))]
    public class ConditionQuestDataListToPopupDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var nowType = System.Type.GetType(property.stringValue);
            int selectedIndex = 0;
            if (nowType != null)
            {
                selectedIndex = ConditionQuestDataUtility.Current.FindIndex(nowType);
            }
            var arr = ConditionQuestDataUtility.Current.GetValues();
            var types = ConditionQuestDataUtility.Current.GetTypes();
            selectedIndex = EditorGUI.Popup(position, "Á¶°Ç", selectedIndex, arr);
            property.stringValue = types[selectedIndex].FullName;

        }
    }
}
#endif