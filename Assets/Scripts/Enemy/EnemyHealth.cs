using UnityEngine;

public class EnemyHealth : Health
{
    public override void ApplyDamage(float damage)
    {
        base.ApplyDamage(damage);
    }

    protected override void Die()
    {
        Debug.Log($"{gameObject.name} умер (враг).");
        Destroy(gameObject); // уничтожаем врага
    }
    //private void Start()
    //{
    //    base.Start();
    //    currentHealth = maxHealth;

    //    // Автоматическая привязка HealthBarUI
    //    HealthBarUI bar = GetComponentInChildren<HealthBarUI>();
    //    if (bar != null)
    //    {
    //        bar.targetHealth = this;
    //    }
    //}
}
