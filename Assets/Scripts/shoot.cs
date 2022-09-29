using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Remove this script as soon as you dealed with animations
public class shoot : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject gun;

    public bool idleG;
    public bool inG;
    public bool shootG;
    public bool die;
    //public bool reload;
    public bool move;
    public float moveS;

   private void Update()
   {
        if (idleG)
        {
            TakeOutGun();
            idleG = false;
        }
        if (inG)
        {
            TakeInGun();
            inG = false;
        }
        if (shootG)
        {
            Shoot();
            shootG = false;
        }
        /*if (reload)
        {
            Reload();
            reload = false;
        }*/
        if (move)
        {
            Move();
        }
        if (die)
        {
            Die();
        }

   }

    private void TakeOutGun()
    {
        gun.SetActive(true);
        animator.SetInteger("WeaponType_int", 4);
        animator.SetBool("Reload_b", false);
        animator.SetBool("Shoot_b", false);
    }
    private void Shoot()
    {
        animator.SetInteger("WeaponType_int", 4);
        animator.SetBool("Reload_b", false);
        animator.SetBool("Shoot_b", true);

    }

    /*private void Reload()
    {
        animator.SetInteger("WeaponType_int", 4);
        animator.SetBool("Reload_b", true);
        animator.SetBool("Shoot_b", false);
    }*/
    private void TakeInGun()
    {
        animator.SetInteger("WeaponType_int", -1);
        animator.SetBool("Reload_b", false);
        animator.SetBool("Shoot_b", false);
        gun.SetActive(false);
    }
    private void Move()
    {
        animator.SetFloat("Speed_f", moveS);
    }
    private void Die()
    {
        animator.SetInteger("DeathType_int", 2);
        animator.SetBool("Death_b",true);
        animator.SetBool("Shoot_b", false);
        gun.SetActive(false);
    }
}