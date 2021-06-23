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

        ChangeMoveSpeed(slowPercent);

        ToggleSlowGFX(true);

        StartCoroutine(SlowDuration(slowDuration));
    }
    public IEnumerator SlowDuration(float slowDuration)
    {
        yield return new WaitForSeconds(slowDuration);
        RemoveSlow();
    }
    private void ChangeMoveSpeed(float slowPercent)
    {
        currentMoveSpeed = currentMoveSpeed * (1 - slowPercent);
        aIPath.maxSpeed = currentMoveSpeed;
    }
    private void RemoveSlow()
    {
        currentMoveSpeed = maxMoveSpeed;
        aIPath.maxSpeed = currentMoveSpeed;

        ToggleSlowGFX(false);
    }
    public void ToggleSlowGFX(bool state)
    {
        if (slowIcon != null)
        {
            slowIcon.enabled = state;
        }
    }
}
