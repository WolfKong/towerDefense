using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private HealthBar healthBar;

    private int hp;

    public void SetData(EnemyData data)
    {
        hp = data.Health;
        healthBar.SetMaxHealth(data.Health);
        navMeshAgent.speed = data.Speed;
    }

    public void SetTarget(Vector3 targetPosition)
    {
        navMeshAgent.SetDestination(targetPosition);
    }

    public void ApplyBlast(TowerData towerData)
    {
        hp = Mathf.RoundToInt(hp - towerData.Damage);

        if (hp <= 0)
            Destroy(gameObject);
        else
            healthBar.SetCurrentHealth(hp);
    }
}
