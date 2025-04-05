using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemInfo
{
    public int id;
    public string iconPath;
    public string title;
    public string description;
    public int cost;

    RefShop refShop;


    public ShopItemInfo(int _id)
    {
        id = _id;
        refShop = RefShop.GetRef(id);
        iconPath = refShop.IconPath;
        title = refShop.Title;
        description = refShop.Description;
        cost = refShop.Cost;
    }

    public Sprite GetItemSprite()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Atlas/UI/Bottles");

        foreach (Sprite sprite in sprites)
        {
            if(sprite.name == iconPath)
            {
                return sprite;
            }
        }

        Debug.Log("NO Sprite Name : " +  iconPath);
        return null;
    }

    public Sprite GetCoinSprite()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Atlas/UI/Coin");

        foreach (Sprite sprite in sprites)
        {
            if (sprite.name == "1")
            {
                return sprite;
            }
        }

        return null;
    }

    public bool TryBuyShopItem()
    {
        Debug.Log(CurrencyMgr.Instance.Gold);
        Debug.Log(cost);
        if (CurrencyMgr.Instance.Gold >= cost)
        {
            CurrencyMgr.Instance.Gold -= cost;
            return true;
        }
        return false;
    }








}
