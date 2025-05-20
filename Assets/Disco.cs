using UnityEngine;

public class Disco : MonoBehaviour
{
    public Light spotlight;
    public float colorChangeSpeed = 1.0f;

    void Update()
    {
        float hue = Mathf.PingPong(Time.time * colorChangeSpeed, 1f);
        Color color = Color.HSVToRGB(hue, 1f, 1f);
        spotlight.color = color;
    }
}
