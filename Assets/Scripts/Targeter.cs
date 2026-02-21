using UnityEngine;

public class EnemyLockOn : EnemyBase
{
    public float diveSpeed = 6f;
    private Vector3 direction;
    private bool locked = false;

    protected override void Update()
    {
        base.Update();

        if (!locked && player != null)
        {
            direction = (player.position - transform.position).normalized;
            locked = true;
        }

        transform.position += direction * diveSpeed * Time.deltaTime;
    }
}
