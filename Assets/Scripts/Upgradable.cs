using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgradable : MonoBehaviour
{

    [SerializeField]
    private int level;

    [SerializeField]
    private GameObject[] statePrefabs;

    [SerializeField]
    private PlayerUpgrade PU;
    private GameObject parent;

    private void Awake()
    {
        level = 1;
        parent = gameObject;
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
        if (PU.CheckIndividualUpgrade(level))
        {
            level++;
            if (level - 1 < statePrefabs.Length)
            {
                Vector3 position = parent.transform.position;
                Quaternion q = parent.transform.rotation;
                GameObject currentState = parent.transform.GetChild(0).gameObject;
                Destroy(currentState);
                GameObject newState = Instantiate(statePrefabs[level - 1], position, q);
                newState.transform.SetParent(parent.transform);
                newState.transform.SetAsFirstSibling();
                PU.CheckForUpgrade();
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
            if (level - 1 >= 0)
            {
                Vector3 position = parent.transform.position;
                Quaternion q = parent.transform.rotation;
                GameObject currentState = parent.transform.GetChild(0).gameObject;
                Destroy(currentState);
                GameObject newState = Instantiate(statePrefabs[level - 1], position, q);
                newState.transform.SetParent(parent.transform);
                newState.transform.SetAsFirstSibling();
            }
            else
            {
                print("Upgradable: no more option Downgrade for this upgradableObject");
            }
        }
    }
    public int GetLevel()
    {
        return level;
    }
}
