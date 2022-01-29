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
        private bool IsMultipleQuest => _isMultipleQuest;

        [SerializeField]
        private QuestConditionData _questConditionData;
        public QuestConditionData QuestConditionData => _questConditionData;

        [SerializeField]
        private QuestConditionData[] _questConditionDataArray = new QuestConditionData[1];
        public QuestConditionData[] QuestConditionDataArray => _questConditionDataArray;


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
                return _questConditionDataArray[index].ConditionValue >= value;
            }
            return _questConditionData.ConditionValue >= value;
        }

        public IAssetData GetRewardAssetData(int index)
        {
            if (IsMultipleQuest)
            {
                return _questConditionDataArray[index].RewardAssetData;
            }
            return _questConditionData.RewardAssetData;
        }

    }

    [System.Serializable]
    public struct QuestConditionData
    {
        [SerializeField]
        private SerializedConditionQuestData _serializedConditionQuestData;

        [NonSerialized]
        private Statistics.IStatisticsData _conditionQuestData;

        /// <summary>
        /// null 가능
        /// </summary>
        public Statistics.IStatisticsData ConditionQuestData
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

        public IAssetData RewardAssetData => _serializedAssetData.GetData();

    }

    [System.Serializable]
    public struct SerializedConditionQuestData
    {
        [SerializeField, Statistics.StatisticsListToPopup]
        private string _classTypeName;

        internal Statistics.IStatisticsData GetSerializeData()
        {
            //ClassName에 맞춰 데이터 가져오기
            var type = System.Type.GetType(_classTypeName);
            if(type != null) return (Statistics.IStatisticsData)Activator.CreateInstance(type);
            return null;
        }
    }


}