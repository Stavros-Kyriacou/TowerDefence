
using UnityEngine;
using TMPro;

public class Turret : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int cost;
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attacksPerSecond;


    [Header("Targeting")]
    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] private bool tracksTarget;
    [SerializeField] private float targetUpdateRate;
    [SerializeField] private float rotationSpeed;
    private Transform target;


    [Header("Projectile")]
    [SerializeField] private bool firesProjectiles;
    [SerializeField] private TrackingProjectile projectilePrefab;
    [SerializeField] private Transform firePoint;

    [Header("Effects")]
    [SerializeField] private GameObject shootParticle;


    private float shootCoolDown = 0f;
    

    public int Cost { get { return cost; } }
    public TrackingProjectile ProjectilePrefab { get { return projectilePrefab; } }
    public Transform FirePoint { get { return firePoint; } }
    public Transform Target { get { return target; } }
    public int MinDamage { get { return minDamage; } }
    public int MaxDamage { get { return maxDamage; } }
    public float AttackRange { get { return attackRange; } }
    public float AttacksPerSecond { get { return attacksPerSecond; } }


    private void Start()
    {
        
        if (firesProjectiles)
        {
            InvokeRepeating("FindClosestTarget", 0f, targetUpdateRate);
        }
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
        //Target tracking
        if (tracksTarget)
        {
            if (target == null)
            {
                return;
            }

            Vector3 dir = target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotationSpeed * Time.deltaTime);

            if (firesProjectiles)
            {
                if (shootCoolDown <= 0f)
                {
                    Shoot();
                    shootCoolDown = 1 / attacksPerSecond;
                }
                shootCoolDown -= Time.deltaTime;
            }
        }
    }
    public virtual void Shoot()
    {
        Debug.LogError("Override Shoot method has not been created for turret");
    }
    public int GetDamageInstance()
    {
        return Random.Range(minDamage, maxDamage + 1);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
    
