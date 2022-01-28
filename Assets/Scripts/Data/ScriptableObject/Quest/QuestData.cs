namespace SEF.Data
{
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
        private QuestConditionData[] _questConditionDataArray;
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
        public IAssetData RewardAssetData => _rewardAssetData;

    }

    [System.Serializable]
    public struct SerializedConditionQuestData
    {
        [SerializeField]
        private string _className;

        internal IConditionQuestData GetSerializeData()
        {
            //ClassName에 맞춰 데이터 가져오기
            return null;
        }
    }

    [System.Serializable]
    public struct SerializedAssetData
    {
        [SerializeField]
        private string _assetData;

        [SerializeField]
        private string _assetValue;

        internal IAssetData GetData()
        {
            //_assetData에 맞춰 데이터 가져오기
            return null;
        }
    }
}