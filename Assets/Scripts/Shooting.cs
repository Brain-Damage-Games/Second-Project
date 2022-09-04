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

    public void Shoot(Transform shootTarget)
    {
        GameObject bullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = (shootTarget.position - gun.position).normalized  * shootingSpeed;
    }

}
