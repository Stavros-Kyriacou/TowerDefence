using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass
{
    private int strength;
    private int intelligence;
    private int dexterity;

    private int armour;
    private int elementalResist;
    private int health;
    private int healthRegen;
    private int manaRegen;
    private int moveSpeed;

    private int Strength
    {
        get { return strength; }
        set { strength = value; }
    }
    private int Intelligence
    {
        get { return intelligence; }
        set { intelligence = value; }
    }
    private int Dexterity
    {
        get { return dexterity; }
        set { dexterity = value; }
    }
    private int Armour
    {
        get { return armour; }
        set { armour = value; }
    }
    private int ElementalResist
    {
        get { return elementalResist; }
        set { elementalResist = value; }
    }
    private int Health
    {
        get { return health; }
        set { health = value; }
    }
    private int HealthRegen
    {
        get { return healthRegen; }
        set { healthRegen = value; }
    }
    private int ManaRegen
    {
        get { return manaRegen; }
        set { manaRegen = value; }
    }
    private int MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

}
