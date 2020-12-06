using UnityEngine;

public class LevelConditions : MonoBehaviour
{
    [SerializeField] private FloatVariable goalBuildingHealth;
    [SerializeField] private LevelData levelData;

    private bool gameOver;

    void Start()
    {
        gameOver = false;
        goalBuildingHealth.Value = levelData.GoalHealth;
    }

    private void Update()
    {
        if (!gameOver && goalBuildingHealth.Value <= 0)
        {
            gameOver = true;
            SceneLoader.LoadScene("GameOver");
        }
    }
}
