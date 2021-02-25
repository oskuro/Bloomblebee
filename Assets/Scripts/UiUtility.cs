using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class UiUtility : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip buttonHoverClip = null;
    public AudioClip buttonClickClip = null;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static IEnumerator ScaleRect(RectTransform rect, Vector3 targetScale, float timeScale)
    {
        float timeElapsed = 0;
        Vector3 startScale = rect.localScale;

        while (timeElapsed <= 1f)
        {
            timeElapsed += Time.unscaledDeltaTime * timeScale;

            rect.localScale = Vector3.Lerp(startScale, targetScale, timeElapsed);
            yield return new WaitForEndOfFrame();
        }
    }

    public static IEnumerator PulseRect(RectTransform rect, Vector3 targetScale, float timeScale)
    {
        float timeElapsed = 0;
        Vector3 startScale = rect.localScale;

        while (timeElapsed <= 1f)
        {
            timeElapsed += Time.unscaledDeltaTime * timeScale;

            rect.localScale = Vector3.Lerp(startScale, targetScale, timeElapsed);
            yield return new WaitForEndOfFrame();
        }
        timeElapsed = 0;
        targetScale = startScale;
        startScale = rect.localScale;

        while (timeElapsed <= 1f)
        {
            timeElapsed += Time.unscaledDeltaTime * timeScale;

            rect.localScale = Vector3.Lerp(startScale, targetScale, timeElapsed);
            yield return new WaitForEndOfFrame();
        }
    }

    public static IEnumerator FadeImage(Image img, float targetAlpha)
    {
        float timeElapsed = 0;
        float startAlpha = img.color.a;
        while (!Mathf.Approximately(img.color.a, targetAlpha))
        {
            timeElapsed += Time.unscaledDeltaTime * 0.5f;
            Color newColor = img.color;
            newColor.a = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed);
            img.color = newColor;
            yield return new WaitForEndOfFrame();
        }
        if (targetAlpha == 0)
            img.enabled = false;
    }

    public void PlayButtonHoverSound()
    {
        if (buttonHoverClip)
            audioSource.PlayOneShot(buttonHoverClip);
    }

    public void PlayButtonClickSound()
    {
        if(buttonClickClip)
            audioSource.PlayOneShot(buttonClickClip);
    }
}
