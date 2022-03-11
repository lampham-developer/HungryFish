using System.Collections;
using UnityEngine;
using Commons;
using UnityEngine.UI;
using System.Collections.Generic;
using Commons.CollectionCommon;
using TMPro;

public class Shop : SingletonBehaviour<Shop>
{
    [SerializeField]
    Transform container;
    [SerializeField]
    Button buyBtn, useBtn, removeBtn;
    [SerializeField]
    Transform mannequin;
    [SerializeField]
    TMP_Text txtPrice;

    public ShopItem shopItemTemplate;
    ShopItem selectingShopItem;

    Dictionary<string, GameObject> mannequinWearing;
    private void OnEnable()
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in mannequin)
        {
            Destroy(child.gameObject);
        }
        buyBtn.gameObject.SetActive(false);
        useBtn.gameObject.SetActive(false);
        removeBtn.gameObject.SetActive(false);
        mannequinWearing = new Dictionary<string, GameObject>();
        // init accessoryList to view
        Main.Instance.accessoryList.ForEach(x =>
        {
            x.ownItem = Instantiate(shopItemTemplate, container);
            x.ownItem.Init(x);
            if (x.IsWearing())
            {
                mannequinWearing[x.accessoryType] = Instantiate(x.prefab, mannequin);
            }
        });

    }

    public static void Select(ShopItem shopItem)
    {
        Instance.OnItemSelect(shopItem);
    }

    void OnItemSelect(ShopItem shopItem)
    {
        if (selectingShopItem != null)
        {
            selectingShopItem.DeSelect();
            var oldAcess = selectingShopItem.accessory;
            if (!oldAcess.IsWearing()) // nếu chỉ dang thử thì tháo ra
            {
                Destroy(mannequinWearing[oldAcess.accessoryType]);
                if (shopItem.accessory.accessoryType != oldAcess.accessoryType)
                {
                    var wearing = Main.GetWearing(oldAcess.accessoryType);
                    if (wearing != null)
                    {
                        mannequinWearing[oldAcess.accessoryType] = Instantiate(wearing.prefab, mannequin);

                    }
                }

            }

        }

        selectingShopItem = shopItem;
        var newAcess = shopItem.accessory;
        var duplicateAcc = mannequinWearing.GetValue(newAcess.accessoryType);
        if (duplicateAcc != null)
        {
            Destroy(duplicateAcc);
        }
        txtPrice.text = newAcess.price.ToString();
        mannequinWearing[newAcess.accessoryType] = Instantiate(shopItem.accessory.prefab, mannequin);
        LoadItemStatus();
    }

    // đoạn này xuống dưới ko tối ưu nhưng mà đừng sửa, do nó xảy ra không thường xuyên
    // đánh đổi 1 frame hiệu suất lấy sự check chặt chẽ sẽ đỡ được bug nhiều lắm
    void LoadItemStatus()
    {
        var acces = selectingShopItem.accessory;
        bool isBought = acces.IsBought();
        buyBtn.gameObject.SetActive(!isBought);
        if (isBought)
        {
            bool wearing = acces.IsWearing();
            useBtn.gameObject.SetActive(!wearing);
            removeBtn.gameObject.SetActive(wearing);
        }
        else
        {
            useBtn.gameObject.SetActive(false);
            removeBtn.gameObject.SetActive(false);
            buyBtn.interactable = Main.coin >= acces.price;
        }

    }

    public void Buy()
    {
        Main.Buy(selectingShopItem.accessory);
        LoadItemStatus();
        selectingShopItem.LoadStatus();
    }

    public void Use()
    {
        var wearingItemOld =  Main.GetWearing(selectingShopItem.accessory.accessoryType)?.ownItem;
        Main.Wear(selectingShopItem.accessory);
        LoadItemStatus();
        selectingShopItem.LoadStatus();
        wearingItemOld?.LoadStatus();

    }

    public void Remove()
    {
        Main.Remove(selectingShopItem.accessory.accessoryType);
        LoadItemStatus();
        selectingShopItem.LoadStatus();
    }

}
