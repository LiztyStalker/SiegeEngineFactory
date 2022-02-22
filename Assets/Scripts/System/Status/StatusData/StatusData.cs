namespace SEF.Data
{
    public class UnitDamageDelayStatusData : StatusData, IStatusData
    {
        public UnitDamageDelayStatusData() { }

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

    public class UnitDamageValueStatusData : StatusData, IStatusData
    {
        public UnitDamageValueStatusData() { }
    }


    public class UnitHealthDataStatusData : StatusData, IStatusData
    {
        public UnitHealthDataStatusData() { }
    }


    public class UnitProductTimeStatusData : StatusData, IStatusData
    {
        public UnitProductTimeStatusData() { }

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


    public class AttackerDamageValueStatusData : StatusData, IStatusData
    {
        public AttackerDamageValueStatusData() { }

    }


    public class AttackerDamageDelayStatusData : StatusData, IStatusData
    {
        public AttackerDamageDelayStatusData() { }
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
