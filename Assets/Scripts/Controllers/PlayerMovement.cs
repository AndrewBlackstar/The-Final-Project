// using UnityEngine;

// public class PlayerMovement : MonoBehaviour
// {
//     [Header("Configuración")]
//     private float horizontal, vertical;
//     public float moveSpeed = 5f;
//     public float rotationSpeed = 10f;
//     public float jumpForce = 7f;
//     [SerializeField] private Rigidbody playerRigidbody;
//     private Vector3 moveDirection;
//     private Animator animator;
//     private bool isGrounded;
    
//     void Start()
//     {
//         playerRigidbody = GetComponent<Rigidbody>();
//         animator = GetComponent<Animator>();
//     }

//     void Update()
//     {
//         // Si está en hit reaction, no procesar input
//         if (animator.GetCurrentAnimatorStateInfo(0).IsName("hit reaction"))
//             return;

//         HandleInput();
//         UpdateAnimator();

//         if (moveDirection != Vector3.zero)
//         {
//             RotateTowardsMovement();
//         }
//     }

//     void FixedUpdate()
//     {
//         ApplyPhysicsMovement();
//     }

//     private void HandleInput()
//     {
//         horizontal = Input.GetAxis("Horizontal");
//         vertical = Input.GetAxis("Vertical");
//         moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

//         if (Input.GetButtonDown("Jump") && isGrounded)
//         {
//             ApplyJump();
//         }
//     }

//     private void UpdateAnimator()
//     {
//         bool isWalking = vertical > 0 && !Input.GetKey(KeyCode.LeftShift);
//         bool isRunning = vertical > 0 && Input.GetKey(KeyCode.LeftShift);
//         bool isIdle = vertical == 0 && horizontal == 0;

//         animator.SetBool("isWalking", isWalking);
//         animator.SetBool("isRunning", isRunning);
//         animator.SetBool("isJumping", !isGrounded);

//         if (isIdle)
//         {
//             animator.SetBool("isWalking", false);
//             animator.SetBool("isRunning", false);
//         }
//     }

//     private void ApplyPhysicsMovement()
//     {
//         if (animator.GetCurrentAnimatorStateInfo(0).IsName("hit reaction"))
//             return; // Bloquea movimiento si está en hit reaction

//         playerRigidbody.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
//     }

//     private void ApplyJump()
//     {
//         playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
//         isGrounded = false;
//     }

//     private void RotateTowardsMovement()
//     {
//         if (moveDirection != Vector3.zero)
//         {
//             Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
//             transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
//         }
//     }

//     private void OnCollisionEnter(Collision other)
//     {
//         if (other.gameObject.CompareTag("Floor"))
//         {
//             isGrounded = true;
//         }
//     }

//     private void OnCollisionExit(Collision other)
//     {
//         if (other.gameObject.CompareTag("Floor"))
//         {
//             isGrounded = false;
//         }
//     }

//     public void TakeHit()
//     {
//         animator.SetTrigger("isHit");
//     }
// }
