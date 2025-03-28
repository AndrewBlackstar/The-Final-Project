using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    // Variables públicas para ajustar las fuerzas de movimiento y salto
    public float moveForce = 10f;  // Fuerza de movimiento
    public float jumpForce = 10.0f;  // Fuerza de salto

    // Referencias a otros componentes y objetos en la escena
    public Transform cameraTransform;  // Cámara para orientar el movimiento
    private Rigidbody rb;  // Componente Rigidbody de la esfera
    private Animator animatorPlayer;  // Animator del jugador

    // Variables de estado del jugador
    private bool isGrounded; // Indica si el jugador está en el suelo
    private bool hasJumped;  // Indica si el jugador ha saltado recientemente

    // Componentes de audio para efectos de sonido
    public AudioSource jumpAudioSource;
    public AudioSource landAudioSource;
    public AudioSource moveAudioSource;

    void Start()
    {
        // Inicialización de componentes y variables
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = 5f;  // Ajuste del drag para reducir el deslizamiento
        animatorPlayer = GetComponent<Animator>();
        
        hasJumped = false; // Inicialmente no ha saltado
        isGrounded = true; // Se asume que empieza en el suelo
    }

    void FixedUpdate()
    {
        // Capturar entrada del jugador
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Obtener las direcciones de la cámara para orientar el movimiento
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0; // Ignorar componente vertical
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // Calcular la dirección de movimiento basada en la cámara
        Vector3 moveDirection = (forward * moveZ + right * moveX).normalized;

        // Aplicar velocidad directamente en el eje X y Z
        rb.linearVelocity = new Vector3(moveDirection.x * moveForce, rb.linearVelocity.y, moveDirection.z * moveForce);

        // Si hay movimiento, rotar el jugador hacia la dirección de movimiento
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // Control de animaciones del jugador
        animatorPlayer.SetBool("isWalking", moveX != 0 || moveZ != 0);

        // Si el jugador presiona espacio y está en el suelo, salta
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            hasJumped = true; // Registrar que ha saltado

            // Reproducir sonido de salto si está asignado
            if (jumpAudioSource != null)
            {
                jumpAudioSource.Play();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Asegúrate de etiquetar el suelo como "Ground"
        {
            isGrounded = true;
            hasJumped = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
