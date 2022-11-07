using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] GameObject deathPrefab; // death animation
    [SerializeField] int maxHealth = 100; 
    [SerializeField] bool destroyOnDeath = true;
    [SerializeField] bool destroyRoot = false;

    public float health;
    bool isDead = false; 

    void Start()
    {
        health = maxHealth; 
    }

    private void Update()
    {
        if (!isDead && health <= 0)
        {
            isDead = true;
            Debug.Log("Dead"); 
            if (TryGetComponent<IDestructable>(out IDestructable destructable))
            {
                destructable.Destroyed();
            }

            if (deathPrefab != null)
            {
                Instantiate(deathPrefab, transform.position, transform.rotation);
            }

            if (destroyOnDeath)
            {
                if (destroyRoot) Destroy(gameObject.transform.root.gameObject);
                else Destroy(gameObject);
            }
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
        if (!isDead && health <= 0)
        {
            isDead = true;
            if (TryGetComponent<IDestructable>(out IDestructable destructable))
            {
                destructable.Destroyed();
            }

            if (deathPrefab != null)
            {
                Instantiate(deathPrefab, transform.position, transform.rotation);
            }

            if (destroyOnDeath)
            {
                if (destroyRoot) Destroy(gameObject.transform.root.gameObject);
                else Destroy(gameObject);
            }
        }
    }
}