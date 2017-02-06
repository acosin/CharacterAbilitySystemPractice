using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShoot : MonoBehaviour {

    public GameObject FPSCharacter;
    private Camera fpsCam;
    [HideInInspector]
    //public Rigidbody projectile;
    public Transform bulletSpawn;
    //public float projectileForce = 1000f;
    //public float fireRate = 0.25f;

    //private float nextFireTime;
	// Use this for initialization
	public void Initialize () {
        fpsCam = GetComponentInParent<Camera>();
        bulletSpawn = GetComponentInChildren<BulletSpawnMarker>().gameObject.transform;
    }
	
	// Update is called once per frame
	public void Launch (Rigidbody projectile, float projectileForce) {
        Rigidbody cloneRb = Instantiate(projectile, bulletSpawn.position, projectile.gameObject.GetComponent<Transform>().rotation) as Rigidbody;
        cloneRb.transform.forward = fpsCam.transform.forward;
        //Debug.Log(LayerMask.LayerToName(gameObject.layer) + "P");
        cloneRb.gameObject.layer = LayerMask.NameToLayer(LayerMask.LayerToName(gameObject.layer) + "P");
        cloneRb.AddForce(fpsCam.transform.forward * projectileForce);
        //nextFireTime = Time.time + fireRate;
    }
}
