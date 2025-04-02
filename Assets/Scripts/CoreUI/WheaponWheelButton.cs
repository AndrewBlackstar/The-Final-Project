// using UnityEngine;
// using UnityEngine.Rendering;
// using UnityEngine.UI;


// public class WheaponWheelButton : MonoBehaviour
// {
//     public int id;
//     Animator animator;
//     public Image selectedItem;
//     bool isSelected = false;
//     public Sprite icon;


//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
//         animator = GetComponent<Animator>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (isSelected) 
//         {
//             selectedItem.sprite = icon;

//         }
//     }

//     public void Selected()
//     {
//         isSelected = true;
//         WeaponWheelController.weaponID = id;
//     }

//     public void Deselected()
//     {
//         isSelected = false;
//         WeaponWheelController.weaponID = 0;
//     }

//     public void HoverEnter()
//     {
//         animator.SetBool("hover", true);
//     }

//     public void HoverExit()
//     {
//         animator.SetBool("hover", false);
//     }



// }





// using UnityEngine;
// using UnityEngine.UI;

// public class WheaponWheelButton : MonoBehaviour
// {
//     public int id;
//     Animator animator;
//     public Image selectedItem;
//     public Sprite icon;

//     void Start()
//     {
//         animator = GetComponent<Animator>();
//     }

//     // public void Selected()
//     // {
//     //     WeaponWheelController.weaponID = id;
//     //     selectedItem.sprite = icon; // Asigna la imagen directamente al seleccionar
//     // }
//     public void Selected()
//     {
//         WeaponWheelController.weaponID = id;
//         selectedItem.sprite = icon; // Actualizar la imagen en el Canvas

//         Debug.Log($"🔫 Arma {id} seleccionada en el Wheel. Imagen actualizada.");
//     }


//     // public void Deselected()
//     // {
//     //     WeaponWheelController.weaponID = 0;
//     // }

//     public void Deselected()
//     {
//         // Evitar que se desactive el arma cuando se usa Fire2
//         if (WeaponWheelController.weaponID != 0)
//         {
//             WeaponWheelController.weaponID = 0;
//         }
//     }


//     public void HoverEnter()
//     {
//         animator.SetBool("hover", true);
//     }

//     public void HoverExit()
//     {
//         animator.SetBool("hover", false);
//     }
// }


using UnityEngine;
using UnityEngine.UI;

public class WheaponWheelButton : MonoBehaviour
{
    public int id;
    Animator animator;
    public Image selectedItem;
    public Sprite icon;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Selected()
    {
        WeaponWheelController.weaponID = id;
        selectedItem.sprite = icon; // Actualizar la imagen en el Canvas

        Debug.Log($"🔫 Arma {id} seleccionada en el Wheel. Imagen actualizada.");
    }


    public void Deselected()
    {
        // Evitar que se desactive el arma cuando se usa Fire2
        if (WeaponWheelController.weaponID != 0)
        {
            WeaponWheelController.weaponID = 0;
        }
    }


    public void HoverEnter()
    {
        animator.SetBool("hover", true);
    }

    public void HoverExit()
    {
        animator.SetBool("hover", false);
    }
}
