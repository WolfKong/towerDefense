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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Blast")
        {
            var blast = collider.gameObject.GetComponent<Blast>();
            ApplyBlast(blast.TowerData);
        }
    }

    private void ApplyBlast(TowerData towerData)
    {
        Debug.LogWarning($"PV-APPLY BLAST hp {hp},  dmg {towerData.Damage}");
        hp = Mathf.RoundToInt(hp - towerData.Damage);

        if (hp < 0)
            Destroy(gameObject);
        else
            healthBar.SetCurrentHealth(hp);
    }
}
