using UnityEngine;

public class FollowPlayerCam : MonoBehaviour
{
    Transform playerTransform;
    public Vector3 offset = new Vector3(0.06f, 1.17f, 2.98f); // Vector3 para guardar la posición del jugador
    void Start()
    {
        //Buscamos el componente transform por medio del Tag Player usando el método FindWithTag
        playerTransform = GameObject.FindWithTag("Player").transform; 
    }

    
    void LateUpdate()
    {
        //Copia las coordenadas del jugador + unos valores adicionales que permiten posicionarla mejor (offset)
        transform.position = playerTransform.position + offset;
        //2D
        //transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
    }
}
