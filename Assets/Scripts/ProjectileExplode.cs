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

	void Start () {
        if(explodeMaxTime <= 0)
        {

        }else
        {
            Invoke("ExplodeNow", explodeMaxTime);
        }
        
	}
	
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
            LayerMask lm = (1 << LayerMask.NameToLayer(LayerSetter.EnemyBaseLayerName(LayerMask.LayerToName(gameObject.layer))) | 1 << LayerMask.NameToLayer(LayerSetter.EnemyBaseLayerName(LayerMask.LayerToName(gameObject.layer)) + "P"));
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, blastRadius, lm);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].GetComponent<IAttackable>() != null)
                {
                    Debug.Log("in range");
                    //RaycastHit visibleHit;
                    if(!Physics.Linecast(transform.position, hitColliders[i].transform.position, 1 << LayerMask.NameToLayer("Terrain")))
                    //Physics.Raycast(transform.position, (hitColliders[i].transform.position - transform.position).normalized, out visibleHit, blastRadius, 1 << LayerMask.NameToLayer("Terrain"));
                    //if (visibleHit.collider == hitColliders[i])
                    {
                        Debug.Log("damage");
                        hitColliders[i].GetComponent<IAttackable>().GetAttacked(damage);
                        hitColliders[i].GetComponent<IAttackable>().GetExplosionForce(explosionForce, transform.position, blastRadius);
                    }
                }
            }
            this.gameObject.SetActive(false);
        }
    }
}
