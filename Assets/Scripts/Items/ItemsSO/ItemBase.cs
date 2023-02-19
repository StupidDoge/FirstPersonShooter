using UnityEngine;

public abstract class ItemBase : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private AudioClip _itemPickupSound;

    [Header("Item settings")]
    [SerializeField] private bool _equippable;
    [SerializeField] private bool _usable;
    [SerializeField] private bool _stackable;

    public string Name => _name;
    public Sprite ItemSprite => _sprite;
    public GameObject ItemPrefab => _itemPrefab;
    public AudioClip ItemPickupSound => _itemPickupSound;
    public bool Equippable => _equippable;
    public bool Usable => _usable;
    public bool Stackable => _stackable;
}
