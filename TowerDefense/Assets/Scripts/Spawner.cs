using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameEvent waveEnded;

    private const float checkWaveEnd = 2;

    private int currentWaveIndex = -1;
    private int spawnCount = 0;
    private float time = 0;

    private void Update()
    {
        if (levelData.CurrentWave != currentWaveIndex)
        {
            currentWaveIndex = levelData.CurrentWave;
            StartCoroutine(SpawnWave(levelData.Waves[currentWaveIndex]));
        }

        time += Time.deltaTime;

        if (time > checkWaveEnd)
        {
            time = 0;
            CheckWaveEnd();
        }
    }

    private IEnumerator SpawnWave(MiniWaveData data)
    {
        spawnCount = 0;
        var spawnPoint = spawnPoints[(int)data.SpawnPoint];
        var waitInterval = new WaitForSeconds(data.IntervalBetweenSpawns);

        foreach (var enemy in data.Enemies)
        {
            SpawnEnemy(spawnPoint, enemy);
            yield return waitInterval;
        }

        waveEnded.Trigger();
    }

    private void SpawnEnemy(Transform spawnPoint, EnemyData data)
    {
        spawnCount++;

        var enemy = Instantiate(data.Prefab, transform);
        enemy.transform.position = spawnPoint.position;
        enemy.transform.forward = Vector3.right;
        enemy.name = $"{data.name}_wave{currentWaveIndex}_{spawnCount}";

        var enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.SetData(data);
        enemyScript.SetTarget(targetTransform.position);
    }

    private void CheckWaveEnd()
    {
    }
}
