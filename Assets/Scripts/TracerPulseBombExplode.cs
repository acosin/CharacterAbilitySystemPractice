using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracerPulseBombExplode : MonoBehaviour
{
    public float blastRadius = 1;
    public int damage = 1;
    public float explodeMaxTime = 0;
    public float explosionForce = 6000;
    private bool explode;
    public bool bounce;

    void Start()
    {
        if (explodeMaxTime <= 0)
        {

        }
        else
        {
            GameObject.Destroy(gameObject, 10);
        }

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamageable>() != null || bounce == false)
        {
            GameObject emptySticker = new GameObject("Sticker");
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.transform.SetParent(emptySticker.transform, true);
            emptySticker.transform.SetParent(other.transform, true);
            LayerSetter.SetAllLayer(emptySticker, gameObject.layer);
            Invoke("ExplodeNow", explodeMaxTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<IDamageable>() != null || bounce == false)
        {
            GameObject emptySticker = new GameObject("Sticker");
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.transform.SetParent(emptySticker.transform, true);
            emptySticker.transform.SetParent(collision.collider.transform, true);
            LayerSetter.SetAllLayer(emptySticker, gameObject.layer);
            Invoke("ExplodeNow", explodeMaxTime);
            Debug.Log(collision.gameObject.name);
        }
    }

    void ExplodeNow()
    {
        explode = true;
    }

    void FixedUpdate()
    {
        if (explode)
        {
            //RaycastHit[] hitss = Physics.SphereCastAll(transform.position, blastRadius, transform.forward, 0);
            //for (int i = 0; i < hitss.Length; i++)
            //{
            //    if (hitss[i].collider.GetComponent<IAttackable>() != null)
            //    {
            //        Debug.DrawLine(transform.position, hitss[i].point, Color.red, 5);
            //        Debug.Log(Physics.Linecast(transform.position, hitss[i].point));
            //        if (!Physics.Linecast(transform.position, hitss[i].point))
            //        {
            //            Debug.Log("damage");
            //            hitss[i].collider.GetComponent<IAttackable>().GetAttacked(damage);
            //            hitss[i].collider.GetComponent<IAttackable>().GetExplosionForce(explosionForce, transform.position, blastRadius);
            //        }
            //    }
            //}
            LayerMask lm = (1 << LayerMask.NameToLayer(LayerSetter.EnemyBaseLayerName(LayerMask.LayerToName(gameObject.layer))) /*| 1 << LayerMask.NameToLayer(LayerSetter.EnemyBaseLayerName(LayerMask.LayerToName(gameObject.layer)) + "P")*/);
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, blastRadius, lm);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].GetComponent<IDamageable>() != null)
                {
                    Debug.Log("in range");
                    //RaycastHit visibleHit;
                    if (!Physics.Linecast(transform.position, hitColliders[i].transform.position, 1 << LayerMask.NameToLayer("Terrain")))
                    //Physics.Raycast(transform.position, (hitColliders[i].transform.position - transform.position).normalized, out visibleHit, blastRadius, 1 << LayerMask.NameToLayer("Terrain"));
                    //if (visibleHit.collider == hitColliders[i])
                    {
                        Debug.Log("damage");
                        hitColliders[i].GetComponent<IDamageable>().GetAttacked(damage);
                        hitColliders[i].GetComponent<IDamageable>().GetExplosionForce(explosionForce, transform.position, blastRadius);
                    }
                }
            }
            this.gameObject.SetActive(false);
        }
    }
}
