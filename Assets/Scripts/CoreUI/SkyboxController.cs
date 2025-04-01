using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    // Asigna el material que deseas usar como nuevo Skybox
    public Material newSkybox;

    // M�todo que se llamar� desde Timeline
    public void ChangeSkybox()
    {
        RenderSettings.skybox = newSkybox;
    }
}
