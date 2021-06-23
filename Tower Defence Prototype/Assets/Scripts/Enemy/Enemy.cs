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
        //prevent slows from stacking
        if (currentMoveSpeed != maxMoveSpeed)
        {
            return;
        }

        //change movespeed
        currentMoveSpeed = currentMoveSpeed * (1 - slowPercent);
        aIPath.maxSpeed = currentMoveSpeed;

        //show slow icon gfx
        if (slowIcon != null)
        {
            slowIcon.enabled = true;
        }
        StartCoroutine(Slow(slowDuration));

    }
    public IEnumerator Slow(float slowDuration)
    {
        yield return new WaitForSeconds(slowDuration);
        RemoveSlow();
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
