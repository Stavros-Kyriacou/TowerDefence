using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    enum State
    {
        WalkToObjective,
        ChasingPlayer,
        Attacking
    }
    private State state;

    void Start()
    {

    }

    void Update()
    {

    }
}
