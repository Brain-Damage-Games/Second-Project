using UnityEngine;
using System.Collections;
public class Recycler : MonoBehaviour
{
    [SerializeField]
    private float radiusAroundReSpawnPoint = 2;

    private GameObject outPost;

    private void Awake()
    {
        outPost = gameObject;
    }

    // to check this class  you can simply remove all commented codes 
    // also remove the comment from line 37
    // don't forget to give it an enemy that already exists in the game

    /*
    public bool recycle = false;
    public GameObject enemy;
    private void Update()
    {
        if (recycle)
        {
            Recycle(1f, enemy);
        }
            
    }
    */
    public void Recycle(float spawnTime, GameObject objectToRespawn) 
    {
        StartCoroutine(RecycleAfterTime(spawnTime, objectToRespawn));

        //recycle = false;
    }
    IEnumerator RecycleAfterTime(float spawnTime, GameObject objectToRespawn)
    {
        yield return new WaitForSeconds(spawnTime);
        objectToRespawn.SetActive(true);
        objectToRespawn.transform.position = outPost.GetComponent<Spawner>().Spawn(objectToRespawn).position;

    }
    

}
