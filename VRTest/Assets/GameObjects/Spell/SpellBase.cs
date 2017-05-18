using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NewtonVR;

public class SpellBase : MonoBehaviour {
    /// <summary>발사 사이 간격 (쿨타임)</summary>
    public float interval = 0.5f;

    private bool canFire = true;

    protected virtual void Update()
    {
        if (canFire == false) return;

        var rightHand = NVRPlayer.Instance.RightHand;

        if (rightHand.UseButtonPressed)
        {
            Cast();
            StartCoroutine(ReloadFunc());
        } 
    }
    IEnumerator ReloadFunc()
    {
        canFire = false;

        yield return new WaitForSeconds(interval);

        canFire = true;
    }

    public virtual void Cast()
    {

    }
    public virtual void OnBeginGrab()
    {

    }
    public virtual void OnEndGrab()
    {
        Destroy(this);
        Destroy(gameObject, 3);
    }
}
