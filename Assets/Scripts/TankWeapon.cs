using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankWeapon : MonoBehaviour
{
    [SerializeField]
    private float impactRadius = 10f;

    [SerializeField]
    private float impactForce = 100f; 
    
    private void OnCollisionEnter(Collision collision)
    {
        Collider[] intersecting = Physics.OverlapSphere(transform.position, impactRadius);
        foreach(Collider coll in intersecting)
        {
            if (coll.gameObject.GetComponent<Rigidbody>() != null)
            {
                Vector3 direction = (coll.transform.position - transform.position).normalized;
                coll.gameObject.GetComponent<Rigidbody>().AddForce(direction * impactForce);
            }
        }
        Destroy(gameObject);
    }
    
}
