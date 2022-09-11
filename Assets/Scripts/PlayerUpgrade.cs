using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerUpgrade : MonoBehaviour
{
    [SerializeField]
    private static int level;


    [SerializeField]
    float maxHealthIncrease = 10f;


    [SerializeField]
    float damageValueIncrease = 10f;

    [SerializeField]
    private GameObject[] statePrefabs;

    private static bool doAction = false;

    private Damageable damageable;

    private Damager damager;

    private GameObject castle;

    private static GameObject[]  upgradableObjects;

    private void Awake()
    {
        level = 1;
        castle = gameObject;
        upgradableObjects = GameObject.FindGameObjectsWithTag("Upgradable");
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
        CastleUpgrade();
    }
    public void CastleUpgrade()
    {
        if (level - 1 < statePrefabs.Length)
        {
            Vector3 position = castle.transform.position;
            Quaternion q = castle.transform.rotation;
            GameObject currentState = castle.transform.GetChild(0).gameObject;
            Destroy(currentState);
            GameObject newState = Instantiate(statePrefabs[level - 1], position, q);
            newState.transform.SetParent(castle.transform);
            newState.transform.SetAsFirstSibling();
        }
        else
        {
            print("PlayerUpgrade: no more statePrefabs for castle");
        }
    }
    public static void CheckForUpgrade()
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
    public static bool CheckIndividualUpgrade(int theLevel)
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
