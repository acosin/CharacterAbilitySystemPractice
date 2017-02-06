using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShoot : MonoBehaviour {
    private Camera fpsCam;
    [HideInInspector]
    public Rigidbody projectile;
    [HideInInspector]
    public Transform bulletSpawn;
    [HideInInspector]
    public float projectileForce = 1000f;

	public void Initialize () {
        fpsCam = GetComponentInParent<Camera>();
        bulletSpawn = GetComponentInChildren<BulletSpawnMarker>().gameObject.transform;
    }

	public void Launch () {
        Rigidbody cloneRb = Instantiate(projectile, bulletSpawn.position, projectile.gameObject.GetComponent<Transform>().rotation) as Rigidbody;
        cloneRb.transform.forward = fpsCam.transform.forward;
        //Debug.Log(LayerMask.LayerToName(gameObject.layer) + "P");
        cloneRb.gameObject.layer = LayerMask.NameToLayer(LayerMask.LayerToName(gameObject.layer) + "P");
        cloneRb.AddForce(fpsCam.transform.forward * projectileForce);
    }
}
