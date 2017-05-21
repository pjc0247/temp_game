using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NewtonVR;

public class GameHotkey : MonoBehaviour {

    void Update() {
        var rightHand = NVRPlayer.Instance.RightHand;

        var b = rightHand.Inputs[NVRButtons.B].IsPressed;
        if (b)
            OnShowMenu();
	}

    void OnShowMenu()
    {
        if (GameObject.Find("Menu") != null)
            return;

        var menuPrefab = Resources.Load<GameObject>("UI/MenuCanvas");
        var menu = Instantiate(menuPrefab);
        menu.name = "Menu";
        menu.transform.position = new Vector3(0, 4.4f, 0);

        NVRCanvasInput.RegisterCanvas(menu);
    }
}
