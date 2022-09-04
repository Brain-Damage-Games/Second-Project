using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    
    [SerializeField]
    float maxHealth = 1f;

    private float health;

    private void Awake() 
    {
        health = maxHealth;
    }

    public void GetDamaged(float damageValue)
    {
        health -= damageValue;
        print("health: " + health);
    }

    public void Heal(float healValue)
    {
        if(health <= maxHealth)
            health += healValue;
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    
}
