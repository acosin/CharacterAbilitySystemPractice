using UnityEngine;
using System.Collections;

public class ShootableBox : MonoBehaviour, IAttackable
{
    public int currentHealth = 3;

    public void GetAttacked(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void GetForce(Vector3 hitPoint, Vector3 force)
    {
        if(gameObject.GetComponent<Rigidbody>() != null)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(force);
        }
    }

    public void GetExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
        }
    }
}