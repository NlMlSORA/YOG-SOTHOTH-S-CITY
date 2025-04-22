using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMFMgr : SingletonMonoBehavior<MMFMgr>
{
    private MMF_Player MMF_Player;

    protected override void Awake()
    {
        base.Awake();
        MMF_Player = GetComponent<MMF_Player>();
    }

    public void PlayMMF()
    {
        if (MMF_Player != null)
        {
            MMF_Player.PlayFeedbacks();
        }
    }


}
