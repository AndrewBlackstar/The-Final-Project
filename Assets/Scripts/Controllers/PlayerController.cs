using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public float moveForce = 10f;  // Fuerza de movimiento
    public Transform cameraTransform;  // Cámara para orientar el movimiento
    private Rigidbody rb;  // Componente Rigidbody de la esfera
    private Animator animatorPlayer;  // Animator del jugador

    //private GameManager gameManager;

    private int npcDestroyed = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = 5f;  // Ajuste del drag para reducir el deslizamiento
        animatorPlayer = GetComponent<Animator>();

        //gameManager = GameManager.Instance;
    }

    void FixedUpdate()
    {
        // Movimiento de la esfera con velocidad en lugar de AddForce
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * moveZ + right * moveX).normalized;

        // Aplicar velocidad directamente en el eje X y Z
        rb.linearVelocity = new Vector3(moveDirection.x * moveForce, rb.linearVelocity.y, moveDirection.z * moveForce);

        // Si hay movimiento, rotar el jugador hacia la dirección de movimiento
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // Animación de movimiento
        if (moveX != 0 || moveZ != 0)
        {
            animatorPlayer.SetBool("walk", true);
        }
        else
        {
            animatorPlayer.SetBool("walk", false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("npc"))
        {
            npcDestroyed++;
            Debug.Log("Npc destruido: " + npcDestroyed);

            if (npcDestroyed == 2)
            {
                Debug.Log("You win");
                //gameManager.GameOverWin();
            }
        }


    }
}
