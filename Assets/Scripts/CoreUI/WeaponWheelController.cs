using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    public Animator anim;
    bool weaponWheeSelected = false;
    public Image selectedItem;
    public Sprite noImage;
    public static int weaponID;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetButtonDown("Fire3"))
        {
            weaponWheeSelected = !weaponWheeSelected;
            Time.timeScale = weaponWheeSelected ? 0f : 1f;
            anim.SetBool("OpenWeaponWheel", weaponWheeSelected);
        }

        if (weaponWheeSelected)
        {
            Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor
            Cursor.visible = true; // Hace visible el cursor
            anim.SetBool("OpenWeaponWheel", true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor
            Cursor.visible = false; // Oculta el cursor
            anim.SetBool("OpenWeaponWheel", false);
        }

        switch (weaponID)
        {
            case 0:
                selectedItem.sprite = noImage;
                break;
            case 1:
                Debug.Log("Canon");
                break;
            case 2:
                Debug.Log("pistol");
                break;
            case 3:
                Debug.Log("sandal");
                break ;
        }


    }
}