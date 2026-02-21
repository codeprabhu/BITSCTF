using UnityEngine;

public class FreezeCamera : MonoBehaviour
{
    private Vector3 lockedPosition;

    void Start()
    {
        // Capture camera position once
        lockedPosition = transform.position;
    }

    void LateUpdate()
    {
        // Force camera to stay here
        transform.position = lockedPosition;
    }
}
