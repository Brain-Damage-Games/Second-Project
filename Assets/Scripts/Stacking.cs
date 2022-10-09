using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening ; 


public class Stacking : MonoBehaviour
{
    [SerializeField]
    private Transform stackPoint ; 
    [SerializeField]
    private float prefabScale = 0 ; 
    [SerializeField]
    private float jumpPower = 1.5f ; 
    [SerializeField]
    private float jumpDuration = 0.5f ; 
    [SerializeField]
    private float delayDestroy = 1f ; 
    [SerializeField] private int stackValue = 10; 
    private List <Transform> items = new List<Transform> () ; 

    public void RemoveItem(int amount , Transform upgradePosition) 
    {
        if(items.Count < amount)
        {
            return ; 
        }
        if(amount <= items.Count ) 
        {
            for(int i = 0 ; i <= amount; i++)
            {
                items[items.Count - i].DOJump(upgradePosition.position , jumpPower , 1 , jumpDuration) ; 
                if (items[items.Count - i].position == upgradePosition.position)
                {
                    Destroy (items[items.Count - i].gameObject ,delayDestroy) ; 
                    items.Remove (items [items.Count - i]) ; 
                }
            }
        }
    }
    public void AddItem(Transform itemToAdd)
    {
        if (items.Count == 0 || Balance.GetBalance() / stackValue > items.Count){
            items.Add(itemToAdd); 
            itemToAdd.DOJump(stackPoint.position + new Vector3(0 , prefabScale * items.Count , 0 ) , jumpPower, 1 , jumpDuration).OnComplete(
                ()=>{
                    itemToAdd.SetParent(stackPoint , true);
                    itemToAdd.localPosition = new Vector3 (0f , prefabScale * items.Count , 0f ); 
                    itemToAdd.localRotation = Quaternion.identity; 
                }
            ); 
        }
        else Destroy(itemToAdd.gameObject);
    }
    private void Start() {
        prefabScale /= 2f ;
    }
}
