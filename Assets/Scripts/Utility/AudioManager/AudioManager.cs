using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Storage;

public class AudioManager
{
    public enum TYPE_AUDIO { BGM, SFX, Environment, UI}

    private static GameObject _gameObject;

    private static GameObject gameObject
    {
        get
        {
            if (_gameObject == null)
            {
                _gameObject = new GameObject();
                _gameObject.transform.position = Vector3.zero;
                _gameObject.name = "Manager@Audio";
                Object.DontDestroyOnLoad(_gameObject);
            }
            return _gameObject;
        }
    }

    private static Dictionary<TYPE_AUDIO, List<AudioActor>> _activateDic = new Dictionary<TYPE_AUDIO, List<AudioActor>>();

    private static List<AudioActor> _inactiveList = new List<AudioActor>();

    private static AudioClip GetClip(string clipKey) => DataStorage.Instance.GetDataOrNull<AudioClip>(clipKey, null, null);

    /// <summary>
    /// AudioData�� GameObject Instanceȭ �մϴ�
    /// EditMode : ������� �ʽ��ϴ�
    /// Play : GameObject�� �����˴ϴ�
    /// </summary>
    /// <param name="effectData"></param>
    /// <param name="position"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static AudioActor Activate(string clipKey, TYPE_AUDIO typeAudio, bool isLoop = false)
    {
        if (Application.isPlaying)
        {
            if (!string.IsNullOrEmpty(clipKey))
            {
                var clip = GetClip(clipKey);
                return Activate(clip, typeAudio, isLoop);
            }
        }
        return null;
    }

    /// <summary>
    /// AudioData�� GameObject Instanceȭ �մϴ�
    /// EditMode : ������� �ʽ��ϴ�
    /// Play : GameObject�� �����˴ϴ�
    /// </summary>
    /// <param name="effectData"></param>
    /// <param name="position"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static AudioActor Activate(AudioClip clip, TYPE_AUDIO typeAudio, bool isLoop = false)
    {
        if (Application.isPlaying)
        {
            if (clip != null)
            {
                var actor = GetActor(typeAudio);
                actor.name = $"AudioActor_{typeAudio}_{clip.name}";
                actor.SetData(clip, isLoop);
                actor.SetData(typeAudio);
                actor.Play();
                return actor;
            }
        }
        return null;
    }

    /// <summary>
    /// AudioActor�� �����մϴ�
    /// </summary>
    /// <param name="effectData"></param>
    public static void Inactivate(AudioActor actor)
    {
        actor.Stop();
        RetrieveActor(actor);
    }

    public static void Inactivate(string clipKey, TYPE_AUDIO typeAudio)
    {
        AudioActor actor = null;
        if (_activateDic.ContainsKey(typeAudio))
        {
            var list = _activateDic[typeAudio];
            for(int i = 0; i < list.Count; i++)
            {
                if (list[i].IsEqualKey(clipKey))
                {
                    actor = list[i];
                    break;
                }
            }
        }

        if(actor != null)
        {
            actor.Stop();
            RetrieveActor(actor);
        }
    }

    private static AudioActor GetActor(TYPE_AUDIO typeAudio)
    {
        AudioActor actor = null;
        for (int i = 0; i < _inactiveList.Count; i++)
        {
            actor = _inactiveList[i];
        }

        if (!_activateDic.ContainsKey(typeAudio))
        {
            _activateDic.Add(typeAudio, new List<AudioActor>());
        }

        if (actor == null)
        {
            actor = CreateActor();
        }
        else
        {
            _inactiveList.Remove(actor);
        }
        _activateDic[typeAudio].Add(actor);
        return actor;
    }

    private static AudioActor CreateActor()
    {
        var gameObejct = new GameObject();
        var actor = gameObejct.AddComponent<AudioActor>();
        actor.SetOnStoppedListener(RetrieveActor);
        actor.transform.SetParent(gameObject.transform);
        return actor;
    }

    private static void RetrieveActor(AudioActor actor)
    {
        var typeAudio = actor.typeAudio;
        if (_activateDic.ContainsKey(typeAudio))
        {
            var list = _activateDic[typeAudio];
            if (list.Contains(actor))
            {
                list.Remove(actor);
                _inactiveList.Add(actor);
            }
        }
    }

}
