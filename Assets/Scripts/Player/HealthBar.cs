using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage;
    public float maxWidth = 1.8f; // выставь это значение как ширину HealthBar_Fill

    // percent — значение от 0 до 1
    public void SetHealth(float percent)
    {
        percent = Mathf.Clamp01(percent);
        fillImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxWidth * percent);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
