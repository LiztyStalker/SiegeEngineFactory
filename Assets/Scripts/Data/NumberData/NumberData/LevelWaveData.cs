namespace SEF.Data
{
    [System.Serializable]
    public class LevelWaveData : NumberData
    {
        private const int LEVEL_THEME = 100;
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

        /// <summary>
        /// �׸� 0-n
        /// </summary>
        /// <returns></returns>
        public int GetTheme()
        {
            return Value / LEVEL_THEME;
        }

        /// <summary>
        /// ���� 0-n
        /// </summary>
        /// <returns></returns>
        public int GetLevel()
        {
            return Value / LEVEL_DIVIDE;
        }

        /// <summary>
        /// ���̺� 0-9
        /// </summary>
        /// <returns></returns>
        public int GetWave()
        {
            return Value % LEVEL_DIVIDE;
        }

        //public TYPE_ENEMY_GROUP NowTypeEnemyGroup()
        //{
        //    if (IsBoss())
        //        return TYPE_ENEMY_GROUP.Boss;
        //    else if (IsThemeBoss())
        //        return TYPE_ENEMY_GROUP.ThemeBoss;
        //    return TYPE_ENEMY_GROUP.Normal;
        //}

        public bool IsBoss()
        {
            if (IsThemeBoss()) return false;
            return GetWave() == 9;
        }

        public bool IsThemeBoss()
        {
            return (GetLevel() % LEVEL_DIVIDE == 9 && GetWave() == 9);
        }

        public override INumberData Clone()
        {
            return new LevelWaveData(this);
        }
    }
}