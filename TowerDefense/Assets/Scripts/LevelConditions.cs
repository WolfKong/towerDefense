using System.Collections;
using UnityEngine;

public class LevelConditions : MonoBehaviour
{
    [SerializeField] private FloatVariable goalBuildingHealth;
    [SerializeField] private LevelData levelData;
    [SerializeField] private GameEvent waveEndedEvent;
    [SerializeField] private GameEvent waveStartedEvent;

    private Coroutine intervalCoroutine;
    private bool gameOver;

    private void Start()
    {
        waveStartedEvent.Listen(OnWaveStarted);
        waveEndedEvent.Listen(OnWaveEnded);

        gameOver = false;
        goalBuildingHealth.Value = levelData.GoalHealth;
        levelData.CurrentWave = 0;

        intervalCoroutine = StartCoroutine(WaitWaveInterval());
    }

    private void OnDestroy()
    {
        waveStartedEvent.Listen(OnWaveStarted);
        waveEndedEvent.Unlisten(OnWaveEnded);
    }

    private void Update()
    {
        if (!gameOver && goalBuildingHealth.Value <= 0)
        {
            gameOver = true;
            SceneLoader.LoadScene("GameOver");
        }
    }

    private IEnumerator WaitWaveInterval()
    {
        var interval = levelData.CurrentWave == 0 ? levelData.InitialInterval : levelData.IntervalBetweenWaves;

        yield return new WaitForSeconds(interval);

        waveStartedEvent.Trigger();
    }

    private void OnWaveStarted()
    {
        if (intervalCoroutine != null)
            StopCoroutine(intervalCoroutine);
    }

    private void OnWaveEnded()
    {
        if (levelData.CurrentWave == levelData.TotalWaves - 1)
        {
            gameOver = true;
            SceneLoader.LoadScene("LevelComplete");
        }
        else
        {
            levelData.CurrentWave += 1;
            intervalCoroutine = StartCoroutine(WaitWaveInterval());
        }
    }
}
