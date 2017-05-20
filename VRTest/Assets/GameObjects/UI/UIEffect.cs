using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NewtonVR;

public class UIEffect : MonoBehaviour {
    private static UIEffect instance;

    private static GameObject clickParticlePrefab;

    private Coroutine grabHapticCoro;

    public UIEffect()
    {
        instance = this;
    }
    void Awake()
    {
        clickParticlePrefab = Resources.Load<GameObject>("UI/UIEffect/ClickParticle");
    }
    
    public static void StartGrabHaptic()
    {
        instance.grabHapticCoro = instance.StartCoroutine(instance.GrabHapticFunc());
    }
    public static void StopGrabHaptic()
    {
        instance.StopCoroutine(instance.grabHapticCoro);
    }
    IEnumerator GrabHapticFunc()
    {
        var hand = NVRPlayer.Instance.RightHand;
        while (true)
        {
            hand.TriggerHapticPulse(333, 0.6f);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public static void PlayHoverHaptic()
    {
        var hand = NVRPlayer.Instance.RightHand;

        hand.LongHapticPulse(0.01f);
    }

    public static void PlayClickEffect()
    {
        var particle = Instantiate(clickParticlePrefab);
        var hand = NVRPlayer.Instance.RightHand;

        particle.transform.position = hand.transform.position;

        hand.LongHapticPulse(0.1f);
    }
}
