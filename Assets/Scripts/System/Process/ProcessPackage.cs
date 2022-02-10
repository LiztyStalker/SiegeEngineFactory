namespace SEF.Process
{
    using SEF.Account;
    using System.Collections.Generic;
    using System.Linq;

    public interface IProcessProvider { }

    public class ProcessPackage
    {
        private Dictionary<IProcessProvider, ProcessEntity> _dic;

        public static ProcessPackage Create()
        {
            return new ProcessPackage();
        }

        public void Initialize(IAccountData data)
        {
            _dic = new Dictionary<IProcessProvider, ProcessEntity>();

            if (data != null)
            {
                //AccountData 적용하기
            }
        }

        public void CleanUp()
        {
            _dic.Clear();
        }

        public void SetProcessEntity(IProcessProvider provider, ProcessEntity entity)
        {
            if (!_dic.ContainsKey(provider))
            {
                _dic.Add(provider, entity);
            }
            else
            {
                _dic[provider] = entity;
            }
        }

        public void RemoveProcessEntity(IProcessProvider provider)
        {
            if (_dic.ContainsKey(provider))
            {
                _dic.Remove(provider);
            }
        }

        public void RunProcess(float deltaTime)
        {
            var keys = _dic.Keys.ToArray();

            for(int i = 0; i < keys.Length; i++)
            {
                var key = keys[i];
                var entity = _dic[key];
                entity.RunProcess(deltaTime, OnCompleteProcessEvent);
                _dic[key] = entity;
            }
        }


        public IAccountData GetSaveData()
        {
            return null;
        }

        #region ##### Listener #####

        private System.Action<ProcessEntity> _completeProcessEvent;
        public void AddOnCompleteProcessEvent(System.Action<ProcessEntity> act) => _completeProcessEvent += act;
        public void RemoveOnCompleteProcessEvent(System.Action<ProcessEntity> act) => _completeProcessEvent -= act;
        private void OnCompleteProcessEvent(ProcessEntity entity) => _completeProcessEvent?.Invoke(entity);
        
        #endregion
    }
}