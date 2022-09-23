using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   [SerializeField]
   private float moveSpeed ; 
   [SerializeField]
   private Joystick joystick ; 
   private Rigidbody rb;
   [SerializeField] private bool moving = false;

   [SerializeField] private Animator animator;

    public void MoveSpeed (float newMoveSpeed)
   {
    moveSpeed = newMoveSpeed; 
   }
   private void Move ()
   {
      rb = GetComponent<Rigidbody>() ; 
      rb.velocity = new Vector3 (joystick.Horizontal*moveSpeed ,rb.velocity.y , joystick.Vertical* moveSpeed);
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            moving = true;
            animator.SetTrigger("startWalking");
        }
        else
        {
            moving = false;
            animator.SetTrigger("StopWalking");
        }

   }
   private void FixedUpdate()
   {
      Move(); 
   }

   public bool IsMoving(){
      return moving;
   }
}
