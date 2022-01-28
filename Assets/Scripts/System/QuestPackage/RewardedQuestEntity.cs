namespace SEF.Quest
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public struct RewardedQuestEntity
    {
        private IQuestData _data;
        private int _value;

        public RewardedQuestEntity(IQuestData data)
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

        internal static RewardedQuestEntity Create(IQuestData data)
        {
            return new RewardedQuestEntity(data);
        }
    }
}