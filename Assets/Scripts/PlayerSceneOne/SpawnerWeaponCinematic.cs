using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class SpawnerWeaponCinematic : MonoBehaviour
{
    public GameObject[] WeaponPrefab;
    public Transform spawnpont;
    GameObject currentWeapon;
    public ParticleSystem spawnVfx;

    private void Start()
    {
        currentWeapon.transform.Rotate(0, 0, 2);
    }

    public void ShowWeaponSequence( )
    {
        StartCoroutine(SpawnSequence(2));
    }

    IEnumerator SpawnSequence(float time)
    {
        foreach (var weapon in WeaponPrefab)
        {
            if (currentWeapon != null)
            {
                Destroy(currentWeapon);
            }

            // Instancia el arma
            currentWeapon = Instantiate(weapon, spawnpont.position, Quaternion.identity);

            // Reproduce el VFX
            if (spawnVfx != null)
            {
                ParticleSystem vfx = Instantiate(spawnVfx, spawnpont.position, Quaternion.identity);
                vfx.Play();
                Destroy(vfx.gameObject, vfx.main.duration + vfx.main.startLifetime.constantMax); // Limpieza automática
            }

            yield return new WaitForSeconds(time);
        }

        HideWeapon();
    }


    public void HideWeapon()
    {
        if(currentWeapon != null)
        {
            Destroy(currentWeapon);
        }
    }
}
