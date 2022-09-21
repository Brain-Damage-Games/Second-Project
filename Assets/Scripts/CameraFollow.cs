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
    public IEnumerator shake (float duration, float magnitude)
    {
        Vector3 camPos = transform.localPosition ; 
        float elapsedTime  = 0.0f ;
        while(elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude ; 
            float y = Random.Range(-1f, 1f) * magnitude ; 

            transform.localPosition = new Vector3 (x, y , camPos.z); 

            elapsedTime += Time.deltaTime ; 

            yield return null ; 
        }
        transform.localPosition = camPos ; 
    }
        

}
