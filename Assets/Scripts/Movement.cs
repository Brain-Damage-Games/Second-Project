using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed ; 
    [SerializeField]
    private float rotationSpeed = 700f ; 
    [SerializeField]
    private Joystick joystick ;
    [SerializeField] private bool moving = false;
    [SerializeField] private float maxMoveSpeed;
    private PlayerManager playerManager;
    private Animator playerAnimator;

   void Awake(){
        playerManager = GetComponent<PlayerManager>();
        playerAnimator = GetComponent<Animator>();
   }
   public void MoveSpeed (float newMoveSpeed)
   {
        moveSpeed = newMoveSpeed; 
   }
   private void Move ()
   {
        Vector3 direction = new Vector3(joystick.Horizontal, 0f, joystick.Vertical) ; 
        direction.Normalize() ; 
        transform.Translate(-direction * moveSpeed * Time.deltaTime , Space.World);
        if (direction != Vector3.zero && !playerManager.hasTarget())
        {
           Quaternion toRotation = Quaternion.LookRotation(-direction , Vector3.up) ; 
           transform.rotation = Quaternion.RotateTowards(transform.rotation , toRotation , rotationSpeed* Time.deltaTime ) ; 
        }

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            moving = true;
            playerAnimator.SetFloat("Speed_f", moveSpeed / maxMoveSpeed);
        }
        else
        {
            moving = false;
            playerAnimator.SetFloat("Speed_f", 0);
        }
   }
   private void Update()
   {
        Move(); 
   }

   public bool IsMoving(){
        return moving;
   }
}
