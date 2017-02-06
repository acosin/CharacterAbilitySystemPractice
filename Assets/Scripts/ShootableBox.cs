using UnityEngine;
using System.Collections;

public class ShootableBox : MonoBehaviour, IAttackable
{

    //The box's current health point total
    public int currentHealth = 3;

    public void GetAttacked(int damageAmount)
    {
        //subtract damage amount when Damage function is called
        currentHealth -= damageAmount;

        //Check if health has fallen below zero
        if (currentHealth <= 0)
        {
            //if health has fallen below zero, deactivate it 
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