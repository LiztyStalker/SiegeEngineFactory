#if UNITY_EDITOR

namespace SEF.Status.Editor
{
    using UnityEngine;

    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true)]
    public class StatusDataListToPopupAttribute : PropertyAttribute { }

}
#endif