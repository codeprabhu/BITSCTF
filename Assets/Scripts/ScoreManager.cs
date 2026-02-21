using UnityEngine;
using TMPro;

public class DistanceScoreManager : MonoBehaviour
{
    public static DistanceScoreManager Instance;

    public TextMeshProUGUI distanceText;

    public long currentDistance;   // CE-friendly authoritative score (8 bytes)

    [Header("Tuning")]
    [Tooltip("How many Unity units = 1 meter of score. Bigger = slower score gain.")]
    public float unitsPerMeter = 3f;

    private Transform player;
    private float startX;
    private float fractional;      // keeps sub-unit movement so pacing stays identical
    private bool isRunning = true;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        var go = GameObject.FindGameObjectWithTag("Player");
        if (go != null)
        {
            player = go.transform;
            startX = player.position.x;
        }
    }

    void Update()
    {
        if (!isRunning || player == null) return;

        float delta = player.position.x - startX;

        // Convert world movement to score units (meters)
        float scaled = delta / unitsPerMeter;

        fractional += scaled;

        long whole = (long)fractional;  // can be + or -

        if (whole != 0)
        {
            currentDistance += whole;  // single stable memory write
            fractional -= whole;
        }

        startX = player.position.x;

        if (distanceText != null)
            distanceText.text = $"{currentDistance} m";
    }

    public long GetDistance()
    {
        return currentDistance;
    }

    public void StopScoring()
    {
        isRunning = false;
    }
}
