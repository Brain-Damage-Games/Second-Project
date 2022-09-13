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
    private float maxSize =10.0f;

    [SerializeField]

    private float minSize=5.0f;
  
    [SerializeField]
    private float zoomSpeed = 2.0f;

    [SerializeField]
    private float zoomSmoothness = 2;
    private Camera cam;

     public void Awake()
     {
         cam = this.GetComponent<Camera>();
     }

    void Update(){
        if (Input.GetMouseButton(1))
        {
            
            StartCoroutine(SetZoom(cam.orthographicSize,minSize,zoomSpeed*zoomSmoothness));
        }
        else if (cam.orthographicSize != maxSize)
        {
            cam.orthographicSize=maxSize;
        }
    }

    void LateUpdate(){
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position,desiredPosition,followSmoothing);
    }

    
   IEnumerator SetZoom( float v_start, float v_end, float duration )
   {
    float elapsed = 0.0f;
    while (elapsed < duration )
    {
        cam.orthographicSize = Mathf.Lerp( v_start, v_end, elapsed / duration );
        elapsed += Time.deltaTime;
        yield return null;
     }
     cam.orthographicSize = v_end;
    
   }
        

}