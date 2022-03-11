using System.Collections;
using UnityEngine;
using Commons.CollectionCommon;

[CreateAssetMenu(fileName = "Accessory", menuName = "Accessory", order = 1)]
public class Accessory : ScriptableObject
{
    public GameObject prefab;
    public string accessoryType;
    public int price;
    public Sprite icon;
    public string name;
    public ShopItem ownItem { get; set; } 
    public bool IsWearing()
    {
        return Main.wearingList.GetValue(accessoryType) == name;
        
    }
    public bool IsBought()
    {
        return Main.boughtAccessory.Contains(name);
    }
}

