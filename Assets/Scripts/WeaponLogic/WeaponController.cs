using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform shootPoint;
    public Weapon[] weapons;
    private int currentWeaponIndex = 0;

    void Start()
    {
        SwitchWeapon(0); // Comienza con la primera arma
    }

    void Update()
    {
        // Cambiar de arma con las teclas 1, 2 y 3
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchWeapon(0); // Cambiar a arma 1
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchWeapon(1); // Cambiar a arma 2
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchWeapon(2); // Cambiar a arma 3

        // Disparar con la tecla Control
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) // Puedes usar LeftControl o RightControl
        {
            Fire();
        }
    }

    void SwitchWeapon(int index)
    {
        if (index >= 0 && index < weapons.Length)
        {
            currentWeaponIndex = index;
            Debug.Log("Arma seleccionada: " + weapons[currentWeaponIndex].weaponName);
        }
    }

    void Fire()
    {
        weapons[currentWeaponIndex].Fire(shootPoint); // Disparar la bala desde el shootPoint
    }
}
