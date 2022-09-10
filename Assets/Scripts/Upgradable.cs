using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgradable : MonoBehaviour
{

    [SerializeField]
    private int level;

    [SerializeField]
    private GameObject[] statePrefabs;

    private GameObject parent;

    private void Awake()
    {
        level = 1;
        parent = gameObject;
    }
    //remove comment form codes below to test this class
    /*
    public bool update = false;
    private void Update()
    {
        if (update)
        {
            Upgrade();
            update = false;
        }
    }*/
    public void Upgrade()
    {

        level++;
        if (level - 2 < statePrefabs.Length)
        {
            Vector3 position = parent.transform.position;
            Quaternion q = parent.transform.rotation;
            GameObject currentState = parent.transform.GetChild(0).gameObject;
            Destroy(currentState);
            GameObject newState = Instantiate(statePrefabs[level - 2], position, q);
            newState.transform.SetParent(parent.transform);
            newState.transform.SetAsFirstSibling();
            PlayerUpgrade.CheckForUpgrade();
        }
        else
        {
            print("Upgradable: no more statePrefabs for this upgradableObject");
        }
    }
    public int GetLevel()
    {
        return level;
    }
}
