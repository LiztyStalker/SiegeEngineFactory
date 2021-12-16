using UnityEngine;
using UnityEngine.EventSystems;
public class UISimpleAudioPlayer : MonoBehaviour, IPointerUpHandler
{
    [SerializeField]
    private string _audioKey;
    public void OnPointerUp(PointerEventData eventData)
    {
        AudioManager.Activate(_audioKey, AudioManager.TYPE_AUDIO.SFX, false);
    }
}
