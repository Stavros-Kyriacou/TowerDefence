﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            var enemy = other.GetComponent<EnemyHealth>();
            enemy.TakeDamage(1000);
        }
    }
}