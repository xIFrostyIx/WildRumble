using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created by Joshua Guerrero
 * This script controls melee combat and will 
 * handle animation queues
 */

public class MeleeSystem : MonoBehaviour
{
    public int damage = 10; //Damage that is dealt by melee weapon
    public float attackCooldown = 0.5f; // The time between attacks
    private float lastAttackTime;

    public LayerMask hitLayers;

    void OnTriggerEnter(Collider other)
    {
        if (Time.time - lastAttackTime < attackCooldown) return;

        if ((hitLayers.value & (1 << other.gameObject.layer)) > 0)
        {
            ObjectWithHealthBar targetHealth = other.GetComponent<ObjectWithHealthBar>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }

            lastAttackTime = Time.time;
        }
    }
}
