using UnityEngine;
using ItemsSystem;

namespace Core
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerAudioController : MonoBehaviour
    {
        private AudioSource _audioSource;
        private PlayerItemHolder _itemHolder;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _itemHolder = GetComponent<PlayerItemHolder>();
        }

        private void OnEnable()
        {
            PhysicalItemBase.OnPickupAudioClipTriggered += PlayAudioClip;
            _itemHolder.OnItemEquipped += PlayAudioClip;
        }

        private void OnDisable()
        {
            PhysicalItemBase.OnPickupAudioClipTriggered -= PlayAudioClip;
            _itemHolder.OnItemEquipped -= PlayAudioClip;
        }

        private void PlayAudioClip(AudioClip audioClip)
        {
            _audioSource.PlayOneShot(audioClip);
        }
    }
}
