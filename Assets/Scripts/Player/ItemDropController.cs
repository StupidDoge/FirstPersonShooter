using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropController : MonoBehaviour
{
    [SerializeField] private Transform _dropPoint;

    private void OnEnable()
    {
        ItemContextMenu.OnItemDropped += SpawnItem;
    }

    private void OnDisable()
    {
        ItemContextMenu.OnItemDropped -= SpawnItem;
    }

    private void SpawnItem(ItemBase itemSO, int amount)
    {
        GameObject item = Instantiate(itemSO.ItemPrefab);
        item.transform.position = _dropPoint.position;
    }
}
