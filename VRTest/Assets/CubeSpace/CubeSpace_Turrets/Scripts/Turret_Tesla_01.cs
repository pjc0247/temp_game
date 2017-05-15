using UnityEngine;
using System.Collections;

public class Turret_Tesla_01 : MonoBehaviour {

    public GameObject Bullet;
    public GameObject Ef_Gun_Light_01;

    public Transform ShotSpawn01;

    public float fireRate;

    private float nextFire;
    private float nextFireRocket;


    // Update is called once per frame
    void Update()
    {

        //                  --- Gun Fire
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(Bullet, ShotSpawn01.position, ShotSpawn01.rotation);
            Instantiate(Ef_Gun_Light_01, ShotSpawn01.position, ShotSpawn01.rotation);
        }

    }
}
