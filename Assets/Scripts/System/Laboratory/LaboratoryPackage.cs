namespace SEF.Research
{
    using SEF.Account;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class LaboratoryPackage
    {
        private List<ResearchEntity> _list;

        public static LaboratoryPackage Create()
        {
            return new LaboratoryPackage();
        }

        public void Initialize(IAccountData data)
        {
            _list = new List<ResearchEntity>();

            if (data != null)
            {
                //AccountData 적용하기
            }
        }

        public void CleanUp()
        {
            _list.Clear();
        }
               
        public void SetResearchData<T>(int value = 1) where T : IResearchData
        {
            var index = GetIndex<T>();
            if (index == -1)
            {
                _list.Add(ResearchEntity.Create<T>());
                index = _list.Count - 1;
            }
            var entity = _list[index];
            entity.SetResearchData(value);
            _list[index] = entity;
        }

        private int GetIndex<T>() => _list.FindIndex(entity => entity.GetResearchType() == typeof(T));

        public bool HasResearchData<T>(int value = 1) where T : IResearchData
        {
            if (value < 0)
            {
                throw new System.Exception("HasResearchData는 음수를 적용할 수 없습니다");
            }

            var index = GetIndex<T>();
            if (index == -1) return false;

            var entity = _list[index];
            return entity.GetResearchValue() >= value;
        }


        public IAccountData GetSaveData()
        {
            return null;
        }
    }
}