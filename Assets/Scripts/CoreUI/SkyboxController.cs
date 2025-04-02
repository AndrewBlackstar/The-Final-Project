using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    // Asigna el material que deseas usar como nuevo Skybox
    public Material newSkybox;

    // Método que se llamará desde Timeline
    public void ChangeSkybox()
    {
        RenderSettings.skybox = newSkybox;
    }
}
