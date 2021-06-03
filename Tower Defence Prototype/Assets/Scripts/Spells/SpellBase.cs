using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBase : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;
    private string damageSource = "Player";

    [Header("Cost")]
    [SerializeField] private int manaCost;

    [Header("Cast Time")]
    [SerializeField] private float castTime;
    [SerializeField] private float coolDown;

    [Header("Target To Hit")]
    [SerializeField] private LayerMask targetLayerMask;

    [Header("Audio")]
    [SerializeField] private bool playAudio;
    [SerializeField] private AudioClip castSFX;
    [SerializeField] private AudioClip hitSFX;

    public int ManaCost
    {
        get
        {
            return manaCost;
        }
    }
    public float CastTime
    {
        get
        {
            return castTime;
        }
    }
    public float CoolDown
    {
        get
        {
            return coolDown;
        }
    }
    public LayerMask TargetLayerMask
    {
        get
        {
            return targetLayerMask;
        }
    }
    public bool PlayAudio
    {
        get
        {
            return playAudio;
        }
    }
    public AudioClip CastSFX
    {
        get
        {
            return castSFX;
        }
    }
    public AudioClip HitSFX
    {
        get
        {
            return hitSFX;
        }
    }
    public string DamageSource
    {
        get
        {
            return damageSource;
        }
    }

    public int GetDamageInstance()
    {
        int damageInstance = Random.Range(minDamage, maxDamage);
        return damageInstance;
    }

    public virtual void CastSpell(Vector2 mousePos, Vector2 playerPos)
    {
        Debug.LogError("CastSpell() not implemented on selected spell");
    }
}
