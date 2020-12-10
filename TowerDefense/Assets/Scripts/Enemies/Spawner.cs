using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const float checkWaveEnd = 2;

    [SerializeField] private LevelData levelData;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private SpawnPoint[] spawnPoints;
    [SerializeField] private GameEvent waveEnded;

    private List<GameObject> aliveEnemies = new List<GameObject>();
    private List<GameObject> removeList = new List<GameObject>();

    private int currentWaveIndex = -1;
    private int spawnCount = 0;
    private bool finishedSpawningWave;
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

    private IEnumerator SpawnWave(WaveData data)
    {
        finishedSpawningWave = false;
        spawnCount = 0;

        var spawnPositionIndex = (int)data.SpawnPosition;
        for (var i = 0; i < spawnPoints.Length; i++)
            spawnPoints[i].SetDanger(i == spawnPositionIndex);

        var spawnTransform = spawnPoints[spawnPositionIndex].transform;
        var miniWaveInterval = new WaitForSeconds(data.IntervalBetweenMiniWaves);

        foreach (var miniWave in data.MiniWaves)
        {
            var spawnInterval = new WaitForSeconds(miniWave.IntervalBetweenSpawns);

            foreach (var enemy in miniWave.Enemies)
            {
                SpawnEnemy(spawnTransform, enemy);
                yield return spawnInterval;
            }

            yield return miniWaveInterval;
        }

        finishedSpawningWave = true;
    }

    private void SpawnEnemy(Transform spawnPoint, EnemyData data)
    {
        spawnCount++;

        var enemy = Instantiate(data.Prefab, transform);
        aliveEnemies.Add(enemy);

        enemy.transform.position = spawnPoint.position;
        enemy.transform.forward = Vector3.right;
        enemy.name = $"{data.name}_wave{currentWaveIndex}_{spawnCount}";

        var enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.SetData(data);
        enemyScript.SetTarget(targetTransform.position);
    }

    private void CheckWaveEnd()
    {
        for (var i = 0; i < aliveEnemies.Count; i++)
        {
            if (!aliveEnemies[i])
                removeList.Add(aliveEnemies[i]);
        }

        foreach (var enemy in removeList)
            aliveEnemies.Remove(enemy);

        removeList.Clear();

        if (finishedSpawningWave && aliveEnemies.Count == 0)
            waveEnded.Trigger();
    }
}
