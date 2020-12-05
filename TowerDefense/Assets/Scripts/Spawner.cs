using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Enemy enemyPrefab;

    private float time = 0;

    private void Update()
    {
        time += Time.deltaTime;

        if (time > 1)
        {
            time = 0;
            Spawn(spawnPoints[Random.Range(0, spawnPoints.Length)]);
        }
    }

    private void Spawn(Transform spawnPoint)
    {
        var enemy = Instantiate(enemyPrefab, transform);
        enemy.transform.position = spawnPoint.position;
        enemy.transform.forward = Vector3.right;
        enemy.SetTarget(targetTransform.position);
    }
}
