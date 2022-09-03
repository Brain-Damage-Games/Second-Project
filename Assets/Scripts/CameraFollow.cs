using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float followSmoothing=0.125f;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    public float zoomMultiplier = 2;
    public float defaultFov = 60;
    public float zoomDuration = 2;
    [SerializeField]
    public float zoomSpeed = 2;
    public Camera cam;

    void Update(){
        if (Input.GetMouseButton(1))
        {
            SetZoom(zoomSpeed,defaultFov / zoomMultiplier);
        }
        else if (cam.fieldOfView != defaultFov)
        {
            SetZoom(zoomSpeed,defaultFov);
        }
    }

    void LateUpdate(){
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position,desiredPosition,followSmoothing);
    }

    
    public void SetZoom(float zoomSpeed, float targetZoom){
        float angle = Mathf.Abs((defaultFov / zoomMultiplier) - defaultFov);
        cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, targetZoom, angle / zoomDuration * zoomSpeed);
        
        
    }
        

}
