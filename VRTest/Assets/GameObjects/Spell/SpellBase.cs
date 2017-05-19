using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NewtonVR;

public class SpellBase : MonoBehaviour {
    /// <summary>발사 사이 간격 (쿨타임)</summary>
    public float interval = 0.5f;

    public TextMesh ammoText;
    public int maxAmmo = 10;

    protected int currentAmmo;

    private NVRHand hand;
    private bool canFire = true;

    protected virtual void Awake()
    {
        currentAmmo = maxAmmo;

        if (ammoText != null)
            ammoText.text = currentAmmo.ToString();
    }

    protected virtual void Update()
    {
        if (canFire == false) return;

        var rightHand = NVRPlayer.Instance.RightHand;

        if (rightHand.UseButtonPressed)
        {
            Cast();

            if (interval > 0.0f)
                StartCoroutine(ReloadFunc());

            DecreaseAmmo();  
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

    protected void DecreaseAmmo()
    {
        currentAmmo -= 1;
        if (ammoText != null)
            ammoText.text = currentAmmo.ToString();

        if (currentAmmo == 0)
        {
            OnOutOfAmmo();
            hand.EndInteraction(GetComponent<NVRInteractableItem>());
        }
    }

    /// <summary>
    /// 총알 다 쓰면 호출되는 콜백
    /// </summary>
    protected virtual void OnOutOfAmmo()
    {
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
        hand = GetComponent<NVRInteractableItem>().AttachedHand;
        hand.SetVisibility(VisibilityLevel.Invisible);
    }
    public virtual void OnEndGrab()
    {
        hand.SetVisibility(VisibilityLevel.Visible);

        Destroy(this);
        Destroy(gameObject, 3);
    }
}
