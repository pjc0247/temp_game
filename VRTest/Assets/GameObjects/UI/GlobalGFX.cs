using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.ImageEffects;

public class GlobalGFX : MonoBehaviour {
    public static GlobalGFX instance;

    private ScreenOverlay overlay;

    private Coroutine redOverlayCoro;

    public GlobalGFX()
    {
        instance = this;
    }
    void Awake()
    {
        overlay = GetComponent<ScreenOverlay>();
    }

    public void StartRedOverlay()
    {
        if (redOverlayCoro != null)
            StopCoroutine(redOverlayCoro);

        StartCoroutine(RedOverlayFunc());
    }
    IEnumerator RedOverlayFunc()
    {
        var intensity = 2.0f;
        for (int i = 0; i < 60; i++)
        {
            overlay.intensity = intensity;
            intensity += (0 - intensity) * 0.07f;
            yield return new WaitForEndOfFrame();
        }
    }
}
