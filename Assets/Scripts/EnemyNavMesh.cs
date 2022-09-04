using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] float minPatrolX ,maxPatrolX, minPatrolZ, maxPatrolZ, maxPatrolTime, followRange;

    private NavMeshAgent navMeshAgent;

    private float timer = 0;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timer = maxPatrolTime;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {

        if (Mathf.Abs(Vector3.Distance(player.transform.position, transform.position)) <= followRange)
        {
            navMeshAgent.destination = player.position;
        }
        else
        {
            if (timer > Random.Range(maxPatrolTime, maxPatrolTime + 2))
            {
                navMeshAgent.destination = NextPostion();
                timer = 0;
            }
            timer += Time.deltaTime;
        }
    }

    private Vector3 NextPostion()
    {
        Vector3 pos = new Vector3(Random.Range(minPatrolX, maxPatrolX), 0, Random.Range(minPatrolZ, maxPatrolZ));
        return pos;
    }

    public void SetStop(bool isStopped) 
    {
        navMeshAgent.isStopped = isStopped;
    }

    public void SetBorder(float minX, float maxX, float minZ, float maxZ) 
    {
        minPatrolX = minX;
        maxPatrolX = maxX;
        minPatrolZ = minZ;
        maxPatrolZ = maxZ;
    }
}

