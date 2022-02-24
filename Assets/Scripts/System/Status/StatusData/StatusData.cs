namespace SEF.Data
{

    #region ##### Smithy #####

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

    #endregion




    #region #### Village #####

    public class IncreaseMaxPopulationStatusData : StatusData, IStatusData
    {
        public IncreaseMaxPopulationStatusData() { }       
    }

    public class IncreaseMaxUpgradeUnitStatusData : StatusData, IStatusData
    {
        public IncreaseMaxUpgradeUnitStatusData() { }
    }

    public class IncreaseMaxUpgradeSmithyStatusData : StatusData, IStatusData
    {
        public IncreaseMaxUpgradeSmithyStatusData() { }
    }

    public class IncreaseMaxUpgradeVillageStatusData : StatusData, IStatusData
    {
        public IncreaseMaxUpgradeVillageStatusData() { }
    }

    public class IncreaseMaxUpgradeMineStatusData : StatusData, IStatusData
    {
        public IncreaseMaxUpgradeMineStatusData() { }
    }

    #endregion
}
