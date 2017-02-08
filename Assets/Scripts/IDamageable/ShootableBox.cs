using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ShootableBox : NetworkBehaviour, IDamageable
{
    [SerializeField] [SyncVar (hook = "OnHealthChanged")] int currentHealth;
    [SerializeField] int maxHealth;

    [ServerCallback]
    void OnEnable()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {

    }

    public void GetAttacked(int damageAmount)
    {
        currentHealth -= damageAmount;

        //if (currentHealth <= 0)
        //{
        //    gameObject.SetActive(false);
        //}
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

    public void OnHealthChanged(int value)
    {
        currentHealth = value;
        if(value <= 0)
        {
            gameObject.SetActive(false);
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}