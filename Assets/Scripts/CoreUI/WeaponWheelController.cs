using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    public Animator anim;
    bool weaponWheeSelected = false;
    public Image selectedItem;
    public Sprite noImage;
    public static int weaponID;
    GameObject player;
    Animator animator;
    PlayerCombat playerCombat;

    private void Start()
    {
        player = GameObject.Find("Player armed");
        animator = player.GetComponent<Animator>();
        playerCombat = player.GetComponent<PlayerCombat>();

        // Asignar un valor predeterminado al weaponID (por ejemplo, 1 para el cañón)
        weaponID = 1;

        // Inicializar el arma al inicio del juego
        UpdateCanvasWeapon(1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            weaponWheeSelected = !weaponWheeSelected;
            Time.timeScale = weaponWheeSelected ? 0f : 1f;
            anim.SetBool("OpenWeaponWheel", weaponWheeSelected);

            if (!weaponWheeSelected) // Si se cierra el menú, actualizar el arma en el Canvas
            {
                UpdateCanvasWeapon(weaponID);
            }
        }

        if (weaponWheeSelected)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            anim.SetBool("OpenWeaponWheel", true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            anim.SetBool("OpenWeaponWheel", false);
        }
    }

    void UpdateCanvasWeapon(int id)
    {
        WeaponController newWeapon = null;
        weaponID = id;
        switch (id)
        {
            
            case 1: // Cañón
                animator.SetBool("hasWeapon", true);
                animator.SetBool("hasSword", false);
                newWeapon = player.transform.Find("Cannon")?.GetComponent<WeaponController>();
                Debug.Log("🔫 Cañón equipado.");
                break;
            case 2: // Pistola
                animator.SetBool("hasWeapon", true);
                animator.SetBool("hasSword", false);
                newWeapon = player.transform.Find("Pistol")?.GetComponent<WeaponController>();
                Debug.Log("🔫 Pistola equipada.");
                break;
            case 3: // Espada
                animator.SetBool("hasWeapon", false);
                animator.SetBool("hasSword", true);
                newWeapon = null;
                Debug.Log("🗡️ Espada equipada.");
                break;
        }

        // Asignar el arma a PlayerCombat
        playerCombat.SetCurrentWeapon(newWeapon);
    }

}
