using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballTurret : Turret
{
    [Header("Fireball")]
    [SerializeField] private Fireball fireballPrefab;
    [SerializeField] private float fireballDamage;
    [SerializeField] private float targetUpdateTime;

    [SerializeField] private GameObject currentTarget;

    private float shortestDistance = 0f;
    private float distanceFromCenter;
    private float attackTimer = 0f;


    //on update, change turret rotation to face target

    //if there is a target and attackTimer is ready
    //spawn a fireball, set its destination to be the targets position


    // IEnumerator Start()
    // {
    //     do
    //     {
    //         StartCoroutine(FindClosestTarget());
    //         yield return new WaitForSeconds(targetUpdateTime);
    //     } while (true);
    // }
    // private void Update()
    // {
    //     if (currentTarget != null && Vector2.Distance(transform.position, currentTarget.transform.position) < AttackRange && attackTimer >= AttacksPerSecond)
    //     {
    //         FireProjectile();
    //         attackTimer = 0f;
    //     }
    //     if (attackTimer < 1f)
    //     {
    //         attackTimer += Time.deltaTime;
    //     }
    //     else
    //     {
    //         attackTimer = 1f;
    //     }
    // }
    // IEnumerator FindClosestTarget()
    // {
    //     Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, AttackRange, targetLayerMask);

    //     foreach (Collider2D enemy in enemiesInRange)
    //     {
    //         if (currentTarget == null)
    //         {
    //             //if no current target is set
    //             currentTarget = enemy.gameObject;
    //         }

    //         if (Vector2.Distance(transform.position, enemy.transform.position) < Vector2.Distance(transform.position, currentTarget.transform.position))
    //         {
    //             //if distance between enemy at array index is less than currentTarget, assign it as the new target
    //             currentTarget = enemy.gameObject;
    //         }

    //         if (Vector2.Distance(transform.position, currentTarget.transform.position) > AttackRange)
    //         {
    //             //if the current target leaves the turret range, assign null as current target
    //             currentTarget = null;
    //         }
    //     }
    //     yield return null;
    // }
    
    // private void FireProjectile()
    // {
    //     var projectileDestination = currentTarget.transform.position;

    //     //angle the fireball must rotate to face its target
    //     Vector3 dir = projectileDestination - transform.position;
    //     float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

    //     Fireball fireball = Instantiate(fireballPrefab, transform.position, Quaternion.AngleAxis(angle, Vector3.forward)) as Fireball;
    //     //fireball.ProjectileDamage = fireballDamage;
    //     fireball.Destination = projectileDestination;
    // }

    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(transform.position, AttackRange);
    // }
}
