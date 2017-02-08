using UnityEngine;
using System.Collections;

public class RaycastShoot : MonoBehaviour, ICastable
{

    [HideInInspector]
    public int gunDamage = 1;
    [HideInInspector]
    public float weaponRange = 50f;
    [HideInInspector]
    public float hitForce = 100f;
    [HideInInspector]
    public Transform gunEnd;

    //public GameObject FPSCharacter;
    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    //private AudioSource gunAudio;
    [HideInInspector]
    public LineRenderer laserLine;

    public void Initialize()
    {
        laserLine = GetComponent<LineRenderer>();

        //gunAudio = GetComponent<AudioSource>();

        fpsCam = gameObject.GetComponentInParent<ParentMarker>().GetComponentInChildren<CameraMarker>().GetComponent<Camera>();
        gunEnd = GetComponentInChildren<GunEndMarker>().gameObject.transform;
    }


    public void Cast()
    {
        StartCoroutine(ShotEffect());

        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        RaycastHit hit;

        laserLine.SetPosition(0, gunEnd.position);

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange, (1 << LayerMask.NameToLayer("Terrain") | 1 << LayerMask.NameToLayer(LayerSetter.EnemyBaseLayerName(LayerMask.LayerToName(gameObject.layer))))))
        {
            laserLine.SetPosition(1, hit.point);

            IDamageable health = hit.collider.GetComponent<IDamageable>();

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