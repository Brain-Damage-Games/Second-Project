using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening ; 

public class GateOpening : MonoBehaviour
{
    [SerializeField]
    private Transform armPivot ;
    [SerializeField]
    private float openingDuration = 1.5f , openingAngle = 75f ; 
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            armPivot.DORotate(new Vector3 (0 , 0  , openingAngle) , openingDuration );
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            armPivot.DORotate(new Vector3 (0 , 0  , 0) , openingDuration );
        }
        
    }
}
