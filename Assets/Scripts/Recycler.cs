using UnityEngine;
using System.Collections;
public class Recycler : MonoBehaviour
{
    [SerializeField]
    private Transform reSpawnPoint;

    [SerializeField]
    private float radiusAroundReSpawnPoint = 2;

    public bool recycle = false;

    private void Update()
    {
        if (recycle)
        {
            print("true");
            Recycle(1f, this.gameObject);
        }
            
    }
    private Vector3 GenerateSpawnPoint(GameObject objectToRespawn) 
    {
        Vector3 v = objectToRespawn.transform.position;
        v.x = reSpawnPoint.position.x + Random.Range(-radiusAroundReSpawnPoint, radiusAroundReSpawnPoint);
        v.z = reSpawnPoint.position.z + Random.Range(-radiusAroundReSpawnPoint, radiusAroundReSpawnPoint);
        return v;
    }
    public void Recycle(float spawnTime, GameObject objectToRespawn) 
    {
        StartCoroutine(RecycleAfterTime(spawnTime, objectToRespawn));

        recycle = false;
    }
    IEnumerator RecycleAfterTime(float spawnTime, GameObject objectToRespawn)
    {
        yield return new WaitForSeconds(spawnTime);
        objectToRespawn.SetActive(true);
        objectToRespawn.transform.position = GenerateSpawnPoint(objectToRespawn);

    }
    

}
