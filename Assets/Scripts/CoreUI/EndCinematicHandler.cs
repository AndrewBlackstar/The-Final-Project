using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCinematicHandler : MonoBehaviour
{
    // Este método se llamará mediante el Signal Receiver al final de la cinemática
    public void LoadGameScene()
    {
        SceneManager.LoadScene("FirstLevelSceneLu");
    }
}