#if UNITY_EDITOR
namespace SEF.Statistics.Editor
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

    [CustomPropertyDrawer(typeof(StatisticsListToPopupAttribute))]
    public class StatisticsListToPopupDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var nowType = System.Type.GetType(property.stringValue);
            int selectedIndex = 0;
            if (nowType != null)
            {
                selectedIndex = SEF.Statistics.StatisticsUtility.Current.FindIndex(nowType);
            }
            var arr = SEF.Statistics.StatisticsUtility.Current.GetValues();
            var types = SEF.Statistics.StatisticsUtility.Current.GetTypes();
            selectedIndex = EditorGUI.Popup(position, "Á¶°Ç", selectedIndex, arr);
            property.stringValue = types[selectedIndex].FullName;

        }
    }
}
#endif