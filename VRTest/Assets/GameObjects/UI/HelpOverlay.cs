using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NewtonVR;

public class HelpOverlay : MonoBehaviour {
    private NVRHand hand;
    private Transform point;

    private TextMesh text;
    private LineRenderer line;

    private bool shown = false;

    void Awake()
    {
        text = GetComponentInChildren<TextMesh>();
        line = GetComponentInChildren<LineRenderer>();
    }
    void Start()
    {
        hand = NVRPlayer.Instance.RightHand;

        var handModel = hand.RenderModel;
        var bButton = handModel.transform.Find("B");
        point = bButton.transform.Find("Point");
    }
	
	void Update () {
        if (hand.Inputs[NVRButtons.B].IsTouched == false)
        {
            if (shown) Hide();
            return;
        }

        if (shown == false)
            Show();

        transform.position = point.position +
            new Vector3(-0.5f, 1.5f, -2.5f);
        line.SetPositions(new Vector3[] {
            point.position, line.transform.position});
    }

    void Hide()
    {
        text.gameObject.SetActive(false);
        line.gameObject.SetActive(false);

        shown = false;
    }
    void Show()
    {
        text.gameObject.SetActive(true);
        line.gameObject.SetActive(true);

        shown = true;
    }
}
