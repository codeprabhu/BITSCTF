using UnityEngine;

public class EnemyZigZag : EnemyBase
{
    public float amplitude = 1.5f;
    public float frequency = 3f;

    private float startY;
    private float time;

    protected override void Start()
    {
        base.Start();
        startY = transform.position.y;
    }

    protected override void Update()
    {
        base.Update();

        time += Time.deltaTime;
        float yOffset = Mathf.Sin(time * frequency) * amplitude;
        transform.position = new Vector3(transform.position.x, startY + yOffset, 0f);
    }
}
