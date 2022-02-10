namespace SEF.Manager
{
    using SEF.Account;
    using SEF.Data;
    using SEF.Entity;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Storage;

    public class BlacksmithManager
    {

        private List<BlacksmithLine> _list;

        public static BlacksmithManager Create()
        {
            return new BlacksmithManager();
        }

        public void Initialize(IAccountData accountData)
        {
            _list = new List<BlacksmithLine>();
            var arr = DataStorage.Instance.GetAllDataArrayOrZero<BlacksmithData>();

            //Debug.Log(arr.Length);

            for(int i = 0; i < arr.Length; i++)
            {
                var line = CreateLine();
                line.SetData(arr[i]);
                _list.Add(line);
            }

            if (accountData != null)
            {
                //accountData 적용
                UnityEngine.Debug.Log("AccountData Load");
            }
        }

        public void CleanUp()
        {
            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].CleanUp();
            }
            _list.Clear();
        }

        public void Refresh()
        {
            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].RunProcess(0f);
            }
        }

        public void RunProcess(float deltaTime)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].RunProcess(deltaTime);
            }
        }

        public IAssetData Upgrade(int index)
        {
            return _list[index].Upgrade();
        }

        public void UpTech(int index)
        {
            Debug.Log("UpTech" + index);
        }

        private BlacksmithLine CreateLine()
        {
            var line = BlacksmithLine.Create();
            line.Initialize();
            line.SetIndex(_list.Count);
            line.SetOnRefreshListener(OnRefreshEvent);
            //line.SetOnStatusPackageListener(GetStatusPackage);
            _list.Add(line);
            return line;
        }


        #region ##### Listener #####


        private System.Action<int, BlacksmithEntity> _refreshEvent;
        public void AddOnRefreshListener(System.Action<int, BlacksmithEntity> act) => _refreshEvent += act;
        public void RemoveOnRefreshListener(System.Action<int, BlacksmithEntity> act) => _refreshEvent -= act;
        private void OnRefreshEvent(int index, BlacksmithEntity unitEntity)
        {
            _refreshEvent?.Invoke(index, unitEntity);
        }

        //private System.Func<StatusPackage> _statusPackageEvent;
        //public void SetOnStatusPackageListener(System.Func<StatusPackage> act) => _statusPackageEvent = act;
        //private StatusPackage GetStatusPackage() => _statusPackageEvent();

        //LineEvent

        #endregion



        #region ##### Data #####
        public IAccountData GetAccountData()
        {
            return null;
        }

        #endregion



#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
        public int Count => _list.Count;

        /// <summary>
        /// 초기화 테스트
        /// </summary>
        /// <param name="accountData"></param>
        public void Initialize_Test(IAccountData accountData)
        {
            if (accountData == null)
            {
                _list = new List<BlacksmithLine>();
                var line = CreateLine();
                line.SetIndex(Count);
            }
            else
            {
                UnityEngine.Debug.Log("AccountData Load Test");
            }
        }

        //언락 테스트

        //한계 테스트
#endif
    }
}