using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public MovementPlayer movementPlayer;
    [SerializeField] private Animator animator;

    void Start()
    {
        if (movementPlayer == null)
        {
            movementPlayer = GetComponent<MovementPlayer>(); // Busca el componente en el mismo GameObject
        }

        if (animator == null)
        {
            animator = GetComponent<Animator>(); // Busca el Animator en el GameObject
        }
    }

    void Update()
    {
        if (movementPlayer == null || animator == null) return; // Evita errores

        // Detectar movimiento
        bool isMoving = movementPlayer.moveDirection != Vector3.zero;
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && isMoving;
        bool isJumping = !movementPlayer.isGrounded; // Ahora `isJumping` será `true` si NO está en el suelo

        // **Control de animaciones**
        animator.SetBool("isWalking", isMoving);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);




    }
}
