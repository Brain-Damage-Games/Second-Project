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

    void LateUpdate(){
        Vector3 desiredPosition = target.position + offset;
        SetZoom(followSmoothing,desiredPosition);
    }
    public void SetZoom(float zoomSpeed, Vector3 targetZoom){
        transform.position = Vector3.Lerp(transform.position,targetZoom,zoomSpeed);
    }

}
