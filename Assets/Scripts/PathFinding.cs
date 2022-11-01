using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class PathFinding : MonoBehaviour
{
    [SerializeField] private Transform target, player;
    [SerializeField] private GameObject patrolPrefab;

    [SerializeField] float minPatrolX ,maxPatrolX, minPatrolZ, maxPatrolZ, maxPatrolTime;

    private Transform directFollowTarget, patrolPoint;
    private bool directFollow = false, patrol = true;

    //AI
    private AIDestinationSetter destinationSetter;
    private AIPath aIPathComponent;

    //Private Floats
    private float baseSpeed;
    private float originalRange, timer = 0;

    private void Awake()
    {
        patrolPoint = Instantiate(patrolPrefab).transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timer = maxPatrolTime;

        destinationSetter = GetComponent<AIDestinationSetter>();
        aIPathComponent = GetComponent<AIPath>();

        baseSpeed = aIPathComponent.maxSpeed;

        NextPostion();
        SetTarget(patrolPoint);
        destinationSetter.target = target;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            UnFollow();
        else if (Input.GetKeyDown(KeyCode.D))
            Follow(player);

            if (patrol)
            {
                if (timer > Random.Range(maxPatrolTime, maxPatrolTime + 2))
                {
                    NextPostion();
                    SetTarget(patrolPoint);
                    timer = 0;
                }
                timer += Time.deltaTime;
            }
    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        destinationSetter.target = target;
    }

    private void NextPostion()
    {
        patrolPoint.position = new Vector3(Random.Range(minPatrolX, maxPatrolX), 0, Random.Range(minPatrolZ, maxPatrolZ));
    }

    public void SetStop(bool isStopped) 
    {
        if (aIPathComponent == null) return;
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

    public void Follow(Transform target){
        patrol = false;
        this.target = target;
        destinationSetter.target = target;
    }

    public void UnFollow(){
        patrol = true;
    }

    public void Patrol(bool state) 
    {
        patrol = state;
    }
}

