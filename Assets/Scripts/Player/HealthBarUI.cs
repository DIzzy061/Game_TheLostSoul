using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Health targetHealth;
    public Image fillImage;
    private void Update()
    {
        if (targetHealth != null && fillImage != null)
        {
            float ratio = targetHealth.currentHealth / targetHealth.maxHealth;
            fillImage.fillAmount = Mathf.Clamp01(ratio);

            // Зафиксировать поворот и масштаб, чтобы не флипался
            transform.rotation = Quaternion.identity;

            // Принудительно сбрасываем флип (масштаб по X = положительный)
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }


}
