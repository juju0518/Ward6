using UnityEngine;
using System.Collections;

public class EndingScene : MonoBehaviour
{

    public CanvasGroup fadeOut;
    public float fadeDuration = 2f;
    public string playerTag = "Player";
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;
        if (!other.CompareTag(playerTag)) return;

        hasTriggered = true;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsed = 0f;
        float startAlpha = fadeOut.alpha;
        float finalAlpha = 1f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            fadeOut.alpha = Mathf.Lerp(startAlpha, finalAlpha, t);
            yield return null;
        } 
    }
}
