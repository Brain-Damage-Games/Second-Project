using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutpostUpgrade : MonoBehaviour
{
    private int level;

    private Damageable damageable;

    private Spawner spawner;

    [SerializeField]
    float maxHealthIncrease = 10f;

    [SerializeField]
    int maxSpawnIncrease = 10;

    [SerializeField]
    float spawnRateIncrease = 10f;

    [SerializeField]
    private GameObject[] statePrefabs;


    private GameObject outPost;


    private void Awake()
    {
        level = 1;
        outPost = gameObject;
        outPost.transform.SetAsFirstSibling();
        damageable = GetComponent<Damageable>();
        spawner = GetComponent<Spawner>();
    }
    //remove comment form codes below to test this class
    /*public bool update = false;
    private void Update()
    {
        if (update)
        {
            Upgrade();
            update = false;
        }
    }
    */
    public void Upgrade()
    {

        level++;

        int curentMaxSpawn = spawner.GetMaxSpawn();
        float currentSpawnRate = spawner.GetSpawnRate();
        float currentMaxHealth = damageable.GetMaxHealth();

        spawner.SetMaxSpawn(curentMaxSpawn + maxSpawnIncrease);
        spawner.SetSpawnRate(currentSpawnRate + spawnRateIncrease);
        damageable.SetMaxHealth(currentMaxHealth + maxHealthIncrease);

        if (level - 1 < statePrefabs.Length)
        {
            Vector3 position = outPost.transform.position;
            Quaternion q = outPost.transform.rotation;
            GameObject currentState = outPost.transform.GetChild(0).gameObject;
            Destroy(currentState);
            GameObject newState = Instantiate(statePrefabs[level - 1], position,q);
            newState.transform.SetParent(outPost.transform);
            newState.transform.SetAsFirstSibling();
        }
        else
        {
            print("OutPostUpgrade: no more statePrefabs");
        }

    }

    public int GetLevel()
    {
        return level;
    }
}
