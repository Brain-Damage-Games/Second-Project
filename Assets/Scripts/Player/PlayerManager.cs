using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] float shootRange = 4f;
    private Shooting shooting;
    private SphereCollider rangeCollider;
    [SerializeField] private List<Transform> targetsInRange = new List<Transform>();
    [SerializeField] private Damageable currentTarget;
    private Damageable damageable;

    void Awake(){
        shooting = GetComponent<Shooting>();
        rangeCollider = GetComponent<SphereCollider>();
        rangeCollider.radius = shootRange;
        damageable = GetComponent<Damageable>();
    }

    private void FindNewTarget(Transform previousTarget){
        if (previousTarget != null) targetsInRange.Remove(previousTarget);
        currentTarget = null;
        shooting.SetShooting(false);
        if (targetsInRange.Count > 0){
            currentTarget = targetsInRange[Random.Range(0,targetsInRange.Count-1)].GetComponent<Damageable>();
            shooting.SetShootTarget(currentTarget.transform);
            shooting.SetShooting(true);
        }
    }

    private void OnTriggerEnter(Collider col){
        if (col.isTrigger) return;
        if (col.tag == "Enemy"){ 
            targetsInRange.Add(col.transform);
            col.GetComponent<Damageable>().onDeath += FindNewTarget;
            if (currentTarget == null) FindNewTarget(null);
        }
    }

    private void OnTriggerExit(Collider col){
        if (col.isTrigger) return;
        if (col.tag == "Enemy"){
            targetsInRange.Remove(col.transform);
            col.GetComponent<Damageable>().onDeath -= FindNewTarget;
            if (currentTarget.gameObject == col.gameObject){
                FindNewTarget(null);
            }
        } 
    }
}
