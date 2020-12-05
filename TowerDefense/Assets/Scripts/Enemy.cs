using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;

    private int hp;

    public void SetData(EnemyData data)
    {
        hp = data.Health;
        navMeshAgent.speed = data.Speed;
    }

    public void SetTarget(Vector3 targetPosition)
    {
        navMeshAgent.SetDestination(targetPosition);
    }
}
