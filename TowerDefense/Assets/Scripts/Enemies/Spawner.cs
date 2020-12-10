using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const float checkWaveEndInterval = 2;

    [SerializeField] private LevelData levelData;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private SpawnPoint[] spawnPoints;
    [SerializeField] private GameEvent waveEndedEvent;
    [SerializeField] private GameEvent waveStartedEvent;

    private List<GameObject> aliveEnemies = new List<GameObject>();
    private List<GameObject> removeList = new List<GameObject>();

    private int currentWaveIndex = -1;
    private int spawnCount = 0;
    private bool finishedSpawningWave;

    private void Start()
    {
        waveStartedEvent.Listen(OnWaveStarted);

        ShowSpawnPointDanger(0);
        InvokeRepeating(nameof(CheckWaveEnd), 0, checkWaveEndInterval);
    }

    private void OnDestroy()
    {
        waveStartedEvent.Unlisten(OnWaveStarted);
    }

    private void OnWaveStarted()
    {
        currentWaveIndex = levelData.CurrentWave;
        StartCoroutine(SpawnWave(levelData.Waves[currentWaveIndex]));
    }

    private void ShowSpawnPointDanger(int waveIndex)
    {
        var spawnPositionIndex = (int)levelData.Waves[waveIndex].SpawnPosition;
        for (var i = 0; i < spawnPoints.Length; i++)
            spawnPoints[i].SetDanger(i == spawnPositionIndex);
    }

    private IEnumerator SpawnWave(WaveData data)
    {
        spawnCount = 0;

        var spawnTransform = spawnPoints[(int)data.SpawnPosition].transform;
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
        {
            finishedSpawningWave = false;
            waveEndedEvent.Trigger();
            ShowSpawnPointDanger(levelData.CurrentWave);
        }
    }
}
