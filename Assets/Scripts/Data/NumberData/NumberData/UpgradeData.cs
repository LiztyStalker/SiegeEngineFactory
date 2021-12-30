namespace SEF.Data
{
    using UnityEngine;

    public class UpgradeData : NumberData
    {
        public UpgradeData() { }
        private UpgradeData(UpgradeData data)
        {
            Value = data.Value;
        }

        public override INumberData Clone() => new UpgradeData(this);
    }
}