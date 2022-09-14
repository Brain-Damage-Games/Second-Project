using UnityEngine;

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
    private Transform shootTarget;
    private bool shooting;
    [SerializeField] private GameObject shootParticle;
    
    private void Update() 
    {
        if(shooting)    Shoot();
    }

    private void Shoot()
    {
        passedTime += Time.deltaTime;

        if(passedTime >= coolDown)
        {
            GameObject bullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
            bullet.transform.SetParent(gameObject.transform);

            GameObject bulletParticle =  Instantiate(shootParticle, gun.position, Quaternion.LookRotation(gun.position - shootTarget.position));
            Destroy(bulletParticle, 2f);

            if(gameObject.CompareTag("Enemy"))           bullet.layer = LayerMask.NameToLayer("EnemyBullet");
            else if(gameObject.CompareTag("Player"))     bullet.layer = LayerMask.NameToLayer("PlayerBullet");
                
            bullet.GetComponent<Rigidbody>().velocity = (shootTarget.position - gun.position).normalized  * shootingSpeed;
            passedTime = 0f;
        }   
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
            passedTime = 0f;
            shootTarget = null;
        }   
    }
}
