using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemView
{
    public ShopItemInfo ShopItemInfo;

    public GameObject ItemGO;
    private Image imgIcon;
    private Image imgCoin;
    private Image imgMask;
    private TextMeshProUGUI textDesc;
    private TextMeshProUGUI textTitle;
    private TextMeshProUGUI textCost;
    private Button btnShopItem;
    


    public ShopItemView(GameObject _itemGO)
    {
        ItemGO = _itemGO;
        imgIcon = ItemGO.GetChildControl<Image>("imgIcon");
        imgCoin = ItemGO.GetChildControl<Image>("textCost/imgCoin");
        imgMask = ItemGO.GetChildControl<Image>("imgMask");
        textDesc = ItemGO.GetChildControl<TextMeshProUGUI>("textDesc");
        textTitle = ItemGO.GetChildControl<TextMeshProUGUI>("textTitle");
        textCost = ItemGO.GetChildControl<TextMeshProUGUI>("textCost");
        btnShopItem = ItemGO.GetChildControl<Button>("btnShopItem");
        btnShopItem.onClick.AddListener(OnShopItemClick);
    }

    public void SetData(ShopItemInfo _shopItemInfo)
    {
        ShopItemInfo = _shopItemInfo;
        //Refresh();
    }

    public void Refresh()
    {
        //imgIcon.sprite = ShopItemInfo.GetItemSprite();
        imgCoin.sprite = ShopItemInfo.GetCoinSprite();
        textDesc.text = ShopItemInfo.description;
        textTitle.text = ShopItemInfo.title;
        if (!ShopItemInfo.IsBuy)
        {
            textCost.text = ShopItemInfo.cost.ToString();
        }
        else
        {
            imgCoin.enabled = false;
            if (ShopUI.Instance.ShopInfo.InUseID == ShopItemInfo.id)
            {
                imgMask.enabled = true;
                textCost.text = "携带中";
            }
            else
            {
                imgMask.enabled = false;
                textCost.text = "已拥有";
            }
            
        }
        
    }

    public void OnShopItemClick()
    {
        if (!ShopItemInfo.IsBuy)
        {
            ShopItemInfo.TryBuyShopItem();
        }

        if (ShopItemInfo.IsBuy)
        {
            ShopItemInfo.UseShopItem();
            
        }
        ShopItemMgr.Instance.Refresh();
    }












}
