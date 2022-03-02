namespace SEF.Quest
{
    using System.Collections.Generic;
    using Data;
    using Entity;
    using Storage;
    using System.Linq;
    using Utility.IO;


    #region ##### StorableData #####

    [System.Serializable]
    public class QuestDictionaryStorableData : StorableData
    {
        [UnityEngine.SerializeField] private string _group;


        public string Group => _group;
        public void SetData(string group,  StorableData[] children)
        {
            _group = group;
            Children = children;
        }
    }

    [System.Serializable]
    public class QuestManagerStorableData : StorableData
    {
        [UnityEngine.SerializeField] private System.DateTime _daily;
        [UnityEngine.SerializeField] private System.DateTime _weekly;

        public System.DateTime Daily => _daily;
        public System.DateTime Weekly => _weekly;
        public void SetData(System.DateTime daily, System.DateTime weekly, StorableData[] children)
        {
            _daily = daily;
            _weekly = weekly;
            Children = children;
        }
    }

    #endregion

    public class QuestManager
    {
        private const int COUNT_DAILY_QUEST = 3;
        private const int COUNT_WEEKLY_QUEST = 7;

        private Dictionary<QuestData.TYPE_QUEST_GROUP, List<QuestEntity>> _dic;

        private System.DateTime _daily;
        private System.DateTime _weekly;

        public static QuestManager Create()
        {
            return new QuestManager();
        }

        public void Initialize()
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


            //오늘
            _daily = GetToday();

            //주간
            _weekly = GetWeekly();

            RefreshAllQuests();
        }

        public static System.DateTime GetToday()
        {
            return new System.DateTime(System.DateTime.UtcNow.Year, System.DateTime.UtcNow.Month, System.DateTime.UtcNow.Day);
        }

        public static System.DateTime GetWeekly()
        {
            var weekly = new System.DateTime(System.DateTime.UtcNow.Year, System.DateTime.UtcNow.Month, System.DateTime.UtcNow.Day);

            //현재 위치의 첫주
            if (weekly.DayOfWeek != System.DayOfWeek.Sunday)
            {
                var subtract = (int)weekly.DayOfWeek;
                weekly = weekly.Subtract(new System.TimeSpan(subtract, 0, 0, 0));
            }
            return weekly;
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
            if(filterArr != null) _dic[typeQuestGroup].AddRange(filterArr);
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
            if (arr.Length > 0)
            {
                var filterArr = arr.Where(data => data.TypeQuestGroup == typeQuestGroup).ToArray();

                if (filterArr.Length > 0)
                {
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
                        if (questList.Count == count || count > filterArr.Length && filterArr.Length == questList.Count)
                        {
                            break;
                        }
                    }

                    QuestEntity[] entities = new QuestEntity[questList.Count];
                    while (questList.Count > 0)
                    {
                        var questData = questList[0];

                        var entity = new QuestEntity();
                        questList.Remove(questData);

                        entity.SetData(questData);
                        entities[questList.Count] = entity;
                    }
                    return entities;
                }
#if UNITY_EDITOR
                else
                {
                    UnityEngine.Debug.LogWarning("QuestData Filter List가 비어있습니다");
                }
#endif
            }
#if UNITY_EDITOR
            else {
                UnityEngine.Debug.LogWarning("QuestData List가 비어있습니다");
            }
#endif
            return null;
        }


        public void CleanUp()
        {
            _dic.Clear();
        }

        public void RefreshAllQuests()
        {
            foreach (var key in _dic.Keys)
            {
                RefreshAllQuests(key);
            }
        }

        public void RefreshAllQuests(QuestData.TYPE_QUEST_GROUP typeQuestGroup)
        {
            var values = _dic[typeQuestGroup];
            for (int i = 0; i < values.Count; i++)
            {
                RefreshQuest(values[i]);
            }
        }

        public void RefreshAllQuests(System.DateTime utcSavedTime)
        {
            var arr = DataStorage.Instance.GetAllDataArrayOrZero<QuestData>();

            //일일
            //하루가 지났으면
            if ((utcSavedTime - _daily).TotalDays >= 1) 
            {
                //하루 지났으면 재구성
                if (_dic.ContainsKey(QuestData.TYPE_QUEST_GROUP.Daily))
                {
                    _dic[QuestData.TYPE_QUEST_GROUP.Daily].Clear();
                }

                //퀘스트 재갱신
                SetRandomDictionary(arr, QuestData.TYPE_QUEST_GROUP.Daily, COUNT_DAILY_QUEST);

                //오늘날짜 등록
                _daily = GetToday();
            }

            //주간
            //일주일이 지났으면
            if((utcSavedTime - _weekly).TotalDays >= 7)
            {
                //7일이 지났으면 재구성
                if (_dic.ContainsKey(QuestData.TYPE_QUEST_GROUP.Weekly))
                {
                    _dic[QuestData.TYPE_QUEST_GROUP.Weekly].Clear();
                }

                //퀘스트 재갱신
                SetRandomDictionary(arr, QuestData.TYPE_QUEST_GROUP.Weekly, COUNT_WEEKLY_QUEST);

                //오늘날짜 등록
                _weekly = GetWeekly();
            }
        }

        private void RefreshQuest(QuestEntity entity)
        {
            UnityEngine.Debug.Log("Refresh");
            OnRefreshEvent(entity);
        }
        public void SetQuestValue(System.Type type, int value)
        {
            var entities = FindQuestEntities(type);
            for (int i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
                entity.SetQuestValue(value);
                SetQuestEntity(entity);
                RefreshQuest(entity);
            }
        }
        public void SetQuestValue<T>(int value) where T : IConditionQuestData
        {
            var entities = FindQuestEntities<T>();
            for (int i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
                entity.SetQuestValue(value);
                SetQuestEntity(entity);
                RefreshQuest(entity);
            }
        }
        public void AddQuestValue(System.Type type, int value)
        {
            var entities = FindQuestEntities(type);
            for (int i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
                entity.AddQuestValue(value);
                SetQuestEntity(entity);
                RefreshQuest(entity);
            }
        }
        public void AddQuestValue<T>(int value) where T : IConditionQuestData
        {
            var entities = FindQuestEntities<T>();
            for(int i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
                entity.AddQuestValue(value);
                SetQuestEntity(entity);
                RefreshQuest(entity);
            }
        }

        //public void AddQuestValue<T>(System.Numerics.BigInteger value) where T : IConditionQuestData
        //{
        //    var entities = FindQuestEntities<T>();
        //    for (int i = 0; i < entities.Length; i++)
        //    {
        //        var entity = entities[i];
        //        entity.AddQuestValue(value);
        //        SetQuestEntity(entity);
        //        RefreshQuest(entity);
        //    }
        //}

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

        private QuestEntity[] FindQuestEntities<T>() where T : IConditionQuestData
        {
            List<QuestEntity> entities = new List<QuestEntity>();
            foreach (var key in _dic.Keys)
            {
                var list = _dic[key];
                var arr = list.Where(entity => entity.HasConditionQuestData<T>()).ToArray();
                entities.AddRange(arr);
            }
            return entities.ToArray();
        }

        private QuestEntity[] FindQuestEntities(System.Type type)
        {
            List<QuestEntity> entities = new List<QuestEntity>();
            foreach (var key in _dic.Keys)
            {
                var list = _dic[key];
                var arr = list.Where(entity => entity.HasConditionQuestData(type)).ToArray();
                entities.AddRange(arr);
            }
            return entities.ToArray();
        }

        public IAssetData GetRewardAssetData(QuestData.TYPE_QUEST_GROUP typeQuestGroup, string key)
        {
            if (_dic.ContainsKey(typeQuestGroup))
            {
                var list = _dic[typeQuestGroup];
                for(int i = 0; i < list.Count; i++)
                {
                    var entity = list[i];
                    if (entity.Key == key)
                    {
                        var assetData = entity.GetRewardAssetData();
                        if (entity.HasNextIndex())
                        {
                            entity.NextIndex();
                            entity.ClearValue();
                        }
                        else
                            entity.SetRewarded(true);
                        list[i] = entity;
                        RefreshQuest(entity);
                        return assetData;
                    }
                }
            }
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




        #region ##### StorableData #####

        public StorableData GetStorableData()
        {
            var data = new QuestManagerStorableData();

            List<StorableData> list = new List<StorableData>();
            foreach(var key in _dic.Keys)
            {
                var strData = new QuestDictionaryStorableData();
                strData.SetData(key.ToString(), GetChildren(_dic[key].ToArray()));
                list.Add(strData);
            }
            data.SetData(_daily, _weekly, list.ToArray());
            return data;
        }

        private StorableData[] GetChildren(QuestEntity[] entities)
        {
            var arr = new StorableData[entities.Length];
            for(int i = 0; i < arr.Length; i++)
            {
                arr[i] = entities[i].GetStorableData();
            }
            return arr;
        }

        public void SetStorableData(StorableData data)
        {
            var storableData = (QuestManagerStorableData)data;

            _daily = storableData.Daily;
            _weekly = storableData.Weekly;

            var children = storableData.Children;
            for (int i = 0; i < children.Length; i++)
            {
                var child = (QuestDictionaryStorableData)children[i];
                var typeGroup = (QuestData.TYPE_QUEST_GROUP)System.Enum.Parse(typeof(QuestData.TYPE_QUEST_GROUP), child.Group);
                SetChildren(typeGroup, child.Children);
            }
        }

        private void SetChildren(QuestData.TYPE_QUEST_GROUP typeGroup, StorableData[] children)
        {
            if (_dic.ContainsKey(typeGroup))
            {
                var list = _dic[typeGroup];
                for(int i = 0; i < children.Length; i++)
                {
                    var child = ((QuestEntityStorableData)children[i]);
                    var index = list.FindIndex(c => c.Key == child.Key);

                    var ch = list[index];
                    ch.SetStorableData(child);
                    list[index] = ch;
                    RefreshQuest(list[index]);
                }
                _dic[typeGroup] = list;
            }
        }

        #endregion
    }
}