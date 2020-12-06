using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private EnemyData[] enemyDatas;

    private int spawnCount = 0;
    private float time = 0;

    private void Update()
    {
        time += Time.deltaTime;

        if (time > 2)
        {
            time = 0;
            Spawn(spawnPoints[Random.Range(0, spawnPoints.Length)]);
        }
    }

    private void Spawn(Transform spawnPoint)
    {
        spawnCount++;
        var data = enemyDatas[Random.Range(0, enemyDatas.Length)];

        var enemy = Instantiate(data.Prefab, transform);
        enemy.transform.position = spawnPoint.position;
        enemy.transform.forward = Vector3.right;
        enemy.name = $"{data.name}_{spawnCount}";

        var enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.SetData(data);
        enemyScript.SetTarget(targetTransform.position);
    }
}
