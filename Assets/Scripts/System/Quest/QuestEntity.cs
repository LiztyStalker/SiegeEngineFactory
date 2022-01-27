namespace SEF.Quest
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public struct QuestEntity
    {
        private IQuestData _data;
        private int _value;

        private QuestEntity(IQuestData data)
        {
            _data = data;
            _value = 0;
        }

        internal void SetQuestData(int value)
        {
            _value = value;
        }

        internal IQuestData GetQuestData() => _data;
        internal int GetQuestValue() => _value;

        internal static QuestEntity Create(IQuestData data)
        {
            return new QuestEntity(data);
        }
    }
}