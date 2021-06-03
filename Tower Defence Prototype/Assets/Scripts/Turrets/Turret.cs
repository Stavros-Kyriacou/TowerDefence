using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int cost;
    [SerializeField] private float attackRange;
    [SerializeField] private float attacksPerSecond;


    [Header("Targeting")]
    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] private float targetUpdateRate;
    [SerializeField] private bool tracksTarget;
    [SerializeField] private float rotationSpeed;
    private Transform target;

    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;

    [Header("Effects")]
    [SerializeField] private GameObject shootParticle;


    private float shootCoolDown = 0f;

    public int Cost
    {
        get
        {
            return cost;
        }
    }

    private void Start()
    {
        InvokeRepeating("FindClosestTarget", 0f, targetUpdateRate);
    }
    public void FindClosestTarget()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, attackRange, targetLayerMask);
        float shortestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (Collider2D enemy in enemiesInRange)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                closestEnemy = enemy.gameObject;
            }
        }

        if (closestEnemy != null && shortestDistance <= attackRange)
        {
            target = closestEnemy.transform;
        }
        else
        {
            target = null;
        }

    }
    private void Update()
    {
        if (target == null)
        {
            return;
        }

        if (tracksTarget)
        {
            Vector3 dir = target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotationSpeed * Time.deltaTime);
        }

        if (shootCoolDown <= 0f)
        {
            Shoot();
            shootCoolDown = 1 / attacksPerSecond;
        }
        shootCoolDown -= Time.deltaTime;
    }
    public virtual void Shoot()
    {
        Debug.LogError("Override Shoot method has not been created for turret");
    }   
    private int GetDamageInstance()
    {
        return Random.Range(minDamage, maxDamage + 1);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
