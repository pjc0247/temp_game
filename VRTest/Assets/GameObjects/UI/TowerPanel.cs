using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NewtonVR;

public class TowerPanel : MonoBehaviour {
    public enum PanelState
    {
        /// <summary>아예 안보이는 상태</summary>
        Hidden,
        /// <summary>누르면 선택됨 문구 표시된 상태</summary>
        Unselected,
        /// <summary>완전히 선택된 상태</summary>
        Selected
    }

    public static TowerPanel instance;

    private TowerViewCamera towerCam;

    private GameObject canvasObject;
    private GameObject preSelectPanel;
    private GameObject postSelectPanel;

    private PanelState state = PanelState.Unselected;

    public TowerPanel()
    {
        instance = this;
    }
    void Awake()
    {
        canvasObject = transform.FindChild("Canvas").gameObject;
        preSelectPanel = canvasObject.transform.FindChild("PreSelect").gameObject;
        postSelectPanel = canvasObject.transform.FindChild("PostSelect").gameObject;

        towerCam = GetComponentInChildren<TowerViewCamera>();

        canvasObject.SetActive(false);
    }

    public void SetTarget(TowerBase tower)
    {
        towerCam.SetTarget(tower);

        postSelectPanel.SetActive(false);
        preSelectPanel.SetActive(true);
        canvasObject.SetActive(true);

        state = PanelState.Unselected;
    }
    public void ClearTarget()
    {
        if (state == PanelState.Selected)
            return;

        state = PanelState.Hidden;

        canvasObject.SetActive(false);
    }

    void Update()
    {
        if (state == PanelState.Hidden)
            return;

        var rightHand = NVRPlayer.Instance.RightHand;
        if (state == PanelState.Unselected && rightHand.UseButtonDown)
            OnSelect();
    }

    // 손을 너무 멀리 치웠을 때, 메뉴 없엠
    void OnTriggerExit(Collider other)
    {
        if (other.transform.parent == null) return;
        if (other.transform.parent.gameObject != NVRPlayer.Instance.RightHand.gameObject)
            return;

        state = PanelState.Unselected;
        ClearTarget();
    }

    void OnSelect()
    {
        preSelectPanel.SetActive(false);
        postSelectPanel.SetActive(true);

        state = PanelState.Selected;

        UIEffect.PlayClickEffect();
    }
}
