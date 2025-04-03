using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [Header("Configuración")]
    float horizontal, vertical;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 7f;
    [SerializeField] private Rigidbody playerRigidbody;
    public Vector3 moveDirection;
    public bool isGrounded;
    Animator animator;

    [Header("Armas")]
    public Transform weaponsParent;
    [SerializeField] private GameObject[] weapons;
    private int lastWeaponID = -1;

    [SerializeField] Camera playerCamera;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        InitializeWeapons();
    }

    void InitializeWeapons()
    {
        if (weaponsParent == null)
        {
            Debug.LogError("❌ Error: No se asignó 'weaponsParent' en el Inspector.");
            return;
        }

        weapons = new GameObject[weaponsParent.childCount];
        for (int i = 0; i < weaponsParent.childCount; i++)
        {
            weapons[i] = weaponsParent.GetChild(i).gameObject;
            weapons[i].SetActive(false);
            Debug.Log($"✅ Arma registrada: {weapons[i].name} (Posición: {i + 1})");
        }
    }

    void Update()
    {
        HandleInput();
        UpdateActiveWeapon();
    }

    void FixedUpdate()
    {
        ApplyPhysicsMovement();
    }

    private void HandleInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        animator.SetFloat("horizontal", moveDirection.x);
        animator.SetFloat("vertical", moveDirection.z);
        

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            ApplyJump();
           
        }
    }

    private void ApplyPhysicsMovement()
    {
        if (moveDirection.magnitude >= 0.1f)
        {
            Vector3 moveDir = playerCamera.transform.right * moveDirection.x + playerCamera.transform.forward * moveDirection.z;
            moveDir.y = 0f;
            playerRigidbody.MovePosition(playerRigidbody.position + moveDir * moveSpeed * Time.fixedDeltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            playerRigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }

    private void ApplyJump()
    {
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetBool("isJumping", true);
        Debug.Log("Inicio de salto");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
            Debug.Log("🔵 Aterrizaje");
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
           
            isGrounded = false;
        }
    }

    void UpdateActiveWeapon()
    {
        int newWeaponID = WeaponWheelController.weaponID;

        if (newWeaponID != lastWeaponID && newWeaponID >= 0 && newWeaponID <= weapons.Length)
        {
            SwitchWeapon(newWeaponID);
        }
    }

    void SwitchWeapon(int weaponID)
    {
        if (weaponID == 0) return;

        foreach (var weapon in weapons)
        {
            if (weapon != null) weapon.SetActive(false);
        }

        int arrayIndex = weaponID - 1;
        if (arrayIndex < weapons.Length && weapons[arrayIndex] != null)
        {
            weapons[arrayIndex].SetActive(true);
            Debug.Log($"🔫 Arma activada: {weapons[arrayIndex].name} (ID: {weaponID})");

            WeaponController weaponController = weapons[arrayIndex].GetComponent<WeaponController>();
            Debug.Log($"🧐 Buscando WeaponController en {weapons[arrayIndex].name} → {(weaponController != null ? "✅ Encontrado" : "❌ No encontrado")}");

            if (weaponController != null)
            {
                PlayerCombat playerCombat = GetComponent<PlayerCombat>();
                if (playerCombat != null)
                {
                    playerCombat.SetCurrentWeapon(weaponController);
                    Debug.Log($"✅ {weapons[arrayIndex].name} asignada a PlayerCombat.");
                }
                else
                {
                    Debug.LogError("❌ No se encontró PlayerCombat en el jugador.");
                }
            }
            else
            {
                Debug.LogError($"❌ El arma {weapons[arrayIndex].name} no tiene WeaponController.");
            }
        }

        lastWeaponID = weaponID;
    }


}