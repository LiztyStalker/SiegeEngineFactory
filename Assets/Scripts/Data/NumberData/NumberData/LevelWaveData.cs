namespace SEF.Data
{
    public class LevelWaveData : NumberData
    {
        private const int LEVEL_DIVIDE = 10;
        public int GetLevel()
        {
            return Value / LEVEL_DIVIDE;
        }
        public int GetWave()
        {
            return Value % LEVEL_DIVIDE;
        }
    }
}