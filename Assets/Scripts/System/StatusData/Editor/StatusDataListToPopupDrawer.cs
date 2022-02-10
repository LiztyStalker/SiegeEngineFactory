#if UNITY_EDITOR
namespace SEF.Status.Editor
{
    using UnityEngine;
    using UnityEditor;
    using SEF.Data;

    [CustomPropertyDrawer(typeof(StatusDataListToPopupAttribute))]
    public class StatusDataListToPopupDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            int selectedIndex = 0;
            var nowType = System.Type.GetType(property.stringValue);
            if (nowType != null)
            {
                selectedIndex = StatusDataUtility.Current.FindIndex(nowType);
            }
            var arr = StatusDataUtility.Current.GetValues();
            var types = StatusDataUtility.Current.GetTypes();
            selectedIndex = EditorGUI.Popup(position, "Á¶°Ç", selectedIndex, arr);
            property.stringValue = types[selectedIndex].FullName;
        }
    }
}
#endif