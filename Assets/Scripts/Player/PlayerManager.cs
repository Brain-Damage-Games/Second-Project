using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] float shootRange = 4f;
    private Shooting shooting;
    private SphereCollider rangeCollider;
    public List<GameObject> enemiesInRange;
    private Damageable damageable;

    void Awake(){
        shooting = GetComponent<Shooting>();
        rangeCollider = GetComponent<SphereCollider>();
        rangeCollider.radius = shootRange;
        enemiesInRange = new List<GameObject>();
        damageable = GetComponent<Damageable>();
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
