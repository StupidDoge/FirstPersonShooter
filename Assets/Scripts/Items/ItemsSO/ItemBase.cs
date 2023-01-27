using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private GameObject _itemPrefab;

    public string Name => _name;
    public Sprite ItemSprite => _sprite;
    public GameObject ItemPrefab => _itemPrefab;
}
