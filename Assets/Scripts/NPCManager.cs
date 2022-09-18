using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    private PathFinding pathFinding;
    private Shooting shooting;
    [SerializeField] private DayNightCycle dayNightCycle; 
    [SerializeField] private List<Transform> targetsInRange = new List<Transform>();
    [SerializeField] private Damageable currentTarget;
    [SerializeField] private float pursuitCooldown;
    private float pursuitTimer;
    private Damageable damageable;
    private Transform damager;
    private Transform baseTransform;
    private float currentHealthValue;
    private float lastHealthValue;
    private bool pursuiting = false;
    private bool followingTarget = false;
    [SerializeField] float shootRange = 4f;
    private bool coolDownComplete => pursuitTimer >= pursuitCooldown;
  
    void Awake(){
        shooting = GetComponent<Shooting>();
        pathFinding = GetComponent<PathFinding>();
        damageable = GetComponent<Damageable>();
        dayNightCycle = GetComponent<DayNightCycle>();

        dayNightCycle.onNight += StartNight;
        dayNightCycle.onDay += StartDay;

        GetComponent<SphereCollider>().radius = shootRange;
        baseTransform = GameObject.FindGameObjectWithTag("PlayerBase").transform;

        pursuitTimer = 0f;
    }

    void Update(){ 
        if(pursuiting){
            pursuitTimer += Time.deltaTime;
            if (coolDownComplete){
               FindNewTarget(null);
            }

        }
    }
    
    private void Follow(Transform target){
        pathFinding.SetStop(false);
        pathFinding.SetTarget(target);
    }
    private void Unfollow(){
        pathFinding.SetStop(true);
    }
    private void FindNewTarget(Transform previousTarget){
        if(dayNightCycle.IsNight() && CompareTag("Enemy")){
            Follow(baseTransform);
            currentTarget = baseTransform.GetComponent<Damageable>();
            pursuiting = false;
            pursuitTimer = 0f;
            return;
        }

        if (previousTarget != null) targetsInRange.Remove(previousTarget);
        currentTarget = null;
        shooting.SetShooting(false);
        if (targetsInRange.Count > 0){
            currentTarget = targetsInRange[Random.Range(0,targetsInRange.Count-1)].GetComponent<Damageable>();
            shooting.SetShootTarget(currentTarget.transform);
            shooting.SetShooting(true);
            Unfollow();
        }
    }
    public void StartNight(){
        FindNewTarget(null);
    }
    public void StartDay(){
        //DayStuff
    }
    public void StartPursuit(){
        
        if (!dayNightCycle.IsNight())
            { return;}

        pursuiting = true;
        damager = damageable.GetLastDamager();
        Follow(damager);
        currentTarget = damager.GetComponent<Damageable>();
        currentTarget.onDeath += FindNewTarget;
        shooting.SetShootTarget(currentTarget.transform);
        shooting.SetShooting(true);
    }
    void OnTriggerEnter(Collider col){
        if (col.isTrigger) return;
        if ((CompareTag("Enemy") && (col.CompareTag("Player") || col.CompareTag("PlayerPatrol"))) ||
            (CompareTag("PlayerPatrol") && col.CompareTag("Enemy"))){
                targetsInRange.Add(col.transform);
                col.GetComponent<Damageable>().onDeath += FindNewTarget;
                if (currentTarget == null) FindNewTarget(null);
        }
        
    }

    void OnTriggerExit(Collider col){
        if (col.isTrigger) return;

        if ((CompareTag("Enemy") && (col.CompareTag("Player") || col.CompareTag("PlayerPatrol"))) ||
            (CompareTag("PlayerPatrol") && col.CompareTag("Enemy"))){
                targetsInRange.Remove(col.transform);
                col.GetComponent<Damageable>().onDeath -= FindNewTarget;
                if (currentTarget.gameObject == col.gameObject) FindNewTarget(null);
        }     
    }
        

}
