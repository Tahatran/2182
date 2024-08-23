using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new item", menuName = "ScriptableObject/ShopItem")]
public class ShopItemSO : ScriptableObject
{
    public List<ItemData> Items = new List<ItemData>();
    public Sprite lockImg;

    public ItemData GetDataById(int id)
    {
        foreach (ItemData item in Items)
        {
            if (id == item.Id) return item;
        }
        return null;
    }
}

[System.Serializable]
public class ItemData
{
    public int Id;
    // public string Name;
    public bool IsBuy = false;
    public Sprite ItemImg;
    public GameObject ItemPrefab;
    public Sprite PreviewImg;
    public Sprite activeLevel;
    // public int Price;
}


