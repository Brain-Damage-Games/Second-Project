using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
  private Vector3 moveDirection ; 
  private Movement movement; 

  private void GetInput () 
  {
    movement.Move(moveDirection) ;
  }
  public Vector3 GetMoveDirection() 
  {
    return moveDirection ; 
  }
}
