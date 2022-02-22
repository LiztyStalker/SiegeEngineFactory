namespace SEF.Data
{
    using System.Collections.Generic;
    using System;
    using UnityEngine;
    using Quest;
    using Utility.Data;

    [CreateAssetMenu(fileName = "QuestData", menuName = "ScriptableObjects/QuestData")]

    public class QuestData : ScriptableObjectData
    {
        public enum TYPE_QUEST_GROUP { Daily, Weekly, Challenge, Goal}

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

        public string GetAddressKey(int index)
        {
            if (IsMultipleQuest)
                return _questConditionDataArray[index].ConditionQuestData.AddressKey;
            return _questConditionData.ConditionQuestData.AddressKey;
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
            Key = key;
            _typeQuestGroup = typeQuestGroup;
            _questConditionData = QuestConditionData.Create_Test(conditionType, conditionValue, assetType, assetValue);           
        }
        public void SetQuestDataArray_Test(QuestConditionData[] arr)
        {
            _isMultipleQuest = true;
            _questConditionDataArray = arr;
        }

        public override void SetData(string[] arr)
        {
            Key = arr[(int)QuestDataGenerator.TYPE_SHEET_COLUMNS.Key];

            name = $"{typeof(QuestData).Name}_{Key}";

            _typeQuestGroup = (TYPE_QUEST_GROUP)System.Enum.Parse(typeof(TYPE_QUEST_GROUP), arr[(int)QuestDataGenerator.TYPE_SHEET_COLUMNS.Group]);

            _questConditionData.SetData(
                arr[(int)QuestDataGenerator.TYPE_SHEET_COLUMNS.TypeConditionData],
                arr[(int)QuestDataGenerator.TYPE_SHEET_COLUMNS.ConditionValue],
                arr[(int)QuestDataGenerator.TYPE_SHEET_COLUMNS.TypeRewardAsset],
                arr[(int)QuestDataGenerator.TYPE_SHEET_COLUMNS.RewardAssetValue]
                );

            _questConditionDataArray = null;

            _isMultipleQuest = false;
        }

        public override void AddData(string[] arr)
        {
            _isMultipleQuest = true;

            var list = new List<QuestConditionData>();

            //첫 연계
            if (_questConditionDataArray == null)
            {
                list.Add(_questConditionData);
                _questConditionData = default;
            }
            //두번째 이상 연계
            else
            {
                list.AddRange(_questConditionDataArray);
            }

            var questConditionData = new QuestConditionData();
            questConditionData.SetData(
                arr[(int)QuestDataGenerator.TYPE_SHEET_COLUMNS.TypeConditionData],
                arr[(int)QuestDataGenerator.TYPE_SHEET_COLUMNS.ConditionValue],
                arr[(int)QuestDataGenerator.TYPE_SHEET_COLUMNS.TypeRewardAsset],
                arr[(int)QuestDataGenerator.TYPE_SHEET_COLUMNS.RewardAssetValue]
                );

            list.Add(questConditionData);

            _questConditionDataArray = list.ToArray();
        }

        public override string[] GetData()
        {
            string[] arr = new string[System.Enum.GetValues(typeof(VillageDataGenerator.TYPE_SHEET_COLUMNS)).Length];

            arr[(int)QuestDataGenerator.TYPE_SHEET_COLUMNS.Group] = _typeQuestGroup.ToString();

            _questConditionData.GetData(out string classTypeName, out string conditionValue, out string typeAssetData, out string assetValue);

            arr[(int)QuestDataGenerator.TYPE_SHEET_COLUMNS.TypeConditionData] = classTypeName;
            arr[(int)QuestDataGenerator.TYPE_SHEET_COLUMNS.ConditionValue] = conditionValue;
            arr[(int)QuestDataGenerator.TYPE_SHEET_COLUMNS.TypeRewardAsset] = typeAssetData;
            arr[(int)QuestDataGenerator.TYPE_SHEET_COLUMNS.RewardAssetValue] = assetValue;

            return arr;
        }

        public override string[][] GetDataArray()
        {
            var list = new List<string[]>();
            for(int i = 0; i < _questConditionDataArray.Length; i++)
            {
                string[] arr = new string[System.Enum.GetValues(typeof(VillageDataGenerator.TYPE_SHEET_COLUMNS)).Length];

                arr[(int)QuestDataGenerator.TYPE_SHEET_COLUMNS.Group] = _typeQuestGroup.ToString();

                _questConditionDataArray[i].GetData(out string classTypeName, out string conditionValue, out string typeAssetData, out string assetValue);

                arr[(int)QuestDataGenerator.TYPE_SHEET_COLUMNS.TypeConditionData] = classTypeName;
                arr[(int)QuestDataGenerator.TYPE_SHEET_COLUMNS.ConditionValue] = conditionValue;
                arr[(int)QuestDataGenerator.TYPE_SHEET_COLUMNS.TypeRewardAsset] = typeAssetData;
                arr[(int)QuestDataGenerator.TYPE_SHEET_COLUMNS.RewardAssetValue] = assetValue;

                list.Add(arr);
            }
            return list.ToArray();
        }

        public override bool HasDataArray()
        {
            return _isMultipleQuest;
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

        public void SetData(string classTypeName, string conditionValue, string typeAssetData, string assetValue)
        {
            _serializedConditionQuestData.SetData(classTypeName);
            _conditionValue = int.Parse(conditionValue);
            _serializedAssetData.SetData(typeAssetData, assetValue);
        }

        public void GetData(out string classTypeName, out string conditionValue, out string typeAssetData, out string assetValue)
        {
            _serializedConditionQuestData.GetData(out classTypeName);
            conditionValue = _conditionValue.ToString();
            _serializedAssetData.GetData(out typeAssetData, out assetValue);
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

        public void SetData(string classTypeName)
        {            
            _classTypeName = $"SEF.Quest.{classTypeName}ConditionQuestData";
        }
        public void GetData(out string classTypeName)
        {
            var split = _classTypeName.Split('.');
            var typeName = split[split.Length - 1].Replace("ConditionQuestData", "");
            classTypeName = typeName;
        }
    }


}