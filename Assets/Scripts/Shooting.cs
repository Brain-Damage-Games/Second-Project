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
    bool shoot;

    [SerializeField]
    float coolDown = 1f;

    float pasedTime = 0f;

    [SerializeField]
    Transform shootTarget;

    private void Update() 
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        print("inside shoot");
        while(shoot)
        {
            print("inside while");
            StartCoroutine(Wait());
            GameObject bullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = (shootTarget.position - gun.position).normalized  * shootingSpeed;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(coolDown);
    }

    public void SetShootTarget(Transform shootTarget)
    {
        this.shootTarget = shootTarget;
    }

    public void SetShoot(bool shoot)
    {
        this.shoot = shoot;
    }




}
