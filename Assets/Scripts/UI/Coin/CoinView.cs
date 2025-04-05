using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class CoinView : MonoBehaviour
{
    private TextMeshProUGUI textCoin;


    private void Awake()
    {
        CurrencyMgr.Instance.Init();
        textCoin = GetComponent<TextMeshProUGUI>();
        textCoin.text = CurrencyMgr.Instance.Gold.ToString();
        Send.RegisterMsg(SendType.GoldChange, UpdateGoldText);

    }


    public void UpdateGoldText(object[] objs)
    {
        DOTween.To(() => int.Parse(textCoin.text), x => textCoin.text = x.ToString(), (int)objs[1], 0.5f);
    }

}
