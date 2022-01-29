namespace SEF.Statistics
{
    using SEF.Account;
    using System.Collections.Generic;
    using System.Numerics;
    using System.Linq;
   

    public class StatisticsPackage
    {
        private List<StatisticsEntity> _list;

        public static StatisticsPackage Create()
        {
            return new StatisticsPackage();
        }

        public void Initialize(IAccountData data)
        {
            _list = new List<StatisticsEntity>();

            if(data != null)
            {
                //AccountData �����ϱ�
            }
        }

        public void CleanUp()
        {
            _list.Clear();
        }

        public void AddStatisticsData<T>(int value = 1) where T : IStatisticsData
        {
            AddStatisticsData<T>(new BigInteger(value));
        }
        public void AddStatisticsData<T>(BigInteger value) where T : IStatisticsData
        {
            AddStatisticsData(typeof(T), value);
        }
        public void AddStatisticsData(System.Type type, BigInteger value)
        {
            var iType = type.GetInterface(typeof(IStatisticsData).Name);
            if (iType != null)
            {
                var index = GetIndex(type);
                if (index == -1)
                {
                    _list.Add(StatisticsEntity.Create(type));
                    index = _list.Count - 1;
                }
                var entity = _list[index];
                entity.AddStatisticsData(value);
                _list[index] = entity;
            }
        }

        public void SetStatisticsData<T>(int value) where T : IStatisticsData
        {
            SetStatisticsData<T>(new BigInteger(value));
        }

        public void SetStatisticsData<T>(BigInteger value) where T : IStatisticsData
        {
            SetStatisticsData(typeof(T), value);
        }

        public void SetStatisticsData(System.Type type, BigInteger value)
        {
            var iType = type.GetInterface(typeof(IStatisticsData).Name);
            if (iType != null)
            {

                var index = GetIndex(type);
                if (index == -1)
                {
                    _list.Add(StatisticsEntity.Create(type));
                    index = _list.Count - 1;
                }
                var entity = _list[index];
                entity.SetStatisticsData(value);
                _list[index] = entity;
            }
        }
        private int GetIndex(System.Type type) => _list.FindIndex(entity => entity.GetStatisticsType() == type);
        public BigInteger? GetStatisticsValue<T>() where T : IStatisticsData
        {
            return GetStatisticsValue(typeof(T));
        }

        public BigInteger? GetStatisticsValue(System.Type type)
        {
            var iType = type.GetInterface(typeof(IStatisticsData).Name);
            if (iType != null)
            {
                var index = GetIndex(type);
                if (index != -1)
                {
                    return _list[index].GetStatisticsValue();
                }
            }
            return null;
        }
        public static System.Type FindType(string key, System.Type classType) => System.Type.GetType($"SEF.Statistics.{key}{classType.Name}");

        public IAccountData GetSaveData()
        {
            return null;
        }

    }

    public class StatisticsUtility
    {
        private Dictionary<System.Type, string> _dic;

        private static StatisticsUtility _current;

        public static StatisticsUtility Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new StatisticsUtility();
                }
                return _current;
            }
        }

        public string GetTypeToContext(System.Type type)
        {
            if (_dic.ContainsKey(type))
                return _dic[type];
            return null;
        }

        public System.Type[] GetTypes() => _dic.Keys.ToArray();

        public string[] GetValues() => _dic.Values.ToArray();

        public int FindIndex(System.Type type) => _dic.Keys.ToList().FindIndex(t => t == type);

        private StatisticsUtility()
        {
            _dic = new Dictionary<System.Type, string>();

            _dic.Add(typeof(CreateUnitStatisticsData), "���� ���� ��");
            
            _dic.Add(typeof(DestroyUnitStatisticsData), "���� �ı��� ��");

            _dic.Add(typeof(GoldUsedAssetStatisticsData), "��� �Һ�");
            _dic.Add(typeof(ResourceUsedAssetStatisticsData), "�ڿ� �Һ�");
            _dic.Add(typeof(MeteoriteUsedAssetStatisticsData), "��ö �Һ�");
            _dic.Add(typeof(ResearchUsedAssetStatisticsData), "���� �Һ�");

            _dic.Add(typeof(GoldGetAssetStatisticsData), "��� ȹ��");
            _dic.Add(typeof(ResourceGetAssetStatisticsData), "�ڿ� ȹ��");
            _dic.Add(typeof(MeteoriteGetAssetStatisticsData), "��ö ȹ��");
            _dic.Add(typeof(ResearchGetAssetStatisticsData), "���� ȹ��");

            _dic.Add(typeof(GoldAccumulateAssetStatisticsData), "��� ����");
            _dic.Add(typeof(ResourceAccumulateAssetStatisticsData), "�ڿ� ����");
            _dic.Add(typeof(MeteoriteAccumulateAssetStatisticsData), "��ö ����");
            _dic.Add(typeof(ResearchAccumulateAssetStatisticsData), "���� ����");

            _dic.Add(typeof(TechUnitStatisticsData), "���� ��ũ ���� ��");

            _dic.Add(typeof(UpgradeUnitStatisticsData), "���� ���� ���� ��");

            _dic.Add(typeof(DestroyEnemyStatisticsData), "�� �ı� ��");

            _dic.Add(typeof(ArrivedLevelStatisticsData), "���� ����");

            _dic.Add(typeof(ExpendWorkshopLineStatisticsData), "���ۼ� ���� ���� ��");

            _dic.Add(typeof(UpgradeVillageStatisticsData), "���� ���� ���� ��");
            _dic.Add(typeof(TechVillageStatisticsData), "���� ��ũ ���� ��");

            _dic.Add(typeof(UpgradeBlacksmithStatisticsData), "���尣 ���� ���� ��");
            _dic.Add(typeof(TechBlacksmithStatisticsData), "���尣 ��ũ ���� ��");

            _dic.Add(typeof(SuccessResearchStatisticsData), "���� ���� ��");
        }
    }
}