using UnityEngine;

namespace ItemsSystem
{
    public class WeaponSO : ItemBase
    {
        [SerializeField] private WeaponType _weaponType;
        [SerializeField] private AudioClip _equipSound;

        [Header("Sway settings")]
        [SerializeField] private float _swayIntensity;
        [SerializeField] private float _swaySmooth;

        [Header("Hold settings")]
        [SerializeField] private Vector3 _holdOffset;
        [SerializeField] private Quaternion _holdRotation;

        public WeaponType Type => _weaponType;
        public AudioClip EquipSound => _equipSound;
        public float SwayIntensity => _swayIntensity;
        public float SwaySmooth => _swaySmooth;
        public Vector3 HoldOffset => _holdOffset;
        public Quaternion HoldRotation => _holdRotation;
    }

    public enum WeaponType
    {
        Melee,
        Range,
        Throwable
    }
}