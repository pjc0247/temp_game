using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NewtonVR;

public class MenuPanel : MonoBehaviour {

    public void OnResume()
    {
        Destroy(gameObject);
    }

    public void OnEndGame()
    {
    }

    public void OnSetting()
    {
        var settingPrefab = Resources.Load<GameObject>("UI/SettingCanvas");
        var settingCanvas = Instantiate(settingPrefab);
        settingCanvas.transform.position = new Vector3(0, 4.4f, 0);

        NVRCanvasInput.RegisterCanvas(settingCanvas);

        Destroy(gameObject);
    }
}
