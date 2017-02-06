using UnityEngine;
using System.Collections;

public class RaycastShoot : MonoBehaviour
{

    [HideInInspector]
    public int gunDamage = 1;
    //[HideInInspector]
    //public float fireRate = 0.25f;
    [HideInInspector]
    public float weaponRange = 50f;
    [HideInInspector]
    public float hitForce = 100f;
    public Transform gunEnd;

    public GameObject FPSCharacter;
    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    //private AudioSource gunAudio;
    [HideInInspector]
    public LineRenderer laserLine;
    //private float nextFire;


    public void Initialize()
    {
        laserLine = GetComponent<LineRenderer>();

        //gunAudio = GetComponent<AudioSource>();

        fpsCam = GetComponentInParent<Camera>();
        gunEnd = GetComponentInChildren<GunEndMarker>().gameObject.transform;
    }


    public void Fire()
    {
        //nextFire = Time.time + fireRate;

        StartCoroutine(ShotEffect());

        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        RaycastHit hit;

        laserLine.SetPosition(0, gunEnd.position);

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange, ~(1 << gameObject.layer)))
        {
            //Debug.Log(hit.point);
            laserLine.SetPosition(1, hit.point);

            IAttackable health = hit.collider.GetComponent<IAttackable>();

            if (health != null)
            {
                health.GetAttacked(gunDamage);
                health.GetForce(hit.point, -hit.normal * hitForce);
            }
        }
        else
        {
            laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
        }
    }


    private IEnumerator ShotEffect()
    {
        //gunAudio.Play();

        laserLine.enabled = true;

        yield return shotDuration;

        laserLine.enabled = false;
    }
}