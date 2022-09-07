using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    // private PathFinding pathFinding;
    private Shooting shooting;
    private Damageable damageable;
    private bool isInArea;

    [SerializeField]
    private float shootRange;
    [SerializeField]
    private float damageValue;
    public SphereCollider collider;
    
    void Start(){
        collider.radius = shootRange;
    }

    void Update(){

        if(isInArea){
            Debug.Log("Found in Area!");
        } 
    }


    void OnTriggerStay(Collider other){

        if(other.CompareTag("Player")){
            isInArea = true;
            shooting.Shoot(other.transform);
            // pathFinding.following=true;
            // pathFinding.SetTarget(other.transform);
        }    
    }

    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Bullet")){
            Debug.Log("bullet found");
            Destroy(collision.gameObject);
        }
    }


    void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            isInArea = false;
            // pathFinding.following=false;
        }       
    }
        

}
