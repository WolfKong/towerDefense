using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private HealthBar healthBar;

    private float speed;
    private int health;
    private bool debuff;

    public void SetData(EnemyData data)
    {
        health = data.Health;
        healthBar.SetMaxHealth(data.Health);
        speed = data.Speed;
        UpdateSpeed();
    }

    public void SetTarget(Vector3 targetPosition)
    {
        navMeshAgent.SetDestination(targetPosition);
    }

    private void UpdateHealth()
    {
        if (health <= 0)
            Destroy(gameObject);
        else
            healthBar.SetCurrentHealth(health);
    }

    private void UpdateSpeed()
    {
        navMeshAgent.speed = speed;
    }

    public bool HasDebuff() => debuff;

    public void ApplyBlast(TowerData towerData)
    {
        if (towerData.DamageType == DamageType.Health)
        {
            health = Mathf.RoundToInt(health - towerData.Damage);
            UpdateHealth();
        }
        else if (towerData.DamageType == DamageType.Speed)
        {
            speed *= towerData.Damage;
            debuff = true;
            UpdateSpeed();
        }
    }
}
