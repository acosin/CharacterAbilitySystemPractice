using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplode : MonoBehaviour {
    public float blastRadius = 1;
    public int damage = 1;
    public float explodeMaxTime = 0;
    public float explosionForce = 6000;
    private bool explode;
    public bool bounce;

	// Use this for initialization
	void Start () {
        if(explodeMaxTime <= 0)
        {

        }else
        {
            Invoke("ExplodeNow", explodeMaxTime);
        }
        
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IAttackable>() != null || bounce == false)
        {
            Debug.Log(other.name);
            explode = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<IAttackable>() != null || bounce == false)
        {
            Debug.Log(collision.gameObject.name);
            explode = true;
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
           
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, blastRadius, ~(1 << gameObject.layer | 1 << LayerMask.NameToLayer(LayerMask.LayerToName(gameObject.layer).Remove(1))));
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].GetComponent<IAttackable>() != null)
                {
                    //Debug.DrawLine(transform.position, hitColliders[i].transform.position, Color.red, 5);
                    RaycastHit visibleHit;
                    Physics.Raycast(transform.position, (hitColliders[i].transform.position - transform.position).normalized, out visibleHit, blastRadius);
                    //Debug.Log(visibleHit.collider.name);
                    //Debug.Log(hitColliders[i].name);
                    //Debug.Log(visibleHit.collider == hitColliders[i]);
                    if (visibleHit.collider == hitColliders[i])
                    {
                        Debug.Log("damage");
                        hitColliders[i].GetComponent<IAttackable>().GetAttacked(damage);
                        hitColliders[i].GetComponent<IAttackable>().GetExplosionForce(explosionForce, transform.position, blastRadius);
                    }
                    //hitColliders[i].GetComponent<IAttackable>().GetAttacked(damage);
                    //hitColliders[i].GetComponent<IAttackable>().GetExplosionForce(explosionForce, transform.position, blastRadius);
                }
            }
            this.gameObject.SetActive(false);
        }
    }
}
