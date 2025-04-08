using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    private WeaponController currentWeapon;
    private float attackCooldown = 0.5f;
    MovementPlayer player;

    [SerializeField] ParticleSystem fireVfx;
    

    [SerializeField] private GameObject hitBox;

    public bool isMeleeDefending { get; private set; } = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        player=GameObject.Find("Player armed").GetComponent<MovementPlayer>();
        hitBox.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireVfx.Play();
            HandleFire();
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            HandleMelee();
        }

        if (animator.GetBool("isAttacking"))
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.normalizedTime >= 1.0f && stateInfo.IsTag("MeleeAttack"))
            {
                ResetAttack();
            }
        }
    }

    private void HandleFire()
    {
        if (animator.GetBool("hasSword")) return;

        

        if (animator.GetBool("hasWeapon") && currentWeapon != null)
        {
            currentWeapon.FireBullet();
        }
        
    }
    private void HandleMelee()
    {
        animator.SetBool("isAttacking", true);

        isMeleeDefending = true; // ← Activamos la defensa

        Invoke(nameof(ActivateHitbox), 0.2f);
        Invoke(nameof(DeactivateHitbox), 0.4f);

        Invoke(nameof(ResetAttack), attackCooldown);
        Invoke(nameof(DisableDefense), attackCooldown); // ← Desactivamos la defensa después del ataque
    }

    private void ActivateHitbox()
    {
        hitBox.SetActive(true);
    }

    private void DisableDefense()
    {
        isMeleeDefending = false;
    }

    private void DeactivateHitbox()
    {
        hitBox.SetActive(false);
    }

    private void ResetAttack()
    {
        animator.SetBool("isAttacking", false);
    }

    public void SetCurrentWeapon(WeaponController newWeapon)
    {
        if (newWeapon == null)
        {
            
            return;
        }

        currentWeapon = newWeapon;
        
    }

    public void EquipDefaultWeaponFromSignal()
    {
        WeaponWheelController.weaponID = 1;
        player.SwitchWeapon(WeaponWheelController.weaponID);
    }
}
