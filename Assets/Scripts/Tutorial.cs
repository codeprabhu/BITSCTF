using UnityEngine;
using System.Collections;

public class FirstTimeMovePrompt : MonoBehaviour
{
    private const string SeenKey = "HasSeenMovePrompt";

    void Start()
    {
        // If player has already seen the prompt, never show again
        if (PlayerPrefs.GetInt(SeenKey, 0) == 1)
        {
            gameObject.SetActive(false);
            return;
        }

        // Mark as seen immediately (even if they quit fast)
        PlayerPrefs.SetInt(SeenKey, 1);
        PlayerPrefs.Save();

        // Hide after 1.5 seconds
        StartCoroutine(HideAfterDelay(3f));
    }

    IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
