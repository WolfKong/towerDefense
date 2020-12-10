using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class PathChecker : MonoBehaviour
{
    [SerializeField] private GameObject noPathWarning;
    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameEvent buildingPlaced;
    [SerializeField] private RuntimeList buildingList;

    private Vector3 targetPosition;
    private NavMeshPath path;

    private void Start()
    {
        noPathWarning.SetActive(false);

        path = new NavMeshPath();
        targetPosition = target.position;

        buildingPlaced.Listen(OnBuildingPlaced);
    }

    private void OnDestroy()
    {
        noPathWarning.transform.DOKill();
        buildingPlaced.Unlisten(OnBuildingPlaced);
    }

    private void OnBuildingPlaced()
    {
        StartCoroutine(CheckPath());
    }

    private IEnumerator CheckPath()
    {
        yield return new WaitForSeconds(0.6f);

        agent.CalculatePath(targetPosition, path);

        if (path.status != NavMeshPathStatus.PathComplete)
        {
            DestroyNewestBuilding();
            ShowWarning();
        }
    }

    private void DestroyNewestBuilding()
    {
        var buildings = buildingList.Items;
        Destroy(buildings[buildings.Count - 1]);
    }

    private void ShowWarning()
    {
        noPathWarning.SetActive(true);

        noPathWarning.transform.DOShakePosition(2f).OnComplete(() => noPathWarning.SetActive(false));
    }
}
