using UnityEngine;

// Following camera's rotation used for item cameras and some items
public class FollowCameraRotation : MonoBehaviour
{
    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void Update()
    {
        transform.rotation = cam.rotation;
    }
}
