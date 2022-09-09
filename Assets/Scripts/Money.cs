using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    bool isAlreadyCollected = false ; 
    private void OnTriggerEnter(Collider other)
    {
        if(isAlreadyCollected)
        {
            return ; 
        }
        if(other.CompareTag("Player"))
        {
            Stacking stacking  ; 
            if(other.TryGetComponent(out stacking))
            {
                stacking.AddItem(this.transform ) ; 
                isAlreadyCollected = true ; 
            }
        }
    }
}
