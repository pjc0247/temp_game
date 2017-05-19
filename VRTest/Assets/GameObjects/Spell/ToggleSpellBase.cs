using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NewtonVR;

public class ToggleSpellBase : SpellBase
{
    private Coroutine TickCoro;

    private bool toggle = false;

    protected override void Awake()
    {
        base.Awake();

        TickCoro = StartCoroutine(TickFunc());
    }
    protected override void Update()
    {
        var rightHand = NVRPlayer.Instance.RightHand;

        if (rightHand.UseButtonPressed)
        {
            if (toggle == false)
                OnBeginCast();
            toggle = true;
        }
        else
        {
            OnEndCast();
            toggle = false;
        }
    }

    IEnumerator TickFunc()
    {
        var rightHand = NVRPlayer.Instance.RightHand;

        while (true)
        {
            if (rightHand.UseButtonPressed)
            {
                Cast();
                DecreaseAmmo();
            }

            yield return new WaitForSeconds(interval);
        }
    }

    protected override void OnOutOfAmmo()
    {
        OnEndCast();
    }
    protected virtual void OnBeginCast()
    {
    }
    protected virtual void OnEndCast()
    {
    }

    public override void OnEndGrab()
    {
        base.OnEndGrab();

        StopCoroutine(TickCoro);
    }
}


