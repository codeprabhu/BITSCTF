using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManaged : MonoBehaviour
{
    [Header("Score Requirement")]
    public float requiredDistance = 1_000_000f;

    [Header("Score Source")]
    public bool useManualScore = false;
    public float manualScore = 0f;

    [Header("UI")]
    public Image flagImage;            // Drag your FlagImage (UI Image) here
    public GameObject retryScreenUI;   // Optional retry screen

    private bool flagUnlocked = false;

    void Start()
    {
        if (flagImage != null)
            flagImage.gameObject.SetActive(false);

        if (retryScreenUI != null)
            retryScreenUI.SetActive(false);
    }

    void Update()
    {
        if (flagUnlocked) return;

        float currentScore = GetCurrentScore();

        if (currentScore >= requiredDistance)
        {
            flagUnlocked = true;
            ShowFlagAndRetry();
        }
    }

    float GetCurrentScore()
    {
        if (useManualScore)
            return manualScore;

        if (DistanceScoreManager.Instance == null)
            return 0f;

        return DistanceScoreManager.Instance.GetDistance();
    }

    void ShowFlagAndRetry()
    {
        if (flagImage != null)
            flagImage.gameObject.SetActive(true);

        if (retryScreenUI != null)
            retryScreenUI.SetActive(true);

        Time.timeScale = 0f;  // freeze game for dramatic effect
    }

    public void ResetFlag()
    {
        flagUnlocked = false;

        if (flagImage != null)
            flagImage.gameObject.SetActive(false);

        if (retryScreenUI != null)
            retryScreenUI.SetActive(false);

        Time.timeScale = 1f;
    }
}
