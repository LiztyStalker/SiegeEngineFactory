namespace SEF.Entity
{
    using Quest;
    using Data;
    using Utility.IO;
    using System.Numerics;

    #region ##### StorableData #####

    [System.Serializable]
    public class QuestEntityStorableData : StorableData
    {
        [UnityEngine.SerializeField] private string _key;
        //[UnityEngine.SerializeField] private string _value;
        [UnityEngine.SerializeField] private int _value;
        [UnityEngine.SerializeField] private int _index;
        [UnityEngine.SerializeField] private bool _rewarded;

        public string Key => _key;
        //public string Value => _value;
        public int Value => _value;
        public int Index => _index;
        public bool Rewarded => _rewarded;

//        public void SetData(string key, BigInteger value, int index, bool rewarded)
        public void SetData(string key, int value, int index, bool rewarded)
        {
            _key = key;
            _value = value;
            _index = index;
            _rewarded = rewarded;
        }
    }

    #endregion

    public struct QuestEntity
    {
        private QuestData _data;
        private int _nowValue;
        private int _nowIndex;
        private bool _hasRewarded;

        public string Key => _data.Key;
        public QuestData.TYPE_QUEST_GROUP TypeQuestGroup => _data.TypeQuestGroup;
        public BigInteger NowValue => _nowValue;
        public int GoalValue => _data.GetGoalValue(_nowIndex);
        public bool HasRewarded => _hasRewarded;
        public string AddressKey => _data.GetAddressKey(_nowIndex);
        public int NowIndex => _nowIndex;

        public void Initialize()
        {
            Clear();
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
        //public void AddQuestValue(BigInteger value)
        //{
        //    _nowValue += value;
        //}
        public bool HasNextIndex()
        {
            if (_data.IsMultipleQuest)
            {
                return _nowIndex + 1 < _data.MaximumIndex;
            }
            return false;
        }

        public void NextIndex() => _nowIndex++;
        public void ClearValue() => _nowValue = 0;

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
            if (HasRewarded) return false;
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

        #region ##### StorableData #####


        public StorableData GetStorableData()
        {
            var data = new QuestEntityStorableData();
            data.SetData(_data.Key, _nowValue, _nowIndex, _hasRewarded);
            return data;
        }

        public void SetStorableData(StorableData data)
        {
            var storableData = (QuestEntityStorableData)data;
            _nowValue = storableData.Value;// BigInteger.Parse(storableData.Value);
            _nowIndex = storableData.Index;
            _hasRewarded = storableData.Rewarded;
        }


        #endregion
    }
}