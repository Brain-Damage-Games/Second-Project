using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
  private Touch touch ; 
  private Vector3 touchPosition;
  private Vector3 moveDirection ; 
  private Movement movement; 

  private void GetInput () 
  {
    //movement.Move(moveDirection) ;
  }
  public Vector3 GetMoveDirection() 
  {
    return moveDirection ; 
  }
  private void Touch ()
  {
    if (Input.touchCount != 1 )
    {
        return;
    }
    touch = Input.touches[0];
    touchPosition = touch.position;
    if (touch.phase == TouchPhase.Began)
    {

    }
    if(touch.phase == TouchPhase.Moved)
    {

    }
    if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) 
    {

    }
  }
  void Update()
  {
    Touch(); 
  }
}
