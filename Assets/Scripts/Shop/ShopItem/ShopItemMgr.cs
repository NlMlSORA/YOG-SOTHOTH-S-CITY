using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemMgr : MonoBehaviour
{
    private GameObject shopItemPrefab;

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

    }
}
