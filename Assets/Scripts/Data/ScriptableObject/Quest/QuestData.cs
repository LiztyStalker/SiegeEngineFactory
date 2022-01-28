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
                if(_conditionQuestData == null)
                {
                    _conditionQuestData = _serializedConditionQuestData.GetSerializeData();
                }
                return _conditionQuestData;
            }
        }


        [SerializeField]
        private int _conditionValue;
        public int ConditionValue => _conditionValue;


//        [SerializeField]
        //private SerializedAssetData _serializedAssetData;


//        [SerializeField]
        [NonSerialized]
        private IAssetData _rewardAssetData;
        public IAssetData RewardAssetData => _rewardAssetData;


        [SerializeField]
        private string _nextQuestDataKey;
        public string NextQuestDataKey => _nextQuestDataKey;


        [SerializeField]
        private QuestData _nextQuestData;
        public QuestData NextQuestData => _nextQuestData;
    }

    internal class SerializedConditionQuestData
    {
        [SerializeField]
        private string className;

        internal IConditionQuestData GetSerializeData()
        {
            //ClassName에 맞춰 직렬 데이터 가져오기
            return null;
        }
    }
}