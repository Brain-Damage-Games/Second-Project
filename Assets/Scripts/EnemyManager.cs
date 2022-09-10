using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    private EnemyNavMesh pathFinding;
    private Shooting shooting;
    
    [SerializeField]
    private float shootRange;
  
    
    
    void Start(){
        GetComponent<SphereCollider>().radius = shootRange;
    }



    void OnTriggerEnter(Collider other){

        if(other.CompareTag("Player")){
            
            shooting.SetShooting(true);
            pathFinding.SetStop(true);
            
        }    
    }

    void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            pathFinding.SetStop(false);
            shooting.SetShooting(false);
        }       
    }
        

}
