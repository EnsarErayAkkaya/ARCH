using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFadeOut : MonoBehaviour
{
    public float fadeOutTime;
    Color originalColor;
    void Awake()
    {
        originalColor = GetComponent<TextMeshProUGUI>().color;
    }
    void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().color = originalColor;
        FadeOut();
    }
    public void FadeOut()
    {
        StartCoroutine(FadeOutRoutine());
    }
    private IEnumerator FadeOutRoutine()
    { 
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            text.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t/fadeOutTime));
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
