using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgradable : MonoBehaviour
{
    [SerializeField]
    float maxHealthIncrease = 10f;
    [SerializeField]
    float damageValueIncrease = 10f;
    [SerializeField] private float upgradeRange = 2f;
    private bool playerInRange = false;
    [SerializeField] private GameObject forceField;

    [SerializeField]
    private GameObject[] statePrefabs;
    [SerializeField]
    BaseUpgrade BU;
    int level;
    private GameObject parent;
    private Damageable damageable;
    private Damager damager;
    

    private void Awake()
    {
        level = 1;
        parent = gameObject;
        damageable = GetComponent<Damageable>();
        damager = GetComponent<Damager>();
        forceField.transform.localScale = new Vector3(upgradeRange*2, upgradeRange*2, upgradeRange*2);
        GetComponent<SphereCollider>().radius = upgradeRange;
    }
    //remove comment form codes below to test this class
    
    /*public bool update = false;
    public bool downdate = false;
    private void Update()
    {
        if (update)
        {
            Upgrade();
            update = false;
        }
        if (downdate)
        {
            Downgrade();
            downdate = false;
        }
    }*/
    public void Upgrade()
    {
        
        if (BU.CheckIndividualUpgrade(level))
        {
            level++;
            damageable.SetMaxHealth(damageable.GetMaxHealth() + maxHealthIncrease);
            damager.SetDamageValue(damager.GetDamageValue() + damageValueIncrease);
            
            if (level - 1 < statePrefabs.Length)
            {
                ChangeFace();
            }
            else
            {
                print("Upgradable: no more statePrefabs for this upgradableObject");
            }
        }
        else
            print("all upgradable objects should reach the same level to make upgrade possible for others ");
    }
    public void Downgrade()
    {
        if (level <= 1)
        {
            print("this upgradableObject shall be destroyed");
        }
        else
        {
            level--;
            damageable.SetMaxHealth(damageable.GetMaxHealth() - maxHealthIncrease);
            damager.SetDamageValue(damager.GetDamageValue() - damageValueIncrease);

            if (level - 1 >= 0)
            {
                BU.ChangeProgress(-level);
                ChangeFace();
            }
            else
            {
                print("Upgradable: no more option Downgrade for this upgradableObject");
            }
        }
    }
    private void ChangeFace()
    {
        Vector3 position = parent.transform.position;
        Quaternion q = parent.transform.rotation;
        GameObject currentState = parent.transform.GetChild(0).gameObject;
        Destroy(currentState);
        GameObject newState = Instantiate(statePrefabs[level - 1], position, q);
        newState.transform.SetParent(parent.transform);
        newState.transform.SetAsFirstSibling();
    }

    private void OnTriggerEnter(Collider col){
        if (col.isTrigger) return;
        if (col.CompareTag("Player")){
            forceField.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider col){
        if (col.isTrigger) return;
        if (col.CompareTag("Player")){
            forceField.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    public int GetLevel()
    {
        return level;
    }
}
