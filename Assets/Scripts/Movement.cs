using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   [SerializeField]
   private float moveSpeed ; 
   [SerializeField]
   private Joystick joystick ; 
   [SerializeField]
   private float maxXRot = 15f ;
   private Rigidbody rb;
   [SerializeField] private bool moving = false; 
   public void MoveSpeed (float newMoveSpeed)
   {
    moveSpeed = newMoveSpeed; 
   }
   private void Move ()
   {
      rb = GetComponent<Rigidbody>() ; 
      rb.velocity = new Vector3 (joystick.Horizontal*moveSpeed ,0 , joystick.Vertical* moveSpeed);
      LimitRotation () ; 

      if (joystick.Horizontal != 0 || joystick.Vertical != 0) moving = true;
      else moving = false; 
   }
   private void LimitRotation ()
   {
      Vector3 playerAngles = transform.rotation.eulerAngles ; 
      playerAngles.z = 0 ; 
      playerAngles.x = Mathf.Clamp(playerAngles.x , 0 , maxXRot) ; 
      
   }
   private void FixedUpdate()
   {
      Move(); 
   }

   public bool IsMoving(){
      return moving;
   }
}
