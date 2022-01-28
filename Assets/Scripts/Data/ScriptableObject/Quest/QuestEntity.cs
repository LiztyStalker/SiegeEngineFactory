namespace SEF.Entity
{
    using Quest;
    using Data;
    using Account;

    public struct QuestEntity
    {
        private QuestData _data;
        private int _nowValue;
        private bool _hasRewarded;
        public string Key => _data.Key;
        public QuestData.TYPE_QUEST_GROUP TypeQuestGroup => _data.TypeQuestGroup;
        public int NowValue => _nowValue;
        public int GoalValue => _data.ConditionValue;
        public bool HasRewarded => _hasRewarded;
        public void Initialize(IAccountData data)
        {
            Clear();

            if(data != null)
            {
                //저장된 데이터 적용하기
            }
        }
        public void SetData(QuestData data)
        {
            _data = data;
        }

        public void SetQuestValue(int value)
        {
            _nowValue = value;
        }

        public void SetRewarded(bool hasRewarded)
        {
            _hasRewarded = hasRewarded;
        }
        public void CleanUp()
        {
            Clear();
        }

        private void Clear()
        {
            _data = null;
            _nowValue = 0;
            _hasRewarded = false;
        }
        public bool HasQuestGoal() => _data.ConditionValue >= _nowValue;

        public IAssetData GetRewardAssetData() => _data.RewardAssetData;

    }
}