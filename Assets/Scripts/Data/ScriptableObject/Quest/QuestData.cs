namespace SEF.Data
{
    using System.Collections.Generic;
    using System;
    using UnityEngine;
    using Quest;

    [CreateAssetMenu(fileName = "QuestData", menuName = "ScriptableObjects/QuestData")]

    public class QuestData : ScriptableObject
    {
        public enum TYPE_QUEST_GROUP { Daily, Weekly, Challenge, Goal}


        [SerializeField]
        private string _key;
        public string Key => _key;


        [SerializeField]
        private TYPE_QUEST_GROUP _typeQuestGroup;
        public TYPE_QUEST_GROUP TypeQuestGroup => _typeQuestGroup;


        [SerializeField]
        private Sprite _icon;
        public Sprite Icon => _icon;


        [SerializeField]
        private bool _isMultipleQuest = false;
        public bool IsMultipleQuest => _isMultipleQuest;

        [SerializeField]
        private QuestConditionData _questConditionData;
        public QuestConditionData QuestConditionData => _questConditionData;

        [SerializeField]
        private QuestConditionData[] _questConditionDataArray = new QuestConditionData[1];
        public QuestConditionData[] QuestConditionDataArray => _questConditionDataArray;

        public int MaximumIndex => _questConditionDataArray.Length;

        public int GetGoalValue(int index) 
        {
            if (IsMultipleQuest)
                return _questConditionDataArray[index].ConditionValue;
            return _questConditionData.ConditionValue;
        }


        public bool HasQuestGoal(int index, int value)
        {
            if (IsMultipleQuest)
            {
                return _questConditionDataArray[index].ConditionValue <= value;
            }
            return _questConditionData.ConditionValue <= value;
        }

        public IAssetData GetRewardAssetData(int index)
        {
            if (IsMultipleQuest)
            {
                return _questConditionDataArray[index].RewardAssetData;
            }
            return _questConditionData.RewardAssetData;
        }

        public bool IsConditionQuestData<T>(int index) where T : IConditionQuestData
        {
            if (IsMultipleQuest)
            {
                return _questConditionDataArray[index].ConditionQuestData is T;
            }
            return _questConditionData.ConditionQuestData.GetType() == typeof(T);
        }
        public bool IsConditionQuestData(System.Type type, int index)
        {
            //IConditionQuestData 확인 조건 필요
            if (IsMultipleQuest)
            {
                return _questConditionDataArray[index].ConditionQuestData.GetType() == type;
            }
            return _questConditionData.ConditionQuestData.GetType() == type;
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static QuestData Create_Test(string key, TYPE_QUEST_GROUP typeQuestGroup, System.Type conditionType, int value, System.Type assetType, int assetValue)
        {
            return new QuestData(key, typeQuestGroup, conditionType, value, assetType, assetValue);
        }

        private QuestData(string key, TYPE_QUEST_GROUP typeQuestGroup, System.Type conditionType, int conditionValue, System.Type assetType, int assetValue)
        {
            _key = key;
            _typeQuestGroup = typeQuestGroup;
            _questConditionData = QuestConditionData.Create_Test(conditionType, conditionValue, assetType, assetValue);           
        }
        public void SetQuestDataArray_Test(QuestConditionData[] arr)
        {
            _isMultipleQuest = true;
            _questConditionDataArray = arr;
        }
#endif
    }

    [System.Serializable]
    public struct QuestConditionData
    {
        [SerializeField]
        private SerializedConditionQuestData _serializedConditionQuestData;

        [NonSerialized]
        private IConditionQuestData _conditionQuestData;

        /// <summary>
        /// null 가능
        /// </summary>
        public IConditionQuestData ConditionQuestData
        {
            get
            {
                if (_conditionQuestData == null)
                {
                    _conditionQuestData = _serializedConditionQuestData.GetSerializeData();
                }
                return _conditionQuestData;
            }
        }


        [SerializeField]
        private int _conditionValue;
        public int ConditionValue => _conditionValue;

        [SerializeField]
        private SerializedAssetData _serializedAssetData;

        [NonSerialized]
        private IAssetData _rewardAssetData;
        public IAssetData RewardAssetData {
            get
            {
                if(_rewardAssetData == null)
                {
                    _rewardAssetData = _serializedAssetData.GetData();
                }
                return _rewardAssetData;
            }
        }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public static QuestConditionData Create_Test(System.Type conditionType, int conditionValue, System.Type assetType, int assetValue)
        {
            return new QuestConditionData(conditionType, conditionValue, assetType, assetValue);
        }

        private QuestConditionData(System.Type conditionType, int conditionValue, System.Type assetType, int assetValue)
        {
            _serializedConditionQuestData = default;
            _conditionQuestData = SerializedConditionQuestData.GetData(conditionType);
            _conditionValue = conditionValue;
            _serializedAssetData = default;
            _rewardAssetData = SerializedAssetData.GetData(assetType, assetValue);

            Debug.Assert(_conditionQuestData != null, "_conditionQuestData 가 null입니다");
            Debug.Assert(_rewardAssetData != null, "_rewardAssetData 가 null입니다");
        }
#endif

    }

    [System.Serializable]
    public struct SerializedConditionQuestData
    {
        [SerializeField, ConditionQuestDataListToPopup]
        private string _classTypeName;

        internal IConditionQuestData GetSerializeData()
        {
            //ClassName에 맞춰 데이터 가져오기
            var type = System.Type.GetType(_classTypeName);
            if (type != null) return GetData(type);
            return null;
        }

        public static IConditionQuestData GetData(System.Type type)
        {
            return (IConditionQuestData)Activator.CreateInstance(type);
        }
    }


}