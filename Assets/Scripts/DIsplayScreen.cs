using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIsplayScreen : MonoBehaviour
{
    float baseScreenResolution = 9f/18f;
    float widthCamera;
    float heightCamera;
    
    Camera thisCamera;
    // Start is called before the first frame update
    void Start()
    {
        thisCamera = GetComponent<Camera>();
        AdjustCamera();
    }
    // Update is called once per frame
    void Update()
    {
        //AdjustCamera();
    }
    private void AdjustCamera(){
        widthCamera = Screen.width;
        heightCamera = Screen.height;
        baseScreenResolution = widthCamera/heightCamera;
        if(baseScreenResolution <= .5f){
            thisCamera.orthographicSize = 5.25f / baseScreenResolution;
        }else{
            thisCamera.orthographicSize = 10.5f;
        }
    }
}
