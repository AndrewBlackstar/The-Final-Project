using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    

    public void GameWin()
    {
        SceneManager.LoadScene("GameWin");
    }


    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
