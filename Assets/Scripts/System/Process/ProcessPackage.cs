namespace SEF.Process
{
    using SEF.Account;
    using System.Collections.Generic;

    public class ProcessPackage
    {
        private List<ProcessEntity> _list;

        public static ProcessPackage Create()
        {
            return new ProcessPackage();
        }

        public void Initialize(IAccountData data)
        {
            _list = new List<ProcessEntity>();

            if (data != null)
            {
                //AccountData 적용하기
            }
        }

        public void CleanUp()
        {
            _list.Clear();
        }

        public void SetProcessData(IProcessData data, int value = 1)
        {
            var index = GetIndex(data);
            if (index == -1)
            {
                _list.Add(ProcessEntity.Create(data));
                index = _list.Count - 1;
            }
            var entity = _list[index];
            entity.SetProcessData(value);
            _list[index] = entity;
        }

        private int GetIndex(IProcessData data) => _list.FindIndex(entity => entity.GetProcessData() == data);


        public void RunProcess(float deltaTime)
        {
            for(int i = 0; i < _list.Count; i++)
            {
                var entity = _list[i];
                entity.RunProcess(deltaTime, OnProcessEvent);
                _list[i] = entity;
            }
        }


        public IAccountData GetSaveData()
        {
            return null;
        }

        #region ##### Listener #####

        private System.Action _processEvent;

        public void AddOnProcessEvent(System.Action act) => _processEvent += act;
        public void RemoveOnProcessEvent(System.Action act) => _processEvent -= act;

        private void OnProcessEvent() { _processEvent?.Invoke(); }
        
        #endregion
    }
}