using UnityEngine;
using ItemsSystem;

namespace Core
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerAudioController : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            PhysicalItemBase.OnPickupAudioClipTriggered += PlayPickupAudioClip;
        }

        private void OnDisable()
        {
            PhysicalItemBase.OnPickupAudioClipTriggered -= PlayPickupAudioClip;
        }

        private void PlayPickupAudioClip(AudioClip audioClip)
        {
            _audioSource.PlayOneShot(audioClip);
        }
    }
}
