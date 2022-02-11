namespace SEF.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ProductTimeStatusData : StatusData, IStatusData
    {
        public ProductTimeStatusData() { }

        public override void SetValue(float startValue, float increaseValue, IStatusData.TYPE_STATUS_DATA typeStatusData)
        {
            TypeStatusData = typeStatusData;
            if (TypeStatusData == IStatusData.TYPE_STATUS_DATA.Absolute)
            {
                StartValue = UniversalBigNumberData.Create(startValue);
                IncreaseValue = UniversalBigNumberData.Create(increaseValue);
            }
            else
            {
                StartValue = UniversalBigNumberData.Create(-startValue);
                IncreaseValue = UniversalBigNumberData.Create(-increaseValue);
            }
        }
    }
}