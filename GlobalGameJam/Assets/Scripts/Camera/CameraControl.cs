using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public bool cameraActive;
    public GameObject camera;
    public float scrollSpeed = 2f;
    public float scrollSmoothing = 2f;
    private float scrollAmount;
    private float smoothAmount;

    // Start is called before the first frame update
    void Start()
    {
        cameraActive = true;
    }

    // Update is called once per frame


    void Update()
    { if (cameraActive)
        {
            var scroll = Input.GetAxis("Mouse ScrollWheel");

            scrollAmount = scroll * scrollSpeed;
            smoothAmount = Mathf.Lerp(smoothAmount, scrollAmount, 1f / scrollSmoothing);
            camera.transform.position += transform.up * smoothAmount;
        }
        
    }

 
    public void disableCamera()
    {
        cameraActive = false;
    }
    public void enableCamera()
    {
        cameraActive = true;
    }
}
