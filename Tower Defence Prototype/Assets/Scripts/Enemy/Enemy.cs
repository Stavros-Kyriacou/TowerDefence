using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, ISlow
{
    [SerializeField] private Image slowIcon;
    [SerializeField] private float maxMoveSpeed;
    private float currentMoveSpeed;
    private AIPath aIPath;

    private void Start()
    {
        slowIcon.enabled = false;
        currentMoveSpeed = maxMoveSpeed;
        aIPath = GetComponent<AIPath>();
        aIPath.maxSpeed = currentMoveSpeed;
    }
    public void ApplySlow(float slowPercent, float slowDuration)
    {
        if (currentMoveSpeed != maxMoveSpeed)
        {
            return;
        }

        currentMoveSpeed = currentMoveSpeed * (1 - slowPercent);
        aIPath.maxSpeed = currentMoveSpeed;

        if (slowIcon != null)
        {
            //show slow effect icon
            slowIcon.enabled = true;
        }

    }
    public void ApplySlow(float pcnt)
    {
        if (currentMoveSpeed != maxMoveSpeed)
        {
            //prevent slows from stacking
            return;
        }

        //change movement speed
        currentMoveSpeed = currentMoveSpeed * (1 - pcnt);
        aIPath.maxSpeed = currentMoveSpeed;

        if (slowIcon != null)
        {
            //show slow effect icon
            slowIcon.enabled = true;
        }
    }
    public void RemoveSlow()
    {
        //change movement speed
        currentMoveSpeed = maxMoveSpeed;
        aIPath.maxSpeed = currentMoveSpeed;

        if (slowIcon != null)
        {
            //hide slow effect icon
            slowIcon.enabled = false;
        }
    }
}
