using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField]
    public float damage = 5f;
    PlayerHealth playerHealth;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Hit Player for " + damage + " damage!");
            }
            else
            {
                Debug.LogWarning("Hit Player, but no PlayerHealth script found on target!");
            }
            
            Destroy(gameObject);
        }
    }

    /*public void FixedUpdate()
    {
        if (playerHealth == 0)
        {
            Destroy(gameObject);
        }
    }*/
}
