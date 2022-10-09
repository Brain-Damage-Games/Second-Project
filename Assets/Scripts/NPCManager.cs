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
    private Transform playerBase;
    private float currentHealthValue;
    private float lastHealthValue;
    private bool pursuiting = false;
    [SerializeField] float shootRange = 4f;
    /*private Animator animator;
    [SerializeField]
    private GameObject gunObject;*/
    private bool coolDownComplete => pursuitTimer >= pursuitCooldown;
  
    void Awake(){
        shooting = GetComponent<Shooting>();
        pathFinding = GetComponent<PathFinding>();
        damageable = GetComponent<Damageable>();
        dayNightCycle = GameObject.FindGameObjectWithTag("DayNight").GetComponent<DayNightCycle>();
        if (dayNightCycle != null){
            dayNightCycle.onNight += StartNight;
            dayNightCycle.onDay += StartDay;
        }

        GetComponent<SphereCollider>().radius = shootRange;
        playerBase = GameObject.FindGameObjectWithTag("PlayerBase").transform;
        //animator = GetComponent<Animator>();
        foreach(Collider col in Physics.OverlapSphere(transform.position, shootRange)){
            if (CompareTag("Enemy")){
                if (col.CompareTag("Player") || col.CompareTag("PlayerPatrol")){
                    targetsInRange.Add(col.transform);
                }
            }
            else if (CompareTag("PlayerPatrol")){
                if (col.CompareTag("Enemy")){
                    targetsInRange.Add(col.transform);
                }
            }
        }
        FindNewTarget(null);
        pursuitTimer = 0f;
    }

    void Update(){ 
        if(pursuiting){
            pursuitTimer += Time.deltaTime;
            if (coolDownComplete){
               FindNewTarget(null);
            }
        }

        if(currentTarget != null){
            if (shooting.CanHitTarget()) pathFinding.SetStop(true);
            else pathFinding.SetStop(false);
        }
    }
    
    private void FindNewTarget(Transform previousTarget){
        if(dayNightCycle.IsNight() && CompareTag("Enemy")){
            pathFinding.Follow(playerBase);
            currentTarget = playerBase.GetComponent<Damageable>();
            pursuiting = false;
            pursuitTimer = 0f;
            return;
        }

        if (previousTarget != null) targetsInRange.Remove(previousTarget);
        currentTarget = null;
        if (targetsInRange.Count > 0){
            currentTarget = targetsInRange[Random.Range(0,targetsInRange.Count-1)].GetComponent<Damageable>();
            shooting.SetShootTarget(currentTarget.transform);
            shooting.SetShooting(true);
        }
        else{
            shooting.SetShooting(false);
            pathFinding.SetStop(false);
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
        pathFinding.Follow(damager);
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

    /*private void Die()
    {
        animator.SetInteger("DeathType_int", 2);
        animator.SetBool("Death_b", true);
        animator.SetBool("Shoot_b", false);
        gunObject.SetActive(false);
    }*/
         
}
