using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] float shootRange = 4f;
    private SphereCollider rangeCollider;
    public List<GameObject> enemiesInRange;

    void Awake(){
        rangeCollider = GetComponent<SphereCollider>();
        rangeCollider.radius = shootRange;
        enemiesInRange = new List<GameObject>();
    }
    void Update(){

    }

    private void Shoot(){

    }

    private void OnTriggerEnter(Collider col){
        if (col.tag == "Enemy") enemiesInRange.Add(col.gameObject);
    }

    private void OnTriggerExit(Collider col){
        if (col.tag == "Enemy") enemiesInRange.Remove(col.gameObject);
    }
}
