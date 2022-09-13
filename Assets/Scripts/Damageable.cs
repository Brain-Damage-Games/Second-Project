using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Damageable : MonoBehaviour
{
    
    [SerializeField] float maxHealth = 1f;
    [SerializeField] private float health;
    [SerializeField] private UnityEvent onDamage;
    public delegate void onDeathDel(Transform transform);
    public event onDeathDel onDeath;
    [SerializeField] private Image healthBar;
    [SerializeField] private float acceleration = 1f;
    private Transform lastDamager;
    private bool dead = false;
    private float timeBetweenHealthBarChange = 0f;
    private float currentHealthValue;      //***** this will change slowly
    private Transform lastDamager;
    private bool dead = false;

    private void Awake() 
    {
        health = maxHealth;
        currentHealthValue = maxHealth;
    }

    private void Update() 
    {
        // if(Input.GetMouseButtonDown(0))     DamageTest();

        if(currentHealthValue != health)
        {
            currentHealthValue = Mathf.Lerp(currentHealthValue, health, timeBetweenHealthBarChange);
            timeBetweenHealthBarChange += acceleration * Time.deltaTime;
        }

        healthBar.fillAmount = currentHealthValue / maxHealth;
    }

    public void GetDamaged(float damageValue, Transform damager)
    {
        health -= damageValue;
        lastDamager = damager;
        onDamage.Invoke();
        if (health <= 0){
            onDeath?.Invoke(transform);
            dead = true;
            gameObject.SetActive(false);
        }
        timeBetweenHealthBarChange = 0f;
    }

    public void Heal(float healValue)
    {
        if(health <= maxHealth)     health += healValue;
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public Transform GetLastDamager()
    {
        return lastDamager;
    }

    public bool IsDead()
    {
        return dead;
    }

    public float GetHealth()
    {
        return health / maxHealth;
    }

}
