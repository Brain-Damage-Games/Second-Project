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

	[SerializeField]
	private float noOverlapRadius = 0.1f;

	private float currentTime = 0f;

	private float countSpawn= 0f;

	//to avoid spawn in the begining make it false and comment the CheckCounta() in Awake
	public bool spawn = false;

	// here i gave this component to the outpost itself so if want to use it elseway you should make spawnPoint a SERIALIZEDFIELD
	
    private void Awake()
    {
		spawnPoint = transform;
		//CheckCount();
    }

    private void FixedUpdate()
    {
		if (spawn)
			StartSpawning();
    }

	//if you want to activate spawning from outside this class use this function
	public void Spawn()
	{
		spawn = true;
		countSpawn = 0f;
	}
	public Transform Spawn(GameObject gameObject)
	{
		Transform t = gameObject.transform;
		t.position = GenerateSpawnPoint();
		return t;
	}

	private void StartSpawning()
	{
		currentTime += Time.deltaTime;

		if (currentTime >= (1 / spawnRate))
		{
			GameObject newEnemy = Instantiate(objectPrefab, spawnPoint.position, Quaternion.identity);
			newEnemy.transform.SetParent(this.transform);
			newEnemy.transform.position = GenerateSpawnPoint();
			currentTime = 0f;
			countSpawn += 1;
			CheckCount();
		}
	}
	private Vector3 GenerateSpawnPoint()
	{
		Vector3 v = spawnPoint.transform.position;
		do
		{
			v.x += Random.Range(-radiusAroundSpawnPoint, radiusAroundSpawnPoint);
			v.z += Random.Range(-radiusAroundSpawnPoint, radiusAroundSpawnPoint);
		}
		while (ObjectExistHere(v, noOverlapRadius));
		return v;
	}
	private void CheckCount() 
	{
		if (countSpawn >= maxSpawn)
			spawn = false;
	}
	bool ObjectExistHere(Vector3 position,float radius)
	{
		Collider[] intersecting = Physics.OverlapSphere(position, radius);
		if (intersecting.Length == 0)
		{
			return false;
		}
		else
		{
			return true;
		}
	}
	public void SetSpawnRate(float spawnRate)
    {
		this.spawnRate = spawnRate;
    }
	public float GetSpawnRate()
	{
		return spawnRate;
	}
    public void SetMaxSpawn(int maxSpawn)
    {
		this.maxSpawn = maxSpawn;
    }
	public int GetMaxSpawn()
    {
		return maxSpawn;
    }
	public bool GetSpawnStatus()
    {
		return spawn;
    }

}
