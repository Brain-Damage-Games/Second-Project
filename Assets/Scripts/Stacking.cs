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
    private int amount =  0 ; 
    private int itemCount = 0 ; 
    private List <Transform> items = new List<Transform> () ; 

    public void RemoveItem(int amount , Transform upgradePosition) 
    {
        itemCount = items.Count ; 
        if(itemCount < amount)
        {
            return ; 
        }
        if(amount <= itemCount ) 
        {
            for(int i = 0 ; i <= amount; i++)
            {
                items[itemCount - i].DOJump(upgradePosition.position , jumpPower , 1 , jumpDuration) ; 
                if (items[itemCount - i].position == upgradePosition.position)
                {
                    Destroy (items[itemCount - i].gameObject ,delayDestroy) ; 
                    items.Remove (items [itemCount - i]) ; 
                }
            }
        }
    }
    public void AddItem(Transform itemToAdd)
    {
        items.Add(itemToAdd); 
        itemToAdd.DOJump(stackPoint.position + new Vector3(0 , prefabScale * amount , 0 ) , jumpPower, 1 , jumpDuration).OnComplete(
            ()=>{
                itemToAdd.SetParent(stackPoint , true);
                itemToAdd.localPosition = new Vector3 (0f , prefabScale * amount , 0f ); 
                itemToAdd.localRotation = Quaternion.identity; 
                amount++;
            }
        ); 
    }
    private void Start() {
        prefabScale /= 2f ;
    }
}
