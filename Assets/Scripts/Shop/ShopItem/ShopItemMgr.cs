using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemMgr : SingletonMonoBehavior<ShopItemMgr>
{
    private GameObject shopItemPrefab;
    public List<ShopItemView> shopItemViewList = new List<ShopItemView>();

    private void Awake()
    {
        RefDataMgr.Instance.InitBasic();
        shopItemPrefab = LocalAssetMgr.Instance.Load_Prefab("ShopItem");
    }


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            CreateShopItem((i+1) * 100);
        }
        Refresh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateShopItem(int _id)
    {
        GameObject ShopItem = ObjectPool.Instance.Get("ShopItem", transform, false);
        ShopItemView shopItemView = new ShopItemView(ShopItem);
        ShopItemInfo shopItemInfo = new ShopItemInfo(_id);
        shopItemView.SetData(shopItemInfo);
        shopItemViewList.Add(shopItemView);
    }

    public void Refresh()
    {
        foreach(ShopItemView shopitem in shopItemViewList)
        {
            shopitem.Refresh();
        }
        RefreshPet();
    }


    public void RefreshPet()
    {
        if (PlayerManager.instance.Pet != null)
        {
            ObjectPool.Instance.Recycle(PlayerManager.instance.Pet);
        }

        if (ShopUI.Instance.ShopInfo.InUseID == 100)
        {
            PlayerManager.instance.Pet = ObjectPool.Instance.Get("Pet/Cat", false);
        }

        else if (ShopUI.Instance.ShopInfo.InUseID == 200)
        {
            PlayerManager.instance.Pet = ObjectPool.Instance.Get("Pet/Pepe", false);
        }
    }
}
