namespace SEF.Data
{
    [System.Serializable]
    public class LevelWaveData : NumberData
    {
        private const int LEVEL_THEME = 100;
        private const int LEVEL_DIVIDE = 10;

        public LevelWaveData() {
            Value = 1;
        }
        private LevelWaveData(LevelWaveData levelWaveData)
        {
            Value = levelWaveData.Value;
        }

        public override string GetValue()
        {
            return $"Level {GetLevel()} - Wave {GetWave()}";
        }

        /// <summary>
        /// 테마 0-n
        /// </summary>
        /// <returns></returns>
        public int GetTheme()
        {
            return Value / LEVEL_THEME;
        }

        /// <summary>
        /// 레벨 0-n
        /// </summary>
        /// <returns></returns>
        public int GetLevel()
        {
            return Value / LEVEL_DIVIDE;
        }

        /// <summary>
        /// 웨이브 0-9
        /// </summary>
        /// <returns></returns>
        public int GetWave()
        {
            return Value % LEVEL_DIVIDE;
        }

        public bool IsBoss()
        {
            if (IsThemeBoss()) return false;
            return GetWave() == 0;
        }

        public bool IsThemeBoss()
        {
            return (GetLevel() % LEVEL_DIVIDE == 0 && GetWave() == 0);
        }

        public override INumberData Clone()
        {
            return new LevelWaveData(this);
        }
    }
}