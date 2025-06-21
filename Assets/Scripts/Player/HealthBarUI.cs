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

            // Keeps the health bar from rotating
            transform.rotation = Quaternion.identity;

            // Keeps the health bar from flipping
            if (transform.parent != null)
            {
                Vector3 parentScale = transform.parent.localScale;
                Vector3 localScale = transform.localScale;
                localScale.x = Mathf.Abs(localScale.x) * Mathf.Sign(parentScale.x);
                transform.localScale = localScale;
            }
        }
    }


}
