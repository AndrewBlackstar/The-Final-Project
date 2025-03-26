using UnityEngine;
using TMPro;

public class TextDepthAnimation : MonoBehaviour
{
    public RectTransform textTransform;
    public float scaleMin = 0.8f;
    public float scaleMax = 1.2f;
    public float speed = 1.5f;

    void Update()
    {
        float scale = Mathf.Lerp(scaleMin, scaleMax, Mathf.PingPong(Time.time * speed, 1));
        textTransform.localScale = new Vector3(scale, scale, 1);
    }
}
