using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private Transform player;
    private float distance;
    [SerializeField] private float magnetDistance = 10f;

    [SerializeField]
    float baseSpeed = 1f;

    private void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() 
    {
        distance = Mathf.Abs((player.position - transform.position).magnitude);
        if (distance <= magnetDistance)
            MoveTowardsPlayer();
    }

    public void MoveTowardsPlayer()
    {
        Vector3 goTo = Vector3.Slerp(transform.position, player.position, (distance + baseSpeed)*baseSpeed*Time.deltaTime);
        transform.position = goTo;   
    }
    
    private void OnTriggerEnter(Collider col){
        if (col.isTrigger) return;
        if (col.CompareTag("Player")){
            if (CompareTag("Money")){
                Balance.ChangeBalance(10);
                print(Balance.GetBalance());
                col.GetComponent<Stacking>().AddItem(transform);
                this.enabled = false;
                // Destroy(gameObject);
            }
        }
    }
}
