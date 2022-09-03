using UnityEngine;

public class Spawner : MonoBehaviour
{
	private Transform spawnPoint;

	[SerializeField]
	private GameObject objectPrefab;

	// number of enemies spawned in one second
	[SerializeField, Min(1)]
	float spawnRate;

	[SerializeField, Min(0)]
	int maxSpawn;

	[SerializeField]
	private float radiusAroundSpawnPoint = 1f;

	private float currentTime = 0f;

	private float countSpawn = 0f;

	private bool spawn = true;

    private void Awake()
    {
		spawnPoint = this.transform;
		CheckCount();
    }

    private void FixedUpdate()
    {
		if (spawn)
			Spawn();
    }

    private void Spawn()
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
