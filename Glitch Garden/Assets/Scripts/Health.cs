using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] private GameObject deathVFX;

    public void DealDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            TriggerDeathVFX();
            
            FindObjectOfType<AudioSource>().Play();
            Destroy(gameObject);
        }
    }

    private void TriggerDeathVFX()
    {
        if (!deathVFX)
        {
            return;
        }

        GameObject dealthVFXObject = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(dealthVFXObject, 1f);    //    Time to wait before destroy
    }
}