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
        Debug.Log("Start apply slow");
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
        Debug.Log("Slow applied");
        StartCoroutine(Slow(slowDuration));

    }
    public IEnumerator Slow(float slowDuration)
    {
        Debug.Log("Enter coroutine");
        yield return new WaitForSeconds(slowDuration);
        Debug.Log("Wait for seconds finished");
        RemoveSlow();
    }
    public void RemoveSlow()
    {
        Debug.Log("Start Remove slow");
        //change movement speed
        currentMoveSpeed = maxMoveSpeed;
        aIPath.maxSpeed = currentMoveSpeed;

        if (slowIcon != null)
        {
            //hide slow effect icon
            slowIcon.enabled = false;
        }
        Debug.Log("Slow removed");
    }
}
