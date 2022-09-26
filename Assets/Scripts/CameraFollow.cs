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

    private float size=5.0f;
  
    [SerializeField]
    private float zoomSpeed = 0.06f;

    [SerializeField]
    private float zoomSmoothness = 5;
    private Camera cam;

     public void Awake()
     {
         cam = this.GetComponent<Camera>();
     }

    void Update(){
        if (Input.GetMouseButton(1))
        {
            ZoomTo(size);
        }

    }

    void LateUpdate(){
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position,desiredPosition,followSmoothing);
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

   public void ZoomTo(float size){
        StartCoroutine(SetZoom(cam.orthographicSize,size,zoomSpeed*zoomSmoothness));
    }
}