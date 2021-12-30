namespace SEF.Data
{
    public class LevelWaveData : NumberData
    {
        private const int LEVEL_DIVIDE = 10;

        public LevelWaveData() { }
        private LevelWaveData(LevelWaveData levelWaveData)
        {
            Value = levelWaveData.Value;
        }

        public override string GetValue()
        {
            return $"Level {GetLevel()} - Wave {GetWave()}";
        }

        public int GetLevel()
        {
            return Value / LEVEL_DIVIDE;
        }
        public int GetWave()
        {
            return Value % LEVEL_DIVIDE;
        }

        public override INumberData Clone()
        {
            return new LevelWaveData(this);
        }
    }
}