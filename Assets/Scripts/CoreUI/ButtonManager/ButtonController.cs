using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    // Este método se llamará cuando el botón sea presionado
    public void LoadScene(string sceneName)
    {
        // Cargar la escena por su nombre
        SceneManager.LoadScene(sceneName);
    }
}
