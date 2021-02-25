using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SecretArea : MonoBehaviour
{
    public GameObject canvas;
    public TextMeshProUGUI youDiscoveredText;
    public float timeScale = .4f;

    private void OnTriggerEnter(Collider other)
    {
        canvas.SetActive(true);
        StartCoroutine(FadeText(youDiscoveredText, 1.0f, true));
    }

    public IEnumerator FadeText(TextMeshProUGUI text, float targetAlpha, bool canvasOn)
    {
        float timeElapsed = 0;
        float startAlpha = text.color.a;
        while (!Mathf.Approximately(text.color.a, targetAlpha))
        {
            timeElapsed += Time.unscaledDeltaTime * timeScale;
            Color newColor = text.color;
            newColor.a = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed);
            text.color = newColor;
            yield return new WaitForEndOfFrame();
        }

        canvas.SetActive(canvasOn);
        if(canvasOn)
            StartCoroutine(FadeText(youDiscoveredText, 0f, false));
    }
}
