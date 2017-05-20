using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerViewCamera : MonoBehaviour {
    public static TowerViewCamera instance;

    private GameObject canvas;
    private Camera cam;

    public TowerViewCamera()
    {
        instance = this;
    }
    void Awake()
    {
        canvas = transform.FindChild("Canvas").gameObject;

        cam = GetComponentInChildren<Camera>();
        cam.cullingMask = ~(LMask.VRHand | LMask.UI);
    }

    public void SetTarget(TowerBase tower)
    {
        transform.position = tower.transform.position + new Vector3(0, 2, 0);

        cam.transform.position = tower.transform.position + new Vector3(3, 2, 0);
        cam.transform.LookAt(tower.transform);
    }
}
