using UnityEngine;
using UnityEngine.Events;

public class BulletController : MonoBehaviour
{
    private Damager damager;
    [SerializeField] private UnityEvent onCollision;
    [SerializeField] private bool explosive;
    [SerializeField] private float impactRadius = 10f;
    [SerializeField] private float impactForce = 100f;
    public ParticleSystem explosion;
    void Awake(){
        damager = GetComponent<Damager>();
    }
    private void OnCollisionEnter(Collision col){
        if (!explosive) 
        {
            Damageable damageable = col.gameObject.GetComponent<Damageable>();
            if (damageable != null)
            {
                onCollision.Invoke();
                damager.Damage(damageable);
            }

        }
        else
        { 
            
            Collider[] intersecting = Physics.OverlapSphere(transform.position, impactRadius);
            explosion.transform.position = transform.position;
            explosion.Play();
            print(intersecting.Length);
            foreach (Collider coll in intersecting)
            {
                Damageable damageable = coll.gameObject.GetComponent<Damageable>();
                Rigidbody rigidbody = coll.gameObject.GetComponent<Rigidbody>();
                if (damageable != null)
                {
                    onCollision.Invoke();
                    damager.Damage(damageable);
                }
                if (rigidbody != null)
                {
                    Vector3 direction = (coll.transform.position - transform.position).normalized;
                    rigidbody.AddForce(direction * impactForce);

                }
            } 
        }
        Destroy(gameObject);

    }

    public Damager GetDamager(){
        return damager;
    }
}
