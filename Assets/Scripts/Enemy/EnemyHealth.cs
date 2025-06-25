using UnityEngine;

public class EnemyHealth : Health
{
    public override void ApplyDamage(float damage)
    {
        base.ApplyDamage(damage);
    }

    protected override void Die()
    {
        Debug.Log($"{gameObject.name} ���� (����).");
        Destroy(gameObject); // ���������� �����
    }
    //private void Start()
    //{
    //    base.Start();
    //    currentHealth = maxHealth;

    //    // �������������� �������� HealthBarUI
    //    HealthBarUI bar = GetComponentInChildren<HealthBarUI>();
    //    if (bar != null)
    //    {
    //        bar.targetHealth = this;
    //    }
    //}
}
