using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStorm : GroundSpell
{
    [Header("Lightning Storm")]
    [SerializeField] private LightningStorm myPrefab;
    [SerializeField] private ParticleSystem pulseParticle;
    [SerializeField] private GameObject boltPrefab;

    [Header("DamageOverTime")]
    [SerializeField] private float hitRate;

    [Header("Bolts")]
    [SerializeField] private int numBolts;

    private List<Vector2> endPoints;
    private List<GameObject> bolts;


    private void Awake()
    {
        GetBoltEndPoints(numBolts, SpellRadius, transform.position);

        Inititalise();
        InvokeRepeating("DealDamage", 0f, hitRate);
    }
    private void Start()
    {

    }
    private void Inititalise()
    {
        bolts = new List<GameObject>();

        for (int i = 0; i < numBolts; i++)
        {
            GameObject bolt = Instantiate(boltPrefab);
            bolt.transform.parent = transform;

            LightningBolt lightningBolt = bolt.GetComponent<LightningBolt>();
            lightningBolt.StartPoint = transform.position;
            lightningBolt.EndPoint = endPoints[i];
            bolt.SetActive(false);
            bolts.Add(bolt);
        }
    }


    public override void CastSpell(Vector2 mousePos, Vector2 playerPos)
    {
        var mouseDirection = (mousePos - playerPos).normalized;
        var mousePlayerDistance = Vector2.Distance(playerPos, mousePos);

        if (mousePlayerDistance > CastRange)
        {
            PlacementLocation = (mouseDirection * CastRange) + playerPos;
        }
        else
        {
            PlacementLocation = mousePos;
        }

        LightningStorm lightningStorm = Instantiate(myPrefab, PlacementLocation, myPrefab.transform.rotation);
    }
    void GetBoltEndPoints(int points, float radius, Vector2 center)
    {
        endPoints = new List<Vector2>();

        float slice = 2 * Mathf.PI / points;

        for (int i = 0; i < points; i++)
        {
            float angle = slice * i;
            float newX = center.x + radius * Mathf.Cos(angle);
            float newY = center.y + radius * Mathf.Sin(angle);
            Vector2 point = new Vector2(newX, newY);
            endPoints.Add(point);
        }
    }
    private void DealDamage()
    {
        // pulseParticle.Play();
        foreach (GameObject bolt in bolts)
        {
            bolt.SetActive(true);
            LightningBolt lightningBolt = bolt.GetComponent<LightningBolt>();
            lightningBolt.Activate();
        }
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, SpellRadius, TargetLayerMask);               //array of all enemies in the spell radius
        var damageInstance = GetDamageInstance();                                                                                     //calculate damage for this particular spell hit

        for (int i = 0; i < enemiesToDamage.Length; i++)                                                                            //apply damage to all hit enemies
        {
            var enemy = enemiesToDamage[i].GetComponent<EnemyHealth>();
            enemy.TakeDamage(damageInstance);
        }
    }




    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SpellRadius);
    }
}
