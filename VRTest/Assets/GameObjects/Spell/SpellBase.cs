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
        OnCooldown();

        yield return new WaitForSeconds(interval);

        canFire = true;
        OnRestored();
    }

    /// <summary>
    /// 쿨타임 상태가 되어 쏠 수 없을 때 호출되는 콜백
    /// </summary>
    protected virtual void OnCooldown()
    {
    }
    /// <summary>
    /// 다시 발사 가능할 때 호출되는 함수
    /// </summary>
    protected virtual void OnRestored()
    {
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
