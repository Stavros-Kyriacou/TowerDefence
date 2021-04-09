using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBase : MonoBehaviour
{
    [SerializeField] private float minDamage;
    [SerializeField] private float maxDamage;

    [SerializeField] private LayerMask targetLayerMask;


    [Header("Audio")]
    [SerializeField] private AudioClip castSFX;
    [SerializeField] private AudioClip hitSFX;
    


}
