using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    private WeaponController currentWeapon;
    private float attackCooldown = 0.5f;
    MovementPlayer player;

    void Start()
    {
        animator = GetComponent<Animator>();
        player=GameObject.Find("Player armed").GetComponent<MovementPlayer>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
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

        Debug.Log($"🔍 Intentando disparar. Arma actual: {currentWeapon?.gameObject.name ?? "❌ Ninguna asignada"}");

        if (animator.GetBool("hasWeapon") && currentWeapon != null)
        {
            currentWeapon.FireBullet();
        }
        else
        {
            Debug.LogError("❌ No hay arma asignada a PlayerCombat.");
        }
    }
    private void HandleMelee()
    {
        animator.SetBool("isAttacking", true);

        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void ResetAttack()
    {
        animator.SetBool("isAttacking", false);
    }

    public void SetCurrentWeapon(WeaponController newWeapon)
    {
        if (newWeapon == null)
        {
            Debug.LogError("❌ SetCurrentWeapon recibió un arma NULL.");
            return;
        }

        currentWeapon = newWeapon;
        Debug.Log($"✅ PlayerCombat ha recibido el arma: {newWeapon.gameObject.name}");
    }

    public void EquipDefaultWeaponFromSignal()
    {
        WeaponWheelController.weaponID = 1;
        player.SwitchWeapon(WeaponWheelController.weaponID);
    }
}
