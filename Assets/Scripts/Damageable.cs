using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    
    [SerializeField]
    float maxHealth = 1f;
    private float health;
    [SerializeField] private UnityEvent onDamage;
    [SerializeField] private UnityEvent onDeath;
    private Transform lastDamager;
    private bool dead = false;

    private void Awake() 
    {
        health = maxHealth;
    }

    public void GetDamaged(float damageValue, Transform damager)
    {
        health -= damageValue;
        lastDamager = damager;
        onDamage.Invoke();
        if (health <= 0){
            onDeath.Invoke();
            dead = true;
        }
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

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public Transform GetLastDamager(){
        return lastDamager;
    }

    public bool IsDead(){
        return dead;
    }
}
