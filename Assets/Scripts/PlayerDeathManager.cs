using UnityEngine;

public class PlayerDeathManager : MonoBehaviour
{
    public static PlayerDeathManager Instance;

    [Header("Death Screen")]
    public GameObject deathScreenUI;

    [Header("Enemy Cleanup")]
    public string enemyTag = "Enemy";
    public LayerMask enemyLayer;

    private bool hasDied = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void HandlePlayerDeath()
    {
        Debug.Log("PLAYER DEATH TRIGGERED");
        if (hasDied) return;
        hasDied = true;

        // Stop scoring
        DistanceScoreManager.Instance?.StopScoring();

        // Remove all enemies
        DespawnAllEnemies();

        // Freeze the entire world
        Time.timeScale = 0f;

        // Show death screen
        if (deathScreenUI != null)
            deathScreenUI.SetActive(true);
    }

    void DespawnAllEnemies()
    {
        // By tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        // Optional: by layer (for hitbox-only enemies)
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (((1 << obj.layer) & enemyLayer) != 0)
            {
                Destroy(obj);
            }
        }
    }
}
