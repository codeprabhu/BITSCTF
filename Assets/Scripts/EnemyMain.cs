using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("World Scroll")]
    public float worldScrollSpeed = 2.5f;

    [Header("Cleanup")]
    public float destroyX = -12f;

    protected Transform player;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    protected virtual void Update()
    {
        // World scroll (everything moves left)
        transform.position += Vector3.left * worldScrollSpeed * Time.deltaTime;

        // Destroy when off screen
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}
