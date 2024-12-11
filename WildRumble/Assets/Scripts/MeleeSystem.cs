using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created by Joshua Guerrero
 * This script controls melee combat and will 
 * handle animation queues
 * 
 * Guide: https://www.youtube.com/watch?v=aNZw588BQBo
 */

public class MeleeSystem : MonoBehaviour
{
    public GameObject StopSign;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public AudioClip StopSignAttackSound;
    public ObjectiveManager objectiveManager;

    public int stopSignDamage = 20; // Damage dealt by the Stop Sign

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack)
            {
                StopSignAttack();
            }
        }
    }

    public void StopSignAttack()
    {
        CanAttack = false;

        // Play the attack animation
        Animator anim = StopSign.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("Attack");
        }

        // Play the attack sound
        AudioSource ac = GetComponent<AudioSource>();
        if (ac != null && StopSignAttackSound != null)
        {
            ac.PlayOneShot(StopSignAttackSound);
        }

        // Enable the Stop Sign's trigger temporarily to detect collisions
        Collider stopSignCollider = StopSign.GetComponent<Collider>();
        if (stopSignCollider != null)
        {
            StartCoroutine(EnableColliderTemporarily(stopSignCollider));
        }

        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator EnableColliderTemporarily(Collider collider)
    {
        yield return new WaitForSeconds(0.5f); // Duration of the attack
        collider.enabled = true;
        yield return new WaitForSeconds(0.4f); // Duration of the attack
        collider.enabled = false;
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // Check if the collided object is an enemy
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Damage(stopSignDamage); // Apply damage to the enemy
                objectiveManager.UpdateObjective("Eliminate 5 Animals");
            }
        }
    }
}
