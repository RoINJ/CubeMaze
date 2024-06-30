using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    private Image image;

    public void FadeOut(float duration)
    {
        gameObject.SetActive(true);
        image = GetComponent<Image>();
        StartCoroutine(Fade(duration, false));
    }

    public void FadeIn(float duration)
    {
        gameObject.SetActive(true);
        image = GetComponent<Image>();
        StartCoroutine(Fade(duration, true));
    }

    private IEnumerator Fade(float duration, bool isAppearing)
    {
        float timer = 0f;
        var color = image.color;
        (float startColor, float endColor) = isAppearing ? (1f, 0f) : (0f, 1f);

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            color.a = Mathf.Lerp(startColor, endColor, timer / duration);
            image.color = color;
            yield return null;
        }

        color.a = endColor;
        image.color = color;

        if (isAppearing)
        {
            gameObject.SetActive(false);
        }
    }
}
