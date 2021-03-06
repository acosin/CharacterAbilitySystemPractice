﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShoot : MonoBehaviour, ICastable {
    private Camera fpsCam;
    [HideInInspector]
    public Rigidbody projectile;
    [HideInInspector]
    public Transform bulletSpawn;
    [HideInInspector]
    public float projectileForce = 1000f;

    void Start()
    {

    }

    public void Initialize () {
        fpsCam = gameObject.GetComponentInParent<ParentMarker>().GetComponentInChildren<CameraMarker>().GetComponent<Camera>();
        bulletSpawn = GetComponentInChildren<BulletSpawnMarker>().gameObject.transform;
    }

	public void Cast () {
        Rigidbody cloneRb = Instantiate(projectile, bulletSpawn.position, projectile.gameObject.GetComponent<Transform>().rotation) as Rigidbody;
        cloneRb.transform.forward = fpsCam.transform.forward;
        //Debug.Log(LayerMask.LayerToName(gameObject.layer) + "P");
        cloneRb.gameObject.layer = LayerMask.NameToLayer(LayerMask.LayerToName(gameObject.layer) + "P");
        cloneRb.AddForce(fpsCam.transform.forward * projectileForce);
    }
}
