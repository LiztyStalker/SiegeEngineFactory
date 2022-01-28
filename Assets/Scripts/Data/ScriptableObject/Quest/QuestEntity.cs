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
        public int GoalValue => _data.GetGoalValue(_nowValue);
        public bool HasRewarded => _hasRewarded;
        public void Initialize(IAccountData data)
        {
            Clear();

            if(data != null)
            {
                //����� ������ �����ϱ�
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
        public bool HasQuestGoal()
        {
            return _data.HasQuestGoal(_nowIndex, _nowValue);
        }

        public IAssetData GetRewardAssetData()
        {
            return _data.GetRewardAssetData(_nowIndex);
        }

    }
}