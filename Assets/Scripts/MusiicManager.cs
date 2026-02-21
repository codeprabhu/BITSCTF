using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);   // kill duplicates on scene loads
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);  // keep across scenes
    }
}
