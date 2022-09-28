using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class PathFinding : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject patrolPrefab;

    [SerializeField] float minPatrolX ,maxPatrolX, minPatrolZ, maxPatrolZ, maxPatrolTime, followRange;

    private Transform directFollowTarget, patrolPoint;
    private bool directFollow = false;

    //AI
    private AIDestinationSetter destinationSetter;
    private AIPath aIPathComponent;

    //Private Floats
    private float baseSpeed;
    private float originalRange, timer = 0;

    private void Awake()
    {
        patrolPoint = Instantiate(patrolPrefab).transform;
        // player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timer = maxPatrolTime;
        originalRange = followRange;

        destinationSetter = GetComponent<AIDestinationSetter>();
        aIPathComponent = GetComponent<AIPath>();

        baseSpeed = aIPathComponent.maxSpeed;
    }

    private void Update()
    {
        if (!directFollow)
        {
            if ((Mathf.Abs(Vector3.Distance(target.position, transform.position)) <= followRange || directFollow ) && !target.CompareTag("PatrolPoint"))
            {
                destinationSetter.target = target;
            }
            else
            {
                if (timer > Random.Range(maxPatrolTime, maxPatrolTime + 2))
                {
                    NextPostion();
                    SetTarget(patrolPoint);
                    destinationSetter.target = target;
                    timer = 0;
                }
                timer += Time.deltaTime;
            }
        }
    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void NextPostion()
    {
        patrolPoint.position = new Vector3(Random.Range(minPatrolX, maxPatrolX), 0, Random.Range(minPatrolZ, maxPatrolZ));
    }

    public void SetStop(bool isStopped) 
    {
        if (isStopped)
            aIPathComponent.maxSpeed = 0;
        else
            aIPathComponent.maxSpeed = baseSpeed;
    }

    public void SetBorder(float minX, float maxX, float minZ, float maxZ) 
    {
        minPatrolX = minX;
        maxPatrolX = maxX;
        minPatrolZ = minZ;
        maxPatrolZ = maxZ;
    }

    public void SetRange(float newRange) 
    {
        followRange = newRange;
    }

    public void RevertRange() 
    {
        followRange = originalRange;
    }

    public void Follow(Transform target){
        directFollow = true;
        this.target = target;
    }

    public void UnFollow(){
        directFollow = false;
    }
}

