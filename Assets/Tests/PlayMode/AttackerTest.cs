#if UNITY_EDITOR && UNITY_INCLUDE_TESTS
namespace SEF.Test
{
    using System.Collections;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using UtilityManager;
    using UnityEngine.Experimental.Rendering.Universal;
    using UnityEngine.Rendering.Universal;
    using Data;
    using Unit;
    using UtilityManager.Test;
    using Storage;

    public class AttackerTest
    {
        private Camera _camera;
        private Light2D _light;

        private Vector2 _position = new Vector2(-2f, 2f);

        private AttackerActor _attackActor;
        private AttackerData _attackerData;
        private LevelWaveData _levelWaveData;
        private UpgradeData _upgradeData;

        [SetUp]
        public void SetUp()
        {
            _camera = PlayTestUtility.CreateCamera();
            _light = PlayTestUtility.CreateLight();
            _attackActor = AttackerActor.Create_Test();
            _attackActor.Initialize();
            _attackerData = AttackerData.Create_Test();
            _upgradeData = NumberDataUtility.Create<UpgradeData>();
        }

        [TearDown]
        public void TearDown()
        {
            _attackActor.CleanUp();

            PlayTestUtility.DestroyCamera(_camera);
            PlayTestUtility.DestroyLight(_light);
        }

        

        [UnityTest]
        public IEnumerator AttackerTest_Initialize()
        {
            yield return new WaitForSeconds(1f);
        }


        [UnityTest]
        public IEnumerator AttackerTest_SetData_SkeletonData()
        {
            Spine.Unity.SkeletonDataAsset _skeletonDataAsset = null;
            if(_attackerData.SkeletonDataAsset != null)
            {
                _skeletonDataAsset = _attackerData.SkeletonDataAsset;
            }
            else if (!string.IsNullOrEmpty(_attackerData.SkeletonDataAssetKey))
            {
                _skeletonDataAsset = DataStorage.Instance.GetDataOrNull<Spine.Unity.SkeletonDataAsset>(_attackerData.SkeletonDataAssetKey, null, null);
            }

            _attackActor.SetData(_skeletonDataAsset, _attackerData, _upgradeData);
            _attackActor.Activate();
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        public IEnumerator AttackerTest_SetData_Empty()
        {
            Spine.Unity.SkeletonDataAsset _skeletonDataAsset = null;           

            _attackActor.SetData(_skeletonDataAsset, _attackerData, _upgradeData);
            _attackActor.Activate();
            yield return new WaitForSeconds(1f);
        }


        //[UnityTest]
        //public IEnumerator AttackerTest_Attack_SkeletonData()
        //{
        //    yield return AttackerTest_SetData_SkeletonData();

        //    bool isRun = true;
        //    _attackActor.SetOnAttackListener(data =>
        //    {
        //        Debug.Log("Attack");
        //        isRun = false;
        //    });

        //    while (isRun)
        //    {
        //        _attackActor.RunProcess(Time.deltaTime);
        //        yield return null;
        //    }

        //    yield return new WaitForSeconds(1f);
        //}

        //[UnityTest]
        //public IEnumerator AttackerTest_Attack_SkeletonData_10Cycles()
        //{
        //    yield return AttackerTest_SetData_SkeletonData();

        //    int count = 0;
        //    _attackActor.SetOnAttackListener(data =>
        //    {
        //        Debug.Log("Attack");
        //        count++;
        //    });

        //    while (true)
        //    {
        //        _attackActor.RunProcess(Time.deltaTime);
        //        if (count >= 10)
        //            break;
        //        yield return null;
        //    }

        //    yield return new WaitForSeconds(1f);
        //}

        //[UnityTest]
        //public IEnumerator AttackerTest_Attack_Empty()
        //{
        //    yield return AttackerTest_SetData_Empty();

        //    bool isRun = true;
        //    _attackActor.SetOnAttackListener(data =>
        //    {
        //        Debug.Log("Attack");
        //        isRun = false;
        //    });

        //    while (isRun)
        //    {
        //        _attackActor.RunProcess(Time.deltaTime);
        //        yield return null;
        //    }

        //    yield return new WaitForSeconds(1f);
        //}

        //[UnityTest]
        //public IEnumerator AttackerTest_Attack_Empty_10Cycles()
        //{
        //    yield return AttackerTest_SetData_Empty();

        //    int count = 0;
        //    _attackActor.SetOnAttackListener(data =>
        //    {
        //        Debug.Log("Attack");
        //        count++;
        //    });

        //    while (true)
        //    {
        //        _attackActor.RunProcess(Time.deltaTime);
        //        if (count >= 10)
        //            break;
        //        yield return null;
        //    }

        //    yield return new WaitForSeconds(1f);
        //}

        [UnityTest]
        public IEnumerator AttackerTest_Destroy_SkeletonData()
        {
            yield return AttackerTest_SetData_SkeletonData();

            bool isRun = true;
            _attackActor.SetOnDestroyedListener(() =>
            {
                Debug.Log("Dead");
                isRun = false;
            });

            _attackActor.Destory();

            while (isRun)
            {
                yield return null;
            }

            yield return new WaitForSeconds(1f);
        }


        [UnityTest]
        public IEnumerator AttackerTest_Destroy_Empty()
        {
            yield return AttackerTest_SetData_Empty();

            bool isRun = true;
            _attackActor.SetOnDestroyedListener(() =>
            {
                Debug.Log("Dead");
                isRun = false;
            });

            _attackActor.Destory();

            while (isRun)
            {
                yield return null;
            }

            yield return new WaitForSeconds(1f);
        }


    }
}
#endif