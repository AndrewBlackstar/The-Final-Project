using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCinematicHandler : MonoBehaviour
{

    public string cinematicScene = "Cinematic_Transformacion";
    
    public void LoadGameScene(string name)
    {
        SceneManager.LoadScene(name);
    }


    public void EndCinematic()
    {
        SceneManager.UnloadSceneAsync(cinematicScene);
        

    }
}