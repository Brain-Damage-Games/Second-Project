using UnityEngine;
using UnityEngine.Events;

public class BulletController : MonoBehaviour
{
    private Damager damager;
    [SerializeField] private UnityEvent onCollision;
    void Awake(){
        damager = GetComponent<Damager>();
    }
    private void OnCollisionEnter(Collision col){
        Damageable damageable = col.gameObject.GetComponent<Damageable>();
        if (damageable != null){
            onCollision.Invoke();
            damager.Damage(damageable);
        }
        Destroy(gameObject);
    }

    public Damager GetDamager(){
        return damager;
    }
}
