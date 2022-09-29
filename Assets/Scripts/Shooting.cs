using UnityEngine;
using UnityEngine.Events;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private float shootingSpeed = 1f;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform gun;
    [SerializeField]
    private float coolDown = 1f;
    private float passedTime = 0f;
    [SerializeField] private Transform shootTarget;
    [SerializeField] private bool shooting;
    [SerializeField] private GameObject shootParticle;
    private Movement playerMovement;
    public UnityEvent OnShoot;
    private float aimSpeed = 700f;
    private Animator animator;
    [SerializeField]
    private GameObject gunObject;

    private void Awake()
    {
        passedTime = coolDown;
        playerMovement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
        //animation handler
        gunObject.SetActive(true);
        animator.SetInteger("WeaponType_int", 4);
        animator.SetBool("Reload_b", false);
        animator.SetBool("Shoot_b", false);
    }
    
    private void Update() 
    {
        if(shooting)
        {
            passedTime += Time.deltaTime;
            AimAtTarget();
            if (passedTime >= coolDown)
                Shoot();
        }
        //animation handler
        else
            animator.SetBool("Shoot_b", false);
        
    }

    private void AimAtTarget(){
        Vector3 targetDirection = (shootTarget.position - transform.position).normalized;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDirection), aimSpeed*Time.deltaTime);
    }

    public void Shoot()
    {
        if (playerMovement != null && playerMovement.IsMoving()) return;
        GameObject bullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
        // bullet.transform.SetParent(gameObject.transform);

        GameObject bulletParticle =  Instantiate(shootParticle, gun.position, Quaternion.LookRotation(gun.position - shootTarget.position));
        Destroy(bulletParticle, 2f);

        if(gameObject.CompareTag("Enemy"))           bullet.layer = LayerMask.NameToLayer("EnemyBullet");
        else if(gameObject.CompareTag("Player"))     bullet.layer = LayerMask.NameToLayer("PlayerBullet");
                
        bullet.GetComponent<Rigidbody>().velocity = (shootTarget.position - gun.position).normalized  * shootingSpeed;
        passedTime = 0f;
        OnShoot.Invoke();
        bullet = null;

        //animation handler

        animator.SetBool("Shoot_b", true);
    }

    public bool CanHitTarget()
    {
        RaycastHit hit;
        Physics.Raycast(gun.transform.position, (shootTarget.position - gun.position).normalized, out hit);

        if(gameObject.CompareTag("Player") || gameObject.CompareTag("PlayerPatrol"))
            if(hit.transform.gameObject.CompareTag("Enemy"))        return true;
            else                                                    return false;

        else if(gameObject.CompareTag("Enemy"))
            if(hit.transform.gameObject.CompareTag("Player") || hit.transform.gameObject.CompareTag("PlayerPatrol"))        return true;
            else                                                                                                            return false;

        else
            return false;
    }

    public void SetShootTarget(Transform shootTarget)
    {
        this.shootTarget = shootTarget;
    }

    public void SetShooting(bool shooting)
    {
        this.shooting = shooting;
        if(!shooting){
            shootTarget = null;
        }   
    }
    
}
