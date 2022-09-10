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
    private bool shooting;

    [SerializeField]
    float coolDown = 1f;
    float passedTime = 0f;
    private Transform shootTarget;
    private Movement movement;

    void Awake(){
        movement = GetComponent<Movement>();
    }
    private void Update() 
    {
        if(shooting)    Shoot();
    }

    private void Shoot()
    {
        if (movement != null && movement.IsMoving()) return;
        passedTime += Time.deltaTime;

        if(passedTime >= coolDown)
        {
            GameObject bullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);

            if(gameObject.CompareTag("Enemy"))           bullet.layer = LayerMask.NameToLayer("EnemyBullet");
            else if(gameObject.CompareTag("Player"))     bullet.layer = LayerMask.NameToLayer("Player");
                
            bullet.GetComponent<Rigidbody>().velocity = (shootTarget.position - gun.position).normalized  * shootingSpeed;
            passedTime = 0f;
        }   
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
