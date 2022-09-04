using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   [SerializeField]
   private float moveSpeed ; 
   [SerializeField]
   private Joystick joystick ; 

   public void Move (Vector3 moveDirection)
   {

   }
   public float SetMoveSpeed (float newMoveSpeed)
   {
    return newMoveSpeed ; 
   }
   private void JoystickMove ()
   {
    var rigidbody = GetComponent<Rigidbody>(); 
    Vector3 direction = transform.position ; 
    direction += new Vector3(joystick.Horizontal* moveSpeed * Time.deltaTime, rigidbody.velocity.y, joystick.Vertical * moveSpeed * Time.deltaTime);
    transform.position = direction ; 
   }
   private void Update() {
    JoystickMove(); 
   }
}
