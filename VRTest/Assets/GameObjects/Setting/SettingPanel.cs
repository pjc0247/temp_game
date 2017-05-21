using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using NewtonVR;

public class SettingPanel : MonoBehaviour {
    public Text camHeightText;

    private float camHeight = 0;

    public void OnIncrementCamHeight()
    {
        var t = NVRPlayer.Instance.transform;

        t.position = new Vector3(t.position.x, t.position.y + 0.5f, t.position.z);

        camHeight += 0.5f;
        camHeightText.text = camHeight.ToString();
    }
    public void OnDecrementCamHeight()
    {
        var t = NVRPlayer.Instance.transform;

        t.position = new Vector3(t.position.x, t.position.y - 0.5f, t.position.z);

        camHeight -= 0.5f;
        camHeightText.text = camHeight.ToString();
    }

    public void OnClose()
    {
        Destroy(gameObject);
    }
}
