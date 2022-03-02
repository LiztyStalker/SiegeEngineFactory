namespace SEF.Entity
{
    using Data;
    using Status;
    using System.Numerics;
    using Utility.IO;

    public struct AssetEntity : IAssetData
    {
        private IAssetData _limitAssetData;
        private IAssetData _nowAssetData;

        public IAssetData LimitAssetData
        {
            get
            {
                if (_limitAssetData == null)
                {
                    return null;
                }

                if (_limitAssetData is PopulationAssetData)
                {
                    var data = new UniversalAssetData(StatusPackage.Current.GetStatusDataToBigNumberData<IncreaseMaxPopulationStatusData, UniversalBigNumberData>(new UniversalBigNumberData(_limitAssetData.AssetValue)));
                    return data;

                }
                return _limitAssetData;
            }
        }
        public IAssetData NowAssetData => _nowAssetData;

        public AssetEntity(IAssetData nowAssetData)
        {
            _limitAssetData = null;
            _nowAssetData = nowAssetData;
        }

        public void SetLimitAssetData(IAssetData limitAssetData)
        {
            _limitAssetData = limitAssetData;
        }

        public BigInteger AssetValue
        {
            get => _nowAssetData.AssetValue;
            set => _nowAssetData.AssetValue = value;
        }
        public bool IsOverflow(IAssetData data)
        {
            if (LimitAssetData == null) return false;
            return (NowAssetData.AssetValue + data.AssetValue > LimitAssetData.AssetValue);
        }

        public bool IsUnderflow(IAssetData data)
        {
            if (LimitAssetData == null) return false;
            return (NowAssetData.AssetValue - data.AssetValue < 0);
        }

        public string GetValue()
        {
            if (LimitAssetData == null)
            {
                return NowAssetData.GetValue();
            }
            else
            {
                return $"{NowAssetData.GetValue()} / {LimitAssetData.GetValue()}";
            }
        }

        public new System.Type GetType() => NowAssetData.GetType();
        public System.Type AccumulativelyUsedStatisticsType() => NowAssetData.AccumulativelyUsedStatisticsType();
        public System.Type AccumulativelyGetStatisticsType() => NowAssetData.AccumulativelyGetStatisticsType();

        public void SetValue(string value) => NowAssetData.SetValue(value);

        public void SetCompoundInterest(float nowValue, float rate, int length = 1) => NowAssetData.SetCompoundInterest(nowValue, rate, length);
        public void SetIsolationInterest(float nowValue, int length) => NowAssetData.SetIsolationInterest(nowValue, length);

        public INumberData Clone()
        {
            var assetCase = new AssetEntity();
            assetCase._limitAssetData = (IAssetData)_limitAssetData.Clone();
            assetCase._nowAssetData = (IAssetData)_nowAssetData.Clone();
            return assetCase;
        }

        public void CleanUp()
        {
        }

        #region ##### StorableData #####
        public StorableData GetStorableData()
        {
            var data = new AssetEntityStorableData();
            data.SetData(NowAssetData.GetStorableData());
            return data;
        }
        #endregion
    }

}