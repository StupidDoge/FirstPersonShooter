using System;
using UnityEngine;

namespace ItemsSystem
{
    public abstract class PhysicalItemBase : MonoBehaviour, IInteractable
    {
        public static Action<ItemBase, int, GameObject> OnItemEquipped;
        public static Action<AudioClip> OnPickupAudioClipTriggered;

        public readonly int baseAmount = 1;

        protected AudioSource audioSource;

        protected virtual void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public abstract void Interact();
    }
}
