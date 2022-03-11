using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField]
    Image image;
    [SerializeField]
    GameObject lockImage, check, border;

    public Accessory accessory { get; set; }

    public void Init(Accessory accessory)
    {
        image.sprite = accessory.icon;
        this.accessory = accessory;
        LoadStatus();
        DeSelect();
    }
    public void LoadStatus() 
    {
        lockImage.SetActive(!accessory.IsBought());
        check.SetActive(accessory.IsWearing());
    }

    public void Select()
    {
        Shop.Select(this);
        border.SetActive(true);
    }
    public void DeSelect()
    {
        border.SetActive(false);
    }

    

    //public void SetWear(bool isWearing)
    //{
    //    check.SetActive(isWearing);
    //}
    //public void SetUnlock(bool isLocking)
    //{
    //    lockImage.SetActive(isLocking);
    //}
}
