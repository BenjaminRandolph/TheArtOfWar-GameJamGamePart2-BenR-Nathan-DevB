using System.Collections;
using UnityEngine;

public class SpriteFader : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float fadeInDuration = 1f;   // Time to fade in
    public float pauseDuration = 1f;    // Time to stay fully visible
    public float fadeOutDuration = 1f;  // Time to fade out

    public bool logoScene = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeSequence());
    }

    IEnumerator FadeSequence()
    {
        // Fade In
        yield return StartCoroutine(Fade(0f, 1f, fadeInDuration));

        // Pause at full visibility
        yield return new WaitForSeconds(pauseDuration);

        // Fade Out
        yield return StartCoroutine(Fade(1f, 0f, fadeOutDuration));

        // Fade out completed, optionally disable the GameObject
        gameObject.SetActive(false); // Optional: Disable GameObject after fading out
        logoScene = false;
    }

    IEnumerator Fade(float startAlpha, float targetAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color color = spriteRenderer.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            spriteRenderer.color = new Color(color.r, color.g, color.b, newAlpha);
            yield return null;
        }

        spriteRenderer.color = new Color(color.r, color.g, color.b, targetAlpha);
    }
}
