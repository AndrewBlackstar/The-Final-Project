using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string nuevaEscena; // Nombre de la escena a  cambiar
    private bool menuShown = false;

    void Update()
    {
        // Si ya se cambió la escena, no hacer nada más
        if (menuShown)
        {
            return;
        }

        // Detecta cualquier tecla, pero ignora clics del mouse
        if (Input.anyKeyDown && 
            !Input.GetMouseButtonDown(0) && 
            !Input.GetMouseButtonDown(1) && 
            !Input.GetMouseButtonDown(2))
        {
            LoadMenuScene();
        }
    }

    void LoadMenuScene()
    {
        menuShown = true; // Evitar múltiples activaciones
        SceneManager.LoadScene(nuevaEscena); // Cambia a la nueva escena
    }
}
