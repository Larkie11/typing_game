using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuCam : MonoBehaviour
{
    public float speedFactor = 0.1f;
    public float zoomFactor = 1.0f;
    public Transform currentMount;
    Transform defaultMount;
    public Camera cameraComp;
    public Camera uiCamera;
    Vector3 lastPosition;
    void Start()
    {
        lastPosition = transform.position;
        defaultMount = currentMount;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, currentMount.position, speedFactor);
        transform.rotation = Quaternion.Slerp(transform.rotation, currentMount.rotation, speedFactor);

        var velocity = Vector3.Magnitude(transform.position - lastPosition);
        cameraComp.GetComponent<Camera>().fieldOfView = 60 + velocity * zoomFactor;
        lastPosition = transform.position;

        if (Input.GetKey(KeyCode.Escape))
        {
            if(transform.position != defaultMount.position)
            {
                 transform.position = Vector3.Lerp(transform.position, currentMount.position, speedFactor);
                 transform.rotation = Quaternion.Slerp(transform.rotation, currentMount.rotation, speedFactor);
                currentMount = defaultMount;
            }
            if (transform.position == defaultMount.position)
            {
                Application.Quit();
            }
        }
    }
    public void setMount(Transform newMount)
    {
        currentMount = newMount;
    }
}
