using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    [SerializeField]
    float shootingSpeed = 1f;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform gun;

    [SerializeField]
    bool shooting;

    [SerializeField]
    float coolDown = 1f;

    float pasedTime = 0f;

    [SerializeField]
    Transform shootTarget;

    [SerializeField]
    private GameObject shootParticlePrefab;

    private void Update() 
    {
        if(shooting)    Shoot();
    }

    public void Shoot()
    {
        pasedTime += Time.deltaTime;

        if(pasedTime >= coolDown)
        {
            GameObject bullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);

            GameObject bulletParticle =  Instantiate(shootParticlePrefab, gun.position, Quaternion.LookRotation(gun.position - shootTarget.position));
            Destroy(bulletParticle, 2f);

            if(gameObject.CompareTag("Enemy"))           bullet.layer = LayerMask.NameToLayer("EnemyBullet");
            else if(gameObject.CompareTag("Player"))     bullet.layer = LayerMask.NameToLayer("Player");
                
            bullet.GetComponent<Rigidbody>().velocity = (shootTarget.position - gun.position).normalized  * shootingSpeed;
            pasedTime = 0f;
        }   
    }

    public bool CanHitTarget()
    {
        RaycastHit hit;
        Physics.Raycast(gun.transform.position, (shootTarget.position - gun.position).normalized, out hit);

        if(gameObject.CompareTag("player") || gameObject.CompareTag("PlayerPatrol"))
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
        if(!shooting)   pasedTime = 0f;
    }




}
