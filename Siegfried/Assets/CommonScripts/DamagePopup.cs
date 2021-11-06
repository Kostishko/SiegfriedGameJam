using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public static void Create(Vector2 position, int damage)
    {
        var popup = Instantiate(GameAssets.instance.DamagePopup, position, Quaternion.identity);
        popup.GetComponent<DamagePopup>().Setup(damage);
    }

    public Color color_i, color_f;
    public Vector3 initialOffset, finalOffset; //position to drift to, relative to the gameObject's local origin
    public float fadeDuration;
    private float fadeStartTime;
    private TextMeshPro _text;
    private void Awake()
    {
        _text = GetComponent<TextMeshPro>();
    }
    public void Setup(int damage)
    {
        _text.text = damage.ToString();
        fadeStartTime = Time.time;
    }

    void Update()
    {
        float progress = (Time.time - fadeStartTime) / fadeDuration;
        if (progress <= 1)
        {
            //lerp factor is from 0 to 1, so we use (FadeExitTime-Time.time)/fadeDuration
            transform.localPosition = Vector3.Lerp(initialOffset, finalOffset, progress);
            _text.color = Color.Lerp(color_i, color_f, progress);
        }
        else Destroy(gameObject);
    }

}
