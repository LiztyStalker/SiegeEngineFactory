namespace SEF.Manager
{
    using SEF.Account;
    using SEF.Data;
    using SEF.Entity;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Storage;

    public class VillageManager : MonoBehaviour
    {

        private List<VillageLine> _list;

        public static VillageManager Create()
        {
            return new VillageManager();
        }

        public void Initialize(IAccountData accountData)
        {
            _list = new List<VillageLine>();
            var arr = DataStorage.Instance.GetAllDataArrayOrZero<VillageData>();

            for (int i = 0; i < arr.Length; i++)
            {
                var line = CreateLine();
                line.SetData(arr[i]);
                _list.Add(line);
            }

            if (accountData != null)
            {
                //accountData ����
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

        private VillageLine CreateLine()
        {
            var line = VillageLine.Create();
            line.Initialize();
            line.SetIndex(_list.Count);
            line.SetOnRefreshListener(OnRefreshEvent);
            line.SetOnStatusPackageListener(GetStatusPackage);
            _list.Add(line);
            return line;
        }


        #region ##### Listener #####


        private System.Action<int, VillageEntity> _refreshEvent;
        public void AddOnRefreshListener(System.Action<int, VillageEntity> act) => _refreshEvent += act;
        public void RemoveOnRefreshListener(System.Action<int, VillageEntity> act) => _refreshEvent -= act;
        private void OnRefreshEvent(int index, VillageEntity entity)
        {
            _refreshEvent?.Invoke(index, entity);
        }

        private System.Func<StatusPackage> _statusPackageEvent;
        public void SetOnStatusPackageListener(System.Func<StatusPackage> act) => _statusPackageEvent = act;
        private StatusPackage GetStatusPackage() => _statusPackageEvent();

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
        /// �ʱ�ȭ �׽�Ʈ
        /// </summary>
        /// <param name="accountData"></param>
        public void Initialize_Test(IAccountData accountData)
        {
            if (accountData == null)
            {
                _list = new List<VillageLine>();
                var line = CreateLine();
                line.SetIndex(Count);
            }
            else
            {
                UnityEngine.Debug.Log("AccountData Load Test");
            }
        }

        //��� �׽�Ʈ

        //�Ѱ� �׽�Ʈ
#endif
    }
}