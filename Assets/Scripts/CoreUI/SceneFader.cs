using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class SceneFader : MonoBehaviour
{
    public string nuevaEscena;
    public float fadeDuration = 0.5f;
    private bool menuShown = false;
    [SerializeField] private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        // Solo cambia si aún no se ha mostrado el menú
        if (!menuShown && Input.anyKeyDown &&
            !Input.GetMouseButtonDown(0) &&
            !Input.GetMouseButtonDown(1) &&
            !Input.GetMouseButtonDown(2))
        {
            menuShown = true; // Evitar múltiples activaciones
            StartCoroutine(FadeOutAndLoadScene());
        }
    }

    IEnumerator FadeOutAndLoadScene()
    {
        float elapsed = 0;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsed / fadeDuration);
            yield return null;
        }

        SceneManager.LoadScene(nuevaEscena); // Cambiar a la nueva escena
    }
}