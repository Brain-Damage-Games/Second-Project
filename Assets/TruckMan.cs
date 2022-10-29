using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TruckMan : MonoBehaviour
{
    [SerializeField]
    float minX, maxX, minZ, maxZ, maxTravelTime;

    float time = 0;

    [SerializeField]
    Transform destiantion, truck, player;

    Transform _prefab;

    void Start()
    {
        _prefab = Instantiate(destiantion);
        NewPoint();
        GetComponent<AIDestinationSetter>().target = _prefab.transform;
    }

    private void Update()
    {

        if (time > maxTravelTime || Mathf.Abs(Vector3.Distance(_prefab.position, transform.position)) < 10f)
        {
            NewPoint();
            time = 0;
        }
        else 
        {
            time += Time.deltaTime;
        }


    }

    void NewPoint() 
    {
        _prefab.position = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
    }
}
