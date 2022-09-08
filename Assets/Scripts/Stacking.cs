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
    private int amount =  0 ; 
    private int itemCount = 0 ; 
    private List <Transform> items = new List<Transform> () ; 

    public void RemoveItem(int amount) 
    {
        itemCount = items.Count ; 
        for(int i = 0 ; i <= amount; i++)
        {
            Destroy (items[itemCount - i].gameObject) ; 
            items.Remove (items [itemCount - i]) ; 
        }
    }
    public void AddItem(Transform itemToAdd)
    {
        items.Add(itemToAdd); 
        itemToAdd.DOJump(stackPoint.position + new Vector3(0 , prefabScale * amount , 0 ) , 1.5f , 1 , prefabScale).OnComplete(
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
