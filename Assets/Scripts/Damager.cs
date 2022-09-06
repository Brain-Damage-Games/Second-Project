using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    
    [SerializeField]
    float damageValue = 1f;

    public void SetDamageValue(float newDamageValue)
    {
        damageValue = newDamageValue;
    }

    public float GetDamageValue()
    {
        return damageValue;
    }

    public void Damage(Damageable objectToDamage)
    {
        objectToDamage.GetDamaged(damageValue);
    }

}
