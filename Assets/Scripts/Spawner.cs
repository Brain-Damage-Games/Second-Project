using UnityEngine;

public class Spawner : MonoBehaviour
{
	private Transform spawnPoint;

	[SerializeField]
	private GameObject objectPrefab;

	// number of enemies spawned in one second
	//it starts from 0
	[SerializeField]
	float spawnRate;

	[SerializeField, Min(0)]
	int maxSpawn;

	[SerializeField]
	private float radiusAroundSpawnPoint = 1f;

	private float currentTime = 0f;

	private float countSpawn= 0f;

	private bool spawn = true;

	// here i gave this component to the outpost itself so if want to use it elseway you should make spawnPoint a SERIALIZEDFIELD
    private void Awake()
    {
		spawnPoint = this.transform;
		CheckCount();
    }

    private void FixedUpdate()
    {
		if (spawn)
			Spawnning();
    }

	//if you want to activate spawning from outside this class use this function
	public void Spawn()
	{
		spawn = true;
		countSpawn = 0f;
	}

private void Spawnning()
	{
		currentTime += Time.deltaTime;

		if (currentTime >= (1 / spawnRate))
		{
			GameObject newEnemy = Instantiate(objectPrefab, spawnPoint.position, Quaternion.identity);
			newEnemy.transform.SetParent(this.transform);
			newEnemy.transform.position = GenerateSpawnPoint();
			currentTime = 0f;
			countSpawn += 1;
		}
		CheckCount();
	}
	private Vector3 GenerateSpawnPoint()
	{
		Vector3 v = spawnPoint.transform.position;
		v.x += Random.Range(-radiusAroundSpawnPoint, radiusAroundSpawnPoint);
		v.z += Random.Range(-radiusAroundSpawnPoint, radiusAroundSpawnPoint);
		return v;
	}
	private void CheckCount() 
	{
		if (countSpawn == maxSpawn)
			spawn = false;
	}
}
