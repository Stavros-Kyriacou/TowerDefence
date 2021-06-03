using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStorm : GroundSpell
{
    [Header("Lightning Storm")]
    [SerializeField] private LightningStorm myPrefab;
    [SerializeField] private ParticleSystem pulseParticle;
    [SerializeField] private GameObject boltPrefab;
    [SerializeField] private GameObject radiusSprite;

    [Header("DamageOverTime")]
    [SerializeField] private float hitRate;

    [Header("Bolts")]
    [SerializeField] private int numBolts;
    [SerializeField] private float endPointVariance;

    private List<GameObject> bolts;
    private List<LightningBolt> lightningBolts;
    private List<Vector2> endPoints;


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
    private void Awake()
    {
        bolts = new List<GameObject>();
        lightningBolts = new List<LightningBolt>();
        radiusSprite.transform.localScale = new Vector3(1 * SpellRadius, 1 * SpellRadius, 1);

        for (int i = 0; i < numBolts; i++)
        {
            //instantiate all of the bolts and add them to a list
            GameObject bolt = Instantiate(boltPrefab);
            bolt.transform.parent = transform;
            bolts.Add(bolt);

            //get access to the lightning bolt components for later use
            LightningBolt lightningBolt = bolt.GetComponent<LightningBolt>();
            lightningBolts.Add(lightningBolt);
        }

        InvokeRepeating("Hit", 0f, hitRate);
    }
    private void Hit()
    {
        DrawBolts();
        DealDamage();
    }
    private void DrawBolts()
    {
        GetBoltEndPoints(numBolts, transform.position);

        for (int i = 0; i < bolts.Count; i++)
        {
            //set the start and end points of each bolt and activate it
            lightningBolts[i].StartPoint = transform.position;
            lightningBolts[i].EndPoint = endPoints[i];
            lightningBolts[i].Activate();
        }
    }
    void GetBoltEndPoints(int points, Vector2 center)
    {
        endPoints = new List<Vector2>();

        float slice = 2 * Mathf.PI / points;

        for (int i = 0; i < points; i++)
        {
            float angle = slice * i;
            float newX = center.x + SpellRadius * Mathf.Cos(angle);
            float newY = center.y + SpellRadius * Mathf.Sin(angle);
            Vector2 point = new Vector2(newX, newY);

            //add randomness to the end point value
            point += Random.insideUnitCircle * endPointVariance;
            endPoints.Add(point);
        }
    }
    private void DealDamage()
    {
        //draw circle collider, store all enemies hit by collider
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, SpellRadius, TargetLayerMask);
        var damageInstance = GetDamageInstance();

        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            //deal damage to all enemies hit
            var enemy = enemiesToDamage[i].GetComponent<EnemyHealth>();
            enemy.TakeDamage(damageInstance, DamageSource);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SpellRadius);
    }
}
