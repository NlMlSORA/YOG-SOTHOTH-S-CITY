using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInfo : MonoBehaviour
{
    public const string SHOPINUSEID = "SHOPINUSEID";
    private int inUseID;
    public int InUseID{
        get => inUseID; 
        set {
            inUseID = value;
            LocalSave.SetInt(SHOPINUSEID, inUseID);
        }
    }

    public ShopInfo()
    {
        inUseID = LocalSave.GetInt(SHOPINUSEID);
    }










}
