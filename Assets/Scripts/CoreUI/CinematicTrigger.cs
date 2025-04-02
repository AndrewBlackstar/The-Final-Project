using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicTrigger : MonoBehaviour
{
    public string cinematicScene = "Cinematic_Transformacion";
    private bool isCinematicPlaying = false;

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra en el trigger es el jugador
        if (other.CompareTag("Player") && !isCinematicPlaying)
        {
            //listo other.GetComponent<PlayerController>().DisablePlayerControl();
            StartCinematic();
        }
    }

    private void StartCinematic()
    {
        
        isCinematicPlaying = true;     
        //Time.timeScale = 0;
        SceneManager.LoadSceneAsync(cinematicScene, LoadSceneMode.Additive);
       
    }
}
