using UnityEngine;

public class LevelConditions : MonoBehaviour
{
    [SerializeField] private FloatVariable goalBuildingHealth;
    [SerializeField] private LevelData levelData;
    [SerializeField] private GameEvent waveEnded;

    private bool gameOver;

    private void Start()
    {
        waveEnded.Listen(OnWaveEnd);

        gameOver = false;
        goalBuildingHealth.Value = levelData.GoalHealth;
        levelData.CurrentWave = 0;
    }

    private void OnDestroy()
    {
        waveEnded.Unlisten(OnWaveEnd);
    }

    private void Update()
    {
        if (!gameOver && goalBuildingHealth.Value <= 0)
        {
            gameOver = true;
            SceneLoader.LoadScene("GameOver");
        }
    }

    private void OnWaveEnd()
    {
        if (levelData.CurrentWave == levelData.TotalWaves - 1)
        {
            gameOver = true;
            SceneLoader.LoadScene("GameOver");
        }
        else
        {
            levelData.CurrentWave += 1;
        }
    }
}
