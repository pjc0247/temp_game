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

        var bTouched = rightHand.Inputs[NVRButtons.B].IsTouched;
        if (bTouched)
            OnShowHelpOverlay();
        else
            OnHideHelpOverlay();
    }

    void OnShowHelpOverlay()
    {
        var rightHandModel = NVRPlayer.Instance.RightHand.RenderModel;
        var bButton = rightHandModel.transform.Find("B");

        var r = bButton.GetComponent<MeshRenderer>();
        r.material.color = Color.red;
    }
    void OnHideHelpOverlay()
    {
        var rightHandModel = NVRPlayer.Instance.RightHand.RenderModel;
        var bButton = rightHandModel.transform.Find("B");

        var r = bButton.GetComponent<MeshRenderer>();
        r.material.color = Color.white;
    }

    void OnShowMenu()
    {
        if (GameObject.FindGameObjectWithTag("GameUI") != null)
            return;

        var menuPrefab = Resources.Load<GameObject>("UI/MenuCanvas");
        var menu = Instantiate(menuPrefab);
        menu.name = "Menu";
        menu.transform.position = new Vector3(0, 4.4f, 0);

        NVRCanvasInput.RegisterCanvas(menu);
    }
}
