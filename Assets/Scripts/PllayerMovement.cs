using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 8f;
    public float forwardSpeed = 4f;

    [Header("Screen Clamp")]
    public float padding = 0.5f;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(moveX, moveY, 0f) * speed;
        Vector3 forward = Vector3.right * forwardSpeed;

        transform.position += (input + forward) * Time.deltaTime;

        ClampToCamera();
    }

    void ClampToCamera()
{
    // Camera bounds
    Vector3 camMin = cam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
    Vector3 camMax = cam.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));

    float minX = camMin.x + padding; // 🚫 can't go behind camera
    float maxX = camMax.x - padding;

    float minY = camMin.y + padding;
    float maxY = camMax.y - padding;

    float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
    float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

    transform.position = new Vector3(clampedX, clampedY, 0f);
}


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Die();
        }
    }

    void Die()
    {
        PlayerDeathManager.Instance?.HandlePlayerDeath();
    }
}
