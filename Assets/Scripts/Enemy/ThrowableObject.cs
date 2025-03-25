using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    public float damage = 10f;

    private void Awake()
    {

        switch (gameObject.tag)
        {
            case "objectSmall":
                damage = 5f;
                break;
            case "objectMedium":
                damage = 15f;
                break;
            case "objectBig":
                damage = 30f;
                break;
        }
    }
    private void Start()
    {
        Debug.Log($"Objeto creado: {gameObject.name}");
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Colisión detectada con: {collision.gameObject.name}");

        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.Log($"Punto de colisión: {contact.point}");
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log($"Golpeó al jugador con {damage} de daño");
        }

    }
    /*private void OnTriggerEnter(Collider collision)
    {

        if (collision.CompareTag("Player"))
        {
            Debug.Log($"Golpeó al jugador con {damage} de daño");
        }
    }*/
}

