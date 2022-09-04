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

    public void Damage(Damageable objectToDamage)
    {
        objectToDamage.GetDamaged(damageValue);
    }

}
