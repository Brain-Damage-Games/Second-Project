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

    private bool persuit=false;
    [SerializeField] float shootRange = 4f;
    
    private bool coolDownComplete => pursuitCooldown >= pursuitCooldown;
  
    void Awake(){
        shooting = GetComponent<Shooting>();
        pathFinding = GetComponent<PathFinding>();
        damageable = GetComponent<Damageable>();
        dayNightCycle = GetComponent<DayNightCycle>();

        dayNightCycle.onNight += StartNight;
        dayNightCycle.onDay += StartDay;

        GetComponent<SphereCollider>().radius = shootRange;
        baseTransform = GameObject.FindGameObjectWithTag("Base").GetComponent<Transform>();

        pursuitTimer = 0f;
    }

    void Update(){ 
        if(persuit==true){
            pursuitTimer += Time.deltaTime;
            if (coolDownComplete){
                StartPursuit();
            }
            else{
                persuit=false;
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
    private void FindNewTarget(Transform previousTarget ){
        if(dayNightCycle.IsNight() && this.tag == "Enemy"){
            Follow(baseTransform);
            isPersuiting=false;
            return;
        }


        if (previousTarget != null) targetsInRange.Remove(previousTarget);
        currentTarget = null;
        shooting.SetShooting(false);
        if (targetsInRange.Count > 0){
            currentTarget = targetsInRange[Random.Range(0,targetsInRange.Count-1)].GetComponent<Damageable>();
            shooting.SetShootTarget(currentTarget.transform);
            shooting.SetShooting(true);
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

        persuit=true;
        damager = damageable.GetLastDamager();
        if(shooting.CanHitTarget() == true){
            Follow(damager);
            //Shoot
            currentTarget = damager.GetComponent<Damageable>();
            currentTarget.onDeath += FindNewTarget;
            shooting.SetShootTarget(currentTarget.transform);
            shooting.SetShooting(true);
        }
            pursuitTimer=0f;
    }
    void OnTriggerEnter(Collider col){
        if (col.isTrigger) return;
        if(this.tag == "Enemy"  )
            {  
                if(dayNightCycle.IsNight()==false)
                    {
                        if((col.CompareTag("Player") || col.CompareTag("PlayerPatrol"))&& shooting.CanHitTarget() == true){
                            targetsInRange.Add(col.transform);
                            Unfollow();
                            col.GetComponent<Damageable>().onDeath += FindNewTarget;
                            if (currentTarget == null) FindNewTarget(null);
                    }
                }
                else{
                    return;
                }
        }

        else{
            if (col.tag == "Enemy"&& shooting.CanHitTarget() == true){ 
                targetsInRange.Add(col.transform);
                col.GetComponent<Damageable>().onDeath += FindNewTarget;
                if (currentTarget == null) FindNewTarget(null);
            }
        }
    }
    void OnTriggerExit(Collider col){
        if (col.isTrigger) return;

        if((col.CompareTag("Player") || col.CompareTag("PlayerPatrol"))){
            Follow(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>());
            targetsInRange.Remove(col.transform);
            col.GetComponent<Damageable>().onDeath -= FindNewTarget;
            if (currentTarget.gameObject == col.gameObject){
                FindNewTarget(null);
            }
        }

        if (col.tag == "Enemy"){
            Follow(GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>());
            targetsInRange.Remove(col.transform);
            col.GetComponent<Damageable>().onDeath -= FindNewTarget;
            if (currentTarget.gameObject == col.gameObject){
                FindNewTarget(null);
            }
        }      
    }
        

}
