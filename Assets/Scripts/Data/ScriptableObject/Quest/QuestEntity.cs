namespace SEF.Entity
{
    using System.Collections.Generic;
    using Quest;
    using Data;
    using Account;

    public struct QuestEntity
    {
        private QuestData _data;
        private int _nowValue;
        private int _nowIndex;
        private bool _hasRewarded;

        public string Key => _data.Key;
        public QuestData.TYPE_QUEST_GROUP TypeQuestGroup => _data.TypeQuestGroup;
        public int NowValue => _nowValue;
        public int GoalValue => _data.GetGoalValue(_nowIndex);
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

        public void AddQuestValue(int value = 1)
        {
            _nowValue += value;
        }

        public bool HasNextIndex()
        {
            if (_data.IsMultipleQuest)
            {
                return _nowIndex < _data.MaximumIndex;
            }
            return false;
        }

        public void NextIndex()
        {
            _nowIndex++;
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
        public bool HasQuestGoal()
        {
            return _data.HasQuestGoal(_nowIndex, _nowValue);
        }

        public bool HasConditionQuestData<T>() where T : IConditionQuestData => _data.IsConditionQuestData<T>(_nowIndex);
        public bool HasConditionQuestData(System.Type type) => _data.IsConditionQuestData(type, _nowIndex);

        public IAssetData GetRewardAssetData()
        {
            return _data.GetRewardAssetData(_nowIndex);
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static QuestEntity Create()
        {
            return new QuestEntity();
        }
#endif
    }
}