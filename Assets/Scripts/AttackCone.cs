using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class AttackCone : MonoBehaviour
{
    public TurretAI turret;

    public bool isLeft= false;

    private void Awake()
    {
        turret = gameObject.GetComponentInParent<TurretAI>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isLeft)
            {
                turret.Attack(false);
            }
            else
            {
                turret.Attack(true);
            }
        }
    }
    
}
