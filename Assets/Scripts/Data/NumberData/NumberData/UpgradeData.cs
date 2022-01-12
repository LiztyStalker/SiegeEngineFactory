namespace SEF.Data
{
    public class UpgradeData : NumberData
    {
        public UpgradeData() { }
        private UpgradeData(UpgradeData data)
        {
            Value = data.Value;
        }

        public override void Initialize()
        {
        }

        public override INumberData Clone() => new UpgradeData(this);
    }
}