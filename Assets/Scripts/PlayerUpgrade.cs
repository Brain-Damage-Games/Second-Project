using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerUpgrade : MonoBehaviour
{
    [SerializeField]
    private int level;


    [SerializeField]
    float maxHealthIncrease = 10f;


    [SerializeField]
    float damageValueIncrease = 10f;
   

    [SerializeField]
    private GameObject[] upgradableObjects;

    private static bool doAction = false;

    private Damageable damageable;

    private Damager damager;


    

    private void Awake()
    {
        level = 1;
        damageable = GetComponent<Damageable>();
        damager = GetComponent<Damager>();
    }


    private void Update()
    {

        if (doAction)
        {
            Upgrade();
            doAction = false;
        }
    }
    
    private void Upgrade() 
    {
        level++;

        float currentMaxHealth = damageable.GetMaxHealth();
        float currentDamageValue = damager.GetDamageValue();

        damageable.SetMaxHealth(currentMaxHealth + maxHealthIncrease);
        damager.SetDamageValue(currentDamageValue + damageValueIncrease);
        gameObject.GetComponent<Upgradable>().Upgrade();
       
    }
    public void CheckForUpgrade()
    {
        bool doAct = true;
        foreach(GameObject up in upgradableObjects)
        {
            if(up.GetComponent<Upgradable>().GetLevel() <= level)
            {
                doAct = false;
                break;
            }
        }
        doAction = doAct;

    }
    public bool CheckIndividualUpgrade(int theLevel)
    {
        bool doActUp = false;
        bool doActEq = true;
        foreach (GameObject up in upgradableObjects)
        {
            int l = up.GetComponent<Upgradable>().GetLevel();
            if (l > theLevel || theLevel < level)
            {
                doActUp = true;
                doActEq = false;
                break;
            }
            else if (l < theLevel)
            {
                doActEq = false;
            }
        }
        return (doActUp || doActEq) ;

    }
    public int GetLevel()
    {
        return level;
    }
}
