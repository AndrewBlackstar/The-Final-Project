using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public static PlayerDetection Instance { get; private set; }
    private Transform playerTransform;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
{
    GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
    if (playerObject != null)
    {
        SetPlayer(playerObject.transform);
    }
    else
    {
        Debug.LogError("No se encontró ningún objeto con la etiqueta 'Player' en la escena.");
    }
}

    public void SetPlayer(Transform player)
    {
        playerTransform = player;
    }

    public Transform GetPlayerTransform()
    {
        return playerTransform;
    }
}
