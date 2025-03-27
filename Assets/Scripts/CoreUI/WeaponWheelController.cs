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
            
        }

        if (weaponWheeSelected)
        {
            
            anim.SetBool("OpenWeaponWheel", true);
        }
        else
        {
            
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
