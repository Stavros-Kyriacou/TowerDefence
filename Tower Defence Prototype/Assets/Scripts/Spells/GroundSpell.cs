using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpell : SpellBase
{
    [Header("Ground Spell")]
    [SerializeField] private float duration;
    [SerializeField] private float spellRadius;
    [SerializeField] private float castRange;
    private float durationCounter = 0;
    private Vector3 placementLocation;

    public float SpellRadius
    {
        get
        {
            return spellRadius;
        }
    }
    public float CastRange
    {
        get
        {
            return castRange;
        }
    }
    public Vector3 PlacementLocation
    {
        get
        {
            return placementLocation;
        }
        set
        {
            placementLocation = value;
        }
    }
    private void Update()
    {
        if (durationCounter < duration)
        {
            durationCounter += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
