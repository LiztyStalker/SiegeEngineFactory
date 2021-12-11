namespace Storage
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Networking;


    /// <summary>
    /// UnityWebRequest.Result와 1대 1대응
    /// </summary>
    public enum TYPE_IO_RESULT
    {
        InProgress = 0,
        Success = 1,
        ConnectionError = 2,
        ProtocolError = 3,
        DataProcessingError = 4
    };


    public class AssetLoader : MonoBehaviour
    {

        private readonly string PATH_ASSET_BUNDLE = "Address";


        public void Load(System.Action<float> loadCallback, System.Action<TYPE_IO_RESULT> endCallback)
        {
            StartCoroutine(LoadCoroutine(loadCallback, endCallback));
        }

        private IEnumerator LoadCoroutine(System.Action<float> loadCallback, System.Action<TYPE_IO_RESULT> endCallback)
        {

#if UNITY_EDITOR
            //TestCode
            var nowTime = 0f;

            while (true)
            {
                nowTime += Time.deltaTime;
                if (nowTime < 1f)
                {
                    loadCallback?.Invoke(nowTime);
                }
                else
                {
                    endCallback?.Invoke(TYPE_IO_RESULT.Success);
                    break;
                }
                yield return null;
            }

#else



            UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(PATH_ASSET_BUNDLE);
            UnityWebRequestAsyncOperation op = www.SendWebRequest();
            
            Debug.Assert(www.result == UnityWebRequest.Result.Success, $"bundle Load 실패 {www.error}");

            while (true)
            {
                if(op.progress < 1f)
                {
                    loadCallback?.Invoke(op.progress);
                }
                else
                {
                    break;
                }
                yield return null;
            }

            switch (www.result)
            {
                case UnityWebRequest.Result.Success:
                    AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
                    Debug.Log("bundle Load 완료");
                    endCallback?.Invoke((TYPE_IO_RESULT)www.result);
                    yield break;
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.InProgress:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError($"bundle Load 실패 {www.error}");
                    endCallback?.Invoke((TYPE_IO_RESULT)www.result);
                    break;
                default:
                    break;
            }
            yield return null;
#endif
        }
    }
}