namespace SEF.Quest
{
    using SEF.Account;
    using System.Collections.Generic;

    public class QuestPackage
    {
        private List<RewardedQuestEntity> _list;

        public static QuestPackage Create()
        {
            return new QuestPackage();
        }

        public void Initialize(IAccountData data)
        {
            _list = new List<RewardedQuestEntity>();

            if (data != null)
            {
                //AccountData 적용하기
            }
        }

        public void CleanUp()
        {
            _list.Clear();
        }

        public void SetQuestData(IQuestData data, int value = 1)
        {
            var index = GetIndex(data);
            if (index == -1)
            {
                _list.Add(RewardedQuestEntity.Create(data));
                index = _list.Count - 1;
            }
            var entity = _list[index];
            entity.SetQuestData(value);
            _list[index] = entity;
        }

        private int GetIndex(IQuestData data) => _list.FindIndex(entity => entity.GetQuestData() == data);

        public bool HasQuestData(IQuestData data, int value)
        {
            if (value < 0)
            {
                throw new System.Exception("HasQuestData는 음수를 적용할 수 없습니다");
            }

            var index = GetIndex(data);

            if (index == -1) return false;

            var entity = _list[index];
            return entity.GetQuestValue() >= value;
        }

        public IAccountData GetSaveData()
        {
            return null;
        }

    }
}