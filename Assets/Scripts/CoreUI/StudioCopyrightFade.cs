using UnityEngine;
using TMPro;
using System.Collections;

public class StudioCopyrightFade : MonoBehaviour
{
    public CanvasGroup panelCanvasGroup;
    public TextMeshProUGUI studioText;
    public TextMeshProUGUI copyRightText;

    public float fadeDuration = 2f;
    private float targetAlpha = 1f; // Totalmente visible

    void Start()
    {
        panelCanvasGroup.alpha = 0;
        studioText.alpha = 0;
        copyRightText.alpha = 0; // Agregar el fade a este texto también

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(0, targetAlpha, elapsed / fadeDuration);

            // Aplicamos el alpha a todos los elementos
            panelCanvasGroup.alpha = newAlpha;
            studioText.alpha = newAlpha;
            copyRightText.alpha = newAlpha;

            yield return null;
        }

        // Asegurar que queda en el targetAlpha exacto
        panelCanvasGroup.alpha = targetAlpha;
        studioText.alpha = targetAlpha;
        copyRightText.alpha = targetAlpha;
    }
}
