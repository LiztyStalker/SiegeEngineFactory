#if UNITY_EDITOR && UNITY_INCLUDE_TESTS
namespace UtilityManager.Test
{
    using System.Collections;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using UtilityManager;
    using UnityEngine.Experimental.Rendering.Universal;
    using UnityEngine.Rendering.Universal;


    public class AudioManagerTest
    {
        private Camera _camera;
        private Light2D _light;

        private Vector2 _position = new Vector2(-2f, 2f);

        [SetUp]
        public void SetUp()
        {
            _camera = PlayTestUtility.CreateCamera();
            _camera.gameObject.AddComponent<AudioListener>();
            _light = PlayTestUtility.CreateLight();
        }

        [TearDown]
        public void TearDown()
        {
            PlayTestUtility.DestroyCamera(_camera);
            PlayTestUtility.DestroyLight(_light);
        }

        
        #region ##### AudioClip Creater #####

        //https://docs.unity3d.com/ScriptReference/AudioClip.Create.html

        private int position = 0;
        private int samplerate = 44100;
        private float frequency = 440f;


        private AudioClip CreateClip()
        {
            return AudioClip.Create("TestClip", samplerate * 2, 1, samplerate, true, OnAudioRead, OnAudioSetPosition);
        }

        private void OnAudioRead(float[] data)
        {
            int count = 0;
            while (count < data.Length)
            {
                data[count] = Mathf.Sin(2 * Mathf.PI * frequency * position / samplerate);
                position++;
                count++;
            }
        }

        private void OnAudioSetPosition(int newPosition)
        {
            position = newPosition;
        }


        #endregion


        [UnityTest]
        public IEnumerator AudioManagerTest_Activate()
        {
            bool isRun = true;
            var clip = CreateClip();
            AudioManager.Current.Activate(clip, AudioManager.TYPE_AUDIO.SFX, false, actor =>
            {
                isRun = false;
            });
            while (isRun)
            {
                yield return null;
            }
            yield return null;
            AudioManager.Current.CleanUp();
        }

        [UnityTest]
        public IEnumerator AudioManagerTest_Activate5()
        {
            var clip = CreateClip();
            int inactivateCount = 0;
            for (int i = 0; i < 5; i++)
            {
                AudioManager.Current.Activate(clip, AudioManager.TYPE_AUDIO.SFX, false, actor => 
                { 
                    Debug.Log("Inactivate"); inactivateCount++; 
                });
                yield return new WaitForSeconds(1f);
            }

            while (true)
            {
                if (inactivateCount == 5)
                    break;
                yield return null;
            }
            yield return null;
            AudioManager.Current.CleanUp();
        }

        [UnityTest]
        public IEnumerator AudioManagerTest_Activate5_After_Activate3()
        {
            var clip = CreateClip();
            int inactivateCount = 0;
            for (int i = 0; i < 5; i++)
            {
                AudioManager.Current.Activate(clip, AudioManager.TYPE_AUDIO.SFX, false, actor =>
                {
                    Debug.Log("Inactivate"); inactivateCount++;
                });
                yield return new WaitForSeconds(1f);
            }

            while (true)
            {
                if (inactivateCount == 5)
                    break;
                yield return null;
            }
            yield return null;

            for (int i = 0; i < 3; i++)
            {
                AudioManager.Current.Activate(clip, AudioManager.TYPE_AUDIO.SFX, false, actor =>
                {
                    Debug.Log("Inactivate"); inactivateCount++;
                });
                yield return new WaitForSeconds(1f);
            }
            while (true)
            {
                if (inactivateCount == 8)
                    break;
                yield return null;
            }
            yield return null;
            AudioManager.Current.CleanUp();
        }


    }
}
#endif