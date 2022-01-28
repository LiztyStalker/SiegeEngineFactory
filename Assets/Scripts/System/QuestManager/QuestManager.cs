namespace SEF.Quest
{
    using System.Collections.Generic;
    using Account;
    using Data;
    using Entity;
    using Storage;
    using System.Linq;

    public class QuestManager
    {
        private const int COUNT_DAILY_QUEST = 3;
        private const int COUNT_WEEKLY_QUEST = 7;

        private Dictionary<QuestData.TYPE_QUEST_GROUP, List<QuestEntity>> _dic;

        public static QuestManager Create()
        {
            return new QuestManager();
        }

        public void Initialize(IAccountData data)
        {
            _dic = new Dictionary<QuestData.TYPE_QUEST_GROUP, List<QuestEntity>>();

            var arr = DataStorage.Instance.GetAllDataArrayOrZero<QuestData>();
            //����
            SetRandomDictionary(arr, QuestData.TYPE_QUEST_GROUP.Daily, COUNT_DAILY_QUEST);
            //�ְ�
            SetRandomDictionary(arr, QuestData.TYPE_QUEST_GROUP.Weekly, COUNT_WEEKLY_QUEST);
            //����
            SetDictionary(arr, QuestData.TYPE_QUEST_GROUP.Challenge);
            //��ǥ
            SetDictionary(arr, QuestData.TYPE_QUEST_GROUP.Goal);

            if(data != null)
            {
                //����� ������ ����
                //�Ϸ� �̻� �������� ���� ������ ������
                //������ �̻� �������� �ְ� ������ ������
            }

            RefreshAllQuests();
        }


        private void SetDictionary(QuestData[] arr, QuestData.TYPE_QUEST_GROUP typeQuestGroup)
        {
            _dic.Add(typeQuestGroup, new List<QuestEntity>());
            var filterArr = GetQuestEntities(arr, typeQuestGroup);
            _dic[typeQuestGroup].AddRange(filterArr);
        }

        private void SetRandomDictionary(QuestData[] arr, QuestData.TYPE_QUEST_GROUP typeQuestGroup, int count)
        {
            _dic.Add(typeQuestGroup, new List<QuestEntity>());
            var filterArr = GetRandomQuestEntities(arr, typeQuestGroup, count);
            _dic[typeQuestGroup].AddRange(filterArr);
        }

        private QuestEntity[] GetQuestEntities(QuestData[] arr, QuestData.TYPE_QUEST_GROUP typeQuestGroup)
        {
            var filterArr = arr.Where(data => data.TypeQuestGroup == typeQuestGroup).ToArray();

            //NextQuest�� ������?
            //NextQuest������ �� �������� ���� ��

            QuestEntity[] entities = new QuestEntity[filterArr.Length];
            for (int i = 0; i < filterArr.Length; i++) 
            {
                var questData = filterArr[i];

                var entity = new QuestEntity();

                entity.SetData(questData);
                entities[i] = entity;
            }
            return entities;
        }

        private QuestEntity[] GetRandomQuestEntities(QuestData[] arr, QuestData.TYPE_QUEST_GROUP typeQuestGroup, int count)
        {
            var filterArr = arr.Where(data => data.TypeQuestGroup == typeQuestGroup).ToArray();

            List<QuestData> questList = new List<QuestData>();

            while (true)
            {
                var questData = filterArr[UnityEngine.Random.Range(0, filterArr.Length)];

                if (!questList.Contains(questData))
                {
                    questList.Add(questData);
                }

                //������ ����
                //��� ������ ����
                if(questList.Count == count || count > filterArr.Length && filterArr.Length == questList.Count)
                {
                    break;
                }
            }

            QuestEntity[] entities = new QuestEntity[questList.Count];
            while(questList.Count > 0)
            {
                var questData = questList[0];

                var entity = new QuestEntity();
                questList.Remove(questData);

                entity.SetData(questData);
                entities[questList.Count] = entity;
            }
            return entities;
        }


        public void CleanUp()
        {
            _dic.Clear();
        }

        public void RefreshAllQuests()
        {
            foreach (var key in _dic.Keys)
            {
                var values = _dic[key];
                for(int i = 0; i < values.Count; i++)
                {
                    RefreshQuest(values[i]);
                }
            }
        }

        private void RefreshQuest(QuestEntity entity)
        {
            OnRefreshEvent(entity);
        }
        public void SetQuestValue(int value)
        {
             
        }
        public IAssetData GetRewardAssetData(string key)
        {
            return null;
        }


        #region ##### Listener #####

        private System.Action<QuestEntity> _refreshEvent;
        public void AddOnRefreshListener(System.Action<QuestEntity> act) => _refreshEvent += act;
        public void RemoveOnRefreshListener(System.Action<QuestEntity> act) => _refreshEvent += act;
        private void OnRefreshEvent(QuestEntity entity)
        {
            _refreshEvent?.Invoke(entity);
        }

        #endregion
    }
}