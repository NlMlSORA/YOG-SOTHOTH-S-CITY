using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeAnimatorTrigger : MonoBehaviour
{
    Enemy_Boss enemy => transform.parent.GetComponentInParent<Enemy_Boss>();

    private void Finish()
    {
        enemy.canUp = true;
    }
}
