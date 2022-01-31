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
            //일일
            SetRandomDictionary(arr, QuestData.TYPE_QUEST_GROUP.Daily, COUNT_DAILY_QUEST);
            //주간
            SetRandomDictionary(arr, QuestData.TYPE_QUEST_GROUP.Weekly, COUNT_WEEKLY_QUEST);
            //도전
            SetDictionary(arr, QuestData.TYPE_QUEST_GROUP.Challenge);
            //목표
            SetDictionary(arr, QuestData.TYPE_QUEST_GROUP.Goal);

            if(data != null)
            {
                //저장된 데이터 적용
                //하루 이상 지났으면 일일 데이터 미적용
                //일주일 이상 지났으면 주간 데이터 미적용
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

            //NextQuest가 있으면?
            //NextQuest끼리는 한 묶음으로 봐야 함

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

                //데이터 부족
                //모든 데이터 충전
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
        public void SetQuestValue(System.Type type, int value)
        {
            var entityNullable = FindQuestEntity(type);
            if (entityNullable != null)
            {
                var entity = entityNullable.Value;
                entity.AddQuestValue(value);
                SetQuestEntity(entity);
                RefreshQuest(entity);
            }
        }
        public void SetQuestValue<T>(int value) where T : IConditionQuestData
        {
            var entityNullable = FindQuestEntity<T>();
            if (entityNullable != null)
            {
                var entity = entityNullable.Value;
                entity.AddQuestValue(value);
                SetQuestEntity(entity);
                RefreshQuest(entity);
            }
        }
        public void AddQuestValue(System.Type type, int value)
        {
            var entityNullable = FindQuestEntity(type);
            if (entityNullable != null)
            {
                var entity = entityNullable.Value;
                entity.AddQuestValue(value);
                SetQuestEntity(entity);
                RefreshQuest(entity);
            }
        }
        public void AddQuestValue<T>(int value) where T : IConditionQuestData
        {
            var entityNullable = FindQuestEntity<T>();
            if (entityNullable != null) 
            {
                var entity = entityNullable.Value;
                entity.AddQuestValue(value);
                SetQuestEntity(entity);
                RefreshQuest(entity);
            }
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public void SetQuestEntity_Test(QuestData.TYPE_QUEST_GROUP typeQuestGroup, QuestEntity entity)
        {
            if (_dic.ContainsKey(typeQuestGroup))
            {
                _dic[typeQuestGroup].Add(entity);
            }
        }
#endif

        private void SetQuestEntity(QuestEntity entity)
        {
            if (_dic.ContainsKey(entity.TypeQuestGroup))
            {
                var list = _dic[entity.TypeQuestGroup];
                var index = list.FindIndex(e => e.Key == entity.Key);
                if(index >= 0)
                {
                    list[index] = entity;
                }
                else
                {
                    UnityEngine.Debug.LogError($"{entity.Key}을 찾을 수 없습니다");
                }
            }
            else
            {
                UnityEngine.Debug.LogError($"{entity.TypeQuestGroup}을 찾을 수 없습니다");
            }
        }

        private QuestEntity? FindQuestEntity<T>() where T : IConditionQuestData
        {
            foreach(var key in _dic.Keys)
            {
                var list = _dic[key];
                for(int i = 0; i < list.Count; i++)
                {
                    if (list[i].HasConditionQuestData<T>())
                        return list[i];
                }
            }
            return null;
        }

        private QuestEntity? FindQuestEntity(System.Type type)
        {
            foreach (var key in _dic.Keys)
            {
                var list = _dic[key];
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].HasConditionQuestData(type))
                        return list[i];
                }
            }
            return null;
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