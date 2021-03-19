using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAggro : MonoBehaviour
{
    [SerializeField] private float aggroRange;
    [SerializeField] private float aggroTriggerTime;
    [SerializeField] private float aggroDuration;
    [SerializeField] private float aggroMoveSpeed;

    [SerializeField]private bool inAggroRange;
    [SerializeField]private float aggroTriggerCounter = 0;

    private bool aggroPlayer;


    private EnemyController enemyController;
    private AIDestinationSetter destinationSetter;
    private Player player;
    private Crystal crystal;

    //properties
    public float AggroRange
    {
        get
        {
            return aggroRange;
        }
    }
    public float AggroTriggerTime
    {
        get
        {
            return aggroTriggerTime;
        }
    }
    public float AggroDuration
    {
        get
        {
            return aggroTriggerTime;
        }
    }
    public float AggroMoveSpeed
    {
        get
        {
            return aggroMoveSpeed;
        }
    }


    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        player = Player.Instance;
        crystal = Crystal.Instance;

        SetTarget(crystal.transform);
        
    }
    void Update()
    {
        FindDistanceToPlayer();

        if (aggroPlayer)
        {
            if (!inAggroRange)
            {
                if (aggroTriggerCounter > 0)
                {
                    //if aggro on the player and not in aggro range range, start to lose aggro. Stops when the counter reaches 0 so it doesn't endlessly count down
                    aggroTriggerCounter -= Time.deltaTime;

                    if (aggroTriggerCounter <= 0)
                    {
                        //if the counter reaches 0, stop aggro on the player and set target back to the crystal
                        aggroPlayer = false;
                        SetTarget(crystal.transform);
                    }
                }
            }
        }
        else
        {
            if (inAggroRange)
            {
                //if not aggro on the player and in the aggro range, start counting up aggro timer
                aggroTriggerCounter += Time.deltaTime;

                if (aggroTriggerCounter >= aggroTriggerTime)
                {
                    //if counter reaches the aggroTriggerTime, set aggro on player true and set target as the player
                    aggroPlayer = true;
                    SetTarget(player.transform);
                }
            }
        }

    }
    private void FindDistanceToPlayer()
    {
        var distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < aggroRange)
        {
            inAggroRange = true;
        }
        else
        {
            inAggroRange = false;
        }
    }
    private void SetTarget(Transform target)
    {
        destinationSetter.target = target;
    }
}
