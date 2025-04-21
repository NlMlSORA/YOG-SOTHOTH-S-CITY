using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemInfo
{
    public const string ISBUY = "ISBUY";
    public int id;
    public string iconPath;
    public string title;
    public string description;
    public int cost;
    private bool isBuy;
    public bool IsBuy
    {
        get => isBuy;
        set
        {
            isBuy = value;
            LocalSave.SetBool(id.ToString() + ISBUY, value);
        }
    }

    RefShop refShop;


    public ShopItemInfo(int _id)
    {
        id = _id;
        refShop = RefShop.GetRef(id);
        iconPath = refShop.IconPath;
        title = refShop.Title;
        description = refShop.Description;
        cost = refShop.Cost;
        IsBuy = LocalSave.GetBool(id.ToString() + ISBUY, false);
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
        if (CurrencyMgr.Instance.Gold >= cost)
        {
            CurrencyMgr.Instance.Gold -= cost;
            IsBuy = true;
            return true;
        }
        return false;
    }

    public void UseShopItem()
    {
        ShopUI.Instance.ShopInfo.InUseID = id;
    }







}
