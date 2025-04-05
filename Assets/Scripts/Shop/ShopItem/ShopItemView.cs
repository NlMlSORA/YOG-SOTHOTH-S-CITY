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
    private TextMeshProUGUI textDesc;
    private TextMeshProUGUI textTitle;
    private TextMeshProUGUI textCost;
    private Button btnBuy;
    


    public ShopItemView(GameObject _itemGO)
    {
        ItemGO = _itemGO;
        imgIcon = ItemGO.GetChildControl<Image>("imgIcon");
        imgCoin = ItemGO.GetChildControl<Image>("textCost/imgCoin");
        textDesc = ItemGO.GetChildControl<TextMeshProUGUI>("textDesc");
        textTitle = ItemGO.GetChildControl<TextMeshProUGUI>("textTitle");
        textCost = ItemGO.GetChildControl<TextMeshProUGUI>("textCost");
        btnBuy = ItemGO.GetComponent<Button>();
        btnBuy.onClick.AddListener(OnBtnBuyClick);
    }

    public void SetData(ShopItemInfo _shopItemInfo)
    {
        ShopItemInfo = _shopItemInfo;
        Refresh();
    }

    public void Refresh()
    {
        imgIcon.sprite = ShopItemInfo.GetItemSprite();
        imgCoin.sprite = ShopItemInfo.GetCoinSprite();
        textDesc.text = ShopItemInfo.description;
        textTitle.text = ShopItemInfo.title;
        textCost.text = ShopItemInfo.cost.ToString();
    }

    public void OnBtnBuyClick()
    {
        ShopItemInfo.TryBuyShopItem();
    }












}
