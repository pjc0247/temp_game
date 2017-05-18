using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NewtonVR;

public class Piece : MonoBehaviour {
    public GameObject towerPrefab;

    public bool isTower = true;
    public int price = 100;

    private GameObject infoObject;

    void Awake()
    {
        infoObject = transform.FindChild("Info").gameObject;
        infoObject.SetActive(false);
    }
	void Start () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == null) return;
        if (other.transform.parent.gameObject != NVRPlayer.Instance.RightHand.gameObject)
            return;

        infoObject.SetActive(true);
    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform.parent == null) return;
        if (other.transform.parent.gameObject != NVRPlayer.Instance.RightHand.gameObject)
            return;

        infoObject.SetActive(false);
    }

    public void OnGrab()
    {
        if (Wallet.gold < price)
        {
            OnNotEnoughGold(); return;
        }

        NVRInteractableItem interactableComp = null;
        if (isTower)
            interactableComp = SpawnDeploymentOverlay();
        else
            interactableComp = SpawnSpellWeapon();

        StartCoroutine(ProcessGrab(interactableComp));

        infoObject.SetActive(false);
    }
    NVRInteractableItem SpawnDeploymentOverlay()
    {
        var overlayPrefab = Resources.Load<GameObject>("Env/DeploymentOverlay");
        var overlay = Instantiate(overlayPrefab);
        var overlayComp = overlay.GetComponent<DeploymentOverlay>();

        overlay.transform.position = NVRPlayer.Instance.RightHand.transform.position;
        overlayComp.towerPrefab = towerPrefab;
        overlayComp.price = price;

        return overlay.GetComponent<NVRInteractableItem>();
    }
    NVRInteractableItem SpawnSpellWeapon()
    {
        var weapon = Instantiate(towerPrefab);

        weapon.transform.position = NVRPlayer.Instance.RightHand.transform.position;

        return weapon.GetComponent<NVRInteractableItem>();
    }

    IEnumerator ProcessGrab(NVRInteractableItem interactableComp)
    {
        yield return new WaitForEndOfFrame();

        NVRPlayer.Instance.RightHand.EndInteraction(NVRPlayer.Instance.RightHand.CurrentlyInteracting);
        NVRPlayer.Instance.RightHand.BeginInteraction(interactableComp);
    }

    void OnNotEnoughGold()
    {
        var sfx = Resources.Load<AudioClip>("SE/Cancel1");
        var audio = gameObject.GetComponent<AudioSource>();
        audio.PlayOneShot(sfx);
    }
}
