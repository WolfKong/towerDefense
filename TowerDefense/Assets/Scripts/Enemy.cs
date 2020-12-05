using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent.SetDestination(targetTransform.position);
    }
}
