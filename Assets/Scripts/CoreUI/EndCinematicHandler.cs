using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCinematicHandler : MonoBehaviour
{
    // Este m�todo se llamar� mediante el Signal Receiver al final de la cinem�tica
    public void LoadGameScene()
    {
        SceneManager.LoadScene("FirstLevelSceneLu");
    }
}