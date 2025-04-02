// using UnityEngine;
// using UnityEngine.UI;

// public class WeaponWheelController : MonoBehaviour
// {
//     public Animator anim;
//     bool weaponWheeSelected = false;
//     public Image selectedItem;
//     public Sprite noImage;
//     public static int weaponID;

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Tab))
//         {
//             weaponWheeSelected = !weaponWheeSelected;
//             Time.timeScale = weaponWheeSelected ? 0f : 1f;
//             anim.SetBool("OpenWeaponWheel", weaponWheeSelected);
//         }

//         if (weaponWheeSelected)
//         {
//             Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor
//             Cursor.visible = true; // Hace visible el cursor
//             anim.SetBool("OpenWeaponWheel", true);
//         }
//         else
//         {
//             Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor
//             Cursor.visible = false; // Oculta el cursor
//             anim.SetBool("OpenWeaponWheel", false);
//         }

//         switch (weaponID)
//         {
//             case 0:
//                 selectedItem.sprite = noImage;
//                 break;
//             case 1:
//                 Debug.Log("Canon");
//                 break;
//             case 2:
//                 Debug.Log("pistol");
//                 break;
//             case 3:
//                 Debug.Log("sandal");
//                 break ;
//         }


//     }
// }


// using UnityEngine;
// using UnityEngine.UI;

// public class WeaponWheelController : MonoBehaviour
// {
//     public Animator anim;
//     bool weaponWheeSelected = false;
//     public Image selectedItem;
//     public Sprite noImage;
//     public static int weaponID;

//     // void Update()
//     // {
//     //     if (Input.GetKeyDown(KeyCode.Tab))
//     //     {
//     //         weaponWheeSelected = !weaponWheeSelected;
//     //         Time.timeScale = weaponWheeSelected ? 0f : 1f;
//     //         anim.SetBool("OpenWeaponWheel", weaponWheeSelected);
//     //     }

//     //     if (weaponWheeSelected)
//     //     {
//     //         Cursor.lockState = CursorLockMode.None;
//     //         Cursor.visible = true;
//     //         anim.SetBool("OpenWeaponWheel", true);

//     //         // Solo actualiza el sprite si el menú está abierto
//     //         switch (weaponID)
//     //         {
//     //             case 0:
//     //                 selectedItem.sprite = noImage;
//     //                 break;
//     //             case 1:
//     //                 Debug.Log("Canon");
//     //                 break;
//     //             case 2:
//     //                 Debug.Log("Pistol");
//     //                 break;
//     //             case 3:
//     //                 Debug.Log("Sandal");
//     //                 break;
//     //         }
//     //     }
//     //     else
//     //     {
//     //         Cursor.lockState = CursorLockMode.Locked;
//     //         Cursor.visible = false;
//     //         anim.SetBool("OpenWeaponWheel", false);
//     //     }
//     // }


//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Tab))
//         {
//             weaponWheeSelected = !weaponWheeSelected;
//             Time.timeScale = weaponWheeSelected ? 0f : 1f;
//             anim.SetBool("OpenWeaponWheel", weaponWheeSelected);

//             if (!weaponWheeSelected) // Si se cierra el menú, actualizar el arma en el Canvas
//             {
//                 UpdateCanvasWeapon();
//             }
//         }

//         if (weaponWheeSelected)
//         {
//             Cursor.lockState = CursorLockMode.None;
//             Cursor.visible = true;
//             anim.SetBool("OpenWeaponWheel", true);
//         }
//         else
//         {
//             Cursor.lockState = CursorLockMode.Locked;
//             Cursor.visible = false;
//             anim.SetBool("OpenWeaponWheel", false);
//         }
//     }

//     // Nuevo método para sincronizar el Canvas con el arma seleccionada
//     void UpdateCanvasWeapon()
//     {
//         switch (weaponID)
//         {
//             case 0:
//                 selectedItem.sprite = noImage;
//                 break;
//             case 1:
//                 Debug.Log("🔫 Canon seleccionado en el Canvas.");
//                 break;
//             case 2:
//                 Debug.Log("🔫 Pistola seleccionada en el Canvas.");
//                 break;
//             case 3:
//                 Debug.Log("🗡️ Espada seleccionada en el Canvas.");
//                 break;
//         }
//     }

// }


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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            weaponWheeSelected = !weaponWheeSelected;
            Time.timeScale = weaponWheeSelected ? 0f : 1f;
            anim.SetBool("OpenWeaponWheel", weaponWheeSelected);

            if (!weaponWheeSelected) // Si se cierra el menú, actualizar el arma en el Canvas
            {
                UpdateCanvasWeapon();
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

    // Nuevo método para sincronizar el Canvas con el arma seleccionada
    void UpdateCanvasWeapon()
    {
        switch (weaponID)
        {
            case 0:
                selectedItem.sprite = noImage;
                break;
            case 1:
                Debug.Log("🔫 Canon seleccionado en el Canvas.");
                break;
            case 2:
                Debug.Log("🔫 Pistola seleccionada en el Canvas.");
                break;
            case 3:
                Debug.Log("🗡️ Espada seleccionada en el Canvas.");
                break;
        }
    }

}
