using System.Collections;
using System.Collections.Generic;
using HTNWIC;
using UnityEngine;

public class DummyHealth : HealthManager
{
    protected override void Die()
    {
        Debug.Log($"Dummy no.{gameObject.GetInstanceID()} has died");
        Destroy(gameObject);
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        if (CurrentHealth > 0) Debug.Log($"Dummy no.{gameObject.GetInstanceID()} has taken {amount} damage. He has {CurrentHealth} health left");
    }
}
