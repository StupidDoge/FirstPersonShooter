using UnityEngine;
using ItemsSystem;

namespace Core
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerAudioController : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            PhysicalItemBase.OnPickupAudioClipTriggered += PlayAudioClip;
            PhysicalWeaponItem.OnWeaponEquipSoundTriggered += PlayAudioClip;
        }

        private void OnDisable()
        {
            PhysicalItemBase.OnPickupAudioClipTriggered -= PlayAudioClip;
            PhysicalWeaponItem.OnWeaponEquipSoundTriggered += PlayAudioClip;
        }

        private void PlayAudioClip(AudioClip audioClip)
        {
            _audioSource.PlayOneShot(audioClip);
        }
    }
}
