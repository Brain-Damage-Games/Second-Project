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
    GameObject shootTarget;

    [SerializeField]
    float distanceFromShooter = 1f;

    public void Shoot()
    {
        Vector3 toTheTarget = (shootTarget.transform.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, gameObject.transform.position + toTheTarget * distanceFromShooter, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = toTheTarget  * shootingSpeed;
    }

    private void Update() 
    {
        if(Input.GetMouseButtonDown(0))     
            Shoot();
    }

}
