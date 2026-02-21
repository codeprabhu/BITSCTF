using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [Header("Follow")]
    public float smoothSpeed = 5f;
    public float xOffset = 3f;

    [Header("Vertical Clamp")]
    public float minY = -2.5f;
    public float maxY =  2.5f;

    private float fixedZ;

    void Start()
    {
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        // Follow X only
        float targetX = target.position.x + xOffset;

        // Clamp Y
        float clampedY = Mathf.Clamp(target.position.y, minY, maxY);

        Vector3 desiredPos = new Vector3(
            targetX,
            clampedY,
            fixedZ
        );

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPos,
            smoothSpeed * Time.deltaTime
        );
    }
}
