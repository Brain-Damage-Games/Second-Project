using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private Transform player;
    private float distance;

    [SerializeField]
    float baseSpeed = 1f;

    private void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() 
    {
        distance = Mathf.Abs((player.position - transform.position).magnitude);
        MoveTowardsPlayer();
    }

    public void MoveTowardsPlayer()
    {
        Vector3 goTo = Vector3.Slerp(transform.position, player.position, (distance + baseSpeed) / baseSpeed);
        transform.position = goTo;
    }
}
