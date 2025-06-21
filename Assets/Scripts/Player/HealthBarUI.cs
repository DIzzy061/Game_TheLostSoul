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

            // ������������� ������� � �������, ����� �� ��������
            transform.rotation = Quaternion.identity;

            // ������������� ���������� ���� (������� �� X = �������������)
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }


}
